$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:44319/api/statistics?restaurantId=bce002c0-54c7-4ed9-a7b5-07c71f332ca9',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            console.log('Information' + JSON.stringify(data));

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

function barChart() {
    window.barChart = Morris.Bar({
        element: 'bar-chart',
        data: [
            { y: 'яну', a: 100, b: 90 },
            { y: 'февруари', a: 75, b: 65 },
            { y: 'март', a: 50, b: 40 },
            { y: 'април', a: 75, b: 65 },
            { y: 'май', a: 50, b: 40 },
            { y: 'юни', a: 75, b: 65 },
            { y: 'юли', a: 100, b: 90 },
            { y: 'август', a: 100, b: 90 },
            { y: 'септември', a: 100, b: 90 },
            { y: 'октонври', a: 100, b: 90 },
            { y: 'ноември', a: 100, b: 90 },
            { y: 'декември', a: 100, b: 90 },
        ],
        xkey: 'y',
        ykeys: ['a', 'b'],
        labels: ['Series A', 'Series B'],
        lineColors: ['#1e88e5', '#ff3321'],
        lineWidth: '3px',
        resize: true,
        redraw: true
    });
}



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