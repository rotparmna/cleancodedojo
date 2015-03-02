// --------------------------------
// <copyright file="CalcularConceptosCobro.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Clase Calcular Conceptos Cobro.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Clases
{
    using System.Collections.Generic;
    using System.Linq;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Clases.CalcularConceptosCobro.
    /// </summary>
    public class CalcularConceptosCobro
    {
        #region Methods (3)

        //// Public Methods (3) 

        /// <summary>
        /// Metodo para actualizar los pagos realizados
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public void ActualizacionPagos(List<FacturaAtencion> atenciones)
        {
            var itemsValidar = from
                                   atencion in atenciones
                               from
                                   detalle in atencion.Detalle
                               where
                                   detalle.Exclusion == null
                                   && detalle.ExclusionManual == null
                                   && atencion.Cruzar == true
                                   && atencion.ConceptosCobro.Count > 0
                               select
                                   atencion;

            foreach (var atencion in itemsValidar)
            {
                this.CruzarConceptosCobro(atencion);
            }
        }

        /// <summary>
        /// Metodo Para Calcular Los pagos Realizados (conceptos de cobro).
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Pagos Realizados en la factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 20/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public static decimal CalcularPagosRealizadosConceptos(EstadoCuentaEncabezado estadoCuenta)
        {
            decimal pagosRealizados = 0;
            List<ConceptoCobro> lstConceptos = new List<ConceptoCobro>();

            if (estadoCuenta.AtencionActiva != null && estadoCuenta.AtencionActiva.Deposito != null)
            {
                lstConceptos = estadoCuenta.AtencionActiva.Deposito.Concepto.ToList();
            }

            if (lstConceptos != null & lstConceptos.Count() > 0)
            {
                pagosRealizados = (from item in lstConceptos
                                   where item.IndHabilitado == 1
                                          && item.IdContrato == estadoCuenta.IdContrato
                                          && item.IdPlan == estadoCuenta.IdPlan
                                   select item.ValorConcepto).Sum();
            }
            else
            {
                pagosRealizados = 0;
            }

            return pagosRealizados;
        }

        /// <summary>
        /// Metodo para cruzar los conceptos de cobro
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 19/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public void CruzarConceptosCobro(FacturaAtencion atencion)
        {
            if (atencion.MovimientosTesoreria != null && atencion.MovimientosTesoreria.Count > 0)
            {
                foreach (var movimiento in atencion.MovimientosTesoreria)
                {
                    foreach (var concepto in atencion.ConceptosCobro)
                    {
                        if (movimiento.ValorSaldo <= 0 || concepto.ValorSaldo == 0)
                        {
                            break;
                        }

                        movimiento.Actualizar = true;
                        concepto.Actualizar = true;

                        if (movimiento.ValorSaldo >= concepto.ValorCruzado)
                        {
                            movimiento.ValorSaldo -= concepto.ValorCruzado;
                            concepto.ValorSaldo = 0;
                        }
                        else
                        {
                            concepto.ValorSaldo = concepto.ValorCruzado - movimiento.ValorSaldo;
                            concepto.ValorCruzado -= movimiento.ValorSaldo;
                            movimiento.ValorSaldo = 0;
                        }
                    }
                }
            }
            else
            {
                foreach (var concepto in atencion.ConceptosCobro)
                {
                    concepto.Actualizar = true;
                    concepto.IndHabilitado = 1;
                    concepto.ValorSaldo = 0;
                }
            }
        }

        #endregion Methods
    }
}