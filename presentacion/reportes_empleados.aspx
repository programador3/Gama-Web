<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="reportes_empleados.aspx.cs" Inherits="presentacion.reportes_empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1 class="page-header" style="text-align: center;">Reportes de Empleado</h1>
    <div class="row">
        <div class="col-lg-2" style="align-content: center;">
            <a>
                <asp:Image ID="imgEmpleado" runat="server" class="img-responsive" alt="Gama System" Style="width: 160px; margin: 0 auto;" />
            </a>
        </div>
        <div class="col-lg-10" style="text-align: left">
            <div class="form-group">
                <h4>
                    <strong>Nombre Empleado: </strong>
                    <asp:Label ID="lblEmpleado" runat="server" Text=""></asp:Label>
                </h4>
                <h4><strong>Puesto: </strong>
                    <asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label>
                </h4>
                <h4><strong>Departamento: </strong>
                    <asp:Label ID="lbldepto" runat="server" Text=""></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <asp:LinkButton ID="lnkhorarios" OnClick="lnkhorarios_Click" CommandName="2" runat="server">
                <div class="card red summary-inline">
                    <div class="card-body">
                        <i class="fa fa-calendar fa fa-4x" aria-hidden="true"></i>
                        <div class="content">
                            <div class="title">IR</div>
                            <div class="sub-title">Solicitud Cambio de Horario</div>
                        </div>
                        <div class="clear-both"></div>
                    </div>
                </div>
            </asp:LinkButton>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <asp:LinkButton ID="LinkButton1" OnClick="lnkhorarios_Click" CommandName="3" runat="server">
                <div class="card blue summary-inline">
                    <div class="card-body">
                        <i class="fa fa-users fa-4x" aria-hidden="true"></i>
                        <div class="content">
                            <div class="title">IR</div>
                            <div class="sub-title">Motivos de Faltas</div>
                        </div>
                        <div class="clear-both"></div>
                    </div>
                </div>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>