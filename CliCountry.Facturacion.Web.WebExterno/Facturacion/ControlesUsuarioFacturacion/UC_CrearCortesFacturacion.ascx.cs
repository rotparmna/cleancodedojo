// --------------------------------
// <copyright file="UC_CrearCortesFacturacion.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCortesFacturacion.
    /// </summary>
    public partial class UC_CrearCortesFacturacion : WebUserControl
    {
        #region Declaraciones Locales

        #region Constantes

        /// <summary>
        /// The CHECKSELECCIONAR
        /// </summary>
        private const string CHECKSELECCIONAR = "chkSeleccionar";

        /// <summary>
        /// The CORTES
        /// </summary>
        private const string CORTES = "Cortes";

        #endregion Constantes

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece fecha minima
        /// </summary>
        public DateTime FechaMinima { get; set; }

        /// <summary>
        /// Id de la Atencion
        /// </summary>
        public int IdAtencion { get; set; }

        /// <summary>
        /// The lista corte
        /// </summary>
        public List<CorteFacturacion> ListaCorte
        {
            get
            {
                return ViewState[CORTES] as List<CorteFacturacion>;
            }

            set
            {
                ViewState[CORTES] = value;
            }
        }

        #endregion Propiedades Publicas

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Metodo para Limpiar Lista CorteFacturacion y el GridView
        /// </summary>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarListaCorteFacturacion()
        {
            ListaCorte.Clear();
            CargaObjetos.OrdenamientoGrilla(this.Page, grvCortesFacturacion, ListaCorte);
        }

        /// <summary>
        ///  Metodo para Obtener una Lista CorteFacturacion Activos
        /// </summary>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public List<CorteFacturacion> ObtenerListaCorteActivo()
        {
            var items = from
                           fila in grvCortesFacturacion.Rows.Cast<GridViewRow>()
                        where
                            (fila.FindControl(CHECKSELECCIONAR) as CheckBox).Checked
                        select
                            new CorteFacturacion()
                            {
                                Activo = (fila.FindControl(CHECKSELECCIONAR) as CheckBox).Checked,
                                FechaInicial = Convert.ToDateTime(fila.Cells[2].Text),
                                FechaFinal = Convert.ToDateTime(fila.Cells[3].Text)
                            };

            return items.ToList();
        }

        #endregion Metodos Publicos

        #region Metodos Protegidos

        /// <summary>
        /// Borrar registro de la grilla
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void GrvCortesFacturacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            RecargarModal();
            ListaCorte.RemoveAt(e.RowIndex);
            CargaObjetos.OrdenamientoGrilla(this.Page, grvCortesFacturacion, ListaCorte);
        }

        /// <summary>
        /// Boton para adicionar registros a la grilla de cortes de facturación
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBtnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            lblErrorFecha.Visible = false;
            RecargarModal();

            if (Convert.ToDateTime(txtFechaFinal.Text) >= Convert.ToDateTime(txtFechaInicial.Text) && Convert.ToDateTime(txtFechaInicial.Text) >= FechaMinima)
            {
                var corteFacturacion = new CorteFacturacion()
                {
                    Activo = bool.Parse(Resources.GlobalWeb.General_IndHabilitado),
                    FechaInicial = Convert.ToDateTime(txtFechaInicial.Text),
                    FechaFinal = Convert.ToDateTime(txtFechaFinal.Text)
                };

                if (grvCortesFacturacion.Rows.Count > 0)
                {
                    var resultado = (from
                                         item in ListaCorte
                                     where
                                         (item.FechaInicial >= corteFacturacion.FechaInicial && item.FechaInicial <= corteFacturacion.FechaFinal) ||
                                         (item.FechaFinal >= corteFacturacion.FechaInicial && item.FechaFinal <= corteFacturacion.FechaFinal)
                                     select
                                         item).FirstOrDefault();

                    if (resultado == null)
                    {
                        AgregarElementoLista(corteFacturacion);
                    }
                    else
                    {
                        lblErrorFecha.Visible = true;
                    }
                }
                else
                {
                    AgregarElementoLista(corteFacturacion);
                }
            }
            else
            {
                lblErrorFecha.Visible = true;
                txtFechaInicial.Text = FechaMinima.ToShortDateString();
                txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                txtFechaInicial.Focus();
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorFecha.Visible = false;

            if (Session["fechaMinimaVenta"] != null)
            {
                FechaMinima = Convert.ToDateTime(Session["fechaMinimaVenta"].ToString());
            }

            rvFechaFinal.MaximumValue = DateTime.Now.ToString("dd.MM.yyyy");
            if (!Page.IsPostBack)
            {
                ListaCorte = new List<CorteFacturacion>();
            }
        }

        #endregion Metodos Protegidos

        #region Metodos Privados

        /// <summary>
        /// Permite Agregar un Elemento a Lista y al DataGrid
        /// </summary>
        /// <param name="corteFacturacion">The corte facturacion.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void AgregarElementoLista(CorteFacturacion corteFacturacion)
        {
            ListaCorte.Add(corteFacturacion);
            CargaObjetos.OrdenamientoGrilla(this.Page, grvCortesFacturacion, ListaCorte);
        }

        /// <summary>
        /// Método que carga de forma inicial los campos fechas inicio y fin.
        /// </summary>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon
        /// FechaDeCreacion: 04/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargueInicialCamposFechas()
        {
            txtFechaFinal.Text = DateTime.Now.ToString(Resources.GlobalWeb.General_FormatoFecha);
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}