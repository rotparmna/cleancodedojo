// --------------------------------
// <copyright file="UC_BuscarCondicionesTarifas.ascx.cs" company="InterGrupo S.A.">
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
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesTarifas
    /// </summary>
    public partial class UC_BuscarCondicionesTarifas : WebUserControl
    {
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
            if (VinculacionSeleccionada != null)
            {
                txtEntidadDC.Text = VinculacionSeleccionada.Tercero.Nombre;
                txtContratoDC.Text = VinculacionSeleccionada.Contrato.Nombre;
                txtPlanDC.Text = VinculacionSeleccionada.Plan.Nombre;
                txtAtencionDC.Text = VinculacionSeleccionada.IdAtencion.ToString();
                CargarComponente();
                CargarComboTipoRelacion();
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
        public void CargarGrilla(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Facturacion.ConsultarTarifas(consulta);

            if (resultado.Ejecuto)
            {
                CargaObjetos.OrdenamientoGrilla(this.Page, grvCondicionesTarifas, resultado.Objeto.Item);
                CargarPaginador(resultado.Objeto);
            }
            else
            {
                pagControl.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvCondicionesTarifas.DataSource = null;
                grvCondicionesTarifas.DataBind();
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
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            mltvBuscarCT.SetActiveView(vDefinirCondicionesTarifa);
            LblMensaje.Visible = false;
            fsResultado.Visible = false;
            txtEntidadDC.Text = string.Empty;
            txtContratoDC.Text = string.Empty;
            txtPlanDC.Text = string.Empty;
            txtAtencionDC.Text = string.Empty;
            txtTipoProductoTar.Text = string.Empty;
            txtGrupoProdTar.Text = string.Empty;
            txtProdTar.Text = string.Empty;
            DdlComponente.SelectedIndex = -1;
            DdlTipoRelacion.SelectedIndex = -1;
            chkActivo.Checked = true;
            grvCondicionesTarifas.DataSource = null;
            grvCondicionesTarifas.DataBind();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

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
        protected void GrvCondicionesTarifas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RecargarModal();

            if (e.CommandName == CliCountry.SAHI.Comun.Utilidades.Global.SELECCIONAR)
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorCondicionTarifa = Convert.ToInt32(grvCondicionesTarifas.DataKeys[indiceFila].Value);

                var resultado = WebService.Facturacion.ConsultarTarifas(
                    new Paginacion<CondicionTarifa>()
                {
                    PaginaActual = 0,
                    LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                    Item = new CondicionTarifa()
                    {
                        CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                        Id = identificadorCondicionTarifa,
                        IndHabilitado = Convert.ToInt16(chkActivo.Checked),
                        Tipo = Properties.Settings.Default.CondicionesTarifa_Tipo,
                        Maestras = new Maestras()
                        {
                            IdPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesTarifa)
                        }
                    }
                });

                if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                {
                    mltvBuscarCT.SetActiveView(vCrearCT);
                    ucDefinirCondicionesTarifa.LimpiarControles();
                    ucDefinirCondicionesTarifa.CondicionTarifa = resultado.Objeto.Item.FirstOrDefault();
                    ucDefinirCondicionesTarifa.CondicionTarifa.NombreContrato = txtContratoDC.Text;
                    ucDefinirCondicionesTarifa.CondicionTarifa.NombreTercero = txtEntidadDC.Text;
                    ucDefinirCondicionesTarifa.CondicionTarifa.NombrePlan = txtPlanDC.Text;
                    ucDefinirCondicionesTarifa.CargarControles(Global.TipoOperacion.MODIFICACION);
                }
            }
        }

        /// <summary>
        /// Metodo para realizar la Busqueda
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
        /// Metodo para realizar la Creacion de la condicion de Tarifa
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 31/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgNuevo_Click(object sender, ImageClickEventArgs e)
        {
            RecargarModal();
            mltvBuscarCT.SetActiveView(vCrearCT);
            ucDefinirCondicionesTarifa.LimpiarControles();

            ucDefinirCondicionesTarifa.CondicionTarifa = new CondicionTarifa()
            {
                NombrePlan = VinculacionSeleccionada.Plan.Nombre,
                NombreTercero = VinculacionSeleccionada.Tercero.Nombre,
                NombreContrato = VinculacionSeleccionada.Contrato.Nombre,
                IdAtencion = VinculacionSeleccionada.IdAtencion,
                IdTercero = VinculacionSeleccionada.Tercero.Id,
                IdPlan = VinculacionSeleccionada.Plan.Id,
                IdContrato = VinculacionSeleccionada.Contrato.Id
            };

            ucDefinirCondicionesTarifa.CargarControles(Global.TipoOperacion.CREACION);
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
            pagControl.PageIndexChanged -= PagControl_PageIndexChanged;
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
            ucDefinirCondicionesTarifa.OperacionEjecutada += DefinirCondicionesTarifa_OperacionEjecutada;
            pagControl.PageIndexChanged += PagControl_PageIndexChanged;
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
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usu.ario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
                DdlComponente.DataTextField = "NombreComponente";
                DdlComponente.DataValueField = "CodigoComponente";
                DdlComponente.DataSource = resultado.Objeto;
                DdlComponente.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlComponente, false);
            }
            else
            {
                DdlComponente.DataSource = null;
                DdlComponente.DataBind();
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
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
        private void CargarPaginador(Paginacion<List<CondicionTarifa>> resultado)
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
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new CondicionTarifa()
                {
                    Componente = DdlComponente.SelectedIndex > 0 ? DdlComponente.SelectedValue : string.Empty,
                    CodigoEntidad = Properties.Settings.Default.General_CodigoEntidad,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IdTercero = VinculacionSeleccionada.Tercero.Id,
                    IdAtencion = !string.IsNullOrEmpty(txtAtencionDC.Text) ? Convert.ToInt32(txtAtencionDC.Text) : 0,
                    IndHabilitado = Convert.ToInt16(chkActivo.Checked),
                    IdTipoRelacion = DdlTipoRelacion.SelectedIndex > 0 ? Convert.ToInt16(DdlTipoRelacion.SelectedValue) : (short)0,
                    Tipo = Properties.Settings.Default.CondicionesTarifa_Tipo,
                    NombreProducto = txtProdTar.Text,
                    NombreGrupoProducto = txtGrupoProdTar.Text,
                    NombreTipoProducto = txtTipoProductoTar.Text,
                    Maestras = new Maestras()
                    {
                        IdPagina = Convert.ToInt32(Resources.CargueCombos.PaginaCombo_CondicionesTarifa)
                    }
                }
            };

            return consulta;
        }

        /// <summary>
        /// Metodo para controlar la ejecucion de la operacion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 07/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void DefinirCondicionesTarifa_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            CargarGrilla(0);
            mltvBuscarCT.SetActiveView(vDefinirCondicionesTarifa);
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}