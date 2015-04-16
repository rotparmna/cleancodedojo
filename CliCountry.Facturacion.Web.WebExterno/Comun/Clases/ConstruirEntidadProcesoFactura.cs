using CliCountry.SAHI.Dominio.Entidades;
using CliCountry.SAHI.Dominio.Entidades.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CliCountry.Facturacion.Web.WebExterno.Comun.Clases
{
    public class ConstruirEntidadProcesoFactura
    {
        #region Declaraciones Locales 

        #region Variables 

        private ProcesoFactura _proceso;

        private Vinculacion _vinculacionSeleccionada;

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Constructores 

        public ConstruirEntidadProcesoFactura(Vinculacion vinculacionSeleccionada)
        {
            this._proceso = new ProcesoFactura();
            this._vinculacionSeleccionada = vinculacionSeleccionada;

        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        public ProcesoFactura Proceso
        {
            get
            {
                return this._proceso;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        public ProcesoFactura Construir(string codigoEntidad,
           string codigoSeccion,
           byte idFormato,
           byte idEstado,
           short idUsuarioFirma,
           int idTipoMovimiento,
           byte indHabilitado,
           string tipoRelacion,
           TipoFacturacion tipoFactura,
           List<int> ventasNoMarcadas,
            Responsable responsable,
            List<Vinculacion> vinculaciones,
            List<int> ventasSeleccionadas)
        {
            ConstruirEncabezado(codigoEntidad, codigoSeccion, idFormato, idEstado, idUsuarioFirma, idTipoMovimiento, indHabilitado, tipoRelacion, tipoFactura, ventasNoMarcadas);
            ConstruirDetalles();
            this._proceso.ExclusionesNoMarcadas = this._vinculacionSeleccionada.NoMarcadas;
            this._proceso.Responsable = responsable;
            this._proceso.Vinculaciones = vinculaciones;
            this._proceso.VentasSeleccionadas = ventasSeleccionadas;
            return this._proceso;
        }

        #endregion Metodos Publicos 
        #region Metodos Privados 

        private void ConstruirDetalles()
        {
            this._proceso.Detalles = new List<ProcesoFacturaDetalle>();
            this._proceso.Detalles.Add(new ProcesoFacturaDetalle()
            {
                IdAtencion = _vinculacionSeleccionada.IdAtencion,
                IdPlan = _vinculacionSeleccionada.Plan.Id,
                Cruzar = true
            });
        }

        private void ConstruirEncabezado(string codigoEntidad,
           string codigoSeccion,
           byte idFormato,
           byte idEstado,
           short idUsuarioFirma,
           int idTipoMovimiento,
           byte indHabilitado,
           string tipoRelacion,
           TipoFacturacion tipoFactura,
           List<int> ventasNoMarcadas)
        {
            this._proceso = new ProcesoFactura()
            {
                CodigoEntidad = codigoEntidad,
                CodigoSeccion = codigoSeccion,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                IdContrato = Convert.ToInt32(_vinculacionSeleccionada.Contrato.Id),
                IdEstado = idEstado,
                IdFormato = idFormato,
                IdUsuarioFirma = idUsuarioFirma,
                IdTercero = Convert.ToInt32(_vinculacionSeleccionada.Tercero.Id),
                IdTipoMovimiento = idTipoMovimiento,
                IndHabilitado = indHabilitado,
                TipoRelacion = tipoRelacion,
                TipoFactura = tipoFactura,
                VentasNoMarcadas = ventasNoMarcadas
            };
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}