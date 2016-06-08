<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="cursos.aspx.cs" Inherits="presentacion.cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/highlight.css" rel="stylesheet" />
    <link href="css/bootstrap-switch.css" rel="stylesheet" />
    <link href="http://getbootstrap.com/assets/css/docs.min.css" rel="stylesheet" />

    <script src="js/bootbox.min.js"></script>
    <style>
        .p-produccion {
            color: #FFF;
            background-color: #449D44;
            border-color: #398439;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .p-borrador {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        A:hover {
            text-decoration: none;
            color: #FFF;
        }

        .produccion {
            color: #FFF;
            background-color: #449D44;
            border-color: #398439;
            padding: 6px 12px;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            -moz-user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            display: block;
            width: 100%;
        }

        .borrador {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
            padding: 6px 12px;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            -moz-user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            display: block;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function Return(cTitulo, cContenido) {
            $('#myModal').modal('show');
            $('#confirmTitulo').text(cTitulo);
            $('#confirmContenido').text(cContenido);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <asp:HiddenField ID="id_res" runat="server" />
        <div class="container-fluid">

            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Cursos<span> <small>
                        <label id="lblTitle"></label>
                    </small></span>
                    </h1>
                </div>
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3">
                    <%-- <a id="btnNew" class="borrador" href="javascript:__doPostBack('btnNew','')">Nuevo Curso <span class='glyphicon glyphicon-new-window'></span></a>--%>
                    <asp:Button ID="btnnuevocurso" runat="server" Text="Nuevo curso" OnClick="btnnuevocurso_Click" />
                    <br />
                </div>
                <div class="col-xs-12 col-sm-12 col-md-2 col-lg-6">
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-3">
                    <label>
                        Curso de<span>
                            <asp:CheckBox ID="cbxTipo" AutoPostBack="false" runat="server" class="checkbox" BorderStyle="None" /></span></label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="table-responsive">
                        <div class="panel panel-primary ">
                            <div id="panel-head1" class="p-borrador" style="text-align: center">
                                <h3 class="panel-title">Cursos <i class="fa fa-list"></i></h3>
                            </div>
                            <div class="panel panel-body">
                                <div class="table table-responsive">
                                    <!-- GRIDVIEW DE CURSOS -->
                                    <asp:GridView ID="grid_cursos" runat="server" CssClass="gvv table table-bordered table-hover grid sortable {disableSortCols: [4]}" AutoGenerateColumns="False" OnRowDataBound="grid_cursos_RowDataBound" DataKeyNames="idc_curso_p,idc_curso_borr" OnRowCommand="grid_cursos_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Idc_curso_p" DataField="idc_curso_p" Visible="False" />
                                            <asp:BoundField DataField="idc_curso_borr" HeaderText="Idc_curso_borr" Visible="False" />

                                            <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtneditcurso" runat="server" CausesValidation="false" CommandName="editcurso" ImageUrl="~/imagenes/btn/icon_editar.png" Text="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Borrar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndeletecurso" runat="server" CausesValidation="false" CommandName="deletecurso" ImageUrl="~/imagenes/btn/icon_delete.png" Text="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Curso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcurso" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="usuario" HeaderText="Autor del Borrador" />
                                            <asp:TemplateField HeaderText="Nuevo Borrador" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgnuevoborr" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo curso">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltipo_curso" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Produccion">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgprod" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Borrador">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgborr" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pendiente">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgpendiente" runat="server" ImageUrl="~/imagenes/btn/inchecked.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aprobacion">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnsolicitar" runat="server" ImageUrl="~/imagenes/btn/btn_solicitar_equipos.png" CommandName="solicitar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desbloquear">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndesbloquear" runat="server" ImageUrl="~/imagenes/btn/icon_autorizar.png" CommandName="desbloquear" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- /.row -->
        </div>
        <!-- /.CONFIRMA -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Yes" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardarSinLigar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="No" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSinLigar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtNombreCurso" EventName="TextChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-content" style="text-align: center">
                            <div class="modal-header" style="background-color: #428bca; color: white">
                                <h4><strong id="confirmTitulo" class="modal-title">Mensaje del Sistema</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <h4>
                                            <label id="confirmContenido"></label>
                                        </h4>
                                        <asp:Panel ID="PanelNuevoBorrador" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:TextBox ID="txtNombreCurso" placeholder="Escriba el Nombre del Perfil" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtNombreCurso_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblerror" CssClass="label label-danger" runat="server" Text="ESCRIBA EL NOMBRE DEL CURSO" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                        <div class="form-group">
                                            <asp:Button ID="Yes" class="btn btn-success btn-block" runat="server" Text="Si" OnClick="btnConfirm_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                                        <div class="form-group">
                                            <asp:Button Visible="false" ID="btnGuardarSinLigar" runat="server" Text="Guardar" class="btn btn-warning btn-block" OnClick="btnGuardarSinLigar_Click" />
                                            <asp:Button Visible="false" ID="btnSinLigar" class="btn btn-warning btn-block" runat="server" Text="Si, sin ligar a perfil" OnClick="btnSinLigar_Click" />
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                        <div class="form-group">
                                            <asp:Button ID="No" class="btn btn-danger btn-block" runat="server" Text="No" OnClick="No_Click" />
                                        </div>
                                    </div>
                                </div>

                                <!--campos ocultos -->
                                <asp:HiddenField ID="modal_oc_idc_curso" runat="server" />
                                <asp:HiddenField ID="modal_oc_accion" runat="server" />
                                <asp:HiddenField ID="modal_oc_pendiente" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal content-->
            </div>
        </div>
    </div>
    <script src="js/bootstrap-switch-original.js"></script>
    <script>
        $(function (argument) {

            $("[id='<%= cbxTipo.ClientID%>']").bootstrapSwitch({
                //state: true,
                //size: null,
                //animate: true,
                //disabled: false,
                //readonly: false,
                //indeterminate: false,
                //inverse: false,
                //radioAllOff: false,
                onColor: "primary",
                offColor: "success",
                onText: "Borrador",
                offText: "Produccion",
                labelText: "< >",
                handleWidth: "auto",
                labelWidth: "auto",
                baseClass: "bootstrap-switch",
                wrapperClass: "wrapper",
            });

            //
            if ($("[id='<%= cbxTipo.ClientID%>']").is(':checked')) {
                $("#lblTitle").text(" de Borrador");
                $('#btnNew').addClass('borrador');
                $('#panel-head1').addClass('p-borrador');
                $('#btnNew').removeClass('produccion');
                $('#panel-head1').removeClass('p-produccion');

            } else {
                $("#lblTitle").text(" de Produccion");
                $('#btnNew').addClass('produccion');
                $('#btnNew').removeClass('borrador');
                $('#panel-head1').addClass('p-produccion');
                $('#panel-head1').removeClass('p-borrador');

            }
        })

        $("[id='<%= cbxTipo.ClientID%>']").on('switchChange.bootstrapSwitch', function (event, state) {
            if ($(this).is(':checked')) {
                window.location = 'cursos.aspx?sborrador=1';
            }
            else {
                window.location = 'cursos.aspx?sborrador=0';
            }
        });
    </script>
</asp:Content>