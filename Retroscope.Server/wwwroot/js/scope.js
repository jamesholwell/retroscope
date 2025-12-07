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

connection.on("Title", (message) =>
    document.getElementById("title").textContent = message);

connection.on("Clear", () => 
    document.getElementById("canvas").innerHTML = "");

const svgParser = new DOMParser();
const allowedTags = new Set([
    'svg',
    'line', 'rect', 'circle', 'ellipse', 'polygon', 'polyline',
    'path', 'text', 'tspan', 'g', 'defs', 'clipPath', 'mask',
    'linearGradient', 'radialGradient', 'stop', 'pattern',
    'image', 'use', 'symbol', 'marker', 'foreignObject'
]);

// excludes event handlers, href and any other potentially dangerous attributes
const allowedAttributes = new Set([
    'x', 'y', 'x1', 'y1', 'x2', 'y2', 'cx', 'cy', 'r', 'rx', 'ry',
    'width', 'height', 'd', 'points', 
    'stroke', 'stroke-width', 'stroke-opacity', 'stroke-linecap', 'stroke-linejoin', 'stroke-dasharray', 'stroke-dashoffset', 
    'fill', 'fill-opacity', 'fill-rule', 
    'opacity', 
    'transform',
    'text-anchor', 'font-family', 'font-size', 'font-weight', 'font-style', 
    'marker-start', 'marker-mid', 'marker-end'
]);

const canvas = document.getElementById("canvas")
connection.on("Draw", (drawable) => {
    const doc = svgParser.parseFromString(drawable, 'image/svg+xml');
    
    // allows only a safe subset of SVG tags and attributes
    for (const element of doc.documentElement.querySelectorAll('*')) {
        if (!allowedTags.has(element.tagName.toLowerCase())) 
            return;
        
        for (const attr of element.attributes)
            if (!allowedAttributes.has(attr.name.toLowerCase())) 
                return;

        const adopted = document.adoptNode(element);
        canvas.appendChild(adopted);
    }
});
