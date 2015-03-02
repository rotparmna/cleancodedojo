<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarVentas.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarVentas" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:ControlesUsuario, VentaListado_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="lblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblNroVenta" runat="server" Text="<%$ Resources:ControlesUsuario, Ventas_NroVenta %>" />
            </td>
            <td>
                <asp:TextBox ID="txtNroVenta" runat="server" MaxLength="15" Height="16px" Width="220px"></asp:TextBox>
                <AspAjax:FilteredTextBoxExtender ID="validaNroVenta" runat="server" TargetControlID="txtNroVenta" FilterType="Numbers"></AspAjax:FilteredTextBoxExtender>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblAtencion" runat="server" Text="<%$ Resources:ControlesUsuario, Atencion_IdAtencion %>" />
            </td>
            <td>
                <asp:TextBox ID="txtIdAtencion" runat="server" MaxLength="15" Height="16px" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblActInformacion" runat="server" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td colspan="3">
                <asp:ImageButton ID="ImgBuscar"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                    OnClick="ImgBuscar_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
        </tr>
    </table>
</div>
<br />
<br />
<fieldset id="fsResultado" runat="server" visible="false">
    <legend>
        <asp:Label ID="lblResultadoBusqueda" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
    </legend>
    <div id="divGrilla">
        <br />
        <uc2:UCPaginacion runat="server" ID="pagControl" />
        <asp:GridView ID="grvVentas" runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:ControlesUsuario, Ventas_GrillaSinDatos %>"
            DataKeyNames="IdTransaccion,NumeroVenta" OnRowCommand="GrvVentas_RowCommand"
            AllowSorting="false" CssClass="AspNet-GridView">
            <Columns>
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbSelect" runat="server" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Select" ImageUrl="~/App_Themes/SAHI/images/seleccionar.png" Text="Select" ToolTip="Seleccionar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdVenta" runat="server" Text='<%# Eval("IdTransaccion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_Transaccion %>" DataField="NombreTransaccion"
                    SortExpression="NombreTransaccion"
                    ItemStyle-CssClass="Numero" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_NroVenta %>" DataField="NumeroVenta"
                    SortExpression="NumeroVenta"
                    ItemStyle-CssClass="Numero" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_FechaVenta %>" DataField="FechaVenta"
                    ItemStyle-CssClass="Centrado"
                    DataFormatString="{0:d}"
                    SortExpression="FechaVenta" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_IdAtencion %>" DataField="IdAtencion"
                    SortExpression="IdAtencion"
                    ItemStyle-CssClass="Numero" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_Paciente %>" DataField="Paciente" ItemStyle-Wrap="true" SortExpression="Paciente" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
<div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif" 
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
    </div>