<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="comidas.aspx.cs" Inherits="presentacion.comidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CerrarModal() {
            $('#myModal').modal('hide');
            $('#myModalConfirmar').modal('hide');
        }

        function ModalMostrar(cTitulo, cContenido) {

            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);

        }

        function ModalConfirmar(cTitulo, cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalConfirmar').modal('show');
            $('#ModalConfirmar_title').text(cTitulo);
            $('#content_ModalConfirmar').text(cContenido);

        }

        function SetDT() {
            $('#<%=gridEmpleados.ClientID%>').dataTable({
                'bSort': false,
                'aoColumns': [
                      { sWidth: "885", bSearchable: false, bSortable: false },
                      { sWidth: "115", bSearchable: false, bSortable: false }
                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true
            });
        }

        function ValidarEnter(e) {
            k = (document.all) ? e.keyCode : e.which;
            if (k == 13) {
                return false;
            } else {
                return true;
            }
        }
        
    </script>
    <style>
        .table > tbody > tr > td,
        .table > tbody > tr > th,
        .table > tfoot > tr > td,
        .table > tfoot > tr > th,
        .table > thead > tr > td,
        .table > thead > tr > th {
            /*padding: 0px;
             line-height: 1.42857143; */
            vertical-align: middle;
            /* border-top: 1px solid #ddd; */
            align-content: center;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 2px;
        }

        a#Contenido_lnkCargar,
        a#Contenido_btnBuscar {
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .input-group-btn {
            font-size: 14px;
        }

        span h2 {
            /*margin-top: 0px;*/
        }

        .mostrar {
            /*/ display:block;*/
        }

        .ocultar {
            display: none;
        }

        /***/


        /*tam_*/
        #col_num_nom {
            WIDTH: 15%;
        }

        #col_empleado {
            WIDTH: 50%;
        }

        #col_sucursal {
            WIDTH: 25%;
        }

        #col_chek {
            WIDTH: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header"><i class="fa fa-cutlery" aria-hidden="true"></i>&nbsp;&nbsp;Registro de Uso de Comedor</h2>

    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4><i class="fa fa-calendar"></i>&nbsp;<strong>Fecha de Captura</strong></h4>
            <div class="input-group">
                <asp:TextBox ID="txtfecha" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="lnkCargar" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkCargar_Click">&nbsp;Ir&nbsp;&nbsp;<i class="fa fa-chevron-circle-right" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                </span>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <h4>&nbsp;<i class="fa fa-search" aria-hidden="true"></i>&nbsp;<strong>Buscar:</strong></h4>
            <div class="input-group">
                <asp:UpdatePanel ID="updatePanel_Buscaer" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:TextBox ID="txtsearch" CssClass="form-control" runat="server" onkeypress="return ValidarEnter(event);"
                            placeholder="Escriba el núm. de nomina, nombre de Empleado o Sucursal."></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <span class="input-group-btn">
                    <asp:LinkButton OnClick="btnBuscar_Click" runat="server" type="button" class="btn btn-success btn-block"
                        ID="btnBuscar">&nbsp;Buscar&nbsp;<i class="fa fa-search" aria-hidden="true"></i>  
                    </asp:LinkButton>
                </span>
            </div>
        </div>
    </div>

    <div class="row" runat="server" id="div_listaEmpleados" visible="true">
        <div class="col-lg-12">
            <div class="panel panel-info fresh-color">
                <div class="panel-heading" style="text-align: center">
                    <div class="row">
                        <div id="encabezado" class="table-responsive">
                            <asp:LinkButton Width="14%" CssClass="col_num_nom btn btn-default " runat="server" ID="lnk_filter_nom" OnClick="lnk_filter_nom_Click">&nbsp;<i class="fa fa-filter" aria-hidden="true"></i>&nbsp; Num. Nomina</asp:LinkButton>
                            <asp:LinkButton Width="47%" CssClass="col_empleado btn btn-default " runat="server" ID="lnk_filter_emp" OnClick="lnk_filter_emp_Click">&nbsp;<i class="fa fa-filter" aria-hidden="true"></i>&nbsp;Empleado</asp:LinkButton>
                            <asp:LinkButton Width="24%" CssClass="col_sucursal btn btn-default " runat="server" ID="lnk_filter_suc" OnClick="lnl_filter_suc_Click">&nbsp;<i class="fa fa-filter" aria-hidden="true"></i>&nbsp;SUCURSAL</asp:LinkButton>
                            <asp:LinkButton Width="11%" CssClass="col_chek btn btn-default " runat="server" ID="lnk_filter_chk" OnClick="lnk_filter_chk_Click">
                                <asp:UpdatePanel runat="server" ID="numeros" UpdateMode="Always">
                                    <ContentTemplate>
                                        &nbsp;<i class="fa fa-filter" aria-hidden="true"></i>&nbsp;
                                        Marcados &nbsp;<span class="badge">
                                            <asp:Label runat="server" ID="NUM_CHK">&nbsp;&nbsp;</asp:Label></span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel-body" style="max-height: 100%; height: 500px; overflow-y: scroll; width: 100%;">
                    <div class="table table-responsive dataTables_wrapper no-footer">

                        <asp:UpdatePanel ID="ssssds" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridEmpleados" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="gridEmpleados" EventName="RowDataBound" />
                                <asp:AsyncPostBackTrigger ControlID="lnk_filter_nom" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnk_filter_emp" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnk_filter_suc" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnk_filter_chk" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkCargar" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="gridEmpleados" runat="server" AutoGenerateColumns="False"
                                    CssClass="gv1 table table-responsive table-bordered table-condensed "
                                    DataKeyNames="NOM_SUCURSAL,img,idc_comidas,idc_empleado,num_nomina,EMPLEADO,idc_depto,DEPTO,idc_sucursal,BORRADO"
                                    OnRowCommand="gridEmpleados_RowCommand" OnRowDataBound="gridEmpleados_RowDataBound"
                                    ShowHeader="False">
                                    <Columns>
                                        <asp:BoundField DataField="idc_comidas" Visible="false" />
                                        <asp:BoundField DataField="idc_empleado" Visible="false" />
                                        <asp:ButtonField DataTextField="num_nomina" HeaderText="Num.Nomina" ItemStyle-Width="15%" FooterStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField DataTextField="EMPLEADO" HeaderText="Empleado" ItemStyle-Width="50%" />
                                        <asp:BoundField DataField="idc_depto" Visible="false" />
                                        <asp:BoundField DataField="DEPTO" Visible="false" HeaderText="Departamento" />
                                        <asp:BoundField DataField="idc_sucursal" Visible="false" />
                                        <asp:ButtonField DataTextField="NOM_SUCURSAL" HeaderText="SUCURSAL" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl='<%# Eval("img") %>' runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="chek" ID="img_check" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BORRADO" HeaderText="BORRADO" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                                <input type="hidden" runat="server" id="H_F_nom" value="ASC" />
                                <input type="hidden" runat="server" id="H_F_emp" value="ASC" />
                                <input type="hidden" runat="server" id="H_F_suc" value="ASC" />
                                <input type="hidden" runat="server" id="H_F_chk" value="ASC" />
                                <input type="hidden" runat="server" id="H_COL_FILTER" value="num_nomina ASC" />
                                <input type="hidden" runat="server" id="H_F_BUSQUEDA" value="%" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
    </div>



    <input type="hidden" id="h_contador" />
    <input type="hidden" runat="server" id="h_strScript" />
    <input type="hidden" runat="server" id="H_strPuesto" />
    <input type="hidden" runat="server" id="H_strDpto" />

    <div class="row">

        <div class="col-lg-6 col-xs-6">
            <asp:Button ID="btnGuardar" class="btn btn-success btn-block" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
        </div>
        <div class="col-lg-6 col-xs-6">
            <asp:Button ID="btnCancelar" class="btn btn-danger btn-block" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->

            <div class="modal-content" style="text-align: center">
                <div class="modal-header" style="background-color: #428bca; color: white">
                    <h4><strong id="confirmTitulo" class="modal-title"></strong></h4>
                </div>
                <div class="modal-body">

                    <div class="row" runat="server" id="confir">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <h4>
                                <label id="confirmContenido"></label>
                            </h4>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="yes" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row" id="div_detalles" style="text-align: left;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <h3 id="H3_msg" runat="server" visible="false" style="text-align: center;">NO HAY DATOS <i class="fa fa-exclamation-triangle"></i></h3>
                                        <!-- reportado-->
                                        <div runat="server" id="div_registrado">
                                            <div class="panel panel-danger fresh-color" id="panel_rep">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;
                                                      <label id="confirmContenido1">Empleados que se eliminaran del registro</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px; overflow-y: scroll; width: 100%;">
                                                    <div class="table table-responsive dataTables_wrapper no-footer">
                                                        <asp:GridView ID="gridRegistrado" runat="server" AutoGenerateColumns="False"
                                                            CssClass=" table table-responsive table-bordered  dt table table-responsive table-bordered- table-condensed dataTable"
                                                            DataKeyNames="idc_empleado,num_nomina,EMPLEADO">
                                                            <Columns>
                                                                <asp:BoundField DataField="idc_empleado" Visible="false" />
                                                                <asp:BoundField DataField="num_nomina" HeaderText="Num. Nomina" />
                                                                <asp:BoundField DataField="EMPLEADO" HeaderText="Empleado" />


                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div runat="server" id="div_registrar">
                                            <div class="panel panel-success  fresh-color">
                                                <div class="panel-heading">
                                                    <i class="fa fa-bookmark-o" aria-hidden="true"></i>&nbsp;
                                                      <label id="confirmContenido2">Empleados que se agregaran en el registro</label>
                                                </div>
                                                <div class="panel-body" style="max-height: 200px; overflow-y: scroll; width: 100%;">
                                                    <div class="table table-responsive dataTables_wrapper no-footer">
                                                        <asp:GridView ID="GridRegistrar" runat="server" AutoGenerateColumns="False"
                                                            CssClass=" table table-responsive table-bordered  dt table table-responsive table-bordered- table-condensed dataTable"
                                                            DataKeyNames="idc_empleado,num_nomina,EMPLEADO">
                                                            <Columns>
                                                                <asp:BoundField DataField="idc_empleado" Visible="false" />
                                                                <asp:BoundField DataField="num_nomina" HeaderText="Num. Nomina" />
                                                                <asp:BoundField DataField="EMPLEADO" HeaderText="Empleado" />


                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="yes" class="btn btn-success btn-block" runat="server" Text="Aceptar" OnClick="yes_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input type="button" id="No" class="btn btn-danger btn-block" onclick="CerrarModal();" value="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>
    <!---->

    <div class="modal fade modal-info" id="myModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="text-align: center">

                <div class="modal-header" style="text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="ModalConfirmar_title"><strong></strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <div style="text-align: center;">
                                <h4>
                                    <label id="content_ModalConfirmar"></label>
                                </h4>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-6 col-xs-6">
                        <asp:Button ID="btnAceptar" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="lnkCargar_Click" />
                    </div>
                    <div class="col-lg-6 col-xs-6">
                        <input id="btnCan" type="button" class="btn btn-danger btn-block" onclick="CerrarModal();" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
