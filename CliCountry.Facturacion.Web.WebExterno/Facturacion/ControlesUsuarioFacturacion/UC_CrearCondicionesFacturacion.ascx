<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_CrearCondicionesFacturacion.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCondicionesFacturacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarAtencion.ascx" TagPrefix="uc1" TagName="UC_BuscarAtencion" %>

<asp:MultiView runat="server" ID="mlvCondicionFacturacion" ActiveViewIndex="0">
    <asp:View ID="vCrearModificar" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_TituloCrear %>"></asp:Label>
            </div>

            <div class="Mensaje">
                <asp:Label ID="LblMensaje" runat="server" />
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Identificador %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtId" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEntidad" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Entidad %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEntidad" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Contrato %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtContrato" runat="server" Enabled="false" ReadOnly="true" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Plan %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPlan" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                        <asp:DropDownList ID="DdlPlan" runat="server"
                            Width="240px"
                            Visible="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvPlan"
                            ValidationGroup="ValidarCondicionFacturacion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlPlan"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="-1"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblAtencion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondiciones, DefinirCondiciones_Atencion %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAtencion" runat="server" Enabled="false" MaxLength="8" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender ID="validaAtencion" runat="server" FilterType="Numbers" TargetControlID="TxtAtencion" />
                        <asp:ImageButton ID="ImgBuscarAtencion"
                            runat="server"
                            Visible="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarAtencion_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTiporelacionCF" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_TipoRelacion %>" Width="120px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoRelacion" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvDdlTipoRelacion"
                            ValidationGroup="ValidarCondicionFacturacion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlTipoRelacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblValorTarCre" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Valor %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValor" runat="server" MaxLength="12" ValidationGroup="ValidarFactura" Width="270px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaValor"
                            runat="server" FilterType="Numbers"
                            TargetControlID="TxtValor" />
                        <%--<asp:RegularExpressionValidator ID="revPorce" runat="server"
                    ControlToValidate="TxtValor"
                    ValidationGroup="ValidarCondicionFacturacion"
                    ValidationExpression="^(100(?:\.0{1,2})?|0*?\.\d{1,2}|\d{1,2}(?:\.\d{1,2})?)$"></asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ValidationGroup="ValidarCondicionFacturacion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtValor"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>

                        <%-- <AspAjax:MaskedEditExtender ID="meetxtValorConFacCre" runat="server"
                    TargetControlID="TxtValor"
                    Mask="99.99"
                    PromptCharacter="%"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="None"
                    ClearMaskOnLostFocus="true"
                    InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left"
                    ErrorTooltipEnabled="True" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVigenciasTarCre" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_Vigencia %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtVigenciaCondicion" runat="server"></asp:TextBox>
                        <asp:Image ID="imgFechaFact" runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <AspAjax:CalendarExtender ID="cetxtVigenciasTarCre" runat="server" PopupButtonID="imgFechaFact" TargetControlID="TxtVigenciaCondicion">
                        </AspAjax:CalendarExtender>
                        <AspAjax:MaskedEditExtender ID="metxtVigenciasConFacCre"
                            runat="server"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="True"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True"
                            InputDirection="RightToLeft"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MaskType="Date" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                            TargetControlID="TxtVigenciaCondicion" />
                        <asp:RequiredFieldValidator ID="RfvVigenciaCondicion"
                            ValidationGroup="ValidarCondicionFacturacion"
                            runat="server"
                            ControlToValidate="TxtVigenciaCondicion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblchkActivoCF" runat="server" SkinID="LabelCampo" Text="<%$ Resources:CondicionesFacturacion, CondicionesFacturacion_CActivo %>" Width="120px" />
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkActivo" runat="server" ToolTip="<% Resources:CondicionesFacturacion, CondicionesFacturacion_ChkActivo %>" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescripcionTarCre" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirCondicionesTarifa, CondicionesCubrimientos_Descripcion %>" Width="120px"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Height="78px" MaxLength="250" TextMode="MultiLine"
                            ValidationGroup="ValidarFactura"
                            Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuardar" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgGuardarConFac" runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            ValidationGroup="ValidarCondicionFacturacion"
                            OnClick="ImgGuardarCondicionFacturacions_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="contenedorBotonesPopup">
            <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
        </div>
    </asp:View>
    <asp:View ID="vBuscarAtencion" runat="server">
        <uc1:UC_BuscarAtencion runat="server" ID="ucBuscarAtencion" />
    </asp:View>
</asp:MultiView>