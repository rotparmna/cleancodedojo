<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarCondicionesTarifas.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesTarifas" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCondicionesTarifa.ascx" TagPrefix="uc1" TagName="UC_CrearCondicionesTarifa" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<asp:MultiView ID="mltvBuscarCT" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDefinirCondicionesTarifa" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Titulo %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje" runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblEntidad" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Entidad %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntidadDC" runat="server" Enabled="false" MaxLength="8" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtContratoDC" runat="server" Enabled="false" ReadOnly="true" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlanDC" runat="server" Enabled="false" MaxLength="8" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Atencion %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtAtencionDC" runat="server" Enabled="false" MaxLength="8" Width="270px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender ID="validaAtencion" runat="server" FilterType="Numbers" TargetControlID="txtAtencionDC" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProductoTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoProducto %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTipoProductoTar" runat="server" MaxLength="30" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProductoTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_GrupoProducto %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGrupoProdTar" runat="server" MaxLength="50" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProductoTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Producto %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProdTar" runat="server" MaxLength="255" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblComponenteTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Componente %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlComponente" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoRelacionTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoRelacion %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblActivoTar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Activo %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBuscar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgBuscar" runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscar_Click"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonConsultar %>" />
                    </td>
                    <td>
                        <asp:Label ID="lblCrear" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgNuevo"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>" OnClick="ImgNuevo_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <fieldset runat="server" visible="false" id="fsResultado">
                <legend>
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
                </legend>
                <div id="divGrilla">
                    <br />
                    <uc2:UCPaginacion runat="server" ID="pagControl" />
                    <asp:GridView ID="grvCondicionesTarifas" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>" DataKeyNames="Id"
                        CssClass="AspNet-GridView" AllowSorting="false" OnRowCommand="GrvCondicionesTarifas_RowCommand">
                        <Columns>
                            <asp:CommandField ButtonType="Image"
                                ShowSelectButton="true"
                                SelectImageUrl="~/App_Themes/SAHI/images/editar.png"
                                SelectText="<%$ Resources:GlobalWeb, General_BotonEditar  %>" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoProducto %>" DataField="NombreTipoProducto" SortExpression="NombreTipoProducto" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_GrupoProducto %>" DataField="NombreGrupoProducto" SortExpression="NombreGrupoProducto" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Producto %>" DataField="NombreProducto" SortExpression="NombreProducto" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Componente %>" DataField="Componente" SortExpression="Componente" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoRelacion %>" DataField="TipoRelacion" SortExpression="TipoRelacion" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_VigenciaTarifa %>"
                                DataField="VigenciaTarifa"
                                SortExpression="VigenciaTarifa"
                                ItemStyle-CssClass="Centrado"
                                DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_NombreTarifa %>" DataField="NombreTarifa" SortExpression="NombreTarifa" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Valor %>" DataField="ValorPropio" DataFormatString="{0:N}" SortExpression="ValorPropio" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_VigenciaCondicion %>"
                                DataField="VigenciaCondicion"
                                SortExpression="VigenciaCondicion"
                                ItemStyle-CssClass="Centrado"
                                DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Descripcion %>" DataField="DescripcionCondicion" SortExpression="DescripcionCondicion" />
                            <asp:TemplateField ShowHeader="False">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSeleccionar" runat="server" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Activo  %>" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivo" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IndHabilitado")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                </div>
            </fieldset>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="vCrearCT" runat="server">
        <uc1:UC_CrearCondicionesTarifa runat="server" ID="ucDefinirCondicionesTarifa" />
    </asp:View>
</asp:MultiView>