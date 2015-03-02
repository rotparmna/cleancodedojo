// --------------------------------
// <copyright file="UC_CrearExclusion.ascx.cs" company="InterGrupo S.A.">
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
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearExclusion.
    /// </summary>
    public partial class UC_CrearExclusion : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The INFORMACIONTERCERO.
        /// </summary>
        private const string INFORMACIONTERCERO = "InformacionTercero";

        /// <summary>
        /// The NOQX.
        /// </summary>
        private const string NOQX = "N";

        /// <summary>
        /// The QX.
        /// </summary>
        private const string QX = "Q";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece informacion tercro.
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
        /// Carga la información del cubrimiento seleccionado en la principal para su modificación.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 15/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarInformacionExclusion(ExclusionContrato exclusion)
        {
            string clasif = EstablecerClasificacionProducto(Convert.ToInt16(exclusion.IdTipoProducto));
            CargarListaTiposComponentes(clasif);
            txtIdentificador.Text = exclusion.Id.ToString();
            txtEntidad.Text = VinculacionSeleccionada.Tercero.Nombre;
            txtIdContrato.Text = VinculacionSeleccionada.Contrato.Id.ToString();
            txtContrato.Text = VinculacionSeleccionada.Contrato.Nombre;
            txtIdPlan.Text = VinculacionSeleccionada.Plan.Id.ToString();
            txtPlan.Text = VinculacionSeleccionada.Plan.Nombre;
            txtIdAtencion.Text = VinculacionSeleccionada.IdAtencion.ToString();
            txtIdGrupoProducto.Text = exclusion.GrupoProducto.IdGrupo.ToString();
            txtGrupoProducto.Text = exclusion.GrupoProducto.Nombre;
            txtIdProducto.Text = exclusion.Producto.IdProducto.ToString();
            txtProducto.Text = exclusion.Producto.Nombre.ToString();
            txtIdVenta.Text = exclusion.IdVenta.ToString();
            txtIdTarifa.Text = exclusion.IdManual.ToString();
            txtNombreTarifa.Text = exclusion.NombreManual.ToString();
            txtVigencia.Text = exclusion.VigenciaTarifa.ToShortDateString();

            if (lblTitulo.Text == Resources.DefinirExclusiones.DefinirExclusiones_Actualizar)
            {
                // CargarTiposComponentes();
                chkActivo.Checked = exclusion.IndicadorContratoActivo == 1 ? true : false;
                ddlComponente.SelectedValue = exclusion.Componente.ToString();
                txtNumeroVenta.Text = exclusion.NumeroVenta.ToString();
                ddlTipoProducto.SelectedValue = exclusion.TipoProducto.IdTipoProducto.ToString();
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarTiposComponentes()
        {
            ddlComponente.DataSource = WebService.Facturacion.ConsultarComponentes(VinculacionSeleccionada.IdAtencion, 0).Objeto;
            ddlComponente.DataValueField = "CodigoComponente";
            ddlComponente.DataTextField = "NombreComponente";
            ddlComponente.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(ddlComponente, false);
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarTiposdeProducto()
        {
            var tipoProducto = new TipoProducto()
            {
                IndHabilitado = true
            };
            var resultado = WebService.Integracion.ConsultarTipoProducto(tipoProducto, VinculacionSeleccionada.IdAtencion);
            if (resultado.Ejecuto)
            {
                ddlTipoProducto.DataSource = resultado.Objeto;
                ddlTipoProducto.DataValueField = "IdTipoProducto";
                ddlTipoProducto.DataTextField = "Nombre";
                ddlTipoProducto.DataBind();
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }

            CargaObjetos.AdicionarItemPorDefecto(ddlTipoProducto, false);
        }

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
            txtIdVenta.Text = string.Empty;
            txtNumeroVenta.Text = string.Empty;
            txtIdTarifa.Text = string.Empty;
            txtNombreTarifa.Text = string.Empty;
            txtVigencia.Text = string.Empty;
            ddlTipoProducto.DataSource = null;
            ddlTipoProducto.DataBind();
            ddlComponente.DataSource = null;
            ddlComponente.DataBind();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para guardar exclusiones.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            lblMensaje.Visible = false;

            var grupo = ddlTipoProducto.ValidationGroup;
            Page.Validate(grupo);

            if (Page.IsValid)
            {
                if (lblTitulo.Text == Resources.DefinirExclusiones.DefinirExclusiones_Crear)
                {
                    GuardarExclusion();
                }
                else
                {
                    ActualizarExclusion();
                }
            }
        }

        /// <summary>
        /// Metodo para Consulta ID del Grupo de Producto.
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
        /// Metodo para Consulta Id del producto.
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
                                IdGrupo = Convert.ToInt32(txtIdGrupoProducto.Text)
                            },

                            Producto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(txtIdProducto.Text)
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
        /// Metodo Para Buscar Tarifa.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnIdTarifa_Click(object sender, EventArgs e)
        {
            RecargarModal();
        }

        /// <summary>
        /// Click venta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnIdVenta_Click(object sender, EventArgs e)
        {
            RecargarModal();
        }

        /// <summary>
        /// Metoo de Tipo De Producto.
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
        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarModal();
            txtIdGrupoProducto.Text = Resources.GlobalWeb.General_ValorCero;
            txtGrupoProducto.Text = string.Empty;
            txtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
            txtProducto.Text = string.Empty;

            string clasif = string.Empty;
            ddlComponente.Items.Clear();
            ddlComponente.DataSource = null;
            ddlComponente.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(ddlComponente, false);

            clasif = EstablecerClasificacionProducto(Convert.ToInt32(ddlTipoProducto.SelectedValue));
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
            short tipoProducto = 0;
            bool valida = short.TryParse(ddlTipoProducto.SelectedValue, out tipoProducto);
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

            if (string.IsNullOrEmpty(txtIdGrupoProducto.Text) || !short.TryParse(txtIdGrupoProducto.Text, out identificadorGrupo))
            {
                identificadorGrupo = 0;
            }

            if (!short.TryParse(ddlTipoProducto.SelectedValue.ToString(), out identificadorTipoProducto) || ddlTipoProducto.SelectedValue.Equals(ValorDefecto.ValorDefectoCombo))
            {
                identificadorTipoProducto = 0;
            }

            RecargarModal();
            ucBuscarProductos.LimpiarCampos();
            ucBuscarProductos.IdGrupoProducto = identificadorGrupo;
            ucBuscarProductos.IdTipoProducto = identificadorTipoProducto;
            ucBuscarProductos.IdAtencion = string.IsNullOrEmpty(txtIdAtencion.Text) ? 0 : Convert.ToInt32(txtIdAtencion.Text);
            multi.ActiveViewIndex = 2;
        }

        /// <summary>
        /// Consulta Tarifa.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgConsultarTarifa_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarTarifas.LimpiarCampos();
            multi.ActiveViewIndex = 4;
        }

        /// <summary>
        /// Consultar Venta.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgConsultarVenta_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarVentas.LimpiarCampos();
            ucBuscarVentas.EstablecerCampoAtencionHabilitado(false);
            multi.ActiveViewIndex = 3;
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
            ucBuscarGrupoProductos.SeleccionarItemGrid += BuscarGrupoProductos_SeleccionarItemGrid;
            ucBuscarProductos.SeleccionarItemGrid += BuscarProductos_SeleccionarItemGrid;
            ucBuscarProductos.OperacionEjecutada += BuscarProductos_OperacionEjecutada;
            ucBuscarGrupoProductos.OperacionEjecutada += BuscarGrupoProductos_OperacionEjecutada;
            ucBuscarVentas.OperacionEjecutada += BuscarVentas_OperacionEjecutada;
            ucBuscarVentas.SeleccionarItemGrid += BuscarVentas_SeleccionarItemGrid;
            ucBuscarTarifas.SeleccionarItemGrid += BuscarTarifas_SeleccionarItemGrid;
            ucBuscarTarifas.OperacionEjecutada += BuscarTarifas_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Se ejecuta al cargar el control.
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
                if (InformacionTercero == null)
                {
                    CargarCampos();
                    CargarListasIniciales();
                    LimpiarCampos();
                }

                txtEntidad.Focus();
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados Estaticos 

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
        private static void CargarClasesCubrimientos()
        {
            var claseCubrimiento = new ClaseCubrimiento()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IndHabilitado = 1
            };
        }

        #endregion Metodos Privados Estaticos 
        #region Metodos Privados 

        /// <summary>
        /// Método para actualizar la exclusión.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ActualizarExclusion()
        {
            RecargarModal();
            Exclusion exclusion = new Exclusion()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                Id = Convert.ToInt32(txtIdentificador.Text),
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdTipoAtencion = 0,
                IdServicio = 0,
                IdManual = string.IsNullOrEmpty(txtIdTarifa.Text) ? Convert.ToInt32(Resources.GlobalWeb.General_ValorCero) : Convert.ToInt32(txtIdTarifa.Text),
                VigenciaTarifa = Convert.ToDateTime(txtVigencia.Text),
                IdTipoProducto = Convert.ToInt16(ddlTipoProducto.SelectedValue),
                IdGrupoProducto = Convert.ToInt16(txtIdGrupoProducto.Text),
                IdProducto = Convert.ToInt32(txtIdProducto.Text),
                Componente = ddlComponente.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? Resources.GlobalWeb.General_ValorNA : ddlComponente.SelectedValue,
                IndicadorContratoActivo = chkActivo.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0),
                IdContrato = VinculacionSeleccionada.Contrato.Id,
                IdPlan = VinculacionSeleccionada.Plan.Id,
                IdAtencion = VinculacionSeleccionada.IdAtencion,
                IdVenta = Convert.ToInt32(txtIdVenta.Text),
                NumeroVenta = Convert.ToInt32(txtNumeroVenta.Text)
            };

            Resultado<int> resultado = WebService.Facturacion.ActualizarExclusionContrato(exclusion);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.ExclusionContrato_MsjActualizacion, resultado.Objeto), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeERROR;
            }
        }

        /// <summary>
        /// Metodo de Buscar Grupos de Producto.
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
        /// Metodo de Operacion ejecutada grupos de producto.
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
        /// Metodo de seleccionar Grupo de PRoducto.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
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

                ddlTipoProducto.SelectedValue = e.Resultado.IdTipoProducto.ToString();
                txtIdGrupoProducto.Text = e.Resultado.IdGrupo.ToString();
                txtGrupoProducto.Text = e.Resultado.Nombre.ToString();
                txtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
                txtProducto.Text = string.Empty;

                clasif = EstablecerClasificacionProducto(e.Resultado.IdTipoProducto);
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo de Operacion Ejecutada Producto.
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
        /// Metodod de Seleccionar producto.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
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

                ddlTipoProducto.SelectedValue = e.Resultado.IdTipoProducto.ToString();
                txtIdGrupoProducto.Text = e.Resultado.GrupoProducto.IdGrupo.ToString();
                txtGrupoProducto.Text = e.Resultado.GrupoProducto.Nombre;
                txtIdProducto.Text = e.Resultado.Producto.IdProducto.ToString();
                txtProducto.Text = e.Resultado.Producto.Nombre;

                clasif = EstablecerClasificacionProducto(e.Resultado.IdTipoProducto);
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo para controlar evento de operacion ejecutada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarTarifas_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    RecargarModal();
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Buscar Tarifas.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarTarifas_SeleccionarItemGrid(EventoControles<ManualesBasicos> e)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;

            if (e.Resultado != null)
            {
                txtIdTarifa.Text = e.Resultado.CodigoTarifa.ToString();
                txtNombreTarifa.Text = e.Resultado.NombreTarifa.ToString();
                txtVigencia.Text = e.Resultado.VigenciaTarifa.ToShortDateString();
            }
        }

        /// <summary>
        /// Metodo de Buscar Ventas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarVentas_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    RecargarModal();
                    ucBuscarVentas.LimpiarCampos();
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Buscar Ventas.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarVentas_SeleccionarItemGrid(EventoControles<Venta> e)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;

            if (e.Resultado != null)
            {
                txtIdVenta.Text = e.Resultado.IdTransaccion.ToString();
                txtNumeroVenta.Text = e.Resultado.NumeroVenta.ToString();
            }
        }

        /// <summary>
        /// Metodo para realizar la carga de los campos.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
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
        }

        /// <summary>
        /// Metodo de Carga datos del Grupo.
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
        /// Metodo de Cargar Datos del Producto.
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
            ddlComponente.Items.Clear();
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
                ddlComponente.DataSource = tiposComponentes;
                ddlComponente.DataValueField = "CodigoComponente";
                ddlComponente.DataTextField = "NombreComponente";
                ddlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(ddlComponente, false);
                ddlComponente.SelectedIndex = 0;
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
        private string EstablecerClasificacionProducto(int identificadorTipoProducto)
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
        /// Método para guardar la exclusión.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarExclusion()
        {
            RecargarModal();
            Exclusion exclusion = new Exclusion()
            {
                CodigoEntidad = Settings.Default.General_CodigoEntidad,
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdTipoAtencion = 0,
                IdServicio = 0,
                IdManual = string.IsNullOrEmpty(txtIdTarifa.Text) ? Convert.ToInt32(Resources.GlobalWeb.General_ValorCero) : Convert.ToInt32(txtIdTarifa.Text),
                VigenciaTarifa = Convert.ToDateTime(txtVigencia.Text),
                IdTipoProducto = Convert.ToInt16(ddlTipoProducto.SelectedValue),
                IdGrupoProducto = Convert.ToInt16(txtIdGrupoProducto.Text),
                IdProducto = Convert.ToInt32(txtIdProducto.Text),
                Componente = ddlComponente.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? Resources.GlobalWeb.General_ValorNA : ddlComponente.SelectedValue,
                IndicadorContratoActivo = chkActivo.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0),
                IdContrato = VinculacionSeleccionada.Contrato.Id,
                IdPlan = VinculacionSeleccionada.Plan.Id,
                IdAtencion = VinculacionSeleccionada.IdAtencion,
                IdVenta = string.IsNullOrEmpty(txtIdVenta.Text) ? 0 : Convert.ToInt32(txtIdVenta.Text),
                NumeroVenta = string.IsNullOrEmpty(txtNumeroVenta.Text) ? 0 : Convert.ToInt32(txtNumeroVenta.Text)
            };

            Resultado<int> resultado = WebService.Facturacion.GuardarExclusionContrato(exclusion);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.ExclusionContrato_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeERROR;
            }
        }

        /// <summary>
        /// Metodo de Limpiar Controles.
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