var diagramUrl = 'https://cdn.staticaly.com/gh/bpmn-io/bpmn-js-examples/dfceecba/starter/diagram.bpmn';

// modeler instance
var bpmnModeler;

function createModeler() {
    bpmnModeler = new BpmnJS({
        container: '#canvas_bpmn',
        keyboard: {
            bindTo: window
        }

    });
}

/**
 * Save diagram contents and print them to the console.
 */
function exportDiagram() {

    bpmnModeler.saveXML({ format: true }, function (err, xml) {

        if (err) {
            return console.error('could not save BPMN 2.0 diagram', err);
        }

        alert('Diagram exported. Check the developer tools!');

        console.log('DIAGRAM', xml);
    });
}

/**
 * Open diagram in our modeler instance.
 *
 * @param {String} bpmnXML diagram to display
 */
function openDiagram(bpmnXML) {

    // import diagram
    bpmnModeler.importXML(bpmnXML, function (err) {

        if (err) {
            return console.error('could not import BPMN 2.0 diagram', err);
        }

        // access modeler components
        var canvas = bpmnModeler.get('canvas');
        var overlays = bpmnModeler.get('overlays');


        // zoom to fit full viewport
        canvas.zoom('fit-viewport');

        // attach an overlay to a node
        overlays.add('SCAN_OK', 'note', {
            position: {
                bottom: 0,
                right: 0
            },
            html: '<div class="diagram-note">Mixed up the labels?</div>'
        });

        // add marker
        canvas.addMarker('SCAN_OK', 'needs-discussion');
    });
}


// load external diagram file via AJAX and open it
//$.get(diagramUrl, openDiagram, 'text');

// wire save button
//$('#save-button').click(exportDiagram);