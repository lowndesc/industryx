# Industry X Sandbox #
## Azure IoT, PnP, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Create Sandbox Resource Group](#create-sandbox-resource-group)
	1. [Access Azure Cloud Shell](#access-azure-cloud-shell)
	2. [Creating Your Sandbox Resource Group](#creating-your-sandbox-resource-group)
4. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
5. [Simulated IoT Devices](#iot-devices)
	1. [Simulating telemetry](#simulating-telemetry)
	2. [Creating simulated devices](#creating-simulated-devices)
	3. [Connecting devices to the hub](#connecting-devices-to-the-hub)
	4. [Running simulated devices](#running-simulated-devices)
6. [Azure Digital Twins Instance](#azure-digital-twins-instance)
	1. [ADT Modelling](#adt-modelling)
	2. [Manufacturing Ontology](#manufacturing-ontology)
	3. [Creating the ADT Instance](#creating-the-adt-instance)
	4. [Uploading your models](#uploading-your-models)
	5. [Creating an asset twin](#creating-an-asset-twin)
	6. [Creating a process twin](#creating-a-process-twin)
	7. [Updating a twin](#updating-a-twin)
7. [Azure TwinSync Functions](#azure-twinsync-functions)
	1. [Using Azure Functions for TwinSync](#using-azure-functions-for-twinsync)
	2. [Subscribing to events](#subscribing-to-events)
	3. [Deploying functions](#deploying-functions)
8. [Plug-and-Play IoT Devices](#plug-and-play-iot-devices)  
	1. [PnP IoT and ADT Architecture](#pnp-iot-and-adt-architecture)
	2. [Deploying PnP Functions](#deploying-pnp-functions)
	3. [Auto-Provisioning a Simulated PnP Device](#auto-provisioning-a-simulated-pnp-device)
	4. [Provisioning a Physical PnP Device](#provisioning-a-physical-pnp-device)
    5. [Adding EventHubs](#adding-eventhubs)
	6. [Verifying PnP Telemetry in ADT](#verifying-pnp-telemetry-in-adt)
	7. [Auto-Retiring PnP Devices](#auto-retiring-pnp-devices)
9. [AnyLogic Simulation](#anylogic-simulation)
	1. [Creating an AnyLogic simulation](#creating-an-anylogic-simulation)
	2. [Preparing the AnyLogic simulation for Bonsai](#preparing-the-anylogic-simulation-for-bonsai)
	3. [Attaching AnyLogic telemetry by querying ADT](#attaching-anylogic-telemetry-by-querying-adt)
    4. [Exporting the AnyLogic Model for Bonsai](#exporting-the-anylogic-model-for-bonsai)
10. [Python Simulation](#python-simulation)
	1. [Creating a Python Simulation](#creating-a-python-simulation)
	2. [Preparing the Python simulation for Bonsai](#preparing-the-python-simulation-for-bonsai)
11. [Databricks Simulation](#databricks-simulation)
	1. [Attaching ADT telemetry to a Databricks Notebook](#attaching-adt-telemetry-to-a-databricks-notebook)
	2. [Creating a Databricks simulation](#creating-a-databricks-simulation)
	3. [Preparing the Databricks simulation for Bonsai](#preparing-the-databricks-simulation-for-bonsai)
12. [Microsoft Bonsai Teaching](#microsoft-bonsai-teaching)
	1. [Testing the simulation](#testing-the-simulation)
	2. [Importing the simulation](#importing-the-simulation)
	3. [Creating the brain](#creating-the-brain)
	4. [Teaching the brain](#teaching-the-brain)
	5. [Exporting the brain](#exporting-the-brain)
	6. [Running a simulation using the brain](#running-a-simulation-using-the-brain)
	7. [Other scenarios for using the trained brain](#other-scenarios-for-using-the-trained-brain)

### Overview ###
In this tutorial, you will create a sandbox environment in which you can further explore the architectural components of a specific Azure IoT architecture aimed at teaching AI through digital twin simulation. You will be completing the following tasks:  
- Create an Azure IoT Hub
- Use Azure IoT Explorer to register IoT devices
- Create Simulated IoT Devices
- Create an Azure Digital Twins instance
- Learn to use the Manufacturing Ontology to create digital twins for a typical Manufacturer
- Use Azure Functions to synchronise data between components
- Auto-Provision Simulated Plug-and-Play IoT Devices
- Auto-Provision Physical Plug-and-Play IoT Devices (in this case, an MXCHIP AZ3166 multi-sensor device)
- Auto-Retire IoT Devices
- Create an AnyLogic Simulation
- Create a Python Simulation
- Create a Databricks Simulation
- Extend Simulations to integrate with Microsoft Bonsai
- Use Bonsai to teach an AI 'Brain' from a Simulation
- Apply the Bonsai Brain to a Simulation for verification 
- Apply the Bonsai Brain to a digital twin 

Before you begin, you may want to clone this entire repository to your local machine, to make some of the steps below more straightforward. This is a collaborative tutorial and sample. Please use the Issues feature of Github to notify others and get help if you run into issues with the instructions or and of the code samples. Issues can be addressed by anyone on the team. Please feel free to submit improvements to the repository, but please document any changes.
### Prerequisites ###
- An Avanade or Accenture domain account
- Authorization to create a sandbox obtained from chris.lowndes@avanade.com
- (Optional) An MXCHIP AZ3166 multi-sensor device 

### Create Sandbox Resource Group ###
#### Access Azure Cloud Shell ####
To create your own sandbox environment, you will execute various commands in the Azure Cloud Shell. To access the Azure Cloud Shell, follow these steps:
1. Navigate to the Azure Portal at https://portal.azure.com
2. Login to the Azure Portal using your usual Avanade or Accenture credentials
3. Once logged in, in the main search bar, type 'subscriptions'
4. In the list of subscriptions, choose the subscription with ID as provided by the facilitator
5. Once switched to the required subscription, select the Cloud Shell button shown circled in red ![image](https://user-images.githubusercontent.com/1761529/125626161-ff2fb4be-a621-4662-a97b-e7d09ee7dd74.png)
6. Once the Cloud Shell opens at the bottom of your browser, switch from Bash mode to PowerShell mode ![image](https://user-images.githubusercontent.com/1761529/125626532-02f2a11e-2146-4586-bb43-5181583962b4.png)
#### Creating Your Sandbox Resource Group ####
To create a resource group to hold all of your sandbox resources, follow these steps:
1. Your sandbox name is your choice, but should follow this pattern:
```zsh
industryx-sandbox-<<your-initials-or-name>> #this is your sandbox name. Please copy it as you will be using it frequently 
```
2. Execute the following command in the Cloud Shell
```zsh
az group create --name <<your-sandbox-name>> --location japaneast
```
### Azure IoT Hub Instance ###
#### Creating An IoT Hub ####
To create the IoT Hub instance follow these steps:  
1. Execute the following command using the Azure Cloud Shell
```zsh
az deployment group create --resource-group <<your-sandbox-name>> --template-uri https://raw.githubusercontent.com/lowndesc/industryx/main/sandbox/IoTHub/azuredeploy.json
```
2. Navigate to your sandbox resource group within the Azure Portal. You should see two resources similar to this image ![image](https://user-images.githubusercontent.com/1761529/125631131-6d02ef16-9057-406a-aaa4-e1d6bc44f177.png)  
### IoT Devices ###
#### Simulating Telemetry ####
For our sandbox, we will first create code-based 3 simulated devices, each transmitting the same range of telemetry on a schedule, using randomisation to vary the values transmitted and the scheduling.
#### Creating Simulated Devices ####
To create the simulated devices, follow these steps:
1. Navigate to your IoT Hub instance
2. On the left-hand menu, under 'Settings', click 'Shared access policies'
3. On the list of Shared access policies, click the policy named 'device'
4. On the policy keys panel, copy the 'Primary connection string' and keep it for later use ![image](https://user-images.githubusercontent.com/1761529/125727559-4678cfb4-211d-4d7d-a16e-d844771c84ba.png)
5. In the Azure Cloud Shell, click on the 'Upload Files' button ![image](https://user-images.githubusercontent.com/1761529/125726411-95674d95-0e83-48aa-8177-ab852080aadd.png)
6. In the 'Open' dialog, post the following file URL into the 'File name:' field
```zsh
https://raw.githubusercontent.com/lowndesc/industryx/main/sandbox/SimulatedDevices/SimulatorCloudRunner.ps1
```
3. This will copy a PowerShell script file from GitHub to your AZure Cloud Shell session
4. Execute the following command in Azure Cloud Shell to run the PowerShell script, using the values copied earler 
```zsh
./SimulatorCloudRunner.ps1 -ResourceGroup <<your-sandbox-name>> -IotHubConnectionString <<your-iothub-connection-string>>
```
5. After a few seconds, you should see a JSON confirmation that the process has started
6. After around 1 minute, the 3 simulated device containers will have been deployed. Check your resource group to confirm.
#### Connecting Devices To The Hub #### 
To connect the simulated devices to the IoT Hub, follow these steps:
1. Navigate to your IoT Hub instance
2. In the left-hand menu, under 'Explorers', click 'IoT Devices'
3. Click 'New' to add a new device
4. For 'Device ID', type 'sim000001'
5. Leave all other fields unchanged, and click 'Save'
6. Repeat for two more devices, 'sim000002' and 'sim000003'
7. Your device list should now look like this ![image](https://user-images.githubusercontent.com/1761529/125725341-6e205aaa-bc36-4768-9abb-f011ac7477ae.png)
#### Running Simulated Devices ####
To run the simulated devices, follow these steps:
1. Navigate to the first simulated device container
2. Click on 'Start' to start the container ![image](https://user-images.githubusercontent.com/1761529/125723618-60df3054-253b-4be9-855b-7d7108a8829d.png)
4. Repeat for each of your simulated device containers
5. Once each container has started, the onboard functions will connect a device to the IoTHub using the registered device names you created in the previous task
6. To verify that the connection is good. navigate back to the IoT Hub insatnce, and on the 'Overview' pane check the 'Iot Hub Usage' panel and it should show 'Iot Devices: 3' and 'Messages used today:' should be a number greater than 0 ![image](https://user-images.githubusercontent.com/1761529/125726261-a9581972-7e9b-4dae-8715-ae68b9cef872.png)
### Azure Digital Twins Instance ###
For our sandbox environment, we will be implementing digital twins using Microsoft's Azure Digital Twins (ADT). ADT is based on a spatial graph model as opposed to traditional relational database technology. In relational databases, to analyse relationships across different table entities, the time expensive ‘JOIN’ operation is used to combine related data. This operation is expensive as it requires index lookups and matching to related columns in other tables. This is a major advantage of graph data models. Graphs store entities and their relationships as nodes and content which may be augmented with additional attributes. Retrieving the relationship between two entities does not involve expensive ‘join’ operations.  

Within our sandbox, we therefore require a single Azure Digital Twins (ADT) instance. Each instance can support many different digital twins based on many different models. For simplicity, it is best practice to use a separate ADT instance for each solution domain. ADT hosts the definitions of digital twins, based on underlying models, as well as the data describing the digital twins, including twin instances, components, relationships and properties. It does not, however, store underlying property and telemetry values, which are accessed from underlying storage. This makes ADT a highly performant abstraction of the digital twins it hosts.
#### ADT Modelling ####
ADT uses models to define digital twin types. Modelling is defined in a JSON-LD-based descriptive language called DTDL (Digital Twins Definition Language). DTDL models are interchangeble between device twins used in IoT Plug-and-Play scenarios and digital twins of those devices within Azure Digital Twins. Each DTDL model is an interface which defines the permissible structure of a digital twin. DTDL supports inheritance, such that one or more interfaces can be a base for other derived interfaces. A model is a generally a definition which can be instantiated as a digital twin. Howevere, a model can also be a component, used to compose other models, but not intended for instantiation by itself. An example would be a smartphone device, defined as containing components defining a front camera and a rear camera.
#### Manufacturing Ontology ####
For manufaturing scenarios, we need a set of base models which can form the foundation for describing manufacturing assets and processes. These base models need to be extendible to problem-specific scenrios, such as production monitoring, optimization and simulation, as well as wider scenarios such as materials handling and supply chain modelling. These base models need to follow industry-specific standards, such as ISA-95 and ISA-88, and be gathered into an overall model known as a manufacturing ontology. 
The starting point for our ontology is this basic data model as defined by the Industrial Automation standard ISA-95: 
![image](https://user-images.githubusercontent.com/1761529/126053763-ffbe065d-d0d1-48d3-a434-666afd45f4bf.png)  
From this, we have derived a basic manufacturing ontology as captured in this diagram. We have incuded a few example Azure IoT Plug-and-PLay devices into our ontology, to illustrate how these interact with the manufacturing assets and processes. We are to use this ontology within our sandbox environments:  
![Screenshot 2021-06-15 120456](https://user-images.githubusercontent.com/1761529/126053677-82c4351f-5e61-4971-88e4-5201c23e4736.jpg)  
Take a moment to examine the manufacturing ontology in its raw JSON DTDL form at this location in this repository:
> ./sandbox/AzureDigitalTwins/models/manufacturing-ontology
This entire folder/file structure is in the clone on your local drive for later use
#### Creating The ADT Instance ####
To create your ADT instance within your sandbox, execute the following steps:
1. Execute the following command in the Azure Cloud Shell:
```zsh
az deployment group create --resource-group <<your-sandbox-name>> --template-uri https://raw.githubusercontent.com/lowndesc/industryx/main/sandbox/AzureDigitalTwins/azuredeploy.json
```
2. When execution has finished, navigate to your sandbox ADT instance, on the Overview pain, copy the host name URL. You will need this later.
In order to create permissions for you to access your ADT instance, you need to add yourself as an owner of this instance. 
3. On the left-hand menu, select 'Access Control (IAM)'
4. On the 'Access Control (IAM)' pane, click +Add, and then 'Add role sssignment'
5. In the 'Add role assignment' panel, in the Role dropdown, select the 'Azure Digital Twins Data Owner' role
6. In the 'Select' field, type the start of your name, then select your user account from the list to add your user account to the 'Selected members' list
7. Click Save to create the role assignment
8. To verify that you can now access your ADT instance, navigate to the [online ADT explorer](https://explorer.digitaltwins.azure.net).
9. In the pop-up that asks for an Azure Digital Twins URL, type 'https://<<your host name URL from step 2 above>>' and click Save.
![image](https://user-images.githubusercontent.com/1761529/126057453-02e80a2c-aade-4aaf-8f97-3a5c1f971570.png)
10. On the top bar, click the cog icon to go to Settings. Set the Console and Output toggles to on.
![image](https://user-images.githubusercontent.com/1761529/126057497-a8db831e-e0df-43d3-a5e8-185e17167a28.png)
11. Now, when you click 'Run Query' in the top right, you should see a message in the centre pain that states 'No Results Found'.
![image](https://user-images.githubusercontent.com/1761529/126057561-36dcb0c2-877b-4b8f-96dd-4aaec329ca0b.png)
#### Uploading Your Models ####
Execute the following steps to upload the manufacturing ontology into the ADT model library: 
1. At the top of the left-hand Models panel, click the 'Upload a directory of Models' button:
![image](https://user-images.githubusercontent.com/1761529/126057655-7b97dfc5-c61a-48dd-b4b0-e5e702e02575.png)
2. Select the folder at the head of your local copy of the manufacturing ontology 'manufacturing-ontology'
3. Click Upload, and then OK when the system reminds you that you are uploading 40 models.
4. When these have uploaded, on the centre pane, click the 'MODEL GRAPH' tab.
5. You should see the entire manufacturing ontology with relationships and inheritance depicted.
![image](https://user-images.githubusercontent.com/1761529/126057790-15b6ddfd-e381-47a5-8dea-900bc1dbd698.png)
#### Creating An Asset Twin ####
We will now create sample digital twins of an entire manufacturing operation.

Execute the following steps to create the sample digital twins:
1. In Azure Digital Twins Explorer, at the top of the centre pane, click the 'TWIN GRAPH' tab.
2. At the top of the Twin Graph pane, click the 'Import Graph' button
![image](https://user-images.githubusercontent.com/1761529/126058503-fda241c7-771a-4f87-98df-5bbeb00b5efb.png)
3. In the file dialog, navigate to the file:
> ./sandbox/AzureDigitalTwins/logistics-twin.xlsx
4. Click Upload. The initial twin graph should load
5. On the 'Graph Preview Only' screen, click the save icon in the top right-hand corner.
6. The system will now process the graph, loading the nodes into ADT.
7. This should process OK. If there are any errors, use the Output screen to determine where the errors may be occuring.
8. Once processed, you can view a visualisation of the digital twins by returning to the 'TWIN GRAPH' tab and clicking 'Run Query' in the top right-hand corner.
9. You can switch layout style for your graph using the 'Choose Layout' button at the top of the 'TWIN GRAPH' pane.
![image](https://user-images.githubusercontent.com/1761529/126751910-5a180732-82db-45c5-b761-59fa27ba6f6f.png)
10. Note that we have included a number of Azure IoT Plug-and-PLay devices into this sample digital twins. One such device is shown in detail on the attached image - an MXCHIP AZ3166 device named 'PnP-Sensors', contains multiple sensors, and will be the subject of auto-provisiioning later.
![image](https://user-images.githubusercontent.com/1761529/126752423-32141e63-8289-4944-a1fa-ea0851713bc8.png) 
#### Creating A Process Twin ####
Now that we have created our digital twins of assets and spaces, we can add digital twins of the processes we wish to model. For our sandbox, we will model an end-to-end supply chain process, incorporating the manufacturing assets and spaces we have previously modelled.
To create digital twins of an end-to-end supply chain, execute the following steps:
1. In Azure Digital Twins Explorer, in the 'TWIN GRAPH' pane, click the 'Import Graph' button
2. Select the file:
> ./sandbox/AzureDigitalTwins/logistics-supply-chain-twin.xlsx
3. After the twins have loaded, click the Save icon in the top right-hand corner
4. The system will process the supply chain twins
5. To display only the supply chain process twins, copy the following SQL Query into the Azure Digitral Twins Explorer query field. This query is filtering the view to only those nodes and their relationships which inherit from the Supply Chain interface.
```sql
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:isa95:core:SupplyChain;1')
```
6. Run the query by clicking the 'Run Query' button.
#### Updating A Twin ####
To update a twin once it is created, you can either use the tools within the Azure Digital Twins explorer or prefereably refer to [this article about using the ADT API](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-postman?tabs=data-plane) 
Here, we will explain how to use the tools in Azure Digital Twins Explorer to make a simple change to an existing twin. Execute the following steps:
1. In Azure Digital Twins Explorer, run the following SQL Query to return only assets and their relationships:
```sql
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:isa95:core:Asset;1')
```
2. In the results view, find and click on the node for our simulated device 'sim000001'
3. Notice in the PROPERTIES panel on the right-hand side, there is a Property of the 'sim000001' twin called 'Name' for which the value is 'undefined'
4. Edit the 'Name' property to be 'sim000001'.
5. Notice that, after making this change, there is a record of the change added to the digital twin properties. All changes to digital twins are audited. ![image](https://user-images.githubusercontent.com/1761529/126096562-7138b3ed-655c-4e97-80da-a2a210218f3a.png)
### Azure TwinSync Functions ###
In order to synchronize data between features within our sandbox, we require some light Azure Functions. These we have grouped under a concept called TwinSync, a general principle to use Azure Functions where possible to execute data transformation code outside of any user context. These functions are descrete, low cost, highly scalable and reliable execution actions. The trigger is normally a standard Azure event occuring, such as a message arriving on an Event Grid topic or Event Hub.
#### Using Azure Functions For TwinSync ####
We will use TwinSync functions to achieve the following:
- to sync IoTHub telemetry with Azure Digital Twins
- to sync Azure Digital Twins data with AnyLogic simulations
- to sync Azure Digital Twins data with Databricks simulations  

For example, we have the following Function code available in a TwinSync Function named IoTHubtoTwins:
```cs
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

namespace Avanade.Japan.Device.Simulation.Sync
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
```
#### Creating Function App ####

#### Deploying Functions ####

#### Subscribing To Events ####

#### Verifying End-to-End Telemetry #### 
Once the events are flowing to our function app, they should be flowing into our ADT twins. 
To verify the end-to-end flow, execute the following steps:
1. Verify that telemetry events are flowing to our function
2. Verify that the function is processing the telemetry events without errors
3. Verify that properties of ADT sensor twins are being updated
### Plug-and-Play IoT Devices ###
#### PnP IoT and ADT Architecture ####
![image](https://user-images.githubusercontent.com/1761529/126741034-73f3cf50-1a98-43be-b3e5-61973fda29a0.png)
#### Deploying PnP Functions ####
##### PnP Auto-Provisioning Function #####
```cs
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices.Provisioning.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Samples.AdtIothub
{
    public static class DpsAdtAllocationFunc
    {
        private const string adtAppId = "https://digitaltwins.azure.net";
        private static string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");
        private static readonly HttpClient singletonHttpClientInstance = new HttpClient();

        [FunctionName("DpsAdtAllocationFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // Get request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogDebug($"Request.Body: {requestBody}");
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Get registration ID of the device
            string regId = data?.deviceRuntimeContext?.registrationId;

            bool fail = false;
            string message = "Uncaught error";
            var response = new ResponseObj();

            // Must have unique registration ID on DPS request
            if (regId == null)
            {
                message = "Registration ID not provided for the device.";
                log.LogInformation("Registration ID: NULL");
                fail = true;
            }
            else
            {
                string[] hubs = data?.linkedHubs.ToObject<string[]>();

                // Must have hubs selected on the enrollment
                if (hubs == null
                    || hubs.Length < 1)
                {
                    message = "No hub group defined for the enrollment.";
                    log.LogInformation("linkedHubs: NULL");
                    fail = true;
                }
                else
                {
                    // Find or create twin based on the provided registration ID and model ID
                    dynamic payloadContext = data?.deviceRuntimeContext?.payload;
                    string dtmi = payloadContext.modelId;
                    log.LogDebug($"payload.modelId: {dtmi}");
                    string dtId = await FindOrCreateTwinAsync(dtmi, regId, log);

                    // Get first linked hub (TODO: select one of the linked hubs based on policy)
                    response.iotHubHostName = hubs[0];

                    // Specify the initial tags for the device.
                    var tags = new TwinCollection();
                    tags["dtmi"] = dtmi;
                    tags["dtId"] = dtId;

                    // Specify the initial desired properties for the device.
                    var properties = new TwinCollection();

                    // Add the initial twin state to the response.
                    var twinState = new TwinState(tags, properties);
                    response.initialTwin = twinState;
                }
            }

            log.LogDebug("Response: " + ((response.iotHubHostName != null)? JsonConvert.SerializeObject(response) : message));

            return fail
                ? new BadRequestObjectResult(message)
                : (ActionResult)new OkObjectResult(response);
        }

        public static async Task<string> FindOrCreateTwinAsync(string dtmi, string regId, ILogger log)
        {
            // Create Digital Twins client
            var cred = new ManagedIdentityCredential(adtAppId);
            var client = new DigitalTwinsClient(
                new Uri(adtInstanceUrl),
                cred,
                new DigitalTwinsClientOptions
                {
                    Transport = new HttpClientTransport(singletonHttpClientInstance)
                });

            // Find existing DigitalTwin with registration ID
            try
            {
                // Get DigitalTwin with Id 'regId'
                BasicDigitalTwin existingDt = await client.GetDigitalTwinAsync<BasicDigitalTwin>(regId).ConfigureAwait(false);

                // Check to make sure it is of the correct model type
                if (StringComparer.OrdinalIgnoreCase.Equals(dtmi, existingDt.Metadata.ModelId))
                {
                    log.LogInformation($"DigitalTwin {existingDt.Id} already exists");
                    return existingDt.Id;
                }

                // Found DigitalTwin but it is not of the correct model type
                log.LogInformation($"Found DigitalTwin {existingDt.Id} but it is not of model {dtmi}");
            }
            catch(RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.NotFound)
            {
                log.LogDebug($"Did not find DigitalTwin {regId}");
            }

            // Either the DigitalTwin was not found, or we found it but it is of a different model type
            // Create or replace it with what it needs to be, meaning if it was not found a brand new DigitalTwin will be created
            // and if it was of a different model, it will replace that existing DigitalTwin
            // If it was intended to only create the DigitalTwin if there is no matching DigitalTwin with the same Id,
            // ETag.All could have been used as the ifNonMatch parameter to the CreateOrReplaceDigitalTwinAsync method call.
            // Read more in the CreateOrReplaceDigitalTwinAsync documentation here:
            // https://docs.microsoft.com/en-us/dotnet/api/azure.digitaltwins.core.digitaltwinsclient.createorreplacedigitaltwinasync?view=azure-dotnet
            BasicDigitalTwin dt = await client.CreateOrReplaceDigitalTwinAsync(
                regId, 
                new BasicDigitalTwin
                {
                    Metadata = { ModelId = dtmi },
                    Contents = 
                    {
                        { "Temperature", 0.0 },
                        {
                            "deviceInformation",
                            new BasicDigitalTwinComponent
                            {
                                Metadata = {},
                                Contents =
                                {
                                    { "manufacturer", "MXCHIP" }
                                }
                            }
                        }
                    }
                }
            ).ConfigureAwait(false);

            log.LogInformation($"Digital Twin {dt.Id} created.");
            return dt.Id;
        }
    }

    /// <summary>
    /// Expected function result format
    /// </summary>
    public class ResponseObj
    {
        public string iotHubHostName { get; set; }
        public TwinState initialTwin { get; set; }
    }
}
```
##### PnP Telemetry to Twin Function #####
```cs
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
```
##### PnP Device Auto-Retiring Function #####
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Samples.AdtIothub
{
    public static class DeleteDeviceInTwinFunc
    {
        private static string adtAppId = "https://digitaltwins.azure.net";
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL", EnvironmentVariableTarget.Process);
        private static readonly HttpClient singletonHttpClientInstance = new HttpClient();

        [FunctionName("DeleteDeviceInTwinFunc")]
        public static async Task Run(
            [EventHubTrigger("lifecycleevents", Connection = "EVENTHUB_CONNECTIONSTRING")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>(events.Length);

            // Create Digital Twin client
            var cred = new ManagedIdentityCredential(adtAppId);
            var client = new DigitalTwinsClient(
                new Uri(adtInstanceUrl),
                cred,
                new DigitalTwinsClientOptions
                {
                    Transport = new HttpClientTransport(singletonHttpClientInstance)
                });

            foreach (EventData eventData in events)
            {
                try
                {
                    //log.LogDebug($"EventData: {System.Text.Json.JsonSerializer.Serialize(eventData)}");

                    string opType = eventData.Properties["opType"] as string;
                    if (opType == "deleteDeviceIdentity")
                    {
                        string deviceId = eventData.Properties["deviceId"] as string;

                        try
                        {
                            // Find twin based on the original Registration ID
                            BasicDigitalTwin digitalTwin = await client.GetDigitalTwinAsync<BasicDigitalTwin>(deviceId);

                            // In order to delete the twin, all relationships must first be removed
                            await DeleteAllRelationshipsAsync(client, digitalTwin.Id, log);

                            // Delete the twin
                            await client.DeleteDigitalTwinAsync(digitalTwin.Id, digitalTwin.ETag);
                            log.LogInformation($"Twin {digitalTwin.Id} deleted in DT");
                        }
                        catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
                        {
                            log.LogWarning($"Twin {deviceId} not found in DT");
                        }
                    }
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }

        /// <summary>
        /// Deletes all outgoing and incoming relationships from a specified digital twin
        /// </summary>
        public static async Task DeleteAllRelationshipsAsync(DigitalTwinsClient client, string dtId, ILogger log)
        {
            AsyncPageable<BasicRelationship> relationships = client.GetRelationshipsAsync<BasicRelationship>(dtId);
            await foreach (BasicRelationship relationship in relationships)
            {
                await client.DeleteRelationshipAsync(dtId, relationship.Id, relationship.ETag);
                log.LogInformation($"Twin {dtId} relationship {relationship.Id} deleted in DT");
            }

            AsyncPageable<IncomingRelationship> incomingRelationships = client.GetIncomingRelationshipsAsync(dtId);
            await foreach (IncomingRelationship incomingRelationship in incomingRelationships)
            {
                await client.DeleteRelationshipAsync(incomingRelationship.SourceId, incomingRelationship.RelationshipId);
                log.LogInformation($"Twin {dtId} incoming relationship {incomingRelationship.RelationshipId} from {incomingRelationship.SourceId} deleted in DT");
            }
        }
    }
}
```
#### Auto-Provisioning a Simulated PnP Device ####
The Azure Device Provisioning Service (DPS) for IoT Hub automatically provisions registered devices so that they will communicate with the correct IoT Hub. The DPS can also be customized to do additional tasks. In our case, we will customize it to also create a Twin in Azure Digital Twins for every device provisioned.

![image](https://user-images.githubusercontent.com/1761529/126741050-98ee91d1-aed3-48d4-9b2d-2d3e39787ab2.png)

1. When the device is freshly manufactured, it is configured to first communicate with the DPS. 
2. The DPS then validates the device by checking if the device is in the enrollment list.
3. Custom: If the device is deemed valid, the Azure Function (DpsAdtAllocationFunc) creates a Twin for the device in Azure Digital Twins.
4. If the device is deemed valid, it is added as a device in the appropriate IoTHub.
5. The device is given back connection details for the appropriate IoTHub where it will then start sending telemetry data.

 The DPS acts like a 'front desk' in a hotel setting, where it identifies the 'guest' (device) and if the 'guest' has a 'booking' / 'reservation' (enrollment) directs him/her to the appropriate 'room' (IoTHub).

 *** Setting up a DPS ***

 1. Create a resource 'Device Provisioning Services'. Provide the appropriate Subscription, Resource Group, Name, and Region. <br>
![image](https://user-images.githubusercontent.com/12861152/131145432-4f47e3c0-56dd-4a8d-80d8-c9912122fbad.png)
 2. Once it is done creating, go to the resource and select 'Linked IoT Hubs'. In here, we will add the IoTHub/s where our devices will be directed to. <br>
![image](https://user-images.githubusercontent.com/12861152/131146194-afc87d8e-1410-4d51-85df-5e550f8511a0.png)
 3. Provide the details of the IoTHub to be added then click 'Save'. <br>
![image](https://user-images.githubusercontent.com/12861152/131146698-04f699cb-b044-46fd-9835-db3856c088cb.png)
 4. Go to 'Manage Enrollments' then select 'Add Individual Enrollment'. In here, we will add an individual device to the enrollment list. <br>
![image](https://user-images.githubusercontent.com/12861152/131147457-870e2a24-67cb-4b33-9c31-23b9f8af83c3.png)
 5. Fill in the details of the enrollment:
   * Use 'Symmetric Key' and provide a 'Registration ID' for the device. Copy and save the Registration ID since it will be used later when configuring the IoT Device.<br>
   ![image](https://user-images.githubusercontent.com/12861152/131149482-3d729b04-ac8f-4400-8592-5fe263d91473.png)
   * Select 'Custom (Use Azure Function)' in assigning devices to hubs, since we will run 'DpsAdtAllocationFunc'. Also make sure the device will be assigned to the correct IoTHub if you have multiple IoTHubs.<br>
   ![image](https://user-images.githubusercontent.com/12861152/131150120-9634b038-7223-4bff-82cc-b82962c7eef5.png)
   * Select the provisioned 'DpsAdtAllocationFunc' then save the enrollment.<br>
  ![image](https://user-images.githubusercontent.com/12861152/131150808-6cac3f2e-50c5-4326-ba52-04fd26594259.png)
  1. Click on the newly enrolled device then copy and save the 'Primary Key'. This will be used later when configuring the IoT Device.<br>
  ![image](https://user-images.githubusercontent.com/12861152/131151786-5d791c00-bb06-4083-9822-16fdb967cd2a.png)<br>
  ![image](https://user-images.githubusercontent.com/12861152/131152229-358a1a22-2661-468f-8ce1-9dfd02feec9d.png)
  7. Go to 'Overview' then copy and save 'Service Endpoint' and 'ID Scope'. These will also be used in configuring the IoT Device.<br>
  ![image](https://user-images.githubusercontent.com/12861152/131153247-1627738f-2297-4cc6-91f0-918227be74ae.png)

Next we will be setting up a simulated device that's supposedly freshly manufactured. The simulator can be run on your local machine.<br>

*** Setting up a simulated freshly manufactured device ***
  1. Install nodejs on your local machine. https://nodejs.org/en/download/
  2. The IoT device simulator can be found in 
  > ./sandbox/PnPDevices/azure-iot-rpisimulator
  3. Open index.js and fill in the values saved earlier.<br>
  ![image](https://user-images.githubusercontent.com/12861152/131158199-7d5378bb-2c41-4afd-94c5-79d98ccb400f.png)<br>
  * insert the 'Service Endpoint' in 'provisioningHost'
  * insert the 'ID Scope' in 'idScope'
  * insert the device's registration id in 'registrationId'
  * insert the primary symmetric key for the device in 'symmetricKey'
  * the 'modelId' corresponds to the MXCHIP AZ3166 multi-sensor device model in Azure Digital Twins. (don't change it)
  4. Run the ff. command in the IoT device simulator directory:
```zsh
npm install
```

*** Testing the DPS ***
  1. Open Azure Digital Twins and run this query.
```sql
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:azurertos:devkit:gsgmxchip;2')
```
 * Currently, only one MXCHIP AZ3166 named 'PnP-Sensors' has an existing twin. This was the one imported to ADT [earlier](#creating-an-asset-twin).<br>
    ![image](https://user-images.githubusercontent.com/12861152/131300892-b012282a-0400-4f8f-b4e6-ee80535389fe.png)
  2. Run the simulator using the ff. command:
```zsh
npm start
```
 * The logs should look like this if successfully run. <br>
 ![image](https://user-images.githubusercontent.com/12861152/131337492-f1034672-9d54-4d49-a21e-3790859c01c8.png) <br>
 * An error message will appear if there is a problem in provisioning. This is usually a problem in the Azure Function configuration and role. <br>
 ![image](https://user-images.githubusercontent.com/12861152/131337954-6c2b2ef7-46b4-4771-90b6-aa5044216495.png)
 * The ff. logs should appear when the simulated device is successfully provisioned and is sending telemetry data to the correct IoTHub. <br>
 ![image](https://user-images.githubusercontent.com/12861152/131338735-967b1449-5086-4135-b3d5-e83804947581.png) <br>

  3. Run the query in step 1 again in Azure Digital Twins. The simulated device should now appear in the twin graph. <br>
 ![image](https://user-images.githubusercontent.com/12861152/131339386-3f81ece1-88a7-47af-bbd9-3503a043d877.png) <br>

#### Provisioning a Physical PnP Device ####
![20210723_143419](https://user-images.githubusercontent.com/1761529/126741223-2aece734-8cf3-4645-8c86-84f253656995.jpg)

#### Adding EventHubs ####
Azure EventHub is a service for ingesting streams of data from various sources including IoT telemetry. This data can be used by various consumers for further processing and analysis. In our case, we will be using EventHub to:

1. Send telemetry data to ADT to update twins (DeviceTelemetryToTwinFunc). This is somewhat similar to TwinSync, except now, telemetry data is coming from EventHub instead of EventGrid.
2. Delete a device's twin in ADT whenever the device is deleted in IoTHub (DeleteDeviceInTwinFunc).

In the following steps, we'll be showing how to setup an event hub for use case 1, i.e., for the DeviceTelemetryToTwinFunc function. You can use the same steps for setting up use case 2 (DeleteDeviceInTwinFunc).

*** Creating an EventHub Namespace *** <br>

1. In order to create an event hub, we first need an event hubs namespace. Create a resource named 'Event Hubs' and fill in the appropriate Subscription, Resource Group, Location, and Pricing Tier. Also provide a namespace name. <br>
![image](https://user-images.githubusercontent.com/12861152/131370727-b00c43b5-b895-485b-b726-8ba690207a35.png) <br>
2. Once the eventhub namespace is created, we'll need to add a policy so that we can read messages being sent to the EventHubs belonging to this namespace. Go to the resource, select 'Shared access policies', and click 'Add' to add a new SAS Policy. <br>
![image](https://user-images.githubusercontent.com/12861152/131377947-fca882a7-efea-43cd-8cd9-88611932f094.png) <br>
3. Provide a name for the policy, then select the 'Listen' checkbox. This allows us to read events coming in to the EventHubs in this namespace. <br>
![image](https://user-images.githubusercontent.com/12861152/131378048-878a1d88-e8c1-4cc7-958b-56980f0f55dc.png) <br>
4. Once the SAS policy is created, click it, copy the 'Connection string-primary key', then save this connection string. This is the connection string that will be used by our Azure Functions to read data from the EventHubs. <br>
![image](https://user-images.githubusercontent.com/12861152/131378594-cecc5cb5-391b-42d4-9768-64e278094db7.png) <br>

*** Creating an EventHub *** <br>
1. Go to 'Event Hubs' then click '+ Event Hub'. <br>
![image](https://user-images.githubusercontent.com/12861152/131379207-5cf72edf-9e59-4732-9cde-4e976d767b32.png) <br>
2. Give the EventHub a name then click 'Create'. Make sure this name matches with the EventHub name in the Azure Function EventHubTrigger Attribute. <br>
![image](https://user-images.githubusercontent.com/12861152/131379644-4f0ced11-a41f-4256-8fb2-ea412adf7ef7.png) <br>

*** Setting up Azure Functions *** <br>
1. The only thing we need to setup in Azure Functions is the connection string which will give the function read access to EventHubs. In Azure Functions, select 'Configuration'. <br>
![image](https://user-images.githubusercontent.com/12861152/131380928-da7039e0-9fd9-45eb-9bc2-8c8da8e36820.png) <br>
2. In here we'll add a new application setting that corresponds to the event hub connection string. <br>
![image](https://user-images.githubusercontent.com/12861152/131381284-45449b2f-369d-4703-b471-8c29fcf23696.png) <br>
3. Fill in the name of the application setting and the value of the connection string. The name must match the application setting name being referred to in the code. <br>
![image](https://user-images.githubusercontent.com/12861152/131381597-2066fca5-6c1e-43c7-a5b9-a2d20701fddb.png) <br>
4. Click 'OK' then click 'Save' to restart the function app. <br>

*** Setting up IoTHub *** <br>
In IoTHub, we need to route messages towards the appropriate EventHub. To do this, we'll use IoTHub's built-in message routing.

 1. Go to the IoTHub instance, click on 'Message Routing' and select 'Custom Endpoints'. In here we will add our EventHubs as custom endpoints where messages will be sent. Click '+Add' and select 'Event hubs'. <br>
![image](https://user-images.githubusercontent.com/12861152/131543132-4d609a02-3b6e-4961-a2f0-ef274391caf1.png) <br>
 2. Provide a name for the custom endpoint and specify the EventHub namespace and instance. (do the same for 'lifecycleevents')<br>
 ![image](https://user-images.githubusercontent.com/12861152/131542680-4364f6d8-0a4d-41d5-9f88-3dc0d737d77e.png) <br>
 3. Go to 'Routes' and click on '+ Add'. <br>
 ![image](https://user-images.githubusercontent.com/12861152/131543313-f6d3cb5a-347b-4841-85b3-d7887cd988b1.png)<br>
 4. Provide a name for the route and specify appropriate custom endpoint and data source. (for lifecycleevents, select 'Device Lifecycle Events') <br>
 ![image](https://user-images.githubusercontent.com/12861152/131543613-0be134bd-03e4-4437-b51d-8d1c94ec8e8c.png)<br>
   * You could also specify a routing query which will filter the messages being routed. For both 'deviceevents' and 'lifecycleevents', we'll just use the default query 'true'. This means no filter will be applied and all messages will be routed. <br>
   ![image](https://user-images.githubusercontent.com/12861152/131544711-fae8aa73-3394-494d-98cd-97abad188550.png)
 5. Click 'Save'. <br>

#### Verifying PnP Telemetry in ADT ####
We can verify that we are updating our twin using the telemetry data from the simulated device. In ADT, we can see that our provisioned device initially has 0 for the value of temperature. <br>
![image](https://user-images.githubusercontent.com/12861152/131551289-25b17941-f3ac-434d-8d62-0d687cdce3cd.png) <br>

After running the simulated device, we should get the following logs for 'DeviceTelemetryToTwinFunc' (enable 'Application Insights' to see function logs). We should see that the temperature is being set to the value 2195. <br>
![image](https://user-images.githubusercontent.com/12861152/131547618-8dc98713-b6b0-46b2-9acc-36ac16af806f.png) <br>
Upon reloading the query in ADT, we can see that the value for temperature of the twin is also updated. <br>
![image](https://user-images.githubusercontent.com/12861152/131547637-660b57a8-3f45-4a31-8c2b-6eac1171970a.png) <br>

#### Auto-Retiring PnP Devices ####
![image](https://user-images.githubusercontent.com/1761529/126741148-e32ee60f-8018-44ca-b21d-8c4d7084f704.png) <br>

Given that we've already [added an EventHub for 'lifecycleevents'](#adding-eventhubs), we can now perform auto-retiring on the simulated device, i.e., when a device is deleted in IoTHub, its twin in ADT should also be deleted. To test this, we'll simply delete our provisioned simulated device from IoTHub. <br>
![image](https://user-images.githubusercontent.com/12861152/131549073-4a807e40-08fe-4165-acba-11a984168a1a.png) <br>

We can verify in the DeleteDeviceInTwinFunc function logs that deletion on ADT was performed.
 <br>
![image](https://user-images.githubusercontent.com/12861152/131550231-dcd1fe85-4a04-4c79-b3d5-4af148c5ed9d.png) <br>

We can also verify in ADT that our simulated device's twin no longer exists. <br>
![image](https://user-images.githubusercontent.com/12861152/131550681-bb6d92da-15dc-449f-8254-bb33e1d46ce6.png) <br>
### AnyLogic Simulation ###
#### Creating An AnyLogic Simulation ####
![image](https://user-images.githubusercontent.com/1761529/126741509-cf675124-da8a-4429-b766-df7961304bfe.png)
![image](https://user-images.githubusercontent.com/1761529/126746438-32286441-8764-407c-99cc-c3d71f2c161a.png)
![image](https://user-images.githubusercontent.com/1761529/126746510-6719d66d-0936-4c1d-8554-047960a6f879.png) <br>


#### Preparing The AnyLogic Simulation For Bonsai ####
In order for an AnyLogic model/simulation to be subject to Microsoft Bonsai's RL Training, it must have an **RLExperiment** experiment. <br>
![image](https://user-images.githubusercontent.com/12861152/132808019-f97afbfd-45b6-48e6-8687-240b7407e5fb.png) <br>
RLExperiment is a type of experiment in AnyLogic that simplifies the wrapping of an AnyLogic model so that it can be easily trained in a RL training platform such as Bonsai. Without RLExperiment, you'd have to use a separate wrapper class in order to use the model for reinforcement learning. <br><br>
<em>Note: By the time this tutorial is created, AnyLogic RLExperiment currently does not support local training. RLExperiment is only used for wrapping and exporting the model/simulation for Bonsai or any other RL Training platform.</em><br>
![image](https://user-images.githubusercontent.com/12861152/133013076-a1eb3062-9c3f-4e78-a0b5-2043145b7244.png) <br><br>
To use RLExperiment, simply add it to the AnyLogic model. <br>
![image](https://user-images.githubusercontent.com/12861152/132809065-047af2c8-3a0f-4727-b20e-6616bf727699.png) <br>
![image](https://user-images.githubusercontent.com/12861152/132809210-66f54890-9c13-4daf-92bf-72d69aa844e7.png) <br>
![image](https://user-images.githubusercontent.com/12861152/132808832-d8b45acd-5e1c-4a8c-a5e1-85868c9cce48.png) <br>
<em>** There is already an existing RLExperiment on this model. The above screenshots just show how to add one.</em> <br><br>

To use RLExperiment, the Observation, Action and Configuration fields must be filled in. The **Observation** field comprises the data being passed to the Bonsai brain in order to make a decision. The **Action** field comprises the parameters that the Bonsai brain can manipulate after making a decision. Lastly, the **Configuration** field comprises the parameters that the Bonsai can set as the model's initial condition/state. <br>

The fields' values are mapped from the **Top-level agent** which is being referenced as **root**. <br>

![image](https://user-images.githubusercontent.com/1761529/126746628-0d08b23a-9908-42b6-abc4-84e9f82e05c9.png) <br>
#### Attaching AnyLogic Telemetry By Querying ADT ####
![image](https://user-images.githubusercontent.com/1761529/126746710-f46365e5-986d-48cd-b051-aa91b73ee38d.png)

#### Exporting the AnyLogic Model for Bonsai ####
1. Right-click the model, select 'Export > Reinforcement Learning'. <br>
![1](https://user-images.githubusercontent.com/12861152/132813112-f8cf3d96-63f1-441d-9ddf-9fdc9093d402.png)<br>
2. Take note of the destination folder where the zip file will be exported. The zip file will be used later when training in Bonsai.<br>
![2](https://user-images.githubusercontent.com/12861152/132813189-eda41e3b-016a-4f66-97e1-8d63264ec0ec.png)<br>
3. Finish the export. <br>
![3](https://user-images.githubusercontent.com/12861152/132813275-e52a359f-e8ec-43de-ac4c-34115790d02f.png)<br>

### Python Simulation ###
#### Creating a Python Simulation ####
#### Preparing the Python simulation for Bonsai ####
### Databricks Simulation ###
#### Attaching ADT telemetry to a Databricks Notebook ####
1. Create a Databricks workspace
2. Create a new Python Notebook
3. Add the following codeblock in a new cell in the Notebook. This codeblock installs the required ADT and Azure Identity Python packages 
```zsh
%pip install azure-digitaltwins-core azure-identity
```
4. Add the following codeblock in a new cell under the previous. This codeblock creates the ADT client and uses it to query models or to query digital twins matching a relationship to another digital twin. 
```python
# Establish a client connection to ADT

# DefaultAzureCredential supports different authentication mechanisms and determines the appropriate credential type based of the environment it is executing in.
# It attempts to use multiple credential types in an order until it finds a working credential.
import os
import azure.identity
import azure.digitaltwins.core

# - AZURE_ADT_URL: The URL to the ADT in Azure
url = os.getenv("AZURE_ADT_URL")

# DefaultAzureCredential expects the following three environment variables:
# - AZURE_TENANT_ID: The tenant ID in Azure Active Directory
# - AZURE_CLIENT_ID: The application (client) ID registered in the AAD tenant
# - AZURE_CLIENT_SECRET: The client secret for the registered application
credential = azure.identity.DefaultAzureCredential()
service_client = azure.digitaltwins.core.DigitalTwinsClient(url, credential)

# List ADT models
# listed_models = service_client.list_models()
# for model in listed_models:
#     print(model)
    
# print()
# print()

# Query ADT for the Process Twin
query_expression ='SELECT Station FROM DIGITALTWINS Station JOIN Process RELATED Station.isStepOf Relationship WHERE Process.$dtId = \'pp_BodyPanelProduction_01\''
query_result = service_client.query_twins(query_expression)
print('DigitalTwins:')
for twin in query_result:
    print(twin)
```
5. Add the following codeblock in a new cell under the previous. This codeblock uses the ADT client to query the telemetry of one sensor, obtaining a value every 5 seconds.
```python
# Query ADT for the Process Delay telemetry for the Coating Station
import time
query_expression = 'SELECT Device.Delay FROM DigitalTwins Device WHERE $dtId=\'sim000001\''
txt = '{}:{}'
for x in range(49):
  query_result = service_client.query_twins(query_expression)
  for value in query_result:
    print(txt.format(x,value))
    time.sleep(5)
```
6. Create a new Cluster
7. Add ADT Environment Variables to the Cluster
```zsh
AZURE_ADT_URL=<The URL for your ADT in Azure, including 'https://'>
AZURE_TENANT_ID=<The tenant ID of your Azure Active Directory>
AZURE_CLIENT_ID=<The application (client) ID registered in the AAD tenant>
AZURE_CLIENT_SECRET=<The client secret for the registered application>
```
8. Start the Cluster
9. Attach the Notebook to the Cluster
10. Execute each cell until you see ADT telemetry streaming
#### Creating a Databricks simulation ####
1. Add an Interface document to the Databricks Workspace
2. Add the following cells to the Notebook
3. Create a new Brain in Microsoft Bonsai
4. Add Inkling code to the new Brain
5. Train the Brain using the registered Databricks Simulation
6. Observe the Brain as teaching takes place
#### Preparing the Databricks simulation for Bonsai ####
1. Add the following environment variables to the Cluster, by first stopping the Cluster, editing the variables, and then restarting the Cluster.
```zsh
SIM_WORKSPACE=<the workspace ID from your Bonsai workspace>
SIM_ACCESS_KEY=<the access key from your Bonsai workspace>
```
2. Detach and re-attach the Notebook from the Cluster
3. Add the following codeblock to the Notebook in a new cell. This codeblock installs the required package to connect with Microsoft Bonsai.
```zsh
%pip install git+https://github.com/microsoft/bonsai-common
```
2. Add the following codeblock to the Notebook in a new cell. This codeblock checks that the path to the Interface document is correct.
```python
interface_file_path = "/FileStore/tables/cartpole-py/cartpole_interface.json"
display(dbutils.fs.ls("dbfs:" + interface_file_path))
```
3. Add the following codeblock to the Notebook in a new cell. This codeblock is the Python simulation. It includes code which registers the simulation 
```python
"""
Classic cart-pole system implemented by Rich Sutton et al.
Derived from http://incompleteideas.net/sutton/book/code/pole.c
permalink: https://perma.cc/C9ZM-652R
"""
__copyright__ = "Copyright 2020, Microsoft Corp."

# pyright: strict

import math
import os
import random
import sys
import json


from bonsai_common import SimulatorSession, Schema
from microsoft_bonsai_api.simulator.client import BonsaiClientConfig
from microsoft_bonsai_api.simulator.generated.models import SimulatorInterface


# Constants
GRAVITY = 9.8  # a classic...
CART_MASS = 0.31  # kg
POLE_MASS = 0.055  # kg
TOTAL_MASS = CART_MASS + POLE_MASS
POLE_HALF_LENGTH = 0.4 / 2  # half the pole's length in m
POLE_MASS_LENGTH = POLE_MASS * POLE_HALF_LENGTH
FORCE_MAG = 1.0
STEP_DURATION = 0.02  # seconds between state updates (20ms)
TRACK_WIDTH = 1.0  # m
FORCE_NOISE = 0.02  # % of FORCE_MAG

# Model parameters
class CartPoleModel(SimulatorSession):
    def reset(
        self,
        initial_cart_position: float = 0,
        initial_pole_angle: float = 0,
        target_pole_position: float = 0,
    ):
        # cart position (m)
        self._cart_position = initial_cart_position

        # cart velocity (m/s)
        self._cart_velocity = 0

        # cart angle (rad)
        self._pole_angle = initial_pole_angle

        # pole angular velocity (rad/s)
        self._pole_angular_velocity = 0

        # pole position (m)
        self._pole_center_position = 0

        # pole velocity (m/s)
        self._pole_center_velocity = 0

        # target pole position (m)
        self._target_pole_position = target_pole_position

    def step(self, command: float):
        # We are expecting the input command to be -1 or 1,
        # but we'll support a continuous action space.
        # Add a small amount of random noise to the force so
        # the policy can't succeed by simply applying zero
        # force each time.
        force = FORCE_MAG * (command + random.uniform(-0.02, 0.02))

        cosTheta = math.cos(self._pole_angle)
        sinTheta = math.sin(self._pole_angle)

        temp = (
            force + POLE_MASS_LENGTH * self._pole_angular_velocity ** 2 * sinTheta
        ) / TOTAL_MASS
        angularAccel = (GRAVITY * sinTheta - cosTheta * temp) / (
            POLE_HALF_LENGTH * (4.0 / 3.0 - (POLE_MASS * cosTheta ** 2) / TOTAL_MASS)
        )
        linearAccel = temp - (POLE_MASS_LENGTH * angularAccel * cosTheta) / TOTAL_MASS

        self._cart_position = self._cart_position + STEP_DURATION * self._cart_velocity
        self._cart_velocity = self._cart_velocity + STEP_DURATION * linearAccel

        self._pole_angle = (
            self._pole_angle + STEP_DURATION * self._pole_angular_velocity
        )
        self._pole_angular_velocity = (
            self._pole_angular_velocity + STEP_DURATION * angularAccel
        )

        # Use the pole center, not the cart center, for tracking
        # pole center velocity.
        self._pole_center_position = (
            self._cart_position + math.sin(self._pole_angle) * POLE_HALF_LENGTH
        )
        self._pole_center_velocity = (
            self._cart_velocity
            + math.sin(self._pole_angular_velocity) * POLE_HALF_LENGTH
        )

    def halted(self):
        # If the pole has fallen past 45 degrees, there's no use in continuing.
        return abs(self._pole_angle) >= math.pi / 4

    def state(self):
        return {
            "cart_position": self._cart_position,
            "cart_velocity": self._cart_velocity,
            "pole_angle": self._pole_angle,
            "pole_angular_velocity": self._pole_angular_velocity,
            "pole_center_position": self._pole_center_position,
            "pole_center_velocity": self._pole_center_velocity,
            "target_pole_position": self._target_pole_position,
        }

    # Callbacks
    def get_state(self):
        return self.state()

    def get_interface(self) -> SimulatorInterface:
        with open("/dbfs" + interface_file_path, "r") as file:
	    json_interface = file.read()
        interface = json.loads(json_interface)
        return SimulatorInterface(
            name=interface["name"],
            timeout=interface["timeout"],
            simulator_context=self.get_simulator_context(),
            description=interface["description"],
        )

    def episode_start(self, config: Schema):
        self.reset(
            config.get("initial_cart_position") or 0,
            config.get("initial_pole_angle") or 0,
            config.get("target_pole_position") or 0,
        )

    def episode_step(self, action: Schema):
        self.step(action.get("command") or 0)


if __name__ == "__main__":
    config = BonsaiClientConfig(argv=sys.argv)
    cartpole = CartPoleModel(config)
    cartpole.reset()
    while cartpole.run():
        continue
```
### Microsoft Bonsai Teaching ###
#### Testing The Simulation ####
#### Importing The Simulation ####
<em> NOTE: The following only applies for AnyLogic Simulations. </em>
1. Go to Bonsai Workspace and click 'Add Sim'. <br>
![9-1](https://user-images.githubusercontent.com/12861152/132816367-9179cda1-ba4c-48f7-bec0-99aab9332986.png)<br>
2. Select 'AnyLogic'. <br>
![9-2](https://user-images.githubusercontent.com/12861152/132817216-3149acae-0d3c-44ff-b89d-3b3291da0b30.png)<br>
3. Upload the [exported zip](#exporting-the-anylogic-model-for-bonsai) of the AnyLogic model and provide a name for the simulator. <br>
![10](https://user-images.githubusercontent.com/12861152/132817746-a2c94017-da60-4495-bc11-6fcbd8ede880.png)<br>
4. Copy the package statement. This will be used later in the inkling code. <br>
![11](https://user-images.githubusercontent.com/12861152/132823960-050b712d-943b-4703-8ae6-c73d31179104.png)

#### Creating The Brain ####
1. Go to the Bonsai Workspace and click 'Create Brain'. <br>
![6-1](https://user-images.githubusercontent.com/12861152/132813932-b7717172-7592-4099-90c4-6c9285d63f39.png)<br>
2. Select 'Empty Brain' and enter details. <br>
![6-2](https://user-images.githubusercontent.com/12861152/132814788-c8500fc2-ea02-4751-94dd-1f645517ca63.png)<br>
![7](https://user-images.githubusercontent.com/12861152/132814881-f22d0578-8a8a-49b5-a10e-40b66018efda.png)<br>
3. After the brain is created, go to the 'Teach' tab and paste the following inkling code. <br>
![8](https://user-images.githubusercontent.com/12861152/132815079-64e12775-4303-4670-81df-fb9d017f0a0e.png)
```python
# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.
inkling "2.0"
using Math
using Goal
# SimState has the same properties as the Observation fields in the RLExperiment
type SimState {
    arrivalRate: number<0 .. 2.0>,
    recentNProducts: number,
    costPerProduct: number,
    utilizationResourceA: number<0 .. 1>,
    utilizationResourceB: number<0 .. 1>,
    ratioFullQueueA: number<0 .. 1>,
    ratioFullQueueB: number<0 .. 1>,
    ratioCostIdleA: number<0 .. 1>,
    ratioCostIdleB: number<0 .. 1>,
    ratioCostWaiting: number<0 .. 1>,
    ratioCostProcessing: number<0 .. 1>,
    ratioCostMoving: number<0 .. 1>,

    exceededCapacityFlag: number<0, 1, >,
    simTimeHours: number<0 .. 24>
}
# The ObservableState is what the brain sees from the simulator.
# In this case, it's just the arrival rate.
type ObservableState {
    arrivalRate: number
}

type Action {
    numResourceA: number<1 .. 20>,
    numResourceB: number<1 .. 20>,
    conveyorSpeed: number<0.01 .. 1.0>,
}
type SimConfig {
    arrivalRate: number,
    sizeBufferQueues: number
}
simulator Simulator(action: Action, config: SimConfig): SimState {

}
# SimAction is the values translated for the sim.
# We do not need ranges here.
# These are the same as the ModelAction class.
type SimAction {
    numResourceA: number,
    numResourceB: number,
    conveyorSpeed: number,
}

function Terminal(obs: SimState) {
    if (obs.exceededCapacityFlag == 1) {
        return true
    }


    # The brain gets one chance at the answer
    return obs.simTimeHours >= 6
}
function Reward(obs: SimState) {
    # Large penalty for exceeding the buffer queue's capacity.
    # Otherwise, try to maximize the cost per product value.
    return -obs.costPerProduct - 1000 * obs.exceededCapacityFlag

}
graph (input: ObservableState): Action {

    concept optimize(input): Action {
        curriculum {
            source Simulator
            reward Reward
            terminal Terminal
            lesson `Vary Arrival Rate` {
                # These map to the SimConfig values
                scenario {
                    arrivalRate: number<0.5 .. 2.0 step .05>,
                    sizeBufferQueues: 45
                }
            }
        }
    }
    output optimize
}

```
4. Paste the [package statement](#importing-the-simulation) in the simulator object code. <br>
![12](https://user-images.githubusercontent.com/12861152/132824478-09c91057-9582-48f0-9cc5-cf6a951c90a4.png)


#### Teaching The Brain ####
1. Go to the 'Train' tab and click the 'Train' button to begin training. <br>
![13](https://user-images.githubusercontent.com/12861152/132826567-cf97f150-4397-4713-836d-dbe92d556d61.png) <br>
2. Training should begin after all the simulator instances are running. On the bottom, a chart showing the values of different variables can be seen. <br>
![14](https://user-images.githubusercontent.com/12861152/132826685-9c316d41-ff51-4560-9c70-4eca667ae50a.png)<br>
3. You can add more charts using the 'Add chart' button. <br>
![15](https://user-images.githubusercontent.com/12861152/132827328-1c76d7ea-b0f5-47f6-8bc5-ee267e0e6202.png)<br>
4. Training usually takes a long time. After a few thousand iterations, the main graph should show the **brain performance** and **mean brain performance**.
![16](https://user-images.githubusercontent.com/12861152/132827729-33320181-6faf-4697-bede-05a1ecbe9da7.png)<br>
5. You may stop the training if you think the performance is good enough. <br>
![17](https://user-images.githubusercontent.com/12861152/132827838-52fdd119-77ce-4326-a399-25a85e3b0dd8.png)

#### Exporting The Brain ####
Once the training is done, the brain can now be exported for use. The brain is usually exported as a docker image which can be run locally or in the cloud as a web app. The exported docker image, when run, can be exposed as a REST API.<br>
1. Click 'Export brain' <br>
![18](https://user-images.githubusercontent.com/12861152/132828761-1956fc58-a752-40f9-88e9-7e1a853bdf02.png)<br>
2. Provide the brain's display name. There's no need to change the processor architecture. <br>
![19](https://user-images.githubusercontent.com/12861152/132829235-57a1942f-cf89-4a8c-9223-085130db779c.png)<br>
3. In the deployment instructions after export, copy the string following the 'docker pull' command. <br>
![image](https://user-images.githubusercontent.com/12861152/132829427-d60c2229-d774-49f7-891a-130ddadd8593.png)<br>
#### Running A Simulation Using The Brain ####
<em> NOTE: The following only applies for AnyLogic Simulations. </em>

1. Go to the Playback Experiment in AnyLogic and paste the string from the [exported brain](#exporting-the-brain) following 'https://'. <br>
![image](https://user-images.githubusercontent.com/12861152/133013460-afe02fa2-5842-4f1d-b1b3-58a5e5d91484.png) <br>
2. Run the Playback Experiment. <br>
![image](https://user-images.githubusercontent.com/12861152/133013659-0c16eb99-1968-47e5-8a89-f24b4295c0ed.png)<br>
![image](https://user-images.githubusercontent.com/12861152/133013767-c61c4057-2cae-43f5-ab54-929bd96f650f.png)<br>

#### Other Scenarios For Using The Trained Brain ####
