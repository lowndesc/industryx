// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Samples.AdtIothub
{
    public static class DeviceTelemetryToTwinFunc
    {
        private static string adtAppId = "https://digitaltwins.azure.net";
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL", EnvironmentVariableTarget.Process);
        private static readonly HttpClient singletonHttpClientInstance = new HttpClient();

        [FunctionName("DeviceTelemetryToTwinFunc")]
        public static async Task Run(
            [EventHubTrigger("deviceevents", Connection = "EVENTHUB_CONNECTIONSTRING")] EventData eventData, ILogger log)
        {
            log.LogInformation($"C# function triggered to process a message: {eventData}");
            log.LogInformation($"C# function triggered to process a message: {Encoding.UTF8.GetString(eventData.Body)}");
            // Metadata accessed by binding to EventData
            log.LogInformation($"EnqueuedTimeUtc={eventData.SystemProperties.EnqueuedTimeUtc}");
            log.LogInformation($"SequenceNumber={eventData.SystemProperties.SequenceNumber}");
            log.LogInformation($"Offset={eventData.SystemProperties.Offset}");

            //var exceptions = new List<Exception>(events.Length);

            // Create Digital Twin client
            var cred = new ManagedIdentityCredential(adtAppId);
            var client = new DigitalTwinsClient(
                new Uri(adtInstanceUrl),
                cred,
                new DigitalTwinsClientOptions
                {
                    Transport = new HttpClientTransport(singletonHttpClientInstance)
                });

            try
            {
                log.LogInformation($"EventData: {System.Text.Json.JsonSerializer.Serialize(eventData)}");

                // Get message body
                string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                log.LogInformation($"MessageBody: {messageBody}");

                // Reading Device ID from message headers
                JObject jbody = (JObject)JsonConvert.DeserializeObject(messageBody);
                string deviceId = eventData.SystemProperties["iothub-connection-device-id"].ToString();
                log.LogInformation($"DeviceId: {deviceId}");

                // Extracting temperature from device telemetry
                double temperature = Convert.ToDouble(jbody["temperature"].ToString());

                // Update device Temperature property
                var updateTwinData = new JsonPatchDocument();
                updateTwinData.AppendReplace("/Temperature", temperature);
                log.LogInformation($"ADT Patch Document: {updateTwinData.ToString()}");
                await client.UpdateDigitalTwinAsync(deviceId, updateTwinData);

                log.LogInformation($"Updated Temperature of device Twin {deviceId} to: {temperature}");
            }
            catch (Exception e)
            {
                // We need to keep processing the rest of the batch - capture this exception and continue.
               log.LogError(e, "Function error");
            }
        }
    }
}
