<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_CrearCliente.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCliente" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarPaisDptoCiudad.ascx" TagPrefix="uc1" TagName="UC_BuscarPaisDptoCiudad" %>

<asp:MultiView runat="server" ID="multi" ActiveViewIndex="0">
    <asp:View ID="View1" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_TituloCrear %>"></asp:Label>
            </div>
            
            <div class="Mensaje">
                <asp:Label ID="lblMensaje" runat="server" />
            </div>
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblDatosGenerales" SkinID="LabelCampo" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_DatosGenerales %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="labelId" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Id %>" />
                    </td>
                    <td>
                        <asp:Label ID="LblId" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblActivo" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Activo %>" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivo" runat="server" Checked="true" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblTipoDocumento" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_TipoDocumento %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoDocumento" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvTipoDocumento"
                            ValidationGroup="ValidarCrearCliente"
                            runat="server"
                            Display="Static"
                            ControlToValidate="DdlTipoDocumento"
                            ErrorMessage="Prueba"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label2" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_NroDocumento %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNroDocumento" runat="server" MaxLength="20" Width="200px" onKeyPress="ValidacionCampo('Alfanumerico');" />
                        <asp:RequiredFieldValidator ID="RfvNroDocumento" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtNroDocumento" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="labelExpedicion" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Expedicion %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtExpedicion" ReadOnly="true" Enabled="false" Width="200px" runat="server" />
                        <asp:RequiredFieldValidator ID="RfvTxtExpedicion" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtExpedicion">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="ImgBuscarExpedicion"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscarExpedicion_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblNombres" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Nombres %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNombres" runat="server" MaxLength="50" Width="200px" onKeyPress="ValidacionCampo('Letras');" />
                        <asp:RequiredFieldValidator ID="RfvNombres" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtNombres">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label3" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Apellidos %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtApellidos" runat="server" MaxLength="50" Width="200px" onKeyPress="ValidacionCampo('Letras');" />
                        <asp:RequiredFieldValidator ID="RfvApellidos" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtApellidos">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblFechaNacimiento" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_FechaNacimiento %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtFechaNacimiento" runat="server" onchange='TxtFechaNacimiento_Change(this, event);' />
                        <asp:Image ID="imgCalendar1" runat="server" ImageUrl="~/App_Themes/SAHI/images/calendario.png" />
                        <AspAjax:CalendarExtender ID="CalendaFechaNacimiento" runat="server" PopupButtonID="imgCalendar1" TargetControlID="TxtFechaNacimiento"></AspAjax:CalendarExtender>
                        <AspAjax:MaskedEditExtender ID="meeFechaNacimiento" runat="server" TargetControlID="TxtFechaNacimiento"
                            Mask="<%$ Resources:GlobalWeb, General_FormatoMascaraFecha %>"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            UserDateFormat="DayMonthYear"
                            InputDirection="RightToLeft"
                            AcceptNegative="None"
                            ClearMaskOnLostFocus="false"
                            ClearTextOnInvalid="true"
                            ErrorTooltipEnabled="True" />
                        <asp:RequiredFieldValidator ID="RfvFechaNacimiento" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtFechaNacimiento">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RanFechaNacimiento" runat="server"
                            ToolTip="<%$ Resources:GlobalWeb, General_MensajeErrorFechaSuperior %>"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_MensajeErrorFechaSuperior %>"
                            Type="Date"
                            ControlToValidate="TxtFechaNacimiento"
                            Display="Dynamic"
                            ValidationGroup="ValidarCrearCliente5">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="labelAnios" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Anios %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAnios" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="labelCiudadNacimiento" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_CiudadNacimiento %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCiudadNacimiento" runat="server" Width="200px" ReadOnly="true" Enabled="false" />
                        <asp:RequiredFieldValidator ID="RfvTxtCiudadNacimiento" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtCiudadNacimiento">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="ImgBuscarCiudadNacimiento"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscarCiudadNacimiento_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblGenero" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Genero %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlGenero" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvGenero"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlGenero"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label4" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Ocupacion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlOcupacion" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvOcupacion"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlOcupacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label5" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_EstadoCivil %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlEstadoCivil" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label6" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Religion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlReligion" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label7" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Nivel %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlNivel" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label8" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_TipoAfiliacion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoAfiliacion" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvTipoAfiliacion"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlTipoAfiliacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblSede" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_SedeAtencion %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlSede" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvSede"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlSede"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" />
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label9" SkinID="LabelCampo" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_DatosComplementarios %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label12" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Zona %>" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlZona" runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvZona"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlZona"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="label10" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Barrio %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtBarrio" runat="server" ReadOnly="true" Width="200px" />
                        <asp:RequiredFieldValidator ID="RfvTxtBarrio"
                            ValidationGroup="ValidarCrearCliente5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="TxtBarrio"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="ImgBuscarCiudadResidencia"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgBuscarCiudadResidencia_Click" ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblDirResidencia" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_DireccionResidencia %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtDirResidencia" runat="server" MaxLength="255" Width="250px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtDirResidencia">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblTelResidencia" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_TelefonoResidencia %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTelResidencia" runat="server" MaxLength="15" />
                        <AspAjax:FilteredTextBoxExtender ID="FtbTxtTelResidencia" runat="server" FilterType="Numbers" TargetControlID="TxtTelResidencia" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            SetFocusOnError="true"
                            ValidationGroup="ValidarCrearCliente5" Display="Dynamic" ControlToValidate="TxtTelResidencia">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label11" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_DireccionOficina %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtDirOficina" runat="server" MaxLength="255" Width="250px" />
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label13" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_TelefonoOficina %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTelOficina" runat="server" MaxLength="15" />
                        <AspAjax:FilteredTextBoxExtender ID="FtbTxtTelOficina" runat="server" FilterType="Numbers" TargetControlID="TxtTelOficina" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="Label14" runat="server" Text="<%$ Resources:ControlesUsuario, Cliente_Email %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEmail" runat="server" Width="250px" />
                        <asp:RegularExpressionValidator ID="RevTxtEmail"
                            runat="server"
                            ValidationGroup="ValidarCrearCliente5"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ExpresionEmailMensaje %>"
                            ControlToValidate="TxtEmail"
                            ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionEmail %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ExpresionEmailMensaje %>">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Label SkinID="LabelCampo" ID="lblGuardar" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgGuardar" runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png" OnClick="ImgGuardar_Click" />
                    </td>
                </tr>
            </table>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
            </div>
        </div>
        <asp:HiddenField ID="HfIdExpedicion" runat="server" />
        <asp:HiddenField ID="HfIdCiudadNacimiento" runat="server" />
        <asp:HiddenField ID="HfIdBarrio" runat="server" />
        <asp:HiddenField ID="HfFormatoFecha" runat="server" Value="<%$ Resources:GlobalWeb, General_FormatoFecha %>" />
    </asp:View>
    <asp:View ID="View2" runat="server">
        <uc1:UC_BuscarPaisDptoCiudad runat="server" ID="ucBuscarPaisDptoCiudad" />
    </asp:View>
</asp:MultiView>