// --------------------------------
// <copyright file="UC_BuscarCondicionesCubrimiento.ascx.cs" company="InterGrupo S.A.">
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
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarCondicionesCubrimiento.
    /// </summary>
    public partial class UC_BuscarCondicionesCubrimiento : WebUserControl
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
            fsResultado.Visible = false;
            txtEntidad.Text = string.Empty;
            txtIdAtencion.Text = string.Empty;
            txtContrato.Text = string.Empty;
            txtPlan.Text = string.Empty;
            fsResultado.Visible = false;
            grvCondicionesCubrimientos.DataSource = null;
            grvCondicionesCubrimientos.DataBind();
            mltvCC.SetActiveView(vConsultaCC);
        }

        /// <summary>
        /// Método que realiza el precargue de la información de la página de definir cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void PrecargueInformacion(CondicionCubrimiento condicionCubrimiento)
        {
            lblIdEntidad.Text = condicionCubrimiento.Cubrimiento.Tercero.Id.ToString();
            txtEntidad.Text = condicionCubrimiento.Cubrimiento.Tercero.Nombre;
            lblIdContrato.Text = condicionCubrimiento.Cubrimiento.Contrato.Id.ToString();
            txtContrato.Text = condicionCubrimiento.Cubrimiento.Contrato.Nombre;
            lblIdPlan.Text = condicionCubrimiento.Cubrimiento.Plan.Id.ToString();
            txtPlan.Text = VisualizarConfiguracion ? string.Empty : condicionCubrimiento.Cubrimiento.Plan.Nombre.ToString();
            txtPlan.Enabled = VisualizarConfiguracion;
            txtIdAtencion.Enabled = VisualizarConfiguracion;
            txtIdAtencion.Text = condicionCubrimiento.Cubrimiento.IdAtencion.ToString();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Se encarga de hacer la paginacion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom 
        /// FechaDeCreacion: 21/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        protected void GrvCondicionesCubrimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(0);
            var resultado = WebService.Facturacion.ConsultarCondicionesCubrimiento(consulta);

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
                numPaginas.Text = grvCondicionesCubrimientos.PageCount.ToString() + ".";
                grvCondicionesCubrimientos.PageIndex = e.NewPageIndex;
                grvCondicionesCubrimientos.DataSource = resultado.Objeto.Item;
                grvCondicionesCubrimientos.DataBind();
                
                // CargaObjetos.OrdenamientoGrilla(this.Page, grvCondicionesCubrimientos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControlCondicionCubrimiento.Visible = false;
                LblMensaje.Visible = true;
                LblMensaje.Text = resultado.Mensaje;
                grvCondicionesCubrimientos.DataSource = null;
                grvCondicionesCubrimientos.DataBind();
            }
        }

        /// <summary>
        /// Carga la información de la grilla de condiciones y lo muestra en la pantalla detallada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCondicionesCubrimientos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == Global.SELECCIONAR || e.CommandName == Global.MODIFICAR)
            {
                var indiceFila = Convert.ToInt32(e.CommandArgument);
                var identificadorCondicionCubrimiento = Convert.ToInt32(grvCondicionesCubrimientos.DataKeys[indiceFila].Value);
                Label numeroTipoRelacion = (Label)grvCondicionesCubrimientos.Rows[indiceFila].Cells[3].FindControl("lblIdTipoRelacion");
                Label identificadorClaseCubrimiento = (Label)grvCondicionesCubrimientos.Rows[indiceFila].Cells[5].FindControl("lblIdClaseCubrimiento");

                Paginacion<CondicionCubrimiento> paginador = new Paginacion<CondicionCubrimiento>()
                {
                    LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                    PaginaActual = 0,
                    Item = new CondicionCubrimiento()
                    {
                        Id = identificadorCondicionCubrimiento,
                        CodigoEntidad = Settings.Default.General_CodigoEntidad,
                        IdTercero = VinculacionSeleccionada.Tercero.Id,
                        IdContrato = VinculacionSeleccionada.Contrato.Id,
                        IdPlan = VinculacionSeleccionada.Plan.Id,
                        Componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue,
                        IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                        NumeroTipoRelacion = Convert.ToInt16(numeroTipoRelacion.Text),
                        VigenciaCondicion = Convert.ToDateTime(grvCondicionesCubrimientos.Rows[indiceFila].Cells[9].Text),
                        ValorPropio = Convert.ToDecimal(grvCondicionesCubrimientos.Rows[indiceFila].Cells[8].Text),
                        DescripcionCondicion = grvCondicionesCubrimientos.Rows[indiceFila].Cells[10].Text,
                        Tercero = new Tercero()
                        {
                            Id = VinculacionSeleccionada.Tercero.Id,
                            Nombre = txtEntidad.Text
                        },

                        Contrato = new Contrato()
                        {
                            Id = VinculacionSeleccionada.Contrato.Id,
                            Nombre = txtContrato.Text
                        },

                        Cubrimiento = new Cubrimiento()
                        {
                            CodigoEntidad = Settings.Default.General_CodigoEntidad,
                            IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                            IdAtencion = Convert.ToInt32(txtIdAtencion.Text),
                            IdClaseCubrimiento = Convert.ToInt32(identificadorClaseCubrimiento.Text),
                            TipoComponente = new TipoComponente(),
                            ClaseCubrimiento = new ClaseCubrimiento()
                            {
                                IdClaseCubrimiento = Convert.ToInt32(identificadorClaseCubrimiento.Text),
                            },
                            GrupoProducto = new GrupoProducto(),
                            Producto = new Producto(),
                            TipoProducto = new TipoProducto(),
                            Tercero = new Tercero()
                            {
                                Id = VinculacionSeleccionada.Tercero.Id,
                                Nombre = txtEntidad.Text
                            },

                            Contrato = new Contrato()
                            {
                                Id = VinculacionSeleccionada.Contrato.Id,
                                Nombre = txtContrato.Text
                            },
                            Plan = new Plan()
                            {
                                Id = VinculacionSeleccionada.Plan.Id,
                                Nombre = VinculacionSeleccionada.Plan.Nombre
                            }
                        },

                        IdAtencion = Convert.ToInt32(txtIdAtencion.Text)
                    }
                };

                if (e.CommandName == Global.SELECCIONAR)
                {
                    var resultado = WebService.Facturacion.ConsultarCondicionesCubrimiento(paginador);

                    if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                    {
                        RecargarModal();
                        ucCrearCondicionCubrimiento.lblTitulo.Text = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Actualizar;
                        ucCrearCondicionCubrimiento.CargarListasIniciales();
                        ucCrearCondicionCubrimiento.VisualizarConfiguracion = VisualizarConfiguracion;
                        ucCrearCondicionCubrimiento.CargarInformacionCondicionCubrimiento(resultado.Objeto.Item.FirstOrDefault());
                        ucCrearCondicionCubrimiento.ActivarCampos(Global.TipoOperacion.CONSULTA);
                        mltvCC.SetActiveView(vCrearModificarCC);
                    }
                }

                if (e.CommandName == Global.MODIFICAR)
                {
                    var resultado = WebService.Facturacion.ConsultarCondicionesCubrimiento(paginador);

                    if (resultado.Ejecuto && resultado.Objeto.TotalRegistros == 1)
                    {
                        RecargarModal();
                        ucCrearCondicionCubrimiento.lblTitulo.Text = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Actualizar;
                        ucCrearCondicionCubrimiento.CargarListasIniciales();
                        ucCrearCondicionCubrimiento.VisualizarConfiguracion = VisualizarConfiguracion;
                        ucCrearCondicionCubrimiento.CargarInformacionCondicionCubrimiento(resultado.Objeto.Item.FirstOrDefault());
                        ucCrearCondicionCubrimiento.ActivarCampos(Global.TipoOperacion.MODIFICACION);
                        mltvCC.SetActiveView(vCrearModificarCC);
                    }
                }
            }
        }

        /// <summary>
        /// Establece los checks de la grilla.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 25/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCondicionesCubrimientos_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
        /// Evento de edición de la grilla.
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
        protected void GrvCondicionesCubrimientos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Metodo para adicionar una nueva condicion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBtnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            ucCrearCondicionCubrimiento.LimpiarCampos();
            ucCrearCondicionCubrimiento.CargarListasIniciales();
            ucCrearCondicionCubrimiento.lblTitulo.Text = Resources.CondicionesCubrimientos.CondicionesCubrimientos_Crear;
            ucCrearCondicionCubrimiento.chkActivo.Checked = true;
            ucCrearCondicionCubrimiento.VisualizarConfiguracion = VisualizarConfiguracion;
            ucCrearCondicionCubrimiento.CargarInformacionCondicionCubrimiento(InformacionCondicionCubrimiento());
            ucCrearCondicionCubrimiento.ActivarCampos(Global.TipoOperacion.CREACION);
            mltvCC.SetActiveView(vCrearModificarCC);
        }

        /// <summary>
        /// Consulta las condiciones de cubrimiento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBtnConsultar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            RecargarModal();
            CargarGrillaCondiciones(0);
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
            // pagControlCondicionCubrimiento.PageIndexChanged += PagControlCC_PageIndexChanged;
            ucCrearCondicionCubrimiento.OperacionEjecutada += CrearCondicionCubrimiento_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Cambio de página.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void PagControlCC_PageIndexChanged(EventoControles<Paginador> e)
        {
            CargarGrillaCondiciones(e.Resultado.PaginaActual);
        }

        /// <summary>
        /// Se ejecuta cuando se carga la página.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 18/04/2013
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
        /// Carga las clases de cubrimiento.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 18/04/2013
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
            DdlClaseCubrimiento.SelectedIndex = 0;
        }

        /// <summary>
        /// Carga la grilla de condiciones de cubrimiento.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 19/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGrillaCondiciones(int numeroPagina)
        {
            RecargarModal();
            LblMensaje.Visible = false;
            fsResultado.Visible = true;
            var consulta = CrearObjetoConsulta(numeroPagina);
            var resultado = WebService.Facturacion.ConsultarCondicionesCubrimiento(consulta);

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
                grvCondicionesCubrimientos.DataSource = resultado.Objeto.Item;
                grvCondicionesCubrimientos.DataBind();
                numPaginas.Text = grvCondicionesCubrimientos.PageCount.ToString() + ".";
                lblNumRegistros.Text = resultado.Objeto.Item.Count.ToString();
                
                // CargaObjetos.OrdenamientoGrilla(this.Page, grvCondicionesCubrimientos, resultado.Objeto.Item);
                // CargarPaginador(resultado.Objeto);
            }
            else
            {
                // pagControlCondicionCubrimiento.Visible = false;
                LblMensaje.Visible = true;
                LblMensaje.Text = resultado.Mensaje;
                grvCondicionesCubrimientos.DataSource = null;
                grvCondicionesCubrimientos.DataBind();
            }
        }

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
            CargarClasesCubrimientos();
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
        /// Crear Condicion Cubrimiento.
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
        private void CrearCondicionCubrimiento_OperacionEjecutada(object sender, EventArgs e)
        {
            RecargarModal();
        }

        /// <summary>
        /// Crear Condicion Cubrimiento.
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
        private void CrearCondicionCubrimiento_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
            mltvCC.SetActiveView(vConsultaCC);
            CargarGrillaCondiciones(0);
        }

        /// <summary>
        /// Metodo para construir Filtros de Condiciones de Cubrimiento.
        /// </summary>
        /// <param name="numeroPagina">The numero pagina.</param>
        /// <returns>
        /// Objeto de Consulta Condicion de Cubrimiento.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<CondicionCubrimiento> CrearObjetoConsulta(int numeroPagina)
        {
            Paginacion<CondicionCubrimiento> consulta = new Paginacion<CondicionCubrimiento>()
            {
                LongitudPagina = 0, // Properties.Settings.Default.Paginacion_CantidadRegistrosPagina,
                PaginaActual = numeroPagina,
                Item = new CondicionCubrimiento()
                {
                    IndHabilitado = chkActivo.Checked ? short.Parse(Resources.GlobalWeb.General_ValorUno) : short.Parse(Resources.GlobalWeb.General_ValorCero),
                    CodigoEntidad = Settings.Default.General_CodigoEntidad,
                    IdTercero = Convert.ToInt32(lblIdEntidad.Text),
                    IdContrato = Convert.ToInt32(lblIdContrato.Text),
                    Componente = DdlComponente.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? Resources.GlobalWeb.General_ValorNA : DdlComponente.SelectedValue,
                    Tercero = new Tercero()
                    {
                        IndHabilitado = true,
                        Id = Convert.ToInt32(lblIdEntidad.Text),
                        Nombre = txtEntidad.Text
                    },
                    Contrato = new Contrato()
                    {
                        IndHabilitado = true,
                        Id = Convert.ToInt32(lblIdContrato.Text),
                        Nombre = txtContrato.Text
                    },

                    IdPlan = Convert.ToInt32(lblIdPlan.Text),
                    IdAtencion = string.IsNullOrEmpty(txtIdAtencion.Text) ? 0 : Convert.ToInt32(txtIdAtencion.Text),
                    NumeroTipoRelacion = DdlTipoRelacion.SelectedValue == Resources.GlobalWeb.General_ComboItemValor ? short.Parse("0") : short.Parse(DdlTipoRelacion.SelectedValue),
                    Cubrimiento = new Cubrimiento()
                    {
                        IdContrato = Convert.ToInt32(lblIdEntidad.Text),
                        IdPlan = Convert.ToInt32(lblIdPlan.Text),
                        IdClaseCubrimiento = Convert.ToInt32(DdlClaseCubrimiento.SelectedValue),
                        Tercero = new Tercero()
                        {
                            IndHabilitado = true,
                            Id = Convert.ToInt32(lblIdEntidad.Text),
                            Nombre = txtEntidad.Text
                        },
                        Contrato = new Contrato()
                        {
                            IndHabilitado = true,
                            Id = Convert.ToInt32(lblIdContrato.Text),
                            Nombre = txtContrato.Text
                        },
                        Plan = new Plan()
                        {
                            Id = Convert.ToInt32(lblIdPlan.Text),
                            Nombre = txtPlan.Text
                        },

                        IdAtencion = string.IsNullOrEmpty(txtIdAtencion.Text) ? 0 : Convert.ToInt32(txtIdAtencion.Text),

                        TipoComponente = new TipoComponente(),
                        ClaseCubrimiento = new ClaseCubrimiento(),
                        GrupoProducto = new GrupoProducto(),
                        Producto = new Producto(),
                        TipoProducto = new TipoProducto()
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
        private CondicionCubrimiento InformacionCondicionCubrimiento()
        {
            var cubrimiento = new CondicionCubrimiento()
            {
                IndHabilitado = chkActivo.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0),

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

                Cubrimiento = new Cubrimiento()
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

                    Plan = new Plan()
                    {
                        Id = Convert.ToInt32(lblIdPlan.Text),
                        Nombre = txtPlan.Text
                    },

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

                    IdAtencion = Convert.ToInt32(txtIdAtencion.Text)
                }
            };

            return cubrimiento;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}