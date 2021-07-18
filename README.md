# Industry X #
## Sandbox for Azure IoT, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [Create Sandbox Resource Group](#create-sandbox-resource-group)
	1. [Access Azure Cloud Shell](#access-azure-cloud-shell)
	2. [Creating Your Sandbox Resource Group](#creating-your-sandbox-resource-group)
2. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
3. [IoT Devices](#iot-devices)
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
6. [AnyLogic Simulation](#anylogic-simulation)
	1. [Creating an AnyLogic simulation](#creating-an-anylogic-simulation)
	2. [Preparing the AnyLogic simulation for Bonsai](#preparing-the-anylogic-simulation-for-bonsai)
	3. [Attaching AnyLogic telemetry by querying ADT](#attaching-anylogic-telemetry-by-querying-adt)
7. [Databricks Simulation](#databricks-simulation)
	1. [Creating a Databricks simulation](#creating-a-databricks-simulation)
	2. [Preparing the Databricks simulation for Bonsai](#preparing-the-databricks-simulation-for-bonsai)
	2. [Attaching Databricks telemetry by querying ADT](#attaching-databricks-telemetry-by-querying-adt)
8. [Microsoft Bonsai Teaching](#microsoft-bonsai-teaching)
	1. [Testing the simulation](#testing-the-simulation)
	2. [Importing the simulation](#importing-the-simulation)
	3. [Creating the brain](#creating-the-brain)
	4. [Teaching the brain](#teaching-the-brain)
	5. [Exporting the brain](#exporting-the-brain)
	6. [Running a simulation using the brain](#running-a-simulation-using-the-brain)
	7. [Other scenarios for using the trained brain](#other-scenarios-for-using-the-trained-brain)

>Because we do not have access to production IoT devices, we will be using simulated devices to create the IoT telemetry for our sandbox environment. These are independent code functions which run within Azure Containers, emulating physical devices, which are registered with an IoT Hub.
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
4. Execute the following command in Azure Cloud Shell to run the PowerHell script 
```zsh
./SimulatorCloudRunner.ps1
```
5. When prompted for an IoTHubConnectionString, paste the connection string you copied in step 4, and press 'Enter'
6. After a few seconds, you should see a JSON confirmation that the process has started
7. After around 1 minute, the 3 simulated device containers will have been deployed. Check your resource group to confirm.
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
Within our sadnbox, we require a single Azure Digital Twins (ADT) instance. Each instance can support many different digital twins based on many different models. For simplicity, it is best practice to use a separate ADT instance for each solution domain. ADT hosts the definitions of digital twins, based on underlying models, as well as the data describing the digital twins, including twin insances, components, relationships and properties. It does not, however, store underlying property and telemetry values, which are accessed from underlying storage. This makes ADT a highly performant abstraction of the digital twins it hosts.
#### ADT Modelling ####
ADT uses models to define digital twin types. Modelling is defined in a JSON-LD-based descriptive language called DTDL (Digital Twins Definition Language). Each DTDL model is an interface which defines the permissible structure of a digital twin. DTDL supports inheritance, such that one or more interfaces can be a base for other derived interfaces. A model is a definition which can be instantiated as a digital twin. A model can alternatively be a component, used to compose other models, but not intended for instantiation by itself. An example would be a smartphone device, defined as containing components defining a front camera and a rear camera.
#### Manufacturing Ontology ####
For manufaturing scenarios, we need a set of base models which can form the foundation for describing manufacturing assets and processes. These base models need to be extendible to problem-specific scenrios, such as production monitoring, optimization and simulation, as well as wider scenarios such as materials handling and supply chain modelling. These base models need to follow industry-specific standards, such as ISA-95 and ISA-88, and be gathered into an overall model known as a manufacturing ontology. 
The starting point for our ontology is this basic data model as defined by the Industrial Automation standard ISA-95:
<img width="395" alt="Equipment-hierarchy-model-11-p-26" src="https://user-images.githubusercontent.com/1761529/126053643-c941d7fb-ca29-41b0-b5ac-ab6922ea58a8.png">
From this, we have derived a basic manufacturing ontology as captured in this diagram. We are to use this ontology within our sandbox environments:
![Screenshot 2021-06-15 120456](https://user-images.githubusercontent.com/1761529/126053677-82c4351f-5e61-4971-88e4-5201c23e4736.jpg)
#### Creating The ADT Instance ####
#### Uploading Your Models ####
#### Creating An Asset Twin ####
#### Creating A Process Twin ####
#### Updating A Twin ####
### Azure TwinSync Functions ###
#### Using Azure Functions For TwinSync ####
#### Subscribing To Events ####
#### Deploying Functions ####
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
