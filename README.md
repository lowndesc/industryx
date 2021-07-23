# Industry X #
## Sandbox for Azure IoT, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [Create Sandbox Resource Group](#create-sandbox-resource-group)
	1. [Access Azure Cloud Shell](#access-azure-cloud-shell)
	2. [Creating Your Sandbox Resource Group](#creating-your-sandbox-resource-group)
2. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
3. [Simulated IoT Devices](#iot-devices)
	1. [Simulating telemetry](#simulating-telemetry)
	2. [Creating simulated devices](#creating-simulated-devices)
	3. [Connecting devices to the hub](#connecting-devices-to-the-hub)
	4. [Running simulated devices](#running-simulated-devices)
4. [Azure Digital Twins Instance](#azure-digital-twins-instance)
	1. [ADT Modelling](#adt-modelling)
	2. [Manufacturing Ontology](#manufacturing-ontology)
	3. [Creating the ADT Instance](#creating-the-adt-instance)
	4. [Uploading your models](#uploading-your-models)
	5. [Creating an asset twin](#creating-an-asset-twin)
	6. [Creating a process twin](#creating-a-process-twin)
	7. [Updating a twin](#updating-a-twin)
5. [Azure TwinSync Functions](#azure-twinsync-functions)
	1. [Using Azure Functions for TwinSync](#using-azure-functions-for-twinsync)
	2. [Subscribing to events](#subscribing-to-events)
	3. [Deploying functions](#deploying-functions)
6. [Plug-and-Play IoT Devices](#plug-and-play-iot-devices)  
	1. PnP IoT and ADT Architecture
	2. Deploying PnP Functions
	3. Auto-Provisioning a Simlated PnP Device
	4. Provisioning a Physical PnP Device
	5. Verifying PnP Telemetry in ADT
	6. Auto-Retiring PnP Devices
7. [AnyLogic Simulation](#anylogic-simulation)
	1. [Creating an AnyLogic simulation](#creating-an-anylogic-simulation)
	2. [Preparing the AnyLogic simulation for Bonsai](#preparing-the-anylogic-simulation-for-bonsai)
	3. [Attaching AnyLogic telemetry by querying ADT](#attaching-anylogic-telemetry-by-querying-adt)
8. [Databricks Simulation](#databricks-simulation)
	1. [Creating a Databricks simulation](#creating-a-databricks-simulation)
	2. [Preparing the Databricks simulation for Bonsai](#preparing-the-databricks-simulation-for-bonsai)
	2. [Attaching Databricks telemetry by querying ADT](#attaching-databricks-telemetry-by-querying-adt)
9. [Microsoft Bonsai Teaching](#microsoft-bonsai-teaching)
	1. [Testing the simulation](#testing-the-simulation)
	2. [Importing the simulation](#importing-the-simulation)
	3. [Creating the brain](#creating-the-brain)
	4. [Teaching the brain](#teaching-the-brain)
	5. [Exporting the brain](#exporting-the-brain)
	6. [Running a simulation using the brain](#running-a-simulation-using-the-brain)
	7. [Other scenarios for using the trained brain](#other-scenarios-for-using-the-trained-brain)

>Because we do not have access to production IoT devices, we will be using simulated devices to create the IoT telemetry for our sandbox environment. These are independent code functions which run within Azure Containers, emulating physical devices, which are registered with an IoT Hub.

Before you begin, you may want to clone this entire repository to your local machine, to make some of the steps below more straightforward.

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
For our sandbox, we will create 3 simulated devices, each transmitting the same range of telemetry on a schedule, using randomisation to vary the values transmitted and the scheduling.
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
ADT uses models to define digital twin types. Modelling is defined in a JSON-LD-based descriptive language called DTDL (Digital Twins Definition Language). Each DTDL model is an interface which defines the permissible structure of a digital twin. DTDL supports inheritance, such that one or more interfaces can be a base for other derived interfaces. A model is a generally a definition which can be instantiated as a digital twin. Howevere, a model can also be a component, used to compose other models, but not intended for instantiation by itself. An example would be a smartphone device, defined as containing components defining a front camera and a rear camera.
#### Manufacturing Ontology ####
For manufaturing scenarios, we need a set of base models which can form the foundation for describing manufacturing assets and processes. These base models need to be extendible to problem-specific scenrios, such as production monitoring, optimization and simulation, as well as wider scenarios such as materials handling and supply chain modelling. These base models need to follow industry-specific standards, such as ISA-95 and ISA-88, and be gathered into an overall model known as a manufacturing ontology. 
The starting point for our ontology is this basic data model as defined by the Industrial Automation standard ISA-95: 
![image](https://user-images.githubusercontent.com/1761529/126053763-ffbe065d-d0d1-48d3-a434-666afd45f4bf.png)  
From this, we have derived a basic manufacturing ontology as captured in this diagram. We are to use this ontology within our sandbox environments:  
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
![image](https://user-images.githubusercontent.com/1761529/126058709-229b416f-3a7d-4f56-9477-9027f88d61f1.png)
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
SELECT * FROM DIGITAL TWINS WHERE IS_OF_MODEL('dtmi:isa95:core:Asset;1')
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
#### Auto-Provisioning a Simlated PnP Device ####
![image](https://user-images.githubusercontent.com/1761529/126741050-98ee91d1-aed3-48d4-9b2d-2d3e39787ab2.png)
#### Provisioning a Physical PnP Device ####
![20210723_143419](https://user-images.githubusercontent.com/1761529/126741223-2aece734-8cf3-4645-8c86-84f253656995.jpg)
#### Verifying PnP Telemetry in ADT ####
#### Auto-Retiring PnP Devices ####
![image](https://user-images.githubusercontent.com/1761529/126741148-e32ee60f-8018-44ca-b21d-8c4d7084f704.png)
### AnyLogic Simulation ###
#### Creating An AnyLogic Simulation ####
#### Preparing The AnyLogic Simulation For Bonsai ####
#### Attaching AnyLogic Telemetry By Querying ADT ####
### Databricks Simulation ###
#### Creating a Databricks simulation ####
#### Preparing the Databricks simulation for Bonsai ####
#### Attaching Databricks telemetry by querying ADT ####
### Microsoft Bonsai Teaching ###
#### Testing The Simulation ####
#### Importing The Simulation ####
#### Creating The Brain ####
#### Teaching The Brain ####
#### Exporting The Brain ####
#### Running A Simulation Using The Brain ####
#### Other Scenarios For Using The Trained Brain ####
