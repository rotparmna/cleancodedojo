// --------------------------------
// <copyright file="WebPage.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Paginas
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Controles;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Paginas.WebPage.
    /// </summary>
    public class WebPage : System.Web.UI.Page
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The ESTADOCUENTAGENERADO.
        /// </summary>
        private const string ESTADOCUENTAGENERADO = "EstadoCuentaGenerado";

        /// <summary>
        /// The VINCULACIONSELECCIONADA.
        /// </summary>
        private const string VINCULACIONSELECCIONADA = "VinculacionSeleccionada";

        #endregion Constantes 
        #region Variables 

        /// <summary>
        /// Variable privada que contiene el nombre del StyleSheetTheme a usar.
        /// </summary>
        private string styleSheetTheme = ConfigurationManager.AppSettings["Theme"];

        /// <summary>
        /// Variable privada que contiene el nombre del Theme a usar.
        /// </summary>
        private string theme = ConfigurationManager.AppSettings["Theme"];

        /// <summary>
        /// The web service.
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
        /// Enumeracion para tipo de mensaje.
        /// </summary>
        public enum TipoMensaje
        {
            /// <summary>
            /// Tipo de Mensaje OK.
            /// </summary>
            Ok,

            /// <summary>
            /// Tipo de Mensaje Error.
            /// </summary>
            Error
        }

        #endregion Enumeraciones 

        #region Propiedades 

        #region Propiedades Publicas 

/// <summary>
        /// Obtiene o establece la direccion IP del usuario.
        /// </summary>
        public string DireccionIPUsuario
        {
            get
            {
                return this.Request.UserHostAddress;
            }
        }

        /// <summary>
        /// Obtiene o establece estado cuenta generado.
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
        /// Obtiene o establece el login del usuario.
        /// </summary>
        public string LoginUsuario
        {
            get
            {
                return this.Page.User.Identity.Name;
            }
        }

        /// <summary>
        /// Obtiene o estable el StyleSheetTheme a aplicar en la pagina.
        /// </summary>
        /// <returns>El StyleSheetTheme a aplicar en la pagina</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.StyleSheetTheme"/> property is set before the <see cref="E:System.Web.UI.Control.Init"/> event completes.</exception>
        public override string StyleSheetTheme
        {
            get
            {
                return this.styleSheetTheme;
            }

            set
            {
                base.StyleSheetTheme = value;
            }
        }

        /// <summary>
        /// Obtiene o estable el tema a aplicar en la página.
        /// </summary>
        /// <returns>El nombre del tema de la página.</returns>
        /// <exception cref="T:System.InvalidOperationException">An attempt was made to set <see cref="P:System.Web.UI.Page.Theme"/> after the <see cref="E:System.Web.UI.Page.PreInit"/> event has occurred.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <see cref="P:System.Web.UI.Page.Theme"/> is set to an invalid theme name.</exception>
        public override string Theme
        {
            get
            {
                return this.theme;
            }

            set
            {
                base.Theme = value;
            }
        }

        /// <summary>
        /// Obtiene o establece web service.
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
        /// Obtiene o establece vinculacion seleccionada.
        /// </summary>
        protected Vinculacion VinculacionSeleccionada
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

        #endregion Propiedades Protegidas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

/// <summary>
        /// Obtiene el usuario Windows sin dominio.
        /// </summary>
        /// <returns>
        /// Retorna el Usuario Windows sin dominio.
        /// </returns>
        /// <remarks>
        /// Autor: INTERGRUPO\wvasquez
        /// FechaDeCreacion: Sep. 25 de 2012
        /// UltimaModificacionPor:
        /// FechaDeUltimaModificacion:
        /// EncargadoSoporte: INTERGRUPO\wvasquez
        /// Descripción: Obtiene el usuario Windows sin dominio.
        /// </remarks>
        public string ObtenerUsuarioWindowsSinDominio()
        {
            string usuarioLimpio;
            if (Request.ServerVariables["AUTH_USER"].ToString().Contains(@"\") == true)
            {
                usuarioLimpio = Request.ServerVariables["AUTH_USER"].ToString().Substring(Request.ServerVariables["AUTH_USER"].ToString().IndexOf(@"\") + 1);
            }
            else
            {
                usuarioLimpio = Request.ServerVariables["AUTH_USER"].ToString();
            }

            return usuarioLimpio;
        }

        /// <summary>
        /// Metodo encargado de realizar la aplicacion de permisos en los botones de las paginas.
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 24/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void ValidarPermisosPagina(Page pagina)
        {
            string permisos = Session[Resources.GlobalWeb.General_Session_PERMISOPAGINAUSUARIO] as string;

            if (!string.IsNullOrEmpty(permisos))
            {
                CargaObjetos.AplicarPermisos(pagina, permisos);
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
        /// Metodo encargado de aplicar los permisos por pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 20/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Master != null)
            {
                HiddenField hiddenfieldPermisos = Master.FindControl("PermisosGenerales") as HiddenField;
                if (hiddenfieldPermisos != null)
                {
                    CargaObjetos.AplicarPermisos(this.Page, hiddenfieldPermisos.Value);
                }
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Metodo encargado de realizar la aplicación de los permisos sobre la pagina a un usuario.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 23/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.Master != null)
            {
                this.ValidarPermisosPagina(this.Page);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Metodo para descargar la pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnUnload(EventArgs e)
        {
            this.WebService.Close();
            base.OnUnload(e);
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
        #region Metodos Privados Estaticos 

        /// <summary>
        /// Obtiene el objeto ordenado.
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto.</typeparam>
        /// <param name="objeto">The p objeto.</param>
        /// <param name="ordenarPor">The p ordenar por.</param>
        /// <returns>Retorna el objeto ordenado.</returns>
        /// <remarks>
        /// Autor: INTERGRUPO\wvasquez
        /// FechaDeCreacion: Sep. 25 de 2012
        /// UltimaModificacionPor:
        /// FechaDeUltimaModificacion:
        /// EncargadoSoporte: INTERGRUPO\wvasquez
        /// Descripción: Obtiene el objeto ordenado.
        /// </remarks>
        private static object GetPropertyValue<T>(T objeto, string ordenarPor)
        {
            PropertyInfo prop = objeto.GetType().GetProperty(ordenarPor);
            return prop.GetValue(objeto, null);
        }

        #endregion Metodos Privados Estaticos 

        #endregion Metodos 
    }
}