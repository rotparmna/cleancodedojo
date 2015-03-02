// --------------------------------
// <copyright file="WebService.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------

namespace CliCountry.Facturacion.Web.WebExterno.Comun.Clases
{
    using CliCountry.Facturacion.Negocio.Controlador;
    using CliCountry.SAHI.Comun.AuditoriaBase;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Utilidades.WebService
    /// </summary>
    public class WebService
    {
        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Web.WebExterno.Comun.Clases.WebService" />
        /// </summary>
        /// <param name="nombreUsuario">The nombre usuario.</param>
        public WebService(string nombreUsuario)
        {
            DatosUsuario datosUsuario = new DatosUsuario();
            datosUsuario.NombreUsuario = nombreUsuario;
            datosUsuario.DireccionIp = Extension.ObtenerDireccionIP();

            if (this.Facturacion == null)
            {
                this.Facturacion = new ControlFacturacion();
            }

            if (this.Integracion == null)
            {
                this.Integracion = new ControlIntegracion();
            }

            if (this.Configuracion == null)
            {
                this.Configuracion = new ControlConfiguracion();
            }
        }

        #endregion Constructores

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece configuracion
        /// </summary>
        public ControlConfiguracion Configuracion { get; private set; }

        /// <summary>
        /// Obtiene o establece facturacion
        /// </summary>
        public ControlFacturacion Facturacion { get; private set; }

        /// <summary>
        /// Obtiene o establece integracion
        /// </summary>
        public ControlIntegracion Integracion { get; private set; }

        #endregion Propiedades Publicas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Se encarga de cerrar las instancias de los servicios
        /// </summary>
        /// <remarks>
        /// Autor: William Vásquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 01/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void Close()
        {
            
        }

        #endregion Metodos Publicos

        #endregion Metodos
    }
}