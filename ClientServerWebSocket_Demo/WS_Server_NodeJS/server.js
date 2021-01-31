'use strict';
var port = process.env.PORT || 1337;

const http = require('http');
const WebSocket = require('ws');
const url = require('url');

const httpServer = http.createServer();
const wssAdmin = new WebSocket.Server({ noServer: true });
const wssServices = new WebSocket.Server({ noServer: true });


function processUserIncommingMessage(message) {
    console.log('ws: %s', this);
    console.log('received: %s', message);
}

wssAdmin.on('connection', function connection(ws) {
    ws.on('message', function incoming(message) {
        console.log('received: %s', message);
    });

    ws.send('Admin console');
});

wssServices.on('connection', function connection(ws, req) {
    console.log('Server: %s', this);
    console.log('ws: %s', ws);

    ws.on('message', function incoming(message) {
        console.log('received: %s', message);
    });
    //ws.on('message', processUserIncommingMessage);

    ws.send('Hello, this is Services console');
});

httpServer.on('upgrade', function upgrade(request, socket, head) {
    const pathname = url.parse(request.url).pathname;

    if (pathname === '/admin') {
        wssAdmin.handleUpgrade(request, socket, head, function done(ws) {
            wssAdmin.emit('connection', ws, request);
        });
    } else if (pathname === '/services') {
        wssServices.handleUpgrade(request, socket, head, function done(ws) {
            wssServices.emit('connection', ws, request);
        });
    } else {
        socket.destroy();
    }
});

httpServer.listen(port);