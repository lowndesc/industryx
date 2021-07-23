using System;
using Azure;
using System.Net.Http;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avanade.TwinSync
{
    public class IoTHubtoTwins
    {
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("IoTHubtoTwins")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            if (adtInstanceUrl == null) log.LogError("Application setting \"ADT_SERVICE_URL\" not set");

            try
            {
                // Authenticate with Digital Twins
                var cred = new ManagedIdentityCredential("https://digitaltwins.azure.net");
                var client = new DigitalTwinsClient(
                    new Uri(adtInstanceUrl),
                    cred,
                    new DigitalTwinsClientOptions { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {
                    log.LogInformation(eventGridEvent.Data.ToString());

                    // <Find_device_ID_and_values>
                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var temperature = deviceMessage["body"]["temperature"];
                    var humidity = deviceMessage["body"]["humidity"];
                    var power = deviceMessage["body"]["power"];
                    var vibration = deviceMessage["body"]["vibration"];
                    var uptime = deviceMessage["body"]["uptime"];
                    var capacity = deviceMessage["body"]["capacity"];
                    var wait = deviceMessage["body"]["wait"];
                    var delay = deviceMessage["body"]["delay"];
                    var arrivalRate = deviceMessage["body"]["arrivalRate"];
                    var counter = deviceMessage["body"]["Counter"].Value<int>(); // counter must be int
                    // </Find_device_ID_and_values>

                    // Correct integer telemetry values by dividing by 100
                    Random rnd = new Random();
                    int randomFactor = rnd.Next(3, 18); // a random integer from 3 to 17
                    var seed = (counter % 99) % randomFactor; // creates a seed value using the random factor, more frequent occurences at lower end
                    var t = temperature.Value<double>() / 100;
                    var h = humidity.Value<double>() / 100;
                    var p = power.Value<double>() / 100;
                    var v = vibration.Value<double>() / 100;
                    var u = uptime.Value<double>() / 100;
                    var c = (int)Math.Round(capacity.Value<double>() / 100); // round capacity to the nearest integer, even integer if midpoint
                    var w = wait.Value<double>() / 100;
                    var d = delay.Value<double>() / 100;
                    var ar = (17*Math.Sin(arrivalRate.Value<double>() / 100) + seed) / 15; // creates a more realistic arrival rate curve

                    log.LogInformation($"Device:{deviceId} Counter: [{counter}]");
                    log.LogInformation($"Device:{deviceId} Temperature: {t}");
                    log.LogInformation($"Device:{deviceId} Humidity: {h}");
                    log.LogInformation($"Device:{deviceId} Power: {p}");
                    log.LogInformation($"Device:{deviceId} Vibration: {v}");
                    log.LogInformation($"Device:{deviceId} Uptime: {u}");
                    log.LogInformation($"Device:{deviceId} Capacity: {c}");
                    log.LogInformation($"Device:{deviceId} Wait: {w}");
                    log.LogInformation($"Device:{deviceId} Delay: {d}");
                    log.LogInformation($"Device:{deviceId} ArrivalRate: {ar}");

                    // <Update_twin_with_device_values>
                    var updateTwinData = new JsonPatchDocument();
                    updateTwinData.AppendReplace("/Temperature", t);
                    updateTwinData.AppendReplace("/Humidity", h);
                    updateTwinData.AppendReplace("/Power", p);
                    updateTwinData.AppendReplace("/Vibration", v);
                    updateTwinData.AppendReplace("/Uptime", u);
                    updateTwinData.AppendReplace("/Capacity", c);
                    updateTwinData.AppendReplace("/Wait", w);
                    updateTwinData.AppendReplace("/Delay", d);
                    updateTwinData.AppendReplace("/ArrivalRate", ar);
                    await client.UpdateDigitalTwinAsync(deviceId, updateTwinData);
                    await client.PublishTelemetryAsync(deviceId,null, new JObject
                        {
                            { "Temperature", t },
                            { "Humidity", h },
                            { "Power", p },
                            { "Vibration", v },
                            { "Uptime", u },
                            { "Capacity", c },
                            { "Wait", w },
                            { "Delay", d },
                            { "ArrivalRate", ar }
                        }.ToString());
                    // </Update_twin_with_device_values>
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error in ingest function: {ex.Message} | {ex.InnerException} | {ex.StackTrace}");
            }
        }
    }
}