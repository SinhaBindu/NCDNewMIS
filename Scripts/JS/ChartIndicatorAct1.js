﻿$(document).ready(function () {

});

let groupBy = (keys) => (array) =>
    array.reduce((objectsByKeyValue, obj) => {
        let value = keys.map((key) => obj[key]).join("-");
        objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
        return objectsByKeyValue;
    }, {});

function Act1IndicatorChart(Data) {
   // debugger;
    $('#subchart').html('');

    if (Data.length > 0) {
        //var objd = new Object();
        var TD = [], AD = [], cate = [], AGVA = [];
        var subtitle = ""; var title = "";
        var TotalTarget = 0, TotalAchieved = 0, TAPercent = 0; Apavg = 0;
        var targetzero = Data.filter(x => x.target == 0);
        var achievedzero = Data.filter(x => x.achieved == 0);
        var dataser = [];
        Data = Data.sort();
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].block);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            TD.push(Data[i].target);
            AD.push(Data[i].achieved);
            if (Data[i].block.toUpperCase() != "TOTAL") {
                TotalTarget += Data[i].target;
                TotalAchieved += Data[i].achieved;
            }

            Apavg = parseFloat((Data[i].achieved / Data.length).toFixed(0));
            AGVA.push(Apavg);
            subtitle = $("#IndicatorId").val() != "" ? $("#IndicatorId option:selected").text() : "";
            title = $("#PageType").val() != "" ? $("#PageType option:selected").text().toUpperCase().replace('_', ' ') : "";

        }
        if (targetzero.length <= 0) {
            dataser.push({ type: 'column', name: 'Target', data: TD, 'color': '#bc6ac9' });
        }
        if (achievedzero <= 0) {
            dataser.push({ type: 'column', name: 'Achieved', data: AD, 'color': '#ff7a01' });//fbc21d//f2c02c
        }

        TAPercent = TotalTarget == TotalAchieved ? (TotalAchieved / TotalTarget * 100) : (TotalAchieved / TotalTarget * 100);
        TAPercent = parseFloat(TAPercent.toFixed(0))

        var tittotal = TotalTarget != 0 ? "Target - " + TotalTarget + " To Achieved - " + TotalAchieved : TotalAchieved != 0 ? TotalAchieved : '';
        debugger;
        if (TAPercent == 0 || TAPercent == Infinity || TAPercent == undefined || TAPercent == "" || TAPercent == "NaN" || isNaN(TAPercent)) {
            $('#div-bar').removeClass("xl:w-2/3");
            $('#div-pie').hide();
            $('#div-bar').addClass('xl:w-1/1');
        }
        else {
            $('#div-pie').show();
            $('#div-bar').addClass('xl:w-2/3');
        }
        Highcharts.chart('subchart', {

            chart: {
                //type: 'lollipop',
                //type: 'columnpyramid',
                borderWidth: 1,
                height: 300,
            },
            title: {
                text: title,
                align: 'center'
            },
            subtitle: {
                text: subtitle + " : " + "<b style='color:#000;font-size:12px;'>" + tittotal + "<b/>",
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
            series: dataser

            //[
            //{
            //    type: 'column',
            //    name: 'Target',
            //    data: TD
            //}, {
            //    type: 'column',
            //    name: 'Achieved',
            //    data: AD
            //},
            //{
            //    type: 'pie',
            //    name: 'Total Achieved',
            //    data: [{
            //        name: subtitle,
            //        y: TAPercent,
            //        color: Highcharts.getOptions().colors[0], // 2020 color
            //        dataLabels: {
            //            enabled: true,
            //            distance: -50,
            //            format: '{point.total} %',
            //            style: {
            //                fontSize: '15px'
            //            }
            //        }
            //    }],
            //    center: [75, 65],
            //    size: 100,
            //    innerSize: '70%',
            //    showInLegend: false,
            //    dataLabels: {
            //        enabled: false
            //    }
            //}]
        });

    }
}

function ChartAchivepie(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var TD = [], AD = [], cate = [], AGVA = [], dataserpie=[];
        var subtitle = ""; var title = "";
        var TotalTarget = 0, TotalAchieved = 0, TAPercent = 0; Apavg = 0;
        var targetzero = Data.filter(x => x.target == 0);
        var achievedzero = Data.filter(x => x.achieved == 0);
        var dataser = [];
        Data = Data.sort();
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].block);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            TD.push(Data[i].target);
            AD.push(Data[i].achieved);
            if (Data[i].block.toUpperCase() != "TOTAL") {
                TotalTarget += Data[i].target;
                TotalAchieved += Data[i].achieved;
            }

            Apavg = parseFloat((Data[i].achieved / Data.length).toFixed(0));
            AGVA.push(Apavg);
            subtitle = $("#IndicatorId").val() != "" ? $("#IndicatorId option:selected").text() : "";
            title = $("#PageType").val() != "" ? $("#PageType option:selected").text().toUpperCase().replace('_', ' ') : "";

        }
        if (targetzero.length <= 0) {
            dataser.push({ type: 'column', name: 'Target', data: TD, 'color': '#bc6ac9' });
        }
        if (achievedzero <= 0) {
            dataser.push({ type: 'column', name: 'Achieved', data: AD, 'color': '#ff7a01' });//fbc21d//f2c02c
        }

        TAPercent = TotalTarget == TotalAchieved ? (TotalAchieved / TotalTarget * 100) : (TotalAchieved / TotalTarget * 100);
        TAPercent = parseFloat(TAPercent.toFixed(0))

        var tittotal = TotalTarget != 0 ? "Target - " + TotalTarget + " To Achieved - " + TotalAchieved : TotalAchieved != 0 ? TotalAchieved : '';

        var achprcent = (TotalAchieved / Data.length);
        var achprcentcolor = TAPercent == 100.00 ? '#21b731' :'#ff7a00';
        dataserpie.push({ name: 'Achieved ' + TotalAchieved, y: TAPercent, color: achprcentcolor });
        
        // Create the chart
        Highcharts.chart('subchartpie', {
            chart: {
                type: 'pie',
                borderWidth: 1,
                height: 300,
            },
            title: {
                text: title,
                align: 'center'
            },
            subtitle: {
                text: "Achieved", //: " + "<b style='color:#000;font-size:12px;'>" +  + "<b/>",
                align: 'center'
            },

            accessibility: {
                announceNewData: {
                    enabled: true
                },
                point: {
                    valueSuffix: '%'
                }
            },

            plotOptions: {
                series: {
                    borderRadius: 5,
                    dataLabels: [{
                        enabled: true,
                        distance: 15,
                        format: '{point.name}'
                    }, {
                        enabled: true,
                        distance: '-30%',
                        filter: {
                            property: 'percentage',
                            operator: '>',
                            value: 5
                        },
                        format: '{point.y:.1f}%',
                        style: {
                            fontSize: '0.9em',
                            textOutline: 'none'
                        }
                    }]
                }
            },
            credits: {
                enabled: false
            },
            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span>: ' +
                    '<b>{point.y:.2f}%</b><br/>'
            },

            series: [
                {
                    name: 'Hypertension',
                    colorByPoint: true,
                    data: dataserpie
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

