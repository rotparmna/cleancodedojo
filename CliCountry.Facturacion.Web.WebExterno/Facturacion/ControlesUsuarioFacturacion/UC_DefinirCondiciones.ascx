<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_DefinirCondiciones.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_DefinirCondiciones" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarDescuentos.ascx" TagPrefix="uc1" TagName="UC_BuscarDescuentos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCondicionesFacturacion.ascx" TagPrefix="uc1" TagName="UC_BuscarCondicionesFacturacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCondicionesTarifas.ascx" TagPrefix="uc1" TagName="UC_BuscarCondicionesTarifas" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCubrimiento.ascx" TagPrefix="uc1" TagName="UC_BuscarCubrimiento" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCondicionesCubrimiento.ascx" TagPrefix="uc1" TagName="UC_BuscarCondicionesCubrimiento" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarExclusiones.ascx" TagPrefix="uc1" TagName="UC_BuscarExclusiones" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblEntidad" runat="server" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Entidad %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtEntidad" runat="server" MaxLength="8" Width="270px" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblContrato" runat="server" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtContrato" ReadOnly="true" runat="server" Width="270px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblPlan" runat="server" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtPlan" runat="server" MaxLength="8" Width="270px" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblAtencion" runat="server" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Atencion %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtAtencion" runat="server" MaxLength="8" Width="270px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table id="tblBotones">
        <tr>
            <td>
                <asp:ImageButton ID="imgDescuentos" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonDescuentos %>" OnClick="ImgDescuentos_Click" />
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="LblDescuento" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonDescuentos %>" />
            </td>
            <td>
                <asp:ImageButton ID="imgCondicionTarifa" Height="20px" Width="20px" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonCondicionTarifa %>" OnClick="ImgCondicionTarifa_Click" />
            </td>
            <td>
                <asp:Label Style="text-align: right" SkinID="LabelCampo" ID="LblCondicionTarifa" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonCondicionTarifa %>" />
            </td>
            <td>
                <asp:ImageButton ID="imgDefinirExclusiones" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonDefinirExclusiones %>" OnClick="ImgDefinirExclusiones_Click" />
            </td>
            <td>
                <asp:Label Style="text-align: right" SkinID="LabelCampo" ID="LblDefinirExclusiones" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonDefinirExclusiones %>" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="imgCondicionFacturacion" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonCondicionFacturacion %>" OnClick="ImgCondicionFacturacion_Click" />
            </td>
            <td>
                <asp:Label Style="text-align: right" SkinID="LabelCampo" ID="LblCondicionFacturacion" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonCondicionFacturacion %>" />
            </td>
            <td>
                <asp:ImageButton ID="imgDefinirCubrimiento" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonDefinirCubrimiento %>" OnClick="ImgDefinirCubrimiento_Click" />
            </td>
            <td>
                <asp:Label Style="text-align: right" SkinID="LabelCampo" ID="LblDefinirCubrimiento" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonDefinirCubrimiento %>" />
            </td>
            <td>
                <asp:ImageButton ID="imgCondicionCubrimiento" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                    Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonCondicionCubrimiento %>" OnClick="ImgCondicionCubrimiento_Click" />
            </td>
            <td>
                <asp:Label Style="text-align: right" SkinID="LabelCampo" ID="LblCondicionCubrimiento" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonCondicionCubrimiento %>" />
            </td>
        </tr>
    </table>
</div>

<asp:Panel ID="pnlBuscarDescuentosPP" runat="server" ScrollBars="Auto" Width="1000" Height="600" BackColor="White" Style="display: none; padding: 10px; margin-right: 5px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImbCerrar" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarDescuentos runat="server" ID="ucBuscarDescuentos" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfBuscarDescuentos" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeBuscarDescuentos"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfBuscarDescuentos"
    PopupControlID="pnlBuscarDescuentosPP">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlBuscarCondicionesTarifas" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton2" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarCondicionesTarifas runat="server" ID="ucBuscarCondicionesTarifas" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfBuscarCondicionesTarifas" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeBuscarCondicionesTarifas"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfBuscarCondicionesTarifas"
    PopupControlID="pnlBuscarCondicionesTarifas">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlBuscarCondicionesFacturacion" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton1" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarCondicionesFacturacion runat="server" ID="ucBuscarCondicionesFacturacion" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfBuscarCondicionesFacturacion" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeBuscarCondicionesFacturacion"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfBuscarCondicionesFacturacion"
    PopupControlID="pnlBuscarCondicionesFacturacion">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlCondicionesCubrimientos" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton4" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarCondicionesCubrimiento runat="server" ID="ucCondicionesCubrimientos" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfCondicionesCubrimientos" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeCondicionesCubrimientos"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfCondicionesCubrimientos"
    PopupControlID="pnlCondicionesCubrimientos">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlDefinirCubrimientos" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton3" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarCubrimiento runat="server" ID="ucDefinirCubrimientos" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfDefinirCubrimientos" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeDefinirCubrimientos"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfDefinirCubrimientos"
    PopupControlID="pnlDefinirCubrimientos">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlDefinirExclusiones" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton5" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarExclusiones runat="server" ID="ucDefinirExclusiones" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfDefinirExclusiones" runat="server" />
<div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
    </div>
<AspAjax:ModalPopupExtender ID="mpeDefinirExclusiones"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfDefinirExclusiones"
    PopupControlID="pnlDefinirExclusiones">
</AspAjax:ModalPopupExtender>