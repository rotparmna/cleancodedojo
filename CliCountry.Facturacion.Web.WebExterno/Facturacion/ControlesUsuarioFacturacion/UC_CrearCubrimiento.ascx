<%@ Control Language="C#"
    AutoEventWireup="true"
    CodeBehind="UC_CrearCubrimiento.ascx.cs"
    Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCubrimiento" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarGrupoProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarGrupoProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarProductos.ascx"
    TagPrefix="uc1" TagName="UC_BuscarProductos" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarAtencion.ascx"
    TagPrefix="uc2" TagName="UC_BuscarAtencion" %>

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
                            Height="16px" Width="270px"
                            Enabled="False"></asp:TextBox>
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
                        <asp:DropDownList ID="ddlPlan"
                            runat="server"
                            Width="240px">
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
                        <asp:ImageButton ID="imgBuscarAtencion"
                            runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/search.png"
                            ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                            OnClick="ImgBuscarAtencion_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_TipoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoProducto"
                            runat="server"
                            Width="240px" AutoPostBack="True"
                            OnSelectedIndexChanged="DdlTipoProducto_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="RfvDdlTipoProducto"
                            ValidationGroup="ValidarDatosCubrimientos"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="DdlTipoProducto"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoProducto"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_GrupoProducto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdGrupoProducto"
                            runat="server" onKeyDown="return TxtProductoGrupo_keydown('grupo',event);"
                            MaxLength="10" Height="16px"
                            Width="40px"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvGrupo"
                            ValidationGroup="ValidarDatosCubrimientos"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtIdGrupoProducto"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdGrupoProducto"
                            runat="server" TargetControlID="txtIdGrupoProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtGrupoProducto"
                            runat="server" ValidationGroup="ValidarFactura"
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
                            ValidationGroup="ValidarFactura"
                            MaxLength="10" Height="16px"
                            Width="40px"></asp:TextBox>
                        <AspAjax:FilteredTextBoxExtender
                            ID="validaIdProducto"
                            runat="server" TargetControlID="txtIdProducto"
                            FilterType="Numbers" />
                        <asp:TextBox ID="txtProducto"
                            runat="server" ValidationGroup="ValidarFactura"
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
                        <asp:DropDownList ID="DdlComponente"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCubrimiento"
                            runat="server" SkinID="LabelCampo"
                            Text="<%$ Resources:DefinirCubrimientos, DefinirCubrimientos_Cubrimiento %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlClaseCubrimiento"
                            runat="server"
                            Width="240px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="rfvCubrimiento"
                            ValidationGroup="ValidarDatosCubrimientos"
                            runat="server"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="DdlClaseCubrimiento"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                    </td>
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
                            ValidationGroup="ValidarDatosCubrimientos" />
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
    <asp:View runat="server"
        ID="vBuscarAtencion">
        <uc2:UC_BuscarAtencion
            runat="server" ID="ucBuscarAtencion" />
    </asp:View>
</asp:MultiView>