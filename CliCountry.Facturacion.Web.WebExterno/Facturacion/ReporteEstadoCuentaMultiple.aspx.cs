// --------------------------------
// <copyright file="ReporteEstadoCuentaMultiple.aspx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------

namespace CliCountry.Facturacion.Web.WebExterno.Facturacion
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Abstractas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using Resources;

    /// <summary>
    ///     Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ReporteEstadoCuentaMultiple
    /// </summary>
    public partial class ReporteEstadoCuentaMultiple : WebPage
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The ATENCIONCLIENTES
        /// </summary>
        private const string ATENCIONCLIENTES = "AtencionClientes";

        /// <summary>
        /// The CONCEPTOABONO
        /// </summary>
        private const string CONCEPTOABONO = "ABON";

        /// <summary>
        ///     The CONCEPTOSCRUZAR
        /// </summary>
        private const string CONCEPTOSCRUZAR = "ListaConceptosCruzar";

        /// <summary>
        ///     The CONTRATOSPLAN
        /// </summary>
        private const string CONTRATOSPLAN = "ContratosPlan";

        /// <summary>
        ///     The CUENTASELECCIONADO
        /// </summary>
        private const string CUENTASELECCIONADO = "EstadoCuentaSeleccionado";

        /// <summary>
        ///     The VISUALIZAR
        /// </summary>
        private const string ESTADOCUENTA = "EstadoCuentaTipo";

        /// <summary>
        ///     The NOFACTURABLE
        /// </summary>
        private const string NOFACTURABLE = "NoFacturable";

        /// <summary>
        ///     The VISUALIZAR
        /// </summary>
        private const string VISUALIZAR = "VisualizarConceptos";

        #endregion Constantes 
        #region Variables 

        /// <summary>
        ///     The CONCEPTOSCRUZAR.
        /// </summary>
        private SqlConnection con = new SqlConnection();

        /// <summary>
        /// The estados cuenta.
        /// </summary>
        List<EstadoCuentaEncabezado> estadosCuenta = new List<EstadoCuentaEncabezado>();

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Enumeraciones 

        /// <summary>
        ///     Propiedad enumerado para evaluar el tipo de Estado de Cuenta
        /// </summary>
        private enum EstadoCuenta
        {
            /// <summary>
            ///     The detallado
            /// </summary>
            Detallado = 1,

            /// <summary>
            ///     The agrupado
            /// </summary>
            Agrupado = 2,

            /// <summary>
            ///     The venta
            /// </summary>
            Venta = 3
        }

        #endregion Enumeraciones 

        #region Propiedades 

        #region Propiedades Publicas 

/// <summary>
        ///     Obtiene o establece contratos plan
        /// </summary>
        public List<EstadoCuentaEncabezado> ContratosPlan
        {
            get
            {
                return ViewState[CONTRATOSPLAN] == null
                    ? new List<EstadoCuentaEncabezado>()
                    : ViewState[CONTRATOSPLAN] as List<EstadoCuentaEncabezado>;
            }

            set 
            { 
                ViewState[CONTRATOSPLAN] = value; 
            }
        }

        /// <summary>
        ///     Obtiene o establece estado cuenta seleccionado
        /// </summary>
        public EstadoCuentaEncabezado EstadoCuentaSeleccionado
        {
            get
            {
                return ViewState[CUENTASELECCIONADO] == null
                    ? new EstadoCuentaEncabezado()
                    : ViewState[CUENTASELECCIONADO] as EstadoCuentaEncabezado;
            }

            set 
            { 
                ViewState[CUENTASELECCIONADO] = value; 
            }
        }

        /// <summary>
        ///     Obtiene o establece visualizar conceptos
        /// </summary>
        public byte EstadoCuentaTipo
        {
            get { return (byte)ViewState[ESTADOCUENTA]; }

            set { ViewState[ESTADOCUENTA] = value; }
        }

        /// <summary>
        ///     Obtiene o establece lista conceptos cruzar
        /// </summary>
        public List<ConceptoCobro> ListaConceptosCruzar
        {
            get { return (List<ConceptoCobro>)ViewState[CONCEPTOSCRUZAR]; }

            set { ViewState[CONCEPTOSCRUZAR] = value; }
        }

        /// <summary>
        ///     Obtiene o establece lista de no facturable
        /// </summary>
        public List<NoFacturable> NoFacturable
        {
            get
            {
                return ViewState[NOFACTURABLE] == null
                    ? new List<NoFacturable>()
                    : ViewState[NOFACTURABLE] as List<NoFacturable>;
            }

            set 
            { 
                ViewState[NOFACTURABLE] = value; 
            }
        }

        /// <summary>
        ///     Variable para verificar reportte si no facturable ya fue generado
        /// </summary>
        public bool NoFacturableGenerado
        {
            get { return (bool)ViewState["NoFacturableGenerado"]; }

            set { ViewState["NoFacturableGenerado"] = value; }
        }

        /// <summary>
        ///     Obtiene o establece visualizar conceptos
        /// </summary>
        public bool VisualizarConceptos
        {
            get { return (bool)ViewState[VISUALIZAR]; }

            set { ViewState[VISUALIZAR] = value; }
        }

        #endregion Propiedades Publicas 
        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece vinculaciones
        /// </summary>
        private AtencionCliente Atencion
        {
            get
            {
                if (ViewState[ATENCIONCLIENTES] == null)
                {
                    ViewState[ATENCIONCLIENTES] = new AtencionCliente();
                }

                return ViewState[ATENCIONCLIENTES] as AtencionCliente;
            }

            set
            {
                ViewState[ATENCIONCLIENTES] = value;
            }
        }

        /// <summary>
        ///     Obtiene o establece estados cuentas
        /// </summary>
        private List<EstadoCuentaEncabezado> EstadosCuentas
        {
            get
            {
                return ViewState["EstadoCuenta"] == null
                    ? new List<EstadoCuentaEncabezado>()
                    : ViewState["EstadoCuenta"] as List<EstadoCuentaEncabezado>;
            }

            set 
            { 
                ViewState["EstadoCuenta"] = value; 
            }
        }

        /// <summary>
        ///     Obtiene o establece reimpresion
        /// </summary>
        private bool Reimpresion { get; set; }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Protegidos 

        /// <summary>
        ///     Metodo para realizar la actualización del proceso y liberar las atenciones
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 19/08/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui
        /// </remarks>
        protected void BtnActualizarProceso_Click(object sender, EventArgs e)
        {
            if (EstadosCuentas.Count > 0)
            {
                Resultado<bool> resultado = WebService.Facturacion.ActualizarEstadoProcesoFactura(EstadosCuentas[0].IdProceso, (int)ProcesoFactura.EstadoProceso.Cancelado);
                if (resultado.Ejecuto == false)
                {
                    MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        ///     Guarda toda lainformación generada del reporte.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        ///     FechaDeCreacion: 04/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui
        /// </remarks>
        protected void ImgGuardar_Click(object sender, ImageClickEventArgs e)
        {
            this.GuardarFactura();
        }

        /// <summary>
        ///     Evento del control de conceptos.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        ///     FechaDeCreacion: 18/12/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui
        /// </remarks>
        protected void MostrarConceptos_GuardarItemGrid(EventoControles<List<ConceptoCobro>> e)
        {
            if (e.Resultado != null)
            {
                MostrarMensaje(ControlesUsuario.ConceptoCobro_MsjGuardado, TipoMensaje.Ok);

                Atencion = (AtencionCliente)Session["Atencion"];

                foreach (EstadoCuentaEncabezado item in EstadosCuentas)
                {
                    if (item.AtencionActiva != null && item.AtencionActiva.Deposito != null)
                    {
                        item.AtencionActiva.Deposito.Concepto = (from o in e.Resultado
                                                                 where o.IndHabilitado == 1
                                                                 select o).ToList();

                        Atencion.Deposito = item.AtencionActiva.Deposito;
                    }
                }

                Session["Atencion"] = Atencion;

                estadosCuenta = EstadosCuentas;

                ContratosPlan.Clear();
                CargarReporteEstadoCuentaFacturacion();
            }
            else
            {
                CargarReporteEstadoCuentaFacturacion();
            }
        }

        /// <summary>
        ///     Metodo init de la pagina
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        ///     FechaDeCreacion: 06/11/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucMostrarConceptos.GuardarItemGrid += MostrarConceptos_GuardarItemGrid;
            ucMostrarConceptos.GuardarDepositos += MostrarConceptos_GuardarDepositos;
            base.OnInit(e);
        }

        /// <summary>
        ///     Carga de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 03/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    NoFacturableGenerado = false;
                    Master.menuUsuario.Visible = false;
                    CargarReporteEstadoCuentaFacturacion();
                    RecalcularConceptosCuenta();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        /// Page Unload del control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 09/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (con.State != System.Data.ConnectionState.Closed)
            {
                con.Close();
            }
        }

        /// <summary>
        /// RblListEstadoCuenta Selected Index Changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        ///     FechaDeCreacion: 06/11/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        protected void rblListEstadoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarEstadoCuenta();
            ucMostrarConceptos.txtDepositoCruzar.Text = string.Empty;
        }

        /// <summary>
        ///     Actualizar datos del estado de cuenta.
        /// </summary>
        /// <remarks>
        ///     Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm
        ///     FechaDeCreacion: 17/02/2014
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        protected void RecalcularConceptosCuenta()
        {
            EstadoCuentaSeleccionado = (from item in EstadosCuentas
                                        join estado in ContratosPlan
                                            on item.IdContrato equals estado.IdContrato
                                        where item.IdContrato == Convert.ToInt32(rblListEstadoCuenta.SelectedValue)
                                        select item).FirstOrDefault();

            ucMostrarConceptos.EstadoCuentaSeleccionado = EstadoCuentaSeleccionado;
            ucMostrarConceptos.InhabilitarComandosConceptos(EstadoCuentaSeleccionado);
            ucMostrarConceptos.CargarConceptos();
        }

        /// <summary>
        ///     Actualizar datos del estado de cuenta.
        /// </summary>
        /// <remarks>
        ///     Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm
        ///     FechaDeCreacion: 17/02/2014
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        protected void RecargarEstadoCuenta()
        {
            var estadoCuentaEnum = (EstadoCuenta)Enum.Parse(typeof(EstadoCuenta), EstadoCuentaTipo.ToString());

            int indice = rblListEstadoCuenta.SelectedIndex;

            List<ConceptoCobro> estCtTemp = EstadoCuentaSeleccionado.AtencionActiva != null ? EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.CopiarObjeto() : null;

            EstadoCuentaSeleccionado = (from item in EstadosCuentas
                                        join estado in ContratosPlan
                                            on item.IdContrato equals estado.IdContrato
                                        where item.IdContrato == Convert.ToInt32(rblListEstadoCuenta.SelectedValue)
                                        select item).FirstOrDefault();

            if (estCtTemp != null)
            {
                if (EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Count < estCtTemp.Count)
                {
                    EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto = estCtTemp.CopiarObjeto();
                }
            }

            ucMostrarConceptos.EstadoCuentaSeleccionado = EstadoCuentaSeleccionado;
            ucMostrarConceptos.InhabilitarComandosConceptos(EstadoCuentaSeleccionado);

            RecalcularPagosRealizados(EstadosCuentas[indice]);
            GenerarEstadoCuenta(EstadosCuentas[indice], estadoCuentaEnum);

            switch (estadoCuentaEnum)
            {
                case EstadoCuenta.Agrupado:
                    EstadoCuentaAgrupado(EstadosCuentas[indice]);
                    break;

                case EstadoCuenta.Detallado:
                    EstadoCuentaDetallado(EstadosCuentas[indice]);
                    break;

                case EstadoCuenta.Venta:
                    break;
            }

            ucMostrarConceptos.CargarConceptos();
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados Estaticos 

        /// <summary>
        ///     Calcula el valor del abono dependiendo del Id de atencion.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="valorAbono">The valor abono.</param>
        /// <returns>Valor del Abono</returns>
        /// <remarks>
        ///     Autor: William Vásquez R. - INTRGRUPO\wvasquez
        ///     FechaDeCreacion: 04/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private static decimal CalcularValorAbono(List<FacturaAtencion> atenciones, int identificadorAtencion, decimal valorAbono)
        {
            IEnumerable<IGrouping<int, FacturaAtencionDetalle>> numeroVentas = from
                atencion in atenciones
                                                                               from
                                                                                   detalle in atencion.Detalle
                                                                               where
                                                                                   atencion.IdAtencion == identificadorAtencion
                                                                                   && detalle.Excluido == false
                                                                                   && atencion.Cruzar
                                                                               group
                                                                                   detalle by detalle.NumeroVenta
                                                                                   into ventas
                                                                                   select
                                                                                       ventas;

            return valorAbono / numeroVentas.Count();
        }

        /// <summary>
        ///     Crea los items no facturables.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Items No Facturables</returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 04/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private static List<NoFacturable> CrearNoFacturable(EstadoCuentaEncabezado estadoCuenta)
        {
            IEnumerable<NoFacturable> lstNoFacturables = from
                atencion in estadoCuenta.FacturaAtencion
                                                         from
                                                             detalle in atencion.Detalle
                                                         where
                                                             detalle.Excluido
                                                         select new NoFacturable
                                                         {
                                                             IdAtencion = atencion.IdAtencion,
                                                             IdExclusion = detalle.Exclusion != null ? detalle.Exclusion.Id : detalle.ExclusionManual != null ? detalle.ExclusionManual.Id : 0,
                                                             IdProcesoDetalle = detalle.IdProcesoDetalle,
                                                             IdProducto = detalle.IdProducto,
                                                             IdVenta = detalle.IdTransaccion,
                                                             NumeroVenta = detalle.NumeroVenta,
                                                             NombreCliente = estadoCuenta.NombreCliente,
                                                             ApellidoCliente = estadoCuenta.ApellidoCliente,
                                                             CodigoProducto = detalle.CodigoProducto,
                                                             CantidadProducto = detalle.CantidadProducto,
                                                             NombreProducto = !string.IsNullOrEmpty(detalle.NombreComponente) ? detalle.NombreProducto : detalle.NombreProducto,
                                                             NumeroDocumento = atencion.Cliente.NumeroDocumento,
                                                         };

            IEnumerable<NoFacturable> compNoFacturables = from atencion in estadoCuenta.FacturaAtencion
                                                          from detalle in atencion.Detalle
                                                          from componente in detalle.VentaComponentes
                                                          where componente.Excluido
                                                          select new NoFacturable
                                                          {
                                                              IdAtencion = atencion.IdAtencion,
                                                              IdExclusion = componente.Exclusion != null ? componente.Exclusion.Id : componente.ExclusionManual != null ? componente.ExclusionManual.Id : 0,
                                                              IdProcesoDetalle = detalle.IdProcesoDetalle,
                                                              IdProducto = componente.IdProductoRelacion,
                                                              IdVenta = detalle.IdTransaccion,
                                                              NumeroVenta = detalle.NumeroVenta,
                                                              NombreCliente = estadoCuenta.NombreCliente,
                                                              ApellidoCliente = estadoCuenta.ApellidoCliente,
                                                              CodigoProducto = componente.Componente,
                                                              CantidadProducto = componente.CantidadProducto,
                                                              NombreProducto = componente.NombreComponente,
                                                              NumeroDocumento = atencion.Cliente.NumeroDocumento,
                                                          };

            IEnumerable<NoFacturable> resultadoNoFacturables = lstNoFacturables.Union(compNoFacturables);

            return resultadoNoFacturables.ToList();
        }

        /// <summary>
        ///     Metodo para Generar Estado de Cuenta Agrupado.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>
        ///     Estado de Cuenta Agrupado
        /// </returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 02/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private static List<EstadoCuentaAgrupado> GenerarEstadoCuentaAgrupado(List<FacturaAtencion> atenciones)
        {
            IEnumerable<EstadoCuentaAgrupado> grupo = from
                atencion in atenciones
                                                      from
                                                          detalle in atencion.Detalle
                                                      where
                                                          detalle.Excluido == false
                                                          && detalle.ValorProductos > 0
                                                      group
                                                          detalle by new
                                                          {
                                                              detalle.IdGrupoProducto,
                                                              detalle.NombreGrupo,
                                                              detalle.CodigoGrupo
                                                          }

                                                          into grupos
                                                          select new EstadoCuentaAgrupado
                                                          {
                                                              CodigoGrupo = grupos.Key.CodigoGrupo,
                                                              IdGrupoProducto = grupos.Key.IdGrupoProducto,
                                                              NombreGrupo = grupos.Key.NombreGrupo,
                                                              ValorTotal =
                                                                  grupos.Sum(
                                                                      sel =>
                                                                          (sel.ValorBruto > 0
                                                                              ? sel.ValorBruto * sel.CantidadFacturar
                                                                              : (sel.ValorTotal +
                                                                                 ((sel.VentaComponentes != null && sel.VentaComponentes.Count > 0)
                                                                                     ? sel.VentaComponentes.Sum(vc => vc.ValorTotalDescuento)
                                                                                     : sel.ValorDescuento)) * sel.CantidadFacturar)),
                                                              ValorUnitario =
                                                                  grupos.Sum(sel => (sel.ValorProductos > 0
                                                                      ? sel.ValorProductos * sel.CantidadFacturar
                                                                      : (sel.ValorTotal + ((sel.VentaComponentes != null && sel.VentaComponentes.Count > 0)
                                                                          ? sel.VentaComponentes.Sum(vc => vc.ValorTotalDescuento)
                                                                          : sel.ValorDescuento)) * sel.CantidadFacturar))
                                                          };

            return grupo.ToList();
        }

        /// <summary>
        ///     Totaliza El Estado de Cuenta Agrupado.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 02/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private static void TotalizarEstadoCuentaAgrupado(EstadoCuentaEncabezado encabezado)
        {
            var cruce = new List<BaseValidacion>();
            IEnumerable<FacturaAtencionDetalle> detalles = from
                atencion in encabezado.FacturaAtencion
                                                           from
                                                               detalle in atencion.Detalle
                                                           where
                                                               detalle.Excluido == false
                                                               && (detalle.VentaComponentes == null || detalle.VentaComponentes.Count == 0)
                                                           select
                                                               detalle;

            cruce.AddRange(detalles);

            IEnumerable<VentaComponente> componentes = from
                atencion in encabezado.FacturaAtencion
                                                       from
                                                           detalle in atencion.Detalle
                                                       where
                                                           detalle.Excluido == false
                                                           && detalle.VentaComponentes != null
                                                           && detalle.VentaComponentes.Count > 0
                                                       from
                                                           item in detalle.VentaComponentes
                                                       select
                                                           item;

            cruce.AddRange(componentes);

            encabezado.ValorBruto =
                Math.Truncate(cruce.Sum(sp => (sp.ValorBruto > 0 ? sp.ValorBruto : sp.ValorTotal) * sp.CantidadFacturar));
            encabezado.ValorDescuento = Math.Truncate(cruce.Sum(sp => sp.ValorTotalDescuento));
            encabezado.ValorTotalFacturado = Math.Truncate(encabezado.ValorBruto - encabezado.ValorDescuento);
            encabezado.ValorSaldo =
                Math.Truncate(encabezado.ValorTotalFacturado -
                              Convert.ToDecimal(encabezado.IdPlan == 482 &&
                                                encabezado.TipoFacturacion == TipoFacturacion.FacturacionRelacion
                                  ? 0
                                  : encabezado.TotalPagos));
            encabezado.ValorLetras =
                NumeroLetra.NumeroALetras(encabezado.ValorSaldo.ToString(CultureInfo.InvariantCulture));
        }

        #endregion Metodos Privados Estaticos 
        #region Metodos Privados 

        /// <summary>
        /// Ajusta el tercero responsable segun la vinculacion.
        /// </summary>
        /// <param name="estadoCuenta">The id tercero.</param>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm
        /// FechaDeCreacion: 09/01/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void AjustarTerceroResponsable(EstadoCuentaEncabezado estadoCuenta)
        {
            Tercero consultaResponsable = null;
            if (estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdTercero > 0)
            {
                consultaResponsable = WebService.Facturacion.ObtenerTercero(estadoCuenta.Responsable.IdTercero);
            }
            else if (estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdCliente > 0)
            {
                consultaResponsable = WebService.Facturacion.ObtenerCliente(estadoCuenta.Responsable.IdCliente);
            }

            if (estadoCuenta.NumeroDocumentoTercero.Equals(Settings.Default.NitCountryParticular))
            {
                if (consultaResponsable != null && consultaResponsable.Id > 0)
                {
                    estadoCuenta.NumeroDocumentoTercero = consultaResponsable.NumeroDocumento;
                    estadoCuenta.NombreTercero = consultaResponsable.Nombre;
                }
                else
                {
                    estadoCuenta.NumeroDocumentoTercero = estadoCuenta.NumeroDocumento;
                    estadoCuenta.NombreTercero = estadoCuenta.Cliente;
                }
            }
        }

        /// <summary>
        ///     Carga el reporte de facturacion.
        /// </summary>
        /// <remarks>
        ///     Autor: Lina Marcela Almanza Espinosa - INTERGRUPO\lalmanza
        ///     FechaDeCreacion: (21/08/2013)
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void CargarReporteEstadoCuentaFacturacion()
        {
            ProcesoFactura procesoFactura = (ProcesoFactura)Session["ProcesoFactura"];
            Atencion = (AtencionCliente)Session["Atencion"];
            
            bool error = false;
            string msgerror = string.Empty;

            var resultado = WebService.Facturacion.GuardarInformacionProceso(procesoFactura);

            if (resultado.Ejecuto && resultado.Objeto.Count > 0)
            {
                Atencion.Vinculacion = procesoFactura.Vinculaciones;

                foreach (EstadoCuentaEncabezado item in resultado.Objeto)
                {
                    item.AtencionActiva = Atencion;
                    item.TotalPagos = CalcularConceptosCobro.CalcularPagosRealizadosConceptos(item);
                }

                EstadoCuentaGenerado = new List<EstadoCuentaEncabezado>();
                EstadoCuentaGenerado = resultado.Objeto.ToList();
                estadosCuenta = resultado.Objeto;

                foreach (var estadoCuenta in estadosCuenta)
                {
                    decimal sumatoriaConceptos = estadoCuenta.AtencionActiva.Deposito.Concepto.Sum(c => c.ValorConcepto);

                    if (estadoCuenta.AtencionActiva.Deposito.TotalDeposito == 0 && sumatoriaConceptos > 0 && estadoCuenta.IdPlan != 482)
                    {
                        estadoCuenta.Observaciones = estadoCuenta.Observaciones + " - EL DEPOSITO DEL PACIENTE ES MENOR AL CONCEPTO DE COBRO EN: " + sumatoriaConceptos.ToString("N2");
                    }
                    else if (estadoCuenta.AtencionActiva.Deposito.TotalDeposito > 0 && sumatoriaConceptos > 0 && estadoCuenta.IdPlan != 482)
                    {
                        if (estadoCuenta.AtencionActiva.Deposito.TotalDeposito < sumatoriaConceptos)
                        {
                            estadoCuenta.Observaciones = estadoCuenta.Observaciones + " - EL DEPOSITO DEL PACIENTE ES MENOR AL CONCEPTO DE COBRO EN: " + (sumatoriaConceptos - estadoCuenta.AtencionActiva.Deposito.TotalDeposito).ToString("N2");
                        }
                    }
                    else if (estadoCuenta.AtencionActiva.Deposito.TotalDeposito > sumatoriaConceptos && estadoCuenta.IdPlan == 482)
                    {
                        estadoCuenta.Observaciones = estadoCuenta.Observaciones + " - EXISTE UN SALDO A FAVOR DEL CLIENTE POR VALOR DE $" + (estadoCuenta.AtencionActiva.Deposito.TotalDeposito - sumatoriaConceptos).ToString("N2");
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(resultado.Mensaje))
                {
                    msgerror = Resources.GlobalWeb.General_Atencion_SinProductos;
                }
                else
                {
                    msgerror = resultado.Mensaje;
                }

                error = true;
            }

            if (!error)
            {
                EstadoCuentaTipo = procesoFactura.IdFormato;
                TipoFacturacion tipoFactura = TipoFacturacion.FacturacionActividad;
                ContratosPlan = new List<EstadoCuentaEncabezado>();

                if (estadosCuenta != null)
                {
                    EstadosCuentas = estadosCuenta.ToList();
                }

                var estadoCuentaEnum = (EstadoCuenta)Enum.Parse(typeof(EstadoCuenta), EstadoCuentaTipo.ToString());

                foreach (EstadoCuentaEncabezado estadoCuenta in EstadosCuentas)
                {
                    ContratosPlan.Add(new EstadoCuentaEncabezado
                    {
                        IdContrato = estadoCuenta.IdContrato,
                        DescripcionContrato = estadoCuenta.DescripcionContrato,
                        IdPlan = estadoCuenta.IdPlan,
                        NombrePlan = estadoCuenta.NombrePlan,
                        DescripcionPlan = estadoCuenta.DescripcionPlan
                    });

                    if (string.IsNullOrEmpty(estadoCuenta.NumeroFactura))
                    {
                        estadoCuenta.NumeroFactura = Reportes.Facturacion_NumeroFactura;
                    }

                    estadoCuenta.TipoFacturacion = (TipoFacturacion)Enum.Parse(typeof(TipoFacturacion), tipoFactura.ToString());
                    ValidarImpresionFactura(estadoCuenta);

                    this.RecalcularPagosRealizados(estadoCuenta);

                    // Adicionar valores concepto de cobro
                    this.GenerarEstadoCuenta(estadoCuenta, estadoCuentaEnum);
                }

                if (rblListEstadoCuenta.Items.Count == 0)
                {
                    rblListEstadoCuenta.DataSource = ContratosPlan;
                    rblListEstadoCuenta.DataValueField = "IdContrato";
                    rblListEstadoCuenta.DataTextField = "DescripcionContrato";
                    rblListEstadoCuenta.DataBind();
                }

                if (rblListEstadoCuenta.Items.Count > 0)
                {
                    rblListEstadoCuenta.SelectedIndex = rblListEstadoCuenta.SelectedIndex == -1 ? 0 : (rblListEstadoCuenta.SelectedIndex > 0 ? rblListEstadoCuenta.SelectedIndex : 0);
                }

                if (EstadosCuentas != null && EstadosCuentas.Count > 0)
                {
                    if (estadoCuentaEnum != EstadoCuenta.Agrupado)
                    {
                        EstadoCuentaDetallado(EstadosCuentas[rblListEstadoCuenta.SelectedIndex]);
                    }
                    else
                    {
                        EstadoCuentaAgrupado(EstadosCuentas[rblListEstadoCuenta.SelectedIndex]);
                    }
                }

                ucMostrarConceptos.EstadoCuentaSeleccionado = EstadosCuentas[rblListEstadoCuenta.SelectedIndex];

                ucMostrarConceptos.CargarConceptos();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "CierraCuentaMultiple", "if(window.opener != null && !window.opener.closed){window.opener.MostrarMensajeError('" + msgerror + "');window.close();}", true);
            }
        }

        /// <summary>
        ///     Metodo para realizar la configuración de Campos.
        /// </summary>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 19/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void ConfigurarCampos()
        {
            imgGuardar.Visible = false;
            lblConsultarEstado.Visible = false;
        }

        /// <summary>
        /// Crear Detalle de la Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 03/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void CrearDetalleFactura(int numeroFactura, EstadoCuentaEncabezado estadoCuenta)
        {
            try
            {
                foreach (EstadoCuentaDetallado detalle in estadoCuenta.EstadoCuentaDetallado)
                {
                    detalle.CantidadDisponible = detalle.Cantidad;
                    detalle.CantidadFacturaDetalle = detalle.Cantidad;
                    detalle.CantidadGlosasFacturaDetalle = ValorDefecto.Cero;
                    detalle.CantidadVenta = detalle.Cantidad;
                    detalle.CodigoConcepto = string.Empty;
                    detalle.CodigoEntidad = estadoCuenta.CodigoEntidad;
                    detalle.CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento;
                    detalle.CodigoSeccion = estadoCuenta.CodigoSeccion;
                    detalle.CodigoTipoRelacion = string.Empty;
                    detalle.CodigoUnidad = detalle.DetalleTarifa != null ? detalle.DetalleTarifa.CodigoUnidad : string.Empty;
                    detalle.DescuentoPorcentajeParametro = detalle.DetalleVenta != null ? detalle.DetalleVenta.MedicoPorcentajeDescuento : 0;
                    detalle.IdDescuento = (detalle.Descuentos != null && detalle.Descuentos.Count() > ValorDefecto.Cero) ? detalle.Descuentos.First().Id : ValorDefecto.Cero;
                    detalle.IdManual = detalle.DetalleTarifa != null ? detalle.DetalleTarifa.IdManual : (detalle.CondicionesTarifa != null && detalle.CondicionesTarifa.Count > 0) ? detalle.CondicionesTarifa.FirstOrDefault().IdManual : 0;
                    detalle.IdRelacion = ValorDefecto.Cero;
                    detalle.IdTercero = detalle.DetalleVenta != null ? detalle.DetalleVenta.TerceroVenta.Id : 0;
                    detalle.IdTipoMovimiento = estadoCuenta.InformacionFactura.IdTipoMovimiento;
                    detalle.MetodoConfiguracion = string.Empty;
                    detalle.NivelOrden = ValorDefecto.Cero;
                    detalle.NumeroFactura = numeroFactura;
                    detalle.ValorDiferencia = ValorDefecto.Cero;
                    detalle.ValorImpuesto = ValorDefecto.Cero;
                    detalle.ValorMaximo = ValorDefecto.Cero;
                    detalle.ValorPagina = ValorDefecto.Cero;
                    detalle.ValorPorcentajeParametro = detalle.DetalleVenta != null ? detalle.DetalleVenta.MedicoPorcentajeValor : 0;
                    detalle.ValorTasas = ValorDefecto.Cero;
                    detalle.VentaTasas = ValorDefecto.Cero;
                    detalle.VigenciaTarifa =
                    detalle.DetalleTarifa != null
                        ? detalle.DetalleTarifa.FechaInicial
                        : (detalle.CondicionesTarifa != null && detalle.CondicionesTarifa.Count > 0)
                            ? detalle.CondicionesTarifa.FirstOrDefault().VigenciaCondicion
                            : detalle.FechaVigenciaContrato;
                    detalle.VentaComponentes = detalle.VentaComponentes;
                }
            }
            catch (Exception)
            {
                // return null;
            }
        }

        /// <summary>
        ///     Metodo para Realizar la Creacion de la Factura.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Objeto de Factura.</returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 27/05/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private FacturaCompuesta CrearFactura(EstadoCuentaEncabezado estadoCuenta)
        {
            var encabezado = new EncabezadoFactura
            {
                CodigoEntidad = estadoCuenta.CodigoEntidad,
                ConsecutivoCartera = estadoCuenta.ConsecutivoCartera,
                ConsecutivoFacturacion = ValorDefecto.Cero,
                Estado = ValorDefecto.EstadoActivo,
                CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                EstadoAuxiliar = ValorDefecto.EstadoActivo,
                PrefijoNumeracion = estadoCuenta.InformacionFactura.PrefijoFactura,
                IdAtencion = estadoCuenta.IdAtencion,
                IdCodigoFactura = ValorDefecto.Cero,
                ValorCodigoFactura = ValorDefecto.Cero,
                IdDocumentoFactura = ValorDefecto.Cero,
                NumeroDocumentoFactura = string.Empty,
                TipoResultadoTercero = estadoCuenta.InformacionFactura.CodigoTipoTercero,
                FechaFactura = estadoCuenta.FechaFactura,
                FechaFinal = estadoCuenta.PeriodoFacturadoHasta,
                FechaInicial = estadoCuenta.PeriodoFacturadoDesde,
                HoraFactura = estadoCuenta.FechaEgreso,
                IdContrato = estadoCuenta.IdContrato,
                IdDescuento = ValorDefecto.Cero,
                IdPlan =
                    estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion
                        ? estadoCuenta.IdPlan
                        : ValorDefecto.Cero,
                CodigoSeccion = estadoCuenta.CodigoSeccion,
                IdTercero = estadoCuenta.IdTercero,
                IdTerceroResponsable = estadoCuenta.IdTercero,
                IdTipoServicio = estadoCuenta.IdTipoAtencion,
                IdTipoMovimiento = estadoCuenta.InformacionFactura.IdTipoMovimiento,
                IdUbicacion = ValorDefecto.Cero,
                IndicadorImpresion = false,
                Observaciones = estadoCuenta.Observaciones,
                TipoFacturacion = EstablecerCodigoTipoFacturacion(estadoCuenta.TipoFacturacion),
                CodigoUsuario = estadoCuenta.Usuario,
                ValorTotalDebito = estadoCuenta.ValorBruto,
                ValorDescuento = estadoCuenta.ValorDescuento,
                ValorTotalFactura = estadoCuenta.ValorTotalFacturado,
                IdSede = Settings.Default.Seccion_CodigoSede,
                ValorSaldo = estadoCuenta.ValorSaldo
            };

            CrearDetalleFactura(encabezado.NumeroFactura, estadoCuenta); // Calcula datos en detalle para almacenar en estadoCuenta.EstadoCuentaDetallado
            encabezado.NoFacturables = CrearNoFacturable(estadoCuenta);
            return new FacturaCompuesta { EncabezadoFactura = encabezado, EstadoCuentaEncabezado = estadoCuenta };
        }

        /// <summary>
        /// Meotodo para Cruzar Deposito.
        /// </summary>
        /// <param name="estadoCuentaParticular">The estado cuenta particular.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CruzarDeposito(EstadoCuentaEncabezado estadoCuentaParticular)
        {
            var estadoCuenta = estadoCuentaParticular;

            if (estadoCuenta != null && estadoCuenta.ValorTotalFacturado > 0)
            {
                decimal valorDeposito = 0; // Abono del particular.

                if (estadoCuenta.AtencionActiva != null && estadoCuenta.AtencionActiva.Deposito != null)
                {
                    valorDeposito = estadoCuenta.AtencionActiva.Deposito.TotalDeposito;
                }

                decimal sumatoriaConceptos = 0; // Sumatoria de todos los conceptos de las aseguradoras.

                if (estadoCuenta.AtencionActiva != null && estadoCuenta.AtencionActiva.Deposito != null && estadoCuenta.AtencionActiva.Deposito.Concepto != null && estadoCuenta.AtencionActiva.Deposito.Concepto.Count > 0)
                {
                    sumatoriaConceptos = (from p in estadoCuenta.AtencionActiva.Deposito.Concepto
                                          where p.IndHabilitado == 1 && p.DepositoParticular == false
                                          select p.ValorConcepto).Sum();
                }

                //// Saldo real del abono para usar en vinculación del particular
                decimal saldoDeposito = valorDeposito - sumatoriaConceptos;

                //// Valor a cruzar. Inicia siendo igual al total de la factura del particular.
                decimal valorCruce = estadoCuenta.ValorTotalFacturado;
                
                //// Saldo a favor del particular.
                decimal valorRestante = 0;

                if (saldoDeposito <= 0)
                {
                    valorCruce = 0;
                }
                else if (valorCruce > saldoDeposito)
                {
                    valorCruce = saldoDeposito;
                }
                else if (valorCruce < saldoDeposito)
                {
                    valorRestante = saldoDeposito - valorCruce;
                }

                //// Se crea un concepto si y solo si hay un valor de cruce válido.
                if (valorCruce > 0)
                {
                    ConceptoCobro depositoParticular = new ConceptoCobro()
                    {
                        Activo = true,
                        IndHabilitado = 1,
                        IdAtencion = estadoCuenta.AtencionActiva.IdAtencion,
                        IdContrato = estadoCuenta.IdContrato,
                        IdPlan = estadoCuenta.IdPlan,
                        NumeroFactura = 0,
                        ValorConcepto = valorCruce,
                        ValorSaldo = valorCruce,
                        CodigoConcepto = CONCEPTOABONO,
                        TotalConcepto = valorCruce,
                        DepositoParticular = true
                    };

                    if (valorRestante > 0)
                    {
                        estadoCuenta.Observaciones = estadoCuenta.Observaciones + " - Existe un saldo a favor del cliente por valor de $" + valorRestante.ToString("#.##");
                    }

                    EstadoCuentaSeleccionado = estadoCuenta;

                    this.GuardarDepositos(depositoParticular);
                }
            }
        }

        /// <summary>
        ///     Obtiene el código del tipo de facturación.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        ///     FechaDeCreacion: 17/07/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private string EstablecerCodigoTipoFacturacion(TipoFacturacion tipoFacturacion)
        {
            string codigo = string.Empty;

            switch (tipoFacturacion)
            {
                case TipoFacturacion.FacturacionActividad:
                    codigo = Resources.GlobalWeb.CodigoTipoFacturacion_Actividad;
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    codigo = Resources.GlobalWeb.CodigoTipoFacturacion_Paquetes;
                    break;
            }

            return codigo;
        }

        /// <summary>
        ///     Obtiene el nombre del reporte a cargar según el tipo de facturación de quien lo este generando.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Nombre del reporte.</returns>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        ///     FechaDeCreacion: 05/02/2014
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private string EstablecerTipoReporteFactura(EstadoCuentaEncabezado estadoCuenta)
        {
            string nombreReporte = string.Empty;

            TipoFacturacion tipoFacturacion = estadoCuenta.TipoFacturacion;

            switch (tipoFacturacion)
            {
                case TipoFacturacion.FacturacionActividad:
                    nombreReporte = AdministradorFacActividades.FacActividad_RptEstadoCuentaDetallado;
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    nombreReporte = Resources.AdministradorFacPaquetes.FacPaquetes_RptEstadoCuentaDetallado;
                    break;
            }

            return nombreReporte;
        }

        /// <summary>
        ///     Reporte Estado de Cuenta Agrupado.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 03/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void EstadoCuentaAgrupado(EstadoCuentaEncabezado estadoCuenta)
        {
            switch (estadoCuenta.TipoFacturacion)
            {
                case TipoFacturacion.FacturacionActividad:
                    RptEstadoCuenta.NombreReporte = AdministradorFacActividades.FacActividad_RptEstadoCuentaAgrupado;
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    RptEstadoCuenta.NombreReporte = AdministradorFacActividades.FacActividad_RptEstadoCuentaAgrupado;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Settings.Default.NitCountryParticular))
            {
                AjustarTerceroResponsable(estadoCuenta);
            }

            NoFacturable = CrearNoFacturable(estadoCuenta);
            ReporteNoFacturable(NoFacturable);

            RptEstadoCuenta.DataSetsReporte = new Dictionary<string, IEnumerable>();
            RptEstadoCuenta.DataSetsReporte.Add(Reportes.FacturacionAgrupada_DataSet_Encabezado, new EstadoCuentaEncabezado[1] { estadoCuenta });
            RptEstadoCuenta.DataSetsReporte.Add(Reportes.FacturacionAgrupada_DataSet_Detalle, estadoCuenta.EstadoCuentaAgrupado);
            RptEstadoCuenta.CargarReporte();
        }

        /// <summary>
        ///     Reporte Estado de Cuenta Detallado.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 03/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void EstadoCuentaDetallado(EstadoCuentaEncabezado estadoCuenta)
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.NitCountryParticular))
            {
                AjustarTerceroResponsable(estadoCuenta);
            }

            // Vuelve a invocar el recálculo de los pagos realizados y la generación de estado de cuenta porque cuando hay una sola vinculación
            // producto de una anulación anterior, y dicha anulación es de un particular y adicionalmente tiene copagos, el sistema no los aplica
            // correctamente. De tal forma que es necesario recalcular una vez se han aplicado los copagos correspondientes.
            var estadoCuentaEnum = (EstadoCuenta)Enum.Parse(typeof(EstadoCuenta), EstadoCuentaTipo.ToString());

            this.RecalcularPagosRealizados(estadoCuenta);
            this.GenerarEstadoCuenta(estadoCuenta, estadoCuentaEnum);
            
            // Fin de Cambio
            this.RecalcularTotalConceptos(estadoCuenta);

            NoFacturable = CrearNoFacturable(estadoCuenta);
            ReporteNoFacturable(NoFacturable);
            RptEstadoCuenta.NombreReporte = EstablecerTipoReporteFactura(estadoCuenta);

            RptEstadoCuenta.DataSetsReporte = new Dictionary<string, IEnumerable>();

            RptEstadoCuenta.DataSetsReporte.Add(Reportes.FacturacionDetallada_DataSet_Encabezado, new EstadoCuentaEncabezado[1] { estadoCuenta });
            RptEstadoCuenta.DataSetsReporte.Add(Resources.Reportes.FacturacionDetallada_DataSet_Detalle, estadoCuenta.EstadoCuentaDetallado);

            decimal subtotal = 0, descuento = 0;

            foreach (EstadoCuentaDetallado det in estadoCuenta.EstadoCuentaDetallado)
            {
                subtotal += det.SubTotal;
                descuento += det.ValorDescuento;
            }

            int cantidadLineas = Convert.ToInt16(ConfigurationManager.AppSettings["MaximoRegistrosReporteLocal"]);
            
            // if (estado.Count() > cantidadLineas)
            if (estadoCuenta.EstadoCuentaDetallado.Count() > cantidadLineas)
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["Facturacion"].ConnectionString;
                    con.Open();
                }

                string tabla = "##Detallado" + Context.User.Identity.Name + DateTime.Now.ToString("ddMMyyyyhhmmss");
                RptEstadoCuenta.CargarReporte(TipoReporte.ActividadesDetallado, tabla, con);
            }
            else
            {
                RptEstadoCuenta.CargarReporte();
            }
        }

        /// <summary>
        ///     Metodo de estado de cuenta detallado por Componente.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>
        ///     Lista de Detalles
        /// </returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 22/05/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private List<EstadoCuentaDetallado> EstadoCuentaDetalladoComponente(FacturaAtencion facturaAtencion)
        {
            IEnumerable<EstadoCuentaDetallado> resultado = from
                item in facturaAtencion.Detalle
                                                           where
                                                               item.VentaComponentes != null
                                                               && item.VentaComponentes.Count > 0
                                                               && item.Excluido == false
                                                           from
                                                               componente in item.VentaComponentes
                                                           where
                                                               componente.Excluido == false
                                                           select new EstadoCuentaDetallado
                                                           {
                                                               ApellidoCliente = facturaAtencion.Cliente != null ? facturaAtencion.Cliente.Apellido : string.Empty,
                                                               Cantidad = componente.CantidadFacturar,
                                                               CodigoGrupo = componente.CodigoGrupo,
                                                               CodigoProducto = componente.CodigoProducto,
                                                               Componente = !string.IsNullOrEmpty(componente.ComponenteBase) ? componente.ComponenteBase : componente.Componente,
                                                               ComponenteHomolgado = componente.ComponenteHomologado,
                                                               FechaVenta = componente.FechaVenta,
                                                               FactorQx =
                                                                   componente.FactoresQx != null && componente.FactoresQx.Count > 0
                                                                       ? Math.Round(componente.FactoresQx.FirstOrDefault().Valor, 0)
                                                                       : 0,
                                                               TiempoCirugia = componente.Tiempo,
                                                               NivelComplejidad =
                                                                   componente.NivelesComplejidad != null ? componente.NivelesComplejidad.OrdenNivel : (short)0,
                                                               IdAtencion = componente.IdAtencion,
                                                               IdGrupoProducto = componente.IdGrupoProducto,
                                                               IdProducto = componente.IdProducto,
                                                               IdComponente = componente.IdComponente,
                                                               IdTerceroDetalle = item.IdTerceroVenta,
                                                               NombreCliente = facturaAtencion.Cliente != null ? facturaAtencion.Cliente.Nombre : string.Empty,
                                                               NombreComponente = componente.NombreComponente,
                                                               NombreGrupo = componente.NombreGrupo,
                                                               NombreProducto = componente.NombreProducto,
                                                               NombreTerceroVenta =
                                                                   componente.Responsable != null ? componente.Responsable.Honorario.ResponsableHonorario.Nombre : string.Empty,
                                                               NumeroDocumentoTerceroVenta =
                                                                   componente.Responsable != null
                                                                       ? componente.Responsable.Honorario.ResponsableHonorario.NumeroDocumento
                                                                       : string.Empty,
                                                               NumeroVenta = componente.NumeroVenta,
                                                               ValorUnitario = componente.ValorUnitario,
                                                               ValorDescuento = componente.ValorTotalDescuento,
                                                               ValorRecargo = componente.ValorTotalRecargo,
                                                               IndPaquete = item.IndPaquete,
                                                               SubTotal =
                                                                   Reimpresion
                                                                       ? ((componente.ValorUnitario * componente.CantidadFacturar) - componente.ValorTotalDescuento +
                                                                          componente.ValorTotalRecargo)
                                                                       : (componente.CantidadFacturar * componente.ValorUnitario) - componente.ValorTotalDescuento + componente.ValorTotalRecargo,

                                                               IdLote = componente.IdLote,
                                                               IdTransaccion = componente.IdTransaccion,

                                                               Descuentos = componente.Descuentos,
                                                               CondicionesTarifa = componente.CondicionesTarifa,
                                                               DetalleVenta = componente.DetalleVenta,
                                                               ValorProductos = componente.ValorProductos,
                                                               VentaComponentes = item.VentaComponentes,
                                                               FechaVigenciaContrato = componente.FechaVigenciaContrato,
                                                               EsComponente = true
                                                           };

            List<EstadoCuentaDetallado> listaEstadoCuentaDetallado = new List<EstadoCuentaDetallado>();

            foreach (var detalle in resultado)
            {
                if (detalle.EsComponente)
                {
                    var responsable = WebService.Facturacion.ObtenerResponsableVentaComponentes(WebService.Facturacion.ConsultarTerceroComponente(facturaAtencion.IdAtencion), detalle.IdProducto, detalle.IdTransaccion, detalle.NumeroVenta, detalle.Componente);

                    detalle.NombreTerceroVenta = responsable.Honorario.ResponsableHonorario.Nombre;
                    detalle.NumeroDocumentoTerceroVenta = responsable.Honorario.ResponsableHonorario.NumeroDocumento;
                }

                listaEstadoCuentaDetallado.Add(detalle);
            }

            return listaEstadoCuentaDetallado.ToList();
        }

        /// <summary>
        ///     Estado de Cuenta Agrupado.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>
        ///     Retorna Estado de Cuenta Agrupado.
        /// </returns>
        /// <remarks>
        ///     Autor: Christian Andres Polania Torres
        ///     FechaDeCreacion: 02/03/2014
        ///     Descripción: Trae el maestro de los componentes para guardar en orden en tabla.
        /// </remarks>
        private List<EstadoCuentaDetallado> GenerarEstadoComponentesMaestro(List<FacturaAtencion> atenciones)
        {
            IEnumerable<EstadoCuentaDetallado> atencionesDetalle = from
                atencion in atenciones
                                                                   from
                                                                       detalle in atencion.Detalle
                                                                   where
                                                                       detalle.Excluido == false
                                                                       && detalle.VentaComponentes.Count != 0
                                                                   select new EstadoCuentaDetallado
                                                                   {
                                                                       CodigoEntidad = "01",
                                                                       ApellidoCliente = atencion.Cliente != null ? atencion.Cliente.Apellido : string.Empty,
                                                                       Cantidad = detalle.CantidadFacturar,
                                                                       CantidadFacturaDetalle = detalle.CantidadFacturar,
                                                                       CantidadVenta = detalle.CantidadProducto,
                                                                       CantidadDisponible = detalle.CantidadProducto,
                                                                       CodigoGrupo = detalle.CodigoGrupo,
                                                                       CodigoProducto = detalle.CodigoProducto,
                                                                       Componente = detalle.Componente,
                                                                       Especialidad = detalle.Especialidad,
                                                                       FechaVenta = detalle.FechaVenta,
                                                                       IdAtencion = atencion.IdAtencion,
                                                                       IdGrupoProducto = detalle.IdGrupoProducto,
                                                                       IdProducto = detalle.IdProducto,
                                                                       IdTerceroDetalle = detalle.IdTerceroVenta,
                                                                       NivelComplejidad = detalle.NivelComponente,
                                                                       NombreCliente = atencion.Cliente != null ? atencion.Cliente.Nombre : string.Empty,
                                                                       NombreComponente = detalle.NombreComponente,
                                                                       NombreGrupo = detalle.NombreGrupo,
                                                                       NombreProducto = detalle.NombreProducto,
                                                                       NombreTerceroVenta =
                                                                           detalle.DetalleVenta != null ? detalle.DetalleVenta.TerceroVenta.Nombre : string.Empty,
                                                                       NumeroDocumentoTerceroVenta =
                                                                           detalle.DetalleVenta != null ? detalle.DetalleVenta.TerceroVenta.NumeroDocumento : string.Empty,
                                                                       NumeroVenta = detalle.NumeroVenta,
                                                                       ValorUnitario = detalle.ValorUnitario,
                                                                       ValorDescuento = detalle.ValorTotalDescuento,
                                                                       ValorRecargo = detalle.ValorTotalRecargo,
                                                                       Via = detalle.Via,
                                                                       TiempoCirugia = detalle.TiempoCirugia,
                                                                       IndPaquete = detalle.IndPaquete,
                                                                       ValorPaquete = detalle.ValorPaquete,
                                                                       SubTotal = detalle.CantidadFacturar * detalle.ValorProductos,
                                                                       IdLote = detalle.IdLote,
                                                                       IdTransaccion = detalle.IdTransaccion,

                                                                       Descuentos = detalle.Descuentos,
                                                                       CondicionesTarifa = detalle.CondicionesTarifa,
                                                                       DetalleVenta = detalle.DetalleVenta,
                                                                       ValorProductos = detalle.ValorProductos,
                                                                       VentaComponentes = detalle.VentaComponentes,
                                                                       VigenciaTarifa = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                                                                       FechaVigenciaContrato = detalle.FechaVigenciaContrato
                                                                   };

            return atencionesDetalle.ToList();
        }

        /// <summary>
        ///     Metodo PAra generar el estado de Cuenta
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="tipoEstadoCuenta">The tipo estado cuenta.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 03/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void GenerarEstadoCuenta(EstadoCuentaEncabezado estadoCuenta, EstadoCuenta tipoEstadoCuenta)
        {
            if (string.IsNullOrEmpty(estadoCuenta.Usuario))
            {
                estadoCuenta.Usuario = User.Identity.Name;
            }

            if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion && estadoCuenta.IdPlan == 482)
            {
                estadoCuenta.TotalPagos = 0;
            }

            switch (tipoEstadoCuenta)
            {
                case EstadoCuenta.Agrupado:
                    estadoCuenta.EstadoCuentaDetallado = GenerarEstadoCuentaDetallado(estadoCuenta.FacturaAtencion);
                    estadoCuenta.EstadoCuentaAgrupado = GenerarEstadoCuentaAgrupado(estadoCuenta.FacturaAtencion);
                    estadoCuenta.EstadoCuentaResumen = GenerarEstadoCuentaResumen(estadoCuenta.FacturaAtencion);
                    estadoCuenta.EstadoCuentaDetalladoComponentesMaestro = GenerarEstadoComponentesMaestro(estadoCuenta.FacturaAtencion);
                    
                // estadoCuenta.EstadoCuentaDetallado = ValidarTipoFacturacion(estadoCuenta, estadoCuenta.TipoFacturacion);
                    estadoCuenta.EstadoCuentaDetallado = ValidarTipoFacturacion(estadoCuenta);
                    RecalculoMaestroComponente(estadoCuenta.EstadoCuentaDetallado, estadoCuenta.EstadoCuentaDetalladoComponentesMaestro);
                    TotalizarEstadoCuentaAgrupado(estadoCuenta);
                    break;

                case EstadoCuenta.Detallado:
                    
                ////Informacion para el reporte
                    estadoCuenta.EstadoCuentaDetallado = GenerarEstadoCuentaDetallado(estadoCuenta.FacturaAtencion);
                    
                // Informacion para el resumen
                    estadoCuenta.EstadoCuentaResumen = GenerarEstadoCuentaResumen(estadoCuenta.FacturaAtencion);
                    
                // Tiene el total de los componentes unicamente para almacenar
                    estadoCuenta.EstadoCuentaDetalladoComponentesMaestro = GenerarEstadoComponentesMaestro(estadoCuenta.FacturaAtencion);
                    
                // Trae los componentes para mostrar en el reporte
                    
                // estadoCuenta.EstadoCuentaDetallado = ValidarTipoFacturacion(estadoCuenta, estadoCuenta.TipoFacturacion);
                    estadoCuenta.EstadoCuentaDetallado = ValidarTipoFacturacion(estadoCuenta);

                    RecalculoMaestroComponente(estadoCuenta.EstadoCuentaDetallado, estadoCuenta.EstadoCuentaDetalladoComponentesMaestro);

                    // Totalizar los datos a mostrar
                    if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionActividad)
                    {
                        TotalizarEstadoCuentaDetalladoActividad(estadoCuenta);
                    }
                    else
                    {
                        TotalizarEstadoCuentaDetallado(estadoCuenta);
                    }

                    break;

                case EstadoCuenta.Venta:
                    break;
            }
        }

        /// <summary>
        ///     Estado de Cuenta Agrupado.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>
        ///     Retorna Estado de Cuenta Agrupado.
        /// </returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 02/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private List<EstadoCuentaDetallado> GenerarEstadoCuentaDetallado(List<FacturaAtencion> atenciones)
        {
            IEnumerable<EstadoCuentaDetallado> atencionesDetalle = from
                atencion in atenciones
                                                                   from
                                                                       detalle in atencion.Detalle
                                                                   where
                                                                       detalle.Excluido == false
                                                                       && (detalle.VentaComponentes == null || detalle.VentaComponentes.Count == 0)
                                                                   select new EstadoCuentaDetallado
                                                                   {
                                                                       ApellidoCliente = atencion.Cliente != null ? atencion.Cliente.Apellido : string.Empty,
                                                                       Cantidad = detalle.CantidadFacturar,
                                                                       CodigoGrupo = detalle.CodigoGrupo,
                                                                       CodigoProducto = detalle.CodigoProducto,
                                                                       Componente = detalle.Componente,
                                                                       Especialidad = detalle.Especialidad,
                                                                       FechaVenta = detalle.FechaVenta,
                                                                       IdAtencion = atencion.IdAtencion,
                                                                       IdGrupoProducto = detalle.IdGrupoProducto,
                                                                       IdProducto = detalle.IdProducto,
                                                                       IdTerceroDetalle = detalle.IdTerceroVenta,
                                                                       NivelComplejidad = detalle.NivelComponente,
                                                                       NombreCliente = atencion.Cliente != null ? atencion.Cliente.Nombre : string.Empty,
                                                                       NombreComponente = detalle.NombreComponente,
                                                                       NombreGrupo = detalle.NombreGrupo,
                                                                       NombreProducto = detalle.NombreProducto,
                                                                       NombreTerceroVenta =
                                                                           detalle.DetalleVenta != null ? detalle.DetalleVenta.TerceroVenta.Nombre : string.Empty,
                                                                       NumeroDocumentoTerceroVenta =
                                                                           detalle.DetalleVenta != null ? detalle.DetalleVenta.TerceroVenta.NumeroDocumento : string.Empty,
                                                                       NumeroVenta = detalle.NumeroVenta,
                                                                       ValorUnitario = detalle.ValorUnitario,
                                                                       ValorDescuento = detalle.ValorTotalDescuento,
                                                                       ValorRecargo = detalle.ValorTotalRecargo,
                                                                       Via = detalle.Via,
                                                                       TiempoCirugia = detalle.TiempoCirugia,
                                                                       IndPaquete = detalle.IndPaquete,
                                                                       ValorPaquete = detalle.ValorPaquete,
                                                                       SubTotal = detalle.ValorTotal,
                                                                       IdLote = detalle.IdLote,
                                                                       IdTransaccion = detalle.IdTransaccion,

                                                                       Descuentos = detalle.Descuentos,
                                                                       CondicionesTarifa = detalle.CondicionesTarifa,
                                                                       DetalleVenta = detalle.DetalleVenta,
                                                                       ValorProductos = detalle.ValorProductos,
                                                                       VentaComponentes = detalle.VentaComponentes,
                                                                       FechaVigenciaContrato = detalle.FechaVigenciaContrato
                                                                   };

            return atencionesDetalle.ToList();
        }

        /// <summary>
        ///     Generar Estado de Cuenta Resumen.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>Estado de Cuenta Resumen.</returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 04/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private List<EstadoCuentaResumen> GenerarEstadoCuentaResumen(List<FacturaAtencion> atenciones)
        {
            IEnumerable<EstadoCuentaResumen> venta = from
                atencion in atenciones
                                                     from
                                                         detalle in atencion.Detalle
                                                     where
                                                         detalle.Excluido == false
                                                     group
                                                         detalle by new
                                                         {
                                                             atencion.IdAtencion,
                                                             NombreCliente = atencion.Cliente.Nombre,
                                                             ApellidoCliente = atencion.Cliente.Apellido,
                                                             detalle.NumeroVenta,
                                                             detalle.FechaVenta,
                                                             NumeroDocumentoCliente = atencion.Cliente.NumeroDocumento,
                                                             ValorAbono =
                                                                 (atencion.Cruzar && atencion.ConceptosCobro != null && atencion.ConceptosCobro.Count > 0) ||
                                                                 Reimpresion
                                                                     ? atencion.ValorConcepto
                                                                     : 0
                                                         }

                                                         into ventas
                                                         select new EstadoCuentaResumen
                                                         {
                                                             ApellidoCliente = ventas.Key.ApellidoCliente,
                                                             FechaVenta = ventas.Key.FechaVenta,
                                                             IdAtencion = ventas.Key.IdAtencion,
                                                             NombreCliente = ventas.Key.NombreCliente,
                                                             NumeroDocumentoCliente = ventas.Key.NumeroDocumentoCliente,
                                                             NumeroVenta = ventas.Key.NumeroVenta,
                                                             ValorAbono =
                                                                 ventas.Key.ValorAbono > 0
                                                                     ? CalcularValorAbono(atenciones, ventas.Key.IdAtencion, ventas.Key.ValorAbono)
                                                                     : 0,
                                                             ValorTotalDescuento = ventas.Sum(sel => sel.ValorTotalDescuento),
                                                             ValorTotalProducto = ventas.Sum(sel => sel.ValorProductos),
                                                             ValorTotalRecargo = ventas.Sum(sel => sel.ValorTotalRecargo)
                                                         };

            return venta.ToList();
        }

        /// <summary>
        /// Realiza el guardado de un concepto válido para un particular
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private void GuardarDepositos(ConceptoCobro e)
        {
            // if (e.Resultado != null)
            // {
            // MostrarMensaje(ControlesUsuario.ConceptoCobro_Deposito_MsjGuardado, TipoMensaje.Ok);
            if (EstadoCuentaSeleccionado.AtencionActiva != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto != null)
            {
                if (EstadoCuentaSeleccionado.AtencionActiva.Deposito != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto != null)
                {
                    ConceptoCobro depositoActual = (from d in EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto
                                                    where d.DepositoParticular
                                                    select d).FirstOrDefault();
                    if (depositoActual != null)
                    {
                        EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Remove(depositoActual);
                        EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Add(e);
                    }
                    else
                    {
                        EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Add(e);
                    }
                }
            }

            Atencion = (AtencionCliente)Session["Atencion"];

            foreach (EstadoCuentaEncabezado item in EstadosCuentas)
            {
                if (item.AtencionActiva != null && item.AtencionActiva.Deposito != null)
                {
                    if (EstadoCuentaSeleccionado.AtencionActiva != null)
                    {
                        item.AtencionActiva.Deposito = EstadoCuentaSeleccionado.AtencionActiva.Deposito;
                        Atencion.Deposito = EstadoCuentaSeleccionado.AtencionActiva.Deposito;
                    }
                }
            }

            Session["Atencion"] = Atencion;

            estadosCuenta = EstadosCuentas;
            
            // ContratosPlan.Clear();
            // CargarReporteEstadoCuentaFacturacion();
            // }
            // else
            // {
            //    CargarReporteEstadoCuentaFacturacion();
            // }
        }

        /// <summary>
        /// Guardars the factura.
        /// </summary>
        private void GuardarFactura()
        {
            List<int> checkedIDs = new List<int>();
            try
            {
                foreach (GridViewRow chkRow in ucMostrarConceptos.grvConceptos.Rows)
                {
                    CheckBox chk = (CheckBox)chkRow.FindControl("chkAplicarConcepto");
                    if (chk.Checked)
                    {
                        checkedIDs.Add(int.Parse(ucMostrarConceptos.grvConceptos.DataKeys[chkRow.RowIndex].Value.ToString()));
                    }
                }

                if (checkedIDs.Count == 1 || checkedIDs.Count == 0)
                {
                    /* Modificar Guardado*/
                    int identificadorAtencionActual = 0;
                    int identificadorProceso = ((EstadosCuentas != null) && (EstadosCuentas.Count > 0)) ? EstadosCuentas.Select(c => c.IdProceso).FirstOrDefault() : 0;
                    int estadoProceso = WebService.Facturacion.ConsultarEstadoProceso(identificadorProceso);
                    
                    // Valida que estado tiene el proceso, para evitar el dobel Click
                    // Es un proceso nuevo
                    if (estadoProceso == 3)
                    {
                        foreach (EstadoCuentaEncabezado estadoCuenta in EstadosCuentas)
                        {
                            identificadorAtencionActual = this.ProcesoCentral(estadoCuenta);
                        }

                        var bloqueo = WebService.Facturacion.ActualizarBloquearAtencion(VinculacionSeleccionada.IdAtencion, Context.User.Identity.Name);

                        if (bloqueo.Ejecuto && bloqueo.Objeto != 0)
                        {
                            string delay = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) +
                                           DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) +
                                           DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) +
                                           DateTime.Now.Minute.ToString(CultureInfo.InvariantCulture) +
                                           DateTime.Now.Second.ToString(CultureInfo.InvariantCulture) +
                                           DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reimpresion",
                                String.Format("window.close();window.open(\"{0}?idAtencion={1}&delay={2}\",'_blank');",
                                    "ReimpresionFactura.aspx", identificadorAtencionActual, delay), true);
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(bloqueo.Mensaje))
                            {
                                MostrarMensaje(Resources.GlobalWeb.FacturacionActividades_ErrorAtencionBloqueada, TipoMensaje.Error);
                            }
                            else
                            {
                                MostrarMensaje(bloqueo.Mensaje, TipoMensaje.Error);
                            }
                        }
                    }
                    else
                    {
                        this.MostrarMensaje(string.Format("El proceso ya se ejecuto, por favor verifique en la venta, Reportes / Visualización de Facturas"), TipoMensaje.Error);
                    }
                }
                else
                {
                    MostrarMensaje(Resources.ControlesUsuario.GenerarFacturar_MsjError, TipoMensaje.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(string.Format("Se produjo en error en el sistema : {0}", ex.Message), TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Realiza el guardado de un concepto válido para un particular
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private void MostrarConceptos_GuardarDepositos(EventoControles<ConceptoCobro> e)
        {
            if (e.Resultado != null)
            {
                MostrarMensaje(ControlesUsuario.ConceptoCobro_Deposito_MsjGuardado, TipoMensaje.Ok);

                if (EstadoCuentaSeleccionado.AtencionActiva != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto != null)
                {
                    if (EstadoCuentaSeleccionado.AtencionActiva.Deposito != null && EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto != null)
                    {
                        ConceptoCobro depositoActual = (from d in EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto
                                                        where d.DepositoParticular
                                                        select d).FirstOrDefault();
                        if (depositoActual != null)
                        {
                            EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Remove(depositoActual);
                            EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Add(e.Resultado);
                        }
                        else
                        {
                            EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.Add(e.Resultado);
                        }
                    }
                }

                Atencion = (AtencionCliente)Session["Atencion"];

                foreach (EstadoCuentaEncabezado item in EstadosCuentas)
                {
                    if (item.AtencionActiva != null && item.AtencionActiva.Deposito != null)
                    {
                        if (EstadoCuentaSeleccionado.AtencionActiva != null)
                        {
                            item.AtencionActiva.Deposito = EstadoCuentaSeleccionado.AtencionActiva.Deposito;
                            Atencion.Deposito = EstadoCuentaSeleccionado.AtencionActiva.Deposito;
                        }
                    }
                }

                Session["Atencion"] = Atencion;

                estadosCuenta = EstadosCuentas;
                
                // ContratosPlan.Clear();
                CargarReporteEstadoCuentaFacturacion();
            }
            else
            {
                CargarReporteEstadoCuentaFacturacion();
            }
        }

        /// <summary>
        /// Mostrar Estado de Cuenta.
        /// </summary>
        /// <param name="estCuenta">The est cuenta.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 19/08/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void MostrarEstadoCuenta(EstadoCuentaEncabezado estCuenta)
        {
            if (estCuenta != null && estCuenta.EstadoCuentaAgrupado.Count() > ValorDefecto.Cero)
            {
                EstadoCuentaAgrupado(estCuenta);
            }
            else
            {
                EstadoCuentaDetallado(estCuenta);
            }
        }

        /// <summary>
        ///     Muestra el reporte de facturacion no clinica.
        /// </summary>
        /// <param name="facturaNoClinicaReporte">The factura no clinica reporte.</param>
        /// <remarks>
        ///     Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias
        ///     FechaDeCreacion: 05/08/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void MostrarReporteFacturacionNoClinica(FacturaNoClinicaReporte facturaNoClinicaReporte)
        {
            RptEstadoCuenta.NombreReporte = Resources.AdministradorFacNoClinica.FacNoClinica_RptFacturaNoClinica;
            RptEstadoCuenta.DataSetsReporte = new Dictionary<string, IEnumerable>();
            RptEstadoCuenta.DataSetsReporte.Add(Reportes.FacturacionNoClinica_DataSet_Reporte,
                new[] { facturaNoClinicaReporte });
            RptEstadoCuenta.CargarReporte();
        }

        /// <summary>
        /// Realiza el proceso central de
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="objCodigoBarras">The obj codigo barras.</param>
        /// <returns>Identificador Atención.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private int ProcesoCentral(EstadoCuentaEncabezado estadoCuenta)
        {
            int identificadorAtencionActual = 0;

            estadoCuenta.InformacionFactura =
                WebService.Facturacion.ConsultarInformacionFactura(estadoCuenta.IdProceso,
                    estadoCuenta.IdTipoMovimiento, 
                    (short)TipoFacturacion.FacturacionRelacion.GetHashCode());

            FacturaCompuesta facturaCompuesta = CrearFactura(estadoCuenta);
            facturaCompuesta.TipoFacturacion = estadoCuenta.TipoFacturacion;

            Resultado<EstadoCuentaEncabezado> resultado =
                WebService.Facturacion.GuardarInformacionFacturaActividadesPaquetes(facturaCompuesta);

            // Relaiza la segunda parte del flujo
            identificadorAtencionActual = this.SegundoProceso(resultado, estadoCuenta);

            return identificadorAtencionActual;
        }

        /// <summary>
        ///     Recalcular pagos realizados.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        ///     Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez
        ///     FechaDeCreacion: 17/02/2014
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void RecalcularPagosRealizados(EstadoCuentaEncabezado estadoCuenta)
        {
            if (estadoCuenta.AtencionActiva != null)
            {
                estadoCuenta.TotalPagos = CalcularConceptosCobro.CalcularPagosRealizadosConceptos(estadoCuenta);
            }
        }

        /// <summary>
        ///     Método que recalcula el total de copagos al cambiar el contrato en el estado de cuenta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        ///     FechaDeCreacion: 22/01/2014
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void RecalcularTotalConceptos(EstadoCuentaEncabezado estadoCuenta)
        {
            if (estadoCuenta.AtencionActiva == null)
            {
                estadoCuenta.AtencionActiva = new AtencionCliente
                {
                    Deposito = new Deposito
                    {
                        Concepto = new List<ConceptoCobro>()
                    }
                };

                if (estadoCuenta.IdPlan != 482)
                {
                    estadoCuenta.TotalPagos = estadoCuenta.AtencionActiva.Deposito.Concepto.Sum(c => c.ValorConcepto);
                }
                else
                {
                    estadoCuenta.TotalPagos = 0;
                }
            }
        }

        /// <summary>
        /// Recalculo Maestro Componente.
        /// </summary>
        /// <param name="detallado">The detallado.</param>
        /// <param name="maestro">The maestro.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 09/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void RecalculoMaestroComponente(List<EstadoCuentaDetallado> detallado, List<EstadoCuentaDetallado> maestro)
        {
            foreach (EstadoCuentaDetallado mestro in maestro)
            {
                IEnumerable<EstadoCuentaDetallado> atencionesDetalle = from
                atencion in detallado
                                                                       where
                                                                           atencion.IdProducto == mestro.IdProducto && atencion.NumeroVenta == mestro.NumeroVenta
                                                                       select atencion;
                mestro.SubTotal = atencionesDetalle.Sum(sp => sp.SubTotal);
                mestro.ValorDescuento = atencionesDetalle.Sum(sp => sp.ValorDescuento);
                mestro.ValorRecargo = atencionesDetalle.Sum(sp => sp.ValorRecargo);
                mestro.ValorProductos = atencionesDetalle.Sum(sp => sp.ValorProductos);
            }
        }

        /// <summary>
        /// Reporte no facturable.
        /// </summary>
        /// <param name="lstNoFacturables">The no facturables.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 31/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ReporteNoFacturable(List<NoFacturable> lstNoFacturables)
        {
            if (NoFacturableGenerado == true)
            {
                return;
            }

            if (Session[Resources.Reportes.FacturacionDetallada_DataSet_NoFacturable] != null)
            {
                Session.Remove(Resources.Reportes.FacturacionDetallada_DataSet_NoFacturable);
            }

            if (lstNoFacturables != null && lstNoFacturables.Count > 0)
            {
                string delay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                Session.Add(Resources.Reportes.FacturacionDetallada_DataSet_NoFacturable, lstNoFacturables);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ReporteNoFacturable", String.Format("vrnf = window.open(\"{0}?delay={1}\",'_blank');", Resources.AdministradorFacRelacion.FacRelacion_ReporteEstadoCuenta, delay), true);
                NoFacturableGenerado = true;
            }
        }

        /// <summary>
        /// Realiza la ejecución de la segunda insatnciacion del flujo.
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        /// <param name="objCodigoBarras">The obj codigo barras.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Identificador Atención.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private int SegundoProceso(Resultado<EstadoCuentaEncabezado> resultado, EstadoCuentaEncabezado estadoCuenta)
        {
            int identificadorAtencionActual = 0;

            if (resultado.Ejecuto)
            {
                hfGuardar.Value = "1";
                ConfigurarCampos();
                resultado.Objeto.ImagenCodigoBarras =
                    CodigoBarras.GenerarCodigoBarrasFacturacion(
                        resultado.Objeto.NumeroFacturaSinPrefijo,
                        Convert.ToInt32(Settings.Default.LargoCodigoBarras),
                        Convert.ToInt32(Settings.Default.AnchoCodigoBarras));

                // Activar trazabilidad de factura 
                if (!string.IsNullOrWhiteSpace(resultado.Objeto.NumeroFacturaSinPrefijo))
                {
                    WebService.Integracion.CrearResponsableFactura(User.Identity.Name,
                        Convert.ToInt32(resultado.Objeto.NumeroFacturaSinPrefijo));
                }

                Resultado<bool> resultadoEstado = WebService.Facturacion.ActualizarEstadoProcesoFactura(estadoCuenta.IdProceso, (int)ProcesoFactura.EstadoProceso.Facturado);
                if (resultadoEstado.Ejecuto == false)
                {
                    MostrarMensaje(resultadoEstado.Mensaje, TipoMensaje.Error);
                }

                string mensaje = string.Format(Reportes.FacturaAlmacenada_Mensaje, resultado.Objeto.NumeroFactura);
                identificadorAtencionActual = resultado.Objeto.IdAtencion;
                MostrarMensaje(mensaje, TipoMensaje.Ok);
            }
            else
            {
                Resultado<bool> resultadoEstado = WebService.Facturacion.ActualizarEstadoProcesoFactura(estadoCuenta.IdProceso, (int)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta);
                if (resultadoEstado.Ejecuto == false)
                {
                    MostrarMensaje(resultadoEstado.Mensaje, TipoMensaje.Error);
                }

                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                return 0;
            }

            return identificadorAtencionActual;
        }

        /// <summary>
        ///     Metodo PAra Totalizar el estado de cuenta Facturado.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 03/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void TotalizarEstadoCuentaDetallado(EstadoCuentaEncabezado encabezado)
        {
            decimal valorBruto = 0;
            decimal valorDescuento = 0;
            decimal valorRecargo = 0;
            decimal valorBrutoReal = 0;
            ////Codigo chr para calcular sumatoria
            foreach (EstadoCuentaDetallado est in encabezado.EstadoCuentaDetallado)
            {
                valorBruto += est.SubTotal;
                valorDescuento += est.ValorDescuento;
                valorRecargo += est.ValorRecargo;
            }

            valorBrutoReal = valorBruto + valorDescuento;

            encabezado.ValorBruto = valorBrutoReal;
            encabezado.ValorDescuento = valorDescuento;
            encabezado.ValorTotalFacturado = valorBruto - valorDescuento;
            encabezado.ValorSaldo = Math.Truncate(encabezado.ValorTotalFacturado - Convert.ToDecimal(encabezado.IdPlan == 482 && encabezado.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? 0 : encabezado.TotalPagos));
            encabezado.ValorLetras = NumeroLetra.NumeroALetras(Math.Truncate(encabezado.ValorSaldo).ToString());
        }

        /// <summary>
        /// Metodo para totalizar el estado de cuenta Facturado en Actividades.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 20/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void TotalizarEstadoCuentaDetalladoActividad(EstadoCuentaEncabezado encabezado)
        {
            decimal valorBruto = 0;
            decimal valorDescuento = 0;
            decimal valorRecargo = 0;
            decimal valorBrutoReal = 0;
            ////Codigo chr para calcular sumatoria
            foreach (EstadoCuentaDetallado est in encabezado.EstadoCuentaDetallado)
            {
                valorBruto += est.SubTotal;
                valorDescuento += est.ValorDescuento;
                valorRecargo += est.ValorRecargo;
            }

            valorBrutoReal = valorBruto + valorDescuento;

            encabezado.ValorBruto = valorBrutoReal;
            encabezado.ValorDescuento = valorDescuento;
            encabezado.ValorTotalFacturado = valorBruto;
            encabezado.ValorSaldo = Math.Truncate(encabezado.ValorTotalFacturado - Convert.ToDecimal(encabezado.IdPlan == 482 && encabezado.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? 0 : encabezado.TotalPagos));
            encabezado.ValorLetras = NumeroLetra.NumeroALetras(Math.Truncate(encabezado.ValorSaldo).ToString());
        }

        /// <summary>
        /// Realiza el proceso de.
        /// </summary>
        /// <returns>Sin Información.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private bool ValidarAtencion()
        {
            return true;
        }

        /// <summary>
        ///     Validar la Impresión de la Factura.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        ///     FechaDeCreacion: 19/03/2013
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy)
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        ///     Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private void ValidarImpresionFactura(EstadoCuentaEncabezado estadoCuenta)
        {
            if (Session[Resources.GlobalWeb.Session_EstadoCuentaFacturaImpresa] != null)
            {
                Reimpresion = true;
                bool copia = Convert.ToBoolean(Session[Resources.GlobalWeb.Session_EstadoCuentaFacturaImpresa]);
                estadoCuenta.NumeroFactura = string.Format("{0}{1}", estadoCuenta.PrefijoFactura, estadoCuenta.NumeroFactura);
                estadoCuenta.Copia = copia ? Resources.GlobalWeb.ImpresionCopiaFactura : string.Empty;
                ConfigurarCampos();
                Session.Remove(Resources.GlobalWeb.Session_EstadoCuentaFacturaImpresa);
            }
        }

        /// <summary>
        /// Metodo de Validar Tipo Facturacion.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Listado de estado de cuenta.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar el metodo aqui.
        /// </remarks>
        private List<EstadoCuentaDetallado> ValidarTipoFacturacion(EstadoCuentaEncabezado estadoCuenta)
        {
            List<EstadoCuentaDetallado> resultado = estadoCuenta.EstadoCuentaDetallado;
            resultado.AddRange(EstadoCuentaDetalladoComponente(estadoCuenta.FacturaAtencion.FirstOrDefault()));
            return resultado;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}