// --------------------------------
// <copyright file="UC_CrearDescuentos.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.Descuentos.
    /// </summary>
    public partial class UC_CrearDescuentos : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CODIGOCOMBO.
        /// </summary>
        private const string CODIGOCOMBO = "Codigo";

        /// <summary>
        /// The CODIGOCOMPONENTE.
        /// </summary>
        private const string CODIGOCOMPONENTE = "CodigoComponente";

        /// <summary>
        /// The CONTRATO
        /// </summary>
        private const string CONTRATO = "Contrato";

        /// <summary>
        /// The DESCUENTO
        /// </summary>
        private const string DESCUENTO = "Descuento";

        /// <summary>
        /// The IDCOMBO
        /// </summary>
        private const string IDCOMBO = "Id";

        /// <summary>
        /// The MODOPANATALLA
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
        /// Constante NOQX.
        /// </summary>
        private const string NOQX = "N";

        /// <summary>
        /// Constante QX.
        /// </summary>
        private const string QX = "Q";

        /// <summary>
        /// The TERCEROENTIDAD
        /// </summary>
        private const string TERCEROENTIDAD = "Tercero";

        /// <summary>
        /// The TEXTOSERVICIO
        /// </summary>
        private const string TEXTOSERVICIO = "NombreServicio";

        /// <summary>
        /// The TIPOOPERACION
        /// </summary>
        private const string TIPOOPERACION = "TipoOperacion";

        /// <summary>
        /// The VALORSERVICIO
        /// </summary>
        private const string VALORSERVICIO = "IdServicio";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece contrato.
        /// </summary>
        /// <value>
        /// Tipo Contrato.
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
        /// Obtiene o establece tipo operacion
        /// </summary>
        public DescuentoConfiguracion Descuento
        {
            get
            {
                return ViewState[DESCUENTO] != null ? (DescuentoConfiguracion)ViewState[DESCUENTO] : new DescuentoConfiguracion();
            }

            set
            {
                ViewState[DESCUENTO] = value;
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
        /// Cambiar estado controles formulario
        /// </summary>
        /// <param name="habilitado">if set to <c>true</c> [habilitado].</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 19/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CambiarEstadoControlesFormulario(bool habilitado)
        {
            lblTitulo.Text = !habilitado ? Resources.Descuentos.Descuentos_TituloConsultar : Resources.Descuentos.Descuentos_TituloModificar;
            ddlClaseServicio.Enabled = habilitado;
            ddlTipoAtencion.Enabled = habilitado;
            DdlPlan.Enabled = habilitado;
            txtAtencion.Enabled = habilitado && ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla);
            imgBuscarAtencion.Enabled = habilitado;
            chkActivo.Enabled = habilitado;
            DdlTipoProducto.Enabled = habilitado;
            TxtIdProducto.Enabled = habilitado;
            TxtIdGrupoProducto.Enabled = habilitado;
            DdlComponente.Enabled = habilitado;
            DdlTipoRelacion.Enabled = habilitado;
            txtValorDesCr.Enabled = habilitado;
            txtFechaInicialDesCr.Enabled = habilitado;
            txtFechaFinalDesCr.Enabled = habilitado;
            chkAplicar.Enabled = habilitado;
            DdlVisualizacion.Enabled = habilitado;
            ImgGuardarDescuentos.Enabled = habilitado;
            ImgBuscarGrupoProducto.Enabled = habilitado;
            ImgBuscarProducto.Enabled = habilitado;
            imgCalendarFechaInicial.Visible = habilitado;
            imgCalendarFechaFinal.Visible = habilitado;
        }

        /// <summary>
        /// Metodo de Cargar Controles
        /// </summary>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarControles(CliCountry.SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion)
        {
            TipoOperacion = tipoOperacion;
            CargarComboTipoRelacion();
            CargarComboTipoProducto();

            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                CargarTipoAtencion();
                CargarVisualizacion();
                CargarClaseServicio();
                CargarListaTiposComponentes(string.Empty);
                CargarPlanes();
                CargarControlesModo(true);
            }
            else
            {
                CargarListaTiposComponentes(string.Empty);
                CargarControlesModo(false);
            }

            switch (TipoOperacion)
            {
                case Global.TipoOperacion.CREACION:
                    CargarControlesEvento(false);
                    CargarControlesModoCreacion(true);
                    break;

                case Global.TipoOperacion.MODIFICACION:
                    CargarControlesEvento(true);
                    break;
            }
        }

        /// <summary>
        /// Metodo de Limpiar Controles
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarControles()
        {
            chkActivo.Checked = true;
            LblMensaje.Text = string.Empty;
            LblMensaje.Visible = false;
            txtEntidad.Text = string.Empty;
            txtAtencion.Text = string.Empty;
            txtFechaFinalDesCr.Text = string.Empty;
            txtFechaInicialDesCr.Text = string.Empty;
            txtIdentificadorDesCr.Text = string.Empty;
            TxtIdGrupoProducto.Text = string.Empty;
            TxtIdProducto.Text = string.Empty;
            TxtNombreGrupo.Text = string.Empty;
            TxtNombreProducto.Text = string.Empty;
            txtPlan.Text = string.Empty;
            txtValorDesCr.Text = string.Empty;
            DdlTipoProducto.SelectedIndex = -1;
            DdlComponente.SelectedIndex = -1;
            ddlClaseServicio.SelectedIndex = -1;
            DdlPlan.Items.Clear();
            DdlPlan.DataSource = null;
            DdlPlan.DataBind();
            ddlTipoAtencion.SelectedIndex = -1;
            Descuento = null;
            TipoOperacion = Global.TipoOperacion.SALIR;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para realizar la Busqueda del Grupo de Producto
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
                Descuento.IdAtencion);
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
        /// Metodo para buscar producto
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
                Descuento.IdAtencion);
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
        /// Metodo para Validar las operaciones del Boton Buscar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 15/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarAtencion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvCrearDescuentos.SetActiveView(vCrearDescuentos);
        }

        /// <summary>
        /// Evento SeleccionarItemGrid de BuscarAtencion
        /// </summary>
        /// <param name="e">Parámetro e.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarAtencion_SeleccionarItemGrid(EventoControles<Atencion> e)
        {
            RecargarModal();
            ucBuscarAtencion.LimpiarCampos();
            mltvCrearDescuentos.SetActiveView(vCrearDescuentos);
            if (e.Resultado != null)
            {
                txtAtencion.Text = e.Resultado.IdAtencion.ToString();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DdlTipoProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 18/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarModal();
            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                string clasif = string.Empty;
                LimpiarCamposAjustes();
                DdlComponente.Items.Clear();
                DdlComponente.DataSource = null;
                DdlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(DdlTipoProducto.SelectedValue));
                CargarListaTiposComponentes(clasif);
            }
            else
            {
                string clasif = string.Empty;
                LimpiarCamposAjustes();
                DdlComponente.Items.Clear();
                DdlComponente.DataSource = null;
                DdlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);

                clasif = EstablecerClasificacionProducto(Convert.ToInt32(DdlTipoProducto.SelectedValue));
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo de Seleccion de Indice del Tipo Relacion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void DdlTipoRelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarFormatoCampoValor();
        }

        /// <summary>
        /// Evento click de imgBuscarAtencion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarAtencion_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            mltvCrearDescuentos.SetActiveView(vBuscarAtencion);
            ucBuscarAtencion.LimpiarCampos();
        }

        /// <summary>
        /// Metodo para realizar la busqueda de Grupo de PRoducto
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarGrupoProducto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            short tipoProducto = 0;
            bool valida = short.TryParse(DdlTipoProducto.SelectedValue, out tipoProducto);
            ucBuscarGrupoProductos.LimpiarCampos();
            ucBuscarGrupoProductos.IdTipoProducto = tipoProducto == -1 ? 0 : tipoProducto;
            ucBuscarGrupoProductos.IdAtencion = !string.IsNullOrEmpty(txtAtencion.Text) ? Convert.ToInt32(txtAtencion.Text) : 0;
            mltvCrearDescuentos.SetActiveView(vBuscarGrupoProductos);
        }

        /// <summary>
        /// Metodo para realizar la Busqueda del Tipo de Producto
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarProducto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            short identificadorTipoProducto = 0;
            short identificadorGrupo = 0;

            if (string.IsNullOrEmpty(TxtIdGrupoProducto.Text) || !short.TryParse(TxtIdGrupoProducto.Text, out identificadorGrupo))
            {
                identificadorGrupo = 0;
            }

            if (!short.TryParse(DdlTipoProducto.SelectedValue.ToString(), out identificadorTipoProducto) || DdlTipoProducto.SelectedValue.Equals(ValorDefecto.ValorDefectoCombo))
            {
                identificadorTipoProducto = 0;
            }

            RecargarModal();
            ucBuscarProductos.LimpiarCampos();
            ucBuscarProductos.IdGrupoProducto = identificadorGrupo;
            ucBuscarProductos.IdTipoProducto = identificadorTipoProducto;
            ucBuscarProductos.IdAtencion = string.IsNullOrEmpty(txtAtencion.Text) ? 0 : Convert.ToInt32(txtAtencion.Text);
            mltvCrearDescuentos.SetActiveView(vBuscarProductos);
        }

        /// <summary>
        /// Metodo para guardar el descuento
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgGuardarDescuentos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            if (Page.IsValid)
            {
                ComplementarDatosDescuento();

                if (Descuento.Id > 0 && TipoOperacion == Global.TipoOperacion.MODIFICACION)
                {
                    ActualizarDescuento();
                }
                else
                {
                    InsertarDescuento();
                }
            }
        }

        /// <summary>
        /// Metodo para regresar a la pagina principal
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgRegresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Metodo de Inicializacion de la pagina
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarGrupoProductos.SeleccionarItemGrid += BuscarGrupoProductos_SeleccionarItemGrid;
            ucBuscarGrupoProductos.OperacionEjecutada += BuscarGrupoProductos_OperacionEjecutada;
            ucBuscarProductos.SeleccionarItemGrid += BuscarProductos_SeleccionarItemGrid;
            ucBuscarProductos.OperacionEjecutada += BuscarProductos_OperacionEjecutada;
            ucBuscarAtencion.SeleccionarItemGrid += BuscarAtencion_SeleccionarItemGrid;
            ucBuscarAtencion.OperacionEjecutada += BuscarAtencion_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo de carga de la pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para realizar la actualizacion del descuento
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ActualizarDescuento()
        {
            var resultado = WebService.Facturacion.ActualizarDescuento(Descuento);
            if (resultado.Ejecuto)
            {
                if (resultado.Objeto)
                {
                    MostrarMensaje(Resources.Descuentos.Descuentos_MsjActualizacion, TipoMensaje.Ok);
                }
                else
                {
                    MostrarMensaje(Resources.Descuentos.Descuentos_MsjActualizacionError, TipoMensaje.Error);
                }
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo de Operacion ejecutada del grupo de producto
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void BuscarGrupoProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    mltvCrearDescuentos.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccion del Grupo de Producto
        /// </summary>
        /// <param name="e">Parámetro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void BuscarGrupoProductos_SeleccionarItemGrid(EventoControles<GrupoProducto> e)
        {
            RecargarModal();
            mltvCrearDescuentos.SetActiveView(vCrearDescuentos);
            if (e.Resultado != null)
            {
                string clasif = string.Empty;
                CargarCamposGrupo(e.Resultado);

                clasif = EstablecerClasificacionProducto(e.Resultado.IdTipoProducto);
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo Indica la Operacion EjEcutada
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void BuscarProductos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    mltvCrearDescuentos.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccion del Producto.
        /// </summary>
        /// <param name="e">Parámetro e.</param>
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
            mltvCrearDescuentos.SetActiveView(vCrearDescuentos);
            if (e.Resultado != null)
            {
                string clasif = string.Empty;
                CargarCamposProducto(e.Resultado);

                clasif = EstablecerClasificacionProducto(e.Resultado.IdTipoProducto);
                CargarListaTiposComponentes(clasif);
            }
        }

        /// <summary>
        /// Metodo para realizar carga de campos dependientes
        /// </summary>
        /// <param name="grupo">The grupo.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarCamposGrupo(GrupoProducto grupo)
        {
            TxtIdProducto.Text = "0";
            TxtNombreProducto.Text = string.Empty;
            TxtIdGrupoProducto.Text = grupo.IdGrupo.ToString();
            TxtNombreGrupo.Text = grupo.Nombre;
            DdlTipoProducto.SelectedValue = grupo.IdTipoProducto.ToString();
        }

        /// <summary>
        /// Metodo para realizar carga de campos dependientes
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
        /// Cargar clase de servicio
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 06/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarClaseServicio()
        {
            var servicios = WebService.Configuracion.ConsultarServicios();
            if (servicios.Ejecuto)
            {
                ddlClaseServicio.DataSource = servicios.Objeto;
                ddlClaseServicio.DataValueField = VALORSERVICIO;
                ddlClaseServicio.DataTextField = TEXTOSERVICIO;
                ddlClaseServicio.DataBind();
            }
            else
            {
                MostrarMensaje(servicios.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para realizar la Carga del Combo de Tipo Producto
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarComboTipoProducto()
        {
            int identificadorAtencion = ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla) ? 0 : VinculacionSeleccionada.IdAtencion;
            try
            {
                var resultado = WebService.Integracion.ConsultarTipoProducto(
                new TipoProducto()
                {
                    IndHabilitado = true
                },
                identificadorAtencion);

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
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Tipo de Relacion
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: Aura Victoria Forero Varela - INTERGRUPO\Aforero
        /// FechaDeUltimaModificacion: (06/09/2013)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarComboTipoRelacion()
        {
            var resultado = WebService.Integracion.ConsultarGenListas(
                new Basica
                {
                    CodigoGrupo = Resources.GlobalWeb.CodGenLista_Relacion,
                    IndHabilitado = true
                });

            if (resultado.Ejecuto)
            {
                DdlTipoRelacion.DataTextField = NOMBRECOMBO;
                DdlTipoRelacion.DataValueField = CODIGOCOMBO;
                DdlTipoRelacion.DataSource = resultado.Objeto;
                DdlTipoRelacion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlTipoRelacion, false);
            }
            else
            {
                DdlTipoRelacion.DataSource = null;
                DdlTipoRelacion.DataBind();
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para Cargar Componente
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarComponente()
        {
            var resultado = WebService.Facturacion.ConsultarComponentes(VinculacionSeleccionada.IdAtencion, 0);

            if (resultado.Ejecuto)
            {
                DdlComponente.Items.Clear();
                DdlComponente.DataTextField = "NombreComponente";
                DdlComponente.DataValueField = "CodigoComponente";
                DdlComponente.DataSource = resultado.Objeto;
                DdlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);
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
            if (resultado.Ejecuto)
            {
                DdlComponente.DataSource = null;
                DdlComponente.DataBind();
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
        /// Metodo para cargar controles por evento
        /// </summary>
        /// <param name="modificacion">if set to <c>true</c> [modificacion].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarControlesEvento(bool modificacion)
        {
            string clasif = EstablecerClasificacionProducto(Convert.ToInt16(Descuento.IdTipoProducto));
            CargarListaTiposComponentes(clasif);
            this.lblTitulo.Text = modificacion ? Resources.Descuentos.Descuentos_TituloModificar : Resources.Descuentos.Descuentos_TituloCrear;
            txtEntidad.Text = Descuento.NombreTercero;
            txtContrato.Text = Descuento.NombreContrato;
            txtPlan.Text = Descuento.NombrePlan;
            txtAtencion.Text = Descuento.IdAtencion.ToString();
            if (modificacion)
            {
                DdlTipoProducto.SelectedValue = Descuento.IdTipoProducto > 0 ? Descuento.IdTipoProducto.ToString() : Resources.GlobalWeb.General_ValorNegativo;
                DdlTipoRelacion.SelectedValue = Descuento.IdTipoRelacion > 0 ? Descuento.IdTipoRelacion.ToString() : Resources.GlobalWeb.General_ValorNegativo;
                ValidarFormatoCampoValor();
                txtFechaInicialDesCr.Text = Descuento.FechaInicial.ToShortDateString();
                txtFechaFinalDesCr.Text = Descuento.FechaInicial.Date < Descuento.FechaFinal.Date ? Descuento.FechaFinal.ToShortDateString() : string.Empty;
                txtValorDesCr.Text = Descuento.ValorDescuento > 0 ? string.Format(Resources.GlobalWeb.Formato_DecimalString, Descuento.ValorDescuento) : Resources.GlobalWeb.General_ValorCero;
                TxtIdProducto.Text = Descuento.IdProducto.ToString();
                TxtNombreProducto.Text = Descuento.NombreProducto;
                TxtIdGrupoProducto.Text = Descuento.IdGrupoProducto.ToString();
                TxtNombreGrupo.Text = Descuento.NombreGrupoProducto;
                txtIdentificadorDesCr.Text = Descuento.Id.ToString();
                chkActivo.Checked = Convert.ToBoolean(Descuento.IndicadorActivo);
                DdlComponente.SelectedValue = string.IsNullOrEmpty(Descuento.Componente) ? Resources.GlobalWeb.General_ValorNA : Descuento.Componente;
                if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
                {
                    DdlPlan.SelectedValue = Descuento.IdPlan > 0 ? Descuento.IdPlan.ToString() : Resources.GlobalWeb.General_ValorCero;
                    ddlClaseServicio.SelectedValue = Descuento.IdServicio > 0 ? Descuento.IdServicio.ToString() : Resources.GlobalWeb.General_ValorCero;
                    ddlTipoAtencion.SelectedValue = Descuento.IdTipoAtencion > 0 ? Descuento.IdTipoAtencion.ToString() : Resources.GlobalWeb.General_ValorCero;
                    txtAtencion.Text = Descuento.IdAtencion.ToString();
                    chkAplicar.Checked = Descuento.IndicadorFacturaDescuento == 1 ? true : false;
                    DdlVisualizacion.SelectedValue = string.IsNullOrEmpty(Descuento.IndVisualizacion) ? Resources.GlobalWeb.General_ValorN : Descuento.IndVisualizacion;
                }
            }
        }

        /// <summary>
        /// Cargar controles modo
        /// </summary>
        /// <param name="configuracion">if set to <c>true</c> [configuracion].</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarControlesModo(bool configuracion)
        {
            ddlTipoAtencion.Visible = configuracion;
            DdlPlan.Visible = configuracion;
            DdlVisualizacion.Enabled = configuracion;
            txtPlan.Visible = !configuracion;
            ddlClaseServicio.Enabled = configuracion;
            imgBuscarAtencion.Enabled = configuracion;
            chkAplicar.Visible = configuracion;
            lblAplicar.Visible = configuracion;
            trServicioAtencion.Visible = configuracion;
            trVisualizacion.Visible = configuracion;
            imgBuscarAtencion.Visible = configuracion;
            txtAtencion.Enabled = configuracion;
            txtIdentificadorDesCr.Enabled = false;
        }

        /// <summary>
        /// Cargar controles modo creacion
        /// </summary>
        /// <param name="configuracion">if set to <c>true</c> [configuracion].</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 12/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarControlesModoCreacion(bool configuracion)
        {
            DdlTipoProducto.Enabled = configuracion;
            ImgBuscarGrupoProducto.Enabled = configuracion;
            ImgBuscarProducto.Enabled = configuracion;
            DdlComponente.Enabled = configuracion;
            DdlTipoRelacion.Enabled = configuracion;
            txtValorDesCr.Enabled = configuracion;
            txtFechaInicialDesCr.Enabled = configuracion;
            txtFechaFinalDesCr.Enabled = configuracion;
            ImgGuardarDescuentos.Enabled = configuracion;
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
        /// Cargar planes
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 06/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarPlanes()
        {
            RecargarModal();
            int contratoValor = -1;
            bool valida = int.TryParse(Contrato.Id.ToString(), out contratoValor);

            var contrato = new Contrato
            {
                Id = contratoValor,
                IdTercero = int.Parse(Descuento.CodigoEntidad)
            };

            var resultado = WebService.Facturacion.ConsultarPlanes(contrato);

            if (resultado.Ejecuto)
            {
                DdlPlan.DataSource = resultado.Objeto;
                DdlPlan.DataValueField = IDCOMBO;
                DdlPlan.DataTextField = NOMBRECOMBO;
                DdlPlan.DataBind();
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Cargar tipo atención
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 06/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarTipoAtencion()
        {
            var tipoAtencion = WebService.Configuracion.ConsultaTipoAtenciones();
            if (tipoAtencion.Ejecuto)
            {
                ddlTipoAtencion.DataSource = tipoAtencion.Objeto;
                ddlTipoAtencion.DataValueField = IDCOMBO;
                ddlTipoAtencion.DataTextField = NOMBRECOMBO;
                ddlTipoAtencion.DataBind();
            }
            else
            {
                MostrarMensaje(tipoAtencion.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Cargar visualización
        /// </summary>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero
        /// FechaDeCreacion: 06/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarVisualizacion()
        {
            var visualizacion = WebService.Integracion.ConsultarGenListas(
                new Basica
                {
                    IndHabilitado = true,
                    CodigoGrupo = Resources.GlobalWeb.CodGenLista_Visualizacion
                });

            if (visualizacion.Ejecuto && visualizacion.Objeto != null & visualizacion.Objeto.Count > 0)
            {
                DdlVisualizacion.DataSource = visualizacion.Objeto;
                DdlVisualizacion.DataValueField = CODIGOCOMBO;
                DdlVisualizacion.DataTextField = NOMBRECOMBO;
                DdlVisualizacion.DataBind();
            }
            else
            {
                MostrarMensaje(visualizacion.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para registrar los campos del descuento
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 24/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ComplementarDatosDescuento()
        {
            var tipoProducto = DdlTipoProducto.SelectedValue;
            var tipoRelacion = DdlTipoRelacion.SelectedValue;
            Descuento.CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad;
            string componente = string.IsNullOrEmpty(DdlComponente.SelectedValue.ToString()) || DdlComponente.SelectedValue.Equals(Resources.GlobalWeb.General_ValorNegativo) ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue;
            Descuento.Componente = componente;
            Descuento.FechaInicial = Convert.ToDateTime(txtFechaInicialDesCr.Text);
            Descuento.FechaFinal = string.IsNullOrEmpty(txtFechaFinalDesCr.Text) ? new DateTime() : Convert.ToDateTime(txtFechaFinalDesCr.Text);
            Descuento.IdGrupoProducto = string.IsNullOrEmpty(TxtIdGrupoProducto.Text) ? (short)0 : Convert.ToInt16(TxtIdGrupoProducto.Text);
            Descuento.IdProducto = string.IsNullOrEmpty(TxtIdProducto.Text) ? (long)0 : Convert.ToInt64(TxtIdProducto.Text);
            Descuento.IdTipoProducto = tipoProducto.Equals(Resources.GlobalWeb.General_ComboItemValor) ? (short)0 : Convert.ToInt16(tipoProducto);
            Descuento.IdTipoRelacion = Convert.ToInt16(tipoRelacion);
            Descuento.IndicadorActivo = Convert.ToInt16(chkActivo.Checked);
            Descuento.ValorDescuento = Convert.ToDecimal(txtValorDesCr.Text.Replace(".", ","));

            if (ModoPantalla.Equals(Resources.GlobalWeb.Configuracion_Modo_Pantalla))
            {
                int identificadorPlan = 0;
                bool validaPlan = int.TryParse(DdlPlan.SelectedValue, out identificadorPlan);
                int identificadorServicio = 0;
                bool validaServicio = int.TryParse(ddlClaseServicio.SelectedValue, out identificadorServicio);
                int identificadorContrato = 0;
                bool contrato = int.TryParse(Contrato.Id.ToString(), out identificadorContrato);
                int identificadorTipoAtencion = 0;
                bool validaTipoAtencion = int.TryParse(ddlTipoAtencion.SelectedValue, out identificadorTipoAtencion);
                int identificadorAtencion = 0;
                bool validaAtencion = int.TryParse(txtAtencion.Text, out identificadorAtencion);
                short indFacturar = chkAplicar.Enabled == true ? (short)1 : (short)0;
                string indVisualizacion = string.IsNullOrEmpty(DdlVisualizacion.SelectedValue.ToString()) ? Resources.GlobalWeb.General_ValorN : DdlVisualizacion.SelectedValue;
                Descuento.IdContrato = identificadorContrato;
                Descuento.IdPlan = identificadorPlan;
                Descuento.IdTercero = VinculacionSeleccionada.Tercero.Id;
                Descuento.IdServicio = identificadorServicio;
                Descuento.IdTipoAtencion = identificadorTipoAtencion;
                Descuento.IdAtencion = identificadorAtencion;
                Descuento.IndicadorFacturaDescuento = indFacturar;
                Descuento.IndVisualizacion = indVisualizacion;
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
        /// Metodo para realizar la insercion del descuento
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void InsertarDescuento()
        {
            var resultado = WebService.Facturacion.GuardarDescuento(Descuento);
            if (resultado.Ejecuto)
            {
                Descuento.Id = resultado.Objeto;
                txtIdentificadorDesCr.Text = resultado.Objeto.ToString();
                MostrarMensaje(string.Format(Resources.Descuentos.Descuentos_MsjInsercion, Descuento.Id), TipoMensaje.Ok);
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
        private void LimpiarCamposAjustes()
        {
            TxtIdProducto.Text = Resources.GlobalWeb.General_ValorCero;
            TxtIdGrupoProducto.Text = Resources.GlobalWeb.General_ValorCero;
            TxtNombreProducto.Text = string.Empty;
            TxtNombreGrupo.Text = string.Empty;
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ValidarFormatoCampoValor()
        {
            RecargarModal();
            if ((DdlTipoRelacion.SelectedValue == Resources.GlobalWeb.General_ValorUno) ||
                DdlTipoRelacion.SelectedValue.Equals("1"))
            {
                txtValorDesCr.Text = Resources.GlobalWeb.General_ValorCero;
                txtValorDesCr.MaxLength = 3;
                RevTxtValorDesCr.Enabled = true;
                validaValor.Enabled = false;
            }
            else
            {
                txtValorDesCr.Text = Resources.GlobalWeb.General_ValorCero;
                txtValorDesCr.MaxLength = 12;
                RevTxtValorDesCr.Enabled = false;
                validaValor.Enabled = true;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}