<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearExclusion.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearExclusion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarGrupoProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarGrupoProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarVentas.ascx"
    TagPrefix="uc1" TagName="UC_BuscarVentas" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarTarifas.ascx"
    TagPrefix="uc1" TagName="UC_BuscarTarifas" %>

<asp:MultiView runat="server"
    ID="multi" ActiveViewIndex="0">
    <asp:View ID="View1"
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
                        <asp:Label ID="lblActiva"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Activo %>" />
                    </td>
                    <td>
                        <input type="checkbox"
                            checked="checked"
                            id="chkActivo" runat="server"
                            style="height: 20px; width: 20px" />
                    </td>
                    <td>
                        <asp:Label ID="lblIdentificador"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Identificador %>" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentificador"
                            runat="server" Enabled="False"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label SkinID="LabelCampo"
                            ID="lblEntidad" runat="server"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Entidad %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntidad"
                            runat="server" BorderWidth="0"
                            ValidationGroup="ValidarFacturaExclusion"
                            ReadOnly="true" MaxLength="100"
                            Height="16px" Width="270px"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:Label SkinID="LabelCampo"
                            ID="lblContrato"
                            runat="server" Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Contrato %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdContrato"
                            runat="server" ReadOnly="true"
                            ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="8" Height="16px"
                            Width="40px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdContrato"
                            runat="server" TargetControlID="txtIdContrato"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtContrato"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
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
                            ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="8" Height="16px"
                            Width="40px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdPlan"
                            runat="server" TargetControlID="txtIdPlan"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtPlan"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            ReadOnly="true" MaxLength="100"
                            Height="16px" Width="220px"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIdAtencion"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_IdAtencion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAtencion"
                            runat="server" ReadOnly="true"
                            ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="8" Height="16px"
                            Width="270px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdAtencion"
                            runat="server" TargetControlID="txtIdAtencion"
                            FilterType="Numbers" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_TipoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoProducto"
                            runat="server"
                            Width="240px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="RfvTipoProducto"
                            ValidationGroup="ValidarFacturaExclusion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlTipoProducto"
                            InitialValue="-1"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_GrupoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdGrupoProducto"
                            runat="server" onKeyDown="return TxtProductoGrupo_keydown('grupo',event);"
                            ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="10" Height="16px"
                            Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdGrupoProducto"
                            runat="server" TargetControlID="txtIdGrupoProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtGrupoProducto"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="50" Height="16px"
                            Width="300px" Enabled="False"></asp:TextBox>
                        <asp:LinkButton ID="btnIdGrupoProducto"
                            runat="server" Text=""
                            Width="0px" Height="0px"
                            OnClick="BtnIdGrupoProducto_Click"></asp:LinkButton>
                        <asp:ImageButton ID="imgConsultarGrupoProducto"
                            runat="server" CausesValidation="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgConsultarGrupoProducto_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Producto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdProducto"
                            runat="server" onKeyDown="return TxtProductoGrupo_keydown('producto',event);"
                            ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="10" Height="16px"
                            Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdProducto"
                            runat="server" TargetControlID="txtIdProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtProducto"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="255" Height="16px"
                            Width="220px" Enabled="False"></asp:TextBox>
                        <asp:LinkButton ID="btnIdProducto"
                            runat="server" Text=""
                            Width="0px" Height="0px"
                            OnClick="BtnIdProducto_Click"></asp:LinkButton>
                        <asp:ImageButton ID="imgConsultarProducto"
                            runat="server" CausesValidation="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgConsultarProducto_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblComponente"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Componente %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlComponente"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCubrimiento"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Venta %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdVenta"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="10" Height="16px"
                            Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="FilteredTextBoxExtender1"
                            runat="server" TargetControlID="txtIdVenta"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtNumeroVenta"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="150" Height="16px"
                            Width="220px" Enabled="False"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator
                            ID="RfvNumeroVenta"
                            ValidationGroup="ValidarFacturaExclusion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtNumeroVenta"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>" Enabled="False">*</asp:RequiredFieldValidator>--%>
                        <asp:LinkButton ID="btnIdVenta"
                            runat="server" Text=""
                            Width="0px" Height="0px"
                            OnClick="BtnIdVenta_Click"></asp:LinkButton>
                        <asp:ImageButton ID="imgConsultarVenta"
                            runat="server" CausesValidation="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgConsultarVenta_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTarifa"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Tarifa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdTarifa"
                            runat="server" ValidationGroup="ValidarFacturaExclusion"
                            MaxLength="10" Height="16px"
                            Width="243px" Enabled="False"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="FilteredTextBoxExtender2"
                            runat="server" TargetControlID="txtIdTarifa"
                            FilterType="Numbers" />
                        <asp:RequiredFieldValidator
                            ID="RfvIdTarifa"
                            ValidationGroup="ValidarFacturaExclusion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtIdTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <asp:LinkButton ID="btnIdTarifa"
                            runat="server" Text=""
                            Width="0px" Height="0px"
                            OnClick="BtnIdTarifa_Click"></asp:LinkButton>
                        <asp:ImageButton ID="imgConsultarTarifa"
                            runat="server" CausesValidation="false"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            OnClick="ImgConsultarTarifa_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblNombreTarifaCre"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCondicionesTarifa, DefinirCondicionesTarifa_NombreTarifa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreTarifa"
                            runat="server" Enabled="False"
                            Height="16px" MaxLength="255"
                            ValidationGroup="ValidarFacturaExclusion"
                            Width="220px"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvNombreTarifa"
                            ValidationGroup="ValidarFacturaExclusion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtNombreTarifa"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVigencia"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirExclusiones, DefinirExclusiones_Vigencia %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVigencia"
                            runat="server" Width="100px"
                            Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RfvVigencia"
                            ValidationGroup="ValidarFacturaExclusion"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtVigencia"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label SkinID="LabelCampo"
                            ID="Label2" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            runat="server" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnGuardar"
                            runat="server" ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>"
                            OnClick="BtnGuardar_Click"
                            ValidationGroup="ValidarFacturaExclusion"
                            CausesValidation="true" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server"
                    ID="imgSalir" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>"
                    OnClick="ImgBtnSalir_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="View2"
        runat="server">
        <uc1:UC_BuscarGrupoProductos
            runat="server" ID="ucBuscarGrupoProductos" />
    </asp:View>
    <asp:View ID="View3"
        runat="server">
        <uc1:UC_BuscarProductos
            runat="server" ID="ucBuscarProductos" />
    </asp:View>
    <asp:View ID="View4"
        runat="server">
        <uc1:UC_BuscarVentas
            runat="server" ID="ucBuscarVentas" />
    </asp:View>
    <asp:View ID="View5"
        runat="server">
        <uc1:UC_BuscarTarifas
            runat="server" ID="ucBuscarTarifas" />
    </asp:View>
</asp:MultiView>