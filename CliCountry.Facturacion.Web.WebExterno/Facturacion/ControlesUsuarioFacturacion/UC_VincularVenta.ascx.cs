// --------------------------------
// <copyright file="UC_VincularVenta.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_VincularVenta
    /// </summary>
    public partial class UC_VincularVenta : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The ATENCIONSELECCIONADA
        /// </summary>
        private const string ATENCIONSELECCIONADA = "AtencionSeleccionada";

        /// <summary>
        /// The CHECKFACTURAR
        /// </summary>
        private const string CHKVINCULAR = "chkVincular";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece atencion seleccionada
        /// </summary>
        public Atencion AtencionSeleccionada
        {
            get
            {
                return ViewState[ATENCIONSELECCIONADA] as Atencion;
            }

            set
            {
                ViewState[ATENCIONSELECCIONADA] = value;
                CargarReferencia();
            }
        }

        #endregion Propiedades Publicas

        #region Propiedades Privadas

        /// <summary>
        /// Obtiene o establece ventas
        /// </summary>
        private ObservableCollection<Venta> Ventas
        {
            get
            {
                return ViewState[CHKVINCULAR] as ObservableCollection<Venta>;
            }

            set
            {
                ViewState[CHKVINCULAR] = value;
            }
        }

        #endregion Propiedades Privadas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Metodo para limpiar el formulario
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            fsResultado.Visible = false;
            lblMensaje.Visible = false;
            lblAtencionOrigen.Text = string.Empty;
            lblAtencionDestino.Text = string.Empty;
            grvVentas.DataSource = null;
            grvVentas.DataBind();
            ucBuscarAtencion.LimpiarCampos();
            multi.ActiveViewIndex = 0;
        }

        #endregion Metodos Publicos

        #region Metodos Protegidos

        /// <summary>
        /// Evento de Confirmar la vinculacion de ventas
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            lblMensaje.Visible = false;
            RecargarModal();
            var vinculaciones = ObtenerVinculacionesSeleccionadas();

            if (vinculaciones == null || vinculaciones.Count() == 0)
            {
                lblCantidadRegistros.Text = string.Format(Resources.GlobalWeb.General_TotalRegistros, 0);
                MostrarMensaje(Resources.ControlesUsuario.Ventas_MensajeSeleccionVentas, TipoMensaje.Error);
            }
            else
            {
                GuardarVinculacion(vinculaciones);
            }
        }

        /// <summary>
        /// Metodo para Validar las operaciones del Boton Buscar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 15/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarAtencion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Metodo para realizar la seleccion de la atencion
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarAtencion_SeleccionarItemGrid(EventoControles<Atencion> e)
        {
            lblMensaje.Visible = false;
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            multi.ActiveViewIndex = 0;

            if (e.Resultado != null)
            {
                if (e.Resultado.IdAtencion == Convert.ToInt32(lblAtencionOrigen.Text))
                {
                    MostrarMensaje(Resources.ControlesUsuario.Ventas_MensajeVinculacionAtencion, TipoMensaje.Error);
                    lblCantidadRegistros.Text = string.Format(Resources.GlobalWeb.General_TotalRegistros, 0);
                    lblAtencionDestino.Text = string.Empty;
                    grvVentas.DataSource = null;
                    grvVentas.DataBind();
                }
                else
                {
                    CargarGrilla(e.Resultado, false);
                }
            }
        }

        /// <summary>
        /// Metodo para buscar la atencion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarAtencion_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Metodo de Regresar de la Pantalla
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Evento de Inicializacion del Control
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarAtencion.SeleccionarItemGrid += BuscarAtencion_SeleccionarItemGrid;
            ucBuscarAtencion.OperacionEjecutada += BuscarAtencion_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo de carga de la´página
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos

        #region Metodos Privados

        /// <summary>
        /// Metodo para realizar la carga de grilla
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="ocultarMensaje">if set to <c>true</c> [ocultar mensaje].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarGrilla(Atencion atencion, bool ocultarMensaje)
        {
            RecargarModal();
            lblMensaje.Visible = ocultarMensaje;
            fsResultado.Visible = true;
            var parametrosConsulta = ConsultarParametros(0, atencion.IdAtencion);
            var resultado = WebService.Facturacion.ConsultarVentasAtencion(parametrosConsulta);
            lblAtencionDestino.Text = atencion.IdAtencion.ToString();

            Ventas = new ObservableCollection<Venta>(resultado.Objeto.Item);

            if (resultado.Ejecuto)
            {
                lblCantidadRegistros.Text = string.Format(Resources.GlobalWeb.General_TotalRegistros, resultado.Objeto.Item.Count);
                foreach (Venta venta in resultado.Objeto.Item)
                {
                    venta.IdAtencion = atencion.IdAtencion;
                }

                CargaObjetos.OrdenamientoGrilla(this.Page, grvVentas, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
                lblCantidadRegistros.Text = string.Format(Resources.GlobalWeb.General_TotalRegistros, 0);
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvVentas.DataSource = null;
                grvVentas.DataBind();
            }
        }

        /// <summary>
        /// Método para cargar paginador.
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez
        /// FechaDeCreacion: 01/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarPaginador(Paginacion<List<Venta>> resultado)
        {
            pagControl.ObjetoPaginador = new Paginador()
            {
                CantidadPaginas = resultado.CantidadPaginas,
                LongitudPagina = resultado.LongitudPagina,
                MaximoPaginas = Properties.Settings.Default.Paginacion_CantidadBotones,
                PaginaActual = resultado.PaginaActual,
                TotalRegistros = resultado.TotalRegistros
            };
        }

        /// <summary>
        /// Obtiene la información de los campos filtro.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez
        /// FechaDeCreacion: 01/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private static Paginacion<Venta> ConsultarParametros(int numeroPagina, int identificadorAtencion)
        {
            Paginacion<Venta> consulta = new Paginacion<Venta>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,

                Item = new Venta()
                {
                    IdAtencion = identificadorAtencion
                }
            };

            return consulta;
        }

        /// <summary>
        /// Metodo para realizar la carga por referencia
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarReferencia()
        {
            lblAtencionOrigen.Text = AtencionSeleccionada.IdAtencion.ToString();
        }

        /// <summary>
        /// Metodo para guardar las vinculaciones
        /// </summary>
        /// <param name="vinculaciones">The vinculaciones.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void GuardarVinculacion(IEnumerable<VinculacionVenta> vinculaciones)
        {
            List<VinculacionVenta> vinculacionesActualizar = VinculacionesVentas(vinculaciones);
            var resultado = WebService.Facturacion.GuardarVinculacionVentas(vinculacionesActualizar);

            if (resultado.Ejecuto && resultado.Objeto)
            {
                CargarGrilla(new Atencion() { IdAtencion = Convert.ToInt32(lblAtencionDestino.Text) }, false);
                MostrarMensaje(Resources.ControlesUsuario.Ventas_MensajeVinculacionExito, TipoMensaje.Ok);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para obtener las vinculaciones seleccionadas
        /// </summary>
        /// <returns>
        /// Lista de Vinculaciones Marcadas
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private IEnumerable<VinculacionVenta> ObtenerVinculacionesSeleccionadas()
        {
            var items = from
                           fila in grvVentas.Rows.Cast<GridViewRow>()
                        where
                            (fila.FindControl(CHKVINCULAR) as CheckBox).Checked
                        select
                            new VinculacionVenta()
                            {
                                IdTransaccion = Convert.ToInt32(grvVentas.DataKeys[fila.RowIndex].Values[0]),
                                NumeroVenta = Convert.ToInt32(grvVentas.DataKeys[fila.RowIndex].Values[1])
                            };

            return items;
        }

        /// <summary>
        /// Metodo para obtener las ventas a vincular
        /// </summary>
        /// <param name="vinculaciones">The vinculaciones.</param>
        /// <returns>Ventas A Vincular</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private List<VinculacionVenta> VinculacionesVentas(IEnumerable<VinculacionVenta> vinculaciones)
        {
            var vinculacionesActualizar = from
                                              vinculacion in vinculaciones
                                          join
                                              venta in Ventas
                                          on
                                              new
                                              {
                                                  vinculacion.IdTransaccion,
                                                  vinculacion.NumeroVenta
                                              }

                                          equals
                                              new
                                              {
                                                  venta.IdTransaccion,
                                                  venta.NumeroVenta
                                              }
                                          select new VinculacionVenta()
                                          {
                                              CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                                              IdAtencionPadre = Convert.ToInt32(lblAtencionOrigen.Text),
                                              IdAtencionVinculada = Convert.ToInt32(lblAtencionDestino.Text),
                                              IdTransaccion = venta.IdTransaccion,
                                              NumeroVenta = venta.NumeroVenta,
                                              Usuario = Context.User.Identity.Name
                                          };

            return vinculacionesActualizar.Count() > 0 ? new List<VinculacionVenta>(vinculacionesActualizar) : new List<VinculacionVenta>();
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}