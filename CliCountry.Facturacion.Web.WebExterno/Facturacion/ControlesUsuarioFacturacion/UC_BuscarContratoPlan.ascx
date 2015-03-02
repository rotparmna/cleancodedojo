<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarContratoPlan.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarContratoPlan" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc1" TagName="UC_Paginacion" %>
<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo"
            CssClass="LabelTitulo"
            runat="server" Text="<%$ Resources:ControlesUsuario, ContratoPlan_Titulo %>"></asp:Label>
    </div>
    <asp:Label ID="LblMensaje"
        runat="server"></asp:Label>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblNumAfiliacion"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:ControlesUsuario, ContratoPlan_Entidad %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEntidad" ClientIDMode="Static"
                    runat="server" MaxLength="100"
                    Width="270px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblContrato"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:ControlesUsuario, ContratoPlan_Contrato %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContrato" ClientIDMode="Static"
                    runat="server" MaxLength="80"
                    Width="270px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMontoEjecutado"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:ControlesUsuario, ContratoPlan_Plan %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPlan" ClientIDMode="Static"
                    runat="server" MaxLength="85"
                    Width="270px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblBuscar"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td>
                <asp:ImageButton ID="ImgBuscar"
                    runat="server"
                    ImageUrl="~/App_Themes/SAHI/images/search.png"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonConsultar %>"
                    OnClick="ImgBuscar_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <fieldset id="fsResultado"
        runat="server">
        <legend>
            <asp:Label ID="Label6"
                runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
        </legend>
        <div id="divGrilla">
            <br />
            <uc1:UC_Paginacion runat="server"
                ID="pagControl" />
            <asp:GridView ID="grvContratoPlan"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="AspNet-GridView"
                EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                AllowSorting="false" OnRowCommand="GrvBuscarContratoPlan_RowCommand">
                <Columns>
                    <asp:CommandField ButtonType="Image"
                        SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                        ItemStyle-Width="16px"
                        ItemStyle-Height="16px"
                        SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png"
                        ShowSelectButton="true">
                        <ItemStyle Height="16px"
                            Width="16px" />
                    </asp:CommandField>
                    <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, ContratoPlan_Entidad %>"
                        DataField="Tercero.Nombre" />
                    <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, ContratoPlan_Contrato %>"
                        DataField="Contrato.Nombre" />
                    <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, ContratoPlan_Plan %>"
                        DataField="Plan.Nombre" />
                    <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, ContratoPlan_IdPlan %>"
                        DataField="Plan.Id"
                        ItemStyle-CssClass="Centrado" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </fieldset>
    <div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server"
            ID="ImgRegresar"
            ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>"
            OnClick="ImgRegresar_Click" />
    </div>
</div>
