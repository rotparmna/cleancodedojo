<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearTercero.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearTercero" %>

<div id="contenedorControl">
    <div class="Header">
        <asp:Label ID="Label1"
            runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_Crear %>"></asp:Label>
    </div>
    <div class="Mensaje">
        <asp:Label ID="lblMensaje"
            runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblID"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:ControlesUsuario, Tercero_IdTercero %>" />
            </td>
            <td>
                <asp:Label ID="lblCampoID"
                    runat="server" Height="16px"
                    Width="150px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIndicadorActivo"
                    runat="server" SkinID="LabelCampo"
                    Text="<%$ Resources:ControlesUsuario, Tercero_IndicadorActivo %>" />
            </td>
            <td>
                <input type="checkbox"
                    checked="checked"
                    disabled="disabled"
                    id="chkActivo" style="height: 20px; width: 20px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblTipoPersona"
                    runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_TipoPersona %>" />
            </td>
            <td>
                <asp:DropDownList ID="DdlTipoPersona"
                    runat="server"
                    AutoPostBack="true"
                    Width="240px"
                    Enabled="false"
                    OnSelectedIndexChanged="DdlTipoPersona_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="RfvTipoPersona"
                    ValidationGroup="ValCampos"
                    runat="server"
                    Display="Dynamic"
                    ControlToValidate="DdlTipoPersona"
                    InitialValue="-1"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblTipoDocumento"
                    runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_TipoDocumento %>" />
            </td>
            <td>
                <asp:DropDownList ID="DdlTipoDocumento"
                    runat="server"
                    Width="240px"
                    Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="RfvTipoDocumento"
                    ValidationGroup="ValCampos"
                    runat="server"
                    Display="Dynamic"
                    ControlToValidate="DdlTipoDocumento"
                    InitialValue="-1"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblNombre" runat="server"
                    Text="<%$ Resources:ControlesUsuario, Tercero_Nombre %>" />
            </td>
            <td>
                <asp:TextBox ID="txtNombre"
                    ValidationGroup="ValCampos"
                    MaxLength="100" runat="server"
                    Height="16px" Width="220px"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="rfvNombre" runat="server"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    SetFocusOnError="true"
                    ValidationGroup="ValCampos"
                    Display="Dynamic"
                    ControlToValidate="txtNombre" />
            </td>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblRegimen" runat="server"
                    Text="<%$ Resources:ControlesUsuario, Tercero_Regimen %>" />
            </td>
            <td>
                <asp:Label ID="lblCampoRegimen"
                    runat="server" Height="16px"
                    Width="150px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblNroDocumento"
                    runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_NumeroDocumento %>" />
            </td>
            <td>
                <asp:TextBox ID="txtNroDocumento"
                    MaxLength="20" runat="server"
                    Height="16px" Width="150px"
                    TextMode="Number" AutoPostBack="True" OnTextChanged="TxtNroDocumento_TextChanged"></asp:TextBox>
                <AspAjax:FilteredTextBoxExtender
                    ID="ftbNroDocumento"
                    runat="server" FilterType="Numbers"
                    TargetControlID="txtNroDocumento" />
                <asp:RegularExpressionValidator
                    ID="revNroDocumento"
                    runat="server" ControlToValidate="txtNroDocumento"
                    ValidationExpression="<%$ Resources:GlobalWeb, General_ExpresionRegularNIT %>"
                    ToolTip="<%$ Resources:GlobalWeb, General_ExpresionRegularNITMensaje %>"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_ExpresionRegularNITMensaje %>"
                    Display="Dynamic"
                    ValidationGroup="ValCampos"
                    Enabled="false">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator
                    ID="rfvNroDocumento"
                    runat="server" SetFocusOnError="true"
                    ValidationGroup="ValCampos"
                    ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                    Display="Dynamic"
                    ControlToValidate="txtNroDocumento">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblDigitoVerificacion"
                    runat="server" Text="<%$ Resources:ControlesUsuario, Tercero_DigitoVerificacion %>" />
            </td>
            <td>
                <asp:Label ID="lblCampoDigitoVerificacion"
                    runat="server" Height="16px"
                    Width="150px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label SkinID="LabelCampo"
                    ID="lblGuardar" runat="server"
                    Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
            </td>
            <td>
                <asp:ImageButton ID="ImgGuardar"
                    runat="server" ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                    CausesValidation="true"
                    ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                    ValidationGroup="ValCampos"
                    OnClick="ImgGuardar_Click" />
            </td>
            <td colspan="2">
                
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
</div>
<asp:HiddenField ID="hfNroDocumento"
    runat="server" />