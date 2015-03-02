<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_VincularEntidad.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_VincularEntidad" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarContratoPlan.ascx" TagPrefix="uc1" TagName="UC_BuscarContratoPlan" %>

<asp:MultiView ID="mltvVincularEntidad" runat="server" ActiveViewIndex="0">
    <asp:View ID="vVincularEntidad" runat="server">
        <div id="contenedorControl">
            <div class="Header">
                <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server"></asp:Label>
            </div>
            <div class="Mensaje">
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblOrden" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Orden %>" Width="120px" />
                        <asp:Label ID="lblIdAtencion" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrden" runat="server" Enabled="false" MaxLength="8" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblNivel" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Nivel %>" Width="120px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlNivel" runat="server"
                            ValidationGroup="ValidarVinculacionEntidad"
                            Width="270px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvDdlNivel"
                            ValidationGroup="ValidarVinculacionEntidad"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="DdlNivel"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEntidad" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Entidad %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntidad" runat="server" Enabled="false" Width="270px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContrato" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Contrato %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContrato" runat="server" Enabled="false" ValidationGroup="ValidarVinculacionEntidad" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContratoPlan" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Plan %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlan" runat="server" Enabled="false" Width="270px"></asp:TextBox>
                        <asp:ImageButton ID="imgBtnContratoPlan" runat="server" ImageUrl="~/App_Themes/SAHI/images/search.png" OnClick="ImgBtnContratoPlan_Click" ToolTip="<%$ Resources:GlobalWeb, General_BotonConsultar %>" />
                        <asp:RequiredFieldValidator ID="rfvPlan"
                            ValidationGroup="ValidarVinculacionEntidad"
                            runat="server"
                            ControlToValidate="txtPlan"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoAfiliacion0" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_TipoAfiliacion %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlTipoAfiliacion" runat="server"
                            Width="270px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvDdlTipoAfiliacion"
                            ValidationGroup="ValidarVinculacionEntidad"
                            runat="server"
                            ControlToValidate="DdlTipoAfiliacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNumAfiliacion" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_NumeroAfiliacion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumeroAfiliacion" runat="server"
                            ValidationGroup="ValidarVinculacionEntidad"
                            Width="200px" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtNumeroAfiliacion"
                            ValidationGroup="ValidarVinculacionEntidad"
                            runat="server"
                            ControlToValidate="txtNumeroAfiliacion"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblMontoEjecutado" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Montoejecutado %>" Width="120px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMontoEjecutado" runat="server"
                            MaxLength="20" TextMode="Number" Text="0"
                            ValidationGroup="ValidarVinculacionEntidad"
                            Width="150px"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvtxtMontoEjecutado" runat="server"
                            ControlToValidate="txtMontoEjecutado"
                            ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                            ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>" ValidationGroup="ValidarVinculacionEntidad">*
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblActivoVE" runat="server" SkinID="LabelCampo" Text="<%$ Resources:VincularEntidad, VincularEntidad_Activo %>" Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActivoVE" runat="server" Checked="true" Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblCrear0" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, General_BotonGuardar %>" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgGuardarVinculacionEntidad" runat="server"
                            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png" ValidationGroup="ValidarVinculacionEntidad"
                            CausesValidation="true" OnClick="ImgGuardarVinculacionEntidad_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp; </td>
                    <td>&nbsp; </td>
                </tr>
            </table>
            <div class="contenedorBotonesPopup">
                <asp:ImageButton runat="server" ID="ImgRegresar" ImageUrl="~/App_Themes/SAHI/images/regresar.gif"
                    ToolTip="<%$ Resources:GlobalWeb, General_BotonRegresar %>" OnClick="ImgRegresar_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="vBuscarContratoPlan" runat="server">
        <uc1:UC_BuscarContratoPlan runat="server" ID="ucBuscarContratoPlan" />
    </asp:View>
</asp:MultiView>