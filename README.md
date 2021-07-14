# Industry X #
## Sandbox for Azure IoT, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [Azure IoT Hub Instance](#azure-iot-hub-instance)
	1. [Creating an IoT Hub](#creating-an-iot-hub)
	2. [Routing events](#routing-events)
2. [IoT Devices](#iot-devices)
	1. [Simulating telemetry](#simulating-telemetry)
	2. [Creating simulated devices](#CreatingSimulatedDevices)
	3. [Connecting devices to the hub](#ConnectingDevicesToTheHub)
	4. [Running simulated devices](#RunningSimulatedDevices)
3. [Azure IoT Hub Instance](#AzureIoTHubInstance)
	1. [Creating an IoT Hub](#CreatingAnIoTHub)
	2. [Connecting devices to the hub](#ConnectingDevicesToTheHub)
	3. [Routing events](#RoutingEvents)
4. [Azure Digital Twins Instance](#AzureDigitalTwinsInstance)
	1. [ADT Modelling](#ADTModelling)
	2. [Manufacturing Ontology](#ManufacturingOntology)
	3. [Creating the ADT Instance](#CreatingTheADTInstance)
	4. [Uploading your models](#UploadingYourModels)
	5. [Creating an asset twin](#CreatingAnAssetTwin)
	6. [Creating a process twin](#CreatingAProcessTwin)
	7. [Updating a twin](#UpdatingATwin)
5. [Azure TwinSync Functions](#AzureTwinSyncFunctions)
	1. [Using Azure Functions for TwinSync](#UsingAzureFunctionsForTwinSync)
	2. [Subscribing to events](#SubscribingToEvents)
	3. [Deploying functions](#DeployingFunctions)
6. [AnyLogic Simulation](#AnyLogicSimulation)
	1. [Creating an AnyLogic simulation](#CreatingAnAnyLogicSimulation)
	2. [Preparing the AnyLogic simulation for Bonsai](#PreparingTheAnyLogicSimulationForBonsai)
	3. [Attaching AnyLogic telemetry by querying ADT](#AttachingAnyLogicTelemetryByQueryingADT)
7. [Databricks Simulation](#DatabricksSimulation)
	1. [Creating a Databricks simulation](#CreatingADatabricksSimulation)
	2. [Preparing the Databricks simulation for Bonsai](#PreparingTheDatabricksSimulationForBonsai)
	2. [Attaching Databricks telemetry by querying ADT](#AttachingDatabricksTelemetryByQueryingADT)
7. [Microsoft Bonsai Teaching](#MicrosoftBonsaiTeaching)
	1. [Testing the simulation](#TestingTheSimulation)
	2. [Importing the simulation](#ImportingTheSimulation)
	3. [Creating the brain](#CreatingTheBrain)
	4. [Teaching the brain](#TeachingTheBrain)
	5. [Exporting the brain](#ExportingTheBrain)
	6. [Running a simulation using the brain](#RunningASimulationUsingTheBrain)
	7. [Other scenarios for using the trained brain](#OtherScenariosForUsingTheTrainedBrain)

>Because we do not have access to production IoT devices, we will be using simulated devices to create the IoT telemetry for our sandbox environment. These are independent code functions which run within Azure Containers, emulating physical devices, which are registered with an IoT Hub.
### Azure IoT Hub Instance ###
#### Creating An IoT Hub ####
To create an IoT Hub, instance follow these steps:  
1. Step 1
2. Step 2
#### Routing Events ####
To create an event route, follow these steps:
1. Step 1
2. Step 2
### IoT Devices ###
#### Simulating Telemetry ####
For our sandbox, we will create 3 simulated devices, each transmitting the same range of telemetry on a schedule, using randomisation to vary the values transmitted and the scheduling.
<h4 id="CreatingSimulatedDevices">Creating Simulated Devices</h4>
To create the simulated devices, follow these steps:
1. Step 1
2. Step 2
<h4 id="ConnectingDevicesToTheHub">Connecting Devices To The Hub</h4> 
To connect the simulated devices to the event hub, follow these steps:
1. Step 1
2. Step 2
<h4 id="RunningSimulatedDevices">Running Simulated Devices</h4>
To run the simulated devices, follow these steps:
1. Step 1
2. Step 2
<h3 id="AzureDigitalTwinsInstance">Azure Digital Twins Instance</h3>
<h4 id="ADTModelling">ADT Modelling</h4>
<h4 id="ManufacturingOntology">Manufacturing Ontology</h4>
<h4 id="CreatingTheADTInstance">Creating The ADT Instance</h4>
<h4 id="UploadingYourModels">Uploading Your Models</h4>
<h4 id="CreatingAnAssetTwin">Creating An Asset Twin</h4>
<h4 id="CreatingAProcessTwin">Creating A Process Twin</h4>
<h4 id="UpdatingATwin">Updating A Twin</h4>
<h3 id="AzureTwinSyncFunctions">Azure TwinSync Functions</h3>
<h4 id="UsingAzureFunctionsForTwinSync">Using Azure Functions For TwinSync</h4>
<h4 id="SubscribingToEvents">SubscribingToEvents</h4>
<h4 id="DeployingFunctions">Deploying Functions</h4>
<h3 id="AnyLogicSimulation">AnyLogic Simulation</h3>
<h4 id="CreatingAnAnyLogicSimulation">Creating An AnyLogic Simulation</h4>
<h4 id="PreparingTheAnyLogicSimulationForBonsai">Preparing The AnyLogic Simulation For Bonsai</h4>
<h4 id="AttachingAnyLogicTelemetryByQueryingADT">Attaching AnyLogic Telemetry By Querying ADT</h4>
<h3 id="DatabricksSimulation">Databricks Simulation</h3>
<h4 id="CreatingADatabricksSimulation">Creating a Databricks simulation</h4>
<h4 id="PreparingTheDatabricksSimulationForBonsai">Preparing the Databricks simulation for Bonsai</h4>
<h4 id="AttachingDatabricksTelemetryByQueryingADT">Attaching Databricks telemetry by querying ADT</h4>
<h3 id="MicrosoftBonsaiTeaching">Microsoft Bonsai Teaching</h3>
<h4 id="TestingTheSimulation">Testing The Simulation</h4>
<h4 id="ImportingTheSimulation">Importing The Simulation</h4>
<h4 id="CreatingTheBrain">Creating The Brain</h4>
<h4 id="TeachingTheBrain">Teaching The Brain</h4>
<h4 id="ExportingTheBrain">Exporting The Brain</h4>
<h4 id="RunningASimulationUsingTheBrain">Running A Simulation Using The Brain</h4>
<h4 id="OtherScenariosForUsingTheTrainedBrain">Other Scenarios For Using The Trained Brain</h4>
