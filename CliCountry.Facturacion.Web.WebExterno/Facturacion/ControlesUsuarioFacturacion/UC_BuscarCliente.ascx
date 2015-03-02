<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarCliente.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCliente" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCliente.ascx"
    TagPrefix="uc2" TagName="UC_CrearCliente" %>

<asp:MultiView runat="server"
    ID="multi" ActiveViewIndex="0">
    <asp:View ID="View1"
        runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="Label1"
                    runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Titulo %>"></asp:Label>
            </div>
            
            <div class="Mensaje">
                <asp:Label ID="lblMensaje"
                    runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblNroAtencion"
                            runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_IdAtencion %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAtencion"
                            runat="server" Height="18px"
                            Width="220px" TextMode="Number"
                            MaxLength="8"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            runat="server" ID="ftbIdAtencion"
                            TargetControlID="txtIdAtencion"
                            FilterType="Numbers" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblIDCliente"
                            runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Id %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdCliente"
                            runat="server" Height="18px"
                            Width="220px" MaxLength="8"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            runat="server" ID="ftbIdCliente"
                            TargetControlID="txtIdCliente"
                            FilterType="Numbers" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblNroDocumento"
                            runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_NroDocumento %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNroDocumento"
                            runat="server" Height="18px"
                            Width="220px" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblApellidos"
                            runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Apellidos %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellidos"
                            runat="server" Height="18px"
                            Width="220px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblClientes"
                            runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Nombres %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre"
                            runat="server" Height="18px"
                            Width="220px" MaxLength="50"></asp:TextBox>
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
                        <asp:ImageButton ID="ImgAdicionarCliente"
                            runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>"
                            ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                            OnClick="ImgAdicionarCliente_Click" />
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
                    ID="pagControl" />
                <asp:GridView ID="grvClientes"
                    runat="server" AutoGenerateColumns="False"
                    EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
                    DataKeyNames="IdCliente"
                    OnRowCommand="GrvClientes_RowCommand"
                    AllowSorting="false"
                    CssClass="AspNet-GridView">
                    <Columns>
                        <asp:CommandField ButtonType="Image"
                            SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                            ItemStyle-Width="18px"
                            ItemStyle-Height="18px"
                            SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png"
                            ShowSelectButton="true" />
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Cliente_Id %>"
                            DataField="IdCliente"
                            SortExpression="IdCliente" />
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Cliente_NroDocumento %>"
                            DataField="NumeroDocumento"
                            ItemStyle-Wrap="true"
                            SortExpression="NumeroDocumento"
                            ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Cliente_Paciente %>"
                            DataField="Nombres"
                            ItemStyle-Wrap="true"
                            SortExpression="Nombres"
                            ItemStyle-HorizontalAlign="Left" />
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
    <asp:View ID="View2"
        runat="server">
        <uc2:UC_CrearCliente
            runat="server" ID="ucCrearCliente" />
    </asp:View>
</asp:MultiView>