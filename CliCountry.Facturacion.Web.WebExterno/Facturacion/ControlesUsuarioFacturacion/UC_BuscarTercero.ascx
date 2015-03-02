<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarTercero.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BusquedaTercero" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearTercero.ascx"
    TagPrefix="uc1" TagName="UC_CrearTercero" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc2" TagName="UCPaginacion" %>



<asp:UpdatePanel ID="upBuscarTercero" runat="server">
    <ContentTemplate>

        <asp:MultiView runat="server"
            ID="multi" ActiveViewIndex="0">
            <asp:View runat="server">
                <div id="contenedorControl">
                    <div class="Header">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_Titulo %>"></asp:Label>
                    </div>
                    <div class="Mensaje">
                        <asp:Label ID="lblMensaje" runat="server" />
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label SkinID="LabelCampo" ID="lblNombreTercero" runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_Nombre %>" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreTercero" runat="server" Height="16px" Width="220px" MaxLength="100"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label SkinID="LabelCampo" ID="lblNroDocumento" runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_NumeroDocumento %>" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNroDocumento" runat="server" Height="16px" Width="220px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label SkinID="LabelCampo" ID="lblBuscar" runat="server" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImgBuscar" runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png" OnClick="ImgBuscar_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                            </td>
                            <td>
                                <asp:Label SkinID="LabelCampo" ID="lblCrear" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImgAdmTercero" runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>" ImageUrl="~/App_Themes/SAHI/images/adicionar.png" OnClick="ImgAdmTercero_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <fieldset id="fsResultado" runat="server" visible="false">
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
                    </legend>
                    <div id="divGrilla">
                        <br />
                        <div>
                            <br />
                            <asp:Label ID="pagActual" runat="server" Font-Bold="true">
                    Página: 
                    <%=grvTerceros.PageIndex + 1%>
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

                        </div>

                        <asp:GridView ID="grvTerceros"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                            OnRowCommand="GrvTerceros_RowCommand"
                            DataKeyNames="Id"
                            AllowSorting="false"
                            CssClass="AspNet-GridView"
                            AllowPaging="true"
                            OnPageIndexChanging="GrvTerceros_PageIndexChanging"
                            PageSize="10"
                            PagerSettings-Position="Top" PagerSettings-Visible="true"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageImageUrl="~/App_Themes/SAHI/images/pagPrimera.png"
                            PagerSettings-LastPageImageUrl="~/App_Themes/SAHI/images/pagUltima.png"
                            PagerStyle-Font-Bold="true"
                            PagerStyle-Font-Size="Small"
                            PagerStyle-Font-Underline="true"
                            PagerStyle-CssClass="gridViewPager" OnRowEditing="GrvTerceros_RowEditing">
                            <PagerStyle ForeColor="DarkBlue" BorderStyle="None" CssClass="gridViewPager" HorizontalAlign="Center" Font-Bold="True" BorderColor="Transparent" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" ItemStyle-Width="20px" ItemStyle-Height="20px" SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ShowSelectButton="true">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:CommandField>
                                <asp:CommandField ButtonType="Image" SelectText="Editar" ItemStyle-Width="20px" ItemStyle-Height="20px" EditImageUrl="~/App_Themes/SAHI/images/editar.png" ShowEditButton="true">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:CommandField>
                                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Tercero_IdTercero %>" DataField="Id" SortExpression="Id" ItemStyle-CssClass="Numero">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Tercero_Nombre %>" DataField="Nombre" SortExpression="Nombre">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Tipo de Documento" DataField="TipoDocumento" SortExpression="TipoDocumento">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Tercero_NumeroDocumento %>" DataField="NumeroDocumento" ItemStyle-Wrap="true" SortExpression="NumeroDocumento" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="Centrado"></ItemStyle>
                                </asp:BoundField>
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
            </asp:View>
            <asp:View runat="server">
                <uc1:UC_CrearTercero
                    runat="server" ID="ucCrearTercero" />
            </asp:View>
        </asp:MultiView>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grvTerceros" EventName="RowCommand" />

    </Triggers>
</asp:UpdatePanel>
<AspAjax:UpdatePanelAnimationExtender ID="upae" BehaviorID="animationTercero" runat="server" TargetControlID="upBuscarTercero">
    <Animations>
                    <OnUpdating>
                        <Parallel duration="0">
                            <%-- place the update progress div over the gridview control --%>
                            <ScriptAction Script="onUpdating('updateProgressDiv1','this.grvTerceros.ClientID');" />  
                            <%-- disable the search button --%>                       
                            <EnableAction AnimationTarget="btnSearch" Enabled="false" />
                            <%-- fade-out the GridView --%>
                            <FadeOut minimumOpacity=".5" />
                         </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <%-- fade back in the GridView --%>
                            <FadeIn minimumOpacity=".5" />
                            <%-- re-enable the search button --%>  
                            <EnableAction AnimationTarget="btnSearch" Enabled="true" />
                            <%--find the update progress div and place it over the gridview control--%>
                            <ScriptAction Script="onUpdated('updateProgressDiv1');" /> 
                        </Parallel> 
                    </OnUpdated>
    </Animations>
</AspAjax:UpdatePanelAnimationExtender>
<div id="updateProgressDiv1" style="display: none;" align="center" class="divProgressDiv">

    <img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
    <br />
    <p>
        <h2>Un momento por favor...</h2>
    </p>
</div>
