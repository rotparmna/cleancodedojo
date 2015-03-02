<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarCubrimiento.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCubrimiento" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCubrimiento.ascx"
    TagPrefix="uc2" TagName="UC_CrearCubrimiento" %>

<asp:MultiView ID="mltvDC"
    runat="server" ActiveViewIndex="0">
    <asp:View ID="vConsultaDC"
        runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="Label1"
                    runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Titulo %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje"
                    runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblEntidad" runat="server"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Entidad %>"></asp:Label>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblIdEntidad"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtEntidad"
                            runat="server" MaxLength="100"
                            Height="16px" Width="270px"
                            ReadOnly="true" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblContrato"
                            runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Contrato %>"></asp:Label>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblIdContrato"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtContrato"
                            runat="server" MaxLength="80"
                            Height="16px" Width="270px"
                            ReadOnly="true" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Plan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblIdPlan"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtPlan"
                            runat="server" MaxLength="100"
                            Height="16px" Width="270px"
                            ReadOnly="true" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIdAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_IdAtencion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAtencion"
                            runat="server" MaxLength="8"
                            Height="16px" Width="270px"
                            ReadOnly="true" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdAtencion"
                            runat="server" TargetControlID="txtIdAtencion"
                            FilterType="Numbers" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_TipoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTipoProducto"
                            runat="server" MaxLength="30"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_GrupoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGrupoProducto"
                            runat="server" MaxLength="50"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Producto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProducto"
                            runat="server" MaxLength="255"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblComponente"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Componente %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlComponente"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtComponente"
                            runat="server" MaxLength="255"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCubrimiento"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Cubrimiento %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlClaseCubrimiento"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtCubrimiento"
                            runat="server" MaxLength="255"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblActivo"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Activo %>"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo"
                            runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblBuscar" runat="server"
                            Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgBuscar"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscar_Click"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblCrear" runat="server"
                            Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgAdmCubrimiento"
                            runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>"
                            OnClick="ImgAdmCubrimiento_Click"
                            ImageUrl="~/App_Themes/SAHI/images/adicionar.png" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <fieldset id="fsResultado"
            runat="server" visible="false">
            <legend>
                <asp:Label ID="Label6"
                    runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
            </legend>
            <div id="divGrilla">
                <br />
                <uc2:UCPaginacion runat="server"
                    ID="pagControlCubrimiento" />
                <asp:GridView ID="grvCubrimientos"
                    runat="server" AutoGenerateColumns="False"
                    CssClass="AspNet-GridView"
                    AllowSorting="True"
                    OnRowDataBound="GrvCubrimientos_RowDataBound"
                    OnRowCommand="GrvCubrimientos_RowCommand"
                    DataKeyNames="IdCubrimiento"
                    EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                    OnRowEditing="GrvCubrimientos_RowEditing">
                    <Columns>
                        <asp:CommandField ButtonType="Image"
                            HeaderText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                            SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                            ItemStyle-Width="16px"
                            ItemStyle-Height="16px"
                            SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png"
                            ShowSelectButton="true"
                            ItemStyle-CssClass="Centrado" />
                        <asp:CommandField ButtonType="Image"
                            HeaderText="<%$ Resources:GlobalWeb, ComandoModificar %>"
                            EditText="<%$ Resources:GlobalWeb, ComandoModificar %>"
                            ItemStyle-Width="16px"
                            ItemStyle-Height="16px"
                            EditImageUrl="~/App_Themes/SAHI/images/editar.png"
                            ShowEditButton="true"
                            ItemStyle-CssClass="Centrado" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAtencion"
                                    runat="server" Text='<%# Eval("IdAtencion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_IdAtencion %>"
                            DataField="IdAtencion"
                            SortExpression="IdAtencion"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Plan %>"
                            DataField="Plan.Nombre"
                            SortExpression="Plan.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPlanGrid"
                                    runat="server" Text='<%# Eval("Plan.Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPlanGrid"
                                    runat="server" Text='<%# Eval("Plan.Nombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblTextoCubrimiento"
                                    runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_IdCubrimiento  %>" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCubrimiento"
                                    runat="server" Text='<%# Eval("IdCubrimiento") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_TipoProducto %>"
                            DataField="TipoProducto.Nombre"
                            SortExpression="TipoProducto.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_GrupoProducto %>"
                            DataField="GrupoProducto.Nombre"
                            SortExpression="GrupoProducto.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProducto"
                                    runat="server" Text='<%# Eval("Producto.IdProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_CodigoProducto %>"
                            DataField="Producto.IdProducto"
                            SortExpression="Producto.IdProducto"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_NombreProducto %>"
                            DataField="Producto.Nombre"
                            SortExpression="Producto.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Componente %>"
                            DataField="TipoComponente.NombreComponente"
                            SortExpression="TipoComponente.NombreComponente"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Cubrimiento %>"
                            DataField="ClaseCubrimiento.Nombre"
                            SortExpression="ClaseCubrimiento.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdContrato"
                                    runat="server" Text='<%# Eval("IdContrato") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdClaseCubrimiento"
                                    runat="server" Text='<%# Eval("IdClaseCubrimiento") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPlan"
                                    runat="server" Text='<%# Eval("IdPlan") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdTipoProducto"
                                    runat="server" Text='<%# Eval("IdTipoProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdGrupo"
                                    runat="server" Text='<%# Eval("IdGrupoProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCodigoComponente"
                                    runat="server" Text='<%# Eval("Componente") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSeleccionar"
                                    runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Activo  %>" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActivo"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </fieldset>
        <div class="contenedorBotonesPopup">
                <asp:ImageButton ID="imgBtnSalir"
                    runat="server"
                    ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" />
            </div>
    </asp:View>

    <asp:View ID="CrearModificarDC"
        runat="server">
        <uc2:UC_CrearCubrimiento
            runat="server" ID="ucCrearCubrimiento" />
    </asp:View>
</asp:MultiView>