<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReporteEstadoCuentaMultiple.aspx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ReporteEstadoCuentaMultiple" %>

<%@ Register Src="~/Comun/Controles/Reporte.ascx" TagPrefix="uc1" TagName="Reporte" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_MostrarConceptos.ascx" TagPrefix="uc1" TagName="UC_MostrarConceptos" %>
<%@ Register Src="~/Comun/Controles/ReporteMultiple.ascx" TagPrefix="uc1" TagName="ReporteMultiple" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedorPrincipal" runat="server">
    <script language="javascript" type="text/javascript">
        var tabSeleccionado = 0;

        function pageLoad(sender, args) {
            $("#tabs").tabs({
                activate: function () {
                    tabSeleccionado = $('#tabs').tabs('option', 'active');
                }, active: tabSeleccionado
            });
        };

        Sys.Application.add_load(function () {
            var form = Sys.WebForms.PageRequestManager.getInstance()._form;
            form._initialAction = form.action = window.location.href;
        });

        $(document).ready(function () {
            $(window).on('unload', function (e) {
                hfGuardar = $('[id$=hfGuardar]').filter(':first');
                btnActualizarProceso = $('[id$=BtnActualizarProceso]');
                if ($(hfGuardar).val() == "0") {
                    $(btnActualizarProceso)[0].click();
                }
                return true;
            });
        });
    </script>


    <div class="Menu_superior">
        <asp:Label SkinID="LabelCampo" ID="lblConsultarEstado" runat="server" Height="30"
            Text="<%$ Resources:AdministradorFacRelacion, FacRelacion_GuardarFacturacion %>" />
        &nbsp;
        <asp:ImageButton ID="imgGuardar" runat="server"
            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>" OnClick="ImgGuardar_Click" OnClientClick="this.style.display='none'" />
    </div>

    <asp:RadioButtonList ID="rblListEstadoCuenta" BorderStyle="Solid" Width="100%" BorderWidth="1" BorderColor="RoyalBlue" runat="server" AutoPostBack="True" RepeatDirection="Vertical" OnSelectedIndexChanged="rblListEstadoCuenta_SelectedIndexChanged" AppendDataBoundItems="True">
    </asp:RadioButtonList>
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
            <asp:Panel ID="panPestanas" runat="server">
                <div class="DivTabs" id="divTabsMarco" runat="server">
                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">
                                <asp:Label ID="lblEstadoCuenta" runat="server" Text="<%$ Resources:GlobalWeb, General_LabelEstadoCuenta %>"></asp:Label>
                            </a>
                                <br />
                            </li>
                            <li><a href="#tabs-2">
                                <asp:Label ID="lblConceptosCobro" runat="server" Text="<%$ Resources:GlobalWeb, General_LabelConceptos %>"></asp:Label>
                            </a>
                                <br />
                            </li>                           
                        </ul>
                        <div id="tabs-1" style="height: auto">
                            <div runat="server" id="div1">
                                <uc1:Reporte ID="RptEstadoCuenta" runat="server" Zoom="true" Imprimir="true" ExportarPdf="true" ExportarExcel="true" ReporteLocal="true" />
                            </div>
                        </div>
                        <div id="tabs-2">
                            <div runat="server" id="div2">
                                <uc1:UC_MostrarConceptos runat="server" ID="ucMostrarConceptos" />
                            </div>
                        </div>                       
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
    <asp:LinkButton ID="BtnActualizarProceso" runat="server" Width="0" Height="0" OnClick="BtnActualizarProceso_Click" />
    <asp:HiddenField ID="hfGuardar" runat="server" Value="0" />
</asp:Content>