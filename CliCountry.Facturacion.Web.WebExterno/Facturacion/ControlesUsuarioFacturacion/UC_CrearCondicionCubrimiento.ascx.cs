// --------------------------------
// <copyright file="UC_CrearCondicionCubrimiento.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCondicionCubrimiento.
    /// </summary>
    public partial class UC_CrearCondicionCubrimiento : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The INFORMACIONTERCERO.
        /// </summary>
        private const string LISTATIPOSRELACION = "listaTiposRelacion";

        /// <summary>
        /// The VISUALIZAR.
        /// </summary>
        private const string VISUALIZAR = "VisualizarConfiguracion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

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
        /// Obtiene o establece Maestro Condición Cubrimiento.
        /// </summary>
        private List<Maestras> TiposRelacion
        {
            get
            {
                return ViewState[LISTATIPOSRELACION] as List<Maestras>;
            }

            set
            {
                ViewState[LISTATIPOSRELACION] = value;
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
                    btnGuardar.Visible = true;
                    lblGuardar.Visible = true;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.MODIFICACION);
                    HabilitarComponentes(true);
                    break;

                case Global.TipoOperacion.CONSULTA:
                    btnGuardar.Visible = false;
                    lblGuardar.Visible = false;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.CONSULTA);
                    HabilitarComponentes(false);
                    break;

                case Global.TipoOperacion.CREACION:
                    btnGuardar.Visible = true;
                    lblGuardar.Visible = true;
                    lblTitulo.Text = EstablecerTitulo(Global.TipoOperacion.CREACION);
                    HabilitarComponentes(true);
                    break;
            }
        }

        /// <summary>
        /// Carga la información de la entidad hasta el número de atención.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarInformacionCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            LimpiarCampos();
            chkActivo.Checked = condicionCubrimiento.IndHabilitado == Convert.ToInt16(Resources.GlobalWeb.General_ValorUno) ? true : false;
            txtIdentificador.Text = condicionCubrimiento.Id.ToString();
            txtEntidad.Text = condicionCubrimiento.Cubrimiento.Tercero.Nombre.ToString();
            txtIdContrato.Text = condicionCubrimiento.Cubrimiento.Contrato.Id.ToString();
            txtContrato.Text = condicionCubrimiento.Cubrimiento.Contrato.Nombre.ToString();
            txtIdPlan.Text = VisualizarConfiguracion ? string.Empty : condicionCubrimiento.Cubrimiento.Plan.Id.ToString();
            txtPlan.Text = VisualizarConfiguracion ? string.Empty : condicionCubrimiento.Cubrimiento.Plan.Nombre.ToString();
            txtIdAtencion.Text = condicionCubrimiento.Cubrimiento.IdAtencion.ToString();
            txtVigenciaCondicion.Text = DateTime.Now.ToString();

            if (lblTitulo.Text == Resources.CondicionesCubrimientos.CondicionesCubrimientos_Actualizar)
            {
                ddlClaseCubrimiento.SelectedValue = condicionCubrimiento.Cubrimiento.IdClaseCubrimiento.ToString();
                ddlTipoRelacion.SelectedValue = condicionCubrimiento.NumeroTipoRelacion.ToString();
                txtVigenciaCondicion.Text = Convert.ToDateTime(condicionCubrimiento.VigenciaCondicion).ToString();
            }

            if (ddlTipoRelacion.SelectedValue == TipoRelacion.ValorMaximoPorcentaje.GetHashCode().ToString())
            {
                // mevValor.Enabled = true;
                txtValor.Text = string.Format(Resources.GlobalWeb.Formato_DecimalString, condicionCubrimiento.ValorPropio);
            }
            else if (ddlTipoRelacion.SelectedValue == TipoRelacion.Cantidad.GetHashCode().ToString())
            {
                // mevValor.Enabled = false;
                txtValor.Text = Convert.ToString(Convert.ToInt32(condicionCubrimiento.ValorPropio));
            }
            else
            {
                // mevValor.Enabled = false;
                txtValor.Text = string.Format(Resources.GlobalWeb.Formato_DecimalString, condicionCubrimiento.ValorPropio);
            }

            trConfiguracion.Visible = !VisualizarConfiguracion ? false : VisualizarConfiguracion;
            ImgBuscarAtencion.Visible = !VisualizarConfiguracion ? false : VisualizarConfiguracion;

            if (VisualizarConfiguracion)
            {
                CargarListaTipoAtencion();
                CargarListaPlanes();
                CargarListaClaseServicios();
                txtIdPlan.Visible = false;
                txtPlan.Visible = false;
                DdlPlan.Visible = true;

                DdlClaseServicio.SelectedValue = condicionCubrimiento.IdServicio.ToString();
                DdlTipoAtencion.SelectedValue = condicionCubrimiento.IdTipoAtencion == 0 ? Resources.GlobalWeb.General_ValorNegativo : condicionCubrimiento.IdTipoAtencion.ToString();
                DdlPlan.SelectedValue = condicionCubrimiento.IdPlan.ToString();
            }
            else
            {
                txtIdPlan.Visible = true;
                txtPlan.Visible = true;
                DdlPlan.Visible = false;
            }

            txtDescripcion.Text = condicionCubrimiento.DescripcionCondicion;
        }

        /// <summary>
        /// Carga los listado de manera inicial.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarListasIniciales()
        {
            int identificadorPagina = Settings.Default.CondicionCubrimiento_IdPagina;
            int identificadorMaestra = Settings.Default.CondicionCubrimiento_IdMaestra;
            CargarTiposRelacion();
            CargarClasesCubrimientos();
            TiposRelacion = WebService.Facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina).Objeto.ToList();
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            lblMensaje.Visible = false;
            txtIdentificador.Text = string.Empty;
            txtEntidad.Text = string.Empty;
            txtIdContrato.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtIdPlan.Text = string.Empty;
            txtPlan.Text = string.Empty;
            txtIdAtencion.Text = string.Empty;
            ddlClaseCubrimiento.SelectedIndex = ddlClaseCubrimiento.SelectedIndex > -1 ? 0 : -1;
            ddlTipoRelacion.SelectedIndex = ddlTipoRelacion.SelectedIndex > -1 ? 0 : -1;
            txtVigenciaCondicion.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para guardar la condicion de cubrimiento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;

            var grupo = ddlTipoRelacion.ValidationGroup;
            Page.Validate(grupo);

            if (Page.IsValid)
            {
                if (lblTitulo.Text == Resources.CondicionesCubrimientos.CondicionesCubrimientos_Crear)
                {
                    GuardarCondicionCubrimiento();
                }
                else
                {
                    ActualizarCondicionCubrimiento();
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta al devolverse a la pantalla anterior.
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
            mlvCondicionCubrimiento.SetActiveView(vCrearModificar);
        }

        /// <summary>
        /// Buscar Atención.
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
            mlvCondicionCubrimiento.SetActiveView(vCrearModificar);
            if (e.Resultado != null)
            {
                txtIdAtencion.Text = e.Resultado.IdAtencion.ToString();
            }
        }

        /// <summary>
        /// Se ejecuta cuando se selecciona un valor del combo.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void DdlTipoRelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarFormatoCampoValor();
        }

        /// <summary>
        /// Se ejecuta cuando se desea retornar a la pagina anterior.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 24/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBtnSalir_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Llama al control de busqueda de atenciones.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarAtencion_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            mlvCondicionCubrimiento.SetActiveView(vBuscarAtencion);
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
        /// Se ejecuta cuando se carga el control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Método para actualizar la condición de cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ActualizarCondicionCubrimiento()
        {
            RecargarModal();
            CondicionCubrimiento codCubrimiento = new CondicionCubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                Id = Convert.ToInt32(txtIdentificador.Text),
                IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdContrato = string.IsNullOrEmpty(txtIdContrato.Text) ? 0 : Convert.ToInt32(txtIdContrato.Text),
                IdPlan = VisualizarConfiguracion ? Convert.ToInt32(DdlPlan.SelectedValue) : Convert.ToInt32(txtIdPlan.Text),
                IdAtencion = VisualizarConfiguracion ? Convert.ToInt32(txtIdAtencion.Text) : VinculacionSeleccionada.IdAtencion,
                IdServicio = VisualizarConfiguracion ? Convert.ToInt32(DdlClaseServicio.SelectedValue) : 0,
                IdTipoAtencion = VisualizarConfiguracion ? Convert.ToInt32(DdlTipoAtencion.SelectedValue) : 0,
                NumeroTipoRelacion = Convert.ToInt16(ddlTipoRelacion.SelectedValue),
                ValorPropio = Convert.ToDecimal(txtValor.Text),
                ValorPorcentaje = ddlTipoRelacion.SelectedValue == Convert.ToString(TipoRelacion.ValorMaximoPorcentaje.GetHashCode()) ? Convert.ToDecimal(txtValor.Text) : (decimal)0,
                VigenciaCondicion = Convert.ToDateTime(txtVigenciaCondicion.Text),
                VigenciaTarifa = DateTime.Now,
                DescripcionCondicion = txtDescripcion.Text,
                Cubrimiento = new Cubrimiento()
                {
                    IdClaseCubrimiento = ddlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? Convert.ToInt32(Resources.GlobalWeb.General_ValorCero) : Convert.ToInt32(ddlClaseCubrimiento.SelectedValue)
                },
                Tipo = Resources.GlobalWeb.General_TipoCondicionCubrimiento,
                Componente = Resources.GlobalWeb.General_ValorNA
            };

            Resultado<int> resultado = WebService.Facturacion.ActualizarCondicionCubrimiento(codCubrimiento);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CondicionCubrimiento_MsjActualizacion, codCubrimiento.Id), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeERROR;
            }
        }

        /// <summary>
        /// Carga las clases de cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarClasesCubrimientos()
        {
            var claseCubrimiento = new ClaseCubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IndHabilitado = Convert.ToInt16(Resources.GlobalWeb.General_ValorUno)
            };

            ddlClaseCubrimiento.DataSource = WebService.Facturacion.ConsultarClasesCubrimiento(claseCubrimiento).Objeto;
            ddlClaseCubrimiento.DataValueField = "IdClaseCubrimiento";
            ddlClaseCubrimiento.DataTextField = "Nombre";
            ddlClaseCubrimiento.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(ddlClaseCubrimiento, false);
        }

        /// <summary>
        /// Carga las clases de servicio en el combo.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 03/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarListaClaseServicios()
        {
            DdlClaseServicio.DataSource = WebService.Configuracion.ConsultarServicios().Objeto;
            DdlClaseServicio.DataValueField = "IdServicio";
            DdlClaseServicio.DataTextField = "NombreServicio";
            DdlClaseServicio.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlClaseServicio, false);
            DdlClaseServicio.SelectedIndex = 0;
        }

        /// <summary>
        /// Obtiene la lista de planes del contrato.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/09/2013
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
        /// Carga los tipos de atención en el combo.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarListaTipoAtencion()
        {
            var resultado = WebService.Integracion.ConsultarTiposAtencion(new TipoAtencion());

            if (resultado.Ejecuto)
            {
                DdlTipoAtencion.DataSource = resultado.Objeto;
                DdlTipoAtencion.DataValueField = "Id";
                DdlTipoAtencion.DataTextField = "Nombre";
                DdlTipoAtencion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlTipoAtencion, false);
                DdlTipoAtencion.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Carga los tipos de relación.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarTiposRelacion()
        {
            int identificadorPagina = Settings.Default.CondicionCubrimiento_IdPagina;
            int identificadorMaestra = Settings.Default.CondicionCubrimiento_IdMaestra;

            var resultado = WebService.Facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina);

            if (resultado.Ejecuto)
            {
                ddlTipoRelacion.DataSource = resultado.Objeto;
                ddlTipoRelacion.DataValueField = Resources.GlobalWeb.General_ValorMaestroDetalle;
                ddlTipoRelacion.DataTextField = Resources.GlobalWeb.General_NombreMaestroDetalle;
                ddlTipoRelacion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(ddlTipoRelacion, false);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Obtiene y establece segun el tipo de relación seleccionado, la expresión regular a la que debe aplicar.
        /// </summary>
        /// <param name="identificadorTipoRelacion">The id tipo relacion.</param>
        /// <returns>Expresión regular a aplicar.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private int EstablecerTipoRelacion(int identificadorTipoRelacion)
        {
            int tipoRelacion = identificadorTipoRelacion;
            int identificadorMaestra = Settings.Default.CondicionCubrimiento_IdMaestra;
            int identificadorPagina = Settings.Default.CondicionCubrimiento_IdPagina;
            string expresion = string.Empty;
            Maestras maestra = new Maestras();
            switch ((TipoRelacion)tipoRelacion)
            {
                case TipoRelacion.ValorMaximo:
                    txtValor.Text = string.Empty;
                    txtValor.MaxLength = 8;
                    maestra = ObtenerTipoRelacionPorProceso(identificadorMaestra, identificadorPagina, tipoRelacion);
                    txtValor.Focus();
                    break;

                case TipoRelacion.ValorMaximoPorcentaje:
                    txtValor.Text = string.Empty;
                    txtValor.MaxLength = 4;
                    maestra = ObtenerTipoRelacionPorProceso(identificadorMaestra, identificadorPagina, tipoRelacion);

                    txtValor.Focus();
                    break;

                case TipoRelacion.Cantidad:
                    txtValor.Text = string.Empty;
                    txtValor.MaxLength = 5;
                    maestra = ObtenerTipoRelacionPorProceso(identificadorMaestra, identificadorPagina, tipoRelacion);
                    txtValor.Focus();
                    break;

                default:
                    txtValor.Text = string.Empty;
                    txtValor.MaxLength = 8;
                    maestra = ObtenerTipoRelacionPorProceso(identificadorMaestra, identificadorPagina, tipoRelacion);
                    txtValor.Focus();
                    break;
            }

            return tipoRelacion;
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
                    titulo = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Actualizar.ToString();
                    break;

                case Global.TipoOperacion.CONSULTA:
                    titulo = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Consultar.ToString();
                    break;

                case Global.TipoOperacion.CREACION:
                    titulo = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Crear.ToString();
                    break;
            }

            return titulo;
        }

        /// <summary>
        /// Método para guardar la condición de cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarCondicionCubrimiento()
        {
            RecargarModal();
            CondicionCubrimiento codCubrimiento = new CondicionCubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdContrato = string.IsNullOrEmpty(txtIdContrato.Text) ? 0 : Convert.ToInt32(txtIdContrato.Text),
                IdPlan = VisualizarConfiguracion ? Convert.ToInt32(DdlPlan.SelectedValue) : Convert.ToInt32(txtIdPlan.Text),
                IdAtencion = VisualizarConfiguracion ? Convert.ToInt32(txtIdAtencion.Text) : VinculacionSeleccionada.IdAtencion,
                IdServicio = VisualizarConfiguracion ? Convert.ToInt32(DdlClaseServicio.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? "0" : DdlClaseServicio.SelectedValue) : 0,
                IdTipoAtencion = VisualizarConfiguracion ? Convert.ToInt32(DdlTipoAtencion.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? "0" : DdlTipoAtencion.SelectedValue) : 0,
                NumeroTipoRelacion = Convert.ToInt16(ddlTipoRelacion.SelectedValue),
                ValorPropio = Convert.ToDecimal(txtValor.Text),
                ValorPorcentaje = ddlTipoRelacion.SelectedValue == Convert.ToString(TipoRelacion.ValorMaximoPorcentaje.GetHashCode()) ? Convert.ToDecimal(txtValor.Text) : (decimal)0,
                VigenciaCondicion = Convert.ToDateTime(txtVigenciaCondicion.Text),
                VigenciaTarifa = DateTime.Now,
                DescripcionCondicion = txtDescripcion.Text,
                Cubrimiento = new Cubrimiento()
                {
                    IdClaseCubrimiento = ddlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? Convert.ToInt32(Resources.GlobalWeb.General_ValorCero) : Convert.ToInt32(ddlClaseCubrimiento.SelectedValue)
                },
                Tipo = Resources.GlobalWeb.General_TipoCondicionCubrimiento,
                Componente = Resources.GlobalWeb.General_ValorNA
            };

            Resultado<int> resultado = WebService.Facturacion.GuardarCondicionCubrimiento(codCubrimiento);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CondicionCubrimiento_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeERROR;
            }
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
            DdlClaseServicio.Enabled = estado;
            DdlTipoAtencion.Enabled = estado;
            ddlClaseCubrimiento.Enabled = estado;
            ddlTipoRelacion.Enabled = estado;
            txtValor.Enabled = estado;
            txtVigenciaCondicion.Enabled = estado;
            txtDescripcion.Enabled = estado;
            txtIdAtencion.Enabled = estado;
            ImgBuscarAtencion.Enabled = estado;
        }

        /// <summary>
        /// Obtiene la información del tipo de relación a aplicar.
        /// </summary>
        /// <param name="identificadorMaestra">The id maestra.</param>
        /// <param name="identificadorPagina">The id pagina.</param>
        /// <param name="identificadorTipoRelacion">The id tipo relacion.</param>
        /// <returns>
        /// Expresión regular a aplicar.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Maestras ObtenerTipoRelacionPorProceso(int identificadorMaestra, int identificadorPagina, int identificadorTipoRelacion)
        {
            var tipoRelacion = (from item in TiposRelacion
                                where item.IdMaestra == identificadorMaestra
                                && item.IdPagina == identificadorPagina
                                && item.Valor == identificadorTipoRelacion
                                select item).FirstOrDefault();

            return tipoRelacion;
        }

        /// <summary>
        /// Método de validación de formato de valor.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 16/01/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ValidarFormatoCampoValor()
        {
            RecargarModal();
            if (ddlTipoRelacion.SelectedValue == Resources.GlobalWeb.General_ValorCCPorcentaje)
            {
                txtValor.Text = Resources.GlobalWeb.General_ValorCero;
                txtValor.MaxLength = 3;
                RevTxtValor.Enabled = true;
            }
            else
            {
                txtValor.Text = Resources.GlobalWeb.General_ValorCero;
                txtValor.MaxLength = 12;
                RevTxtValor.Enabled = false;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}