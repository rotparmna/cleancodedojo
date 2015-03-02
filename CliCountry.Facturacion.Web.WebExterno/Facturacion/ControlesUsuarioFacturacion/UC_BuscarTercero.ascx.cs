// --------------------------------
// <copyright file="UC_BuscarTercero.ascx.cs" company="InterGrupo S.A.">
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

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BusquedaTercero.
    /// </summary>
    public partial class UC_BusquedaTercero : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The EVALUARTIPO.
        /// </summary>
        private const string EVALUARTIPO = "EvaluarTipoDocumento";

        /// <summary>
        /// The JURIDICA.
        /// </summary>
        private const string JURIDICA = "CondicionJuridica";

        /// <summary>
        /// The TIPOFACTURACION.
        /// </summary>
        private const string TIPOFACTURACION = "TipoFacturacion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado Para Seleccionar el Cliente.
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGrid(EventoControles<Tercero> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on seleccionar item.
        /// </summary>
        public event OnSeleccionarItemGrid SeleccionarItemGrid;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece condicion juridica.
        /// </summary>
        public bool CondicionJuridica
        {
            get
            {
                return (bool)ViewState[JURIDICA];
            }

            set
            {
                ViewState[JURIDICA] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece evaluar tipo documento.
        /// </summary>
        public bool EvaluarTipoDocumento
        {
            get
            {
                return (bool)ViewState[EVALUARTIPO];
            }

            set
            {
                ViewState[EVALUARTIPO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece controles crear visibles.
        /// </summary>
        public bool OcultarControlesNuevo { get; set; }

        #endregion Propiedades Publicas 
        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece paginador tercero.
        /// </summary>
        /// <value>
        /// Tipo Dato Paginador.
        /// </value>
        private Paginador PaginadorTercero
        {
            get
            {
                var objeto = Session["SessionPaginadorTercero"];
                return objeto != null ? objeto as Paginador : new Paginador();
            }

            set
            {
                Session["SessionPaginadorTercero"] = value;
            }
        }

        /// <summary>
        /// Mantiene la lista de terceros consultados.
        /// </summary>
        private List<Tercero> Terceros
        {
            get
            {
                return ViewState["ListaTerceros"] as List<Tercero>;
            }

            set
            {
                ViewState["ListaTerceros"] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece tipo facturacion.
        /// </summary>
        private TipoFacturacion TipoFacturacion
        {
            get
            {
                return ViewState[TIPOFACTURACION] != null ? (TipoFacturacion)ViewState[TIPOFACTURACION] : TipoFacturacion.FacturacionRelacion;
            }

            set
            {
                ViewState[TIPOFACTURACION] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            fsResultado.Visible = false;
            lblMensaje.Visible = false;
            txtNombreTercero.Text = string.Empty;
            txtNroDocumento.Text = string.Empty;
            grvTerceros.DataSource = null;
            grvTerceros.DataBind();
            ucCrearTercero.LimpiarCampos();
            multi.ActiveViewIndex = 0;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para controlar las ejecuciones del Formulario.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 15/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void CrearTercero_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    ucCrearTercero.LimpiarCampos();
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo para controlar las ejecuciones del Formulario.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <param name="nombreTercero">Nombre del Tercero.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void CrearTercero_RecargarGrilla(object sender, Global.TipoOperacion tipoOperacion, string nombreTercero)
        {
            RecargarModal();

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.CANCELAR:
                    break;
                case Global.TipoOperacion.CONSULTA:
                    break;
                case Global.TipoOperacion.CREACION:
                    txtNombreTercero.Text = nombreTercero;
                    CargarGrilla(0);
                    multi.ActiveViewIndex = 0;
                    ucCrearTercero.LimpiarCampos();
                    break;
                case Global.TipoOperacion.DESRADICAR:
                    break;
                case Global.TipoOperacion.DEVOLVER:
                    break;
                case Global.TipoOperacion.ELIMINACION:
                    break;
                case Global.TipoOperacion.IMPRIMIR:
                    break;
                case Global.TipoOperacion.MODIFICACION:
                    txtNombreTercero.Text = nombreTercero;
                    CargarGrilla(0);
                    multi.ActiveViewIndex = 0;
                    ucCrearTercero.LimpiarCampos();
                    break;
                case Global.TipoOperacion.RADICAR:
                    break;
                case Global.TipoOperacion.SALIR:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Cambia indice de pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom 
        /// FechaDeCreacion: 11/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        protected void GrvTerceros_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;
            fsResultado.Visible = true;

            var consulta = CrearObjetoConsulta(0);
            var resultado = WebService.Integracion.ConsultarTercero(consulta);

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
                grvTerceros.PageIndex = e.NewPageIndex;
                grvTerceros.DataSource = resultado.Objeto.Item;
                grvTerceros.DataBind();
                numPaginas.Text = grvTerceros.PageCount.ToString() + ".";
                
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControlTercero.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvTerceros.DataSource = null;
                grvTerceros.DataBind();
            }
        }

        /// <summary>
        /// Metodo de Seleccion de COmandos del Grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void GrvTerceros_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                var indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorTercero = Convert.ToInt32(grvTerceros.DataKeys[indiceFila].Value);

                Paginacion<Tercero> paginador = new Paginacion<Tercero>()
                {
                    Item = new Tercero()
                    {
                        Id = identificadorTercero,
                        IndHabilitado = true
                    }
                };

                var resultado = WebService.Integracion.ConsultarTercero(paginador);

                if (resultado.Ejecuto && SeleccionarItemGrid != null)
                {
                    SeleccionarItemGrid(new EventoControles<Tercero>(this, resultado.Objeto.Item.FirstOrDefault()));
                }
            }
            else if (e.CommandName == SAHI.Comun.Utilidades.Global.MODIFICAR)
            {
                Tercero terceroEditar = new Tercero();

                var indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorTercero = Convert.ToInt32(grvTerceros.DataKeys[indiceFila].Value);

                foreach (var terc in this.Terceros)
                {
                    if (identificadorTercero == terc.Id)
                    {
                        terceroEditar = terc;
                        break;
                    }
                }

                RecargarModal();
                ucCrearTercero.TipoOperacion = Global.TipoOperacion.MODIFICACION;
                ucCrearTercero.HabilitarEdicionTercero(terceroEditar);
                multi.ActiveViewIndex = 1;
            }
        }

        /// <summary>
        /// Manejo del evento Edit de la Grilla. Se cancela para direccionar el método a través del RowCommand.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void GrvTerceros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Metodo para Crear Tercero.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgAdmTercero_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucCrearTercero.LimpiarCampos();
            ucCrearTercero.TipoOperacion = Global.TipoOperacion.CREACION;
            ucCrearTercero.CargarControles();
            ucCrearTercero.EstablecerTipoDocumentoJuridico(CondicionJuridica);
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Metodo para realizar la Búsqueda.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
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
            ucCrearTercero.OperacionEjecutada += CrearTercero_OperacionEjecutada;
            ucCrearTercero.AfectacionTercero += CrearTercero_RecargarGrilla;
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
        protected void PagControlTercero_PageIndexChanged(EventoControles<Utilidades.Paginador> e)
        {
            CargarGrilla(e.Resultado.PaginaActual);
        }

        /// <summary>
        /// Metodo de Carga de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (OcultarControlesNuevo)
                {
                    lblCrear.Visible = false;
                    ImgAdmTercero.Visible = false;
                }
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para realizar la carga de grilla.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            lblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Integracion.ConsultarTercero(consulta);

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
                this.Terceros = resultado.Objeto.Item.ToList<Tercero>();

                grvTerceros.DataSource = resultado.Objeto.Item;
                grvTerceros.DataBind();
                numPaginas.Text = grvTerceros.PageCount.ToString() + ".";
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvTerceros.DataSource = null;
                grvTerceros.DataBind();
            }
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
        private Paginacion<Tercero> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<Tercero> consulta = new Paginacion<Tercero>()
            {
                LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                PaginaActual = numeroPagina,
                Item = new Tercero()
                {
                    IndHabilitado = true,
                    Nombre = txtNombreTercero.Text,
                    NumeroDocumento = txtNroDocumento.Text
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}