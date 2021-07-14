# Industry X #
## Sandbox for Azure IoT, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
	2. [Routing events](#routing-events)
2. [IoT Devices](#iot-devices)
	1. [Simulating telemetry](#simulating-telemetry)
	2. [Creating simulated devices](#creating-simulated-devices)
	3. [Connecting devices to the hub](#connecting-devices-to-the-hub)
	4. [Running simulated devices](#running-simulated-devices)
3. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
	2. [Connecting devices to the hub](#connecting-devices-to-the-hub)
	3. [Routing events](#routing-events)
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
7. [Microsoft Bonsai Teaching](#microsoft-bonsai-teaching)
	1. [Testing the simulation](#testing-the-simulation)
	2. [Importing the simulation](#importing-the-simulation)
	3. [Creating the brain](#creating-the-brain)
	4. [Teaching the brain](#teaching-the-brain)
	5. [Exporting the brain](#exporting-the-brain)
	6. [Running a simulation using the brain](#running-a-simulation-using-the-brain)
	7. [Other scenarios for using the trained brain](#other-scenarios-for-using-the-trained-brain)

>Because we do not have access to production IoT devices, we will be using simulated devices to create the IoT telemetry for our sandbox environment. These are independent code functions which run within Azure Containers, emulating physical devices, which are registered with an IoT Hub.
### Azure IoT Hub Instance ###
#### Creating An IoT Hub ####
To create an IoT Hub, instance follow these steps:  
1. Execute the following command using the Azure CLI
```lang-bsh
az deployment group create --resource-group industryx-sandbox-cl1 --template-uri https://raw.githubusercontent.com/lowndesc/industryx/24c2315c216368c33028a94487193865a21b8eb3/sandbox/azuredeploy.json
```
3. Step 2
#### Routing Events ####
To create an event route, follow these steps:
1. Step 1
2. Step 2
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
