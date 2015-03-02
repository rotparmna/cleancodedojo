<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearCondicionesTarifa.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCondicionesTarifa" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarGrupoProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarGrupoProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarTarifas.ascx"
    TagPrefix="uc1" TagName="UC_BuscarTarifas" %>

<div id="contenedorControl">
    <asp:MultiView ID="mltvCrearCondicionTarifas"
        runat="server" ActiveViewIndex="0">
        <asp:View ID="vCrearTarifa"
            runat="server">
            <div class="Header">
                <asp:Label ID="lblTitulo"
                    CssClass="LabelTitulo"
                    runat="server" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Titulo %>"></asp:Label>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCondicionTarifaActivaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Activo %>"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkActivo"
                            runat="server" Checked="true" />
                    </td>
                    <td>
                        <asp:Label ID="lblIdentificadorTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Identificador %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtId"
                            runat="server" Enabled="false"
                            MaxLength="8" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProductoTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoProducto"
                            runat="server"
                            Width="240px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProductoTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_GrupoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIdGrupoProducto"
                            runat="server"
                            onKeyDown="return TxtProductoGrupo_keydown('grupo',event);"
                            MaxLength="8"
                            Width="50px"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"></asp:TextBox>
                        <asp:TextBox ID="TxtNombreGrupo"
                            runat="server" Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarGrupoProducto"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarGrupoProducto_Click" />
                        <asp:LinkButton ID="BtnIdGrupoProducto"
                            runat="server" Width="0px"
                            Height="0px" OnClick="BtnGrupoProducto_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProductoTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Producto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIdProducto"
                            runat="server"
                            onKeyDown="return TxtProductoGrupo_keydown('producto',event);"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"
                            Width="50px"></asp:TextBox>

                        <asp:TextBox ID="TxtNombreProducto"
                            runat="server" Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarProducto"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarProducto_Click" />
                        <asp:LinkButton ID="BtnIdProducto"
                            runat="server" Text=""
                            OnClick="BtnProducto_Click"
                            Width="0" Height="0"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label ID="lblComponenteTarCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Componente %>"></asp:Label>
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
                        <asp:Label ID="lblTipoRelacionTarCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TipoRelacion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion"
                            runat="server"
                            OnSelectedIndexChanged="DdlTipoRelacion_SelectedIndexChanged"
                            AutoPostBack="true"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="RfvDdlTipoRelacion"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlTipoRelacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Tarifa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTarifa"
                            runat="server" Enabled="false"
                            Width="243px"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvTarifa"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="ImgBuscarTarifa"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarTarifa_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblNombreTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_NombreTarifa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNombreTarifa"
                            runat="server" Enabled="false"
                            MaxLength="8" Width="270px"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvNombreTarifa"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtNombreTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVigenciaTarifaCre"
                            runat="server"
                            SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_VigenciaTarifa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtVigenciaTarifa"
                            runat="server" Enabled="false"></asp:TextBox>
                        <AspAjax:MaskedEditExtender
                            ID="meetxtVigenciaTarifaCre"
                            runat="server"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="true"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True"
                            InputDirection="RightToLeft"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MaskType="Date"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            TargetControlID="TxtVigenciaTarifa" />
                        <asp:RequiredFieldValidator
                            ID="RfvVigenciaTarifa"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtVigenciaTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTarifaAlternaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_TarifaAlterna %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTarifaAlt"
                            Enabled="false" runat="server"
                            Width="243px"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvTarifaAlt"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtTarifaAlt"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="ImgBuscarTarifaAlterna"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarTarifa_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblNombreTarifaAlternaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_NombreTarifaAlterna %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNombreTarifaAlt"
                            Enabled="false" runat="server"
                            MaxLength="8" Width="270px"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvNombreTarifaAlt"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtNombreTarifaAlt"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVigenciaAlternaCre0"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_VigenciaAlterna %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtVigenciaTarifaAlt"
                            runat="server" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvVigenciaTarifaAlt"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtVigenciaTarifaAlt"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblValorTarCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Valor %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValor"
                            runat="server"
                            Width="270px"
                            Text="<%$ Resources:GlobalWeb, General_ValorCero %>"></asp:TextBox>
                 <%--       <asp:RegularExpressionValidator
                            ID="RevTxtValorDesCr"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="TxtValor"
                            ValidationExpression="^100$|^\d{0,2}(\.\d{1,7})? *%?$"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            Enabled="false">*</asp:RegularExpressionValidator>--%>
                               <asp:RegularExpressionValidator
                            ID="RevTxtValorDesCr"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="TxtValor"
                            ValidationExpression="^100$|^\d{0,2}(\.\d{1,7})? *%?$"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoPorcentaje %>"
                            Enabled="false">*</asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rgvValorInvalido"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            MinimumValue="1"
                            MaximumValue="99999999999"
                            ControlToValidate="TxtValor"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoAjusteTarifa %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoAjusteTarifa %>"
                            Enabled="false">*</asp:RangeValidator>
                        
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaValor"
                            runat="server" FilterType="Numbers, Custom" ValidChars=","
                            TargetControlID="TxtValor" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtValorTarCre"
                            ForeColor="Red"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server" 
                            ControlToValidate="TxtValor"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblVigenciasTarCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Vigencia %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtVigenciaCondicionTarifa"
                            runat="server" Width="128px"></asp:TextBox>
                        <AspAjax:CalendarExtender
                            ID="cetxtVigenciasTarCre"
                            runat="server" PopupButtonID="imgFechaVigencia"
                            TargetControlID="TxtVigenciaCondicionTarifa">
                        </AspAjax:CalendarExtender>

                        <asp:Image ID="imgFechaVigencia"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <asp:RequiredFieldValidator
                            ID="RFVtxtVigenciasTarCre"
                            ForeColor="Red"
                            ValidationGroup="ValidarCondicionTarifa"
                            runat="server"
                            ControlToValidate="TxtVigenciaCondicionTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                        <AspAjax:MaskedEditExtender
                            ID="metxtVigenciasTarCre"
                            runat="server"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="true"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True"
                            InputDirection="RightToLeft"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MaskType="Date"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            TargetControlID="TxtVigenciaCondicionTarifa"
                            CultureName="es-ES"
                            UserDateFormat="DayMonthYear" />
                        <AspAjax:MaskedEditValidator ID="mevTxtVigenciaCondicionTarifa" runat="server"
                            ControlExtender="metxtVigenciasTarCre"
                            ControlToValidate="TxtVigenciaCondicionTarifa"
                            EmptyValueMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InvalidValueMessage="<%$ Resources:GlobalWeb, General_FechaInvalida %>"
                            Display="Dynamic"
                            IsValidEmpty="true"
                            TooltipMessage="<%$ Resources:GlobalWeb, General_IngreseFecha %>"
                            InvalidValueBlurredMessage="<%$ Resources:GlobalWeb, General_FormatoInvalido %>"
                            ValidationGroup="ValidarCondicionTarifa" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescripcionTarCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Descripcion %>"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtObservaciones"
                            Width="100%" runat="server"
                            Height="50px" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuardar"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnGuardatTarifasCre"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            ValidationGroup="ValidarCondicionTarifa"
                            OnClick="ImgGuardarCondicionTarifas_Click" style="height: 16px" />
                    </td>
                </tr>
            </table>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server"
                    ID="ImgRegresar"
                    ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>"
                    OnClick="ImgRegresar_Click" />
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
        <asp:View ID="vBuscarTarifa"
            runat="server">
            <uc1:UC_BuscarTarifas
                runat="server" ID="ucBuscarTarifas" />
        </asp:View>
    </asp:MultiView>
</div>