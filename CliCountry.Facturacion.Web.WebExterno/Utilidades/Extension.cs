// --------------------------------
// <copyright file="Extension.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Utilidades
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using AjaxControlToolkit;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Utilidades.Extension
    /// </summary>
    public static class Extension
    {
        #region Metodos

        #region Metodos Publicos Estaticos

        /// <summary>
        /// Metodo para obtener un control por Tipo.
        /// </summary>
        /// <param name="controlPrincipal">The control principal.</param>
        /// <returns>
        /// Contenedor Principal.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static ModalPopupExtender BuscarModalPopup(this Control controlPrincipal)
        {
            IEnumerable<ModalPopupExtender> modalPopup = null;

            if (controlPrincipal.Parent != null)
            {
                string identificadorContenedor = controlPrincipal.Parent.ID;

                modalPopup = from
                                  control in controlPrincipal.NamingContainer.Controls.Cast<Control>()
                              where
                                  control.GetType().Equals(typeof(ModalPopupExtender))
                                  && (control as ModalPopupExtender).PopupControlID == identificadorContenedor
                              select
                                  (ModalPopupExtender)control;
            }

            return modalPopup != null ? modalPopup.FirstOrDefault() : null;
        }

        /// <summary>
        /// Metodo para obtener un control por Tipo.
        /// </summary>
        /// <param name="controlPrincipal">The control principal.</param>
        /// <returns>
        /// Contenedor Principal.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static ModalPopupExtender BuscarModalPopupRecursivo(this Control controlPrincipal)
        {
            string identificadorContenedor = controlPrincipal.NamingContainer.Parent.ID;

            var modalPopup = from
                                 control in controlPrincipal.NamingContainer.NamingContainer.Controls.Cast<Control>()
                              where
                                  control.GetType().Equals(typeof(ModalPopupExtender))
                                  && (control as ModalPopupExtender).PopupControlID == identificadorContenedor
                              select
                                  (ModalPopupExtender)control;

            return modalPopup.FirstOrDefault();
        }

        #endregion Metodos Publicos Estaticos

        #endregion Metodos
    }
}