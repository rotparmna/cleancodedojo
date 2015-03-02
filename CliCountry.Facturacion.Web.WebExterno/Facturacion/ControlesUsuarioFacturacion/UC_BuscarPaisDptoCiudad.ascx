<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_BuscarPaisDptoCiudad.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarPaisDptoCiudad" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx"
    TagPrefix="uc2" TagName="UCPaginacion" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="Label1"
            runat="server" Text="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje"
            runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblPais" runat="server"
                    Text="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Pais %>" />
            </td>
            <td>
                <%--JREYESG-BUG FACTJ03-17/07/2014 Se quita "TextMode="Number""--%>
                <asp:TextBox ID="txtPais"
                    runat="server" Height="18px"
                    Width="270px"
                    MaxLength="40"></asp:TextBox>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblDpto" runat="server"
                    Text="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Departamento %>" />
            </td>
            <td>
                <asp:TextBox ID="txtDepartamento"
                    runat="server" Height="18px"
                    Width="270px" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblCiudad" runat="server"
                    Text="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Ciudad %>" />
            </td>
            <td>
                <asp:TextBox ID="txtCiudad"
                    runat="server" Height="18px"
                    Width="270px" MaxLength="40"></asp:TextBox>
            </td>
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
        <asp:GridView ID="grvInformacion"
            runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
            OnRowCommand="GrvInformacion_RowCommand"
            AllowSorting="false"
            CssClass="AspNet-GridView">
            <Columns>
                <asp:CommandField ButtonType="Image"
                    SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                    ItemStyle-Width="18px"
                    ItemStyle-Height="18px"
                    SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png"
                    ShowSelectButton="true" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, PaisDptoCiudad_CodigoCiudad %>"
                    DataField="Departamento.Ciudad.Codigo"
                    ItemStyle-CssClass="Centrado" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Pais %>"
                    DataField="Nombre" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Departamento %>"
                    DataField="Departamento.Nombre" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, PaisDptoCiudad_Ciudad %>"
                    DataField="Departamento.Ciudad.Nombre" />
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