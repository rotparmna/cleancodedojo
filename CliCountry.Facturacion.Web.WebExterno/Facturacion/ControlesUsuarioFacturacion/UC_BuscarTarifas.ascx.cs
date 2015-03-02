// --------------------------------
// <copyright file="UC_BuscarTarifas.ascx.cs" company="InterGrupo S.A.">
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
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using Comun = CliCountry.SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarTarifas.
    /// </summary>
    public partial class UC_BuscarTarifas : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The TIPOBUSQUEDA
        /// </summary>
        private const string TIPOBUSQUEDA = "TipoBusqueda";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delagado Seleccionar Item Grid
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnSeleccionarItemGrid(Comun.EventoControles<ManualesBasicos> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on seleccionar item grid
        /// </summary>
        public event OnSeleccionarItemGrid SeleccionarItemGrid;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Enumeraciones 

        /// <summary>
        /// Enumeracion de Tipo Busqueda
        /// </summary>
        public enum TipoBusqueda
        {
            /// <summary>
            /// The valor nulo
            /// </summary>
            Nulo,

            /// <summary>
            /// The principal
            /// </summary>
            Principal,

            /// <summary>
            /// The alterno
            /// </summary>
            Alterno
        }

        #endregion Enumeraciones 

        #region Propiedades 

        #region Propiedades Publicas 

/// <summary>
        /// Obtiene o establece tipo busqueda ejecutada
        /// </summary>
        public TipoBusqueda TipoBusquedaEjecutada
        {
            get
            {
                return ViewState[TIPOBUSQUEDA] != null ? (TipoBusqueda)Enum.Parse(typeof(TipoBusqueda), ViewState[TIPOBUSQUEDA].ToString()) : TipoBusqueda.Nulo;
            }

            set
            {
                ViewState[TIPOBUSQUEDA] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

/// <summary>
        /// Metodo para limpiar campos
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            TipoBusquedaEjecutada = TipoBusqueda.Nulo;
            LblMensaje.Visible = false;
            TxtIdTarifa.Text = string.Empty;
            TxtNombreManualTarifa.Text = string.Empty;
            TxtVigenciaTarifa.Text = string.Empty;
            TxtNombreTarifa.Text = string.Empty;
            grvManualesTar.DataSource = null;
            grvManualesTar.DataBind();
            fsResultado.Visible = false;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Evento para selecionar un registro y cargarlo en el flujo anterior
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvManualesTar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var camposConsulta = grvManualesTar.DataKeys[indiceFila];

                var respuesta = WebService.Facturacion.ConsultarManualesBasicos(new Paginacion<ManualesBasicos>()
                {
                    Item = new ManualesBasicos()
                    {
                        CodigoTarifa = Convert.ToInt32(camposConsulta.Values[0]),
                        NombreManualesTarifa = camposConsulta.Values[1].ToString(),
                        NombreTarifa = camposConsulta.Values[2].ToString(),
                        VigenciaTarifa = Convert.ToDateTime(camposConsulta.Values[3])
                    }
                });

                if (SeleccionarItemGrid != null && respuesta.Ejecuto)
                {
                    SeleccionarItemGrid(new Comun.EventoControles<ManualesBasicos>(this, respuesta.Objeto.Item.FirstOrDefault()));
                    LimpiarCampos();
                }
            }
        }

        /// <summary>
        /// Metodo de Busqueda de Tarifas
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 02/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CargarGrilla(0);
        }

        /// <summary>
        /// Evento que me permite regresar al flujo anterior.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
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
        /// Metodo de Inicio de la pagina
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 15/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            pagControl.PageIndexChanged += PagControl_PageIndexChanged;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo para CONTROLAR evento de Cambio de Pagina
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void PagControl_PageIndexChanged(EventoControles<Paginador> e)
        {
            CargarGrilla(e.Resultado.PaginaActual);
        }

        /// <summary>
        /// Page load del control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 02/05/2013
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
        /// Metodo para realizar la carga de la grilla.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 03/05/2013
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
            var resultado = WebService.Facturacion.ConsultarManualesBasicos(consulta);

            if (resultado.Ejecuto == true)
            {
                CargaObjetos.OrdenamientoGrilla(this.Page, grvManualesTar, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
                pagControl.Visible = false;
                grvManualesTar.DataSource = null;
                grvManualesTar.DataBind();
            }
        }

        /// <summary>
        /// Metodo para cargar paginador.
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarPaginador(Paginacion<List<ManualesBasicos>> resultado)
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
        /// Metodo para crear objeto de Consulta.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>Objeto de Consulta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<ManualesBasicos> CrearObjetoConsulta(int numeroPagina)
        {
            DateTime result = new DateTime();

            Paginacion<ManualesBasicos> consulta = new Paginacion<ManualesBasicos>()
            {
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                PaginaActual = numeroPagina,
                Item = new ManualesBasicos()
                {
                    CodigoTarifa = !string.IsNullOrEmpty(TxtIdTarifa.Text) ? Convert.ToInt32(TxtIdTarifa.Text) : 0,
                    NombreManualesTarifa = TxtNombreManualTarifa.Text,
                    NombreTarifa = TxtNombreTarifa.Text,
                    VigenciaTarifa = DateTime.TryParse(TxtVigenciaTarifa.Text, out result) ? Convert.ToDateTime(TxtVigenciaTarifa.Text) : new DateTime() 
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}