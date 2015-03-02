<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarProductos.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarProductos" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<asp:UpdatePanel ID="upBuscarProductos" runat="server">
<ContentTemplate>
<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" runat="server" Text="<%$ Resources:ControlesUsuario, Producto_Titulo %>"></asp:Label>
    </div>
    
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblProducto" runat="server" Text="<%$ Resources:ControlesUsuario, Producto_Label_Producto %>" />
            </td>
            <td>
                <asp:TextBox ID="txtProducto" runat="server" MaxLength="255" Height="16px" Width="220px"></asp:TextBox>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblCodigo" runat="server" Text="<%$ Resources:ControlesUsuario, Producto_Codigo %>" />
            </td>
            <td>
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" Height="16px" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblGrupo" runat="server" Text="<%$ Resources:ControlesUsuario, Producto_Grupo %>" />
            </td>
            <td>
                <asp:TextBox ID="txtGrupo" runat="server" MaxLength="50" Height="16px" Width="220px"></asp:TextBox>
            </td>
            <td>
                <asp:Label Width="120px" SkinID="LabelCampo" ID="lblTipoProducto" runat="server" Text="<%$ Resources:ControlesUsuario, Producto_TipoProducto %>" />
            </td>
            <td>
                <asp:TextBox ID="txtTipoProducto" runat="server" MaxLength="30" Height="16px" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblActInformacion" runat="server" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td>
                <asp:ImageButton ID="ImgBuscar"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                    OnClick="ImgBuscar_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
        </tr>
    </table>
</div>
<br />
<br />
<fieldset id="fsResultado" runat="server" visible="false" style="text-align: center">
    <legend>
        <asp:Label ID="lblResultadoBusqueda" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
    </legend>
    <div id="divGrilla">
        <br />
        <%--<uc2:UCPaginacion runat="server" ID="pagControl" />--%>

         <br />
        <asp:Label ID="pagActual" runat="server" Font-Bold="true">
                Página: 
                <%=grvProductos.PageIndex + 1%>
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

        <asp:GridView ID="grvProductos" runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
            AllowSorting="false" HorizontalAlign="Center" CssClass="AspNet-GridView"
            OnRowCommand="GrvProductos_RowCommand"
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
            OnPageIndexChanging="GrvProductos_PageIndexChanging"
            >

            <PagerStyle ForeColor="DarkBlue" BorderStyle="None" CssClass="gridViewPager" HorizontalAlign="Center" Font-Bold="True" BorderColor="Transparent" />

            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgSeleccionar" runat="server" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Select" ImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ToolTip="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" />
                    </ItemTemplate>
                    <ItemStyle Height="16px" Width="16px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Producto_IdProducto %>" DataField="Producto.IdProducto" SortExpression="Producto.IdProducto" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Producto_Codigo %>" DataField="Producto.CodigoProducto" SortExpression="Producto.CodigoProducto" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Producto_NombreProducto %>" DataField="Producto.Nombre" ItemStyle-Wrap="true" SortExpression="Producto.Nombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Producto_Grupo %>" DataField="GrupoProducto.Nombre" SortExpression="GrupoProducto.Nombre" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Producto_TipoProducto %>" DataField="Nombre" SortExpression="Nombre" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
    <div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
    </div>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grvProductos" EventName="RowCommand" />                      
</Triggers> 
</asp:UpdatePanel>
<AspAjax:UpdatePanelAnimationExtender ID="upaeBuscarProductos" BehaviorID="animationProductos" runat="server" TargetControlID="upBuscarProductos">
<Animations>
    <OnUpdating>
        <Parallel duration="0">
            <%-- place the update progress div over the gridview control --%>
            <ScriptAction Script="onUpdating('updateProgressDivProductos','this.grvProductos.ClientID');" />  
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
            <ScriptAction Script="onUpdated('updateProgressDivProductos');" /> 
        </Parallel> 
    </OnUpdated>
</Animations>
</AspAjax:UpdatePanelAnimationExtender>
<div id="updateProgressDivProductos" style="display: none;" align="center" class="divProgressDiv">

<img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
<br/>
        <p><h2>Un momento por favor...</h2></p>
</div>