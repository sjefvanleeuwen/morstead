function owl() {
  
    webvowl.app().initialize();

}
function dataTable(el) {
    $(el).DataTable();
}

    

function createCharts() {

    /* ChartJS
     * -------
     * Here we will create a few charts using ChartJS
     */
    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    var areaChartData = {
        labels: ['januari', 'february', 'maart', 'april', 'mei', 'juni', 'juli'],
        datasets: [
            {
                label: 'Zorg',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: '#f56954',
                pointRadius: false,
                pointColor: '#f56954',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: [1, 2, 8, 14, 22, 27, 29]
            },
            {
                label: 'Werk',
                backgroundColor: 'rgba(210, 214, 222, 1)',
                borderColor: '#00a65a',
                pointRadius: false,
                pointColor: 'rgba(210, 214, 222, 1)',
                pointStrokeColor: '#00a65a',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: [1, 3, 4, 6, 7, 8, 12]
            },
            {
                label: 'Inkomen',
                backgroundColor: 'rgba(210, 214, 222, 1)',
                borderColor: '#f39c12',
                pointRadius: false,
                pointColor: 'rgba(210, 214, 222, 1)',
                pointStrokeColor: '#f39c12',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: [5, 7, 12, 26, 42, 53, 63]
            }
        ]
    }

    //-------------
    //- LINE CHART -
    //--------------
    var lineChartCanvas = $('#ProefBerekeningenPerDomeinBurger').get(0).getContext('2d')
    var lineChartOptions = jQuery.extend(true, {}, areaChartOptions)
    var lineChartData = jQuery.extend(true, {}, areaChartData)
    lineChartData.datasets[0].fill = false;
    lineChartData.datasets[1].fill = false;
    lineChartData.datasets[2].fill = false;
    lineChartOptions.datasetFill = false

    var lineChart = new Chart(lineChartCanvas, {
        type: 'line',
        data: lineChartData,
        options: lineChartOptions
    })

    var lineChartCanvas = $('#ToenameRegelingenPerDomein').get(0).getContext('2d')
    var lineChartOptions = jQuery.extend(true, {}, areaChartOptions)
    var lineChartData = jQuery.extend(true, {}, areaChartData)
    lineChartData.datasets[0].fill = false;
    lineChartData.datasets[1].fill = false;
    lineChartData.datasets[2].fill = false;
    lineChartOptions.datasetFill = false

    var lineChart = new Chart(lineChartCanvas, {
        type: 'line',
        data: lineChartData,
        options: lineChartOptions
    })

    //-------------
    //- DONUT CHART -
    //-------------
    // Get context with jQuery - using jQuery's .get() method.

    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.

    var donutChartCanvas = $('#donutChart').get(0).getContext('2d')
    var donutData = {
        labels: [
            'Zorg',
            'Werk',
            'Inkomen',
        ],
        datasets: [
            {
                data: [700, 500, 400],
                backgroundColor: ['#f56954', '#00a65a', '#f39c12'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

    var donutChart = new Chart(donutChartCanvas,
        {
            type: 'doughnut',
            data: donutData,
            options: donutOptions
        });


}
