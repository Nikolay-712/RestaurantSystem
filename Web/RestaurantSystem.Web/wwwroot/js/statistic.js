$(document).ready(function () {
    var baseUrl = (window.location).href; // You can also use document.URL
    var restaurantId = baseUrl.substring(baseUrl.lastIndexOf('=') + 1);

    $.ajax({
        url: '/api/statistics',
        type: 'GET',
        data: { restaurantId: restaurantId },
        dataType: 'json',
        success: function (data) {
            let monthlyReport =[];
            $.each(data.monthlyReport, function (idex, report) {

                console.log(report)
                monthlyReport.push({ y: report.month, a: report.ordersCount, b: report.ordersRevenu })
            });

            window.barChart = Morris.Bar({
                element: 'bar-chart',
                data: monthlyReport,
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['онлайн поръчки', 'оборот лв.'],
                lineColors: ['#1e88e5', '#ff3321'],
                lineWidth: '3px',
                resize: true,
                redraw: true
            });

            window.donutChart = Morris.Donut({
                element: 'donut-chart',
                data: [
                    { label: "онлайн поръчки", value: data.ordersCount },
                    { label: "отказани поръчки", value: data.rejectedOrdersCount },
                    { label: "резервации", value: data.reservationsCount },
                    { label: "отказани резервации", value: data.rejectedReservationsCount},
                ],
                resize: true,
                redraw: true
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
});

$(document).ready(function () {
    barChart();
    donutChart();

    $(window).resize(function () {
        window.barChart.redraw();
        window.donutChart.redraw();
    });
});


function pieChart() {
    var paper = Raphael("pie-chart");
    paper.piechart(
        100, // pie center x coordinate
        100, // pie center y coordinate
        90,  // pie radius
        [18.373, 18.686, 2.867, 23.991, 9.592, 0.213], // values
        {
            legend: ["Windows/Windows Live", "Server/Tools", "Online Services", "Business", "Entertainment/Devices", "Unallocated/Other"]
        }
    );
} 
