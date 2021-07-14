# Industry X #
## Sandbox for Azure IoT, Azure Digital Twins and Bonsai Simulation Teaching ##
### Summary ###
1. [IoT Devices](#IoTDevices)
	1. [Simulating telemetry](#SimulatingTelemetry)
	2. [Creating simulated devices](#CreatingSimulatedDevices)
	3. [Running simulated devices](#RunningSimulatedDevices)
2. [Azure IoT Hub Instance](#AzureIoTHubInstance)
	1. [Creating an IoT Hub](#CreatingAnIoTHub)
	3. [Connecting devices to the hub](#ConnectingDevicesToTheHub)
	4. [Routing events](#RoutingEvents)
3. [Azure Digital Twins Instance](#AzureDigitalTwinsInstance)
	1. [ADT Modelling](#ADTModelling)
	2. [Manufacturing Ontology](#ManufacturingOntology)
	3. [Creating the ADT Instance](#CreatingTheADTInstance)
	4. [Uploading your models](#UploadingYourModels)
	5. [Creating an asset twin](#CreatingAnAssetTwin)
	6. [Creating a process twin](#CreatingAProcessTwin)
	7. [Updating a twin](#UpdatingATwin)
4. [Azure TwinSync Functions](#AzureTwinSyncFunctions)
	1. [Using Azure Functions for TwinSync](#UsingAzureFunctionsForTwinSync)
	2. [Subscribing to events](#SubscribingToEvents)
	3. [Deploying functions](#DeployingFunctions)
5. [AnyLogic Simulation](#AnyLogicSimulation)
	1. [Creating a simulation](#CreatingASimulation)
	2. [Preparing for Bonsai](#PreparingForBonsai)
	3. [Attaching telemetry by querying ADT](#AttachingTelemetryByQueryingADT)
6. [Microsoft Bonsai Teaching](#MicrosoftBonsaiTeaching)
	1. [Testing the simulation](#TestingTheSimulation)
	2. [Importing the simulation](#ImportingTheSimulation)
	3. [Creating the brain](#CreatingTheBrain)
	4. [Teaching the brain](#TeachingTheBrain)
	5. [Exporting the brain](#ExportingTheBrain)
	6. [Running a simulation using the brain](#RunningASimulationUsingTheBrain)
	7. [Other scenarios for using the trained brain](#OtherScenariosForUsingTheTrainedBrain)

<h3 id="IoTDevices">IoT Devices</h3>
<h4 id="SimulatingTelemetry">Simulating Telemetry</h4>
<h4 id="CreatingSimulatedDevices">Creating Simulated Devices</h4>
<h4 id="RunningSimulatedDevices">Running Simulated Devices</h4>
<h3 id="AzureIoTHubInstance">Azure IoT Hub Instance</h3>
<h4 id="CreatingAnIoTHub">Creating An IoT Hub</h4>
<h4 id="ConnectingDevicesToTheHub">Connecting Devices To The Hub</h4> 
<h4 id="RoutingEvents">Routing Events</h4>
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
<h4 id="CreatingASimulation">Creating A Simulation</h4>
<h4 id="PreparingForBonsai">Preparing For Bonsai</h4>
<h4 id="AttachingTelemetryByQueryingADT">AttachingTelemetryByQueryingADT</h4>
<h3 id="MicrosoftBonsaiTeaching">Microsoft Bonsai Teaching</h3>
<h4 id="TestingTheSimulation">Testing The Simulation</h4>
<h4 id="ImportingTheSimulation">Importing The Simulation</h4>
<h4 id="CreatingTheBrain">Creating The Brain</h4>
<h4 id="TeachingTheBrain">Teaching The Brain</h4>
<h4 id="ExportingTheBrain">Exporting The Brain</h4>
<h4 id="RunningASimulationUsingTheBrain">Running A Simulation Using The Brain</h4>
<h4 id="OtherScenariosForUsingTheTrainedBrain">Other Scenarios For Using The Trained Brain</h4>
