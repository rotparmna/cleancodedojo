// --------------------------------
// <copyright file="UC_BuscarCubrimiento.ascx.cs" company="InterGrupo S.A.">
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
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCubrimiento.
    /// </summary>
    public partial class UC_BuscarCubrimiento : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The CUBRIMIENTOENTIDAD
        /// </summary>
        private const string CUBRIMIENTOENTIDAD = "Cubrimiento";

        /// <summary>
        /// The ESTADOCONSULTA
        /// </summary>
        private const string ESTADOCONSULTA = "EstadoConsulta";

        /// <summary>
        /// The MODOPANATALLA
        /// </summary>
        private const string MODOPANATALLA = "ModoPantalla";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece cubrimiento.
        /// </summary>
        /// <value>
        /// Tipo dato Cubrimiento.
        /// </value>
        public Cubrimiento Cubrimiento
        {
            get
            {
                return ViewState[CUBRIMIENTOENTIDAD] != null ? (Cubrimiento)ViewState[CUBRIMIENTOENTIDAD] : new Cubrimiento();
            }

            set
            {
                ViewState[CUBRIMIENTOENTIDAD] = value;
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

        #endregion Propiedades Publicas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Carga los componentes.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarTiposComponentes()
        {
            CargarListasIniciales();
            DdlComponente.DataSource = WebService.Facturacion.ConsultarComponentes(VinculacionSeleccionada.IdAtencion, 0).Objeto;
            DdlComponente.DataValueField = "CodigoComponente";
            DdlComponente.DataTextField = "NombreComponente";
            DdlComponente.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);
        }

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
            LblMensaje.Visible = false;
            txtEntidad.Text = string.Empty;
            txtIdAtencion.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtPlan.Text = string.Empty;
            fsResultado.Visible = false;
            grvCubrimientos.DataSource = null;
            grvCubrimientos.DataBind();
        }

        /// <summary>
        /// Limpiar Campos Configuracion.
        /// </summary>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 09/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public void LimpiarCamposConfiguracion()
        {
            this.mltvDC.SetActiveView(vConsultaDC);
            txtIdAtencion.Text = string.Empty;
            txtTipoProducto.Text = string.Empty;
            txtGrupoProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtComponente.Text = string.Empty;
            txtPlan.Text = string.Empty;
        }

        /// <summary>
        /// Método que realiza el precargue de la información de la página de definir cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void PrecargueInformacion(Cubrimiento cubrimiento)
        {
            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                AjustarModoPantalla(true);
            }
            else
            {
                AjustarModoPantalla(false);
            }

            this.Cubrimiento = cubrimiento;
            lblIdEntidad.Text = cubrimiento.Tercero.Id.ToString();
            txtEntidad.Text = cubrimiento.Tercero.Nombre.ToString();
            lblIdContrato.Text = cubrimiento.Contrato.Id.ToString();
            txtContrato.Text = cubrimiento.Contrato.Nombre.ToString();
            txtIdAtencion.Text = cubrimiento.IdAtencion.ToString();
            if (!ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                lblIdPlan.Text = cubrimiento.Plan.Id.ToString();
                txtPlan.Text = cubrimiento.Plan.Nombre.ToString();
            }
        }

        #endregion Metodos Publicos
        #region Metodos Protegidos

        /// <summary>
        /// Evento para controlar el evento de seleccion del item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCubrimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RecargarModal();
            var indiceFila = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                CargarPantallaModificacionModo(true, indiceFila);
            }

            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR)
            {
                CargarPantallaModificacionModo(false, indiceFila);
            }
        }

        /// <summary>
        /// Metodo de Carga de Cubrimientos
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCubrimientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkActivo");
                chk.Enabled = false;

                if (chkActivo.Checked)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }

        /// <summary>
        /// grvCubrimientos RowEditing.
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
        protected void GrvCubrimientos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Se devuelve a la vista de consulta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImbCerrar_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            mltvDC.SetActiveView(vConsultaDC);
        }

        /// <summary>
        /// Click cubrimiento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgAdmCubrimiento_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucCrearCubrimiento.LimpiarCampos();
            ucCrearCubrimiento.ModoPantalla = ModoPantalla;
            ucCrearCubrimiento.Cubrimiento = Cubrimiento;
            if (!ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                ucCrearCubrimiento.CargarTiposComponentes();
            }
            else
            {
                ucCrearCubrimiento.CargarPlanes();
            }

            ucCrearCubrimiento.CargarTiposdeProducto();
            ucCrearCubrimiento.lblTitulo.Text = Resources.DefinirCubrimientos.DefinirCubrimientos_Crear;
            ucCrearCubrimiento.chkActivo.Checked = true;
            ucCrearCubrimiento.CargarInformacionCubrimiento(InformacionCubrimiento());
            mltvDC.SetActiveView(CrearModificarDC);
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
        /// Evento init.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            pagControlCubrimiento.PageIndexChanged += PagControlCubrimiento_PageIndexChanged;
            ucCrearCubrimiento.OperacionEjecutada += CrearCubrimiento_OperacionEjecutada;
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
        protected void PagControlCubrimiento_PageIndexChanged(EventoControles<Paginador> e)
        {
            RecargarModal();
            CargarGrilla(e.Resultado.PaginaActual);
        }

        /// <summary>
        /// Se ejecuta cuando se carga la página.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/04/2013
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
        /// Ajustar Modo Pantalla.
        /// </summary>
        /// <param name="configuracion">if set to <c>true</c> [configuracion].</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 04/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void AjustarModoPantalla(bool configuracion)
        {
            txtComponente.Visible = configuracion;
            DdlComponente.Visible = !configuracion;
            DdlClaseCubrimiento.Visible = !configuracion;
            txtCubrimiento.Visible = configuracion;
            txtPlan.Enabled = configuracion;
            txtPlan.ReadOnly = !configuracion;
            txtIdAtencion.ReadOnly = !configuracion;
            txtIdAtencion.Enabled = configuracion;
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
                IndHabilitado = 1
            };

            DdlClaseCubrimiento.DataSource = WebService.Facturacion.ConsultarClasesCubrimiento(claseCubrimiento).Objeto;
            DdlClaseCubrimiento.DataValueField = "IdClaseCubrimiento";
            DdlClaseCubrimiento.DataTextField = "Nombre";
            DdlClaseCubrimiento.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlClaseCubrimiento, false);
        }

        /// <summary>
        /// Cargar datos cubrimiento seleccionado
        /// </summary>
        /// <param name="indiceFila">The indice fila.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 26/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private Paginacion<Cubrimiento> CargarDatosCubrimientoSeleccionado(int indiceFila)
        {
            var identificadorCubrimiento = Convert.ToInt32(grvCubrimientos.DataKeys[indiceFila].Value);
            Label lidClaseCubrimiento = (Label)grvCubrimientos.Rows[indiceFila].Cells[9].FindControl("lblIdClaseCubrimiento");
            Label lidTipoProducto = (Label)grvCubrimientos.Rows[indiceFila].Cells[11].FindControl("lblIdTipoProducto");
            Label lidGrupoProducto = (Label)grvCubrimientos.Rows[indiceFila].Cells[12].FindControl("lblIdGrupo");
            Label lidProducto = (Label)grvCubrimientos.Rows[indiceFila].Cells[8].FindControl("lblIdProducto");
            Label lcodComponente = (Label)grvCubrimientos.Rows[indiceFila].Cells[10].FindControl("lblCodigoComponente");
            int tercero = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Tercero.Id : VinculacionSeleccionada.Tercero.Id;
            string entidad = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Tercero.Nombre : VinculacionSeleccionada.Tercero.Nombre;
            int identificadorContrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Contrato.Id : VinculacionSeleccionada.Contrato.Id;
            string nombreContrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Contrato.Nombre : VinculacionSeleccionada.Contrato.Nombre;
            Label planId = (Label)grvCubrimientos.Rows[indiceFila].Cells[2].FindControl("lblIdPlanGrid");
            Label planNombre = (Label)grvCubrimientos.Rows[indiceFila].Cells[3].FindControl("lblPlanGrid");
            int identificadorPlanSeleccion = 0;
            bool validaPlan = int.TryParse(planId.Text, out identificadorPlanSeleccion);
            int identificadorPlan = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? identificadorPlanSeleccion : VinculacionSeleccionada.Plan.Id;
            string nombrePlan = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? planNombre.Text : VinculacionSeleccionada.Plan.Nombre;
            Paginacion<Cubrimiento> paginador = new Paginacion<Cubrimiento>()
            {
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                PaginaActual = 0,
                Item = new Cubrimiento()
                {
                    IdCubrimiento = identificadorCubrimiento,
                    CodigoEntidad = Settings.Default.General_CodigoEntidad,
                    IdAtencion = VinculacionSeleccionada.IdAtencion,
                    IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                    TipoComponente = new TipoComponente()
                    {
                        CodigoComponente = lcodComponente.Text
                    },
                    ClaseCubrimiento = new ClaseCubrimiento()
                    {
                        IdClaseCubrimiento = Convert.ToInt32(lidClaseCubrimiento.Text)
                    },
                    GrupoProducto = new GrupoProducto()
                    {
                        IdGrupo = Convert.ToInt32(lidGrupoProducto.Text),
                        Nombre = grvCubrimientos.Rows[indiceFila].Cells[8].Text
                    },
                    Producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(lidProducto.Text),
                        Nombre = grvCubrimientos.Rows[indiceFila].Cells[11].Text
                    },
                    TipoProducto = new TipoProducto()
                    {
                        IdTipoProducto = Convert.ToInt16(lidTipoProducto.Text),
                        Nombre = grvCubrimientos.Rows[indiceFila].Cells[7].Text
                    },
                    Tercero = new Tercero()
                    {
                        Id = tercero,
                        Nombre = entidad
                    },
                    Contrato = new Contrato()
                    {
                        Id = identificadorContrato,
                        Nombre = nombreContrato
                    },
                    Plan = new Plan()
                    {
                        Id = identificadorPlan,
                        Nombre = nombrePlan
                    },

                    Componente = lcodComponente.Text
                }
            };

            return paginador;
        }

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
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Facturacion.ConsultarCubrimientos(consulta);

            if (resultado.Ejecuto)
            {
                CargaObjetos.OrdenamientoGrilla(this.Page, grvCubrimientos, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
                pagControlCubrimiento.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvCubrimientos.DataSource = null;
                grvCubrimientos.DataBind();
            }
        }

        /// <summary>
        /// Carga de listas inicial.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarListasIniciales()
        {
            CargarClasesCubrimientos();
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
        private void CargarPaginador(Paginacion<List<Cubrimiento>> resultado)
        {
            pagControlCubrimiento.ObjetoPaginador = new Paginador()
            {
                CantidadPaginas = resultado.CantidadPaginas,
                LongitudPagina = resultado.LongitudPagina,
                MaximoPaginas = Properties.Settings.Default.Paginacion_CantidadBotones,
                PaginaActual = resultado.PaginaActual,
                TotalRegistros = resultado.TotalRegistros
            };
        }

        /// <summary>
        /// Cargar Pantalla Modificación Modo.
        /// </summary>
        /// <param name="modoConsulta">If set to <c>true</c> [modo consulta].</param>
        /// <param name="indiceFila">The indice fila.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 27/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarPantallaModificacionModo(bool modoConsulta, int indiceFila)
        {
            this.EstadoConsulta = modoConsulta ? CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR : CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR;
            ucCrearCubrimiento.lblTitulo.Text = modoConsulta ? Resources.DefinirCubrimientos.DefinirCubrimientos_Consultar : Resources.DefinirCubrimientos.DefinirCubrimientos_Actualizar;
            var paginador = CargarDatosCubrimientoSeleccionado(indiceFila);
            ucCrearCubrimiento.LimpiarCampos();
            ucCrearCubrimiento.CargarTiposdeProducto();
            ucCrearCubrimiento.ModoPantalla = ModoPantalla;
            ucCrearCubrimiento.Cubrimiento = Cubrimiento;
            ucCrearCubrimiento.CargarInformacionCubrimiento(paginador.Item);
            ucCrearCubrimiento.CargarControlesModoConsulta(!modoConsulta);
            mltvDC.SetActiveView(CrearModificarDC);
        }

        /// <summary>
        /// Evento para recargar la vista de consulta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CrearCubrimiento_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvDC.SetActiveView(vConsultaDC);
        }

        /// <summary>
        /// Evento para recargar la vista de consulta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 24/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CrearCubrimiento_OperacionEjecutada(object sender, EventArgs e)
        {
            RecargarModal();
        }

        /// <summary>
        /// Metodo para construir Filtros Cubrimiento.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Objeto Consulta Cubrimiento.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<Cubrimiento> CrearObjetoConsulta(int numeroPagina)
        {
            string componenteSeleccionado = string.IsNullOrEmpty(DdlComponente.SelectedValue) || DdlComponente.SelectedValue.Equals(Resources.GlobalWeb.General_ValorNegativo) ? string.Empty : DdlComponente.SelectedItem.Text;
            string cubrimientoSeleccionado = string.IsNullOrEmpty(DdlClaseCubrimiento.SelectedValue) || DdlClaseCubrimiento.SelectedValue.Equals(Resources.GlobalWeb.General_ValorNegativo) ? string.Empty : DdlClaseCubrimiento.SelectedItem.Text;
            string nombreComponente = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? txtComponente.Text.Trim() : componenteSeleccionado;
            string nombreCubrimiento = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? txtCubrimiento.Text.Trim() : cubrimientoSeleccionado;
            string entidad = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? string.Empty : txtEntidad.Text;
            string contrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? string.Empty : txtContrato.Text;
            int identificadorContrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Contrato.Id : VinculacionSeleccionada.Contrato.Id;
            int identificadorTercero = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Tercero.Id : VinculacionSeleccionada.Tercero.Id;
            int identificadorAtencion = 0;
            bool validaAtencion = int.TryParse(txtIdAtencion.Text, out identificadorAtencion);
            string nombrePlan = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? txtPlan.Text.Trim() : string.Empty;
            Paginacion<Cubrimiento> consulta = new Paginacion<Cubrimiento>()
            {
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                PaginaActual = numeroPagina,
                Item = new Cubrimiento()
                {
                    IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                    CodigoEntidad = Settings.Default.General_CodigoEntidad,
                    IdAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? identificadorAtencion : VinculacionSeleccionada.IdAtencion,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    Componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlComponente.SelectedValue.ToString()) ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue,
                    IdClaseCubrimiento = DdlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlClaseCubrimiento.SelectedValue.ToString()) ? int.Parse(Resources.GlobalWeb.General_ValorCero) : int.Parse(DdlClaseCubrimiento.SelectedValue),
                    TipoComponente = new TipoComponente()
                    {
                        CodigoComponente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlComponente.SelectedValue.ToString()) ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue,
                        NombreComponente = nombreComponente
                    },
                    ClaseCubrimiento = new ClaseCubrimiento()
                    {
                        IdClaseCubrimiento = DdlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlClaseCubrimiento.SelectedValue.ToString()) ? int.Parse(Resources.GlobalWeb.General_ValorCero) : int.Parse(DdlClaseCubrimiento.SelectedValue),
                        Nombre = nombreCubrimiento
                    },
                    GrupoProducto = new GrupoProducto()
                    {
                        IndHabilitado = true,
                        Nombre = txtGrupoProducto.Text
                    },
                    Producto = new Producto()
                    {
                        IndHabilitado = true,
                        Nombre = txtProducto.Text
                    },
                    TipoProducto = new TipoProducto()
                    {
                        IndHabilitado = true,
                        Nombre = txtTipoProducto.Text
                    },
                    Tercero = new Tercero()
                    {
                        IndHabilitado = true,
                        Id = identificadorTercero,
                        Nombre = entidad
                    },
                    Contrato = new Contrato()
                    {
                        IndHabilitado = true,
                        Id = identificadorContrato,
                        Nombre = contrato
                    },
                    Plan = new Plan()
                    {
                        Nombre = nombrePlan
                    }
                }
            };

            return consulta;
        }

        /// <summary>
        /// Obtiene la información del cubrimiento.
        /// </summary>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Cubrimiento InformacionCubrimiento()
        {
            int identificadorAtencion = 0;
            bool validaAtencion = int.TryParse(txtIdAtencion.Text, out identificadorAtencion);
            var plan = !ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? new Plan()
                {
                    Id = Convert.ToInt32(lblIdPlan.Text),
                    Nombre = txtPlan.Text
                } 
                : new Plan
                {
                    Nombre = string.Empty
                };
            var cubrimiento = new Cubrimiento()
            {
                Tercero = new Tercero()
                {
                    Id = Convert.ToInt32(lblIdEntidad.Text),
                    Nombre = txtEntidad.Text
                },

                Contrato = new Contrato()
                {
                    Id = Convert.ToInt32(lblIdContrato.Text),
                    Nombre = txtContrato.Text
                },

                Plan = plan,

                TipoProducto = new TipoProducto()
                {
                    Nombre = string.Empty
                },

                GrupoProducto = new GrupoProducto()
                {
                    Nombre = string.Empty
                },

                Producto = new Producto()
                {
                    Nombre = string.Empty
                },

                TipoComponente = new TipoComponente()
                {
                    NombreComponente = string.Empty
                },

                ClaseCubrimiento = new ClaseCubrimiento()
                {
                    Nombre = string.Empty
                },

                IdAtencion = identificadorAtencion
            };

            return cubrimiento;
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}