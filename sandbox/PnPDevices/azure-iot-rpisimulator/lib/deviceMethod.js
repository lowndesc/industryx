exports.onStart = (request, response) => {
    console.log('Try to invoke method start(' + request.payload + ')');

    response.send(200, 'Successully start sending message to cloud', function (err) {
    if (err) {
        console.error('[IoT hub Client] Failed sending a method response:\n' + err.message);
    }
    });
};

exports.onStop = (request, response) => {
    console.log('Try to invoke method stop(' + request.payload + ')');

    response.send(200, 'Successully stop sending message to cloud', function (err) {
    if (err) {
        console.error('[IoT hub Client] Failed sending a method response:\n' + err.message);
    }
    });
}