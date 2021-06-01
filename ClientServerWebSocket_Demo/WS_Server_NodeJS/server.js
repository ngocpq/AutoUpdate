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

function adminConnection(ws) {
    ws.on('message', function incoming(message) {
        console.log('received: %s', message);
    });
    ws.on('disconnect', function disconnect() {
        console.log('disconnect %s', this);
    });
    ws.on('close', function close(code, signal) {
        console.log('close code: %s, signal: %s', code, signal);
    });
    ws.on('exit', function exit(code, signal) {
        console.log('exit, code: %s, signal: %s', code, signal);
    });
    ws.send('Admin console');
}

function userConnection(ws, req) {
    console.log('Server: %s', this);
    console.log('ws: %s', ws);

    ws.on('message', function incoming(message) {
        console.log('received: %s', message);
    });    

    ws.send('Hello, this is Services console');
}

function upgradeHttp2WebSocket(request, socket, head) {
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
}

wssAdmin.on('connection', adminConnection);

wssServices.on('connection', userConnection);

httpServer.on('upgrade', upgradeHttp2WebSocket);

httpServer.listen(port);