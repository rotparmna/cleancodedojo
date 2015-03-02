<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarCondicionesFacturacion.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesFacturacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCondicionesFacturacion.ascx" TagPrefix="uc1" TagName="UC_CrearCondicionesFacturacion" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<asp:UpdatePanel ID="upBuscarCondicionesFacturacion" runat="server">
<ContentTemplate>

<asp:MultiView ID="mltvCondicionesFacturacion" runat="server" ActiveViewIndex="0">
    <asp:View ID="vCondicionesFacturacion" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_Titulo %>"></asp:Label>
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
                        <asp:TextBox ID="txtEntidadDC" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtContratoDC" runat="server" Enabled="false" ReadOnly="true" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlanDC" runat="server" Enabled="false" MaxLength="85" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
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
                        <asp:Label SkinID="LabelCampo" ID="lblTiporelacionCF" runat="server" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_TipoRelacion %>" Width="120px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion" runat="server"
                            Width="240px" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label8" runat="server" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_CActivo %>" Width="120px" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivoCF" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBuscar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_Buscar %>" Width="120px" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgBuscar" runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscar_Click"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonConsultar %>" />
                    </td>
                    <td>
                        <asp:Label ID="lblCrear" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" Width="120px" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgNuevo" runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                            OnClick="ImgNuevo_Click"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <fieldset id="fsResultado" runat="server" visible="false">
            <legend>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
            </legend>
            <div id="divGrilla" runat="server">
                <br />
                <%--<uc2:UCPaginacion runat="server" ID="pagControl" />--%>
                <br />
                <asp:Label ID="pagActual" runat="server" Font-Bold="true">
                        Página: 
                        <%=grvCondicionesFacturacion.PageIndex + 1%>
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
                <asp:GridView ID="grvCondicionesFacturacion" runat="server" AutoGenerateColumns="False"
                    CssClass="AspNet-GridView" AllowSorting="false" DataKeyNames="Id"
                    EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>" OnRowCommand="GrvCondicionesFacturacion_RowCommand"
                    OnRowEditing="GrvCondicionesFacturacion_RowEditing"
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
                    OnPageIndexChanging="GrvCondicionesFacturacion_PageIndexChanging"
                    >

                    <PagerStyle ForeColor="DarkBlue" BorderStyle="None" CssClass="gridViewPager" HorizontalAlign="Center" Font-Bold="True" BorderColor="Transparent" />

                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSeleccionar" runat="server" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Select" ImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ToolTip="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" />
                            </ItemTemplate>
                            <ItemStyle Height="16px" Width="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="<%$ Resources:GlobalWeb, ComandoModificar %>">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Edit" ImageUrl="~/App_Themes/SAHI/images/editar.png" ToolTip="<%$ Resources:GlobalWeb, ComandoModificar %>" />
                            </ItemTemplate>
                            <ItemStyle Height="16px" Width="16px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Plan" DataField="NombrePlan" SortExpression="NombrePlan" />
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesFacturacion, FacActividad_GrvCondicionesFacturacion_IdAtencion %>" DataField="IdAtencion" SortExpression="IdAtencion" />
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesFacturacion, FacActividad_GrvCondicionesFacturacion_TipoRelacion %>" DataField="TipoRelacion" SortExpression="TipoRelacion" />
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesFacturacion, FacActividad_GrvCondicionesFacturacion_Valor %>" DataField="ValorPropio" DataFormatString="{0:c}" SortExpression="ValorPropio" />
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesFacturacion, FacActividad_GrvCondicionesFacturacion_Vigencia %>"
                            DataField="VigenciaCondicion"
                            ItemStyle-CssClass="Centrado"
                            DataFormatString="{0:d}"
                            SortExpression="VigenciaCondicion" />
                        <asp:BoundField HeaderText="<%$ Resources:CondicionesFacturacion, FacActividad_GrvCondicionesFacturacion_Descripcion %>" DataField="DescripcionCondicion" SortExpression="DescripcionCondicion" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblActivo" runat="server" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_CActivo %>" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActivo" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IndHabilitado")) %>' CssClass="chkSeleccionar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
        <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
            </div>
    </asp:View>
    <asp:View ID="vCrearCondicionesFact" runat="server">
        <uc1:UC_CrearCondicionesFacturacion runat="server" ID="ucCondicionesFacturacion" />
    </asp:View>
</asp:MultiView>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grvCondicionesFacturacion" EventName="RowCommand" />                      
</Triggers> 
</asp:UpdatePanel>

<div id="updateProgressDivCondicionesFacturacion" style="display: none;" align="center" class="divProgressDiv">

<img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
<br/>
        <p><h2>Un momento por favor...</h2></p>
</div>