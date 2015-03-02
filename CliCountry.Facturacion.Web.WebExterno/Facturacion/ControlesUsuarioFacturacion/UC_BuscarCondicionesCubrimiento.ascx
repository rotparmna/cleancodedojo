<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarCondicionesCubrimiento.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesCubrimiento" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc1" TagName="UC_Paginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCondicionCubrimiento.ascx"
    TagPrefix="uc1" TagName="UC_CrearCondicionCubrimiento" %>

<asp:UpdatePanel ID="upBuscarCondicionesCubrimiento" runat="server">
<ContentTemplate>

<asp:MultiView ID="mltvCC"
    runat="server" ActiveViewIndex="0">
    <asp:View ID="vConsultaCC"
        runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo"
                    CssClass="LabelTitulo"
                    runat="server" Text="<%$ Resources:Descuentos, Descuentos_TituloCondicionesCubrimientos %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje"
                    runat="server" Visible="false" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblEntidad" runat="server"
                            Text="<%$ Resources:Descuentos, Descuentos_Entidad %>"></asp:Label>
                        &nbsp;:</td>
                    <td>
                        <asp:Label ID="lblIdEntidad"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtEntidad"
                            runat="server" Width="250px"
                            ReadOnly="True" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblFechaFinal"
                            runat="server" Text="<%$ Resources:Descuentos, Descuentos_Contrato %>"></asp:Label>
                        &nbsp;:</td>
                    <td>
                        <asp:Label ID="lblIdContrato"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtContrato"
                            runat="server" Width="250px"
                            ReadOnly="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblPlan"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Plan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblIdPlan"
                            runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtPlan"
                            runat="server" Width="250px"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIdAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_IAtencion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAtencion"
                            runat="server" Width="250px"
                            Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="fteIdAtencion"
                            runat="server" TargetControlID="txtIdAtencion"
                            FilterType="Numbers" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTiporelacion0"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_TipoRelacion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblComponente"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Componente %>"></asp:Label>
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
                            Text="<%$ Resources:Descuentos, Descuentos_Activo %>"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo"
                            runat="server" Checked="true" />
                    </td>
                    <td>
                        <asp:Label ID="lblCubrimiento"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Cubrimiento %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlClaseCubrimiento"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="Label1" runat="server"
                            Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnConsultar"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBtnConsultar_Click" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="Label2" runat="server"
                            Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnAdicionar"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>"
                            OnClick="ImgBtnAdicionar_Click" />
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
            <div id="divGrilla" runat="server">
                <br />
<%--                <uc1:UC_Paginacion runat="server"
                    ID="pagControlCondicionCubrimiento" />--%>
                <br />
                <asp:Label ID="pagActual" runat="server" Font-Bold="true">
                        Página: 
                        <%=grvCondicionesCubrimientos.PageIndex + 1%>
                        de
                </asp:Label>
                <asp:Label ID="numPaginas" runat="server" Font-Bold="true">            
                </asp:Label>
                &nbsp;
                <asp:Label ID="lblTotalRegistros" runat="server" Font-Bold="true">
                    Total Registros: 
                </asp:Label>
                <asp:Label ID="lblNumRegistros" runat="server" Text="0" Font-Bold="true">              
                </asp:Label>


                <asp:GridView ID="grvCondicionesCubrimientos"
                    runat="server" AutoGenerateColumns="False"
                    CssClass="AspNet-GridView"
                    AllowSorting="false"
                    EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                    OnRowCommand="GrvCondicionesCubrimientos_RowCommand"
                    OnRowEditing="GrvCondicionesCubrimientos_RowEditing"
                    DataKeyNames="Id"
                    AllowPaging="true" 
                    PageSize="10" 
                    PagerSettings-Position="Top" PagerSettings-Visible="true" 
                    PagerSettings-Mode="NumericFirstLast"
                    PagerSettings-FirstPageImageUrl="~/App_Themes/SAHI/images/pagPrimera.png"
                    PagerSettings-LastPageImageUrl="~/App_Themes/SAHI/images/pagUltima.png"
                    PagerStyle-Font-Bold="true"
                    PagerStyle-Font-Size="Small"
                    PagerStyle-Font-Underline="true" 
                    PagerStyle-CssClass="gridViewPager"  
                    OnPageIndexChanging="GrvCondicionesCubrimientos_PageIndexChanging"
                    >

                    <PagerStyle ForeColor="DarkBlue" BorderStyle="None" CssClass="gridViewPager" HorizontalAlign="Center" Font-Bold="True" BorderColor="Transparent" />

                    <Columns>
                        <asp:TemplateField ShowHeader="False"
                            HeaderText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSeleccionar"
                                    runat="server" CausesValidation="False"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CommandName="Select"
                                    ImageUrl="~/App_Themes/SAHI/images/seleccionar.png"
                                    ToolTip="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" />
                            </ItemTemplate>
                            <ItemStyle Height="16px"
                                Width="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False"
                            HeaderText="<%$ Resources:GlobalWeb, ComandoModificar %>">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar"
                                    runat="server" CausesValidation="False"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CommandName="Edit"
                                    ImageUrl="~/App_Themes/SAHI/images/editar.png"
                                    ToolTip="<%$ Resources:GlobalWeb, ComandoModificar %>" />
                            </ItemTemplate>
                            <ItemStyle Height="16px"
                                Width="16px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_IdCondicionCubrimiento %>"
                            DataField="Id" SortExpression="Id"
                            ItemStyle-CssClass="Numero" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdTipoRelacion"
                                    runat="server" Text='<%# Eval("NumeroTipoRelacion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_TipoRelacion %>"
                            DataField="NombreTipoRelacion"
                            SortExpression="NombreTipoRelacion"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdClaseCubrimiento"
                                    runat="server" Text='<%# Eval("Cubrimiento.ClaseCubrimiento.IdClaseCubrimiento") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_Cubrimiento %>"
                            DataField="Cubrimiento.ClaseCubrimiento.Nombre"
                            SortExpression="Cubrimiento.ClaseCubrimiento.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:Descuentos, Descuentos_Producto %>"
                            DataField="Cubrimiento.Producto.Nombre"
                            SortExpression="Cubrimiento.Producto.Nombre"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:Descuentos, CondicionesCubrimientos_Valor %>"
                            DataField="ValorPropio"
                            SortExpression="ValorPropio"
                            DataFormatString="{0:N2}"
                            ItemStyle-CssClass="Numero" />
                        <asp:BoundField HeaderText="<%$ Resources:Descuentos, CondicionesCubrimientos_Vigencia %>"
                            DataField="VigenciaCondicion"
                            ItemStyle-CssClass="Centrado"
                            DataFormatString="{0:d}"
                            SortExpression="VigenciaCondicion" />
                        <asp:BoundField HeaderText="<%$ Resources:Descuentos, CondicionesCubrimientos_Descripcion %>"
                            DataField="DescripcionCondicion"
                            SortExpression="DescripcionCondicion"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
        <div class="contenedorBotonesPopup">
                <asp:ImageButton ID="imgBtnSalir"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" />
            </div>
    </asp:View>
    <asp:View ID="vCrearModificarCC"
        runat="server">
        <uc1:UC_CrearCondicionCubrimiento
            runat="server" ID="ucCrearCondicionCubrimiento" />
    </asp:View>
</asp:MultiView>

</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grvCondicionesCubrimientos" EventName="RowCommand" />                      
</Triggers> 
</asp:UpdatePanel>
<div id="updateProgressDivCondicionesCubrimiento" style="display: none;" align="center" class="divProgressDiv">

<img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
<br/>
        <p><h2>Un momento por favor...</h2></p>
</div>