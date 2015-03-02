// --------------------------------
// <copyright file="UC_BuscarExclusiones.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarExclusiones.
    /// </summary>
    public partial class UC_BuscarExclusiones : WebUserControl
    {
        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Se encarga del cargue inicial de la grilla de exclusiones.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
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
            var resultado = WebService.Facturacion.ConsultarExclusionesAtencionContrato(consulta);

            if (resultado.Ejecuto)
            {
                resultado.Objeto.TotalRegistros = resultado.Objeto.Item.Count();
                CargarPaginador(resultado.Objeto);
                CargaObjetos.OrdenamientoGrilla(this.Page, grvExclusiones, resultado.Objeto.Item.Where(c => c.IdAtencion == VinculacionSeleccionada.IdAtencion));
            }
            else
            {
                pagControlExclusiones.Visible = false;
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                grvExclusiones.DataSource = null;
                grvExclusiones.DataBind();
            }
        }

        /// <summary>
        /// Carga los componentes.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 18/04/2013
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
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos()
        {
            LblMensaje.Visible = false;
            fsResultado.Visible = false;
            txtEntidad.Text = string.Empty;
            txtIdAtencion.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtPlan.Text = string.Empty;
            grvExclusiones.DataSource = null;
            grvExclusiones.DataBind();
        }

        /// <summary>
        /// Método que realiza el precargue de la información de la página de definir exclusiones.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void PrecargueInformacion()
        {
            lblIdEntidad.Text = VinculacionSeleccionada.Tercero.Id.ToString();
            txtEntidad.Text = VinculacionSeleccionada.Tercero.Nombre;
            lblIdContrato.Text = VinculacionSeleccionada.Contrato.Id.ToString();
            txtContrato.Text = VinculacionSeleccionada.Contrato.Nombre;
            lblIdPlan.Text = VinculacionSeleccionada.Plan.Id.ToString();
            txtPlan.Text = VinculacionSeleccionada.Plan.Nombre.ToString();
            txtIdAtencion.Text = VinculacionSeleccionada.IdAtencion.ToString();
            mltvDE.SetActiveView(vConsultaDE);
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Se ejecuta al seleccionar un registro de la grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvExclusiones_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            RecargarModal();
            if (e.CommandName == Global.SELECCIONAR)
            {
                var indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorCubrimiento = Convert.ToInt32(grvExclusiones.DataKeys[indiceFila].Value);
                CheckBox chkEsActivo = (CheckBox)grvExclusiones.Rows[indiceFila].Cells[1].FindControl("chkActivo");
                Label lblExclusion = (Label)grvExclusiones.Rows[indiceFila].Cells[2].FindControl("lblIdExclusion");
                Label lblTipoProducto = (Label)grvExclusiones.Rows[indiceFila].Cells[3].FindControl("lblIdTipoProducto");
                Label lblGrupoProducto = (Label)grvExclusiones.Rows[indiceFila].Cells[5].FindControl("lblIdGrupoProducto");
                Label lblProducto = (Label)grvExclusiones.Rows[indiceFila].Cells[7].FindControl("lblIdProducto");
                Label lblVenta = (Label)grvExclusiones.Rows[indiceFila].Cells[12].FindControl("lblIdVenta");
                Label lblNumeroVenta = (Label)grvExclusiones.Rows[indiceFila].Cells[13].FindControl("lblNumeroVenta");
                Label lblManual = (Label)grvExclusiones.Rows[indiceFila].Cells[14].FindControl("lblIdManual");

                Paginacion<ExclusionContrato> paginador = new Paginacion<ExclusionContrato>()
                {
                    LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                    PaginaActual = 0,
                    Item = new ExclusionContrato()
                    {
                        CodigoEntidad = Settings.Default.General_CodigoEntidad,
                        IndicadorContratoActivo = chkEsActivo.Checked ? (short)1 : (short)0,
                        Id = Convert.ToInt32(lblExclusion.Text),
                        IdTipoProducto = Convert.ToInt16(lblTipoProducto.Text),
                        TipoProducto = new TipoProducto()
                        {
                            IdTipoProducto = Convert.ToInt16(lblTipoProducto.Text),
                            Nombre = grvExclusiones.Rows[indiceFila].Cells[4].Text
                        },
                        GrupoProducto = new GrupoProducto()
                        {
                            IdGrupo = Convert.ToInt32(lblGrupoProducto.Text),
                            Nombre = Server.HtmlDecode(grvExclusiones.Rows[indiceFila].Cells[6].Text)
                        },
                        Producto = new Producto()
                        {
                            IdProducto = Convert.ToInt32(lblProducto.Text),
                            Nombre = Server.HtmlDecode(grvExclusiones.Rows[indiceFila].Cells[8].Text)
                        },
                        Componente = grvExclusiones.Rows[indiceFila].Cells[9].Text,
                        IdManual = Convert.ToInt32(lblManual.Text),
                        VigenciaTarifa = Convert.ToDateTime(grvExclusiones.Rows[indiceFila].Cells[11].Text),
                        IdVenta = Convert.ToInt32(lblVenta.Text),
                        NumeroVenta = Convert.ToInt32(lblNumeroVenta.Text),
                        NombreManual = grvExclusiones.Rows[indiceFila].Cells[10].Text
                    }
                };

                RecargarModal();
                ucCrearExclusion.LimpiarCampos();
                ucCrearExclusion.CargarTiposdeProducto();
                ucCrearExclusion.lblTitulo.Text = Resources.DefinirExclusiones.DefinirExclusiones_Actualizar;
                ucCrearExclusion.CargarInformacionExclusion(paginador.Item);
                mltvDE.SetActiveView(vCrearModificarDE);
            }
        }

        /// <summary>
        /// Metodo de Carga del Grid de Exclusiones
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvExclusiones_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        /// Llama a la pantalla de creación de nueva exclusión.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgAdmExclusion_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ucCrearExclusion.LimpiarCampos();
            ucCrearExclusion.CargarTiposdeProducto();
            ucCrearExclusion.CargarTiposComponentes();
            ucCrearExclusion.lblTitulo.Text = Resources.DefinirExclusiones.DefinirExclusiones_Crear;
            ucCrearExclusion.chkActivo.Checked = true;
            ucCrearExclusion.CargarInformacionExclusion(InformacionExclusion());
            mltvDE.SetActiveView(vCrearModificarDE);
        }

        /// <summary>
        /// Se ejecuta cuando de da click para mostrar grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
            pagControlExclusiones.PageIndexChanged += PagControl_PageIndexChanged;
            ucCrearExclusion.OperacionEjecutada += CrearExclusion_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Se ejecuta cuando se carga la página.
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
            if (grvExclusiones.Rows.Count > 0)
            {
                grvExclusiones.UseAccessibleHeader = true;
                grvExclusiones.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvExclusiones.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Carga los listado de manera inicial.
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
            CargarTiposRelacion();
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
        private void CargarPaginador(Paginacion<List<Exclusion>> resultado)
        {
            pagControlExclusiones.ObjetoPaginador = new Paginador()
            {
                CantidadPaginas = resultado.CantidadPaginas,
                LongitudPagina = resultado.LongitudPagina,
                MaximoPaginas = Properties.Settings.Default.Paginacion_CantidadBotones,
                PaginaActual = resultado.PaginaActual,
                TotalRegistros = resultado.TotalRegistros
            };
        }

        /// <summary>
        /// Carga los tipos de relación.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarTiposRelacion()
        {
            int identificadorPagina = Settings.Default.CondicionCubrimiento_IdPagina;
            int identificadorMaestra = Settings.Default.CondicionCubrimiento_IdMaestra;
            DdlTipoRelacion.DataSource = WebService.Facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina).Objeto;
            DdlTipoRelacion.DataValueField = Resources.GlobalWeb.General_ValorMaestroDetalle;
            DdlTipoRelacion.DataTextField = Resources.GlobalWeb.General_NombreMaestroDetalle;
            DdlTipoRelacion.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(DdlTipoRelacion, false);
        }

        /// <summary>
        /// Crear exclusión.
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
        private void CrearExclusion_OperacionEjecutada(object sender, EventArgs e)
        {
            RecargarModal();
        }

        /// <summary>
        /// Operacion ejecutada.
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
        private void CrearExclusion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvDE.SetActiveView(vConsultaDE);
            CargarGrilla(0);
        }

        /// <summary>
        /// Metodo para obtener objeto de consulta de exclusiones.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Objeto de COnsulta.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<Exclusion> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<Exclusion> consulta = new Paginacion<Exclusion>()
            {
                PaginaActual = numeroPagina,
                LongitudPagina = Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                Item = new Exclusion()
                {
                    CodigoEntidad = Settings.Default.General_CodigoEntidad,
                    IdTercero = VinculacionSeleccionada.Tercero.Id,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IdAtencion = VinculacionSeleccionada.IdAtencion,
                    IndicadorContratoActivo = chkActivo.Checked ? (short)1 : (short)0,
                    NombreTipoProducto = txtTipoProducto.Text,
                    NombreGrupoProducto = txtGrupoProducto.Text,
                    NombreProducto = txtProducto.Text
                }
            };

            return consulta;
        }

        /// <summary>
        /// Carga información de la exclusión.
        /// </summary>
        /// <returns>Retorna Exclusión.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 30/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private ExclusionContrato InformacionExclusion()
        {
            var exclusion = new ExclusionContrato()
            {
                IdTipoProducto = 0,
                TipoProducto = new TipoProducto()
                {
                    IdTipoProducto = 0,
                    Nombre = string.Empty
                },
                GrupoProducto = new GrupoProducto()
                {
                    IdGrupo = 0,
                    Nombre = string.Empty
                },
                Producto = new Producto()
                {
                    IdProducto = 0,
                    Nombre = string.Empty
                },
                IdVenta = 0,
                Componente = Resources.GlobalWeb.General_ValorNA                
            };

            object[] infoManual = ObtenerInformacionManual(VinculacionSeleccionada.Contrato.Id);

            exclusion.IdManual = Convert.ToInt32(infoManual[0]);
            exclusion.NombreManual = Convert.ToString(infoManual[1]);
            exclusion.VigenciaTarifa = Convert.ToDateTime(infoManual[2]);

            return exclusion;
        }

        /// <summary>
        /// Consulta la información del manual dado el identificador del contrato.
        /// </summary>
        /// <param name="identificadorContrato">Identificador del Contrato presente en la vinculación seleccionada.</param>
        /// <returns>Objeto con el id del manual y el nombre del manual.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO/gocampo.
        /// FechaDeCreacion: 21/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private object[] ObtenerInformacionManual(int identificadorContrato)
        {
            object[] respuesta = new object[3];

            // Consulta las datos básicos de un manual dado el id del contrato
            var resultado = WebService.Facturacion.ConsultarManualesBasicosContrato(identificadorContrato);

            if (resultado.Ejecuto)
            {
                if (resultado.Objeto.Rows.Count > 0)
                {
                    respuesta[0] = resultado.Objeto.Rows[0]["IdManual"];
                    respuesta[1] = resultado.Objeto.Rows[0]["NombreManual"];
                    respuesta[2] = resultado.Objeto.Rows[0]["Vigencia"];
                }
                else
                {
                    respuesta[0] = 0;
                    respuesta[1] = String.Empty;
                    respuesta[2] = DateTime.Now;
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Pags the control_ page index changed.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 29/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void PagControl_PageIndexChanged(EventoControles<Paginador> e)
        {
            RecargarModal();
            CargarGrilla(e.Resultado.PaginaActual);
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}