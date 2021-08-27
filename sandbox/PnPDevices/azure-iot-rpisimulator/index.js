const run = require('./main.js').run;

const params = {
  provisioningHost: '<INSERT_PROVISIONING_HOST>',
  idScope: '<INSERT_ID_SCOPE>',
  registrationId: '<INSERT_REGISTRATION_ID>',
  symmetricKey: '<INSERT_SYMMETRIC_KEY>',
  modelId: 'dtmi:azurertos:devkit:gsgmxchip;2',
}

run(params);