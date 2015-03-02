<%@ Page Culture="es-CO" Async="true" Title="Facturación por Actividades" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorFacActividad.aspx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Facturacion.AdministradorFacActividad" %>

<%@ Register Src="~/Comun/Controles/UC_Paginacion.ascx" TagPrefix="uc2" TagName="UCPaginacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_CrearCortesFacturacion.ascx" TagPrefix="uc1" TagName="UC_CrearCortesFacturacion" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_Exclusiones.ascx" TagPrefix="uc1" TagName="UC_Exclusiones" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_DefinirCondiciones.ascx" TagPrefix="uc1" TagName="UC_DefinirCondiciones" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarTercero.ascx" TagPrefix="uc1" TagName="UC_BuscarTercero" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarCliente.ascx" TagPrefix="uc1" TagName="UC_BuscarCliente" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_VincularVenta.ascx" TagPrefix="uc1" TagName="UC_VincularVenta" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_VincularEntidad.ascx" TagPrefix="uc1" TagName="UC_VincularEntidad" %>
<%@ Register Src="~/Facturacion/ControlesUsuarioFacturacion/UC_BuscarClienteTercero.ascx" TagPrefix="uc1" TagName="UC_BuscarClienteTercero" %>
<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedorPrincipal" runat="server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Pager span {
            text-align: center;
            color: #999;
            display: inline-block;
            width: 20px;
            background-color: #A1DCF2;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #3AC0F2;
        }

        .Pager a {
            text-align: center;
            display: inline-block;
            width: 20px;
            background-color: #3AC0F2;
            color: #fff;
            border: 1px solid #3AC0F2;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }
        .imgConsultaAtencion
        {}
    </style>
    <script type="text/javascript">
        var tabSeleccionado = 0;
        function pageLoad(sender, args) {
            $('[id$=chkFacturarTodos]').prop('checked', MarcarTodosCheckBox('chkFacturar'));
            OrdenarGrilla('grvEntidades');
            OrdenarGrilla('grvVentas');
            if (args.get_isPartialLoad()) {
                $("#tabs").tabs({
                    activate: function () {
                        tabSeleccionado = $('#tabs').tabs('option', 'active');
                    }, active: tabSeleccionado
                });
            }
        };

        // Función de validación de longitud
        function checkMaxLen(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) {
                    var cont = txt.value;
                    txt.value = cont.substring(0, (maxLen - 1));
                    return false;
                };
            } catch (e) {
            }
        }

        // Función de tecla enter en TxtAtencion
        function TxtAtencion_keydown(source, e) {
            teclaPulsada = e.which == undefined ? e.keyCode : e.which;
            if (teclaPulsada == 13) {
                $('.imgConsultaAtencion').click();
                return false;
            }
            return true;
        }

        /* Funcion Para Controlar la Seleccion de uno de los Check */
        function Check_Click(event) {
            source = event.srcElement;
            fila = $(source).parent().parent().parent()[0];
            if (source.id.indexOf("chkActivo") != -1) {
                checkGenerar = $('[id*="chkGenerar"]', fila)[0];
                checkGenerar.disabled = !source.checked;
                checkGenerar.checked = source.checked;
            }
        }

        // Función para validarlos saltos de los registros de vinculaciones
        function ValidarConsecutivo() {
            gridEntidades = $('[id$=grvEntidades]');
            controlOrden = $(gridEntidades).find('[id*=txtOrden]');
            controlActivo = $(gridEntidades).find('[id*=chkActivo]');
            var campoOrigen = true;
            var retorno = true;

            if (controlOrden != null && controlOrden.length > 0) {
                for (i = 0; i < controlOrden.length; i++) {
                    var posicion = BuscarPosicionNumero(controlOrden, i);

                    if (posicion == -1) {
                        retorno = false;
                        break;
                    }

                    orden = $(controlOrden[posicion]).val();
                    activo = $(controlActivo[posicion])[0].checked;

                    if (orden == i + 1) {
                        if (activo == false) {
                            campoOrigen = activo;
                        }
                        if (activo == true && campoOrigen == false) {
                            retorno = false;
                            break;
                        }
                    }
                    else {
                        retorno = false;
                        break;
                    }
                }
                if (!retorno) {
                    $('#lblMensajeOculto').hide();
                    alert($('[id$=hfMensajeOculto]').val());
                }
                return retorno;
            }
        }

        // Busca la posición del número de orden
        function BuscarPosicionNumero(objetoOrdenes, posicion) {
            var retorno = -1;

            for (k = 0; k < objetoOrdenes.length; k++) {
                if ($(objetoOrdenes[k]).val() == (posicion + 1)) {
                    retorno = k;
                    break;
                }
            }
            return retorno;
        }

        function MostrarMensajeError(msg)
        {
            MostrarMensaje(msg, '1');
        }

    </script>

    <script type='text/javascript'>
        //Maps an event to the window closing event
        var ideate;
        function idatencion() {
            ideate = document.getElementById('<%= txtAtencion.ClientID %>').value;
        }

        window.onbeforeunload = function (e) {
            if (ideate == null) {
                return;
            }
            var usuario = '<%= Context.User.Identity.Name %>';
            var filtrar = '{idatencion: ' + ideate + ', usuario:\'' + usuario + '\'}';

            $.ajax({
                type: "POST",
                url: "AdministradorFacActividad.aspx/DesbloquearAtencion",
                data: filtrar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false
            });
        };

    </script>

    <%--<script type="text/javascript" src="../App_Themes/SAHI/scripts/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../App_Themes/SAHI/scripts/PaginarGrillaJson.js"></script>
    <script type="text/javascript">
        function msg() {
            $find('contenedorPrincipal_mpeVincularentidad').show();
            filtra("company: 'an'");;
        };
    </script>
    <asp:MultiView ID="mltvFacAct" runat="server" ActiveViewIndex="0">
        <asp:View ID="vFacAct" runat="server">
            <div id="contenedorControl">
                <div class="Header">
                    <asp:Label ID="lblTitulo" CssClass="LabelTitulo" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_Titulo %>"></asp:Label>
                </div>
                <asp:UpdatePanel ID="upFacturacionActividad" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="height: 50px;">
                                    <div style="text-align: right; vertical-align: middle;">
                                        <asp:Label SkinID="LabelCampo" ID="lblConsultarEstado" runat="server"
                                            Text="<%$ Resources:GlobalWeb, General_BotonEstadoCuenta %>"
                                            Style="margin-right: 20px;" />
                                        <asp:ImageButton ID="ImgGenerarEstadoCuenta" runat="server"
                                            ImageUrl="~/App_Themes/SAHI/images/procesar.png"
                                            OnClick="ImgGenerarEstadoCuenta_Click" 
                                            ValidationGroup="ValidarCampoEstadoCuenta"
                                            ToolTip="<%$ Resources:GlobalWeb, General_BotonEstadoCuenta %>" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="LblMovimientosActivos" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_MovimientosActivos %>" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkMovimiento" Enabled="false" runat="server" />
                                    &nbsp;&nbsp;&nbsp;
                                       <asp:ImageButton ID="ImgGuardarMovimiento"
                                           runat="server"
                                           Enabled="false"
                                           ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                                           ToolTip="<%$ Resources:AdministradorFacActividades, FacActividad_GuardarMovimientos %>"
                                           OnClick="ImgGuardarMovimiento_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblAtencion" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_Atencion %>" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAtencion"
                                        runat="server" onKeyDown="return TxtAtencion_keydown(this,event);"
                                        MaxLength="8" Width="150px"></asp:TextBox>
                                    <AspAjax:FilteredTextBoxExtender ID="validaAtencion" runat="server" TargetControlID="txtAtencion" FilterType="Numbers" />
                                    <asp:RequiredFieldValidator ID="rqIdAtencion"
                                        ValidationGroup="ValidarCamposActividades"
                                        runat="server"
                                        ControlToValidate="txtAtencion"
                                        Text="*"
                                        ErrorMessage="Prueba" />
                                    <asp:ImageButton ID="imgConsultaAtencion" runat="server"
                                        ImageUrl="~/App_Themes/SAHI/images/search.png"
                                        CssClass="imgConsultaAtencion"
                                        CausesValidation="true"
                                        ToolTip="<%$ Resources:GlobalWeb, General_Buscar %>"
                                        ValidationGroup="ValidarCamposActividades"
                                        OnClick="ImgConsultaAtencion_Click" Height="20px" Width="20px" OnClientClick="idatencion()" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblPaciente" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_Paciente %>" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPaciente" ToolTip="El campo es Obligatorio" runat="server" ValidationGroup="ValidarFactura" MaxLength="100" Width="270px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblNoDocumento" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_NoDocumento %>" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoDocumento" runat="server" ValidationGroup="ValidarFactura" MaxLength="12" Width="150px" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RfvTxtNoDocumento"
                                        ValidationGroup="ValidarCampoEstadoCuenta"
                                        runat="server"
                                        ControlToValidate="txtNoDocumento"
                                        ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                                        ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblTipoAtencion" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_TipoAtencion %>" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTipoAtencion" runat="server" ValidationGroup="ValidarFactura" MaxLength="100" Width="270px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblSeccion" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_Seccion %>" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSeccion" runat="server"
                                        Width="240px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqSeccion"
                                        ValidationGroup="ValidarCamposActividades"
                                        runat="server"
                                        ControlToValidate="ddlSeccion"
                                        ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                                        ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                                        InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblTipoMoviFactura" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_TipoMovimientoFactura %>" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoMovimiento" runat="server"
                                        Width="240px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqTipoMovimiento"
                                        ValidationGroup="ValidarCamposActividades"
                                        runat="server"
                                        ControlToValidate="ddlTipoMovimiento"
                                        ErrorMessage="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                                        ToolTip="<%$ Resources:GlobalWeb, General_ValorCampoObligatorio %>"
                                        InitialValue="<%$ Resources:GlobalWeb, General_ComboItemValor %>">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label SkinID="LabelCampo" ID="lblFormato" runat="server" Text="<%$ Resources:AdministradorFacRelacion, FacRelacion_Formato %>" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlFormato" runat="server"
                                        Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <br />
            <div class="DivTabs" id="divTabsMarco" runat="server" visible="false">
                <div id="tabs">
                    <ul>
                        <li>
                            <a href="#tabs-1">
                                <asp:Literal runat="server" Text="<%$ Resources:AdministradorFacActividades,  FacActividad_TabEntidades %>" />
                            </a>
                            <br />
                        </li>
                        <li>
                            <a href="#tabs-2">
                                <asp:Literal runat="server" Text="<%$ Resources:AdministradorFacActividades,  FacActividad_TabVentas %>" />
                            </a>
                        </li>
                        <li>
                            <a href="#tabs-3">
                                <asp:Literal runat="server" Text="<%$ Resources:AdministradorFacActividades,  FacActividad_TabResponsable %>" />
                            </a>
                        </li>
                    </ul>

                    <div id="tabs-1">
                        <table id="tblComandosEntidades" class="tblComandos">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgbtnVincularEntidad" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                                        Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb,  Global_VincularEntidad %>" OnClick="ImgbtnVincularEntidad_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb,  Global_VincularEntidad %>" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImgDefinirExclusiones" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                                        Height="20px" Width="20px" OnClick="ImgDefinirExclusiones_Click" ToolTip="<%$ Resources:DefinirExclusiones,  DefinirExclusiones_Titulo %>" />
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" SkinID="LabelCampo" Text="<%$ Resources:DefinirExclusiones,  DefinirExclusiones_Titulo %>" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImgDefinirCondiciones" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                                        Height="20px" Width="20px" OnClick="ImgDefinirCondiciones_Click" ToolTip="<%$ Resources:GlobalWeb,  General_BotonDefinirCondiciones %>" />
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb,  General_BotonDefinirCondiciones %>" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div>
                            <asp:HiddenField ID="hfMensajeOculto" runat="server" Value="<%$ Resources:VincularEntidad, VincularEntidad_MsjError %>" />
                        </div>
                        <div id="divEntidades">
                            <br />
                            <asp:Label ID="lblCantidadRegistros" SkinID="LabelCampo" runat="server" />
                            <br />
                            <uc2:UCPaginacion runat="server" ID="pagControl" Visible="false" />
                            <asp:GridView ID="grvEntidades" runat="server" AutoGenerateColumns="False" CssClass="AspNet-GridView"
                                OnRowCommand="GrvEntidades_RowCommand">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSelect" runat="server" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>'
                                                CommandName="Select"
                                                ToolTip="<%$ Resources:GlobalWeb, ComandoSeleccionar %>"
                                                ImageUrl="~/App_Themes/SAHI/images/seleccionar.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="Centrado" ItemStyle-Width="30px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblGenerar" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_GrvChkGenerar %>" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkGenerar" CssClass="chkGenerar" Checked='<%# Convert.ToBoolean(Eval("IndHabilitado")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                        <ItemStyle CssClass="Centrado" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="Centrado">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblActivo" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_GrvChkActivo %>" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkActivo" Checked='<%# Convert.ToBoolean(Eval("IndHabilitado")) %>' CssClass="chkActivo" onclick="Check_Click(event);" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Centrado" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_Entidad %>" DataField="Tercero.Nombre" SortExpression="Tercero.Nombre">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_Orden %>" SortExpression="Orden" ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrden" runat="server" Text='<%# Eval("Orden") %>' MaxLength="1" Width="20px" />
                                            <AspAjax:FilteredTextBoxExtender ID="validaOrden" runat="server" TargetControlID="txtOrden" FilterType="Numbers" InvalidChars="0" />
                                            <asp:RequiredFieldValidator ID="rvOrden" runat="server" ErrorMessage="*" ControlToValidate="txtOrden"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="reOrden" runat="server" ErrorMessage="*" ControlToValidate="txtOrden" ValidationExpression="^[1-9]{1,5}(\.[1-9])?$"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_Contrato %>" DataField="Contrato.Nombre" SortExpression="Contrato.Nombre">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_Plan %>" DataField="Plan.Nombre" SortExpression="Plan.Nombre">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_IdPlan %>" DataField="Plan.Id" SortExpression="Plan.Id">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvEntidad_Observacion %>" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObservacion" runat="server" Text='<%# Eval("Observacion") %>' TextMode="MultiLine" MaxLength="256" onKeyUp="return checkMaxLen(this,256)" Width="140px" Height="35px" />
                                        </ItemTemplate>
                                        <ItemStyle Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdEntidad" runat="server" Text='<%# Eval("Tercero.Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdContrato" runat="server" Text='<%# Eval("Contrato.Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMontoEjecutado" runat="server" Text='<%# Eval("MontoEjecutado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdTipoAfiliacion" runat="server" Text='<%# Eval("IdTipoAfiliacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="LightGray" />
                            </asp:GridView>
                            <br />
                        </div>
                        <div>
                            <asp:ImageButton ID="imgActualizarVinculaciones" runat="server"
                                ImageUrl="~/App_Themes/SAHI/images/guardarItem.png"
                                Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, Vinculacion_GuardarCambios %>"
                                OnClick="ImgActualizarVinculaciones_Click"
                                OnClientClick="return ValidarConsecutivo();" />
                            <asp:Label ID="lblVinculaciones" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb, Vinculacion_GuardarCambios %>" />
                        </div>
                    </div>
                    <div id="tabs-2">
                        <table id="tblComandosVentas" class="tblComandos">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImgCortesFacturacion" runat="server"
                                        ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                                        Height="20px" Width="20px" ToolTip="<%$ Resources:GlobalWeb, General_BotonCortesFacturacion %>" OnClick="ImgCortesFacturacion_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelCampo" Text="<%$ Resources:GlobalWeb,  General_BotonCortesFacturacion %>" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImgVincularVentas" runat="server" ImageUrl="~/App_Themes/SAHI/images/condicionFacturacion.png"
                                        ToolTip="<%$ Resources:ControlesUsuario,  Ventas_Titulo %>"
                                        Height="20px" Width="20px" OnClick="ImgVincularVentas_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" SkinID="LabelCampo" Text="<%$ Resources:ControlesUsuario,  Ventas_Titulo %>" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div id="divVentas" style="max-height: 300px; min-height: 50px; overflow-x: hidden; overflow-y: scroll;">
                            <asp:GridView ID="grvVentas" runat="server" AutoGenerateColumns="False" CssClass="AspNet-GridView" DataKeyNames="NumeroVenta">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkFacturarTodos"
                                                onclick="CheckTodos_Click(event, 'chkFacturar');"
                                                runat="server" />
                                            <asp:Label ID="lblFacturar" runat="server" Text="<%$ Resources:AdministradorFacActividades, FacActividad_GrvChkFacturar %>" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFacturar" runat="server"
                                                onclick="CheckBox_Click(event, 'chkFacturar');"
                                                Checked='<%# Convert.ToBoolean(Eval("Facturar")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Centrado" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_Transaccion %>"
                                        DataField="NombreTransaccion"
                                        SortExpression="NombreTransaccion" />
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_NumeroVenta %>"
                                        DataField="NumeroVenta"
                                        SortExpression="NumeroVenta" />
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_FechaVenta %>"
                                        DataField="FechaVenta"
                                        ItemStyle-CssClass="Centrado"
                                        DataFormatString="{0:d}"
                                        SortExpression="FechaVenta" />
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_UbicacionConsumo %>"
                                        DataField="NombreUbicacionConsumo"
                                        SortExpression="NombreUbicacionConsumo" />
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_UbicacionEntrega %>"
                                        DataField="NombreUbicacionEntrega"
                                        SortExpression="NombreUbicacionEntrega" />
                                    <asp:BoundField HeaderText="<%$ Resources:AdministradorFacActividades, FacActividad_GrvVentas_AtencionEnlazada %>"
                                        DataField="IdAtencion"
                                        ItemStyle-CssClass="Numero"
                                        SortExpression="IdAtencion" />
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                    </div>
                    <div id="tabs-3">
                        <uc1:UC_BuscarClienteTercero runat="server" ID="ucBuscarClienteTercero" EnableTheming="True" />
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="vDefinirCondiciones" runat="server">
            <asp:UpdatePanel ID="updDefinirCondiciones" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:UC_DefinirCondiciones runat="server" ID="ucDefinirCondiciones" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="pnlExclusiones" runat="server" ScrollBars="Auto" Width="1080" BackColor="White" Height="500" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
        <table class="tablaPopup">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" OnClick="BtnCerrarPopupExclusiones_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:UC_Exclusiones runat="server" ID="ucExclusiones" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfExclusion" runat="server" />
    <AspAjax:ModalPopupExtender ID="mpeExclusiones"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfExclusion"
        PopupControlID="pnlExclusiones">
    </AspAjax:ModalPopupExtender>

    <asp:Panel ID="pnlCortes" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
        <table class="tablaPopup">
            <tr>
                <td>
                    <uc1:UC_CrearCortesFacturacion runat="server" ID="ucCortesFacturacion" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnCerrarCorte" runat="server" Text="<%$ Resources:GlobalWeb, General_BotonCerrar %>" OnClick="BtnCerrarCorte_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfCortes" runat="server" />
    <AspAjax:ModalPopupExtender ID="mpeCortes"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfCortes"
        PopupControlID="pnlCortes">
    </AspAjax:ModalPopupExtender>

    <asp:Panel ID="pnlDefinirCondiciones" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
        <table class="tablaPopup">
            <tr>
                <td style="text-align: right;">
                    <asp:ImageButton ID="ImgSalirDefinirCondiciones" runat="server" Width="20px" Height="20px" ImageUrl="~/App_Themes/SAHI/images/imgClosePopupSL.png" ToolTip="<%$ Resources:GlobalWeb, General_BotonSalir %>" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:UC_DefinirCondiciones runat="server" ID="UC_DefinirCondiciones" />
                </td>
            </tr>
            <tr>
                <td align="center"></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfDefinirCondiciones" runat="server" />
    <AspAjax:ModalPopupExtender ID="mepDefinirCondiciones"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfDefinirCondiciones"
        PopupControlID="pnlDefinirCondiciones">
    </AspAjax:ModalPopupExtender>

    <asp:Panel ID="pnlVentas" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
        <table class="tablaPopup">
            <tr>
                <td>
                    <uc1:UC_VincularVenta runat="server" ID="ucVinculacionVenta" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfVentas" runat="server" />
    <AspAjax:ModalPopupExtender ID="mpeVentas"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfVentas"
        PopupControlID="pnlVentas">
    </AspAjax:ModalPopupExtender>

    <asp:Panel ID="pnlVincularentidad" runat="server" ScrollBars="Auto" Width="1000" BackColor="White" Height="600" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
        <table class="tablaPopup">
            <tr>
                <td>
                    <uc1:UC_VincularEntidad runat="server" ID="ucVincularEntidad" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfVincularentidad" runat="server" />
    <AspAjax:ModalPopupExtender ID="mpeVincularentidad"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfVincularentidad"
        PopupControlID="pnlVincularentidad">
    </AspAjax:ModalPopupExtender>

    <asp:Panel ID="pnlConfirmar" runat="server" ScrollBars="Auto" Width="320" BackColor="White" Height="140" Style="display: none; margin-right: 5px; padding: 10px; border: 1px; border-style: solid;">
          <table style="vertical-align:middle">
              <tr>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td align="center"><asp:Label ID="lblConfirmacion" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td align="center"><asp:Label ID="lblConfirmacionDos" runat="server" Text="Haga click en Confirmar para desbloquear."></asp:Label></td>
              </tr>
              <tr>
                  <td>&nbsp;</td>
              </tr>
              <tr>
                  <td align="center">
                      <table style="align-content:center">
                          <tr>
                              <td><asp:Button ID="btn_confirm" runat="server" CssClass="btnGeneral" Text="Confirmar" Width="100px" OnClick="Btn_confirm_Click" /></td>
                              <td><asp:Button ID="btn_cancel" runat="server" CssClass="btnGeneral" Text="Cancelar" Width="100px"/></td>
                          </tr>
                      </table>
                  </td>                 
              </tr>
          </table>    
    </asp:Panel>
    <asp:HiddenField ID="hfConfirmar" runat="server" />
    <AspAjax:ModalPopupExtender ID="mpeConfirmar"
        runat="server"
        DropShadow="true"
        BackgroundCssClass="modalBackground"
        RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hfConfirmar"
        PopupControlID="pnlConfirmar">
    </AspAjax:ModalPopupExtender>

</asp:Content>