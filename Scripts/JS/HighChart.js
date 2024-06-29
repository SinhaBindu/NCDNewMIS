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
        var subtitle = "";
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
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
                text: 'Elevated blood pressure (Systolic > 140 mm of Hg and/or Diastolic > 90 mm of Hg) or taking medicine to control blood pressure (%)',
            },
            credits: {
                enabled: false
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
                    text: 'No.of Members'
                }
            },
            //tooltip: {
            //    valueSuffix: ' m'
            //},
            series: [{
                name: 'Members',
                //colorByPoint: true,
                data: DataList,
                color: '#2caffe',
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
        var subtitle = "";
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
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
                text: 'Blood sugar level - high or very high (>140 mg/dl) or taking medicine to control blood sugar level (%)'
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
                    text: 'No.of Members'
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
                color: '#F0673C',
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
        var subtitle = "";
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].Block);
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            DataList.push(obj);
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
                text: 'Hypertension & Blood sugar level - high or very high (>140 mg/dl) or taking medicine to control blood sugar level (%)'
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
                    text: 'No.of Members'
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
                color: '#274CC1',
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

