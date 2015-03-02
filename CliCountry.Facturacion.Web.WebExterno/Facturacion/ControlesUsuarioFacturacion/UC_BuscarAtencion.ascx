<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarAtencion.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarAtencion" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="Label1" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:ControlesUsuario, Atencion_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="lblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblNroAtencion" runat="server" Text="<%$ Resources:ControlesUsuario, Atencion_IdAtencion %>" />
            </td>
            <td>
                <asp:TextBox ID="txtIdAtencion" runat="server" Height="18px" MaxLength="8" TextMode="Number" Width="170px"></asp:TextBox>
                <AspAjax:FilteredTextBoxExtender ID="ftbIdAtencion" runat="server" FilterType="Numbers" TargetControlID="txtIdAtencion" />
            </td>
            <td>
                <asp:Label ID="lblNroDocumento" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Atencion_NroIdentificacion %>" />
            </td>
            <td style="width: 150px;">
                <asp:TextBox ID="txtNroDocumento" runat="server" Height="18px" MaxLength="20" Width="170px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApellidos" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Atencion_Apellidos %>" />
            </td>
            <td>
                <asp:TextBox ID="txtApellidos" runat="server" Height="18px" MaxLength="50" Width="170px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblAtenciones" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Atencion_Nombres %>" />
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Height="18px" MaxLength="50" Width="170px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px;">
                <asp:Label ID="lblTipoAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Atencion_TipoAtencion %>" />
            </td>
            <td>
                <asp:TextBox ID="txtTipoAtencion" runat="server" Height="18px" MaxLength="50" Width="170px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario, Atencion_Activo %>" />
            </td>
            <td>
                <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td style="width: 100px;">
                <asp:Label ID="lblBuscar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td>
                <asp:ImageButton ID="ImgBuscar" runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                    OnClick="ImgBuscar_Click" TabIndex="6" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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
        <uc2:UCPaginacion runat="server" ID="pagControl" />
        <asp:GridView ID="grvAtenciones" runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
            DataKeyNames="IdAtencion" OnRowCommand="GrvAtenciones_RowCommand"
            AllowSorting="false" CssClass="AspNet-GridView">
            <Columns>
                <asp:CommandField ButtonType="Image" SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>" ItemStyle-Width="18px" ItemStyle-Height="18px" SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ShowSelectButton="true" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Atencion_IdAtencion %>" DataField="IdAtencion"
                    SortExpression="IdAtencion"
                    ItemStyle-CssClass="Numero" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Atencion_NroIdentificacion %>" DataField="NumeroDocumento" ItemStyle-Wrap="true" SortExpression="NumeroDocumento" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Atencion_Paciente %>" DataField="Paciente" ItemStyle-Wrap="true" SortExpression="Paciente" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Atencion_TipoAtencion %>" DataField="DescripcionTipoAtencion" ItemStyle-Wrap="true" SortExpression="DescripcionTipoAtencion" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
<div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif" 
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
    </div>