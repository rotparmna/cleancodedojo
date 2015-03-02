<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearDescuentos.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearDescuentos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarGrupoProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarGrupoProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarAtencion.ascx"
    TagPrefix="uc2" TagName="UC_BuscarAtencion" %>
<div id="contenedorControl">
    <asp:MultiView ID="mltvCrearDescuentos"
        runat="server" ActiveViewIndex="0">
        <asp:View ID="vCrearDescuentos"
            runat="server">
            <div class="Header">
                <asp:Label ID="lblTitulo"
                    CssClass="LabelTitulo"
                    runat="server" Text="<%$ Resources:Descuentos, Descuentos_Titulo %>"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje"
                    runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblEntidad"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Entidad %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntidad"
                            runat="server" Enabled="false"
                            Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContrato"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtContrato"
                            runat="server" Enabled="false"
                            ReadOnly="true" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="trServicioAtencion">
                    <td>
                        <asp:Label ID="lblClaseServicio"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Configuracion, Descuentos_ClaseServicio %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClaseServicio"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Configuracion, Descuentos_TipoAtencion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoAtencion"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlan"
                            runat="server" Enabled="false"
                            Width="270px"></asp:TextBox>
                        <asp:DropDownList ID="DdlPlan"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Atencion %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtAtencion"
                            runat="server" Enabled="false"
                            MaxLength="8"
                            Width="270px"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"></asp:TextBox>
                        <asp:ImageButton ID="imgBuscarAtencion"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarAtencion_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblActivoDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Activo %>"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo"
                            runat="server" Checked="true" />
                    </td>
                    <td>
                        <asp:Label ID="lblIdentificadorDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Identificador %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificadorDesCr"
                            runat="server" MaxLength="8"
                            Enabled="false" Width="270px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="ftbetxtIdentificadorDesCr"
                            runat="server" FilterType="Numbers"
                            TargetControlID="txtIdentificadorDesCr" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProductoDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_TipoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoProducto"
                            runat="server"
                            AutoPostBack="true"
                            Width="240px" OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProductoDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_GrupoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIdGrupoProducto"
                            runat="server" Enabled="false"
                            onkeydown="return TxtProductoGrupo_keydown('grupo',event);"
                            onblur="DependenciaCampo_OnBlur('grupo');"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"
                            MaxLength="10"
                            Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validatxtIdGrupoProducto"
                            runat="server" TargetControlID="TxtIdGrupoProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="TxtNombreGrupo"
                            runat="server" Width="270px"
                            Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarGrupoProducto"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarGrupoProducto_Click" />
                        <asp:LinkButton ID="btnIdGrupoProducto"
                            runat="server" Width="0px"
                            Height="0px" OnClick="BtnGrupoProducto_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProductoDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Producto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIdProducto" Enabled="false"
                            runat="server"
                            onkeydown="return TxtProductoGrupo_keydown('producto',event);"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"
                            onblur="DependenciaCampo_OnBlur('producto');"
                            MaxLength="10" Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validatxtIdProductoDesCre"
                            runat="server" TargetControlID="TxtIdProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="TxtNombreProducto"
                            runat="server" Width="270px"
                            Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarProducto"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarProducto_Click" />
                        <asp:LinkButton ID="btnIdProducto"
                            runat="server" OnClick="BtnProducto_Click"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label ID="lblComponenteDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Componente %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlComponente"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_TipoRelacion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion"
                            runat="server"
                            Width="240px"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="DdlTipoRelacion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="RfvDdlTipoRelacion"
                            ValidationGroup="ValidarDatosDescuentos"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="DdlTipoRelacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblValorDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Valor %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValorDesCr"
                            runat="server" Text="<%$ Resources:GlobalWeb, General_ValorCero %>"
                            MaxLength="8"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtValorDesCr"
                            ForeColor="Red"
                            ValidationGroup="ValidarDatosDescuentos"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtValorDesCr"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="RevTxtValorDesCr"
                            ValidationGroup="ValidarDatosDescuentos"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtValorDesCr"
                            ValidationExpression="^100$|^\d{0,2}(\.\d{1,7})? *%?$"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            Enabled="false">*</asp:RegularExpressionValidator>
                        <AspAjax:MaskedEditExtender
                            ID="meetxtValorDesCr"
                            runat="server"
                            TargetControlID="txtValorDesCr"
                            Enabled="false"
                            Mask="999,999,999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Number"
                            ClearMaskOnLostFocus="true"
                            InputDirection="RightToLeft"
                            AcceptNegative="Left"
                            DisplayMoney="Left"
                            ErrorTooltipEnabled="True" />
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaValor"
                            runat="server" FilterType="Numbers"
                            TargetControlID="txtValorDesCr" />
                        <asp:Label ID="lblAplicar"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Configuracion, Descuento_Aplicar %>" />
                        <asp:CheckBox ID="chkAplicar"
                            runat="server" Checked="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFechaInicialDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_FechaInicial %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaInicialDesCr" runat="server" MaxLength="8"></asp:TextBox>
                        <asp:Image ID="imgCalendarFechaInicial" runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <AspAjax:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgCalendarFechaInicial" TargetControlID="txtFechaInicialDesCr">
                        </AspAjax:CalendarExtender>
                        <AspAjax:MaskedEditExtender
                            ID="meeFechaInicial"
                            runat="server" TargetControlID="txtFechaInicialDesCr"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            InputDirection="RightToLeft"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="true"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True" />
                        <asp:RequiredFieldValidator
                            ID="RfvFechaInicio"
                            ValidationGroup="ValidarDatosDescuentos"
                            runat="server"
                            ControlToValidate="txtFechaInicialDesCr"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revFechaInicial"
                            runat="server" ForeColor="Red"
                            ValidationGroup="ValidarDatosDescuentos"
                            ControlToValidate="txtFechaInicialDesCr"
                            ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha3 %>"
                            ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularFecha %>">*</asp:RegularExpressionValidator>
                        <asp:CompareValidator
                            ID="cvFechasFacturacion"
                            runat="server" ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha1 %>"
                            ValidationGroup="ValidarDatosDescuentos"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFecha1 %>"
                            Operator="LessThanEqual"
                            Type="Date" ControlToValidate="txtFechaInicialDesCr"
                            ForeColor="Red"
                            ControlToCompare="txtFechaFinalDesCr">*</asp:CompareValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaFinalDesCr"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_FechaFinal %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaFinalDesCr"
                            runat="server" MaxLength="8"></asp:TextBox>
                        <asp:Image ID="imgCalendarFechaFinal"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <AspAjax:CalendarExtender
                            ID="CalendarExtender2"
                            runat="server" PopupButtonID="imgCalendarFechaFinal"
                            TargetControlID="txtFechaFinalDesCr">
                        </AspAjax:CalendarExtender>
                        <AspAjax:MaskedEditExtender
                            ID="meeFechaFinal"
                            runat="server" TargetControlID="txtFechaFinalDesCr"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            InputDirection="RightToLeft"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="true"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True" />
                        <%--<asp:RequiredFieldValidator
                            ID="rfvTxtFechaFinalDesCr"
                            ValidationGroup="ValidarDatosDescuentos"
                            runat="server"
                            ControlToValidate="txtFechaFinalDesCr"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator
                            ID="revFechaFinal"
                            runat="server" ForeColor="Red"
                            ValidationGroup="ValidarDatosDescuentos"
                            ControlToValidate="txtFechaFinalDesCr"
                            ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha3 %>"
                            ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularFecha %>">*</asp:RegularExpressionValidator>
                        <asp:CompareValidator
                            ID="cvFechasFacturacion1"
                            runat="server" ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFecha4 %>"
                            ValidationGroup="ValidarDatosDescuentos"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFecha4 %>"
                            Operator="GreaterThanEqual"
                            Type="Date" ControlToValidate="txtFechaFinalDesCr"
                            ForeColor="Red"
                            ControlToCompare="txtFechaInicialDesCr">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr runat="server" id="trVisualizacion">
                    <td>
                        <asp:Label ID="lblVisualizacion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:Descuentos, Descuentos_Visualizacion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlVisualizacion"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuardar"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgGuardarDescuentos"
                            runat="server"
                            ValidationGroup="ValidarDatosDescuentos"
                            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            OnClick="ImgGuardarDescuentos_Click" />
                    </td>
                </tr>
            </table>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server"
                    ID="ImgRegresar"
                    ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>"
                    OnClick="ImgRegresar_Click"
                    CausesValidation="false" />
            </div>
        </asp:View>
        <asp:View ID="vBuscarProductos"
            runat="server">
            <uc1:UC_BuscarProductos
                runat="server" ID="ucBuscarProductos" />
        </asp:View>
        <asp:View ID="vBuscarGrupoProductos"
            runat="server">
            <uc1:UC_BuscarGrupoProductos
                runat="server" ID="ucBuscarGrupoProductos" />
        </asp:View>
        <asp:View runat="server"
            ID="vBuscarAtencion">
            <uc2:UC_BuscarAtencion
                runat="server" ID="ucBuscarAtencion" />
        </asp:View>
    </asp:MultiView>
</div>