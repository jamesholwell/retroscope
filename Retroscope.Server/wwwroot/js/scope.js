"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});
