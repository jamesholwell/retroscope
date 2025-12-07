"use strict";

var reconnectionPolicy = [
    0, 500, 1000, 1500, 2000, 5000, 10000, 10000, 10000, 10000, 10000,
    30000, 30000, 30000, 30000, 60000, 60000
];

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .withAutomaticReconnect(reconnectionPolicy)
    .build();

connection.onclose(() => updateStatus("Disconnected"));
connection.onreconnecting(() => updateStatus("Reconnecting..."));
connection.onreconnected(() => updateStatus("Connected"));

function updateStatus(status) {
    var statusElement = document.getElementById("connection-status");
    if (!statusElement) return;

    statusElement.textContent = status;
    
    if (status === "Connected") 
        statusElement.classList.add("connected");
    else
        statusElement.classList.remove("connected");
}

connection.start()
    .then(() => updateStatus("Connected"))
    .catch(() => updateStatus("Failed to connect"));
