<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarDescuentos.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarDescuentos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearDescuentos.ascx" TagPrefix="uc1" TagName="UC_CrearDescuentos" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<asp:UpdatePanel ID="upBuscarDescuentos" runat="server">
    <ContentTemplate>

        <asp:MultiView ID="mltvDescuentos" runat="server" ActiveViewIndex="0">
            <asp:View ID="vDescuentos" runat="server">
                <div id="contenedorControl">
                    <div class="Header">
                        <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:Descuentos, Descuentos_Titulo %>"></asp:Label>
                    </div>

                    <div class="Mensaje">
                        <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntidad" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Entidad %>" Width="120px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEntidadDC" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" Width="120px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtContratoDC" runat="server"
                                    Enabled="false" ReadOnly="true" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trServicioAtencion">
                            <td>
                                <asp:Label ID="lblNombreServicio" SkinID="LabelCampo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:Configuracion, Descuento_Servicio%>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClaseServicio" runat="server" Width="270px" MaxLength="30"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoAtencion" SkinID="LabelCampo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:Configuracion, Descuentos_TipoAtencion%>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTipoAtencion" runat="server" Width="270px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPlanDC" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Atencion %>" Width="120px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtAtencionDC" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                                <AspAjax:FilteredTextBoxExtender ID="validaAtencion" runat="server" FilterType="Numbers" TargetControlID="txtAtencionDC" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoProductoDes" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_TipoProducto %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTipoProductoDes" runat="server" MaxLength="30" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblGrupoProductoDes" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_GrupoProducto %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrupoProductoDes" runat="server" MaxLength="50" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductoDes" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_Producto %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductoDes" runat="server" ValidationGroup="ValidarFactura" MaxLength="255" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblComponenteDes" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_Componente %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlComponenteDes" runat="server"
                                    Width="240px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtComponente" runat="server" MaxLength="255" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_TipoRelacion %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoRelacionDes" runat="server"
                                    Width="240px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblActivoDes" runat="server" SkinID="LabelCampo" Text="<%$ Resources:Descuentos, Descuentos_Activo %>" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivoDes" runat="server" Checked="true" />
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
                                    ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                            </td>
                            <td>
                                <asp:Label ID="lblCrear" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImgNuevo"
                                    OnClick="ImgNuevo_Click"
                                    runat="server"
                                    ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                                    ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <fieldset id="fsResultado" runat="server" visible="false">
                        <legend>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
                        </legend>
                        <div id="divGrilla" style="overflow:scroll; max-height:235px; max-width:965px;">
                            <br />
                            <uc2:UCPaginacion runat="server" ID="pagControl" />
                            
                            <asp:GridView ID="grvDescuentos" runat="server" AutoGenerateColumns="False" CssClass="AspNet-GridView"
                                AllowSorting="false"
                                EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>" OnRowCommand="GrvDescuentos_RowCommand"
                                DataKeyNames="Id" OnRowEditing="GrvDescuentos_RowEditing">

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
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_IdDescuento %>" DataField="Id" SortExpression="Id" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_TipoProducto %>" DataField="NombreTipoProducto" SortExpression="NombreTipoProducto" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_GrupoProducto %>" DataField="NombreGrupoProducto" SortExpression="NombreGrupoProducto" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_Producto %>" DataField="NombreProducto" SortExpression="NombreProducto" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_Componente %>" DataField="Componente" SortExpression="Componente" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_TipoRelacion %>" DataField="TipoRelacion" SortExpression="TipoRelacion" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_FechaInicial %>"
                                        DataField="FechaInicial"
                                        ItemStyle-CssClass="Centrado"
                                        DataFormatString="{0:d}"
                                        SortExpression="FechaInicial" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_FechaFinal %>"
                                        DataField="FechaFinal"
                                        ItemStyle-CssClass="Centrado"
                                        DataFormatString="{0:d}"
                                        SortExpression="FechaFinal" />
                                    <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_ValorDescuentos %>"
                                        DataField="ValorDescuento"
                                        DataFormatString="{0:N}"
                                        ItemStyle-CssClass="Numero"
                                        SortExpression="ValorDescuentos" />
                                    <asp:TemplateField ShowHeader="False">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSeleccionar" runat="server" Text="<%$ Resources:Descuentos, Descuentos_Activo  %>" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivo" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IndicadorActivo")) %>' />
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
            <asp:View ID="vCrearDescuento" runat="server">
                <uc1:UC_CrearDescuentos runat="server" ID="ucDescuentos" />
            </asp:View>
        </asp:MultiView>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grvDescuentos" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
<div id="updateProgressDivDescuentos" style="display: none;" align="center" class="divProgressDiv">
    <img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
    <br />
    <p>
        <h2>Un momento por favor...</h2>
    </p>
</div>
