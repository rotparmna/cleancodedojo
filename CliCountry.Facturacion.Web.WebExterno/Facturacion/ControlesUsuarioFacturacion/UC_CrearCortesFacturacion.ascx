<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearCortesFacturacion.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCortesFacturacion" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="lblTitulo"
            runat="server" Text="<%$ Resources:CortesFacturacion, CortesFacturacion_Titulo %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="LblMensaje"
            runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblFechaInicial"
                    runat="server" Text="<%$ Resources:CortesFacturacion, CortesFacturacion_FechaDesde %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicial"
                    runat="server"></asp:TextBox>
                <asp:Image ID="imgCalendar1"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                <AspAjax:CalendarExtender
                    ID="CalendarExtender1"
                    runat="server" PopupButtonID="imgCalendar1"
                    TargetControlID="txtFechaInicial">
                </AspAjax:CalendarExtender>
                <AspAjax:MaskedEditExtender
                    ID="meeFechaInicial"
                    runat="server" TargetControlID="txtFechaInicial"
                    Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    InputDirection="RightToLeft"
                    AcceptNegative="None"
                    ClearMaskOnLostFocus="false"
                    ClearTextOnInvalid="true"
                    ErrorTooltipEnabled="True" />
                <asp:RegularExpressionValidator
                    ID="revFechaInicial"
                    runat="server" ForeColor="Red"
                    ControlToValidate="txtFechaInicial"
                    ValidationGroup="ValidarCampos"
                    ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha3 %>"
                    ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularFecha %>">*</asp:RegularExpressionValidator>
                <asp:CompareValidator
                    ID="cvFechasFacturacion"
                    runat="server" ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha1 %>"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFecha1 %>"
                    Operator="LessThanEqual"
                    Type="Date" ControlToValidate="txtFechaInicial"
                    ForeColor="Red"
                    ControlToCompare="txtFechaFinal"
                    ValidationGroup="ValidarCampos">*</asp:CompareValidator>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblFechaFinal"
                    runat="server" Text="<%$ Resources:CortesFacturacion, CortesFacturacion_FechaHasta %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFinal"
                    runat="server"></asp:TextBox>
                <asp:Image ID="imgCalendar2"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                <AspAjax:CalendarExtender
                    ID="CalendarExtender2"
                    runat="server" PopupButtonID="imgCalendar2"
                    TargetControlID="txtFechaFinal">
                </AspAjax:CalendarExtender>
                <AspAjax:MaskedEditExtender
                    ID="meeFechaFinal"
                    runat="server" TargetControlID="txtFechaFinal"
                    Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    InputDirection="RightToLeft"
                    AcceptNegative="None"
                    ClearMaskOnLostFocus="false"
                    ClearTextOnInvalid="true"
                    ErrorTooltipEnabled="True" />
                <asp:RegularExpressionValidator
                    ID="revFechaFinal"
                    runat="server" ForeColor="Red"
                    ControlToValidate="txtFechaFinal"
                    ValidationGroup="ValidarCampos"
                    ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha3 %>"
                    ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularFecha %>">*</asp:RegularExpressionValidator>
                <asp:CompareValidator
                    ID="cvFechasFacturacion1"
                    runat="server" ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha4 %>"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFecha4 %>"
                    Operator="GreaterThanEqual"
                    Type="Date" ControlToValidate="txtFechaFinal"
                    ForeColor="Red"
                    ControlToCompare="txtFechaInicial"
                    ValidationGroup="ValidarCampos">*</asp:CompareValidator>
                <asp:RangeValidator ID="rvFechaFinal"
                    runat="server" ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha2 %>"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFecha2 %>"
                    ControlToValidate="txtFechaFinal"
                    ForeColor="Red"
                    Type="Date" MinimumValue="<%$ Resources:GlobalWeb, General_ValorDefectoFecha %>"
                    ValidationGroup="ValidarCampos">*</asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 30px;">
                <asp:Label SkinID="LabelCampo"
                    ID="lblCrear" runat="server"
                    Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
            </td>
            <td>
                <asp:ImageButton ID="imgBtnAdicionar"
                    ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
                    runat="server"
                    AlternateText="<%$ Resources:CortesFacturacion, CortesFacturacion_BtnAdicionar %>"
                    ValidationGroup="ValidarCampos"
                    ToolTip="<%$ Resources:CortesFacturacion, CortesFacturacion_BtnAdicionarToolTip %>"
                    OnClick="ImgBtnAdicionar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblErrorFecha"
                    runat="server" Text="<%$ Resources:GlobalWeb, General_FechaGrillaCortesFacturacion %>"
                    ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<br />
<br />
<fieldset id="fsResultado"
    runat="server">
    <legend>
        <asp:Label ID="Label6"
            runat="server" Text="<%$ Resources:CortesFacturacion, CortesFacturacion_TituloGrilla %>"></asp:Label>
    </legend>
    <div id="divGrilla">
        <br />
        <asp:GridView ID="grvCortesFacturacion"
            runat="server" AutoGenerateColumns="False"
            CssClass="AspNet-GridView"
            AllowSorting="false"
            OnRowDeleting="GrvCortesFacturacion_RowDeleting">
            <Columns>
                <asp:TemplateField ShowHeader="False"
                    ItemStyle-CssClass="Centrado">
                    <HeaderTemplate>
                        <asp:Label ID="lblSeleccionar"
                            runat="server" Text="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvChkSeleccionar %>" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSeleccionar"
                            runat="server" Checked="true"
                            ToolTip="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvBtnSeleccionarToolTip %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ItemStyle-CssClass="Centrado"
                    DeleteImageUrl="~/App_Themes/SAHI/images/eliminar.png"
                    ShowDeleteButton="true"
                    DeleteText="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvLblEliminar %>"
                    ButtonType="Image"
                    HeaderText="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvLblEliminar %>"
                    ShowHeader="true"></asp:CommandField>
                <asp:BoundField
                    ItemStyle-CssClass="Centrado"
                    HeaderText="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvFechaInicial %>"
                    DataField="FechaInicial"
                    DataFormatString="{0:d}"
                    SortExpression="FechaInicial" />
                <asp:BoundField
                    ItemStyle-CssClass="Centrado"
                    HeaderText="<%$ Resources:CortesFacturacion, CortesFacturacion_GrvFechaFinal %>"
                    DataField="FechaFinal"
                    DataFormatString="{0:d}"
                    SortExpression="FechaFinal" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>