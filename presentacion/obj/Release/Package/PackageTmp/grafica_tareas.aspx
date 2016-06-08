﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="grafica_tareas.aspx.cs" Inherits="presentacion.grafica_tareas" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Graph/Chart.bundle.js"></script>
    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script type="text/javascript">
    </script>
    <style>
        canvas {
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
        }
    </style>
    <script>
        var randomScalingFactor = function () {
            return Math.round(Math.random() * 100);
        };
        var randomColorFactor = function () {
            return Math.round(Math.random() * 255);
        };
        var randomColor = function (opacity) {
            return 'rgba(' + randomColorFactor() + ',' + randomColorFactor() + ',' + randomColorFactor() + ',' + (opacity || '.3') + ')';
        };

        var config = {
            data: {
                datasets: [{
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                    ],
                    backgroundColor: [
                        "#F7464A",
                        "#46BFBD",
                        "#FDB45C",
                        "#949FB1",
                        "#4D5360",
                    ],
                    label: 'My dataset' // for legend
                }],
                labels: [
                    "Red",
                    "Green",
                    "Yellow",
                    "Grey",
                    "Dark Grey"
                ]
            },
            options: {
                responsive: true,
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Chart.js Polar Area Chart'
                },
                scale: {
                    ticks: {
                        beginAtZero: true
                    },
                    reverse: false
                },
                animateRotate: false
            }
        };

        window.onload = function () {
            var ctx = document.getElementById("chart-area");
            window.myPolarArea = Chart.PolarArea(ctx, config);
        };

        $('#randomizeData').click(function () {
            $.each(config.data.datasets, function (i, piece) {
                $.each(piece.data, function (j, value) {
                    config.data.datasets[i].data[j] = randomScalingFactor();
                    config.data.datasets[i].backgroundColor[j] = randomColor();
                });
            });
            window.myPolarArea.update();
        });

        $('#addData').click(function () {
            if (config.data.datasets.length > 0) {
                config.data.labels.push('dataset #' + config.data.labels.length);

                $.each(config.data.datasets, function (i, dataset) {
                    dataset.backgroundColor.push(randomColor());
                    dataset.data.push(randomScalingFactor());
                });

                window.myPolarArea.update();
            }
        });

        $('#removeData').click(function () {
            config.data.labels.pop(); // remove the label first

            $.each(config.data.datasets, function (i, dataset) {
                dataset.backgroundColor.pop();
                dataset.data.pop();
            });

            window.myPolarArea.update();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">
        <div id="canvas-holder" style="width: 75%">
            <canvas id="chart-area"></canvas>
        </div>
        <button id="randomizeData">Randomize Data</button>
        <button id="addData">Add Data</button>
        <button id="removeData">Remove Data</button>
    </div>
</asp:Content>