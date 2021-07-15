
param (
    [string]$Location = "japaneast",
    [string]$ResourceGroup = "industryx-sandbox-cl1",
    [int]$DeviceCount = 3,
    [int]$ContainerCount = 3,
    [int]$MessageCount = 0,
    [int]$Interval = 5000,
    [string]$FixPayload='',
    [int]$FixPayloadSize=0,
    [string]$Template = '{ \"deviceId\": \"$.DeviceId\", \"temperature\": \"$.Temperature\", \"humidity\": \"$.Humidity\", \"power\": \"$.Power\", \"vibration\": \"$.Vibration\", \"uptime\": \"$.Uptime\", \"capacity\": \"$.Capacity\", \"wait\": \"$.Wait\", \"delay\": \"$.Delay\", \"arrivalRate\": \"$.ArrivalRate\", \"Ticks\": \"$.Ticks\", \"Counter\": \"$.Counter\", \"time\": \"$.Time\", \"engine\": \"$.Engine\", \"source\": \"$.MachineName\" }',
    [string]$Header = '',
    [string]$Variables = '[{name: \"Temperature\", random: true, max: 3187, min: 1971}, {name: \"Humidity\", random: true, max: 5425, min: 4781}, {name: \"Power\", random: false, max: 358701, min: 129699}, {name: \"Vibration\", random: true, max: 949, min: 401}, {name: \"Uptime\", random: true, max: 10000, min: 8901}, {name: \"Capacity\", random: true, max: 1200, min: 100}, {name: \"Wait\", random: true, max: 36000, min: 1000}, {name: \"Delay\", random: true, max: 144000, min: 36000}, {name: \"ArrivalRate\", random: true, max: 200, min: 50}, {name: \"Counter\", min:100}, {name: \"Engine\", values: [\"on\", \"off\"]}]',
    [Parameter(Mandatory=$true)][string]$IotHubConnectionString,
    [string]$Image = "iottelemetrysimulator/azureiot-telemetrysimulator:latest",
    [double]$Cpu = 1.0,
    [double]$Memory = 1.5
 )

 az group create --name $ResourceGroup --location $Location

 $i = 0
 $deviceIndex = 1
 $devicesPerContainer = [int]($DeviceCount / $ContainerCount)
 while($i -lt $ContainerCount)
 {
    $i++
    $containerName = "iotsimulator-" + $i.ToString()
    az container create -g $ResourceGroup --no-wait --location $Location --restart-policy Never --cpu $Cpu --memory $Memory --name $containerName --image $Image --environment-variables IotHubConnectionString=$IotHubConnectionString Template=$Template Variables=$Variables DeviceCount=$devicesPerContainer MessageCount=$MessageCount DeviceIndex=$deviceIndex Interval=$Interval Header=$Header FixPayloadSize=$FixPayloadSize FixPayload=$FixPayload

    $deviceIndex = $deviceIndex + $devicesPerContainer
 }

 Write-Host "Creation of" $ContainerCount "container instances has started. Telemetry will start flowing soon"
