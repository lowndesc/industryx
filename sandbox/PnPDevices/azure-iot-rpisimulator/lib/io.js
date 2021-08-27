
exports.readSensorData = async () => {
  const data =   {
    "temperature": "2195",
    "humidity": "5397",
    "power": "129716",
    "vibration": "764",
    "uptime": "9237",
    "capacity": "902",
    "wait": "33346",
    "delay": "43054",
    "arrivalRate": "113",
  };

  return data;
}

exports.blinkLED = () => {
    console.log('LED Blinked!');
}