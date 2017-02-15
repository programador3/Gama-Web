﻿<%@ Page Title="Rendimiento Tareas" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="rendimiento_tareas.aspx.cs" Inherits="presentacion.rendimiento_tareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function ModalClose() {
            $('#myModal').modal('hide');

        }

        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#confirmtitle').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }

    </script>
    <style>
        .imgbtn {
            width: 40px;
            height: 20px;
            vertical-align: text-bottom;
            background-color: #1ABC9C;
            padding: 2px 6px 2px 6px;
            border-radius: 2px;
            border-color: #1ABC9C;
            
        }

            .imgbtn:hover {
                background-color: #008d4c;
                border-color: #398439;
                color: #333;
                width: 43px;
                /*height: 26px;*/
            /*position:fixed;*/
            vertical-align: text-bottom;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header">Reportes de rendimiento por Puesto</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddldeptos" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgbtn" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPuestoAsigna" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddldeptos" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtpuesto_filtro" EventName="TextChanged" />

            <asp:AsyncPostBackTrigger ControlID="lnkbuscarpuestos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" id="DEPTO" runat="server">
                    <h4><strong>Departamento</strong></h4>
                    <asp:DropDownList ID="ddldeptos" OnSelectedIndexChanged="ddldeptos_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <h4>
                        <strong>
                            <asp:Label runat="server" ID="lblPuesto_Tarea"> Puesto que realiza la Tarea</asp:Label></strong>&nbsp&nbsp
                
                        <asp:ImageButton CssClass="imgbtn" runat="server" ID="imgbtn" src="imagenes/btn/transfer_blanco2.png" OnClick="imgbtn_Click" />

                    </h4>
                    <asp:DropDownList ID="ddlPuestoAsigna" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="false">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-8 col-xs-12">
                    <h4>Escriba un Filtro</h4>
                    <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="false" OnTextChanged="lnkbuscarpuestos_Click" placeholder="Escriba el Nombre del Puesto o del Empleado"></asp:TextBox>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-12">
                    <h4>&nbsp&nbsp</h4>
                    <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de inicio</strong></h4>
                    <asp:TextBox ID="txtfechainicio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Seleccione una fecha de fin</strong></h4>
                    <asp:TextBox ID="txtfechafin" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:LinkButton ID="lnkir" OnClick="lnkir_Click" CssClass="btn btn-success btn-block" runat="server">Generar Graficas <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="btn btn-info btn-block" runat="server">Ver Detalles Desglosados <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
                </div>
                <div class="col-lg-12 col-xs-12">
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger btn-block" runat="server" OnClick="LinkButton2_Click">Reporte para Junta Direccion <i class="fa fa-repeat" aria-hidden="true"></i></asp:LinkButton>
                </div>
                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkButton3" CssClass=" btn btn-success btn-block" runat="server" OnClientClick="window.open('grafica_reclutamiento.aspx');">Grafica de Rendimiento Reclutamiento</asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->

            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="confirmContenido"></label>
                            </h4>
                            <div class="col-xs-2 ">&nbsp;</div>
                            <div class="col-xs-6 ">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="pRealizo" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="pAplico" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="pRealizoAplico" EventName="CheckedChanged" />

                                        <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div style="text-align: left">
                                            <asp:CheckBox AutoPostBack="true" runat="server" ID="pRealizo" CssClass="radio3 radio-info radio-check" Text="Puesto que realiza la Tarea." OnCheckedChanged="pRealizo_CheckedChanged" />
                                            <asp:CheckBox AutoPostBack="true" runat="server" ID="pAplico" CssClass="radio3 radio-info radio-check" Text="Puesto que aplico la Tarea." OnCheckedChanged="pAplico_CheckedChanged" />
                                            <asp:CheckBox AutoPostBack="true" runat="server" ID="pRealizoAplico" CssClass="radio3 radio-info radio-check" Text="Puesto que realiza y aplico la Tarea." OnCheckedChanged="pRealizoAplico_CheckedChanged" />
                                        </div>
                                        <input type="hidden" runat="server" id="h_casoFiltro"  />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <asp:Button ID="btnAceptar" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="Yes_Click" />
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <input type="button" id="No" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <input type="hidden" runat="server" id="h_Caso" value="" />
    


</asp:Content>
