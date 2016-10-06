<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="mapa.aspx.cs" Inherits="presentacion.mapa" %>

<asp:Content ID="pdetalle_head" ContentPlaceHolderID="head" runat="server">

    <%--   <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBTBOXOFJPFg0DnBmx5Nmo-ZROhiRuOTiw"
  type="text/javascript"></script>
    <script type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0"></script>
    --%>
    <script src='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.js'></script>
    <link href='https://api.mapbox.com/mapbox.js/v2.4.0/mapbox.css' rel='stylesheet' />
    <script type="text/javascript">

        function ver(lat, lon) {
            $("#mapcanvas").height($(window).height() - 250);
            
            L.mapbox.accessToken = 'pk.eyJ1IjoicHJvZ3JhbWFkb3IzIiwiYSI6ImNpc2hwN3JoNjAwNXczM3BpZ3ZocmUwamMifQ.Qpennd5geuMVwKlgUBb69w';
            var map = L.mapbox.map('mapcanvas', 'mapbox.streets')
                .setView([lat, lon], 15);
            var marker = L.marker([lat, lon], {
                icon: L.mapbox.marker.icon({
                    'marker-color': '#f86767'
                })
            });
            marker.addTo(map);
        }
        function cerrar() {
            window.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="pdetalle_content" ContentPlaceHolderID="Contenido" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h2>Ubicación<span>
          <asp:Button ID="close" runat="server" Text="Cerrar Mapa" CssClass="btn btn-danger" ToolTip="Limpiar" OnClick="close_Click" />
                                                                              </span></h2>
            <h5>La Ubicación esta en un radio de 30 metros.</h5>
            <strong><asp:Label ID="lbldetalles" runat="server" Text="" Visible="false"></asp:Label></strong>
        </div>
        <div class="col-lg-12 col-xs-12">
            <div id="mapcanvas" style="width: 100%;"></div>
        </div>
    </div>
</asp:Content>