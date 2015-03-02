// --------------------------------
// <copyright file="UC_MostrarConceptos.ascx.cs" company="InterGrupo S.A.">
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

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_MostrarConceptos
    /// </summary>
    public partial class UC_MostrarConceptos : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CONCEPTOABONO
        /// </summary>
        private const string CONCEPTOABONO = "ABON";

        /// <summary>
        /// The CUENTASELECCIONADO
        /// </summary>
        private const string CUENTASELECCIONADO = "EstadoCuentaSeleccionado";

        /// <summary>
        /// The IDCONCEPTO
        /// </summary>
        private const string IDCONCEPTO = "IdConcepto";

        /// <summary>
        /// The VALORCONCEPTO
        /// </summary>
        private const string VALORCONCEPTO = "txtValorConcepto";

        #endregion Constantes 
        #region Variables 

        /// <summary>
        /// The estado cuenta.
        /// </summary>
        private EstadoCuentaEncabezado estadoCuenta = new EstadoCuentaEncabezado();

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado para seleccionar el valor del deposito a cruzar
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnGuardarDeposito(EventoControles<ConceptoCobro> e);

        /// <summary>
        /// Delegado Para Seleccionar el Cliente
        /// </summary>
        /// <param name="e">The e.</param>
        public delegate void OnGuardarItemGrid(EventoControles<List<ConceptoCobro>> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on guardar deposito
        /// </summary>
        public event OnGuardarDeposito GuardarDepositos;

        /// <summary>
        /// Evento de on seleccionar item
        /// </summary>
        public event OnGuardarItemGrid GuardarItemGrid;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece estado cuenta seleccionado
        /// </summary>
        public EstadoCuentaEncabezado EstadoCuentaSeleccionado
        {
            get
            {
                return ViewState[CUENTASELECCIONADO] == null ? new EstadoCuentaEncabezado() : ViewState[CUENTASELECCIONADO] as EstadoCuentaEncabezado;
            }

            set
            {
                ViewState[CUENTASELECCIONADO] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo de Cargar Conceptos
        /// </summary>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm
        /// FechaDeCreacion: 26/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void CargarConceptos()
        {
            List<ConceptoCobro> lstAtClTemporal = EstadoCuentaSeleccionado.AtencionActiva != null ? EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto.CopiarObjeto() : null;

            estadoCuenta = EstadoCuentaSeleccionado;

            if (estadoCuenta != null)
            {
                if (estadoCuenta.AtencionActiva != null)
                {
                    CargarConceptos(estadoCuenta.AtencionActiva);

                    if (lstAtClTemporal != null)
                    {
                        if (estadoCuenta.AtencionActiva.Deposito.Concepto.Count < lstAtClTemporal.Count)
                        {
                            estadoCuenta.AtencionActiva.Deposito.Concepto = lstAtClTemporal.CopiarObjeto();
                            EstadoCuentaSeleccionado.AtencionActiva.Deposito.Concepto = lstAtClTemporal.CopiarObjeto();
                        }
                    }

                    List<Vinculacion> vinculaciones = estadoCuenta.AtencionActiva.Vinculacion;

                    if (vinculaciones == null)
                    {
                        return;
                    }

                    if (estadoCuenta.AtencionActiva.Deposito != null)
                    {
                        List<ConceptoCobro> lstConceptos = estadoCuenta.AtencionActiva.Deposito.Concepto;
                        List<ContratoPlan> conPlan = new List<ContratoPlan>();

                        foreach (Vinculacion item in vinculaciones)
                        {
                            conPlan.Add(new ContratoPlan
                            {
                                Plan = item.Plan,
                                Contrato = item.Contrato,
                                Tercero = item.Tercero
                            });
                        }

                        var query = from lc in lstConceptos
                                    join cp in conPlan
                                    on lc.IdPlan equals cp.Plan.Id
                                    where lc.IdContrato == estadoCuenta.IdContrato
                                    select new
                                    {
                                        IndConcepto = lc.IndHabilitado == 1 ? true : false,
                                        lc.IdConcepto,
                                        lc.Activo,
                                        lc.IdAtencion,
                                        lc.IdContrato,
                                        Contrato = cp.Contrato.Nombre,
                                        lc.IdPlan,
                                        Plan = cp.Plan.Nombre,
                                        Tercero = cp.Tercero.Nombre,
                                        ValorConcepto = lc.ValorConcepto
                                    };

                        txtDeposito.Text = estadoCuenta.AtencionActiva.Deposito.TotalDeposito.ToString();
                        grvConceptos.DataSource = query.ToList();
                        grvConceptos.DataBind();

                        if (estadoCuenta.IdPlan == 482)
                        {
                            foreach (GridViewRow row in grvConceptos.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    TextBox txtConcepto = row.FindControl("txtValorConcepto") as TextBox;

                                    if (txtConcepto != null)
                                    {
                                        txtConcepto.Enabled = false;
                                    }
                                }
                            }
                        }

                        if (query.Count() > 0)
                        {
                            imgGuardar.Enabled = true;
                        }
                        else
                        {
                            imgGuardar.Enabled = false;
                        }
                    }
                }
                else
                {
                    imgGuardar.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Método de inactivación de comandos.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\IVAN J
        /// FechaDeCreacion: 28/01/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void InhabilitarComandosConceptos(EstadoCuentaEncabezado estadoCuenta)
        {
            if (estadoCuenta.IdPlan == 482)
            {
                if (!string.IsNullOrEmpty(txtDeposito.Text) && Convert.ToDecimal(txtDeposito.Text) > 0)
                {
                    divCruzarDepositos.Visible = true;
                }

                divGuardarConceptos.Visible = false;
            }
            else
            {
                divCruzarDepositos.Visible = false;
                divGuardarConceptos.Visible = true;
            }
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Cargar conceptos y depositos
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez
        /// FechaDeCreacion: 18/02/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void CargarConceptos(AtencionCliente atencion)
        {
            var resultadoDepositos = WebService.Facturacion.ConsultarDepositos(
                   new Atencion()
                   {
                       IdAtencion = atencion.IdAtencion
                   });

            if (resultadoDepositos.Ejecuto)
            {
                atencion.Deposito = resultadoDepositos.Objeto.FirstOrDefault();

                if (atencion.Deposito == null)
                {
                    atencion.Deposito = new Deposito()
                    {
                        TotalDeposito = 0
                    };
                }

                var resultadoConceptos = WebService.Facturacion.ConsultarConceptos(
                new Atencion()
                {
                    IdAtencion = atencion.IdAtencion
                });

                if (resultadoConceptos.Ejecuto)
                {
                    if (atencion.Deposito != null)
                    {
                        atencion.Deposito.Concepto = resultadoConceptos.Objeto.ToList();
                    }
                }
                else
                {
                    MostrarMensaje(resultadoConceptos.Mensaje, TipoMensaje.Error);
                }
            }
            else
            {
                MostrarMensaje(resultadoDepositos.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// CrearConceptoCobro OperacionEjecutada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 27/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void CrearConceptoCobro_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            if (tipoOperacion == Global.TipoOperacion.CREACION)
            {
                MostrarMensaje(Resources.GlobalWeb.ConceptoCobro_MsjCreacion, TipoMensaje.Ok);
                CargarConceptos();

                GuardarItemGrid(new EventoControles<List<ConceptoCobro>>(this, estadoCuenta.AtencionActiva.Deposito.Concepto));
            }
        }

        /// <summary>
        /// ImgAgregarConcepto Click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 27/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgAgregarConcepto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ValidarIngresoVinculacionConcepto())
            {
                estadoCuenta = EstadoCuentaSeleccionado;

                if (EstadoCuentaSeleccionado.IdContrato == 473 && EstadoCuentaSeleccionado.IdPlan == 482 && estadoCuenta.AtencionActiva.Deposito.TotalDeposito == 0)
                {
                    MostrarMensaje(Resources.ControlesUsuario.ConceptoCobro_SinDeposito, TipoMensaje.Error);
                }
                else
                {
                    ucCrearConceptoCobro.LimpiarCampos();
                    ucCrearConceptoCobro.CargarInformacionEstadoCuenta(EstadoCuentaSeleccionado);
                    ucCrearConceptoCobro.EstadoCuentaSeleccionado = EstadoCuentaSeleccionado;
                    mpeCrearConcepto.Show();
                }
            }
            else
            {
                MostrarMensaje(Resources.GlobalWeb.ConceptoCobro_MsjErrorCrear, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Realiza el cruce de un depósito para un particular.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 18/09/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgCruzarDeposito_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeposito.Text) && !string.IsNullOrEmpty(txtDepositoCruzar.Text))
            {
                var estadoCuenta = EstadoCuentaSeleccionado;
                if (estadoCuenta != null && estadoCuenta.ValorTotalFacturado > 0)
                {
                    if (Convert.ToDecimal(txtDepositoCruzar.Text) > Convert.ToDecimal(txtDeposito.Text))
                    {
                        MostrarMensaje(Resources.GlobalWeb.Cruzar_Depositos_Mensaje, TipoMensaje.Error);
                        txtDepositoCruzar.Text = txtDeposito.Text;
                    }
                    else if (Convert.ToDecimal(txtDepositoCruzar.Text) > estadoCuenta.ValorTotalFacturado)
                    {
                        MostrarMensaje(Resources.GlobalWeb.Cruzar_Depositos_Valor_Mensaje, TipoMensaje.Error);
                        txtDepositoCruzar.Text = Math.Round(estadoCuenta.ValorTotalFacturado, 2).ToString();
                    }
                    else
                    {
                        decimal sumatoria = 0;

                        if (estadoCuenta.AtencionActiva != null
                            && estadoCuenta.AtencionActiva.Deposito != null
                            && estadoCuenta.AtencionActiva.Deposito.Concepto != null
                            && estadoCuenta.AtencionActiva.Deposito.Concepto.Count > 0)
                        {
                            sumatoria = (from p in estadoCuenta.AtencionActiva.Deposito.Concepto
                                         where p.IndHabilitado == 1 && p.DepositoParticular == false
                                         select p.ValorConcepto).Sum();
                        }

                        if (Convert.ToDecimal(txtDeposito.Text) - sumatoria <= 0)
                        {
                            MostrarMensaje(string.Format(Resources.GlobalWeb.Depositos_Cruzar_Total_Mensaje, sumatoria.ToString()), TipoMensaje.Error);
                            txtDepositoCruzar.Text = "0";
                        }
                        else if (Convert.ToDecimal(txtDepositoCruzar.Text) > (Convert.ToDecimal(txtDeposito.Text) - sumatoria))
                        {
                            MostrarMensaje(string.Format(Resources.GlobalWeb.Depositos_Cruzar_Parcial_Mensaje, sumatoria, (Convert.ToDecimal(txtDeposito.Text) - sumatoria).ToString()), TipoMensaje.Error);
                            txtDepositoCruzar.Text = (Convert.ToDecimal(txtDeposito.Text) - sumatoria).ToString();
                        }
                        else
                        {
                            ConceptoCobro depositoParticular = new ConceptoCobro()
                                                 {
                                                     Activo = true,
                                                     IndHabilitado = 1,
                                                     IdAtencion = estadoCuenta.AtencionActiva.IdAtencion,
                                                     IdContrato = estadoCuenta.IdContrato,
                                                     IdPlan = estadoCuenta.IdPlan,
                                                     NumeroFactura = 0,
                                                     ValorConcepto = Convert.ToDecimal(txtDepositoCruzar.Text),
                                                     ValorSaldo = Convert.ToDecimal(txtDepositoCruzar.Text),
                                                     CodigoConcepto = CONCEPTOABONO,
                                                     TotalConcepto = Convert.ToDecimal(txtDepositoCruzar.Text),
                                                     DepositoParticular = true
                                                 };
                            GuardarDepositos(new EventoControles<ConceptoCobro>(this, depositoParticular));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Guardar cambios en el valor de los conceptos
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 06/11/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            estadoCuenta = EstadoCuentaSeleccionado;
            List<ConceptoCobro> lstConceptos = estadoCuenta.AtencionActiva.Deposito.Concepto.ToList();

            var listaConceptos = from fila in grvConceptos.Rows.Cast<GridViewRow>()
                                 select new FacturaAtencionConceptoCobro()
                                 {
                                     Actualizar = true,
                                     IndHabilitado = (fila.Cells[0].FindControl("chkAplicarConcepto") as CheckBox).Checked ? Convert.ToInt16(1) : Convert.ToInt16(0),
                                     IdAtencionConcepto = Convert.ToInt32((fila.Cells[1].FindControl("lblIdConcepto") as Label).Text),
                                     IdAtencion = Convert.ToInt32((fila.Cells[2].FindControl("lblIdAtencion") as Label).Text),
                                     NumeroFactura = 0,
                                     ValorConcepto = Convert.ToDecimal((fila.Cells[6].FindControl("txtValorConcepto") as TextBox).Text),
                                     ValorSaldo = Convert.ToDecimal((fila.Cells[6].FindControl("txtValorConcepto") as TextBox).Text),
                                     CodigoConcepto = CONCEPTOABONO
                                 };

            var contratoParticular = from registro in listaConceptos
                                     join contrato in lstConceptos
                                     on registro.IdAtencion equals contrato.IdAtencion
                                     where registro.ValorConcepto > Convert.ToDecimal(txtDeposito.Text)
                                     && contrato.IdConcepto == registro.IdAtencionConcepto
                                     && contrato.IdContrato == 473
                                     && contrato.IdPlan == 482
                                     select contrato;

            estadoCuenta.TotalPagos = (from total in listaConceptos
                                       where total.IndHabilitado == 1
                                       select total.ValorConcepto).Sum();

            foreach (var item in estadoCuenta.FacturaAtencion)
            {
                if (listaConceptos != null && listaConceptos.Count() > 0)
                {
                    item.ConceptosCobro = new List<FacturaAtencionConceptoCobro>();
                    item.ConceptosCobro = listaConceptos.ToList();
                }
            }

            if (contratoParticular.Count() > 0)
            {
                MostrarMensaje(Resources.GlobalWeb.ConceptoCobro_MsjValorConcepto, TipoMensaje.Error);
            }
            else
            {
                foreach (var item in listaConceptos)
                {
                    WebService.Facturacion.ActualizarConceptosCobro(item);

                    foreach (var itemConcepto in lstConceptos)
                    {
                        if (itemConcepto.IdConcepto == item.IdAtencionConcepto)
                        {
                            itemConcepto.ValorConcepto = item.ValorConcepto;
                            itemConcepto.TotalConcepto = item.ValorConcepto;
                            itemConcepto.IndHabilitado = item.IndHabilitado;
                        }
                    }
                }

                if (lstConceptos.Count > 0)
                {
                    GuardarItemGrid(new EventoControles<List<ConceptoCobro>>(this, lstConceptos));
                }
            }
        }

        /// <summary>
        /// On Init.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 27/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucCrearConceptoCobro.OperacionEjecutada += CrearConceptoCobro_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Se ejecuta cuando se carga la página.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm
        /// FechaDeCreacion: 26/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Valida si la atención es apta para generar concepto de cobro.
        /// </summary>
        /// <returns>true si no existen conceptos generados.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 27/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private bool ValidarIngresoVinculacionConcepto()
        {
            var listaConceptos = from fila in grvConceptos.Rows.Cast<GridViewRow>()
                                 where (fila.Cells[1].FindControl("lblIdAtencion") as Label).Text == EstadoCuentaSeleccionado.IdAtencion.ToString()
                                 && (fila.Cells[6].FindControl("lblIdContrato") as Label).Text == EstadoCuentaSeleccionado.IdContrato.ToString()
                                 && (fila.Cells[7].FindControl("lblIdPlan") as Label).Text == EstadoCuentaSeleccionado.IdPlan.ToString()
                                 select new FacturaAtencionConceptoCobro()
                                 {
                                     IdAtencionConcepto = Convert.ToInt32((fila.Cells[0].FindControl("lblIdConcepto") as Label).Text),
                                     IdAtencion = Convert.ToInt32((fila.Cells[1].FindControl("lblIdAtencion") as Label).Text),
                                     NumeroFactura = 0,
                                     ValorConcepto = Convert.ToDecimal((fila.Cells[5].FindControl("txtValorConcepto") as TextBox).Text),
                                     ValorSaldo = Convert.ToDecimal((fila.Cells[5].FindControl("txtValorConcepto") as TextBox).Text)
                                 };

            if (listaConceptos.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}