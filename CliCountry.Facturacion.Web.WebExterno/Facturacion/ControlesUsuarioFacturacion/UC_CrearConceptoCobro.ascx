<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_CrearConceptoCobro.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearConceptoCobro" %>

<asp:MultiView runat="server" ID="multi" ActiveViewIndex="0">
    <asp:View ID="View1" runat="server">
        <div id="contenedorControl">
            <div class="Header" style="text-align: center">
                <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server"
                    Text="<%$ Resources:ControlesUsuario,ConceptoCobro_LblTitulo %>">
                </asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje" runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="LblPEntidad" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CrearPaquetes, LblEntidad %>"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtEntidad" runat="server" Width="270px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblPContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CrearPaquetes, LblContrato %>"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtContrato" runat="server" Width="270px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CrearPaquetes, LblPlan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPlan" runat="server" Width="270px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CrearPaquetes, LblAtencion %>"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtAtencion" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTituloValorConcepto" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, lblValorConcepto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValorConcepto" runat="server" Width="180px" MaxLength="9"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender ID="fteValorConcepto" runat="server" TargetControlID="TxtValorConcepto" FilterType="Numbers" ValidChars="," />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        <asp:Label ID="lblGuardar"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                        &nbsp;
            <asp:ImageButton ID="ImgGuardarConcepto"
                runat="server"
                ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                OnClick="ImgGuardarConcepto_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
            </div>
        </div>
    </asp:View>
</asp:MultiView>