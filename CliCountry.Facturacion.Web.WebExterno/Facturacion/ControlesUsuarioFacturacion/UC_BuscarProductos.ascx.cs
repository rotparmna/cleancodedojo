// --------------------------------
// <copyright file="UC_BuscarProductos.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using Comun = CliCountry.SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarProductos.
    /// </summary>
    public partial class UC_BuscarProductos : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The IDATENCION
        /// </summary>
        private const string IDATENCION = "IdAtencion";

        /// <summary>
        /// The IDGRUPOPORDUCTO
        /// </summary>
        private const string IDGRUPOPORDUCTO = "IdGrupoProducto";

        /// <summary>
        /// The IDPARAMETRO
        /// </summary>
        private const string IDPARAMETRO = "IdParametro";

        /// <summary>
        /// The IDTIPOSERVICIO
        /// </summary>
        private const string IDTIPOPRODUCTO = "IdTipoProducto";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado del Evento de Seleccion
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGrid(Comun.EventoControles<TipoProductoCompuesto> e);

        /// <summary>
        /// Delegado del evento de seleccion con un parametro
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGridParametro(Comun.EventoControles<TipoProductoCompuesto> e);

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
        /// Obtiene o establece id grupo producto
        /// </summary>
        public int IdGrupoProducto
        {
            get
            {
                return ViewState[IDGRUPOPORDUCTO] == null ? Convert.ToInt32(0) : Convert.ToInt32(ViewState[IDGRUPOPORDUCTO]);
            }

            set
            {
                ViewState[IDGRUPOPORDUCTO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece id parametro
        /// </summary>
        public byte IdParametro
        {
            get
            {
                return ViewState[IDPARAMETRO] == null ? Convert.ToByte(0) : Convert.ToByte(ViewState[IDPARAMETRO]);
            }

            set
            {
                ViewState[IDPARAMETRO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece id tipo producto
        /// </summary>
        public short IdTipoProducto
        {
            get
            {
                return ViewState[IDTIPOPRODUCTO] == null ? Convert.ToInt16(0) : Convert.ToInt16(ViewState[IDTIPOPRODUCTO]);
            }

            set
            {
                ViewState[IDTIPOPRODUCTO] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Inicializa los campos de texto del control web.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/04/2013
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
            txtProducto.Text = string.Empty;
            txtGrupo.Text = string.Empty;
            txtTipoProducto.Text = string.Empty;
            grvProductos.DataSource = null;
            grvProductos.DataBind();
            fsResultado.Visible = false;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Hace la paginación de la grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom 
        /// FechaDeCreacion: 21/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        protected void GrvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(0);
            var resultado = WebService.Integracion.ConsultarTipoProductoCompuesto(consulta, IdAtencion);

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

            if (resultado.Ejecuto)
            {
                grvProductos.PageIndex = e.NewPageIndex;
                grvProductos.DataSource = resultado.Objeto.Item;
                grvProductos.DataBind();
                numPaginas.Text = grvProductos.PageCount.ToString() + ".";

                // CargaObjetos.OrdenamientoGrilla(this.Page, grvProductos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvProductos.DataSource = null;
                grvProductos.DataBind();
            }

            ResultadoEjecucion(Global.TipoOperacion.CONSULTA);
        }

        /// <summary>
        /// Metodo para cargar la grilla de productos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void GrvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int indiceFila = gvr.RowIndex;

                // int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorProducto = grvProductos.Rows[indiceFila].Cells[1].Text;

                var respuesta = WebService.Integracion.ConsultarTipoProductoCompuesto(
                    new Paginacion<TipoProductoCompuesto>()
                {
                    Item = new TipoProductoCompuesto()
                    {
                        Producto = new Producto()
                        {
                            IdProducto = Convert.ToInt32(identificadorProducto),
                        },

                        GrupoProducto = new GrupoProducto()
                        {
                            IdGrupo = IdGrupoProducto,
                        },
                        IndHabilitado = true
                    }
                },
                IdAtencion);

                if (SeleccionarItemGrid != null && respuesta.Ejecuto)
                {
                    if (respuesta.Objeto.TotalRegistros == 1)
                    {
                        SeleccionarItemGrid(new Comun.EventoControles<TipoProductoCompuesto>(this, respuesta.Objeto.Item.FirstOrDefault()));
                        LimpiarCampos();
                    }
                    else
                    {
                        if (Convert.ToInt32(identificadorProducto) == 0)
                        {
                            TipoProductoCompuesto producto = respuesta.Objeto.Item.First(x => x.Producto.IdProducto == 0);
                            SeleccionarItemGrid(new Comun.EventoControles<TipoProductoCompuesto>(this, producto));
                            LimpiarCampos();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para realizar la Búsqueda.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
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
        /// FechaDeCreacion: 15/04/2013
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
        /// Metodo de carga de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
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
        /// Obtiene los parametros para la información de busque.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Integracion.ConsultarTipoProductoCompuesto(consulta, IdAtencion);

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
                grvProductos.DataSource = resultado.Objeto.Item;
                grvProductos.DataBind();
                numPaginas.Text = grvProductos.PageCount.ToString() + ".";

                // CargaObjetos.OrdenamientoGrilla(this.Page, grvProductos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvProductos.DataSource = null;
                grvProductos.DataBind();
            }

            ResultadoEjecucion(Global.TipoOperacion.CONSULTA);
        }

        /// <summary>
        /// Metodo para Crear Objeto de Consulta.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Retorna Tercero.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<TipoProductoCompuesto> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<TipoProductoCompuesto> consulta = new Paginacion<TipoProductoCompuesto>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new TipoProductoCompuesto()
                {
                    Nombre = txtTipoProducto.Text,
                    GrupoProducto = new GrupoProducto()
                    {
                        IdGrupo = IdGrupoProducto,
                        Nombre = txtGrupo.Text,
                        IndHabilitado = true
                    },
                    Producto = new Producto()
                    {
                        CodigoProducto = txtCodigo.Text,
                        Nombre = txtProducto.Text,
                        IndHabilitado = true
                    },
                    IndHabilitado = true,
                    IdTipoProducto = string.IsNullOrEmpty(txtTipoProducto.Text.Trim()) ? IdTipoProducto : Convert.ToInt16(0)
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}