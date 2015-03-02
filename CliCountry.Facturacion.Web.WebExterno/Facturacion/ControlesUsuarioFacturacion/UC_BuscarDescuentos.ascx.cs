// --------------------------------
// <copyright file="UC_BuscarDescuentos.ascx.cs" company="InterGrupo S.A.">
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
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarDescuentos
    /// </summary>
    public partial class UC_BuscarDescuentos : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CODIGOCOMBO
        /// </summary>
        private const string CODIGOCOMBO = "Codigo";

        /// <summary>
        /// The CONTRATO
        /// </summary>
        private const string CONTRATO = "Contrato";

        /// <summary>
        /// The ESTADOCONSULTA
        /// </summary>
        private const string ESTADOCONSULTA = "EstadoConsulta";

        /// <summary>
        /// The MODOPANATALLA
        /// </summary>
        private const string MODOPANATALLA = "ModoPantalla";

        /// <summary>
        /// The NOMBRECOMBO
        /// </summary>
        private const string NOMBRECOMBO = "Nombre";

        /// <summary>
        /// The TERCEROENTIDAD
        /// </summary>
        private const string TERCEROENTIDAD = "Tercero";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece contrato.
        /// </summary>
        /// <value>
        /// Tipo Dato Contrato.
        /// </value>
        public Contrato Contrato
        {
            get
            {
                return ViewState[CONTRATO] != null ? (Contrato)ViewState[CONTRATO] : new Contrato();
            }

            set
            {
                ViewState[CONTRATO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece estado consulta
        /// </summary>
        public string EstadoConsulta
        {
            get
            {
                return ViewState[ESTADOCONSULTA] != null ? ViewState[ESTADOCONSULTA] as string : string.Empty;
            }

            set
            {
                ViewState[ESTADOCONSULTA] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece tipo operacion
        /// </summary>
        public string ModoPantalla
        {
            get
            {
                return ViewState[MODOPANATALLA] != null ? ViewState[MODOPANATALLA] as string : string.Empty;
            }

            set
            {
                ViewState[MODOPANATALLA] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece tercero
        /// </summary>
        public Tercero Tercero
        {
            get
            {
                return ViewState[TERCEROENTIDAD] != null ? (Tercero)ViewState[TERCEROENTIDAD] : new Tercero();
            }

            set
            {
                ViewState[TERCEROENTIDAD] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para Realizar Carga de Datos Iniciales.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarDatosControles(Tercero tercero)
        {
            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                Tercero = tercero;
                CargarComboTipoRelacion();
                txtEntidadDC.Text = Tercero.Nombre;
                txtContratoDC.Text = VinculacionSeleccionada.Contrato.Nombre;
                ConfigurararModoPantalla(true);
            }
            else
            {
                if (VinculacionSeleccionada != null)
                {
                    txtEntidadDC.Text = VinculacionSeleccionada.Tercero.Nombre;
                    txtContratoDC.Text = VinculacionSeleccionada.Contrato.Nombre;
                    txtPlanDC.Text = VinculacionSeleccionada.Plan.Nombre;
                    txtAtencionDC.Text = VinculacionSeleccionada.IdAtencion.ToString();
                    CargarComponente();
                    CargarComboTipoRelacion();
                }

                ConfigurararModoPantalla(false);
            }
        }

        /// <summary>
        /// Inicializa los campos de texto del control web.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            mltvDescuentos.ActiveViewIndex = 0;
            LblMensaje.Visible = false;
            fsResultado.Visible = false;
            txtEntidadDC.Text = string.Empty;
            txtContratoDC.Text = string.Empty;
            txtPlanDC.Text = string.Empty;
            txtAtencionDC.Text = string.Empty;
            txtTipoProductoDes.Text = string.Empty;
            txtGrupoProductoDes.Text = string.Empty;
            txtProductoDes.Text = string.Empty;
            txtClaseServicio.Text = string.Empty;
            txtTipoAtencion.Text = string.Empty;
            txtComponente.Text = string.Empty;
            ddlComponenteDes.SelectedIndex = -1;
            ddlTipoRelacionDes.SelectedIndex = -1;
            chkActivoDes.Checked = true;
            grvDescuentos.DataSource = null;
            grvDescuentos.DataBind();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 
        
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
        protected void GrvDescuentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RecargarModal();
            int indiceFila = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                this.EstadoConsulta = CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR;
                CargarDatosPaginaCrear(indiceFila);
            }

            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR)
            {
                this.EstadoConsulta = CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR;
                CargarDatosPaginaCrear(indiceFila);
            }
        }

        /// <summary>
        /// grvDescuentos RowEditing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 26/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void GrvDescuentos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
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
        /// Metodo para realizar la creación de Nuevo Descuento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgNuevo_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            mltvDescuentos.SetActiveView(vCrearDescuento);
            ucDescuentos.LimpiarControles();
            int identificadorAtencion = 0;
            bool validaAtencion = int.TryParse(txtAtencionDC.Text, out identificadorAtencion);
            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                ucDescuentos.Tercero = Tercero;
                ucDescuentos.Descuento = new DescuentoConfiguracion()
                {
                    NombrePlan = txtPlanDC.Text,
                    NombreTercero = txtEntidadDC.Text,
                    NombreContrato = txtContratoDC.Text,
                    IdAtencion = identificadorAtencion,
                    IdTercero = Tercero.Id,
                    CodigoEntidad = Tercero.Id.ToString()
                };
            }
            else
            {
                ucDescuentos.Descuento = new DescuentoConfiguracion()
                {
                    NombrePlan = txtPlanDC.Text,
                    NombreTercero = txtEntidadDC.Text,
                    NombreContrato = txtContratoDC.Text,
                    IdAtencion = VinculacionSeleccionada.IdAtencion,
                    IdTercero = VinculacionSeleccionada.Tercero.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    CodigoEntidad = VinculacionSeleccionada.CodigoEntidad
                };
            }

            ucDescuentos.Contrato = Contrato;
            ucDescuentos.ModoPantalla = ModoPantalla;
            ucDescuentos.CargarControles(Global.TipoOperacion.CREACION);
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
            pagControl.PageIndexChanged -= PagControl_PageIndexChanged;
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
            ucDescuentos.OperacionEjecutada += Descuentos_OperacionEjecutada;
            pagControl.PageIndexChanged += PagControl_PageIndexChanged;

            base.OnInit(e);
        }

        /// <summary>
        /// Metodo para CONTROLAR evento de Cambio de Pagina.
        /// </summary>
        /// <param name="e">Parametro e.</param>
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
            var resultado = WebService.Integracion.ConsultarGenListas(new Basica { CodigoGrupo = Resources.GlobalWeb.CodGenLista_Relacion, IndHabilitado = true });
            if (resultado.Ejecuto)
            {
                ddlTipoRelacionDes.DataTextField = NOMBRECOMBO;
                ddlTipoRelacionDes.DataValueField = CODIGOCOMBO;
                ddlTipoRelacionDes.DataSource = resultado.Objeto;
                ddlTipoRelacionDes.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(ddlTipoRelacionDes, false);
                ddlTipoRelacionDes.SelectedIndex = 0;
            }
            else
            {
                ddlTipoRelacionDes.DataSource = null;
                ddlTipoRelacionDes.DataBind();
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para Cargar Componente.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarComponente()
        {
            var resultado = WebService.Facturacion.ConsultarComponentes(VinculacionSeleccionada.IdAtencion, 0);

            if (resultado.Ejecuto)
            {
                ddlComponenteDes.DataTextField = "NombreComponente";
                ddlComponenteDes.DataValueField = "CodigoComponente";
                ddlComponenteDes.DataSource = resultado.Objeto;
                ddlComponenteDes.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(ddlComponenteDes, false);
            }
            else
            {
                ddlComponenteDes.DataSource = null;
                ddlComponenteDes.DataBind();
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Cargar datos página crear.
        /// </summary>
        /// <param name="indiceFila">The indice fila.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 19/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarDatosPaginaCrear(int indiceFila)
        {
            ucDescuentos.LimpiarControles();
            var identificadorDescuento = Convert.ToInt32(grvDescuentos.DataKeys[indiceFila].Value);
            bool modificar = this.EstadoConsulta.Equals(CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR);
            var resultado = WebService.Configuracion.ConsultaDescuentoConfiguracion(new Paginacion<DescuentoConfiguracion>()
            {
                PaginaActual = 0,
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new DescuentoConfiguracion()
                {
                    Id = identificadorDescuento,
                    IndicadorActivo = Convert.ToInt16(chkActivoDes.Checked)
                }
            });
            if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
            {
                mltvDescuentos.SetActiveView(vCrearDescuento);
                ucDescuentos.LimpiarControles();
                ucDescuentos.Contrato = Contrato;
                ucDescuentos.ModoPantalla = ModoPantalla;
                ucDescuentos.Descuento = (DescuentoConfiguracion)resultado.Objeto.Item.FirstOrDefault();
                ucDescuentos.Descuento.NombreContrato = txtContratoDC.Text;
                ucDescuentos.Descuento.NombreTercero = txtEntidadDC.Text;
                ucDescuentos.Descuento.NombrePlan = txtPlanDC.Text;
                ucDescuentos.Descuento.CodigoEntidad = Tercero.Id.ToString();
                ucDescuentos.Tercero = Tercero;
                ucDescuentos.CargarControles(Global.TipoOperacion.MODIFICACION);
                ucDescuentos.CambiarEstadoControlesFormulario(modificar);
            }
        }

        /// <summary>
        /// Obtiene los parametros para la información de busque.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
        /// UltimaModificacionPor: Aura Victoria Forero Varela - INTERGRUPO\Aforero
        /// FechaDeUltimaModificacion: (06/09/2013)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Configuracion.ConsultaDescuentoConfiguracion(consulta);

            if (resultado.Ejecuto)
            {
                CargaObjetos.OrdenamientoGrilla(this.Page, grvDescuentos, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
                pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvDescuentos.DataSource = null;
                grvDescuentos.DataBind();
            }
        }

        /// <summary>
        /// Metodo para cargar paginador
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarPaginador(Paginacion<List<DescuentoConfiguracion>> resultado)
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
        /// Configurar modo pantalla.
        /// </summary>
        /// <param name="configuracion">If set to <c>true</c> [configuracion].</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ConfigurararModoPantalla(bool configuracion)
        {
            txtComponente.Visible = configuracion;
            ddlComponenteDes.Visible = !configuracion;
            txtClaseServicio.Enabled = configuracion;
            txtTipoAtencion.Enabled = configuracion;
            txtAtencionDC.Enabled = configuracion;
            txtPlanDC.ReadOnly = !configuracion;
            txtPlanDC.Enabled = configuracion;
            trServicioAtencion.Visible = configuracion;
        }

        /// <summary>
        /// Metodo para Crear Objeto de Consulta.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Retorna .Tercero
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<DescuentoConfiguracion> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<DescuentoConfiguracion> consulta = new Paginacion<DescuentoConfiguracion>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new DescuentoConfiguracion()
                {
                    Componente = ddlComponenteDes.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? string.Empty : ddlComponenteDes.SelectedValue,
                    CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IdTercero = Tercero.Id,
                    IdAtencion = !string.IsNullOrEmpty(txtAtencionDC.Text) ? Convert.ToInt32(txtAtencionDC.Text) : 0,
                    NombreProducto = txtProductoDes.Text,
                    NombreGrupoProducto = txtGrupoProductoDes.Text,
                    NombreTipoProducto = txtTipoProductoDes.Text,
                    IndicadorActivo = Convert.ToInt16(chkActivoDes.Checked),
                    IdTipoRelacion = ddlTipoRelacionDes.SelectedIndex > 0 ? Convert.ToInt16(ddlTipoRelacionDes.SelectedValue) : (short)0,
                    NombrePlan = txtPlanDC.Text.Trim(),
                    NombreServicio = txtClaseServicio.Text.Trim(),
                    NombreTipoAtencion = txtTipoAtencion.Text.Trim(),
                    NombreTipoComponente = txtComponente.Text.Trim()
                }
            };

            return consulta;
        }

        /// <summary>
        /// Metodo que controla el evento de regresar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void Descuentos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            if (tipoOperacion == Global.TipoOperacion.SALIR)
            {
                CargarGrilla(0);
            }

            RecargarModal();
            mltvDescuentos.SetActiveView(vDescuentos);
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}