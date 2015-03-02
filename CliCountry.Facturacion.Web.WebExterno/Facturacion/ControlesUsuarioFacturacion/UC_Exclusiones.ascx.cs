// --------------------------------
// <copyright file="UC_Exclusiones.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// --------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.Facturacion.Web.WebExterno.Comun;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using Enumeracion = CliCountry.Facturacion.Web.WebExterno.Comun;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_Exclusiones.
    /// </summary>
    public partial class UC_Exclusiones : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The NOMARCADAS
        /// </summary>
        private const string NOMARCADAS = "ExclusionesNoMarcadas";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece exclusiones no marcadas
        /// </summary>
        public List<NoMarcada> ExclusionesNoMarcadas
        {
            get
            {
                return ViewState[NOMARCADAS] as List<NoMarcada>;
            }

            set
            {
                ViewState[NOMARCADAS] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Se encarga del cargue inicial de la grilla de exclusiones.
        /// </summary>
        /// <param name="exclusion">Entidad con los datos de los parametros para obtener las exclusiones.</param>
        public void CargarGrillaExclusiones(ExclusionFacturaActividades exclusion)
        {
            ExclusionFacturaActividades param = new ExclusionFacturaActividades()
            {
                CodigoEntidad = exclusion.CodigoEntidad,
                IdTercero = exclusion.IdTercero,
                IdContrato = exclusion.IdContrato,
                IdPlan = exclusion.IdPlan
            };
            var ejecExclusiones = WebService.Facturacion.ObtenerExclusiones(param);
            var exclusiones = from exc in ejecExclusiones.Objeto
                              select new ExclusionesVinculacion
                              {
                                  CheckPrincipal = true,
                                  CheckActivo = ValidarMarcaActiva(exc.Id, (TipoExclusion)Enum.Parse(typeof(TipoExclusion), exc.TipoContrato.ToString())),
                                  TipoAtencionNombre = exc.NombreTipoAtencion,
                                  NombreServicioExclusion = exc.NombreServicio,
                                  TipoProductoNombre = exc.NombreTipoProducto,
                                  GrupoProductoNombre = exc.NombreGrupo,
                                  ProductoNombre = exc.NombreProducto,
                                  ProductoAlternoNombre = exc.NombreProductoAlterno,
                                  ManualContrato = Enum.Parse(typeof(TipoExclusion), exc.TipoContrato.ToString()).ToString(),
                                  IdExclusion = exc.IdContrato
                              };
            CargaObjetos.OrdenamientoGrilla(this.Page, grvExclusiones, exclusiones);
        }

        /// <summary>
        /// Se encarga del cargue inicial de la grilla de exclusiones.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <param name="exclusionManual">The exclusion manual.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarGrillaExclusiones(Exclusion exclusion, ExclusionManual exclusionManual)
        {
            List<Exclusion> listaExclusionContraro = new List<Exclusion>();
            listaExclusionContraro = WebService.Facturacion.ConsultarExclusionesAtencionContrato(ConstruirExclusionesContrato()).Objeto.Item.ToList();

            List<ExclusionManual> listaExclusionManual = new List<ExclusionManual>();
            listaExclusionManual = WebService.Facturacion.ConsultarExclusionesManual(exclusionManual).Objeto.ToList();

            var exclusiones = (from exContrato in listaExclusionContraro
                               select new ExclusionesVinculacion
                               {
                                   CheckPrincipal = true,
                                   CheckActivo = ValidarMarcaActiva(exContrato.Id, Enumeracion.TipoExclusion.Contrato),
                                   TipoAtencionNombre = exContrato.TipoAtencion.Nombre,
                                   NombreServicioExclusion = exContrato.NombreServicio,
                                   TipoProductoNombre = exContrato.TipoProducto.Nombre,
                                   GrupoProductoNombre = exContrato.GrupoProducto.Nombre,
                                   ProductoNombre = exContrato.Producto.Nombre,
                                   ProductoAlternoNombre = exContrato.NombreProductoAlterno,
                                   ManualContrato = Enumeracion.TipoExclusion.Contrato.ToString(),
                                   IdExclusion = exContrato.Id
                               }).Union(from exManual in listaExclusionManual
                                        select new ExclusionesVinculacion
                                        {
                                            CheckPrincipal = true,
                                            CheckActivo = ValidarMarcaActiva(exManual.Id, Enumeracion.TipoExclusion.Manual),
                                            TipoAtencionNombre = string.Empty,
                                            NombreServicioExclusion = exManual.NombreServicio == null ? string.Empty : exManual.NombreServicio,
                                            TipoProductoNombre = exManual.TipoProducto.Nombre,
                                            GrupoProductoNombre = exManual.GrupoProducto.Nombre,
                                            ProductoNombre = exManual.Producto.Nombre,
                                            ProductoAlternoNombre = exManual.NombreProductoAlterno,
                                            ManualContrato = Enumeracion.TipoExclusion.Manual.ToString(),
                                            IdExclusion = exManual.Id
                                        }).ToList();

            CargaObjetos.OrdenamientoGrilla(this.Page, grvExclusiones, exclusiones);
        }

        /// <summary>
        /// Obtiene los registros de exclusiones que han sido desmarcadas.
        /// </summary>
        /// <returns>Lista de Exclusiones No Seleccionadas.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 17/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<NoMarcada> ObtenerExclusionesDesmarcadas()
        {
            List<NoMarcada> exclusionesNoMarcadas = (from
                                           item in grvExclusiones.Rows.Cast<GridViewRow>()
                                                     where
                                                         !(item.FindControl("chkAplicar") as CheckBox).Checked
                                                     select new NoMarcada
                                                     {
                                                         IdExclusion = Convert.ToInt32(grvExclusiones.DataKeys[item.RowIndex].Value),
                                                         TipoExclusion = item.Cells[7].Text
                                                     }).ToList();

            return exclusionesNoMarcadas;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Guardado temporal de las exclusiones marcadas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 16/01/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgAplicarExclusiones_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            List<NoMarcada> lstNoMarcadas = new List<NoMarcada>();
            lstNoMarcadas = ObtenerExclusionesDesmarcadas();
            VinculacionSeleccionada.NoMarcadas = lstNoMarcadas;
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Se ejecuta cuando se carga la página.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
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
        /// Obtiene las exclusiones de contrato asociadas a la atención.
        /// </summary>
        /// <returns>lista de Esclusiones de Contrato.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 20/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Paginacion<Exclusion> ConstruirExclusionesContrato()
        {
            var exclusion = new Paginacion<Exclusion>()
            {
                LongitudPagina = 0,
                PaginaActual = 0,
                TotalRegistros = 0,
                Item = new Exclusion()
                {
                    CodigoEntidad = Settings.Default.General_CodigoEntidad,
                    IdTercero = VinculacionSeleccionada.Tercero.Id,
                    IdContrato = VinculacionSeleccionada.Contrato.Id,
                    IdAtencion = 0,
                    IdPlan = VinculacionSeleccionada.Plan.Id,
                    IndicadorContratoActivo = 1
                }
            };

            return exclusion;
        }

        /// <summary>
        /// Metodo Valida la marca activa del Control.
        /// </summary>
        /// <param name="identificadorExclusion">The id exclusion.</param>
        /// <param name="tipo">The tipo.</param>
        /// <returns>Resultado de Validacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 17/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private bool ValidarMarcaActiva(int identificadorExclusion, Enumeracion.TipoExclusion tipo)
        {
            if (VinculacionSeleccionada.NoMarcadas != null)
            {
                ExclusionesNoMarcadas = VinculacionSeleccionada.NoMarcadas;

                var algo = from
                               item in ExclusionesNoMarcadas
                           where
                               item.IdExclusion == identificadorExclusion
                               && item.TipoExclusion == tipo.ToString()
                           select
                               item;

                return algo.Count() > 0 ? false : true;
            }
            else
            {
                return true;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}