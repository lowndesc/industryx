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
1. Execute the following command using the Azure CLI
```zsh
az deployment group create --resource-group <<your-sandbox-name>> --template-uri https://raw.githubusercontent.com/lowndesc/industryx/main/sandbox/IoTHub/azuredeploy.json
```
2. Navigate to your sandbox resource group within the Azure Portal. You should see two resources similar to this image ![image](https://user-images.githubusercontent.com/1761529/125631131-6d02ef16-9057-406a-aaa4-e1d6bc44f177.png)  
### IoT Devices ###
#### Simulating Telemetry ####
For our sandbox, we will create 3 simulated devices, each transmitting the same range of telemetry on a schedule, using randomisation to vary the values transmitted and the scheduling.
#### Creating Simulated Devices ####
To create the simulated devices, follow these steps:
1. Step 1
2. Step 2
#### Connecting Devices To The Hub #### 
To connect the simulated devices to the event hub, follow these steps:
1. Step 1
2. Step 2
#### Running Simulated Devices ####
To run the simulated devices, follow these steps:
1. Step 1
2. Step 2
### Azure Digital Twins Instance ###
#### ADT Modelling ####
#### Manufacturing Ontology ####
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
