// --------------------------------
// <copyright file="UC_BuscarVentas.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using Comun = CliCountry.SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarVentas.
    /// </summary>
    public partial class UC_BuscarVentas : WebUserControl
    {
        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado del Evento de Seleccion.
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGrid(Comun.EventoControles<Venta> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on seleccionar item grid.
        /// </summary>
        public event OnSeleccionarItemGrid SeleccionarItemGrid;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Establece el estado de la propiedad Enabled del campo Atención del formulario.
        /// </summary>
        /// <param name="campoAtencionHabilitado">If set to <c>true</c> [campo atencion habilitado].</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 30/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Establece el estado de la propiedad Enabled del campo Atención del formulario.
        /// </remarks>
        public void EstablecerCampoAtencionHabilitado(bool campoAtencionHabilitado)
        {
            txtIdAtencion.Enabled = campoAtencionHabilitado;
        }

        /// <summary>
        /// Inicializa los campos de texto del control web.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            lblMensaje.Visible = false;
            txtNroVenta.Text = string.Empty;
            txtIdAtencion.Text = string.Empty;
            txtIdAtencion.Text = VinculacionSeleccionada.IdAtencion.ToString();
            grvVentas.DataSource = null;
            grvVentas.DataBind();
            fsResultado.Visible = false;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Selecciona una venta de la grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void GrvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorNumeroVenta = Convert.ToInt32(grvVentas.DataKeys[indiceFila].Value);
                var parametrosConsulta = ConsultarParametros(0, VinculacionSeleccionada.IdAtencion);
                var respuesta = WebService.Facturacion.ConsultarVentasAtencion(parametrosConsulta);

                Label lidVenta = (Label)grvVentas.Rows[indiceFila].Cells[1].FindControl("lblIdVenta");
                int numeroVenta = Convert.ToInt32(grvVentas.Rows[indiceFila].Cells[3].Text);

                if (SeleccionarItemGrid != null && respuesta.Ejecuto)
                {
                    SeleccionarItemGrid(new Comun.EventoControles<Venta>(this, new Venta() { IdTransaccion = Convert.ToInt32(lidVenta.Text), NumeroVenta = numeroVenta }));
                    LimpiarCampos();
                }
            }
        }

        /// <summary>
        /// Consulta las ventas asociadas a la atención.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrilla(VinculacionSeleccionada.IdAtencion, 0, true);
        }

        /// <summary>
        /// Metodo de Boton Regresar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Evento de Inicializacion del Control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            pagControl.PageIndexChanged += PagControl_PageIndexChanged;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo para controlar la paginacion del control.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void PagControl_PageIndexChanged(EventoControles<Utilidades.Paginador> e)
        {
            CargarGrilla(VinculacionSeleccionada.IdAtencion, e.Resultado.PaginaActual, false);
        }

        /// <summary>
        /// Page load del control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para realizar la carga de grilla.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="paginaActual">The pagina actual.</param>
        /// <param name="ocultarMensaje">If set to <c>true</c> [ocultar mensaje].</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGrilla(int identificadorAtencion, int paginaActual, bool ocultarMensaje)
        {
            RecargarModal();
            lblMensaje.Visible = ocultarMensaje;
            fsResultado.Visible = true;
            var parametrosConsulta = ConsultarParametros(paginaActual, identificadorAtencion);
            var resultado = WebService.Facturacion.ConsultarVentasNumeroVenta(parametrosConsulta);

            if (resultado.Ejecuto)
            {
                foreach (Venta venta in resultado.Objeto.Item)
                {
                    venta.IdAtencion = identificadorAtencion;
                }

                CargaObjetos.OrdenamientoGrilla(this.Page, grvVentas, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
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
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez
        /// FechaDeCreacion: 01/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<Venta> ConsultarParametros(int numeroPagina, int identificadorAtencion)
        {
            int numeroVenta = 0;

            if (!string.IsNullOrWhiteSpace(txtNroVenta.Text))
            {
                numeroVenta = Convert.ToInt32(txtNroVenta.Text);
            }

            Paginacion<Venta> consulta = new Paginacion<Venta>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,

                Item = new Venta()
                {
                    NumeroVenta = numeroVenta,
                    IdAtencion = identificadorAtencion
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}