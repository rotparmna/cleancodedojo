<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dialogo.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Comun.Controles.Dialogo" %>

<div id="dialogo" title="<%$ Resources:GlobalWeb, Dialogo_Titulo %>" runat="server" style="display: none;">
    <p>
        <span style="float: left; margin: 0 7px 50px 0;">
            <asp:Image ID="imgOk" runat="server" ImageUrl="~/App_Themes/SAHI/images/ok16x16.png" />
            <asp:Image ID="imgError" runat="server" ImageUrl="~/App_Themes/SAHI/images/error16x16.png" />
        </span>
    </p>
    <div id="divMensaje"></div>
</div>