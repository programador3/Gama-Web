<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="comisiones_detalles.aspx.cs" Inherits="presentacion.comisiones_Detalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ModalClose() {
            $('#myModalEditar').modal('hide');
            $("#myModal_Esp").modal("hide");
        }

        function ModalEditar(cContenido) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModalEditar').modal('show');
            $('#ModalEditar_title').text(cContenido);
            $('#content_ModalEditar').text('');
        }

        //$(document).ready(function () {
        //    $(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
        //        "lengthMenu": [[15, 25, -1], [15, 25, "Todos"]] //value:item pair
        //    });
        //});

    </script>
    <style>
        .form-control {
            float: right;
            text-align: right;
            font-weight: bold;
            color: blue;
        }

            .form-control + .form-control-feedback {
                left: 0;
            }

        .input-group {
            display: block;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h2 class="page-header">Detalles de Comisiones</h2>
    <div class="container">
        <div class="row">
            <div>
                <div id="div_detalles">
                    <table class="table table-bordered">
                        <tr>
                            <th>&nbsp;</th>
                            <th><i class="fa fa-shopping-cart" aria-hidden="true"></i>&nbsp;Venta</th>
                            <th><strong>%</strong> &nbsp;Comision</th>
                            <%--<th class="Ocultar" >&nbsp;Aportacion</th>--%>
                        </tr>
                        <tr>
                            <th>Compartida:&nbsp;</th>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="txtventa_c" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>
                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="txtcomision_c" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <strong class="form-control-feedback">%</strong>
                                </div>
                            </td>
                            <%--<td class="Ocultar">
                                                                    <asp:TextBox ID="txtaportacion_c" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                                </td>--%>
                        </tr>

                        <tr>
                            <th>Directa:&nbsp;</th>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="txtventa_d" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                                </div>

                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="txtcomision_d" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <strong class="form-control-feedback">%</strong>
                                </div>
                            </td>
                            <%--<td class="Ocultar">
                                                                    <asp:TextBox ID="txtaportacion_d" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                                </td>--%>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="row">
            <div>
                <div class="table table-responsive">
                    <asp:GridView ID="gridv2" CssClass="gvv table table-responsive table-bordered" runat="server"
                        DataKeyNames="codfac,Venta,apo,tipod,idc_factura,tot_ven,tot_apo,pedscg" AutoGenerateColumns="false"
                        SelectedRowStyle-BackColor="LightCyan" SelectedRowStyle-Font-Bold="true"
                        OnRowCommand="gridv2_RowCommand">
                        <Columns>

                            <asp:ButtonField ButtonType="Link" DataTextField="codfac" HeaderText="Documento" CommandName="Ver">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>

                            <%-- <asp:BoundField DataField="codfac" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:ButtonField ButtonType="Link" DataTextField="venta" HeaderText="Venta" CommandName="Venta" DataTextFormatString="{0:N2}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
                            <%--<asp:BoundField DataField="Venta" HeaderText="Venta" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:ButtonField ButtonType="Link" DataTextField="apo" HeaderText="Aportacion" CommandName="Aportacion" DataTextFormatString="{0:N2}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
                            <%--<asp:BoundField DataField="apo" HeaderText="Aportacion" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />--%>

                            <asp:BoundField DataField="tipod" HeaderText="tipo" Visible="false" />
                            <asp:BoundField DataField="idc_factura" HeaderText="fac" Visible="false" />
                            <asp:BoundField DataField="tot_ven" HeaderText="tot_ven" Visible="false" />
                            <asp:BoundField DataField="tot_apo" HeaderText="tot_apo" Visible="false" DataFormatString="{0:N2}" />
                            <asp:TemplateField HeaderText="Dir">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkdirecta" Checked='<%# DataBinder.Eval(Container, "DataItem.Directa") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" DataTextField="pedscg" HeaderText="Pedscg" CommandName="Pedscg">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
                            <%--<asp:BoundField DataField="pedscg" HeaderText="Pedscg" ItemStyle-HorizontalAlign="Center" />--%>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="input-group">
                <asp:TextBox ID="txtapo" onfocus="$(this).select();" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                <span class="glyphicon glyphicon-usd form-control-feedback"></span>

            </div>
        </div>
        <div class="row">

            <asp:Button ID="btnCerrar" class="btn btn-success btn-block" runat="server" Text="Cerrar" OnClick="cerrar_click" />

        </div>

    </div>

    <div class="modal fade modal-info" id="myModalEditar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg ">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="gridv2" EventName="RowCommand" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header" style="text-align: center;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 id="ModalEditar_title"><strong>Detalle de Aportaciones</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                    <div style="text-align: center;">
                                        <h4>
                                            <label id="content_ModalEditar"></label>
                                        </h4>
                                    </div>


                                    <!-- -->
                                    <div runat="server" id="div_fac" visible="true">
                                        <div class="table table-responsive">
                                            <div style="text-align: center;">
                                                <h4><i class="fa fa-file-text-o" aria-hidden="true"></i>&nbsp;
                                                <asp:Label runat="server" ID="lbldoc" Style="background-color: White; color: Blue; font-weight: bold; width: 100%;"></asp:Label>
                                                </h4>
                                            </div>
                                            <asp:GridView ID="Grid_fac" CssClass="table table-responsive table-bordered" runat="server" AutoGenerateColumns="False" BackColor="White" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="idc_factura" HeaderText="Documento" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="apo_factura" HeaderText="apo_factura" Visible="false" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="desart" HeaderText="Descripcion" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="venta" HeaderText="Venta" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="comi" HeaderText="% Comision" DataFormatString="{0:N4}" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="apo" HeaderText="Aportacion" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="codfac" HeaderText="codfac" Visible="false" />


                                                </Columns>
                                                <%--<HeaderStyle CssClass="RowStyle" Font-Names="arial" Font-Size="0.7em" />
                                                        <ItemStyle Font-Names="arial" Font-Size="0.7em" />--%>
                                            </asp:GridView>
                                        </div>

                                    </div>

                                    <!-- -->
                                </div>
                            </div>
                        </div>

                        <!-- BOTONES -->
                        <div class="modal-footer">
                            <%--<div class="col-lg-6 col-xs-6">
                                        <asp:Button ID="btnGuardar" class="btn btn-info btn-block" runat="server" Text="Guardar" OnClick="guardar_click" />
                                    </div>--%>

                            <input id="btnCancelar" type="button" class="btn btn-danger btn-block" onclick="ModalClose();" value="Cancelar" />

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <input type="hidden" runat="server" id="h_idc_factura" value="" />
</asp:Content>
