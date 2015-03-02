<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Paginacion.ascx.cs" Inherits="CliCountry.Facturacion.Web.WebExterno.Comun.Controles.UC_Paginacion" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="paginador">
            <asp:Button runat="server" class="hidden" ID="LnkCambioPagina" OnClick="LnkCambioPagina_Click" />
            <asp:HiddenField ID="hfPaginaCambio" runat="server" />
            <span runat="server" class="LabelCampo" id="lblPaginaActual"></span>
            <asp:ImageButton ID="ImgPrimeraPag" runat="server" ImageUrl="~/App_Themes/SAHI/images/pagPrimera.png"
                OnClick="ImgPrimeraPag_Click"
                ToolTip="<%$ Resources:GlobalWeb, Paginador_Primera %>" />
            <asp:ImageButton ID="ImgPagAnterior" runat="server" ImageUrl="~/App_Themes/SAHI/images/pagPrimera.png"
                OnClick="ImgPagAnterior_Click"
                ToolTip="<%$ Resources:GlobalWeb, Paginador_Anterior %>" />
            <div id="controlPaginas" runat="server" />
            <asp:ImageButton ID="ImgPagSiguiente" runat="server" ImageUrl="~/App_Themes/SAHI/images/pagSiguiente.png"
                OnClick="ImgPagSiguiente_Click"
                ToolTip="<%$ Resources:GlobalWeb, Paginador_Siguiente %>" />
            <asp:ImageButton ID="ImgUltimaPag" runat="server" ImageUrl="~/App_Themes/SAHI/images/pagUltima.png"
                OnClick="ImgUltimaPag_Click"
                ToolTip="<%$ Resources:GlobalWeb, Paginador_Ultima %>" />
            <span class="LabelCampo" id="LblTotalRegistros" runat="server"></span>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
