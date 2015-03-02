<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarGrupoProductos.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarGrupoProductos" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<asp:UpdatePanel ID="upBuscarGrupoProductos" runat="server">
<ContentTemplate>
<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:ControlesUsuario, GrupoProducto_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblCodigo" runat="server" Text="<%$ Resources:ControlesUsuario, GrupoProducto_Codigo %>" />
            </td>
            <td>
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="8" Height="16px" Width="220px"></asp:TextBox>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblGrupo" runat="server" Text="<%$ Resources:ControlesUsuario, GrupoProducto_Grupo %>" />
            </td>
            <td>
                <asp:TextBox ID="txtGrupo" runat="server" MaxLength="50" Height="16px" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblActInformacion" runat="server" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td colspan="3">
                <asp:ImageButton ID="ImgBuscar"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                    OnClick="ImgBuscar_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
        </tr>
    </table>
</div>
<br />
<br />
<fieldset id="fsResultado" runat="server" visible="false">
    <legend>
        <asp:Label ID="lblResultadoBusqueda" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
    </legend>
    <div id="divGrilla">
        <br />
        <%--<uc2:UCPaginacion runat="server" ID="pagControl" />--%>
         <br />
        <asp:Label ID="pagActual" runat="server" Font-Bold="true">
                Página: 
                <%=grvGrupoProductos.PageIndex + 1%>
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

        <asp:GridView ID="grvGrupoProductos" runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
            AllowSorting="false" CssClass="AspNet-GridView"
            DataKeyNames="IdGrupo"
            OnRowCommand="GrvGrupoProductos_RowCommand"
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
            OnPageIndexChanging="GrvGrupoProductos_PageIndexChanging"
            >

            <PagerStyle ForeColor="DarkBlue" BorderStyle="None" CssClass="gridViewPager" HorizontalAlign="Center" Font-Bold="True" BorderColor="Transparent" />

            <Columns>
                <asp:CommandField ButtonType="Image" SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" ItemStyle-Width="16px" ItemStyle-Height="16px" SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ShowSelectButton="true" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, GrupoProducto_Codigo %>" DataField="CodigoGrupo" SortExpression="CodigoGrupo" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, GrupoProducto_Grupo %>" DataField="Nombre" SortExpression="Nombre" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
    <div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click"  />
    </div>
    </ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grvGrupoProductos" EventName="RowCommand" />                      
</Triggers> 
</asp:UpdatePanel>
<AspAjax:UpdatePanelAnimationExtender ID="upaeBuscarGrupoProductos" BehaviorID="animationGrupoProductos" runat="server" TargetControlID="upBuscarGrupoProductos">
<Animations>
    <OnUpdating>
        <Parallel duration="0">
            <%-- place the update progress div over the gridview control --%>
            <ScriptAction Script="onUpdating('updateProgressDivGrupoProductos','this.grvGrupoProductos.ClientID');" />  
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
            <ScriptAction Script="onUpdated('updateProgressDivGrupoProductos');" /> 
        </Parallel> 
    </OnUpdated>
</Animations>
</AspAjax:UpdatePanelAnimationExtender>
<div id="updateProgressDivGrupoProductos" style="display: none;" align="center" class="divProgressDiv">

<img alt='Espere por favor...' src='../App_Themes/SAHI/images/loading.gif' />
<br/>
        <p><h2>Un momento por favor...</h2></p>
</div>
