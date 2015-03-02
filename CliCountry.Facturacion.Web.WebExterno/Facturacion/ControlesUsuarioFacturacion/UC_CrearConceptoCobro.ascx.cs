// ---------------------------------
// <copyright file="UC_CrearConceptoCobro.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearConceptoCobro.
    /// </summary>
    public partial class UC_CrearConceptoCobro : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The ABONO
        /// </summary>
        private const string ABONO = "ABON";

        /// <summary>
        /// The CUENTASELECCIONADO
        /// </summary>
        private const string CUENTASELECCIONADO = "EstadoCuentaSeleccionado";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece estado cuenta seleccionado
        /// </summary>
        public EstadoCuentaEncabezado EstadoCuentaSeleccionado
        {
            get
            {
                return ViewState[CUENTASELECCIONADO] == null ? new EstadoCuentaEncabezado() : ViewState[CUENTASELECCIONADO] as EstadoCuentaEncabezado;
            }

            set
            {
                ViewState[CUENTASELECCIONADO] = value;
            }
        }

        #endregion Propiedades Publicas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Carga la información en los campos al formulario.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autór: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarInformacionEstadoCuenta(EstadoCuentaEncabezado estadoCuenta)
        {
            TxtEntidad.Text = estadoCuenta.NombreTercero;
            TxtContrato.Text = estadoCuenta.DescripcionContrato;
            TxtPlan.Text = estadoCuenta.NombrePlan;
            TxtAtencion.Text = estadoCuenta.IdAtencion.ToString();
        }

        /// <summary>
        /// Inicia los campos del formulario.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            TxtEntidad.Text = string.Empty;
            TxtContrato.Text = string.Empty;
            TxtPlan.Text = string.Empty;
            TxtAtencion.Text = string.Empty;
            TxtValorConcepto.Text = string.Empty;
        }

        #endregion Metodos Publicos

        #region Metodos Protegidos

        /// <summary>
        /// Guarda la información del concepto de cobro.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgGuardarConcepto_Click(object sender, ImageClickEventArgs e)
        {
            ConceptoCobro concepto = new ConceptoCobro()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdAtencion = string.IsNullOrEmpty(TxtAtencion.Text) ? 0 : Convert.ToInt32(TxtAtencion.Text),
                IdContrato = EstadoCuentaSeleccionado.IdContrato,
                IdPlan = EstadoCuentaSeleccionado.IdPlan,
                FechaConcepto = DateTime.Now,
                CodigoConcepto = ABONO,
                ValorConcepto = string.IsNullOrEmpty(TxtValorConcepto.Text) ? 0 : Convert.ToDecimal(TxtValorConcepto.Text),
                IndHabilitado = 1,
                ValorContrato = 0,
                Porcentaje = 0,
                ValorSaldo = string.IsNullOrEmpty(TxtValorConcepto.Text) ? 0 : Convert.ToDecimal(TxtValorConcepto.Text),
                ValorMaximo = 0,
                NumeroFactura = 0
            };

            if (EstadoCuentaSeleccionado.IdContrato == 473 && EstadoCuentaSeleccionado.IdPlan == 482 && concepto.ValorConcepto > EstadoCuentaGenerado.FirstOrDefault().AtencionActiva.Deposito.TotalDeposito)
            {
                RecargarModal();
                this.MostrarMensaje(Resources.ControlesUsuario.ConceptoCobro_DepositoMenorParticular, TipoMensaje.Error);
            }
            else
            {
                Resultado<int> resultado = WebService.Facturacion.GuardarConceptoCobro(concepto);
                if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
                {
                    this.ResultadoEjecucion(SAHI.Comun.Utilidades.Global.TipoOperacion.CREACION);
                }
                else
                {
                    RecargarModal();
                    this.MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        /// Regresa al formulario anterior.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 27/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            this.ResultadoEjecucion(SAHI.Comun.Utilidades.Global.TipoOperacion.CONSULTA);
        }

        /// <summary>
        /// Evento de inicio del control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autór: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }

        #endregion Metodos Protegidos

        #endregion Metodos
    }
}