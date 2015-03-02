// --------------------------------
// <copyright file="UC_CrearCubrimiento.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCubrimiento.
    /// </summary>
    public partial class UC_CrearCubrimiento : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CODIGOCOMPONENTE
        /// </summary>
        private const string CODIGOCOMPONENTE = "CodigoComponente";

        /// <summary>
        /// The CUBRIMIENTOENTIDAD
        /// </summary>
        private const string CUBRIMIENTOENTIDAD = "Cubrimiento";

        /// <summary>
        /// The IDCOMBO
        /// </summary>
        private const string IDCOMBO = "Id";

        /// <summary>
        /// The INFORMACIONTERCERO
        /// </summary>
        private const string INFORMACIONTERCERO = "InformacionTercero";

        /// <summary>
        /// The MODOPANATALLA.
        /// </summary>
        private const string MODOPANATALLA = "ModoPantalla";

        /// <summary>
        /// The NOMBRECOMBO
        /// </summary>
        private const string NOMBRECOMBO = "Nombre";

        /// <summary>
        /// The NOMBRECOMPONENTE
        /// </summary>
        private const string NOMBRECOMPONENTE = "NombreComponente";

        /// <summary>
        /// The NOQX
        /// </summary>
        private const string NOQX = "N";

        /// <summary>
        /// The QX
        /// </summary>
        private const string QX = "Q";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece cubrimiento
        /// </summary>
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
        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece informacion tercro
        /// </summary>
        private InformacionBasicaTercero InformacionTercero
        {
            get
            {
                return ViewState[INFORMACIONTERCERO] as InformacionBasicaTercero;
            }

            set
            {
                ViewState[INFORMACIONTERCERO] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Cargar controles modo consulta
        /// </summary>
        /// <param name="modifica">if set to <c>true</c> [modifica].</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 26/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarControlesModoConsulta(bool modifica)
        {
            imgBuscarAtencion.Visible = modifica;
            txtIdGrupoProducto.Enabled = modifica;
            txtIdProducto.Enabled = modifica;
            imgBuscarAtencion.Visible = modifica;
            imgConsultarGrupoProducto.Visible = modifica;
            imgConsultarProducto.Visible = modifica;
            btnGuardar.Visible = modifica;
            chkActivo.Disabled = !modifica;
            lblGuardar.Visible = modifica;
            ddlPlan.Enabled = modifica;
            DdlClaseCubrimiento.Enabled = modifica;
            DdlComponente.Enabled = modifica;
            DdlTipoProducto.Enabled = modifica;
        }

        /// <summary>
        /// Carga la información del cubrimiento seleccionado en la principal para su modificación.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarInformacionCubrimiento(Cubrimiento cubrimiento)
        {
            string clasif = EstablecerClasificacionProducto(Convert.ToInt16(cubrimiento.TipoProducto.IdTipoProducto));
            CargarListaTiposComponentes(clasif);
            txtIdentificador.Text = cubrimiento.IdCubrimiento.ToString();
            txtEntidad.Text = cubrimiento.Tercero.Nombre.ToString();
            txtIdContrato.Text = cubrimiento.Contrato.Id.ToString();
            txtContrato.Text = cubrimiento.Contrato.Nombre.ToString();
            txtIdPlan.Text = cubrimiento.Plan.Id.ToString();
            txtPlan.Text = cubrimiento.Plan.Nombre.ToString();
            txtIdAtencion.Text = VinculacionSeleccionada.IdAtencion.ToString();
            DdlTipoProducto.SelectedValue = cubrimiento.TipoProducto.IdTipoProducto != 0 ? cubrimiento.TipoProducto.IdTipoProducto.ToString() : Resources.GlobalWeb.General_ValorNegativo;
            txtIdGrupoProducto.Text = cubrimiento.GrupoProducto.IdGrupo.ToString();
            txtGrupoProducto.Text = cubrimiento.GrupoProducto.Nombre.ToString();
            txtIdProducto.Text = cubrimiento.Producto.IdProducto.ToString();
            txtProducto.Text = cubrimiento.Producto.Nombre.ToString();
            if (lblTitulo.Text == Resources.DefinirCubrimientos.DefinirCubrimientos_Actualizar)
            {
                if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
                {
                    CargarComponenteConfiguracion();
                }
                else
                {
                    CargarListaTiposComponentes(clasif);
                }

                chkActivo.Checked = cubrimiento.IndHabilitado == 1 ? true : false;
                DdlComponente.SelectedValue = cubrimiento.Componente == null ? Resources.GlobalWeb.General_ValorNA : cubrimiento.Componente.ToString();
                DdlClaseCubrimiento.SelectedValue = cubrimiento.ClaseCubrimiento.IdClaseCubrimiento.ToString();
                DdlTipoProducto.SelectedValue = cubrimiento.TipoProducto.IdTipoProducto != 0 ? cubrimiento.TipoProducto.IdTipoProducto.ToString() : Resources.GlobalWeb.General_ValorNegativo;
                if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
                {
                    CargarPlanes();
                    var listItem = new System.Web.UI.WebControls.ListItem(cubrimiento.Plan.Nombre, cubrimiento.Plan.Id.ToString());
                    if (ddlPlan.Items.Contains(listItem))
                    {
                        ddlPlan.SelectedValue = cubrimiento.Plan.Id.ToString();
                    }
                }
            }
            else
            {
                txtIdGrupoProducto.Text = string.Empty;
            }

            ddlPlan.Visible = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? true : false;
            txtPlan.Visible = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? false : true;
            txtIdPlan.Visible = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? false : true;
            imgBuscarAtencion.Visible = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? true : false;
            btnGuardar.Visible = true;
            lblGuardar.Visible = true;
        }

        /// <summary>
        /// Cargar planes.
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 06/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarPlanes()
        {
            var contrato = new Contrato { Id = Cubrimiento.Contrato.Id, IdTercero = Cubrimiento.Contrato.IdEntidad };
            var resultado = WebService.Facturacion.ConsultarPlanes(contrato);
            if (resultado.Ejecuto)
            {
                ddlPlan.DataSource = resultado.Objeto;
                ddlPlan.DataValueField = IDCOMBO;
                ddlPlan.DataTextField = NOMBRECOMBO;
                ddlPlan.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(ddlPlan, false);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

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
            DdlComponente.DataSource = WebService.Facturacion.ConsultarComponentes(VinculacionSeleccionada.IdAtencion, 0).Objeto;
            DdlComponente.DataValueField = "CodigoComponente";
            DdlComponente.DataTextField = "NombreComponente";
            DdlComponente.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);
        }

        /// <summary>
        /// Carga los tipos de producto.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarTiposdeProducto()
        {
            try
            {
                int identificadorAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? 0 : VinculacionSeleccionada.IdAtencion;
                var tipoProducto = new TipoProducto()
                {
                    IndHabilitado = true
                };
                var resultado = WebService.Integracion.ConsultarTipoProducto(tipoProducto, identificadorAtencion);
                if (resultado.Ejecuto)
                {
                    DdlTipoProducto.DataSource = resultado.Objeto;
                    DdlTipoProducto.DataValueField = Resources.GlobalWeb.General_IdTipoProducto;
                    DdlTipoProducto.DataTextField = Resources.GlobalWeb.General_Nombre;
                    DdlTipoProducto.DataBind();
                }
                else
                {
                    MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }

                CargaObjetos.AdicionarItemPorDefecto(DdlTipoProducto, false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para limpiar el formulario
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
            txtIdGrupoProducto.Text = string.Empty;
            txtGrupoProducto.Text = string.Empty;
            txtIdProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            DdlClaseCubrimiento.SelectedIndex = 0;
            DdlTipoProducto.DataSource = null;
            DdlTipoProducto.DataBind();
            DdlComponente.DataSource = null;
            DdlComponente.DataBind();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para guardar los Cubrimientos
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;
            if (Page.IsValid)
            {
                if (lblTitulo.Text == Resources.DefinirCubrimientos.DefinirCubrimientos_Crear)
                {
                    GuardarCubrimiento();
                }
                else
                {
                    ActualizarCubrimiento();
                }
            }
        }

        /// <summary>
        /// Grupo producto.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnIdGrupoProducto_Click(object sender, EventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;
            if (!string.IsNullOrEmpty(txtIdGrupoProducto.Text) && txtIdGrupoProducto.Text != "0")
            {
                var resultado = WebService.Integracion.ConsultarGrupoProducto(
                    new Paginacion<GrupoProducto>()
                {
                    Item = new GrupoProducto()
                    {
                        IdGrupo = Convert.ToInt32(txtIdGrupoProducto.Text),
                        IndHabilitado = true
                    }
                },
                Convert.ToInt32(txtIdAtencion.Text));
                if (resultado.Ejecuto == true && resultado.Objeto.Item.Count == 1)
                {
                    CargarDatosGrupo(resultado.Objeto.Item.FirstOrDefault());
                }
                else
                {
                    MostrarMensaje(string.Format(Resources.ControlesUsuario.TerceroCliente_NoExiste, txtIdGrupoProducto.Text), TipoMensaje.Error);
                    LimpiarControles(true);
                }
            }
        }

        /// <summary>
        /// Click producto.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnIdProducto_Click(object sender, EventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;
            if (!string.IsNullOrEmpty(txtIdProducto.Text) && txtIdProducto.Text != "0")
            {
                var resultado = WebService.Integracion.ConsultarTipoProductoCompuesto(
                    new Paginacion<TipoProductoCompuesto>()
                {
                    Item = new TipoProductoCompuesto()
                    {
                        GrupoProducto = new GrupoProducto()
                        {
                            IdGrupo = Convert.ToInt32(txtIdGrupoProducto.Text),
                        },
                        Producto = new Producto()
                        {
                            IdProducto = Convert.ToInt32(txtIdProducto.Text),
                        },
                        IndHabilitado = true
                    }
                },
                Convert.ToInt32(txtIdAtencion.Text));
                if (resultado.Ejecuto == true && resultado.Objeto.Item.Count == 1)
                {
                    CargarDatosProducto(resultado.Objeto.Item.FirstOrDefault());
                }
                else
                {
                    MostrarMensaje(string.Format(Resources.ControlesUsuario.TerceroCliente_NoExiste, txtIdProducto.Text), TipoMensaje.Error);
                    LimpiarControles(false);
                }
            }
        }

        /// <summary>
        /// Metodo para Validar las operaciones del Boton Buscar.
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
        protected void BuscarAtencion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            multi.SetActiveView(View1);
        }

        /// <summary>
        /// SeleccionarItemGrid de BuscarAtencion.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 10/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BuscarAtencion_SeleccionarItemGrid(EventoControles<Atencion> e)
        {
            ucBuscarAtencion.LimpiarCampos();
            multi.SetActiveView(View1);
            if (e.Resultado != null)
            {
                txtIdAtencion.Text = e.Resultado.IdAtencion.ToString();
            }
        }

        /// <summary>
        /// Evento SelectedIndexChanged de DdlTipoProducto_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarModal();
            string clasif = string.Empty;
            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                CargarComponenteConfiguracion();
            }

            LimpiarCamposGrupo();
            clasif = EstablecerClasificacionProducto(Convert.ToInt32(DdlTipoProducto.SelectedValue));
            CargarListaTiposComponentes(clasif);
        }

        /// <summary>
        /// Evento para regresar del popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
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
        /// Evento click de imgBuscarAtencion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarAtencion_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            multi.SetActiveView(vBuscarAtencion);
            ucBuscarAtencion.LimpiarCampos();
        }

        /// <summary>
        /// Se ejecuta para el llamado de la busqueda de productos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgConsultarGrupoProducto_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            int tipoProducto = 0;
            bool validaTipo = int.TryParse(DdlTipoProducto.SelectedValue, out tipoProducto);
            ucBuscarGrupoProductos.LimpiarCampos();
            ucBuscarGrupoProductos.IdTipoProducto = tipoProducto == -1 ? 0 : tipoProducto;
            ucBuscarGrupoProductos.IdAtencion = Convert.ToInt32(txtIdAtencion.Text);
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Se ejecuta para el llamado de la busqueda de productos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgConsultarProducto_Click(object sender, ImageClickEventArgs e)
        {
            short identificadorTipoProducto = 0;
            short identificadorGrupo = 0;

            if (!short.TryParse(txtIdGrupoProducto.Text, out identificadorGrupo))
            {
                identificadorGrupo = 0;
            }

            if (!short.TryParse(DdlTipoProducto.SelectedValue.ToString(), out identificadorTipoProducto) || DdlTipoProducto.SelectedValue.Equals(ValorDefecto.ValorDefectoCombo))
            {
                identificadorTipoProducto = 0;
            }

            RecargarModal();
            ucBuscarProductos.LimpiarCampos();
            ucBuscarProductos.IdTipoProducto = identificadorTipoProducto;
            ucBuscarProductos.IdGrupoProducto = identificadorGrupo;
            ucBuscarProductos.IdAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? 0 : Convert.ToInt32(VinculacionSeleccionada.IdAtencion);
            multi.ActiveViewIndex = 2;
        }

        /// <summary>
        /// Evento de Inicializacion del Control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarGrupoProductos.SeleccionarItemGrid += BuscarGrupoProductos_SeleccionarItemGrid;
            ucBuscarProductos.SeleccionarItemGrid += BuscarProductos_SeleccionarItemGrid;
            ucBuscarProductos.OperacionEjecutada += BuscarProductos_OperacionEjecutada;
            ucBuscarGrupoProductos.OperacionEjecutada += BuscarGrupoProductos_OperacionEjecutada;
            ucBuscarAtencion.SeleccionarItemGrid += BuscarAtencion_SeleccionarItemGrid;
            ucBuscarAtencion.OperacionEjecutada += BuscarAtencion_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Se ejecuta al cargar el control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
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
                if (InformacionTercero == null && VinculacionSeleccionada != null)
                {
                    CargarCampos();
                    CargarListasIniciales();
                    LimpiarCampos();
                }

                txtEntidad.Focus();
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para guardar el cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ActualizarCubrimiento()
        {
            RecargarModal();
            int identificadorContrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Contrato.Id : VinculacionSeleccionada.Contrato.Id;
            int planSeleccionado = 0;
            bool validaPlan = int.TryParse(ddlPlan.SelectedValue.ToString(), out planSeleccionado);
            int identificadorPlan = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? planSeleccionado : VinculacionSeleccionada.Plan.Id;
            int atencionSeleccionada = 0;
            bool validaAtencion = int.TryParse(txtIdAtencion.Text, out atencionSeleccionada);
            int identificadorAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? atencionSeleccionada : VinculacionSeleccionada.IdAtencion;
            Cubrimiento cubrimiento = new Cubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdCubrimiento = Convert.ToInt32(txtIdentificador.Text),
                IdProducto = string.IsNullOrEmpty(txtIdProducto.Text) ? 0 : Convert.ToInt32(txtIdProducto.Text),
                IdClaseCubrimiento = DdlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? int.Parse(Resources.GlobalWeb.General_ValorCero) : int.Parse(DdlClaseCubrimiento.SelectedValue),
                CodigoUsuario = Page.User.Identity.Name,
                IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                IdContrato = identificadorContrato,
                IdPlan = identificadorPlan,
                IdAtencion = identificadorAtencion,
                IdTipoProducto = DdlTipoProducto.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? 0 : Convert.ToInt32(DdlTipoProducto.SelectedValue),
                IdGrupoProducto = string.IsNullOrEmpty(txtIdGrupoProducto.Text) ? 0 : Convert.ToInt32(txtIdGrupoProducto.Text),
                Componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue
            };
            Resultado<int> resultado = WebService.Facturacion.ActualizarCubrimientos(cubrimiento);
            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearCubrimiento_MsjActualizacion, resultado.Objeto), TipoMensaje.Ok);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Operacion ejecutada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarGrupoProductos_OperacionEjecutada(object sender, EventArgs e)
        {
            RecargarModal();
            ucBuscarGrupoProductos.LimpiarCampos();
            ucBuscarGrupoProductos.IdAtencion = VinculacionSeleccionada.IdAtencion;
            multi.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Grupos productos.
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
        private void BuscarGrupoProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    RecargarModal();
                    ucBuscarGrupoProductos.LimpiarCampos();
                    ucBuscarGrupoProductos.IdAtencion = VinculacionSeleccionada.IdAtencion;
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Seleccionar Item Grid.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarGrupoProductos_SeleccionarItemGrid(SAHI.Comun.Utilidades.EventoControles<GrupoProducto> e)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;

            if (e.Resultado != null)
            {
                string clasif = string.Empty;

                DdlTipoProducto.SelectedValue = e.Resultado.IdTipoProducto.ToString();
                txtIdGrupoProducto.Text = e.Resultado.IdGrupo.ToString();
                txtGrupoProducto.Text = e.Resultado.Nombre.ToString();
                txtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
                txtProducto.Text = string.Empty;

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(e.Resultado.IdTipoProducto));
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Buscar productos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    RecargarModal();
                    ucBuscarProductos.LimpiarCampos();
                    ucBuscarProductos.IdAtencion = VinculacionSeleccionada.IdAtencion;
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Buscar Productos Seleccionar Item Grid.
        /// </summary>
        /// <param name="e">parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarProductos_SeleccionarItemGrid(SAHI.Comun.Utilidades.EventoControles<TipoProductoCompuesto> e)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;
            if (e.Resultado != null)
            {
                string clasif = string.Empty;

                DdlTipoProducto.SelectedValue = e.Resultado.IdTipoProducto.ToString();
                txtIdGrupoProducto.Text = e.Resultado.GrupoProducto.IdGrupo.ToString();
                txtGrupoProducto.Text = e.Resultado.GrupoProducto.Nombre;
                txtIdProducto.Text = e.Resultado.Producto.IdProducto.ToString();
                txtProducto.Text = e.Resultado.Producto.Nombre;
                CargarComponenteConfiguracion();

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(e.Resultado.IdTipoProducto));
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo para realizar la carga de los campos.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarCampos()
        {
            lblMensaje.Visible = false;
            var resultado = WebService.Facturacion.ConsultarInformacionBasicaTercero();

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                InformacionTercero = resultado.Objeto;
            }
            else
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = resultado.Mensaje;
            }

            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                ddlPlan.Visible = true;
                txtIdPlan.Visible = false;
                txtPlan.Visible = false;
            }
            else
            {
                ddlPlan.Visible = false;
                txtIdPlan.Visible = true;
                txtPlan.Visible = false;
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
                IndHabilitado = 1
            };
            DdlClaseCubrimiento.DataSource = WebService.Facturacion.ConsultarClasesCubrimiento(claseCubrimiento).Objeto;
            DdlClaseCubrimiento.DataValueField = "IdClaseCubrimiento";
            DdlClaseCubrimiento.DataTextField = "Nombre";
            DdlClaseCubrimiento.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlClaseCubrimiento, false);
        }

        /// <summary>
        /// Cargar componente
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 05/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarComponenteConfiguracion()
        {
            int identificadorTipoProducto = -1;
            string inicial = string.Empty;
            bool valida = int.TryParse(DdlTipoProducto.SelectedValue, out identificadorTipoProducto);
            if (identificadorTipoProducto == 5 || identificadorTipoProducto == 6)
            {
                inicial = identificadorTipoProducto == 5 ? Resources.Configuracion.TipoComponente_InicialQuirurgico : Resources.Configuracion.TipoComponente_InicialNoQuirurgico;
            }

            var resultado = WebService.Configuracion.ConsultarComponentePorProdcuto(inicial);
            if (resultado.Ejecuto && resultado.Objeto != null && resultado.Objeto.Count > 0)
            {
                DdlComponente.Items.Clear();
                DdlComponente.DataSource = resultado.Objeto;
                DdlComponente.DataValueField = CODIGOCOMPONENTE;
                DdlComponente.DataTextField = NOMBRECOMPONENTE;
                DdlComponente.DataBind();
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Cargar Datos Grupo.
        /// </summary>
        /// <param name="grupoProducto">The grupo producto.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarDatosGrupo(GrupoProducto grupoProducto)
        {
            txtIdGrupoProducto.Text = grupoProducto.IdGrupo.ToString();
            txtGrupoProducto.Text = grupoProducto.Nombre;
        }

        /// <summary>
        /// Cargar Datos Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarDatosProducto(TipoProductoCompuesto producto)
        {
            txtIdProducto.Text = producto.Producto.IdProducto.ToString();
            txtProducto.Text = producto.Producto.Nombre;
        }

        /// <summary>
        /// Cargue inicial de los combos.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
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
        /// Carga los componentes en el combo.
        /// </summary>
        /// <param name="clasificacion">The clasificacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 03/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarListaTiposComponentes(string clasificacion)
        {
            DdlComponente.Items.Clear();
            var componentes = new Paginacion<TipoComponente>()
            {
                LongitudPagina = 100,
                PaginaActual = 0,
                Item = new TipoComponente()
                {
                    IndHabilitado = 1
                }
            };

            var resultado = WebService.Configuracion.ConsultarTipoComponente(componentes);

            if (resultado.Ejecuto)
            {
                List<TipoComponente> tiposComponentes = resultado.Objeto.Item.Where(c => c.Clasificacion == clasificacion || c.CodigoComponente == Resources.GlobalWeb.General_ValorNA).ToList();
                DdlComponente.DataSource = tiposComponentes;
                DdlComponente.DataValueField = "CodigoComponente";
                DdlComponente.DataTextField = "NombreComponente";
                DdlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);
                DdlComponente.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Retorna la clasificación del componente segun el tipo de producto.
        /// </summary>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <returns>Retorna Clasificación.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static string EstablecerClasificacionProducto(int identificadorTipoProducto)
        {
            string clasificacion = string.Empty;
            switch (identificadorTipoProducto)
            {
                case 5:
                    clasificacion = QX;
                    break;

                case 6:
                    clasificacion = NOQX;
                    break;

                default:
                    clasificacion = string.Empty;
                    break;
            }

            return clasificacion;
        }

        /// <summary>
        /// Metodo para guardar el cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarCubrimiento()
        {
            RecargarModal();
            int identificadorContrato = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? Cubrimiento.Contrato.Id : VinculacionSeleccionada.Contrato.Id;
            int planSeleccionado = 0;
            bool validaPlan = int.TryParse(ddlPlan.SelectedValue.ToString(), out planSeleccionado);
            int identificadorPlan = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? planSeleccionado : VinculacionSeleccionada.Plan.Id;
            int atencionSeleccionada = 0;
            bool validaAtencion = int.TryParse(txtIdAtencion.Text, out atencionSeleccionada);
            int identificadorAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? atencionSeleccionada : VinculacionSeleccionada.IdAtencion;
            Cubrimiento cubrimiento = new Cubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdProducto = string.IsNullOrEmpty(txtIdProducto.Text) ? 0 : Convert.ToInt32(txtIdProducto.Text),
                IdClaseCubrimiento = DdlClaseCubrimiento.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlClaseCubrimiento.SelectedValue.ToString()) ? int.Parse(Resources.GlobalWeb.General_ValorCero) : int.Parse(DdlClaseCubrimiento.SelectedValue),
                CodigoUsuario = Page.User.Identity.Name,
                IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                IdContrato = identificadorContrato,
                IdPlan = identificadorPlan,
                IdAtencion = identificadorAtencion,
                IdTipoProducto = DdlTipoProducto.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? 0 : Convert.ToInt32(DdlTipoProducto.SelectedValue),
                IdGrupoProducto = string.IsNullOrEmpty(txtIdGrupoProducto.Text) ? 0 : Convert.ToInt32(txtIdGrupoProducto.Text),
                Componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor || string.IsNullOrEmpty(DdlComponente.SelectedValue.ToString()) ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue
            };
            Resultado<int> resultado = WebService.Facturacion.GuardarCubrimientos(cubrimiento);
            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearCubrimiento_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo de Limpiar Controles
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 18/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void LimpiarCamposGrupo()
        {
            txtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
            txtIdGrupoProducto.Text = Resources.GlobalWeb.General_ValorCero;
            txtProducto.Text = string.Empty;
            txtGrupoProducto.Text = string.Empty;
        }

        /// <summary>
        /// Limpiar Controles.
        /// </summary>
        /// <param name="grupo">If set to <c>true</c> [grupo].</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 23/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void LimpiarControles(bool grupo)
        {
            if (grupo)
            {
                txtGrupoProducto.Text = string.Empty;
                txtIdGrupoProducto.Text = string.Empty;
            }
            else
            {
                txtProducto.Text = string.Empty;
                txtIdProducto.Text = string.Empty;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}