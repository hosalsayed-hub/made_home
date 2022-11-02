/*
 *  Document   : base_comp_chartjs_v2.js
 *  Author     : pixelcave
 *  Description: Custom JS code used in Chart.js v2 Page
 */

var BaseCompChartJSv2 = function() {
    // Chart.js v2 Charts, for more examples you can check out http://www.chartjs.org/docs
    var initChartsChartJSv2 = function () {
        // Set Global Chart.js configuration
        Chart.defaults.global.defaultFontColor              = '#999';
        Chart.defaults.global.defaultFontFamily             = 'Open Sans';
        Chart.defaults.global.defaultFontStyle              = '600';
        Chart.defaults.scale.gridLines.color                = "rgba(0,0,0,.05)";
        Chart.defaults.scale.gridLines.zeroLineColor        = "rgba(0,0,0,.1)";
        Chart.defaults.global.elements.line.borderWidth     = 2;
        Chart.defaults.global.elements.point.radius         = 4;
        Chart.defaults.global.elements.point.hoverRadius    = 6;
        Chart.defaults.global.tooltips.titleFontFamily      = 'Source Sans Pro';
        Chart.defaults.global.tooltips.bodyFontFamily       = 'Open Sans';
        Chart.defaults.global.tooltips.cornerRadius         = 3;
        Chart.defaults.global.legend.labels.boxWidth        = 15;
        

        // Get Chart Containers
        var $chart2DonutCon  = jQuery('.js-chartjs2-donut');

        // Set Chart and Chart Data variables
        var $chart2Donut;
        var $chart2PolarPieDonutData;


        // Polar/Pie/Donut Data
        var $chart2PolarPieDonutData = {
            labels: [
                'Earnings',
                'Sales',
                'Tickets'
            ],
            datasets: [{
                data: [
                    48,
                    26,
                    26
                ],
                backgroundColor: [
                    'rgba(171, 227, 125, 1)',
                    'rgba(250, 219, 125, 1)',
                    'rgba(117, 176, 235, 1)'
                ],
                hoverBackgroundColor: [
                    'rgba(171, 227, 125, .75)',
                    'rgba(250, 219, 125, .75)',
                    'rgba(117, 176, 235, .75)'
                ],
                borderWidth: [
                    0,
                    0,
                    0
                ]
            }]
        };

        // Init Charts
        $chart2Donut = new Chart($chart2DonutCon, { type: 'doughnut', data: $chart2PolarPieDonutData });
    };

    return {
        init: function () {
            // Init charts
            initChartsChartJSv2();
        }
    };
}();

// Initialize when page loads
jQuery(function(){ BaseCompChartJSv2.init(); });