"use strict";

(function() {
    const svg = document.getElementById("canvas");
    if (!svg) return;
    
    const initialViewBox = { x: 0, y: 0, width: 1024, height: 1024 };
    let viewBox = { ...initialViewBox };
    let isPanning = false;
    let startPoint = { x: 0, y: 0 };
    
    function updateViewBox() {
        svg.setAttribute('viewBox', `${viewBox.x} ${viewBox.y} ${viewBox.width} ${viewBox.height}`);
    }
    
    function resetViewBox() {
        viewBox = { ...initialViewBox };
        updateViewBox();
    }
    
    function getMousePosition(evt) {
        const CTM = svg.getScreenCTM();
        return {
            x: (evt.clientX - CTM.e) / CTM.a,
            y: (evt.clientY - CTM.f) / CTM.d
        };
    }
    
    // zooming
    svg.addEventListener('wheel', function(evt) {
        evt.preventDefault();
        
        const mousePos = getMousePosition(evt);
        const zoomFactor = evt.deltaY > 0 ? 1.1 : 0.9;
        
        // calculate new viewport
        const newWidth = viewBox.width * zoomFactor;
        const newHeight = viewBox.height * zoomFactor;
        
        // orient viewport centre on mouse position
        viewBox.x += (mousePos.x - viewBox.x) * (1 - zoomFactor);
        viewBox.y += (mousePos.y - viewBox.y) * (1 - zoomFactor);
        
        viewBox.width = newWidth;
        viewBox.height = newHeight;
        
        updateViewBox();
    });
    
    // panning
    svg.addEventListener('mousedown', function(evt) {
        isPanning = true;
        startPoint = getMousePosition(evt);
        svg.style.cursor = 'grabbing';
    });
    
    svg.addEventListener('mousemove', function(evt) {
        if (!isPanning) return;
        
        evt.preventDefault();
        const currentPoint = getMousePosition(evt);
        
        viewBox.x -= (currentPoint.x - startPoint.x);
        viewBox.y -= (currentPoint.y - startPoint.y);
        
        updateViewBox();
        startPoint = getMousePosition(evt);
    });
    
    svg.addEventListener('mouseup', function() {
        isPanning = false;
        svg.style.cursor = 'grab';
    });
    
    svg.addEventListener('mouseleave', function() {
        isPanning = false;
        svg.style.cursor = 'default';
    });
    
    // reset viewbox on double click
    svg.addEventListener('dblclick', function(evt) {
        evt.preventDefault();
        resetViewBox();
    });
    
    // show appopriate cursor
    svg.style.cursor = 'grab';
})();
