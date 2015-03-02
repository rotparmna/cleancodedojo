<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Comun.Controles.Reporte" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<rsweb:ReportViewer ID="rpvGenerico" AsyncRendering="true" runat="server" SizeToReportContent="true" ShowZoomControl="true" ShowBackButton="false" ShowPrintButton="true" ShowRefreshButton="false" Width="100%">
</rsweb:ReportViewer>