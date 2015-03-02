// --------------------------------
// <copyright file="UC_VincularEntidad.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_VincularEntidad
    /// </summary>
    /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
    public partial class UC_VincularEntidad : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The IDCONTRATO
        /// </summary>
        private const string IDCONTRATO = "Contrato";

        /// <summary>
        /// The IDPLAN
        /// </summary>
        private const string IDPLAN = "Plan";

        /// <summary>
        /// The TEXTO
        /// </summary>
        private const string TEXTO = "Nombre";

        /// <summary>
        /// The ID
        /// </summary>
        private const string VALOR = "Id";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Privadas

        /// <summary>
        /// Obtiene o establece identificador contrato
        /// </summary>
        private Contrato IdentificadorContrato
        {
            get
            {
                return ViewState[IDCONTRATO] as Contrato;
            }

            set
            {
                ViewState[IDCONTRATO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece identificador contrato
        /// </summary>
        private Plan IdentificadorPlan
        {
            get
            {
                return ViewState[IDPLAN] as Plan;
            }

            set
            {
                ViewState[IDPLAN] = value;
            }
        }

        #endregion Propiedades Privadas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Método para actualizar la vinculación.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void ActualizarVinculacion()
        {
            Vinculacion vinculacion = new Vinculacion()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdAtencion = Convert.ToInt32(lblIdAtencion.Text),
                Contrato = new Contrato()
                {
                    Id = VinculacionSeleccionada.Contrato.Id
                },
                Plan = new Plan()
                {
                    Id = VinculacionSeleccionada.Plan.Id
                },
                IdTipoAfiliacion = Convert.ToInt32(DdlTipoAfiliacion.SelectedValue),
                IdEstado = 0,
                Orden = Convert.ToInt32(txtOrden.Text),
                NumeroAfiliacion = txtNumeroAfiliacion.Text,
                IndHabilitado = chkActivoVE.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                MontoEjecutado = Convert.ToDecimal(txtMontoEjecutado.Text)
            };

            Resultado<int> resultado = WebService.Facturacion.ActualizarVinculacion(vinculacion);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearVinculacion_MsjActualizacion, resultado.Objeto), TipoMensaje.Ok);
                LblMensaje.Visible = true;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                LblMensaje.Visible = true;
            }
        }

        /// <summary>
        /// Metodo de Carga de Combos
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 16/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarCombos()
        {
            CargarNivel();
            CargarTipoAfiliacion();
        }

        /// <summary>
        /// Carga la información de la vinculación seleccionada en modo edición.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 09/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarInformacionVinculacion()
        {
            txtEntidad.Text = VinculacionSeleccionada.Tercero.Nombre;
            txtContrato.Text = VinculacionSeleccionada.Contrato.Nombre;
            txtPlan.Text = VinculacionSeleccionada.Plan.Nombre;
            DdlTipoAfiliacion.SelectedValue = VinculacionSeleccionada.IdTipoAfiliacion.ToString();
            txtMontoEjecutado.Text = VinculacionSeleccionada.MontoEjecutado.ToString();
            chkActivoVE.Checked = VinculacionSeleccionada.IndHabilitado == 1 ? true : false;
            chkActivoVE.Enabled = true;
        }

        /// <summary>
        /// Método para guardar la vinculación.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void GuardarVinculacion()
        {
            Vinculacion vinculacion = new Vinculacion()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdAtencion = Convert.ToInt32(lblIdAtencion.Text),
                Contrato = new Contrato()
                {
                    Id = IdentificadorContrato.Id
                },
                Plan = new Plan()
                {
                    Id = IdentificadorPlan.Id
                },
                IdTipoAfiliacion = DdlTipoAfiliacion.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? Convert.ToInt32(Resources.GlobalWeb.General_ValorCero) : Convert.ToInt32(DdlTipoAfiliacion.SelectedValue),
                IdEstado = Convert.ToInt32(0),
                Orden = Convert.ToInt32(txtOrden.Text),
                NumeroAfiliacion = txtNumeroAfiliacion.Text,
                IndHabilitado = chkActivoVE.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                MontoEjecutado = Convert.ToDecimal(txtMontoEjecutado.Text)
            };

            Resultado<int> resultado = WebService.Facturacion.GuardarVinculacion(vinculacion);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearVinculacion_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
                LblMensaje.Visible = true;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                LblMensaje.Visible = true;
            }
        }

        /// <summary>
        /// Limpia los campos de forma inicial.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 09/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            txtEntidad.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtPlan.Text = string.Empty;
            txtMontoEjecutado.Text = Resources.GlobalWeb.General_ValorCero;
            LblMensaje.Text = string.Empty;
        }

        #endregion Metodos Publicos

        #region Metodos Protegidos

        /// <summary>
        /// Metodo para Buscar Contrato Plan
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBtnContratoPlan_Click(object sender, ImageClickEventArgs e)
        {
            LblMensaje.Visible = false;
            ucBuscarContratoPlan.LimpiarCampos();
            RecargarModal();
            mltvVincularEntidad.SetActiveView(vBuscarContratoPlan);
        }

        /// <summary>
        /// Metodo para almacenar la vinculacion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgGuardarVinculacionEntidad_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            var grupo = DdlTipoAfiliacion.ValidationGroup;
            Page.Validate(grupo);

            if (Page.IsValid)
            {
                if (lblTitulo.Text == Resources.VincularEntidad.VincularEntidad_Titulo)
                {
                    GuardarVinculacion();
                }
                else
                {
                    ActualizarVinculacion();
                }
            }
        }

        /// <summary>
        /// Metodo de regresar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
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
        /// Metodo de Inicializacion Pagina
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarContratoPlan.OperacionEjecutada += BuscarContratoPlan_OperacionEjecutada;
            ucBuscarContratoPlan.SeleccionarItemGrid += BuscarContratoPlan_SeleccionarItemGrid;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo de Carga de la Pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarNivel();
                CargarTipoAfiliacion();
            }
        }

        #endregion Metodos Protegidos

        #region Metodos Privados

        /// <summary>
        /// Metodo de Operacion Ejecutada
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 16/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void BuscarContratoPlan_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvVincularEntidad.SetActiveView(vVincularEntidad);
        }

        /// <summary>
        /// Metodo de Seleccion del Contrato Plan
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 16/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void BuscarContratoPlan_SeleccionarItemGrid(EventoControles<ContratoPlan> e)
        {
            RecargarModal();
            mltvVincularEntidad.SetActiveView(vVincularEntidad);

            if (e.Resultado != null)
            {
                IdentificadorContrato = e.Resultado.Contrato;
                IdentificadorPlan = e.Resultado.Plan;
                txtEntidad.Text = e.Resultado.Tercero.Nombre;
                txtContrato.Text = e.Resultado.Contrato.Nombre;
                txtPlan.Text = e.Resultado.Plan.Nombre;
            }
        }

        /// <summary>
        /// Metodo para cargar Nivel Categoria
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarNivel()
        {
            var respuesta = WebService.Integracion.ConsultarNiveles(new Nivel() { IndHabilitado = true });

            if (respuesta.Ejecuto)
            {
                DdlNivel.DataTextField = TEXTO;
                DdlNivel.DataValueField = VALOR;
                DdlNivel.DataSource = respuesta.Objeto;
                DdlNivel.DataBind();
                PreseleccionarNivel();
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Tipo de Afiliacion
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarTipoAfiliacion()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "afil" });

            if (respuesta.Ejecuto)
            {
                DdlTipoAfiliacion.DataTextField = TEXTO;
                DdlTipoAfiliacion.DataValueField = VALOR;
                DdlTipoAfiliacion.DataSource = respuesta.Objeto;
                DdlTipoAfiliacion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlTipoAfiliacion, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para preseleccionar Nivel
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void PreseleccionarNivel()
        {
            var nivel = DdlNivel.Items.FindByText(Properties.Settings.Default.Nivel_Cliente);
            DdlNivel.SelectedValue = nivel.Value;
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}