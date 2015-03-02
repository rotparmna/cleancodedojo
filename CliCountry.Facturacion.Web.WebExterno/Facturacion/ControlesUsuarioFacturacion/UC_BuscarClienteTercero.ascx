<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarClienteTercero.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarClienteTercero" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarTercero.ascx" TagPrefix="uc1" TagName="UC_BuscarTercero" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCliente.ascx" TagPrefix="uc1" TagName="UC_BuscarCliente" %>

<div id="contenidoPrincipal" style="text-align: left; padding-left: 70px;">
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
    <br />
    <br />
    <div id="cliente">
        <table style="border: 1px solid lightgray; width: 92%;">
            <tr>
                <td style="text-align: left; width: 120px;">
                    <asp:Label ID="Label1" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Cliente %>" />
                </td>
                <td style="padding-left: 20px; text-align: left; width: 40%;">
                    <asp:TextBox runat="server" ID="TxtCliente" MaxLength="8" onKeyDown="return TxtClienteTercero_keydown('cliente',event);" />
                    <AspAjax:FilteredTextBoxExtender ID="ftbTxtCliente" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                        TargetControlID="TxtCliente" />
                    <asp:LinkButton ID="BtnCargarCliente" runat="server" OnClick="BtnCargarCliente_Click" Height="0" Width="0"></asp:LinkButton>
                </td>
                <td style="text-align: left; width: 220px;">
                    <asp:Label ID="Label2" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Responsable %>" />
                </td>
                <td style="text-align: left;">
                    <asp:ImageButton ID="ImgBuscarCliente" runat="server"
                        ImageUrl="~/App_Themes/SAHI/images/search.png"
                        Height="20px" Width="20px"
                        OnClick="ImgBuscarCliente_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 120px;">
                    <asp:Label ID="Label3" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Apellidos %>" />
                </td>
                <td style="padding-left: 20px; text-align: left; width: 40%;">
                    <asp:Label ID="LblApellidos" runat="server" />
                </td>
                <td style="">
                    <asp:Label ID="Label4" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Nombre %>" />
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="LblNombres" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 120px;">
                    <asp:Label ID="Label5" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_NoDocumento %>" />
                </td>
                <td style="padding-left: 20px; text-align: left; width: 40%;">
                    <asp:Label ID="LblNroDocumentoCliente" runat="server" />
                </td>
                <td />
                <td />
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div id="tercero">
        <table style="border: 1px solid lightgray; width: 92%;">
            <tr>
                <td style="text-align: left; width: 120px;">
                    <asp:Label ID="Label6" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Tercero %>" />
                </td>
                <td style="padding-left: 20px; text-align: left; width: 40%;">
                    <asp:TextBox runat="server" ID="TxtTercero" MaxLength="8" onKeyDown="return TxtClienteTercero_keydown('tercero',event);" />
                    <AspAjax:FilteredTextBoxExtender ID="ftbTercero" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                        TargetControlID="TxtTercero" />
                    <asp:LinkButton ID="BtnCargarTercero" runat="server" OnClick="BtnCargarTercero_Click" Height="0" Width="0"></asp:LinkButton>
                </td>
                <td style="text-align: left; width: 220px;">
                    <asp:Label ID="Label7" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_Responsable %>" />
                </td>
                <td style="text-align: left;">
                    <asp:ImageButton ID="ImgBuscarTercero" runat="server"
                        ImageUrl="~/App_Themes/SAHI/images/search.png"
                        Height="20px" Width="20px"
                        OnClick="ImgBuscarTercero_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 120px;">
                    <asp:Label ID="Label8" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_NombreTercero %>" />
                </td>
                <td style="padding-left: 20px; text-align: left; width: 40%;">
                    <asp:Label ID="LblNombreTercero" runat="server" />
                </td>
                <td style="">
                    <asp:Label ID="Label9" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, TerceroCliente_NoDocumento %>" />
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="LblNroDocumentoTercero" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</div>

<asp:Panel ID="pnlTercero" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton2" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarTercero runat="server" ID="ucBuscarTercero" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfTercero" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeTercero"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfTercero"
    PopupControlID="pnlTercero">
</AspAjax:ModalPopupExtender>

<asp:Panel ID="pnlCliente" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton3" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:UC_BuscarCliente runat="server" ID="ucBuscarCliente" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfCliente" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeCliente"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfCliente"
    PopupControlID="pnlCliente">
</AspAjax:ModalPopupExtender>