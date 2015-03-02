<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_MostrarConceptos.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_MostrarConceptos" %>
<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCliente.ascx" TagPrefix="uc2" TagName="UC_CrearCliente" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearConceptoCobro.ascx" TagPrefix="uc2" TagName="UC_CrearConceptoCobro" %>

<fieldset id="fsResultado" runat="server" visible="true">
    <legend>
        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:ControlesUsuario, Concepto_Titulo %>"></asp:Label>
    </legend>
    <table>
        <tr>
            <td>
                <asp:Label SkinID="lblDepositoTitulo" ID="lblPaquete" runat="server" Text="<%$ Resources:ControlesUsuario, Deposito_Titulo %>" />
            </td>
            <td>
                <asp:TextBox ID="txtDeposito" Enabled="false" runat="server" Height="18px" MaxLength="10" TextMode="Number" Width="170px"></asp:TextBox>
            </td>
    </table>
    <div id="divCruzarDepositos" runat="server" class="Menu_cruzar_deposito">
        <asp:Label SkinID="lblDepositoTituloCruzar" ID="lblDepositoCruzar" runat="server" Text="<%$ Resources:ControlesUsuario, Deposito_Titulo_Cruzar %>" />
        <asp:TextBox ID="txtDepositoCruzar"
            ValidationGroup="CruzarDepositos"
            onblur="return ValidarDeposito(this);" runat="server" Height="18px" MaxLength="10" TextMode="Number" Width="170px"></asp:TextBox>
        <asp:ImageButton ID="imgCruzarDeposito" runat="server"
            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
            ValidationGroup="CruzarDepositos"
            ToolTip="<%$ Resources:ControlesUsuario, Boton_Cruzar_Deposito %>" OnClick="ImgCruzarDeposito_Click" />
        <asp:RequiredFieldValidator ID="rfvCruzarDeposito" runat="server" Text="*"
            ForeColor="Red"
            ControlToValidate="txtDepositoCruzar"
            ValidationGroup="CruzarDepositos" Display="Dynamic" Font-Size="Large"></asp:RequiredFieldValidator>
    </div>
    <div id="divGuardarConceptos" runat="server" class="Menu_superior">
        <asp:Label SkinID="LabelCampo" ID="lblNuevoConcepto" runat="server" Height="30px"
            Text="<%$ Resources:GlobalWeb, General_BotonNuevo %>" />
        &nbsp;
        <asp:ImageButton ID="ImgAgregarConcepto" runat="server"
            ImageUrl="~/App_Themes/SAHI/images/adicionar.png"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonNuevo %>" OnClick="ImgAgregarConcepto_Click" />
        &nbsp;
        &nbsp;
        <asp:Label SkinID="LabelCampo" ID="lblConsultarEstado" runat="server" Height="30px"
            Text="<%$ Resources:ControlesUsuario, Concepto_BotonActualizar %>" />
        &nbsp;
        <asp:ImageButton ID="imgGuardar" runat="server"
            ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
            ToolTip="<%$ Resources:GlobalWeb, General_BotonGuardar %>" OnClick="ImgGuardar_Click" />
    </div>
    <div id="divGrilla">
        <br />
        <asp:GridView ID="grvConceptos" runat="server"
            EmptyDataText="<%$ Resources:GlobalWeb, General_GrillaSinDatos %>" CssClass="AspNet-GridView" AutoGenerateColumns="False" DataKeyNames="IdConcepto">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblCampoAplicar" runat="server" Text="<%$ Resources:ControlesUsuario, Concepto_CheckAplicar %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAplicarConcepto" runat="server" Checked='<%# Eval("IndConcepto") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdConcepto" runat="server" Text='<%# Eval("IdConcepto") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdAtencion" runat="server" Text='<%# Eval("IdATencion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Concepto_Tercero %>" DataField="Tercero" ItemStyle-Wrap="true" SortExpression="Tercero" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="30%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Concepto_Contrato %>" DataField="Contrato" ItemStyle-Wrap="true" SortExpression="Contrato" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$ Resources:ControlesUsuario, Concepto_Plan %>" DataField="Plan" ItemStyle-Wrap="true" SortExpression="Plan" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField SortExpression="ValorConcepto" HeaderText="<%$ Resources:ControlesUsuario, Concepto_ValorConceptoTitulo %>">
                    <ItemTemplate>
                        <asp:TextBox ID="txtValorConcepto" runat="server" Height="20px" TextMode="Number" MaxLength="10" Text='<%# Bind("ValorConcepto") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdContrato" runat="server" Text='<%# Eval("IdContrato") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdPlan" runat="server" Text='<%# Eval("IdPlan") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</fieldset>
<asp:Panel ID="pnlCrearConcepto" runat="server" ScrollBars="Auto" Width="800" BackColor="White" Height="250" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
    <table class="tablaPopup">
        <tr>
            <td>
                <uc2:UC_CrearConceptoCobro runat="server" ID="ucCrearConceptoCobro" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hfCrearConcepto" runat="server" />
<AspAjax:ModalPopupExtender ID="mpeCrearConcepto"
    runat="server"
    DropShadow="true"
    BackgroundCssClass="modalBackground"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    TargetControlID="hfCrearConcepto"
    PopupControlID="pnlCrearConcepto">
</AspAjax:ModalPopupExtender>