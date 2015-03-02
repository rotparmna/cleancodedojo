<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarExclusiones.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarExclusiones" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearExclusion.ascx"
    TagPrefix="uc2" TagName="UC_CrearExclusion" %>

<asp:MultiView ID="mltvDE"
    runat="server" ActiveViewIndex="0">
    <asp:View ID="vConsultaDE"
        runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo"
                    CssClass="LabelTitulo"
                    runat="server" Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Titulo %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje"
                    runat="server" />
            </div>
            <asp:UpdatePanel ID="upCondicionesCubrimientoInput"
                runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label SkinID="LabelCampo"
                                    ID="lblEntidad" runat="server"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Entidad %>"></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblIdEntidad"
                                    runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtEntidad"
                                    runat="server" Enabled="false"
                                    Height="16px" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label SkinID="LabelCampo"
                                    ID="lblContrato"
                                    runat="server" Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Contrato %>"></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblIdContrato"
                                    runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtContrato"
                                    runat="server" Enabled="false"
                                    Height="16px" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPlan"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Plan %>"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIdPlan"
                                    runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtPlan"
                                    runat="server" Enabled="false"
                                    Height="16px" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblIdAtencion"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_IdAtencion %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdAtencion"
                                    runat="server" Enabled="false"
                                    Height="16px" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoProducto"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_TipoProducto %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTipoProducto"
                                    runat="server" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblGrupoProducto"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_GrupoProducto %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrupoProducto"
                                    runat="server" Width="270px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProducto"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Producto %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProducto"
                                    runat="server" Width="270px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblComponente"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Componente %>"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlComponente"
                                    runat="server"
                                    Width="240px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblActivo0"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Activo %>"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivo"
                                    runat="server" Checked="true" />
                            </td>
                            <td>
                                <asp:Label ID="lblTiporelacion"
                                    runat="server" SkinID="LabelCampo"
                                    Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_TipoRelacion %>"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlTipoRelacion"
                                    runat="server"
                                    Width="240px"
                                    Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label SkinID="LabelCampo"
                                    ID="lblBuscar" runat="server"
                                    Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="ImgBuscar"
                                    runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                                    OnClick="ImgBuscar_Click"
                                    ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                            </td>
                            <td align="left">
                                <asp:Label SkinID="LabelCampo"
                                    ID="lblCrear" runat="server"
                                    Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="ImgAdmExclusion"
                                    runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>"
                                    OnClick="ImgAdmExclusion_Click"
                                    ImageUrl="~/App_Themes/SAHI/images/adicionar.png" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <fieldset id="fsResultado"
            runat="server" visible="false"
            style="text-align: center">
            <legend>
                <asp:Label ID="lblResultadoBusqueda"
                    runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
            </legend>
            <div id="divGrilla">
                <br />
                <uc2:UCPaginacion runat="server"
                    ID="pagControlExclusiones" />
                <asp:GridView ID="grvExclusiones"
                    runat="server" AutoGenerateColumns="False"
                    EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                    DataKeyNames="Id"
                    CssClass="AspNet-GridView"
                    AllowSorting="false"
                    OnRowCommand="GrvExclusiones_RowCommand"
                    OnRowDataBound="GrvExclusiones_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbSelect"
                                    runat="server" CausesValidation="False"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CommandName="Select"
                                    ImageUrl="~/App_Themes/SAHI/images/editar.png"
                                    Text="Select" ToolTip="Consultar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <HeaderTemplate>
                                <asp:Label ID="lblSeleccionar"
                                    runat="server" Text="Activo" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActivo"
                                    runat="server" Checked='<%# Eval("IndicadorContratoAplicado").Equals("1") ? true : false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdExclusion"
                                    runat="server" Text='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdTipoProducto"
                                    runat="server" Text='<%# Eval("TipoProducto.IdTipoProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_TipoProducto %>"
                            DataField="TipoProducto.Nombre" />
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdGrupoProducto"
                                    runat="server" Text='<%# Eval("GrupoProducto.IdGrupo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_GrupoProducto %>"
                            DataField="GrupoProducto.Nombre" />
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProducto"
                                    runat="server" Text='<%# Eval("Producto.IdProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Producto %>"
                            DataField="Producto.Nombre" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Componente %>"
                            DataField="Componente" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Manual %>"
                            DataField="NombreManual" />
                        <asp:BoundField HeaderText="<%$ Resources:DefinirExclusiones, DefinirExclusiones_VigenciaTarifa %>"
                            DataField="VigenciaTarifa"
                            ItemStyle-CssClass="Centrado"
                            DataFormatString="{0:d}" />
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdVenta"
                                    runat="server" Text='<%# Eval("IdVenta") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblNumeroVenta"
                                    runat="server" Text='<%# Eval("NumeroVenta") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdManual"
                                    runat="server" Text='<%# Eval("IdManual") %>' />
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
    <asp:View ID="vCrearModificarDE"
        runat="server">
        <uc2:UC_CrearExclusion
            runat="server" ID="ucCrearExclusion" />
    </asp:View>
</asp:MultiView>