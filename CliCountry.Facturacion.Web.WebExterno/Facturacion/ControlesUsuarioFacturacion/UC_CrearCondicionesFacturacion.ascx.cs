// --------------------------------
// <copyright file="UC_CrearCondicionesFacturacion.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
// --------------------------------
// <copyright file="UC_CrearCondicionesTarifa.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCondicionesFacturacion.
    /// </summary>
    public partial class UC_CrearCondicionesFacturacion : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CondicionFacturacion.
        /// </summary>
        private const string CONDICIONFACTURACION = "CondicionFacturacion";

        /// <summary>
        /// The TIPOOPERACION.
        /// </summary>
        private const string TIPOOPERACION = "TipoOperacion";

        /// <summary>
        /// The VISUALIZAR.
        /// </summary>
        private const string VISUALIZAR = "VisualizarConfiguracion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece tipo operacion.
        /// </summary>
        public CondicionFacturacion CondicionFacturacion
        {
            get
            {
                return ViewState[CONDICIONFACTURACION] != null ? ViewState[CONDICIONFACTURACION] as CondicionFacturacion : new CondicionFacturacion();
            }

            set
            {
                ViewState[CONDICIONFACTURACION] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece visualizar configuracion.
        /// </summary>
        public bool VisualizarConfiguracion
        {
            get
            {
                return (bool)ViewState[VISUALIZAR];
            }

            set
            {
                ViewState[VISUALIZAR] = value;
            }
        }

        #endregion Propiedades Publicas 
        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece tipo operacion.
        /// </summary>
        private Global.TipoOperacion TipoOperacion
        {
            get
            {
                return ViewState[TIPOOPERACION] != null ? (Global.TipoOperacion)ViewState[TIPOOPERACION] : Global.TipoOperacion.CREACION;
            }

            set
            {
                ViewState[TIPOOPERACION] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Activar los campos segun el tipo de operacion.
        /// </summary>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 26/08/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void ActivarCampos(Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.MODIFICACION:
                    ChkActivo.Enabled = true;
                    ImgGuardarConFac.Visible = true;
                    lblGuardar.Visible = true;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.MODIFICACION);
                    HabilitarComponentes(true);
                    break;

                case Global.TipoOperacion.CONSULTA:
                    ChkActivo.Enabled = false;
                    ImgGuardarConFac.Visible = false;
                    lblGuardar.Visible = false;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.CONSULTA);
                    HabilitarComponentes(false);
                    break;

                case Global.TipoOperacion.CREACION:
                    ChkActivo.Enabled = true;
                    ImgGuardarConFac.Visible = true;
                    lblGuardar.Visible = true;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.CREACION);
                    HabilitarComponentes(true);
                    break;
            }
        }

        /// <summary>
        /// Metodo de Cargar Controles.
        /// </summary>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarControles(CliCountry.SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion)
        {
            TipoOperacion = tipoOperacion;
            CargarComboTipoRelacion();

            switch (TipoOperacion)
            {
                case Global.TipoOperacion.CREACION:
                    lblTitulo.Text = Resources.CondicionesFacturacion.CondicionesFacturacion_TituloCrear;
                    CargarControlesEvento(false);
                    break;

                case Global.TipoOperacion.MODIFICACION:
                    lblTitulo.Text = Resources.CondicionesFacturacion.CondicionesFacturacion_TituloActualizar;
                    CargarControlesEvento(true);
                    break;
            }
        }

        /// <summary>
        /// Metodo de Limpiar Controles.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarControles()
        {
            ChkActivo.Checked = true;
            LblMensaje.Text = string.Empty;
            LblMensaje.Visible = false;
            TxtEntidad.Text = string.Empty;
            TxtContrato.Text = string.Empty;
            TxtAtencion.Text = string.Empty;
            TxtValor.Text = Resources.GlobalWeb.General_ValorCero;
            TxtVigenciaCondicion.Text = string.Empty;
            TxtPlan.Text = string.Empty;
            TxtValor.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
            DdlTipoRelacion.SelectedIndex = -1;
            CondicionFacturacion = null;
            TipoOperacion = Global.TipoOperacion.SALIR;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Buscar atención.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BuscarAtencion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mlvCondicionFacturacion.SetActiveView(vCrearModificar);
        }

        /// <summary>
        /// Buscar Atencion Seleccionar Item Grid.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BuscarAtencion_SeleccionarItemGrid(EventoControles<Atencion> e)
        {
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            mlvCondicionFacturacion.SetActiveView(vCrearModificar);
            if (e.Resultado != null)
            {
                TxtAtencion.Text = e.Resultado.IdAtencion.ToString();
            }
        }

        /// <summary>
        /// Buscar atención.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarAtencion_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            mlvCondicionFacturacion.SetActiveView(vBuscarAtencion);
        }

        /// <summary>
        /// Metodo para guardar el descuento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgGuardarCondicionFacturacions_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ComplementarDatosCondicionFacturacion();

            if (CondicionFacturacion.Id > 0)
            {
                ActualizarCondicionFacturacion();
            }
            else
            {
                InsertarCondicionFacturacion();
            }
        }

        /// <summary>
        /// Metodo para regresar a la pagina principal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgRegresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Evento de inicialización del control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarAtencion.SeleccionarItemGrid += BuscarAtencion_SeleccionarItemGrid;
            ucBuscarAtencion.OperacionEjecutada += BuscarAtencion_OperacionEjecutada;
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
        /// Metodo para realizar la actualizacion del descuento.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ActualizarCondicionFacturacion()
        {
            var resultado = WebService.Facturacion.ActualizarCondicionTarifa(ObjetoProceso());

            if (resultado.Ejecuto)
            {
                if (resultado.Objeto)
                {
                    MostrarMensaje(Resources.DefinirCondicionesTarifa.CondicionFacturacion_MsjActualizacion, TipoMensaje.Ok);
                }
                else
                {
                    MostrarMensaje(Resources.DefinirCondicionesTarifa.CondicionFacturacion_MsjActualizacionError, TipoMensaje.Error);
                }
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Tipo de Relacion.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarComboTipoRelacion()
        {
            int identificadorMaestra = Convert.ToInt32(Resources.CargueCombos.CombosFacturacionCondicionesFacturacion);
            int identificadorPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesFacturacion);
            var resultado = WebService.Facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina);

            if (resultado.Ejecuto)
            {
                DdlTipoRelacion.DataTextField = "NombreMaestroDetalle";
                DdlTipoRelacion.DataValueField = "Valor";
                DdlTipoRelacion.DataSource = resultado.Objeto;
                DdlTipoRelacion.DataBind();
                DdlTipoRelacion.SelectedIndex = 0;
            }
            else
            {
                DdlTipoRelacion.DataSource = null;
                DdlTipoRelacion.DataBind();
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar controles por evento.
        /// </summary>
        /// <param name="modificacion">If set to <c>true</c> [modificacion].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarControlesEvento(bool modificacion)
        {
            TxtEntidad.Text = CondicionFacturacion.NombreTercero;
            TxtContrato.Text = CondicionFacturacion.NombreContrato;
            TxtPlan.Text = VisualizarConfiguracion ? string.Empty : CondicionFacturacion.NombrePlan;
            if (VisualizarConfiguracion)
            {
                TxtPlan.Visible = false;
                CargarListaPlanes();
                DdlPlan.Visible = true;
                ImgBuscarAtencion.Visible = !VisualizarConfiguracion ? false : VisualizarConfiguracion;
            }

            TxtAtencion.Enabled = VisualizarConfiguracion;
            TxtAtencion.Text = CondicionFacturacion.IdAtencion.ToString();
            TxtId.Text = Resources.GlobalWeb.General_ValorCero;

            if (modificacion)
            {
                if (VisualizarConfiguracion)
                {
                    TxtPlan.Visible = false;
                    CargarListaPlanes();
                    DdlPlan.Visible = true;
                    DdlPlan.SelectedValue = CondicionFacturacion.IdPlan.ToString();
                    ImgBuscarAtencion.Visible = !VisualizarConfiguracion ? false : VisualizarConfiguracion;
                }

                DdlTipoRelacion.SelectedValue = CondicionFacturacion.IdTipoRelacion > 0 ? CondicionFacturacion.IdTipoRelacion.ToString() : "-1";
                TxtValor.Text = CondicionFacturacion.Porcentaje ? string.Format(Resources.GlobalWeb.Formato_DecimalString, CondicionFacturacion.ValorPorcentaje.ToString()) : string.Format(Resources.GlobalWeb.Formato_DecimalString, CondicionFacturacion.ValorPropio.ToString());
                ChkActivo.Checked = Convert.ToBoolean(CondicionFacturacion.IndHabilitado);
                TxtId.Text = CondicionFacturacion.Id.ToString();
                TxtVigenciaCondicion.Text = CondicionFacturacion.VigenciaCondicion.ToShortDateString();
                TxtValor.Text = string.Format(Resources.GlobalWeb.Formato_DecimalString, CondicionFacturacion.ValorPropio);
                TxtDescripcion.Text = CondicionFacturacion.DescripcionCondicion;
            }
        }

        /// <summary>
        /// Carga la lista de planes por contrato.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarListaPlanes()
        {
            var contrato = new Contrato()
            {
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                Id = VinculacionSeleccionada.Contrato.Id,
                IndHabilitado = true
            };

            var resultado = WebService.Facturacion.ConsultarPlanes(contrato);

            if (resultado.Ejecuto)
            {
                DdlPlan.DataSource = resultado.Objeto;
                DdlPlan.DataValueField = "Id";
                DdlPlan.DataTextField = "Nombre";
                DdlPlan.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlPlan, false);
                DdlPlan.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para registrar los campos del descuento.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 24/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ComplementarDatosCondicionFacturacion()
        {
            var tipoRelacion = DdlTipoRelacion.SelectedValue;

            CondicionFacturacion.CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad;
            CondicionFacturacion.Id = Convert.ToInt32(TxtId.Text);
            CondicionFacturacion.Tipo = Properties.Settings.Default.CondicionesFacturacion_Tipo;
            CondicionFacturacion.IdTercero = VinculacionSeleccionada.Tercero.Id;
            CondicionFacturacion.IdContrato = VinculacionSeleccionada.Contrato.Id;
            CondicionFacturacion.IdPlan = VinculacionSeleccionada.Plan.Id;
            CondicionFacturacion.IdTipoRelacion = Convert.ToInt16(tipoRelacion);
            CondicionFacturacion.IndHabilitado = Convert.ToInt16(ChkActivo.Checked);
            CondicionFacturacion.VigenciaCondicion = !string.IsNullOrEmpty(TxtVigenciaCondicion.Text) ? Convert.ToDateTime(TxtVigenciaCondicion.Text) : new DateTime();
            CondicionFacturacion.Usuario = Context.User.Identity.Name;
            CondicionFacturacion.ValorPropio = Convert.ToDecimal(TxtValor.Text);
            CondicionFacturacion.DescripcionCondicion = TxtDescripcion.Text;
        }

        /// <summary>
        /// Establecer Titulo.
        /// </summary>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static string EstablecerTitulo(Global.TipoOperacion tipoOperacion)
        {
            string titulo = string.Empty;

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.MODIFICACION:
                    titulo = Resources.CondicionesFacturacion.CondicionesFacturacion_TituloModificar.ToString();
                    break;

                case Global.TipoOperacion.CONSULTA:
                    titulo = Resources.CondicionesFacturacion.CondicionesFacturacion_TituloConsulta.ToString();
                    break;

                case Global.TipoOperacion.CREACION:
                    titulo = Resources.CondicionesFacturacion.CondicionesFacturacion_TituloCrear.ToString();
                    break;
            }

            return titulo;
        }

        /// <summary>
        /// Activa o inactiva los componentes del control segun el caso.
        /// </summary>
        /// <param name="estado">If set to <c>true</c> [estado].</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void HabilitarComponentes(bool estado)
        {
            DdlPlan.Enabled = estado;
            ChkActivo.Enabled = estado;
            DdlTipoRelacion.Enabled = estado;
            TxtValor.Enabled = estado;
            TxtVigenciaCondicion.Enabled = estado;
            TxtDescripcion.Enabled = estado;
            TxtAtencion.Enabled = estado;
            ImgBuscarAtencion.Enabled = estado;
        }

        /// <summary>
        /// Metodo para realizar la insercion del descuento.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void InsertarCondicionFacturacion()
        {
            var resultado = WebService.Facturacion.GuardarCondicionTarifa(ObjetoProceso());

            if (resultado.Ejecuto)
            {
                CondicionFacturacion.Id = resultado.Objeto;
                TxtId.Text = resultado.Objeto.ToString();
                MostrarMensaje(string.Format(Resources.DefinirCondicionesTarifa.CondicionFacturacion_MsjInsercion, CondicionFacturacion.Id), TipoMensaje.Ok);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para almacenar la Condicion de Facturacion.
        /// </summary>
        /// <returns>Condicion de Tarifa a Almacenar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private CondicionTarifa ObjetoProceso()
        {
            CondicionTarifa condicion = new CondicionTarifa()
            {
                CodigoEntidad = CondicionFacturacion.CodigoEntidad,
                Contrato = CondicionFacturacion.Contrato,
                DescripcionCondicion = CondicionFacturacion.DescripcionCondicion,
                EstadoTarifa = CondicionFacturacion.EstadoTarifa,
                FechaFinal = CondicionFacturacion.FechaFinal,
                FechaInicial = CondicionFacturacion.FechaInicial,
                Id = CondicionFacturacion.Id,
                IdAtencion = VisualizarConfiguracion ? Convert.ToInt32(TxtAtencion.Text) : CondicionFacturacion.IdAtencion,
                IdContrato = CondicionFacturacion.IdContrato,
                IdPlan = VisualizarConfiguracion ? Convert.ToInt32(DdlPlan.SelectedValue) : CondicionFacturacion.IdPlan,
                IdTercero = CondicionFacturacion.IdTercero,
                IdTipoRelacion = CondicionFacturacion.IdTipoRelacion,
                IndHabilitado = CondicionFacturacion.IndHabilitado,
                Tipo = CondicionFacturacion.Tipo,
                TipoRelacion = CondicionFacturacion.TipoRelacion,
                ValorPropio = CondicionFacturacion.ValorPropio,
                VigenciaCondicion = CondicionFacturacion.VigenciaCondicion,
                Usuario = CondicionFacturacion.Usuario
            };

            return condicion;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}