$(document).ready(function () {
    $.ajax({
        url: '/api/statistics?restaurantId=bce002c0-54c7-4ed9-a7b5-07c71f332ca9',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            let aa =[];
            $.each(data.monthlyReport, function (idex, report) {

                console.log(report)
                aa.push({ y: report.month, a: report.ordersCount, b: report.ordersRevenu })
            });

            window.barChart = Morris.Bar({
                element: 'bar-chart',
                data: aa,
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['онлайн поръчки', 'оборот'],
                lineColors: ['#1e88e5', '#ff3321'],
                lineWidth: '3px',
                resize: true,
                redraw: true
            });

            window.donutChart = Morris.Donut({
                element: 'donut-chart',
                data: [
                    { label: "онлайн поръчки", value: data.ordersCount },
                    { label: "резервации", value: data.reservationsCount },
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



