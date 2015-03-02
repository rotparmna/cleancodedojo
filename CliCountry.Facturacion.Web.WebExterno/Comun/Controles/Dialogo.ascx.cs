// --------------------------------
// <copyright file="Dialogo.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Controles
{
    using System;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Controles.Dialogo
    /// </summary>
    public partial class Dialogo : WebUserControl
    {
        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Metodo para realizar la visualizacion del mensaje en un popup
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="mensaje">The mensaje.</param>
        /// <param name="clase">The clase.</param>
        /// <param name="tipo">Parámetro tipo.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void MostrarMensaje(Page pagina, string mensaje, string clase, short tipo)
        {
            var mensajeScript = string.Format(Resources.GlobalWeb.Dialogo_Mensaje, clase, mensaje);
            var script = string.Format(Resources.GlobalWeb.Dialogo_JavaScript, mensajeScript, tipo);
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        #endregion Metodos Publicos

        #region Metodos Protegidos

        /// <summary>
        /// Metodo para controlar la carga la pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos

        #endregion Metodos
    }
}