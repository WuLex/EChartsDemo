// 基于准备好的dom，初始化echarts实例
$(function () {
    LoadChart();

    $("#btnRefresh").click(function () {
        LoadChart();
    });

});

function LoadChart() {
    var myChart = echarts.init(document.getElementById('main'));

    //JQ Ajax
    $.ajax({
        url: '/User/GetUserColumnReport',
        type: 'POST',
        dataType: "json",
        success: function (data) {
            myChart.setOption(data);
        }
    });
}