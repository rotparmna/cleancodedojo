// --------------------------------
// <copyright file="UC_BuscarGrupoProductos.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using Comun = CliCountry.SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarGrupoProductos
    /// </summary>
    public partial class UC_BuscarGrupoProductos : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The IDATENCION
        /// </summary>
        private const string IDATENCION = "IdAtencion";

        /// <summary>
        /// The TIPOPRODUCTO
        /// </summary>
        private const string TIPOPRODUCTO = "TipoProducto";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado del Evento de Seleccion
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGrid(Comun.EventoControles<GrupoProducto> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on seleccionar item grid
        /// </summary>
        public event OnSeleccionarItemGrid SeleccionarItemGrid;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Id de la Atencion
        /// </summary>
        public int IdAtencion
        {
            get
            {
                return ViewState[IDATENCION] == null ? 0 : Convert.ToInt32(ViewState[IDATENCION]);
            }

            set
            {
                ViewState[IDATENCION] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece id tipo producto
        /// </summary>
        public int IdTipoProducto
        {
            get
            {
                return ViewState[TIPOPRODUCTO] == null ? 0 : Convert.ToInt32(ViewState[TIPOPRODUCTO]);
            }

            set
            {
                ViewState[TIPOPRODUCTO] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para limpiar los campos
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            IdAtencion = 0;
            LblMensaje.Visible = false;
            txtCodigo.Text = string.Empty;
            txtGrupo.Text = string.Empty;
            grvGrupoProductos.DataSource = null;
            grvGrupoProductos.DataBind();
            fsResultado.Visible = false;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Realiza el cambio de pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom 
        /// FechaDeCreacion: 16/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        protected void GrvGrupoProductos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(0);
            var resultado = WebService.Integracion.ConsultarGrupoProducto(consulta, IdAtencion);

            if (resultado.Objeto.Item.Count == 0)
            {
                pagActual.Visible = false;
                numPaginas.Visible = false;
                lblTotalRegistros.Visible = false;
                lblNumRegistros.Visible = false;
            }
            else
            {
                pagActual.Visible = true;
                numPaginas.Visible = true;
                lblTotalRegistros.Visible = true;
                lblNumRegistros.Visible = true;
            }

            lblNumRegistros.Text = resultado.Objeto.Item.Count.ToString();

            if (resultado.Ejecuto)
            {
                grvGrupoProductos.PageIndex = e.NewPageIndex;
                grvGrupoProductos.DataSource = resultado.Objeto.Item;
                grvGrupoProductos.DataBind();
                numPaginas.Text = grvGrupoProductos.PageCount.ToString() + ".";

                // CargaObjetos.OrdenamientoGrilla(this.Page, grvGrupoProductos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvGrupoProductos.DataSource = null;
                grvGrupoProductos.DataBind();
            }
        }

        /// <summary>
        /// Metodo para presentar Grupos de Productos
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvGrupoProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorGrupoProducto = Convert.ToInt32(grvGrupoProductos.DataKeys[indiceFila].Value);
                var respuesta = WebService.Integracion.ConsultarGrupoProducto(
                    new Paginacion<GrupoProducto>()
                {
                    Item = new GrupoProducto()
                    {
                        IdGrupo = identificadorGrupoProducto,
                        IndHabilitado = true
                    }
                },
                IdAtencion);

                if (SeleccionarItemGrid != null && respuesta.Ejecuto && respuesta.Objeto.TotalRegistros == 1)
                {
                    SeleccionarItemGrid(new Comun.EventoControles<GrupoProducto>(this, respuesta.Objeto.Item.FirstOrDefault()));

                    // LimpiarCampos();
                }
            }
        }

        /// <summary>
        /// Metodo para realizar la busqueda.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 03/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrilla(0);
        }

        /// <summary>
        /// Metodo de Regresar de la Pantalla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
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
        /// Metodo de Inicio de la pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 20/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            // pagControl.PageIndexChanged += PagControl_PageIndexChanged;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo para CONTROLAR evento de Cambio de Pagina.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void PagControl_PageIndexChanged(EventoControles<Paginador> e)
        {
            CargarGrilla(e.Resultado.PaginaActual);
        }

        /// <summary>
        /// Evento de Page Load de la Pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 03/04/2013
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
        /// Metodo para construir el objeto de Grupo de Producto
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 03/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Integracion.ConsultarGrupoProducto(consulta, IdAtencion);

            if (resultado.Objeto.Item.Count == 0)
            {
                pagActual.Visible = false;
                numPaginas.Visible = false;
                lblTotalRegistros.Visible = false;
                lblNumRegistros.Visible = false;
            }
            else
            {
                pagActual.Visible = true;
                numPaginas.Visible = true;
                lblTotalRegistros.Visible = true;
                lblNumRegistros.Visible = true;
            }

            lblNumRegistros.Text = resultado.Objeto.Item.Count.ToString();

            if (resultado.Ejecuto)
            {
                grvGrupoProductos.DataSource = resultado.Objeto.Item;
                grvGrupoProductos.DataBind();
                numPaginas.Text = grvGrupoProductos.PageCount.ToString() + ".";

                // CargaObjetos.OrdenamientoGrilla(this.Page, grvGrupoProductos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvGrupoProductos.DataSource = null;
                grvGrupoProductos.DataBind();
            }
        }

        /// <summary>
        /// Metodo para cargar paginador.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Objeto Consulta Grupo de Producto.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<GrupoProducto> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<GrupoProducto> consulta = new Paginacion<GrupoProducto>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new GrupoProducto()
                {
                    CodigoGrupo = txtCodigo.Text,
                    Nombre = txtGrupo.Text,
                    IndHabilitado = Settings.Default.Entidad_IndicadorHabilitadoContrato,
                    IdTipoProducto = IdTipoProducto,
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}