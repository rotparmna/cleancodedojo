<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Exclusiones.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_Exclusiones" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" runat="server" Text="<%$ Resources:Exclusiones, Exclusiones_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
</div>
<fieldset id="fsResultado" runat="server">
    <legend>
        <asp:Label ID="lblResultadoBusqueda" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
    </legend>
    <div id="divGrilla" style="max-height: 350px;">
        <br />
        <asp:GridView ID="grvExclusiones" runat="server" AutoGenerateColumns="False"
            CssClass="AspNet-GridView" AllowSorting="false"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>" DataKeyNames="IdExclusion">
            <Columns>
                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="Centrado">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAplicarTodos" runat="server"
                            onclick="CheckTodos_Click(event, 'chkAplicar');" />
                        <asp:Label ID="lblAplicar" runat="server" Text="<%$ Resources:Exclusiones, Exclusiones_GrvChkAplicar %>" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAplicar"
                            runat="server"
                            ToolTip="<%$ Resources:Exclusiones, Exclusiones_GrvChkAplicarToolTip %>"
                            onclick="CheckBox_Click(event, 'chkAplicar');"
                            Checked='<%# Eval("CheckActivo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_TipoAtencion %>" DataField="TipoAtencionNombre" SortExpression="TipoAtencionNombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_Servicio %>" DataField="NombreServicioExclusion" SortExpression="NombreServicio" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_TipoProducto %>" DataField="TipoProductoNombre" SortExpression="TipoProductoNombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_GrupoProducto %>" DataField="GrupoProductoNombre" SortExpression="GrupoProductoNombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_Producto %>" DataField="ProductoNombre" SortExpression="ProductoNombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_ProductoAlterno %>" DataField="ProductoAlternoNombre" SortExpression="ProductoAlternoNombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:Exclusiones, Exclusiones_ManualOContrato %>" DataField="ManualContrato" SortExpression="ManualContrato" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdExclusion" runat="server" Text='<%# Eval("IdExclusion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
<br />
<div>
    <table style="align-content: center; width: 62%;">
        <tr>
            <td align="right">
                <asp:Label ID="lblGuardarDatosTemporales" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Exclusiones_GuardarCheckExclusiones %>"></asp:Label>
            </td>
            <td align="right">
                <asp:ImageButton ID="ImgAplicarExclusiones" runat="server"
                    ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                    ToolTip="<%$ Resources:ControlesUsuario, Exclusiones_GuardarCheckExclusiones %>"
                    OnClick="ImgAplicarExclusiones_Click" />
            </td>
        </tr>
    </table>
</div>