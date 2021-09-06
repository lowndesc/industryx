const run = require('./main.js').run;

// choose a model
const MODELID_MXCHIP_AZ3166 = 'dtmi:azurertos:devkit:gsgmxchip;2';
const MODELID_SIMULATED_SENSOR = 'dtmi:isa95:capability:SimulatedSensor;1'; // NOTE: Simulated Sensor can't be used for auto-provision demo. Only works when device already has a twin in ADT.

const params = {
  provisioningHost: '<INSERT_PROVISIONING_HOST>',
  idScope: '<INSERT_ID_SCOPE>',
  registrationId: '<INSERT_REGISTRATION_ID>',
  symmetricKey: '<INSERT_SYMMETRIC_KEY>',
  modelId: MODELID_MXCHIP_AZ3166,
}

run(params);