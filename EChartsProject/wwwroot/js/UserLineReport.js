// 基于准备好的dom，初始化echarts实例
$(function () {


    LoadChart();

    $("#btnRefresh").click(function () {
        //$.messager.progress({
        //    msg: '正在提交...',
        //    text: '',
        //    width: 170,
        //    height:120
        //});
        onloading('加载中...');

        LoadChart();
    });

});

function LoadChart() {
    var myChart = echarts.init(document.getElementById('main'));

    //JQ Ajax
    $.ajax({
        url: '/User/GetUserLineReport',
        type: 'POST',
        dataType: "json",
        success: function (data) {
            myChart.setOption(data, true);
            removeload();
        }
    });
}