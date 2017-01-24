<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="tareas_asignadas_lista.aspx.cs" Inherits="presentacion.tareas_asignadas_lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
        <script type="text/javascript">
        function Press(id, url, estado, desc)
        {
            return true;
        }
        function PressRev(id, url, estado, desc) {
            return true;
        }
        function ModalClose() {
            $('#myModal').modal('hide');
        }
        function ModalConfirm(cTitulo, cContenido, ctype) {
            var audio = new Audio('sounds/modal.wav');
            audio.play();
            $('#myModal').modal('show');
            $('#myModal').removeClass('modal fade modal-info');
            $('#myModal').addClass(ctype);
            $('#modal_title').text(cTitulo);
            $('#content_modal').text(cContenido);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-12">
                    <div class="card">
                        <div class="card-header" style="background-color: #22A7F0; color: white;">
                            <div class="card-title" style="background-color: #22A7F0; color: white;">
                                <div class="title" style="background-color: #22A7F0; color: white;">
                                    <h3 style="background-color: #22A7F0; color: white;">Tareas Asignadas Pendientes <small style="background-color: #22A7F0; color: white;" id="total_tareas" runat="server">
                                        <asp:Label ID="lbltotaltt" runat="server" Text=""></asp:Label></small></h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" id="TAREAS_IND" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4>Escriba un Filtro<small> Ingrese "/" para Buscar Departamento, "+" para Buscar Empleados y "*" para Buscar Estado</small></h4>
                                    <asp:TextBox ID="TextBox1" onfocus="$(this).select();" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpuesto_filtro_TextChanged" placeholder="Buscar"></asp:TextBox>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>

                                </div>
                            </div>
                            <h3 style="text-align: center" id="notareas" runat="server" visible="false">No tiene Tareas Asignadas Pendientes <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>
                            <asp:UpdatePanel ID="Ddd" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkexcel" />
                                    <asp:PostBackTrigger ControlID="lnkexcel" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:LinkButton Width="49%" ID="lnkexcel" CssClass="btn btn-success btn-block" OnClick="lnkexcel_Click" runat="server">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>

                                    <asp:LinkButton Width="49%" ID="lnkcancelar" Visible="false" CssClass="btn btn-danger btn-block" runat="server">Cancelar Tareas Seleccionadas </asp:LinkButton>
                                    <asp:LinkButton Width="100%" ID="lnkseltodo" CssClass="btn btn-default btn-block" OnClick="lnksel_Click" runat="server">Seleccionar Todas las Tareas Visibles</asp:LinkButton>

                                    <div class="list-group">

                                        <asp:Repeater ID="repeat_tareas" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                                    <span onclick="return PressRev('<%#Eval("idc_tarea")%>','<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>','<%#Eval("tipo")%>','<%#Eval("descripcion").ToString().Replace(System.Environment.NewLine," ")%>')"
                                                        class="badge btn btn-default btn-xs"><%#Eval("icono")%></span>
                                                    <span class="badge" style="padding:0px;" onclick="return true;">
                                                        <asp:Button ID="lnkseleccionar" CommandArgument='<%#Eval("idc_tarea")%>' runat="server" OnClick="lnkselected_Click"
                                                            CssClass="btn btn-default" Text="Seleccionar"></asp:Button></span>
                                                    <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
                                                    <p class="list-group-item-text"><%#Eval("descripcion")%></p>
                                                    <p class="list-group-item-text"><strong>Realiza</strong>: <%#Eval("empleado")%></p>
                                                    <p class="list-group-item-text"><strong>Depto</strong>: <%#Eval("depto")%></p>
                                                    <p class="list-group-item-text"><strong>Estado</strong>: <%#Eval("tipo")%></p>
                                             
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="card-body" id="tareas_todas" runat="server" visible="false">
                                <h3 style="text-align: center" id="H1" runat="server" visible="false">No tiene Tareas Pendientes <i class="fa fa-thumbs-o-up" aria-hidden="true"></i></h3>

                                <div class="row">
                                    <div class="col-lg-10 col-xs-8">
                                        <h4>Escriba un Filtro <small>Ingrese "/" para Buscar Departamento, "+" para Buscar Empleados y "*" para Buscar Estado</small></h4>
                                        <asp:TextBox ID="txtpuesto_filtro" runat="server" TextMode="SingleLine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpuesto_filtro_TextChanged" placeholder="Buscar"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2 col-xs-4">
                                        <h4>.</h4>
                                        <asp:LinkButton ID="lnkbuscarpuestos" runat="server" CssClass="btn btn-success btn-block" OnClick="lnkbuscarpuestos_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <h5>
                                    <asp:Label ID="lbtot_global" runat="server" Text=""></asp:Label></h5>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-block" OnClick="lnkexcel_Click" runat="server">Exportar a Excel <i class="fa fa-file-excel-o" aria-hidden="true"></i></asp:LinkButton>
                                <div class="list-group">
                                    <asp:Repeater ID="repeatglobal" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnktarea" PostBackUrl='<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>' runat="server" CssClass='<%#Eval("css_class")%>' ToolTip='<%#Eval("desc_completa")%>'>
                                               <span onclick="return PressRev('<%#Eval("idc_tarea")%>','<%#Eval("url").ToString()+presentacion.funciones.deTextoa64(Eval("idc_tarea").ToString())%>','<%#Eval("tipo")%>','<%#Eval("descripcion").ToString().Replace(System.Environment.NewLine," ")%>')" 
                                                class="badge btn btn-default btn-xs"><%#Eval("icono")%></span> <h5 class="list-group-item-heading"><strong><%#Eval("fecha_compromiso")%></strong></h5>
                                                <p class="list-group-item-text"><%#Eval("descripcion")%></p>
                                                <p class="list-group-item-text"><strong>Estado</strong>: <%#Eval("empleado")%></p>
                                                <p class="list-group-item-text"><strong>Depto</strong>: <%#Eval("depto")%></p>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                
                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 id="modal_title"><strong>Mensaje del Sistema</strong></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="text-align: center;">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                        <h4>
                                            <label id="content_modal"></label>
                                        </h4>
                                        <asp:TextBox ID="txtobservaciones" CssClass="form-control" placeholder="Puede Ingresar Observaciones" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        <asp:CheckBox ID="cbxcontarbien" Checked="False" CssClass="radio3 radio-check radio-info radio-inline" Text="Contar Mal las Tareas Canceladas" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-lg-6 col-xs-6">
                                    <asp:Button ID="Yes" class="btn btn-info btn-block" runat="server" Text="Aceptar" OnClick="lnkborrar_Click" />
                                </div>
                                <div class="col-lg-6 col-xs-6">
                                    <input id="No" class="btn btn-danger btn-block" type="button" onclick="ModalClose();" value="Cerrar" />
                                </div>
                            </div>
                        </div>
            </div>
        </div>

    </div>
</asp:Content>