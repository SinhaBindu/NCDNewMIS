﻿$(document).ready(function () {

});

let groupBy = (keys) => (array) =>
    array.reduce((objectsByKeyValue, obj) => {
        let value = keys.map((key) => obj[key]).join("-");
        objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
        return objectsByKeyValue;
    }, {});

function ChartDistBlockSub(Data) {
    debugger;
    if (Data.length > 0) {
        //var objd = new Object();
        var TM = [], EM = [], PM = [], cate = [];
        var subtitle = "";
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            TM.push(Data[i].TM);
            EM.push(Data[i].EM);
            PM.push(Data[i].PM);
            subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }

        // Set up the chart
        chart = Highcharts.chart('subchart', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Block Wise',
                align: 'center'
            },
            subtitle: {
                text: subtitle + ' Submission Data'
            },
            xAxis: {
                categories: cate
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'No.Of Submissions ' + subtitle
                },
                stackLabels: {
                    enabled: true
                }
            },
            credits: {
                enabled: false
            },
            legend: {
                align: 'left',
                //x: 70,
                //verticalAlign: 'bottom',
                //y: 70,
                //floating: true,
                backgroundColor:
                    Highcharts.defaultOptions.legend.backgroundColor || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            tooltip: {
                headerFormat: '<b>{point.x}</b><br/>',
                pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
            },
            plotOptions: {
                column: {
                    stacking: 'normal',
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            series: [
                //    {
                //    name: 'Total Member',
                //    data: TM,
                //    color: "#4743BA",
                //}, 
                {
                    name: 'Entry Done',
                    data: EM,
                    color: "#bc6ac9",
                }, {
                    name: 'Pending',
                    data: PM,
                    color: "#e4b62c",
                }]
        });
    }
}

