$(document).ready(function () {

});

let groupBy = (keys) => (array) =>
    array.reduce((objectsByKeyValue, obj) => {
        let value = keys.map((key) => obj[key]).join("-");
        objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
        return objectsByKeyValue;
    }, {});

function Act1IndicatorChart(Data) {
    debugger;
    $('#subchart').html('');

    if (Data.length > 0) {
        //var objd = new Object();
        var TD = [], AD = [], cate = [], AGVA=[];
        var subtitle = ""; var title = "";
        var TotalTarget = 0, TotalAchieved = 0, TAPercent = 0; Apavg=0;
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].block);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            TD.push(Data[i].target);
            AD.push(Data[i].achieved);
            TotalTarget += Data[i].target;
            TotalAchieved += Data[i].achieved;
            Apavg = parseFloat((Data[i].achieved / Data.length).toFixed(0));
            AGVA.push(Apavg);
            subtitle = $("#IndicatorId").val() != "" ? $("#IndicatorId option:selected").text() : "";
            title = $("#PageType").val() != "" ? $("#PageType option:selected").text().toUpperCase().replace('_',' ') : "";
        }

        TAPercent = TotalTarget == TotalAchieved ? (TotalAchieved / TotalTarget * 100) : (TotalAchieved / TotalTarget * 100);
        TAPercent = parseFloat(TAPercent.toFixed(0))

        Highcharts.chart('subchart', {
            title: {
                text: title ,
                align: 'center'
            },
            subtitle: {
                text: subtitle,
                align: 'center'
            },
            xAxis: {
                categories: cate
            },
            yAxis: {
                title: {
                    text: 'No.of data'
                }
            },
            credits: {
                enabled: false
            },
            //tooltip: {
            //    valueSuffix: ' million liters'
            //},
            //plotOptions: {
            //    series: {
            //        borderRadius: '25%'
            //    }
            //},
            plotOptions: {
                series: {
                    borderRadius: '25%',
                  //  stacking: 'normal',
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            series: [
                {
                    type: 'column',
                    name: 'Target',
                    data: TD
                }, {
                    type: 'column',
                    name: 'Achieved',
                    data: AD
                }
                , {
                type: 'line',
                step: 'center',
                    name: 'Average',
                    data: AGVA,
                marker: {
                    lineWidth: 2,
                    lineColor: Highcharts.getOptions().colors[3],
                    fillColor: 'white'
                }
                }
                ,
                {
                    type: 'pie',
                    name: 'Total Achieved',
                    data: [{
                        name: subtitle,
                        y: TAPercent,
                        color: Highcharts.getOptions().colors[0], // 2020 color
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            format: '{point.total} %',
                            style: {
                                fontSize: '15px'
                            }
                        }
                    }],
                    center: [75, 65],
                    size: 100,
                    innerSize: '70%',
                    showInLegend: false,
                    dataLabels: {
                        enabled: false
                    }
                }]
        });

    }
}






















//function ChartDistBlockSub(Data) {
//    debugger;
//    if (Data.length > 0) {
//        //var objd = new Object();
//        var TM = [], EM = [], PM = [], cate = [];
//        var subtitle = "";
//        for (var i = 0; i < Data.length; i++) {
//            cate.push(Data[i].Block);
//            //var obj = [Data[i].ColumnName, Data[i].NoofData];
//            TM.push(Data[i].TM);
//            EM.push(Data[i].EM);
//            PM.push(Data[i].PM);
//            subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
//        }

//        // Set up the chart
//        chart = Highcharts.chart('subchart', {
//            chart: {
//                type: 'column'
//            },
//            title: {
//                text: 'Block Wises',
//                align: 'center'
//            },
//            subtitle: {
//                text: subtitle + ' Submissions Data'
//            },
//            xAxis: {
//                categories: cate
//            },
//            yAxis: {
//                min: 0,
//                title: {
//                    text: 'No.Of Submissions ' + subtitle
//                },
//                stackLabels: {
//                    enabled: true
//                }
//            },
//            credits: {
//                enabled: false
//            },
//            legend: {
//                align: 'left',
//                //x: 70,
//                //verticalAlign: 'bottom',
//                //y: 70,
//                //floating: true,
//                backgroundColor:
//                    Highcharts.defaultOptions.legend.backgroundColor || 'white',
//                borderColor: '#CCC',
//                borderWidth: 1,
//                shadow: false
//            },
//            tooltip: {
//                headerFormat: '<b>{point.x}</b><br/>',
//                pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
//            },
//            plotOptions: {
//                column: {
//                    stacking: 'normal',
//                    dataLabels: {
//                        enabled: true
//                    }
//                }
//            },
//            series: [
//                //    {
//                //    name: 'Total Member',
//                //    data: TM,
//                //    color: "#4743BA",
//                //},
//                {
//                    name: 'Entry Member',
//                    data: EM,
//                    color: "#8BC873",
//                }, {
//                    name: 'Pending Member',
//                    data: PM,
//                    color: "#CC5C5D",
//                }]
//        });
//    }
//}

