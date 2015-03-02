// --------------------------------
// <copyright file="UC_BuscarCondicionesFacturacion.ascx.cs" company="InterGrupo S.A.">
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

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesFacturacion.
    /// </summary>
    public partial class UC_BuscarCondicionesFacturacion : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The VISUALIZAR
        /// </summary>
        private const string VISUALIZAR = "VisualizarConfiguracion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece visualizar configuracion
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

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para Realizar Carga de Datos Iniciales
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarDatosControles()
        {
            txtEntidadDC.Text = VinculacionSeleccionada.Tercero.Nombre;
            txtContratoDC.Text = VinculacionSeleccionada.Contrato.Nombre;
            txtPlanDC.Text = VisualizarConfiguracion ? string.Empty : VinculacionSeleccionada.Plan.Nombre;
            txtPlanDC.Enabled = VisualizarConfiguracion;
            txtAtencionDC.Text = VinculacionSeleccionada.IdAtencion.ToString();
            txtAtencionDC.Enabled = VisualizarConfiguracion;
            CargarComboTipoRelacion();
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            LblMensaje.Visible = false;
            fsResultado.Visible = false;
            txtEntidadDC.Text = string.Empty;
            txtContratoDC.Text = string.Empty;
            txtPlanDC.Text = string.Empty;
            txtAtencionDC.Text = string.Empty;
            DdlTipoRelacion.SelectedIndex = -1;
            chkActivoCF.Checked = true;
            grvCondicionesFacturacion.DataSource = null;
            grvCondicionesFacturacion.DataBind();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Cambia de pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom 
        /// FechaDeCreacion: 15/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        protected void GrvCondicionesFacturacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(0);
            var resultado = WebService.Facturacion.ConsultarTarifas(consulta);

            if (resultado.Objeto.Item.Count == 0)
            {
                pagActual.Visible = false;
                numPaginas.Visible = false;
                lblTotalRegistros.Visible = false;
                lblNumRegistros.Visible = false;
            }
            else
            {
                pagActual.Visible = true;
                numPaginas.Visible = true;
                lblTotalRegistros.Visible = true;
                lblNumRegistros.Visible = true;
            }

            if (resultado.Ejecuto)
            {
                numPaginas.Text = grvCondicionesFacturacion.PageCount.ToString() + ".";
                grvCondicionesFacturacion.PageIndex = e.NewPageIndex;
                grvCondicionesFacturacion.DataSource = resultado.Objeto.Item;
                grvCondicionesFacturacion.DataBind();
                
                // CargaObjetos.OrdenamientoGrilla(this.Page, grvCondicionesFacturacion, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvCondicionesFacturacion.DataSource = null;
                grvCondicionesFacturacion.DataBind();
            }
        }

        /// <summary>
        /// Metodo para cargar la grilla de productos
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCondicionesFacturacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RecargarModal();
            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorCondicionTarifa = Convert.ToInt32(grvCondicionesFacturacion.DataKeys[indiceFila].Value);

                var resultado = WebService.Facturacion.ConsultarTarifas(
                    new Paginacion<CondicionTarifa>()
                    {
                        PaginaActual = 0,
                        LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                        Item = new CondicionTarifa()
                        {
                            CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                            Id = identificadorCondicionTarifa,
                            IndHabilitado = Convert.ToInt16(chkActivoCF.Checked),
                            Tipo = Properties.Settings.Default.CondicionesFacturacion_Tipo,
                            Maestras = new Maestras()
                            {
                                IdPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesFacturacion)
                            }
                        }
                    });

                if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                {
                    mltvCondicionesFacturacion.SetActiveView(vCrearCondicionesFact);
                    ucCondicionesFacturacion.LimpiarControles();
                    ucCondicionesFacturacion.VisualizarConfiguracion = VisualizarConfiguracion;
                    var itemCondicion = resultado.Objeto.Item.FirstOrDefault();
                    ucCondicionesFacturacion.CondicionFacturacion = new CondicionFacturacion()
                    {
                        NombreContrato = txtContratoDC.Text,
                        NombreTercero = txtEntidadDC.Text,
                        NombrePlan = txtPlanDC.Text,
                        CodigoEntidad = itemCondicion.CodigoEntidad,
                        Contrato = itemCondicion.Contrato,
                        DescripcionCondicion = itemCondicion.DescripcionCondicion,
                        EstadoTarifa = itemCondicion.EstadoTarifa,
                        FechaFinal = itemCondicion.FechaFinal,
                        FechaInicial = itemCondicion.FechaInicial,
                        Id = itemCondicion.Id,
                        IdAtencion = itemCondicion.IdAtencion,
                        IdContrato = itemCondicion.IdContrato,
                        IdPlan = itemCondicion.IdPlan,
                        IdTercero = itemCondicion.IdTercero,
                        IdTipoRelacion = itemCondicion.IdTipoRelacion,
                        IndHabilitado = itemCondicion.IndHabilitado,
                        Tercero = itemCondicion.Tercero,
                        Tipo = itemCondicion.Tipo,
                        TipoRelacion = itemCondicion.TipoRelacion,
                        ValorPropio = itemCondicion.ValorPropio,
                        ValorPorcentaje = itemCondicion.ValorPorcentaje,
                        VigenciaCondicion = itemCondicion.VigenciaCondicion
                    };

                    ucCondicionesFacturacion.CargarControles(Global.TipoOperacion.MODIFICACION);
                    ucCondicionesFacturacion.ActivarCampos(SAHI.Comun.Utilidades.Global.TipoOperacion.CONSULTA);
                }
            }

            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.MODIFICAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorCondicionTarifa = Convert.ToInt32(grvCondicionesFacturacion.DataKeys[indiceFila].Value);

                var resultado = WebService.Facturacion.ConsultarTarifas(
                    new Paginacion<CondicionTarifa>()
                    {
                        PaginaActual = 0,
                        LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                        Item = new CondicionTarifa()
                        {
                            CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                            Id = identificadorCondicionTarifa,
                            IndHabilitado = Convert.ToInt16(chkActivoCF.Checked),
                            Tipo = Properties.Settings.Default.CondicionesFacturacion_Tipo,
                            Maestras = new Maestras()
                            {
                                IdPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesFacturacion)
                            }
                        }
                    });

                if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                {
                    mltvCondicionesFacturacion.SetActiveView(vCrearCondicionesFact);
                    ucCondicionesFacturacion.LimpiarControles();
                    ucCondicionesFacturacion.VisualizarConfiguracion = VisualizarConfiguracion;
                    var itemCondicion = resultado.Objeto.Item.FirstOrDefault();
                    ucCondicionesFacturacion.CondicionFacturacion = new CondicionFacturacion()
                    {
                        NombreContrato = txtContratoDC.Text,
                        NombreTercero = txtEntidadDC.Text,
                        NombrePlan = txtPlanDC.Text,
                        CodigoEntidad = itemCondicion.CodigoEntidad,
                        Contrato = itemCondicion.Contrato,
                        DescripcionCondicion = itemCondicion.DescripcionCondicion,
                        EstadoTarifa = itemCondicion.EstadoTarifa,
                        FechaFinal = itemCondicion.FechaFinal,
                        FechaInicial = itemCondicion.FechaInicial,
                        Id = itemCondicion.Id,
                        IdAtencion = itemCondicion.IdAtencion,
                        IdContrato = itemCondicion.IdContrato,
                        IdPlan = itemCondicion.IdPlan,
                        IdTercero = itemCondicion.IdTercero,
                        IdTipoRelacion = itemCondicion.IdTipoRelacion,
                        IndHabilitado = itemCondicion.IndHabilitado,
                        Tercero = itemCondicion.Tercero,
                        Tipo = itemCondicion.Tipo,
                        TipoRelacion = itemCondicion.TipoRelacion,
                        ValorPropio = itemCondicion.ValorPropio,
                        ValorPorcentaje = itemCondicion.ValorPorcentaje,
                        VigenciaCondicion = itemCondicion.VigenciaCondicion
                    };

                    ucCondicionesFacturacion.CargarControles(Global.TipoOperacion.MODIFICACION);
                    ucCondicionesFacturacion.ActivarCampos(SAHI.Comun.Utilidades.Global.TipoOperacion.MODIFICACION);
                }
            }
        }

        /// <summary>
        /// Evento de edición de grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCondicionesFacturacion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Metodo para realizar la Búsqueda
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
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
        /// Metodo para realizar la creación de Condicion de Facturación
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgNuevo_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            mltvCondicionesFacturacion.SetActiveView(vCrearCondicionesFact);
            ucCondicionesFacturacion.LimpiarControles();
            ucCondicionesFacturacion.VisualizarConfiguracion = VisualizarConfiguracion;

            ucCondicionesFacturacion.CondicionFacturacion = new CondicionFacturacion()
            {
                NombrePlan = VinculacionSeleccionada.Plan.Nombre,
                NombreTercero = VinculacionSeleccionada.Tercero.Nombre,
                NombreContrato = VinculacionSeleccionada.Contrato.Nombre,
                IdAtencion = VinculacionSeleccionada.IdAtencion,
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdPlan = VinculacionSeleccionada.Plan.Id,
                IdContrato = VinculacionSeleccionada.Contrato.Id
            };

            ucCondicionesFacturacion.CargarControles(Global.TipoOperacion.CREACION);
            ucCondicionesFacturacion.ActivarCampos(Global.TipoOperacion.CREACION);
        }

        /// <summary>
        /// Metodo de Regresar de la Pantalla
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            // pagControl.PageIndexChanged -= PagControl_PageIndexChanged;
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
            ucCondicionesFacturacion.OperacionEjecutada += CondicionesFacturacion_OperacionEjecutada;
            
            // pagControl.PageIndexChanged += PagControl_PageIndexChanged;
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
        /// Metodo para cargar Tipo de Relacion
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
        /// Obtiene los parametros para la información de busque
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 02/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Facturacion.ConsultarTarifas(consulta);

            if (resultado.Objeto.Item.Count == 0)
            {
                pagActual.Visible = false;
                numPaginas.Visible = false;
                lblTotalRegistros.Visible = false;
                lblNumRegistros.Visible = false;
            }
            else
            {
                pagActual.Visible = true;
                numPaginas.Visible = true;
                lblTotalRegistros.Visible = true;
                lblNumRegistros.Visible = true;
            }

            if (resultado.Ejecuto)
            {
                grvCondicionesFacturacion.DataSource = resultado.Objeto.Item;
                grvCondicionesFacturacion.DataBind();
                numPaginas.Text = grvCondicionesFacturacion.PageCount.ToString() + ".";
                lblNumRegistros.Text = resultado.Objeto.Item.Count.ToString();
                
                // CargaObjetos.OrdenamientoGrilla(this.Page, grvCondicionesFacturacion, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvCondicionesFacturacion.DataSource = null;
                grvCondicionesFacturacion.DataBind();
            }
        }

        /// <summary>
        /// Metodo para cargar paginador.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CondicionesFacturacion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvCondicionesFacturacion.SetActiveView(vCondicionesFacturacion);
        }

        /// <summary>
        /// Metodo para Crear Objeto de Consulta.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Retorna Tercero.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<CondicionTarifa> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<CondicionTarifa> consulta = new Paginacion<CondicionTarifa>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new CondicionTarifa()
                {
                    CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IdTercero = VinculacionSeleccionada.Tercero.Id,
                    IdAtencion = !string.IsNullOrEmpty(txtAtencionDC.Text) ? Convert.ToInt32(txtAtencionDC.Text) : 0,
                    IndHabilitado = Convert.ToInt16(chkActivoCF.Checked),
                    IdTipoRelacion = DdlTipoRelacion.SelectedIndex > 0 ? Convert.ToInt16(DdlTipoRelacion.SelectedValue) : (short)0,
                    Tipo = Properties.Settings.Default.CondicionesFacturacion_Tipo,
                    Maestras = new Maestras()
                    {
                        IdPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesFacturacion)
                    },
                    NombrePlan = txtPlanDC.Text
                }
            };

            return consulta;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}