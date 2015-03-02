<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_BuscarTarifas.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarTarifas" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo" runat="server" Text="<%$ Resources:ControlesUsuario, Manuales_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblIdTarifa" runat="server" Text="<%$ Resources:ControlesUsuario, Manuales_CodigoTarifa %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtIdTarifa" runat="server" MaxLength="8" Width="220px"></asp:TextBox>
                <AspAjax:FilteredTextBoxExtender runat="server" ID="ftbIdTarifa" FilterType="Numbers"
                    TargetControlID="TxtIdTarifa" />
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblNombreManualTarifa" runat="server" Text="<%$ Resources:ControlesUsuario, Manuales_NombreManual %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtNombreManualTarifa" runat="server" MaxLength="30" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblVigenciaTarifa" runat="server" Text="Vigencia Tarifa" />
            </td>
            <td>
                <asp:TextBox ID="TxtVigenciaTarifa" runat="server"></asp:TextBox>
                <asp:Image ID="imgCalendarFechaInicial" runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                <AspAjax:CalendarExtender ID="CalendarExtender1" runat="server"
                    PopupButtonID="imgCalendarFechaInicial"
                    TargetControlID="TxtVigenciaTarifa">
                </AspAjax:CalendarExtender>
                <AspAjax:MaskedEditExtender ID="meeFechaInicial" runat="server"
                    TargetControlID="TxtVigenciaTarifa"
                    Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    InputDirection="RightToLeft"
                    AcceptNegative="None"
                    ErrorTooltipEnabled="True" />
            </td>
            <td>
                <asp:Label SkinID="LabelCampo" ID="lblNombreTarifa" runat="server" Text="<%$ Resources:ControlesUsuario, Manuales_NombreTarifa %>" />
            </td>
            <td>
                <asp:TextBox ID="TxtNombreTarifa" runat="server" MaxLength="30" Width="220px"></asp:TextBox>
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
<fieldset id="fsResultado" runat="server" visible="false">
    <legend>
        <asp:Label ID="lblResultadoBusqueda" runat="server" Text="<%$ Resources:GlobalWeb, General_ResultadoBusqueda %>"></asp:Label>
    </legend>
    <div id="divGrilla">
        <br />
        <uc2:UCPaginacion runat="server" ID="pagControl" />
        <asp:GridView ID="grvManualesTar" runat="server" AutoGenerateColumns="False"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>"
            AllowSorting="false" CssClass="AspNet-GridView"
            OnRowCommand="GrvManualesTar_RowCommand"
            DataKeyNames="CodigoTarifa,NombreManualesTarifa,NombreTarifa,VigenciaTarifa">
            <Columns>
                <asp:CommandField ButtonType="Image" SelectText="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                    ItemStyle-Width="16px"
                    SelectImageUrl="~/App_Themes/SAHI/images/seleccionar.png" ShowSelectButton="true" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Manuales_CodigoTarifa %>"
                    DataField="CodigoTarifa"
                    ItemStyle-CssClass="Centrado"
                    SortExpression="CodigoTarifa" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Manuales_NombreManual %>"
                    DataField="NombreManualesTarifa"
                    SortExpression="NombreManualesTarifa" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Manuales_NombreTarifa %>"
                    DataField="NombreTarifa"
                    SortExpression="NombreTarifa" />
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Manuales_VigenciaTarifa %>"
                    DataField="VigenciaTarifa"
                    ItemStyle-CssClass="Centrado"
                    DataFormatString="{0:d}"
                    SortExpression="VigenciaTarifa" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
<div class="contenedorBotonesPopup">
        <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
    </div>