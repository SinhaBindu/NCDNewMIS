$(document).ready(function () {

});

let groupBy = (keys) => (array) =>
    array.reduce((objectsByKeyValue, obj) => {
        let value = keys.map((key) => obj[key]).join("-");
        objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
        return objectsByKeyValue;
    }, {});

function ChartHSub(Data) {
    //debugger;
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var ATM = 0;
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            ATM += Data[i].NoofData;
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            // subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }

        // Set up the chart
        chart = Highcharts.chart('subHYTchart', {
            chart: {
                type: 'columnpyramid',
                borderWidth: 1,
            },
            title: {
                text: 'Hypertension',
                align: 'center'
            },
            subtitle: {
                text: 'Total Rural Households / No.of Members : ' + TH + " / " + TM,
            },
            credits: {
                enabled: false,
                //text: 'Elevated blood pressure (Systolic > 140 mm of Hg and/or Diastolic > 90 mm of Hg)<br/ > or taking medicine to control blood pressure (%)',
                position: {
                    align: 'center',
                    y: -5 // position of credits
                },
                style: {
                    textwrap: 'wrap',
                },
            },
            // colors: ['#C79D6D', '#B5927B', '#CE9B84', '#B7A58C', '#C7A58C'],
            colors: ['#2caffe'],
            xAxis: {
                //crosshair: true,
                //labels: {
                //    style: {
                //        fontSize: '14px'
                //    }
                //},
                type: 'category'
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'No.of Members :  <b>' + ATM + '</ b>',
                }
            },
            //tooltip: {
            //    valueSuffix: ' m'
            //},
            series: [{
                name: 'Members',
                //colorByPoint: true,
                data: DataList,
                color: '#bc6ac9',
                dataLabels: {
                    enabled: true,
                    //rotation: -50,
                    color: '#000000',
                    align: 'right',
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '14px',
                        fontFamily: 'helvetica, arial, sans-serif',
                        textShadow: false,
                        fontWeight: 'normal'

                    }
                },
                showInLegend: false
            }]
        });
    }
}

function ChartBsub(Data) {
    // Data retrieved from https://worldpopulationreview.com/countries
    // debugger;
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var ATM = 0;
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            ATM += Data[i].NoofData;
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            //DataList.push({ name: Data[i].ColumnName, y: Data[i].NoofData });
            //subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }
        Highcharts.chart('subRBSchart', {

            chart: {
                //type: 'lollipop',
                type: 'columnpyramid',
                borderWidth: 1,
            },

            //accessibility: {
            //    point: {
            //        valueDescriptionFormat: '{index}. {xDescription}, {point.y}.'
            //    }
            //},

            legend: {
                enabled: false
            },
            title: {
                text: 'Blood sugar level - high or very high '
            },
            subtitle: {
                text: 'Total Rural Households / No.of Members : ' + TH + " / " + TM,
                // text: 'Blood sugar level - high or very high (>140 mg/dl) or taking medicine to control blood sugar level (%)'
            },
            credits: {
                enabled: false
            },
            //tooltip: {
            //    shared: true
            //},
            //tooltip: {
            //    valueSuffix: ' m'
            //},

            xAxis: {
                type: 'category'
            },

            yAxis: {
                title: {
                    text: 'No.of Members <b>' + ATM + '</ b>',
                }
            },
            //plotOptions: {
            //    series: {
            //        dataLabels: {
            //            allowOverlap: true,
            //            crop: false,
            //            enabled: true,
            //            //inside: true,
            //            borderRadius: 5,
            //            backgroundColor: 'rgba(252, 255, 197, 0.7)',
            //            borderWidth: 1,
            //            borderColor: '#AAA',
            //           // marginLeft: -90,
            //           // paddingRight: '40px',
            //            y: -10,
            //            shape: 'callout',
            //            pointPadding: 0
            //            //formatter: function () {
            //            //    return this.y;
            //            //}
            //        }
            //    }
            //},
            //plotOptions: {
            //    connectorWidth: 2,
            //    series: {
            //        dataLabels: {
            //        enabled: true,
            //        //rotation: -90,
            //        color: '#FFFFFF',
            //        align: 'center',
            //       // y: 10, // 10 pixels down from the top
            //        style: {
            //            fontSize: '10px',
            //            fontFamily: 'helvetica, arial, sans-serif',
            //            textShadow: false,
            //            fontWeight: 'normal'

            //        }
            //    },
            //    }
            //},

            series: [{
                name: 'Members',
                data: DataList,
                color: '#FE9900',
                dataLabels: {
                    enabled: true,
                    //rotation: -50,
                    color: '#000000',
                    align: 'right',
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '14px',
                        fontFamily: 'helvetica, arial, sans-serif',
                        //textShadow: false,
                        fontWeight: 'normal'

                    }
                },
                showInLegend: false

            }]

        });
    }
}

function ChartHYTToBsub(Data) {
    // Data retrieved from https://worldpopulationreview.com/countries
    // debugger;
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var ATM = 0;
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            ATM += Data[i].NoofData;
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            //DataList.push({ name: Data[i].ColumnName, y: Data[i].NoofData });
            //subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }
        Highcharts.chart('subHYTToRBSchart', {

            chart: {
                //type: 'lollipop',
                type: 'columnpyramid',
                borderWidth: 1,
            },

            //accessibility: {
            //    point: {
            //        valueDescriptionFormat: '{index}. {xDescription}, {point.y}.'
            //    }
            //},

            legend: {
                enabled: false
            },
            title: {
                text: 'Hypertension & Blood sugar level - high or very high '
            },
            subtitle: {
                text: 'Total Rural Households / No.of Members : ' + TH + " / " + TM,
                //text: 'Hypertension & Blood sugar level - high or very high (>140 mg/dl) or taking medicine to control blood sugar level (%)'
                //text: 'Hypertension & Blood sugar level - high or very high (>140 mg/dl) or taking medicine to control blood sugar level (%)'
            },
            credits: {
                enabled: false
            },
            //tooltip: {
            //    shared: true
            //},
            //tooltip: {
            //    valueSuffix: ' m'
            //},

            xAxis: {
                type: 'category'
            },

            yAxis: {
                title: {
                    text: 'No.of Members : <b>' + ATM +'</ b>',
                }
            },
            //plotOptions: {
            //    series: {
            //        dataLabels: {
            //            allowOverlap: true,
            //            crop: false,
            //            enabled: true,
            //            //inside: true,
            //            borderRadius: 5,
            //            backgroundColor: 'rgba(252, 255, 197, 0.7)',
            //            borderWidth: 1,
            //            borderColor: '#AAA',
            //           // marginLeft: -90,
            //           // paddingRight: '40px',
            //            y: -10,
            //            shape: 'callout',
            //            pointPadding: 0
            //            //formatter: function () {
            //            //    return this.y;
            //            //}
            //        }
            //    }
            //},
            //plotOptions: {
            //    connectorWidth: 2,
            //    series: {
            //        dataLabels: {
            //        enabled: true,
            //        //rotation: -90,
            //        color: '#FFFFFF',
            //        align: 'center',
            //       // y: 10, // 10 pixels down from the top
            //        style: {
            //            fontSize: '10px',
            //            fontFamily: 'helvetica, arial, sans-serif',
            //            textShadow: false,
            //            fontWeight: 'normal'

            //        }
            //    },
            //    }
            //},

            series: [{
                name: 'Members',
                data: DataList,
                color: '#E94748',
                dataLabels: {
                    enabled: true,
                    //rotation: -50,
                    color: '#000000',
                    align: 'right',
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '14px',
                        fontFamily: 'helvetica, arial, sans-serif',
                        //textShadow: false,
                        fontWeight: 'normal'

                    }
                },
                showInLegend: false

            }]

        });
    }
}

function ChartStateHypertension(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var PHYT = 0; var dataser = [];
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            PHYT += Data[i].NoofData;
            // subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }
        var pmemHYT = (TM / PHYT);
        dataser.push({ name: 'No.of Members ' + PHYT, y: pmemHYT, color: '#bc6ac9' });
        // Create the chart
        Highcharts.chart('StatesubHYTchart', {
            chart: {
                type: 'pie',
                borderWidth: 1,
            },
            title: {
                text: 'Hypertension',
                align: 'center'
            },
            subtitle: {
                text: 'Total No.of Members / No.of Members in (Hypertension) : ' + TM + " / " + PHYT,
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
                    data: dataser
                }]
        });

    }
}

function ChartStateBsub(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var PHYT = 0; var dataser = [];
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            PHYT += Data[i].NoofData;
            // subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }
        var pmemHYT = (TM / PHYT);
        dataser.push({ name: 'No.of Members ' + PHYT, y: pmemHYT, color: '#FE9900' });
        // Create the chart
        Highcharts.chart('StatesubRBSchart', {
            chart: {
                type: 'pie',
                borderWidth: 1,
            },
            title: {
                text: 'Blood sugar level',
                align: 'center'
            },
            subtitle: {
                text: 'Total No.of Members / No.of Members in (Blood sugar level) : ' + TM + " / " + PHYT,
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
                    name: 'Blood sugar level',
                    colorByPoint: true,
                    data: dataser
                }]
        });

    }
}

function ChartStateHYTToBsub(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var DataList = [], cate = [];
        var subtitle = ""; var TH = 0; var TM = 0; var PHYT = 0; var dataser = [];
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
            TH += Data[i].TotalHouseHold; TM += Data[i].TotalMember;
            PHYT += Data[i].NoofData;
            // subtitle = $("#SType").val() != "" ? $("#SType option:selected").text() : "Home and Camp";
        }
        var pmemHYT = (TM / PHYT);
        dataser.push({ name: 'No.of Members ' + PHYT, y: pmemHYT, color: '#E94748' });
        // Create the chart
        Highcharts.chart('StateHYTToRBSchart', {
            chart: {
                type: 'pie',
                borderWidth: 1,
            },
            title: {
                text: 'Hypertension & Blood sugar level - high or very high ',
                align: 'center'
            },
            subtitle: {
                text: 'Total No.of Members / No.of Members in (Hypertension & Blood sugar level) : ' + TM + " / " + PHYT,
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
                    name: 'Hypertension & Blood sugar level',
                    colorByPoint: true,
                    data: dataser
                }]
        });

    }
}