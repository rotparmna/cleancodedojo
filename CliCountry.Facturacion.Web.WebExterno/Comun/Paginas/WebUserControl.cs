// --------------------------------
// <copyright file="WebUserControl.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ----------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Paginas
{
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Controles;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.SAHI.WebInterno.Comun.Paginas.WebUserControl
    /// </summary>
    public class WebUserControl : System.Web.UI.UserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The ESTADOCUENTAGENERADO
        /// </summary>
        private const string ESTADOCUENTAGENERADO = "EstadoCuentaGenerado";

        /// <summary>
        /// The MANUALSELECCIONADO
        /// </summary>
        private const string MANUALSELECCIONADO = "TarifaSeleccionada";

        /// <summary>
        /// The VINCULACIONSELECCIONADA
        /// </summary>
        private const string VINCULACIONSELECCIONADA = "VinculacionSeleccionada";

        #endregion Constantes 
        #region Variables 

        /// <summary>
        /// The web service
        /// </summary>
        private WebService webService = null;

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado para evento de Crear Tercero.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        public delegate void OnOperacionEjecutada(object sender, SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on operacion ejecutada.
        /// </summary>
        public event OnOperacionEjecutada OperacionEjecutada;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Enumeraciones 

        /// <summary>
        /// Enumeracion para tipo de mensaje
        /// </summary>
        public enum TipoMensaje
        {
            /// <summary>
            /// Opción ok.
            /// </summary>
            Ok,

            /// <summary>
            /// Opción error.
            /// </summary>
            Error
        }

        #endregion Enumeraciones 

        #region Propiedades 

        #region Propiedades Publicas 

/// <summary>
        /// Obtiene o establece estado cuenta generado
        /// </summary>
        public List<EstadoCuentaEncabezado> EstadoCuentaGenerado
        {
            get
            {
                return this.Session[ESTADOCUENTAGENERADO] == null ? new List<EstadoCuentaEncabezado>() : this.Session[ESTADOCUENTAGENERADO] as List<EstadoCuentaEncabezado>;
            }

            set
            {
                this.Session[ESTADOCUENTAGENERADO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la referencia al objeto WebPage
        /// </summary>
        public WebPage PaginaWeb
        {
            get
            {
                return this.Page as WebPage;
            }
        }

        /// <summary>
        /// Obtiene o establece vinculacion seleccionada
        /// </summary>
        public Vinculacion VinculacionSeleccionada
        {
            get
            {
                return this.Session[VINCULACIONSELECCIONADA] == null ? new Vinculacion() : this.Session[VINCULACIONSELECCIONADA] as Vinculacion;
            }

            set
            {
                this.Session[VINCULACIONSELECCIONADA] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece web service
        /// </summary>
        public WebService WebService
        {
            get
            {
                if (this.webService == null)
                {
                    string nombreUsuario = Context.User.Identity.Name;
                    this.webService = new WebService(nombreUsuario);
                }

                return this.webService;
            }

            private set
            {
                this.webService = value;
            }
        }

        #endregion Propiedades Publicas 
        #region Propiedades Protegidas 

        /// <summary>
        /// Obtiene o establece tarifa seleccionada
        /// </summary>
        protected TarifasManual TarifaSeleccionada
        {
            get
            {
                return this.Session[MANUALSELECCIONADO] == null ? new TarifasManual() : this.Session[MANUALSELECCIONADO] as TarifasManual;
            }

            set
            {
                this.Session[MANUALSELECCIONADO] = value;
            }
        }

        #endregion Propiedades Protegidas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

/// <summary>
        /// Recargar Modal Popup.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void RecargarModal()
        {
            bool existeNivel = false;
            ModalPopupExtender modalPopup = null;
            for (int numero = 1; numero <= 4; numero++)
            {
                modalPopup = this.BuscarModal(numero, out existeNivel);

                if (modalPopup != null || !existeNivel)
                {
                    break;
                }
            }

            if (modalPopup != null)
            {
                modalPopup.Show();
            }
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para Visualizar el Mensaje.
        /// </summary>
        /// <param name="mensaje">The mensaje.</param>
        /// <param name="clase">The clase.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 01/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void MostrarMensaje(string mensaje, TipoMensaje clase)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                mensaje = mensaje.Replace("'", string.Empty);

                short identificadorTipo = 0;
                string estilo = string.Empty;

                switch (clase)
                {
                    case TipoMensaje.Ok:
                        estilo = Resources.GlobalWeb.Estilo_MensajeOK;
                        break;

                    case TipoMensaje.Error:
                        estilo = Resources.GlobalWeb.Estilo_MensajeERROR;
                        identificadorTipo = 1;
                        break;
                }

                var dialogo = this.Page.Form.FindControl("Dialogo") as Dialogo;

                if (dialogo != null)
                {
                    dialogo.MostrarMensaje(this.Page, mensaje, estilo, identificadorTipo);
                }
            }
        }

        /// <summary>
        /// Evento para indicar el estado de la carga.
        /// </summary>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 15/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ResultadoEjecucion(SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion)
        {
            if (this.OperacionEjecutada != null)
            {
                this.OperacionEjecutada(this, tipoOperacion);
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Recargar Modal Popup.
        /// </summary>
        /// <param name="nivel">The nivel.</param>
        /// <param name="existeNivel">if set to <c>true</c> [existe nivel].</param>
        /// <returns>Modal Popup Extender.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private ModalPopupExtender BuscarModal(int nivel, out bool existeNivel)
        {
            ModalPopupExtender modalPopup = null;
            existeNivel = true;

            switch (nivel)
            {
                case 1:
                    modalPopup = this.BuscarModalPopup();
                    break;

                case 2:
                    if (this.NamingContainer != null)
                    {
                        modalPopup = this.NamingContainer.BuscarModalPopup();
                    }
                    else
                    {
                        existeNivel = false;
                    }

                    break;

                case 3:
                    if (this.NamingContainer.NamingContainer != null)
                    {
                        modalPopup = this.NamingContainer.NamingContainer.BuscarModalPopup();
                    }
                    else
                    {
                        existeNivel = false;
                    }

                    break;

                case 4:
                    if (this.NamingContainer.NamingContainer.NamingContainer != null)
                    {
                        modalPopup = this.NamingContainer.NamingContainer.NamingContainer.BuscarModalPopup();
                    }
                    else
                    {
                        existeNivel = false;
                    }

                    break;
            }

            return modalPopup;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}