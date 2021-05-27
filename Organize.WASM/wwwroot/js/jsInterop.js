window.addEventListener('resize',
    () => {
        //Call the static Method OnResize in the Organize assembly
        console.log("Static Resize from js");
        DotNet.invokeMethodAsync("Organize.WASM", "OnResize");
    });

//window.blazorDimension = {
//    getWidth: () => window.innerWidth
//}

window.blazorResize = {
    assignments: [],
    registerReferenceForResizeEvent: (name,dotnetReference) => {
        const handler = () => {
            console.log("HandleResize from js");
            dotnetReference.invokeMethodAsync("HandleResize", window.innerWidth, window.innerHeight);
        }
        const assignment = {
            name: name,
            handler: handler
        }
        blazorResize.assignments.push(assignment);
        window.addEventListener("resize", assignment.handler);
    },
    unRegister: (name) => {
        const assignment = blazorResize.assignments.find(a => a.name === name);
        if (assignment != null) {
            console.log(assignment);
            window.removeEventListener("resize", assignment.handler);
        }
    }
}