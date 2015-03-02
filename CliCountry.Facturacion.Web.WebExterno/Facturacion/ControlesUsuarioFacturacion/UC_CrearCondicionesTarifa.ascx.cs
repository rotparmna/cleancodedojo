// --------------------------------
// <copyright file="UC_CrearCondicionesTarifa.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.CondicionTarifas.
    /// </summary>
    public partial class UC_CrearCondicionesTarifa : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The CONDICIONTARIFA
        /// </summary>
        private const string CONDICIONTARIFA = "CondicionTarifa";

        /// <summary>
        /// The NOQX
        /// </summary>
        private const string NOQX = "N";

        /// <summary>
        /// The QX
        /// </summary>
        private const string QX = "Q";

        /// <summary>
        /// The TIPOOPERACION
        /// </summary>
        private const string TIPOOPERACION = "TipoOperacion";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece tipo operacion
        /// </summary>
        public CondicionTarifa CondicionTarifa
        {
            get
            {
                return ViewState[CONDICIONTARIFA] != null ? ViewState[CONDICIONTARIFA] as CondicionTarifa : new CondicionTarifa();
            }

            set
            {
                ViewState[CONDICIONTARIFA] = value;
            }
        }

        #endregion Propiedades Publicas
        #region Propiedades Privadas

        /// <summary>
        /// Obtiene o establece tipo operacion
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
            CargarComponente();
            CargarComboTipoRelacion();
            CargarComboTipoProducto();

            switch (TipoOperacion)
            {
                case Global.TipoOperacion.CREACION:
                    CargarControlesEvento(false);
                    break;

                case Global.TipoOperacion.MODIFICACION:
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
            mltvCrearCondicionTarifas.SetActiveView(vCrearTarifa);
            ChkActivo.Checked = true;
            LblMensaje.Text = string.Empty;
            LblMensaje.Visible = false;
            txtEntidad.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtAtencion.Text = string.Empty;
            TxtTarifa.Text = string.Empty;
            TxtNombreTarifa.Text = string.Empty;
            TxtVigenciaTarifa.Text = string.Empty;
            TxtTarifaAlt.Text = string.Empty;
            TxtNombreTarifaAlt.Text = string.Empty;
            TxtVigenciaTarifaAlt.Text = string.Empty;
            TxtVigenciaCondicionTarifa.Text = string.Empty;
            TxtIdGrupoProducto.Text = string.Empty;
            TxtIdProducto.Text = string.Empty;
            TxtNombreGrupo.Text = string.Empty;
            TxtNombreProducto.Text = string.Empty;
            txtPlan.Text = string.Empty;
            TxtValor.Text = string.Empty;
            TxtObservaciones.Text = string.Empty;
            DdlTipoProducto.SelectedIndex = -1;
            DdlComponente.SelectedIndex = -1;
            DdlTipoRelacion.SelectedIndex = -1;
            CondicionTarifa = null;
            TipoOperacion = Global.TipoOperacion.SALIR;
        }

        /// <summary>
        /// Limpiar los campos de tarifas.
        /// </summary>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 23/01/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarTarifas()
        {
            TxtVigenciaTarifa.Text = string.Empty;
            TxtVigenciaTarifaAlt.Text = string.Empty;
            TxtTarifa.Text = string.Empty;
            TxtTarifaAlt.Text = string.Empty;
            TxtNombreTarifa.Text = string.Empty;
            TxtNombreTarifaAlt.Text = string.Empty;
        }

        #endregion Metodos Publicos
        #region Metodos Protegidos

        /// <summary>
        /// Metodo para realizar la Busqueda del Grupo de Producto.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnGrupoProducto_Click(object sender, EventArgs e)
        {
            RecargarModal();

            if (!string.IsNullOrEmpty(TxtIdGrupoProducto.Text))
            {
                var resultado = WebService.Integracion.ConsultarGrupoProducto(
                    new Paginacion<GrupoProducto>()
                    {
                        Item = new GrupoProducto()
                        {
                            IdGrupo = Convert.ToInt32(TxtIdGrupoProducto.Text),
                            IndHabilitado = true,
                        }
                    },
                CondicionTarifa.IdAtencion);

                if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                {
                    CargarCamposGrupo(resultado.Objeto.Item.FirstOrDefault());
                }
                else if (!string.IsNullOrEmpty(resultado.Mensaje))
                {
                    MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        /// Metodo para buscar producto.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BtnProducto_Click(object sender, EventArgs e)
        {
            RecargarModal();

            if (!string.IsNullOrEmpty(TxtIdProducto.Text))
            {
                var resultado = WebService.Integracion.ConsultarTipoProductoCompuesto(
                    new Paginacion<TipoProductoCompuesto>()
                    {
                        Item = new TipoProductoCompuesto()
                        {
                            Producto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(TxtIdProducto.Text),
                            },

                            GrupoProducto = new GrupoProducto(),
                            IndHabilitado = true,
                        }
                    },
                    CondicionTarifa.IdAtencion);

                if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                {
                    CargarCamposProducto(resultado.Objeto.Item.FirstOrDefault());
                }
                else if (!string.IsNullOrEmpty(resultado.Mensaje))
                {
                    MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DdlTipoProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarModal();
            LimpiarCamposProductoGrupo();

            string clasif = string.Empty;
            DdlComponente.Items.Clear();
            DdlComponente.DataSource = null;
            DdlComponente.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);

            clasif = EstablecerClasificacionProducto(Convert.ToInt32(DdlTipoProducto.SelectedValue));
            CargarListaTiposComponentes(clasif);
        }

        /// <summary>
        /// Metodo de Seleccion de Indice del Tipo Relacion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
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
        /// Metodo para realizar la busqueda de Grupo de PRoducto.
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
        protected void ImgBuscarGrupoProducto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarGrupoProductos.LimpiarCampos();
            ucBuscarGrupoProductos.IdAtencion = !string.IsNullOrEmpty(txtAtencion.Text) ? Convert.ToInt32(txtAtencion.Text) : 0;
            mltvCrearCondicionTarifas.SetActiveView(vBuscarGrupoProductos);
        }

        /// <summary>
        /// Metodo para realizar la Busqueda del Tipo de Producto.
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
        protected void ImgBuscarProducto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ucBuscarProductos.LimpiarCampos();
            ucBuscarProductos.IdAtencion = !string.IsNullOrEmpty(txtAtencion.Text) ? Convert.ToInt32(txtAtencion.Text) : 0;
            mltvCrearCondicionTarifas.SetActiveView(vBuscarProductos);
        }

        /// <summary>
        /// Metodo para realizar la Busqueda de la Tarifa.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarTarifa_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var boton = sender as ImageButton;
            RecargarModal();
            mltvCrearCondicionTarifas.SetActiveView(vBuscarTarifa);
            ucBuscarTarifas.LimpiarCampos();

            if (boton.ID == ImgBuscarTarifa.ID)
            {
                ucBuscarTarifas.TipoBusquedaEjecutada = UC_BuscarTarifas.TipoBusqueda.Principal;
            }
            else
            {
                ucBuscarTarifas.TipoBusquedaEjecutada = UC_BuscarTarifas.TipoBusqueda.Alterno;
            }
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
        protected void ImgGuardarCondicionTarifas_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            if (!this.TxtValor.Text.Equals("0.00") && !this.TxtValor.Text.Equals("0"))
            {
                ComplementarDatosCondicionTarifa();

                switch (Convert.ToInt32(DdlTipoRelacion.SelectedValue))
                {
                    case (int)TipoTarifa.NoCubrimiento:
                        TxtValor.Text = Resources.GlobalWeb.General_ValorCero;
                        rfvtxtValorTarCre.Enabled = false;
                        break;

                    default:
                        rfvtxtValorTarCre.Enabled = true;
                        break;
                }

                if (CondicionTarifa.Id > 0)
                {
                    ActualizarCondicionTarifa();
                }
                else
                {
                    InsertarCondicionTarifa();
                }

                ResultadoEjecucion(Global.TipoOperacion.SALIR);
            }
            else
            {
                this.MostrarMensaje("El valor debe ser mayor a cero.", TipoMensaje.Error);
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
        /// Metodo de Inicializacion de la pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarGrupoProductos.SeleccionarItemGrid += BuscarGrupoProductos_SeleccionarItemGrid;
            ucBuscarGrupoProductos.OperacionEjecutada += BuscarGrupoProductos_OperacionEjecutada;
            ucBuscarProductos.SeleccionarItemGrid += BuscarProductos_SeleccionarItemGrid;
            ucBuscarProductos.OperacionEjecutada += BuscarProductos_OperacionEjecutada;
            ucBuscarTarifas.SeleccionarItemGrid += BuscarTarifas_SeleccionarItemGrid;
            ucBuscarTarifas.OperacionEjecutada += BuscarTarifas_OperacionEjecutada;
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
            if (DdlComponente.SelectedIndex > -1)
            {
                switch (Convert.ToInt32(DdlTipoRelacion.SelectedValue))
                {
                    case (int)TipoTarifa.AjusteTarifas:
                        ValidarCamposRequeridos(true);
                        break;

                    default:
                        LimpiarTarifas();
                        ValidarCamposRequeridos(false);
                        break;
                }
            }
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
        private void ActualizarCondicionTarifa()
        {
            var resultado = WebService.Facturacion.ActualizarCondicionTarifa(CondicionTarifa);

            if (resultado.Ejecuto)
            {
                if (resultado.Objeto)
                {
                    MostrarMensaje(Resources.DefinirCondicionesTarifa.DefinirCondicionesTarifa_MsjActualizacion, TipoMensaje.Ok);
                }
                else
                {
                    MostrarMensaje(Resources.DefinirCondicionesTarifa.DefinirCondicionesTarifa_MsjActualizacionError, TipoMensaje.Error);
                }
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo de Operacion ejecutada del grupo de producto.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarGrupoProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    mltvCrearCondicionTarifas.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccion del Grupo de Producto.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarGrupoProductos_SeleccionarItemGrid(EventoControles<GrupoProducto> e)
        {
            RecargarModal();
            mltvCrearCondicionTarifas.SetActiveView(vCrearTarifa);

            if (e.Resultado != null)
            {
                string clasif = string.Empty;
                CargarCamposGrupo(e.Resultado);

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(e.Resultado.IdTipoProducto));
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo Indica la Operacion EjEcutada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    mltvCrearCondicionTarifas.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccion del Producto.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarProductos_SeleccionarItemGrid(EventoControles<TipoProductoCompuesto> e)
        {
            RecargarModal();
            mltvCrearCondicionTarifas.SetActiveView(vCrearTarifa);

            if (e.Resultado != null)
            {
                string clasif = string.Empty;
                CargarCamposProducto(e.Resultado);

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(e.Resultado.IdTipoProducto));
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo para controlar la operacion ejecutada de Busqueda de Tarifas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarTarifas_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();

            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    mltvCrearCondicionTarifas.SetActiveView(vCrearTarifa);
                    break;
            }
        }

        /// <summary>
        /// Metodo para controlar seleccion de la tarifa.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarTarifas_SeleccionarItemGrid(EventoControles<ManualesBasicos> e)
        {
            RecargarModal();
            mltvCrearCondicionTarifas.SetActiveView(vCrearTarifa);

            if (e.Resultado != null)
            {
                if (ucBuscarTarifas.TipoBusquedaEjecutada == UC_BuscarTarifas.TipoBusqueda.Principal)
                {
                    TxtTarifa.Text = e.Resultado.CodigoTarifa.ToString();
                    TxtNombreTarifa.Text = e.Resultado.NombreTarifa;
                    TxtVigenciaTarifa.Text = e.Resultado.VigenciaTarifa.ToString();
                }
                else if (ucBuscarTarifas.TipoBusquedaEjecutada == UC_BuscarTarifas.TipoBusqueda.Alterno)
                {
                    TxtTarifaAlt.Text = e.Resultado.CodigoTarifa.ToString();
                    TxtNombreTarifaAlt.Text = e.Resultado.NombreTarifa;
                    TxtVigenciaTarifaAlt.Text = e.Resultado.VigenciaTarifa.ToString();
                }
            }
        }

        /// <summary>
        /// Metodo para realizar carga de campos dependientes.
        /// </summary>
        /// <param name="grupo">The grupo.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarCamposGrupo(GrupoProducto grupo)
        {
            TxtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreProducto.Text = string.Empty;
            TxtIdGrupoProducto.Text = grupo.IdGrupo.ToString();
            TxtNombreGrupo.Text = grupo.Nombre;
            DdlTipoProducto.SelectedValue = grupo.IdTipoProducto.ToString();
        }

        /// <summary>
        /// Metodo para realizar carga de campos dependientes.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarCamposProducto(TipoProductoCompuesto producto)
        {
            TxtIdProducto.Text = producto.Producto.IdProducto.ToString();
            TxtNombreProducto.Text = producto.Producto.Nombre;
            TxtIdGrupoProducto.Text = producto.GrupoProducto.IdGrupo.ToString();
            TxtNombreGrupo.Text = producto.GrupoProducto.Nombre;
            DdlTipoProducto.SelectedValue = producto.IdTipoProducto.ToString();
        }

        /// <summary>
        /// Metodo para realizar la Carga del Combo de Tipo Producto.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarComboTipoProducto()
        {
            var resultado = WebService.Integracion.ConsultarTipoProducto(
            new TipoProducto()
            {
                IndHabilitado = true
            },
            VinculacionSeleccionada.IdAtencion);
            if (resultado.Ejecuto)
            {
                DdlTipoProducto.DataTextField = "Nombre";
                DdlTipoProducto.DataValueField = "IdTipoProducto";
                DdlTipoProducto.DataSource = resultado.Objeto;
                DdlTipoProducto.DataBind();
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }

            CargaObjetos.AdicionarItemPorDefecto(DdlTipoProducto, false);
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
            int identificadorMaestra = Convert.ToInt32(Resources.CargueCombos.CombosFacturacionCondicionesTarifa);
            int identificadorPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesTarifa);
            var resultado = WebService.Facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina);

            if (resultado.Ejecuto)
            {
                DdlTipoRelacion.DataTextField = "NombreMaestroDetalle";
                DdlTipoRelacion.DataValueField = "Valor";
                DdlTipoRelacion.DataSource = resultado.Objeto;
                DdlTipoRelacion.DataBind();
                DdlTipoRelacion.SelectedIndex = 0;
                ValidarFormatoCampoValor();
            }
            else
            {
                DdlTipoRelacion.DataSource = null;
                DdlTipoRelacion.DataBind();
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
                DdlComponente.DataTextField = "NombreComponente";
                DdlComponente.DataValueField = "CodigoComponente";
                DdlComponente.DataSource = resultado.Objeto;
                DdlComponente.DataBind();
                DdlComponente.SelectedIndex = 0;
            }
            else
            {
                DdlComponente.DataSource = null;
                DdlComponente.DataBind();
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
            string clasif = EstablecerClasificacionProducto(Convert.ToInt16(CondicionTarifa.IdTipoProducto));
            CargarListaTiposComponentes(clasif);
            txtEntidad.Text = CondicionTarifa.NombreTercero;
            txtContrato.Text = CondicionTarifa.NombreContrato;
            txtPlan.Text = CondicionTarifa.NombrePlan;
            txtAtencion.Text = CondicionTarifa.IdAtencion.ToString();
            TxtId.Text = Resources.GlobalWeb.General_ValorCero;

            if (modificacion)
            {
                ListItem selectedListItem = DdlTipoProducto.Items.FindByValue(CondicionTarifa.IdTipoProducto.ToString());

                if (selectedListItem != null)
                {
                    DdlTipoProducto.SelectedValue = CondicionTarifa.IdTipoProducto > 0 ? CondicionTarifa.IdTipoProducto.ToString() : "-1";
                }

                foreach (ListItem item in this.DdlComponente.Items)
                {
                    if (item.Value == CondicionTarifa.Componente)
                    {
                        DdlComponente.SelectedValue = CondicionTarifa.Componente;
                        break;
                    }
                    else
                    {
                        DdlComponente.SelectedIndex = -1;
                    }
                }

                DdlTipoRelacion.SelectedValue = CondicionTarifa.IdTipoRelacion > 0 ? CondicionTarifa.IdTipoRelacion.ToString() : "-1";
                ValidarFormatoCampoValor();
                TxtValor.Text = CondicionTarifa.Porcentaje ? CondicionTarifa.ValorPorcentaje.ToString() : CondicionTarifa.ValorPropio.ToString();
                TxtIdProducto.Text = CondicionTarifa.IdProducto.ToString();
                TxtNombreProducto.Text = CondicionTarifa.NombreProducto;
                TxtIdGrupoProducto.Text = CondicionTarifa.IdGrupoProducto.ToString();
                TxtNombreGrupo.Text = CondicionTarifa.NombreGrupoProducto;
                ChkActivo.Checked = Convert.ToBoolean(CondicionTarifa.IndHabilitado);
                TxtId.Text = CondicionTarifa.Id.ToString();
                TxtTarifa.Text = CondicionTarifa.IdManual.ToString();
                TxtNombreTarifa.Text = CondicionTarifa.NombreTarifa;
                TxtVigenciaTarifa.Text = CondicionTarifa.VigenciaTarifa.ToShortDateString();
                TxtTarifaAlt.Text = CondicionTarifa.IdManualAlterno.ToString();
                TxtNombreTarifaAlt.Text = CondicionTarifa.NombreTarifaAlterna;
                TxtVigenciaTarifaAlt.Text = CondicionTarifa.VigenciaTarifaAlterna.ToShortDateString();
                TxtVigenciaCondicionTarifa.Text = CondicionTarifa.VigenciaCondicion.ToShortDateString();
                TxtValor.Text = string.Format(Resources.GlobalWeb.Formato_DecimalString, CondicionTarifa.ValorPropio);
                TxtObservaciones.Text = CondicionTarifa.DescripcionCondicion;

                if (DdlComponente.SelectedIndex > -1)
                {
                    switch (Convert.ToInt32(DdlTipoRelacion.SelectedValue))
                    {
                        case (int)TipoTarifa.AjusteTarifas:
                            ValidarCamposRequeridos(true);
                            break;

                        default:
                            LimpiarTarifas();
                            ValidarCamposRequeridos(false);
                            break;
                    }
                }
            }
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
        private void ComplementarDatosCondicionTarifa()
        {
            var tipoProducto = DdlTipoProducto.SelectedValue;
            var componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ValorNegativo ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue;
            var tipoRelacion = DdlTipoRelacion.SelectedValue;

            CondicionTarifa.CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad;
            CondicionTarifa.Id = Convert.ToInt32(TxtId.Text);
            CondicionTarifa.Tipo = Properties.Settings.Default.CondicionesTarifa_Tipo;
            CondicionTarifa.IdTipoProducto = tipoProducto.Equals(Resources.GlobalWeb.General_ComboItemValor) ? (short)0 : Convert.ToInt16(tipoProducto);
            CondicionTarifa.Componente = componente;
            CondicionTarifa.IdTercero = VinculacionSeleccionada.Tercero.Id;
            CondicionTarifa.IdContrato = VinculacionSeleccionada.Contrato.Id;
            CondicionTarifa.IdPlan = VinculacionSeleccionada.Plan.Id;
            CondicionTarifa.IdTipoRelacion = Convert.ToInt16(tipoRelacion);
            CondicionTarifa.IdProducto = string.IsNullOrEmpty(TxtIdProducto.Text) ? 0 : Convert.ToInt32(TxtIdProducto.Text);
            CondicionTarifa.IdGrupoProducto = string.IsNullOrEmpty(TxtIdGrupoProducto.Text) ? (short)0 : Convert.ToInt16(TxtIdGrupoProducto.Text);
            CondicionTarifa.IndHabilitado = Convert.ToInt16(ChkActivo.Checked);
            CondicionTarifa.IdManual = string.IsNullOrEmpty(TxtTarifa.Text) ? (short)0 : Convert.ToInt32(TxtTarifa.Text);
            CondicionTarifa.VigenciaTarifa = !string.IsNullOrEmpty(TxtVigenciaTarifa.Text) ? Convert.ToDateTime(TxtVigenciaTarifa.Text) : new DateTime();
            CondicionTarifa.IdManualAlterno = string.IsNullOrEmpty(TxtTarifaAlt.Text) ? (short)0 : Convert.ToInt32(TxtTarifaAlt.Text);
            CondicionTarifa.VigenciaTarifaAlterna = !string.IsNullOrEmpty(TxtVigenciaTarifaAlt.Text) ? Convert.ToDateTime(TxtVigenciaTarifaAlt.Text) : new DateTime();
            CondicionTarifa.VigenciaCondicion = !string.IsNullOrEmpty(TxtVigenciaCondicionTarifa.Text) ? Convert.ToDateTime(TxtVigenciaCondicionTarifa.Text) : new DateTime();
            CondicionTarifa.Usuario = Context.User.Identity.Name;
            CondicionTarifa.ValorPropio = string.IsNullOrEmpty(TxtValor.Text) ? (decimal)0 : Convert.ToDecimal(TxtValor.Text.Replace(".", ","));
            CondicionTarifa.DescripcionCondicion = TxtObservaciones.Text;
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
        private void InsertarCondicionTarifa()
        {
            var resultado = WebService.Facturacion.GuardarCondicionTarifa(CondicionTarifa);

            if (resultado.Ejecuto)
            {
                CondicionTarifa.Id = resultado.Objeto;
                TxtId.Text = resultado.Objeto.ToString();
                MostrarMensaje(string.Format(Resources.DefinirCondicionesTarifa.DefinirCondicionesTarifa_MsjInsercion, CondicionTarifa.Id), TipoMensaje.Ok);
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para limpiar los campos de ajustes.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void LimpiarCamposAjustes()
        {
            TxtTarifa.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreTarifa.Text = string.Empty;
            TxtVigenciaTarifa.Text = string.Empty;
            TxtTarifaAlt.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreTarifaAlt.Text = string.Empty;
            TxtVigenciaTarifaAlt.Text = string.Empty;
        }

        /// <summary>
        /// Metodo para limpiar los campos de ajustes de tipo producto.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void LimpiarCamposProductoGrupo()
        {
            TxtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreProducto.Text = string.Empty;
            TxtIdGrupoProducto.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreGrupo.Text = string.Empty;
        }

        /// <summary>
        /// Metodos para validar los campos requeridos.
        /// </summary>
        /// <param name="activo">If set to <c>true</c> [activo].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ValidarCamposRequeridos(bool activo)
        {
            ImgBuscarTarifa.Enabled = activo;
            ImgBuscarTarifaAlterna.Enabled = activo;
            RfvTarifa.Enabled = activo;
            RfvNombreTarifa.Enabled = activo;
            RfvVigenciaTarifa.Enabled = activo;
            RfvTarifaAlt.Enabled = activo;
            RfvNombreTarifaAlt.Enabled = activo;
            RfvVigenciaTarifaAlt.Enabled = activo;
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
            switch (Convert.ToInt32(DdlTipoRelacion.SelectedValue))
            {
                case (int)TipoTarifa.AjusteTarifas:
                    TxtValor.Text = Resources.GlobalWeb.General_ValorUno;
                    TxtValor.MaxLength = 14;
                    TxtValor.Enabled = true;
                    RevTxtValorDesCr.Enabled = false;
                    rgvValorInvalido.Enabled = true;
                    ImgBuscarTarifa.Enabled = true;
                    ImgBuscarTarifaAlterna.Enabled = true;
                    rfvtxtValorTarCre.Enabled = true;
                    this.validaValor.Enabled = true;
                    break;

                case (int)TipoTarifa.Cantidad:
                case (int)TipoTarifa.ValorPropio:
                case (int)TipoTarifa.ValorMaximo:
                    TxtValor.Text = Resources.GlobalWeb.General_ValorCero;
                    TxtValor.MaxLength = 9;
                    TxtValor.Enabled = true;
                    this.validaValor.Enabled = false;
                    RevTxtValorDesCr.Enabled = true;
                    rgvValorInvalido.Enabled = false;
                    ImgBuscarTarifa.Enabled = false;
                    ImgBuscarTarifaAlterna.Enabled = false;
                    rfvtxtValorTarCre.Enabled = true;
                    LimpiarTarifas();
                    this.TxtValor.Text = "0.00";
                    break;

                case (int)TipoTarifa.ValorMaxPorcentaje:
                    TxtValor.Text = Resources.GlobalWeb.General_ValorCero;
                    TxtValor.MaxLength = 3;
                    RevTxtValorDesCr.Enabled = true;
                    this.validaValor.Enabled = true;
                    rgvValorInvalido.Enabled = false;
                    TxtValor.Enabled = true;
                    ImgBuscarTarifa.Enabled = false;
                    ImgBuscarTarifaAlterna.Enabled = false;
                    rfvtxtValorTarCre.Enabled = true;
                    LimpiarTarifas();
                    break;

                case (int)TipoTarifa.NoCubrimiento:
                    TxtValor.Text = string.Empty;
                    TxtValor.Enabled = false;
                    RevTxtValorDesCr.Enabled = false;
                    rgvValorInvalido.Enabled = false;
                    ImgBuscarTarifa.Enabled = false;
                    ImgBuscarTarifaAlterna.Enabled = false;
                    rfvtxtValorTarCre.Enabled = false;
                    this.validaValor.Enabled = true;
                    LimpiarTarifas();
                    break;

                default:
                    TxtValor.Text = Resources.GlobalWeb.General_ValorCero;
                    TxtValor.Enabled = false;
                    RevTxtValorDesCr.Enabled = false;
                    rgvValorInvalido.Enabled = false;
                    ImgBuscarTarifa.Enabled = false;
                    this.validaValor.Enabled = true;
                    ImgBuscarTarifaAlterna.Enabled = false;
                    rfvtxtValorTarCre.Enabled = true;
                    LimpiarTarifas();
                    break;
            }
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}