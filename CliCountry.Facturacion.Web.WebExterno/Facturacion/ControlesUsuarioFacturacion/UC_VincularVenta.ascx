<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_VincularVenta.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_VincularVenta" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarAtencion.ascx" TagPrefix="uc2" TagName="UC_BuscarAtencion" %>

<asp:MultiView runat="server" ID="multi" ActiveViewIndex="0">
    <asp:View ID="View1" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ControlesUsuario, Ventas_Titulo %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="lblMensaje" runat="server" />
            </div>
            <table style="border: 1px solid lightgray;">
                <tr>
                    <td style="border: 1px solid lightgray;">
                        <asp:Label ID="lblCampoAtencionDestino" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Ventas_AtencionDestino %>" />
                    </td>
                    <td style="border: 1px solid lightgray;">
                        <asp:Label SkinID="LabelCampo" ID="lblAtencionOrigen" runat="server" />
                    </td>
                    <td style="border: 1px solid lightgray;"></td>
                    <td style="border: 1px solid lightgray;"></td>
                </tr>
                <tr>
                    <td style="border: 1px solid lightgray;">
                        <asp:Label ID="lblNroAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Ventas_AtencionOrigen %>" />
                    </td>
                    <td style="border: 1px solid lightgray;">
                        <asp:Label SkinID="LabelCampo" ID="lblAtencionDestino" runat="server" />
                    </td>
                    <td style="border: 1px solid lightgray;">
                        <asp:Label ID="lblBuscar1" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgBuscarAtencion" runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscarAtencion_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <fieldset id="fsResultado" runat="server" visible="false">
            <legend>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:ControlesUsuario, Ventas_TituloGrilla %>"></asp:Label>
            </legend>
            <br />
            <br />
            <div style="background-color: lightgray; width: 90%; vertical-align: text-top; text-align: left; color: #003366; margin-left: 25px;">
                <asp:Label Text="<%$ Resources:ControlesUsuario, Ventas_MensajeVinculacion %>" runat="server" />
                &nbsp;&nbsp;
                        <asp:Image runat="server" ImageUrl="~/App_Themes/SAHI/images/info_icono.png" />
            </div>
            <br />
            <div style="margin-left: auto; margin-right: auto;">
                <asp:Label SkinID="LabelCampo" ID="Label2" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" runat="server" />&nbsp; >>
                &nbsp;
                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                    OnClick="BtnGuardar_Click" />
            </div>
            <div id="divGrilla">
                <br />
                <asp:Label ID="lblCantidadRegistros" SkinID="LabelCampo" runat="server" />
                <br />
                <uc2:UCPaginacion runat="server" ID="pagControl" />
                <asp:GridView ID="grvVentas" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="<%$ Resources:ControlesUsuario, Ventas_GrillaSinDatos %>"
                    DataKeyNames="IdTransaccion,NumeroVenta"
                    AllowSorting="false"
                    CssClass="AspNet-GridView">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="Centrado" ItemStyle-Width="70">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="chkVincularTodos" onclick="CheckTodos_Click(event,'chkVincular');" />
                                <asp:Label ID="lblVincular" runat="server" Text="<%$ Resources:ControlesUsuario, Ventas_Vincular %>" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkVincular" CssClass="chkVincular" onclick="CheckBox_Click(event, 'chkVincular');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_Transaccion %>" DataField="NombreTransaccion"
                            SortExpression="NombreTransaccion"
                            ItemStyle-CssClass="Numero" />
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_NroVenta %>" DataField="NumeroVenta"
                            SortExpression="NumeroVenta"
                            ItemStyle-CssClass="Numero" />
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Ventas_FechaVenta %>"
                            DataField="FechaVenta"
                            DataFormatString="{0:d}"
                            SortExpression="FechaVenta"
                            ItemStyle-CssClass="Centrado" />
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
    </asp:View>
    <asp:View ID="View2" runat="server">
        <uc2:UC_BuscarAtencion runat="server" ID="ucBuscarAtencion" />
    </asp:View>
</asp:MultiView>