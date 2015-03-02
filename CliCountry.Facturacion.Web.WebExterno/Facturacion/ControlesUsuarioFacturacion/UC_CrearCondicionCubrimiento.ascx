<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearCondicionCubrimiento.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCondicionCubrimiento" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarAtencion.ascx"
    TagPrefix="uc1" TagName="UC_BuscarAtencion" %>

<asp:MultiView runat="server"
    ID="mlvCondicionCubrimiento"
    ActiveViewIndex="0">
    <asp:View ID="vCrearModificar"
        runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo"
                    CssClass="LabelTitulo"
                    runat="server"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="lblMensaje"
                    runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblActiva" runat="server"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Activo %>" />
                    </td>
                    <td>
                        <input type="checkbox"
                            id="chkActivo" runat="server"
                            style="height: 20px; width: 20px" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblIdentificador"
                            runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Identificador %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificador"
                            runat="server" ReadOnly="true"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblEntidad" runat="server"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Entidad %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntidad"
                            runat="server" BorderWidth="0"
                            ValidationGroup="ValidarFactura"
                            ReadOnly="true" MaxLength="100"
                            Height="16px" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblContrato"
                            runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Contrato %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdContrato"
                            runat="server" ReadOnly="true"
                            ValidationGroup="ValidarFactura"
                            MaxLength="8" Height="16px"
                            Width="40px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdContrato"
                            runat="server" TargetControlID="txtIdContrato"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtContrato"
                            runat="server" ValidationGroup="ValidarFactura"
                            ReadOnly="true" MaxLength="80"
                            Height="16px" Width="300px"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Plan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdPlan"
                            runat="server" ReadOnly="true"
                            ValidationGroup="ValidarFactura"
                            MaxLength="8" Height="16px"
                            Width="40px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdPlan"
                            runat="server" TargetControlID="txtIdPlan"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtPlan"
                            runat="server" ValidationGroup="ValidarFactura"
                            ReadOnly="true" MaxLength="100"
                            Height="16px" Width="220px"
                            Enabled="False"></asp:TextBox>
                        <asp:DropDownList ID="DdlPlan"
                            runat="server"
                            Width="240px"
                            Visible="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblIdAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_IdAtencion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAtencion"
                            runat="server" ReadOnly="true"
                            ValidationGroup="ValidarFactura"
                            MaxLength="8" Height="16px"
                            Width="270px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdAtencion"
                            runat="server" TargetControlID="txtIdAtencion"
                            FilterType="Numbers" />
                        <asp:RequiredFieldValidator
                            ID="rqIdAtencion"
                            runat="server" ControlToValidate="txtIdAtencion"
                            Display="Dynamic"
                            ValidationGroup="ValidarFactura"
                            ErrorMessage="*" />
                        <asp:ImageButton ID="ImgBuscarAtencion"
                            runat="server"
                            Visible="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarAtencion_Click" />
                    </td>
                </tr>
                <tr id="trConfiguracion"
                    runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblClaseServicio"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_ClaseServicio %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlClaseServicio"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_TipoAtencion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoAtencion"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCubrimiento"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_ClaseCubrimiento %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClaseCubrimiento"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="rvfClaseCubrimiento"
                            ValidationGroup="ValidaCondicion"
                            runat="server"
                            ControlToValidate="ddlClaseCubrimiento"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoRelacion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_TipoRelacion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoRelacion"
                            runat="server"
                            Width="240px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlTipoRelacion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="rfvTipoRelacion"
                            ValidationGroup="ValidaCondicion"
                            runat="server"
                            ControlToValidate="ddlTipoRelacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValor"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_Valor %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValor"
                            runat="server" MaxLength="10"
                            Height="16px" Width="150px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaValor"
                            runat="server" ValidChars="."
                            TargetControlID="txtValor"
                            FilterType="Custom, Numbers" />
                        <asp:RegularExpressionValidator
                            ID="RevTxtValor"
                            ValidationGroup="ValidaCondicion"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtValor"
                            ValidationExpression="^100$|^\d{0,2}(\.\d{1,7})? *%?$"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            Enabled="false">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvGrupo"
                            ValidationGroup="ValidaCondicion"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtValor"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <%--  <AspAjax:MaskedEditExtender ID="meeValor" runat="server" MaskType="Number" Mask="999999999.99" AutoComplete="false" TargetControlID="txtValor" InputDirection="LeftToRight"></AspAjax:MaskedEditExtender>
                <AspAjax:MaskedEditValidator ID="mevValor" runat="server" ControlToValidate="txtValor" ControlExtender="meeValor" Enabled="false"
                    MinimumValue="0.00" MaximumValue="100.00" MinimumValueMessage="*"
                    MaximumValueMessage="*"
                    Display="Dynamic" IsValidEmpty="false"
                    InvalidValueMessage="*" EmptyValueMessage="*">
                </AspAjax:MaskedEditValidator>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblDescripcion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_Descripcion %>"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:TextBox ID="txtDescripcion"
                            runat="server" Height="70px"
                            TextMode="MultiLine"
                            Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVigenciaCondicion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:CondicionesCubrimientos, CondicionesCubrimientos_VigenciaCondicion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVigenciaCondicion"
                            runat="server" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgCalendar1"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <AspAjax:CalendarExtender
                            ID="CalendarExtender1"
                            runat="server" PopupButtonID="imgCalendar1"
                            TargetControlID="txtVigenciaCondicion">
                        </AspAjax:CalendarExtender>
                        <AspAjax:MaskedEditExtender
                            ID="meeVigenciaCondicion"
                            runat="server" TargetControlID="txtVigenciaCondicion"
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
                            ID="revVigenciaCondicion"
                            runat="server" ForeColor="Red"
                            ControlToValidate="txtVigenciaCondicion"
                            ValidationGroup="ValidarCampos"
                            ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha3 %>"
                            ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularFecha %>">*</asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvTxtVigenciaCondicion"
                            ValidationGroup="ValidaCondicion"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtVigenciaCondicion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="lblGuardar" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            runat="server" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnGuardar"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            OnClick="BtnGuardar_Click"
                            ValidationGroup="ValidaCondicion"
                            CausesValidation="true" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="contenedorBotonesPopup">
                <asp:ImageButton ID="imgBtnSalir"
                    runat="server" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    OnClick="ImgBtnSalir_Click"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" />
            </div>
    </asp:View>
    <asp:View ID="vBuscarAtencion"
        runat="server">
        <uc1:UC_BuscarAtencion
            runat="server" ID="ucBuscarAtencion" />
    </asp:View>
</asp:MultiView>