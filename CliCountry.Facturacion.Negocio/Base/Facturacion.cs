// --------------------------------
// <copyright file="Facturacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Negocio Facturacion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Negocio.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Transactions;
    using System.Xml.Linq;
    using CliCountry.Facturacion.Datos.Fachada;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.Facturacion.Negocio.Comun.Recursos;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Abstractas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Auditoria;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using Excepcion = CliCountry.SAHI.Comun.Excepciones;

    /// <summary>
    /// Negocio facturacion.
    /// </summary>
    public class Facturacion
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// Para llamar condiciones de redondeo unicamente cuando cambia el contrato.
        /// </summary>
        private int contratoRedondeo = 0;

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Privadas 

/// <summary>
        /// Obtiene o establece arr redondeo.
        /// </summary>
        /// <value>
        /// Tipo Dato ArrayList.
        /// </value>
        private ArrayList ArrRedondeo { get; set; }

        /// <summary>
        /// Obtiene o establece código entidad.
        /// </summary>
        private string CodigoEntidad { get; set; }

        /// <summary>
        /// Obtiene o establece condic proceso.
        /// </summary>
        /// <value>
        /// Tipo Dato Lista.
        /// </value>
        private List<CondicionProceso> CondicProceso { get; set; }

        /// <summary>
        /// Obtiene o establece detalle tarifa productos.
        /// </summary>
        /// <value>
        /// Tipo Dato Lista.
        /// </value>
        private List<DetalleTarifa> detalleTarifaProductos { get; set; }

        /// <summary>
        /// Obtiene o establece GBL responsables componente.
        /// </summary>
        /// <value>
        /// Tipo dato Lista.
        /// </value>
        private List<VentaResponsable> gblResponsablesComponente { get; set; }

        /// <summary>
        /// Obtiene o establece GBL venta detalle.
        /// </summary>
        /// <value>
        /// Tipo Dato Lista.
        /// </value>
        private List<VentaDetalle> gblVentaDetalle { get; set; }

        /// <summary>
        /// Obtiene o establece homologa productos.
        /// </summary>
        /// <value>
        /// Tipo dato Lista.
        /// </value>
        private List<HomologacionProducto> homologaProductos { get; set; }

        /// <summary>
        /// Obtiene o establece lista todos factores qx.
        /// </summary>
        /// <value>
        /// Tipo Dato Lista.
        /// </value>
        private List<FactoresQX> listaTodosFactoresQx { get; set; }

        /// <summary>
        /// Obtiene o establece orden factor QX.
        /// </summary>
        private short OrdenFactorQX { get; set; }

        /// <summary>
        /// Obtiene o establece tarifa unidad.
        /// </summary>
        /// <value>
        /// Tipo Dato Lista.
        /// </value>
        private List<TarifaUnidad> tarifaUnidad { get; set; }

        /// <summary>
        /// Obtiene o establece val bilateralidad.
        /// </summary>
        private short ValBilateralidad { get; set; }

        /// <summary>
        /// Obtiene o establece val especialidad.
        /// </summary>
        private int ValEspecialidad { get; set; }

        /// <summary>
        /// Obtiene o establece val via.
        /// </summary>
        private int ValVia { get; set; }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Enumeraciones 

        /// <summary>
        /// Tipo de exclusiones de contrato al que se debe validar.
        /// </summary>
        private enum Tipo
        {
            /// <summary>
            /// El producto.
            /// </summary>
            Producto,

            /// <summary>
            /// El componente.
            /// </summary>
            Componente
        }

/// <summary>
        /// Enumeración para tipo de exclusión.
        /// </summary>
        private enum TipoExclusion
        {
            /// <summary>
            /// Indica si la exclusión es de Contrato.
            /// </summary>
            Manual,

            /// <summary>
            /// Indica si la exclusión es de Manual.
            /// </summary>
            Contrato
        }

        #endregion Enumeraciones 

        #region Metodos 

        #region Metodos Publicos Estaticos 

        /// <summary>
        /// Guarda el descuento.
        /// </summary>
        /// <param name="descuento">Parametro Descuento.</param>
        /// <returns>Id del Descuento.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public static int GuardarDescuento(DescuentoConfiguracion descuento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarDescuento(descuento);
        }

        #endregion Metodos Publicos Estaticos 
        #region Metodos Publicos 

        /// <summary>
        /// Actualizars the atencion.
        /// </summary>
        /// <param name="atencionCliente">Es un parametro de ingreso.</param>
        /// <returns>Indica el Resultado de la Actualización.</returns>
        public bool ActualizarAtencion(AtencionCliente atencionCliente)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarAtencion(atencionCliente);
        }

        /// <summary>
        /// Actualizars the bloquear atencion.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <param name="usuario">El parametro usuario.</param>
        /// <returns>Cantidad registros.</returns>
        public int ActualizarBloquearAtencion(int identificadorAtencion, string usuario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarBloquearAtencion(identificadorAtencion, usuario);
        }

        /// <summary>
        /// Método para actualizar el indicador del concepto en contabilidad de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Número de la factura anulada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 01/08/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarConceptoContabilidad(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarConceptoContabilidad(notaCredito);
        }

        /// <summary>
        /// Método para actualizar los conceptos de cobro.
        /// </summary>
        /// <param name="conceptoCobro">The concepto cobro.</param>
        /// <returns>Indica si se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarConceptosCobro(FacturaAtencionConceptoCobro conceptoCobro)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarConceptosCobro(conceptoCobro);
        }

        /// <summary>
        /// Actualiza la informacion de la condición de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Id condición cubrimiento actualizado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarCondicionCubrimiento(condicionCubrimiento);
        }

        /// <summary>
        /// Método para realizar la actualización de condición de tarifa.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Indica si se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarCondicionTarifa(CondicionTarifa condicionTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ActualizarCondicionTarifa(condicionTarifa);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Método para actualizar el convenio.
        /// </summary>
        /// <param name="convenioNoClinico">The convenio no clinico.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 09/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarConvenioNoClinico(ConvenioNoClinico convenioNoClinico)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarConvenioNoClinico(convenioNoClinico);
        }

        /// <summary>
        /// Actualiza la informacion del cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Id cubrimiento actualizado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarCubrimientos(Cubrimiento cubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarCubrimientos(cubrimiento);
        }

        /// <summary>
        /// Método para realizar la actualizacion del descuento.
        /// </summary>
        /// <param name="descuento">The descuentos.</param>
        /// <returns>Indica si se afecto el registro en la base de datos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarDescuento(DescuentoConfiguracion descuento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarDescuento(descuento);
        }

        /// <summary>
        /// Método para actualizar el estado de la cuenta de cartera de l afactura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>N mero de la nota credito.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoCuentaCartera(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoCuentaCartera(notaCredito);
        }

        /// <summary>
        /// Metodo para realizar la actualización del estado de la factura a eliminado.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Numero de factura eliminada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoFactura(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoFactura(notaCredito);
        }

        /// <summary>
        /// Metodo Para Actualizar el Estado de proceso.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Indica Si Se Actualiza el estado de proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarEstadoProcesoFactura(ProcesoFactura procesoFactura)
        {
            try
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                return fachada.ActualizarEstadoProcesoFactura(procesoFactura);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Metodo Para Actualizar el Estado de proceso de Facturación No Clínica.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Indica Si Se Actualiza el estado de proceso.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarEstadoProcesoFacturaNC(ProcesoFactura procesoFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoProcesoFacturaNC(procesoFactura);
        }

        /// <summary>
        /// Actualiza el estado de cada venta asociada a una factura durante el proceso de anulación.
        /// </summary>
        /// <param name="notacredito">Objeto NotaCredito.</param>
        /// <param name="numeroVenta">El número de venta asociado.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 29/04/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaAnulacion(NotaCredito notacredito, int numeroVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoVentaAnulacion(notacredito, numeroVenta);
        }

        /// <summary>
        /// Actualiza el estado de cada venta asociada a una factura durante el proceso de anulación - Facturación No Clínico
        /// </summary>
        /// <param name="notacredito">Objeto NotaCredito.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 06/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaAnulacionNC(NotaCredito notacredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoVentaAnulacionNC(notacredito);
        }

        /// <summary>
        /// Método que actualiza el estado de las ventas.
        /// </summary>
        /// <param name="ventaEstadoFactura">The detalle factura.</param>
        /// <returns>Estado de venta.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaFactura(VentaEstadoFactura ventaEstadoFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoVentaFactura(ventaEstadoFactura);
        }

        /// <summary>
        /// Método que actualiza el estado de las ventas. Facturación No Clínica.
        /// </summary>
        /// <param name="ventaEstadoFactura">The detalle factura.</param>
        /// <returns>Estado de venta.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaFacturaNC(VentaEstadoFactura ventaEstadoFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoVentaFacturaNC(ventaEstadoFactura);
        }

        /// <summary>
        /// Método que Actualiza y/o elimina informacion de ventas de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id del estado de la venta anulada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 02/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentasAnulacion(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarEstadoVentasAnulacion(notaCredito);
        }

        /// <summary>
        /// Método que actualiza la informacion de la exclusión de contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de la exclusión actualizada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarExclusionContrato(Exclusion exclusion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarExclusionContrato(exclusion);
        }

        /// <summary>
        /// Método que actualiza el Id de la cuenta en la factura.
        /// </summary>
        /// <param name="identificadorCuenta">The id cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Indica si se actualizo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarIdCuentaFactura(int identificadorCuenta, int numeroFactura)
        {
            try
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                return fachada.ActualizarIdCuentaFactura(identificadorCuenta, numeroFactura);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Método para actualizar los movimientos de tesoreria.
        /// </summary>
        /// <param name="movimiento">The movimiento.</param>
        /// <returns>Indica si se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarMovimientosTesoreria(FacturaAtencionMovimiento movimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarMovimientosTesoreria(movimiento);
        }

        /// <summary>
        /// Metodo que actualiza el número de factura en cuenta cartera.
        /// </summary>
        /// <param name="identificadorCuenta">The id cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Indica si se actualizo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 12/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarNumeroFacturaCuentaCartera(int identificadorCuenta, int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarNumeroFacturaCuentaCartera(identificadorCuenta, numeroFactura);
        }

        /// <summary>
        /// Metodo para realizar la actualización de recargo.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>
        /// Indica si Se realiza la actualización.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez - INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarRecargo(Recargo recargo)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ActualizarRecargo(recargo);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Método para actualizar los saldos afectados en los movimientos de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Número de factura anulada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarSaldosMovimientos(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarSaldosMovimientos(notaCredito);
        }

        /// <summary>
        /// Método para actualizar la vinculación.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Registro modificado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarVinculacion(Vinculacion vinculacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarVinculacion(vinculacion);
        }

        /// <summary>
        /// Metodo de actualizacion de la vinculacion de la venta.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Indica Si Se realiza la actualizacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool ActualizarVinculacionVentas(VinculacionVenta vinculacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ActualizarVinculacionVentas(vinculacion);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Realiza la anulación de un conjunto de ventas no clínicas.
        /// </summary>
        /// <param name="identificadorVentaTipoProceso">Objeto de tipo diccionario que contiene el # de la Venta y el Id del Tipo de Transacción.</param>
        /// <returns>Lista de enteros que contiene todas las ventas que no se hayan podido anular.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 14/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Dictionary<int, string> AnularVentaNoClinica(Dictionary<int, int> identificadorVentaTipoProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            Dictionary<int, string> ventasNoAnuladas = new Dictionary<int, string>();
            int result;

            foreach (var item in identificadorVentaTipoProceso)
            {
                var resultado = fachada.AnularVentaNoClinica(item.Key, item.Value);

                if (!Int32.TryParse(resultado, out result))
                {
                    ventasNoAnuladas.Add(item.Key, resultado);
                }
            }

            return ventasNoAnuladas;
        }

        /// <summary>
        /// Metodo para aplicar el ajuste de tarifa a la venta.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <param name="porcentaje">If set to <c>true</c> [porcentaje].</param>
        /// <returns>Lista de detalle de tarifas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DetalleTarifa AplicarAjusteTarifaVenta(CondicionContrato condicionContrato, BaseValidacion detalle, TipoFacturacion tipoFacturacion, CondicionTarifa condicionTarifa, bool porcentaje)
        {
            DetalleTarifa detalleTarifa = null;

            detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManual, condicionContrato.FechaVigencia);
            this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManual);

            if (detalleTarifa == null)
            {
                detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManualAlterno, condicionContrato.FechaVigenciaAlterna);
                this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManualAlterno);

                if (detalleTarifa == null)
                {
                    detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManualInstitucional, null);
                    this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManualInstitucional);
                }
            }

            if (detalleTarifa != null)
            {
                detalleTarifa.ValorTarifa = this.CalcularValorUnitario(detalleTarifa, condicionContrato, detalle);
            }

            return detalleTarifa;
        }

        /// <summary>
        /// Método auditar los productos del detalle de ventas.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de confirmacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int AuditoriaVentaDetalle(AuditoriaVentaDetalle detalleVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.AuditoriaVentaDetalle(detalleVenta);
        }

        /// <summary>
        /// Metodo para borrar los productos de BilVentasDetalle.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de confirmacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int BorrarVentaDetalle(VentaDetalle detalleVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.BorrarVentasDetalle(detalleVenta);
        }

        /// <summary>
        /// Borrars the venta detalles nc.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de Confirmacion.</returns>
        public int BorrarVentaDetallesNC(VentaDetalle detalleVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.BorrarVentasDetallesNC(detalleVenta);
        }

        /// <summary>
        /// Realiza el cierre de facturacion.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>
        /// Retorna int.
        /// </returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int CierreFacturacion(string tipoFacturacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.CierreFacturacion(tipoFacturacion);
        }

        /// <summary>
        /// Retorna los componentes por productos.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaComponente> ComponentesPorProductos(FacturaAtencion atencion)
        {
            List<VentaComponente> componentesProductos = this.ConsultarProductoComponentes(new VentaComponente()
            {
                IdAtencion = atencion.IdAtencion,
                IdContrato = atencion.IdContrato,
                IndHabilitado = true
            });

            return componentesProductos;
        }

        /// <summary>
        /// Retorna los componentes por productos.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaComponente> ComponentesPorProductosReimpresion(FacturaAtencion atencion)
        {
            List<VentaComponente> componentesProductos = this.ConsultarProductoComponentesReimpresion(new VentaComponente()
            {
                IdAtencion = atencion.IdAtencion,
                IdContrato = atencion.IdContrato,
                IndHabilitado = true
            });

            return componentesProductos;
        }

        /// <summary>
        /// Permite consultar las aproximaciones.
        /// </summary>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Aproximacion> ConsultarAproximaciones()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarAproximaciones();
        }

        /// <summary>
        /// Permite consultar las aproximaciones.
        /// </summary>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Aproximacion> ConsultarAproximacionesActivas()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarAproximacionesActivas();
        }

        /// <summary>
        /// Consultars the atencion cliente.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <returns>Objeto tipo AtencionCliente.</returns>
        public AtencionCliente ConsultarAtencionCliente(int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarAtencionCliente(identificadorAtencion);
        }

        /// <summary>
        /// Consultars the atenciones pendientesx procesar.
        /// </summary>
        /// <param name="facturaAtencion">La factura atencion.</param>
        /// <returns>Listado de Atendicion de Facturas.</returns>
        public List<FacturaAtencion> ConsultarAtencionesPendientesxProcesar(FacturaAtencion facturaAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarAtencionesPendientesxProcesar(facturaAtencion);
        }

        /// <summary>
        /// Permite Consultar las atenciones de facturación por relación.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Lista de las atenciones de facturación por relación.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano.
        /// FechaDeCreacion: (01/02/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: atenciones de facturación por relación.
        /// </remarks>
        public List<FacturaAtencionRelacion> ConsultarAtencionFacturacionRelacion(FacturaAtencionRelacion facturaAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarAtencionFacturacionRelacion(facturaAtencion);
        }

        /// <summary>
        /// Consulta el detalle de causales paginado.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Causales detalle.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<CausalDetalle>> ConsultarCausalesDetallePaginado(Paginacion<CausalDetalle> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarCausalesDetallePaginado(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite consultar las clases de cubrimiento.
        /// </summary>
        /// <param name="claseCubrimiento">The clase cubrimiento.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<ClaseCubrimiento> ConsultarClasesCubrimiento(ClaseCubrimiento claseCubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarClasesCubrimiento(claseCubrimiento);
        }

        /// <summary>
        /// Consultars the clientex atencion.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <returns>Resultado operacion.</returns>
        public Cliente ConsultarClientexAtencion(int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarClientexAtencion(identificadorAtencion);
        }

        /// <summary>
        /// Consultars the componentes.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <param name="identificadorTipoProducto">El identificador tipo producto.</param>
        /// <returns>Lista de Datos.</returns>
        public List<TipoComponente> ConsultarComponentes(int identificadorAtencion, int identificadorTipoProducto)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarComponentes(identificadorAtencion, identificadorTipoProducto);
        }

        /// <summary>
        /// Consultars the componentes x producto.
        /// </summary>
        /// <param name="productoBase">El producto base.</param>
        /// <param name="producto">El producto.</param>
        /// <returns>Metodo para retornar los Componentes Asociados.</returns>
        public List<Componente> ConsultarComponentesXProducto(TipoProductoCompuesto productoBase, ProductoVenta producto)
        {
            var resultado = new List<Componente>();
            var consulta = new List<Componente>();
            FachadaFacturacion fachada = new FachadaFacturacion();

            switch (producto.TipoVenta)
            {
                case TipoProductoVenta.VentaNoQx:
                    consulta = fachada.ConsultarComponentesNoQx(producto);
                    break;

                case TipoProductoVenta.VentaQx:
                    consulta = fachada.ConsultarComponentesQx(producto);
                    break;
            }

            if (producto.ValidarComponentes)
            {
                resultado = consulta.ToList();
            }
            else
            {
                resultado = consulta.Take(1).ToList();
            }

            return resultado;
        }

        /// <summary>
        /// Consultars the conceptos.
        /// </summary>
        /// <param name="atencion">La atencion.</param>
        /// <returns>Retorna Lista de ConceptoCobro.</returns>
        public List<ConceptoCobro> ConsultarConceptos(Atencion atencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarConceptos(atencion);
        }

        /// <summary>
        /// Consultars the conceptos cartera.
        /// </summary>
        /// <param name="conceptoCartera">El concepto cartera.</param>
        /// <returns>Lista de conceptos.</returns>
        public List<ConceptoCartera> ConsultarConceptosCartera(ConceptoCartera conceptoCartera)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarConceptosCartera(conceptoCartera);
        }

        /// <summary>
        /// Devuelve el registro de una condicion de contrato.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <returns>Objeto condicion de contrato.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public CondicionContrato ConsultarCondicionContrato(CondicionContrato condicionContrato)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarCondicionesContrato(condicionContrato);
        }

        /// <summary>
        /// Permite consultar las condiciones de cubrimiento aplicadas a una vinculación.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<CondicionCubrimiento>> ConsultarCondicionesCubrimiento(Paginacion<CondicionCubrimiento> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarCondicionesCubrimiento(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultar condiciones de facturacion.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Lista de condiciones facturacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 18/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<CondicionTarifa> ConsultarCondicionesFacturacion(CondicionTarifa condicionTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarCondicionesFacturacion(condicionTarifa);
        }

        /// <summary>
        /// Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </summary>
        /// <param name="condicionTarifa">The tarifa.</param>
        /// <returns>Lista las condiciones de tarifa.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </remarks>
        public List<CondicionTarifa> ConsultarCondicionesTarifas(CondicionTarifa condicionTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarCondicionesTarifas(condicionTarifa);
        }

        /// <summary>
        /// Consultars the contrato plan.
        /// </summary>
        /// <param name="paginacion">La Paginacion.</param>
        /// <returns>Lista de datos.</returns>
        public Paginacion<List<ContratoPlan>> ConsultarContratoPlan(Paginacion<ContratoPlan> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarContratoPlan(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultars the convenio no clinico.
        /// </summary>
        /// <param name="paginacion">La paginacion.</param>
        /// <returns>Lista de convenios no clinicos.</returns>
        public Paginacion<List<ConvenioNoClinico>> ConsultarConvenioNoClinico(Paginacion<ConvenioNoClinico> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarConvenioNoClinico(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite consultar los costos asociados.
        /// </summary>
        /// <param name="costoAsociado">El costo asociado.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 23/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<CostoAsociado> ConsultarCostoAsociado(CostoAsociado costoAsociado)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarCostoAsociado(costoAsociado);
        }

        /// <summary>
        /// Metodo de consulta de cubrimientos paginado.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de datos.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Cubrimiento>> ConsultarCubrimientos(Paginacion<Cubrimiento> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarCubrimientos(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultars the cuentas cartera.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <returns>Lista de cuentas de cartera.</returns>
        public List<CuentaCartera> ConsultarCuentasCartera(int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarCuentasCartera(identificadorAtencion);
        }

        /// <summary>
        /// Consulta los datos del cierre de facturacion.
        /// </summary>
        /// <returns>Datos del cierre de facturación.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public DataSet ConsultarDatosCierre()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();

            return fachada.ConsultarDatosCierre();
        }

        /// <summary>
        /// Consultars the depositos.
        /// </summary>
        /// <param name="atencion">La Atencion es un parametro de ingreso.</param>
        /// <returns>Lista de depositos.</returns>
        public List<Deposito> ConsultarDepositos(Atencion atencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDepositos(atencion);
        }

        /// <summary>
        /// Consultars the descuentos.
        /// </summary>
        /// <param name="descuentos">Los descuentos.</param>
        /// <returns>Lista de datos.</returns>
        public Paginacion<List<Descuento>> ConsultarDescuentos(Paginacion<Descuento> descuentos)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarDescuentos(descuentos);
            resultado.LongitudPagina = descuentos.LongitudPagina;
            resultado.PaginaActual = descuentos.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultars the descuentos contrato.
        /// </summary>
        /// <param name="descuento">Los descuento.</param>
        /// <returns>Lsitado de Descuento.</returns>
        public List<Descuento> ConsultarDescuentosContrato(Descuento descuento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDescuentosContrato(descuento);
        }

        /// <summary>
        /// Consulta los Clientes Asociados a la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Listado de Clientes de Factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Cliente> ConsultarDetalleClienteFactura(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleClienteFactura(numeroFactura);
        }

        /// <summary>
        /// Método para consultar los productos no facturables.
        /// </summary>
        /// <param name="itemNoFacturable">The no facturable.</param>
        /// <returns>Lista de productos que quedaron en no facturables.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<NoFacturable> ConsultarDetalleNoFacturable(NoFacturable itemNoFacturable)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleNoFacturable(itemNoFacturable);
        }

        /// <summary>
        /// Consulta la informacion detallada de la tarifa.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifa(int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleTarifa(identificadorProceso);
        }

        /// <summary>
        /// Metodo para Consultar El Valor de Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public TipoProductoCompuesto ConsultarDetalleTarifa(TipoProductoCompuesto producto)
        {
            var facturaAtencion = new FacturaAtencion()
            {
                IdAtencion = producto.IdAtencion,
                IdContrato = producto.CondicionContrato.IdContrato,
                IdManual = producto.CondicionContrato.IdManual,
                IdServicio = producto.Atencion.IdServicio,
                IdTercero = producto.VinculacionActiva.Tercero.Id,
                IdTipoAtencion = Convert.ToInt16(producto.Atencion.IdAtencionTipo),
                IdPlan = producto.VinculacionActiva.Plan.Id
            };

            var baseValidacion = new FacturaAtencionDetalle()
            {
                FechaVenta = producto.Fecha,
                IdAtencion = producto.IdAtencion,
                IdGrupoProducto = Convert.ToInt16(producto.GrupoProducto.IdGrupo),
                IdPlan = producto.VinculacionActiva.Plan.Id,
                IdProducto = producto.Producto.IdProducto,
                IdServicio = producto.Atencion.IdServicio,
                IdTipoAtencion = producto.Atencion.IdAtencionTipo,
                IdTipoProducto = producto.IdTipoProducto,
            };

            return this.ProcesarProductoVenta(producto, facturaAtencion, baseValidacion);
        }

        /// <summary>
        /// Metodo para realizar la consulta de detalle de tarifa de manual.
        /// </summary>
        /// <param name="detalleTarifa">The detalle tarifa.</param>
        /// <returns>
        /// Listado de tarifas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifaManual(DetalleTarifa detalleTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleTarifaManual(detalleTarifa);
        }

        /// <summary>
        /// Metodo para realizar la consulta de detalle de tarifa de manual.
        /// </summary>
        /// <param name="detalleTarifa">The detalle tarifa.</param>
        /// <returns>
        /// Listado de tarifas.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 18/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifaXManual(DetalleTarifa detalleTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleTarifaXManual(detalleTarifa);
        }

        /// <summary>
        /// Consulta los detalles de la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Detalles de la factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaDetalle> ConsultarDetalleVentaFactura(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarDetalleVentaFactura(numeroFactura);
        }

        /// <summary>
        /// Consultar el encabezado del estado de cuenta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Encabezado estado cuenta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarEstadoCuentaEncabezado(EstadoCuentaEncabezado estadoCuenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarEstadoCuentaEncabezado(estadoCuenta);
        }

        /// <summary>
        /// Consultar encabezado estado de cuenta multiple.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Encabezado estado cuenta.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 12/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarEstadoCuentaEncabezadoMultiple(EstadoCuentaEncabezado estadoCuenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarEstadoCuentaEncabezadoMultiple(estadoCuenta);
        }

        /// <summary>
        /// Consultar el encabezado del estado de cuenta - Facturación No Clínica.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Encabezado estado cuenta.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 22/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarEstadoCuentaEncabezadoNC(EstadoCuentaEncabezado estadoCuenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarEstadoCuentaEncabezadoNC(estadoCuenta);
        }

        /// <summary>
        /// Consultar factura por número de factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="identificadorUsuarioFirma">The id usuario firma.</param>
        /// <returns>
        /// Factura encabezado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarEstadoCuentaxNumeroFactura(int numeroFactura, short identificadorUsuarioFirma)
        {
            var estadoCuenta = this.ConsultarFacturaEncabezado(numeroFactura, identificadorUsuarioFirma);
            if (estadoCuenta != null)
            {
                estadoCuenta.FacturaAtencion = this.RelacionDetallexAtencion(numeroFactura);
                estadoCuenta.FacturaPaquetes = this.RelacionDetallexPaquete(numeroFactura);
            }

            return estadoCuenta;
        }

        /// <summary>
        /// Consulta el estado del proceso.
        /// </summary>
        /// <param name="identificadorProceso">The identificador proceso.</param>
        /// <returns>Estado del proceso.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ConsultarEstadoProceso(int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarEstadoProceso(identificadorProceso);
        }

        /// <summary>
        /// Metodo para consultar exclusiones del Contrato x Atencion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de exclusiones por atencion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 30/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Exclusion>> ConsultarExclusionesAtencionContrato(Paginacion<Exclusion> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarExclusionesAtencionContrato(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar las exclusiones aplicadas en el contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Listado de exclusiones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las exclusiones aplicadas en el contrato.
        /// </remarks>
        public List<Exclusion> ConsultarExclusionesContrato(Exclusion exclusion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarExclusionesContrato(exclusion);
        }

        /// <summary>
        /// Permite Consultar las exclusiones aplicadas en una tarifa.
        /// </summary>
        /// <param name="tarifaExclusion">The tarifa exclusion.</param>
        /// <returns>Listado de exclusiones de manual.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las exclusiones aplicadas en una tarifa.
        /// </remarks>
        public List<ExclusionManual> ConsultarExclusionesManual(ExclusionManual tarifaExclusion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarExclusionesManual(tarifaExclusion);
        }

        /// <summary>
        /// Permite consultar los faxtores QX de una Atención.
        /// </summary>
        /// <returns>
        /// Lista de factores QX.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<FactoresQX> ConsultarFactoresQx()
        {
            // se hace una sola consulta
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFactoresQX();
        }

        /// <summary>
        /// Consultar Factores QX.
        /// </summary>
        /// <param name="factoresQX">The factores QX.</param>
        /// <returns>Lista de factores qx.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Factores QX.
        /// </remarks>
        public List<FactoresQX> ConsultarFactoresQX(FactoresQX factoresQX)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFactoresQX(factoresQX);
        }

        /// <summary>
        /// Metodo para consultar las causales aplicadas en la factura anulada.
        /// </summary>
        /// <param name="detalleNotaCredito">The detalle Nota Credito.</param>
        /// <returns>Lista de causales de la factura.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<DetalleNotaCredito> ConsultarFacturaAnuladaDetalle(DetalleNotaCredito detalleNotaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaAnuladaDetalle(detalleNotaCredito);
        }

        /// <summary>
        /// Metodo para realizar la carga de componentes.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Lista de Componentes.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaComponente> ConsultarFacturaComponentes(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaComponentes(numeroFactura);
        }

        /// <summary>
        /// Consultar Factura Detalle.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Detalles de la factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<FacturaAtencion> ConsultarFacturaDetalle(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaDetalle(numeroFactura);
        }

        /// <summary>
        /// Método para consultar el detalle de los paquetes armados de la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Detalle de paquetes de la factura.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<PaqueteProducto> ConsultarFacturaDetallePaquetes(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaDetallePaquetes(numeroFactura);
        }

        /// <summary>
        /// Consulta los detalles de la Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Detalles de la Factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<FacturaAtencionDetalle> ConsultarFacturaDetallexNumeroFactura(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaDetallexNumeroFactura(numeroFactura);
        }

        /// <summary>
        /// Consultar Factura Encabezado.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="identificadorUsuarioFirma">The id usuario firma.</param>
        /// <returns>
        /// Factura encabezado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarFacturaEncabezado(int numeroFactura, short identificadorUsuarioFirma)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarFacturaEncabezado(numeroFactura, identificadorUsuarioFirma);
        }

        /// <summary>
        /// Permite Consultar las facturas.
        /// </summary>
        /// <param name="facturaResultado">The factura resultado.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Jos  Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<FacturaResultado>> ConsultarFacturas(Paginacion<FacturaResultado> facturaResultado)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarFacturas(facturaResultado);
            resultado.LongitudPagina = facturaResultado.LongitudPagina;
            resultado.PaginaActual = facturaResultado.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para consulta en notas credito las facturas anuladas.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de facturas anuladas.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 23/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<NotaCredito>> ConsultarFacturasAnuladas(Paginacion<NotaCredito> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarFacturasAnuladas(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultars the facturas nc.
        /// </summary>
        /// <param name="facturaResultado">The factura resultado.</param>
        /// <returns>Lista de Datos.</returns>
        public Paginacion<List<FacturaResultado>> ConsultarFacturasNC(Paginacion<FacturaResultado> facturaResultado)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarFacturasNC(facturaResultado);
            resultado.LongitudPagina = facturaResultado.LongitudPagina;
            resultado.PaginaActual = facturaResultado.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar las facturas para reimprimir.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista facturas para reimprimir.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<ReimprimirFactura>> ConsultarFacturasReimpresion(Paginacion<ReimprimirFactura> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarFacturasReimpresion(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar las atenciones pendientes por procesar.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="xml">Parametro XML.</param>
        /// <returns>
        /// Listado de Atendicion de Facturas.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public List<FacturaAtencion> ConsultarGeneralFacturacion(FacturaAtencion facturaAtencion, XDocument xml)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarGeneralFacturacion(facturaAtencion, xml);
        }

        /// <summary>
        /// Consulta todo los grupos de tipo de producto paquetes
        /// </summary>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom.
        /// FechaDeCreacion: 23/02/2015.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<String> ConsultarGrupoPaquetes()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarGrupoPaquetes();
        }

        /// <summary>
        /// Consulta las homologaciones de un producto.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<HomologacionProducto> ConsultarHomologacionProducto(int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarHomologacionProducto(identificadorProceso);
        }

        /// <summary>
        /// Metodo de consulta de honorarios medicos.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Lista de Honorarios.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Honorario>> ConsultarHonorariosMedicos(Paginacion<Honorario> honorario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarHonorariosMedicos(honorario);
            resultado.LongitudPagina = honorario.LongitudPagina;
            resultado.PaginaActual = honorario.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo de consulta de honorarios medicos.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Lista de Honorarios.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Honorario>> ConsultarHonorariosMedicosxProducto(Paginacion<Honorario> honorario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            honorario.PaginaActual = 0;
            honorario.LongitudPagina = 0;
            return fachada.ConsultarHonorariosMedicos(honorario);
        }

        /// <summary>
        /// Metodo para consultar la informacion basica del tercero.
        /// </summary>
        /// <returns>Informacion Basica del Tercero.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public InformacionBasicaTercero ConsultarInformacionBasicaTercero()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarInformacionBasicaTercero();
        }

        /// <summary>
        /// Consulta la informacion adicional para una factura.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="identificadorTipoMovimiento">The id tipo movimiento.</param>
        /// <param name="identificadorTipoFacturacion">The id tipo facturacion.</param>
        /// <returns>
        /// Registro con informacion adicional.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public InformacionFactura ConsultarInformacionFactura(int identificadorProceso, int identificadorTipoMovimiento, short identificadorTipoFacturacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarInformacionFactura(identificadorProceso, identificadorTipoMovimiento, identificadorTipoFacturacion);
        }

        /// <summary>
        /// Consulta la informacion adicional para una factura - Facturación No Clínica.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="identificadorTipoMovimiento">The id tipo movimiento.</param>
        /// <param name="identificadorTipoFacturacion">The id tipo facturacion.</param>
        /// <returns>
        /// Registro con informacion adicional.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 24/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public InformacionFactura ConsultarInformacionFacturaNC(int identificadorProceso, int identificadorTipoMovimiento, short identificadorTipoFacturacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarInformacionFacturaNC(identificadorProceso, identificadorTipoMovimiento, identificadorTipoFacturacion);
        }

        /// <summary>
        /// Metodo para realizar la Consulta de Multiples Atenciones.
        /// </summary>
        /// <param name="numerosAtencion">The numeros atencion.</param>
        /// <returns>Lista de Atenciones.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<AtencionCliente> ConsultarListaAtenciones(List<int> numerosAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarListaAtenciones(numerosAtencion);
        }

        /// <summary>
        /// Metodo para consultar las atenciones que se encuentran en proceso.
        /// </summary>
        /// <param name="numerosAtencion">The numeros atencion.</param>
        /// <returns>Lista de atenciones en proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<AtencionCliente> ConsultarListaAtencionesProceso(List<int> numerosAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarListaAtencionesProceso(numerosAtencion);
        }

        /// <summary>
        /// Consulta Logs de Cierre de facturación de acuerdo a los filtros enviados.
        /// </summary>
        /// <param name="mes">Parámetro mes.</param>
        /// <param name="año">Parámetro año.</param>
        /// <param name="estado">Parámetro estado.</param>
        /// <returns>Tabla de Datos.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public DataTable ConsultarLogFacturacion(int mes, int año, string estado)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarLogFacturacion(mes, año, estado);
        }

        /// <summary>
        /// Consultars Maestras.
        /// </summary>
        /// <param name="identificadorMaestra">The id maestra.</param>
        /// <param name="identificadorPagina">The id pagina.</param>
        /// <returns>
        /// Lista de Maestras.
        /// </returns>
        /// <remarks>
        /// Autor: Alex Mattos.
        /// FechaDeCreacion: (04/04/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion:  Consulta los Maestras.
        /// </remarks>
        public List<Maestras> ConsultarMaestras(int identificadorMaestra, int identificadorPagina)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarMaestras(identificadorMaestra, identificadorPagina);
        }

        /// <summary>
        /// Permite Consultar lAS TARIFAS.
        /// </summary>
        /// <param name="manuales">The manuales.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<ManualesBasicos>> ConsultarManualesBasicos(Paginacion<ManualesBasicos> manuales)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarManualesBasicos(manuales);
            resultado.LongitudPagina = manuales.LongitudPagina;
            resultado.PaginaActual = manuales.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar los datos del manual asociado a un contrato.
        /// </summary>
        /// <param name="identificadorContrato">Identificador del Contrato presente en la vinculación seleccionada.</param>
        /// <returns>
        /// Datos del manual básico.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO/gocampo.
        /// FechaDeCreacion: 21/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarManualesBasicosContrato(int identificadorContrato)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarManualesBasicosContrato(identificadorContrato);
        }

        /// <summary>
        /// Metodo para obtener el tipo de movimiento del usuario.
        /// </summary>
        /// <param name="movimientoUsuario">The movimiento usuario.</param>
        /// <returns>Tipo de movimiento del usuario.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public MovimientoUsuario ConsultarMovimientoUsuario(MovimientoUsuario movimientoUsuario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarMovimientoUsuario(movimientoUsuario);
        }

        /// <summary>
        /// Consulta el nivel de complejidad de un producto teniendo en cuenta el idManual y la fecha de venta
        /// </summary>
        /// <param name="producto">Entidad con la información del producto</param>
        /// <returns>Entidad con la información del nivel de complejidad</returns>
        /// <remarks>
        /// Autor: Viviana Paola Torres - INTERGRUPO\vtorres.
        /// FechaDeCreacion: 10/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta el nivel de complejidad de un producto teniendo en cuenta el idManual y la fecha de venta
        /// </remarks>
        public List<NivelComplejidad> ConsultarNivelComplejidadProducto(TipoProductoCompuesto producto)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarNivelComplejidadProducto(producto);
        }

        /// <summary>
        /// Permite consultar los niveles de complejidad de una exclusi n de manual vigente.
        /// </summary>
        /// <param name="nivelComplejidad">The nivel complejidad.</param>
        /// <returns>Niveles de Complejidad de una exclusi n de manual vigente.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<NivelComplejidad> ConsultarNivelesComplejidad(NivelComplejidad nivelComplejidad)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarNivelesComplejidad(nivelComplejidad);
        }

        /// <summary>
        /// Metodo que consulta Paquete.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de paquete.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Paquete>> ConsultarPaquete(Paginacion<Paquete> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarPaquete(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo que consulta Paquete.
        /// </summary>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Lista de paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<PaqueteProducto> ConsultarPaqueteDetallado(int identificadorPaquete, int identificadorAtencion, string ventas)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarPaqueteDetallado(identificadorPaquete, identificadorAtencion, ventas);
        }

        /// <summary>
        /// Consultar Paquete Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Lista Estado Cuenta Detallado.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Paquete Factura.
        /// </remarks>
        public List<EstadoCuentaDetallado> ConsultarPaqueteFactura(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarPaqueteFactura(numeroFactura);
        }

        /// <summary>
        /// Metodo que consulta Paquete.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<PaqueteEncabezado>> ConsultarPaqueteProducto(Paginacion<PaqueteEncabezado> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarPaqueteProducto(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo que consulta Paquete.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lista de paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<PaqueteProducto>> ConsultarPaqueteProductoDetallado(Paginacion<PaqueteProducto> paginacion, int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarPaqueteProductoDetallado(paginacion, identificadorAtencion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consulta los planes.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <returns>
        /// Lista de los contratos por entidad.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez.
        /// FechaDeCreacion: 28/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Consulta los contratos.
        /// </remarks>
        public List<Plan> ConsultarPlanes(Contrato contrato)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarPlanes(contrato);
        }

        /// <summary>
        /// Obtiene el porcentaje establecido en manual alterno para costos asociados.
        /// </summary>
        /// <param name="identificadorContrato">Id del Contrato.</param>
        /// <param name="identificadorManualAlterno">Id del Manual Alterno.</param>
        /// <param name="identificadorManual">Id del Manual Principal.</param>
        /// <returns>Porcentaje alterno establecido</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 14/10/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public decimal ConsultarPorcentajeAlterno(int identificadorContrato, int identificadorManualAlterno, int identificadorManual)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarPorcentajeAlterno(identificadorContrato, identificadorManualAlterno, identificadorManual);
        }

        /// <summary>
        /// Permite consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaComponente> ConsultarProductoComponentes(VentaComponente detalle)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductoComponentes(detalle);
        }

        /// <summary>
        /// Permite consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaComponente> ConsultarProductoComponentesReimpresion(VentaComponente detalle)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductoComponentesReimpresion(detalle);
        }

        /// <summary>
        /// Metodo para consultar en condiciones de cubrimientos los productos que pertenezcan a una clase de cubrimiento para una atenci n.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicionCubrimiento.</param>
        /// <returns>Productos de la clase de cubrimiento.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<CondicionCubrimiento> ConsultarProductosCondicionCubrimientos(CondicionCubrimiento condicionCubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductosCondicionCubrimientos(condicionCubrimiento);
        }

        /// <summary>
        /// M?todo para consultar en cubrimientos los productos que pertenezcan a una clase de cubrimiento para una atenci n.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Productos de la clase de cubrimiento.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Cubrimiento> ConsultarProductosCubrimientos(Cubrimiento cubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductosCubrimientos(cubrimiento);
        }

        /// <summary>
        /// Permite Consultar las productos de una venta no clinica.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Listado de productos de Facturas.</returns>
        /// <remarks>
        /// Autor: Diana C rdenas- INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 26/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public List<FacturaAtencion> ConsultarProductosNC(FacturaAtencion facturaAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductosNC(facturaAtencion);
        }

        /// <summary>
        /// Metodo para realizar las consulta de productos de la venta.
        /// </summary>
        /// <param name="productoVenta">The producto venta.</param>
        /// <returns>Retorna los productos de la Venta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<ProductoVenta> ConsultarProductosVenta(ProductoVenta productoVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarProductosVenta(productoVenta);
            return resultado;
        }

        /// <summary>
        /// Metodo para realizar las consulta de productos de la venta.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <returns>
        /// Retorna los productos de la Venta.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<ProductoVenta> ConsultarProductosVentaPaquete(int identificadorAtencion, int identificadorPaquete)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarProductosVentaPaquete(identificadorAtencion, identificadorPaquete);
            return resultado;
        }

        /// <summary>
        /// Obtiene los productos del paquete que se encuentras dentro de las ventas asociadas.
        /// </summary>
        /// <param name="productosPaquete">The productos paquete.</param>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Productos del paquete.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 03/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<PaqueteProducto> ConsultarProductosVentasNoMarcadas(List<PaqueteProducto> productosPaquete, FacturaAtencion atencion)
        {
            IEnumerable<PaqueteProducto> lista = from
                                                      item in atencion.Detalle
                                                 join
                                                     producto in productosPaquete
                                                 on
                                                    item.IdProducto equals producto.IdProducto
                                                 select new PaqueteProducto()
                                                 {
                                                     IdPaquete = producto.IdPaquete,
                                                     IdProducto = producto.IdProducto,
                                                     IdGrupo = producto.IdGrupo,
                                                     NombreProducto = producto.NombreProducto,
                                                     NombreGrupo = producto.NombreGrupo,
                                                     CantidadMaxima = producto.CantidadMaxima,
                                                     IndHabilitado = producto.IndHabilitado
                                                 };

            return lista.ToList();
        }

        /// <summary>
        /// Consultar productos por Id Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>
        /// Retorna Producto.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes- INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 13/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Producto ConsultarProductoxIdProducto(Producto producto)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarProductoxIdProducto(producto);
        }

        /// <summary>
        /// Permite Consultar lAS TARIFAS.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Recargo>> ConsultarRecargos(Paginacion<Recargo> recargo)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarRecargos(recargo);
            resultado.LongitudPagina = recargo.LongitudPagina;
            resultado.PaginaActual = recargo.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el contrato.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar los recargos aplicados en el contrato a un producto.
        /// </remarks>
        public List<Recargo> ConsultarRecargosContrato(Recargo recargo)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarRecargosContrato(recargo);
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el manual.
        /// </summary>
        /// <param name="recargoManual">The recargo manual.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<RecargoManual> ConsultarRecargosManual(RecargoManual recargoManual)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarRecargosManual(recargoManual);
        }

        /// <summary>
        /// Metodo de Consultar recargos.
        /// </summary>
        /// <param name="tarifas">The tarifas.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez- INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<CondicionTarifa>> ConsultarTarifas(Paginacion<CondicionTarifa> tarifas)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarTarifas(tarifas);
            resultado.LongitudPagina = tarifas.LongitudPagina;
            resultado.PaginaActual = tarifas.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para la consulta de Unidad.
        /// </summary>
        /// <param name="tarifaUnidad">The tarifa unidad.</param>
        /// <returns>
        /// Lista de Unidades.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 20/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<TarifaUnidad> ConsultarTarifaUnidad(TarifaUnidad tarifaUnidad)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTarifaUnidad(tarifaUnidad);
        }

        /// <summary>
        /// Metodo para realizar la consulta de la tasa de Impuestos.
        /// </summary>
        /// <returns>
        /// Listado de Tasas de Impuestos.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Tasa> ConsultarTasa()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTasa();
        }

        /// <summary>
        /// Consulta los datos b sicos de un tercero.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jos  Alexander Murcia Salamana - INTERGRUPO\Administrator.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public FacturaNoClinicaReporte ConsultarTercero(int identificadorTercero)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTercero(identificadorTercero);
        }

        /// <summary>
        /// Consultar tercero responsable de componentes.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Lista de objetos de tipo VentaResponsable.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaResponsable> ConsultarTerceroComponente(int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTerceroComponente(identificadorAtencion);
        }

        /// <summary>
        /// Retorna la lista de terceros responsables luego de la caida.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Retorna Responsables.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Descripcion detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Honorario>> ConsultarTercerosResponsables(Paginacion<Honorario> honorario)
        {
            var resultado = this.CargarResponsableHonorariosXML(honorario);
            resultado.LongitudPagina = honorario.LongitudPagina;
            resultado.PaginaActual = honorario.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para consultar los tipos de empresa.
        /// </summary>
        /// <param name="tipoEmpresa">The tipo empresa.</param>
        /// <returns>Listado de tipos de empresa.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<TipoEmpresa> ConsultarTipoEmpresa(TipoEmpresa tipoEmpresa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTipoEmpresa(tipoEmpresa);
        }

        /// <summary>
        /// Consultars the tipo factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna el tipo de factura.</returns>
        public string ConsultarTipoFactura(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTipoFactura(numeroFactura);
        }

        /// <summary>
        /// Metodo para consultar Tipo Producto.
        /// </summary>
        /// <param name="tipoProducto">The tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        /// Lista de Tipo Producto por Ubicacion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<TipoProducto> ConsultarTipoProducto(TipoProducto tipoProducto, string usuario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTipoProducto(tipoProducto, usuario);
        }

        /// <summary>
        /// Consulta Transacciones.
        /// </summary>
        /// <param name="transaccion">The transaccion.</param>
        /// <returns>Retorna Lista.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Transaccion> ConsultarTransaccion(Transaccion transaccion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTransaccion(transaccion);
        }

        /// <summary>
        /// Selecciona las transacciones permitidas en Ventas Qx.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Listado de transacciones.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 22/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Selecciona las transacciones permitidas en Ventas Qx.
        /// </remarks>
        public List<Transaccion> ConsultarTransaccionesVentasQx(string usuario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarTransaccionesVentasQx(usuario);
        }

        /// <summary>
        /// Metodo de Consulta de Ubicaci n.
        /// </summary>
        /// <param name="ubicacion">The ubicacion.</param>
        /// <returns>Lista de cnsulta Ubicaci n por Usuario.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Ubicacion> ConsultarUbicacion(Ubicacion ubicacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarUbicacion(ubicacion);
        }

        /// <summary>
        /// Metodo de Consulta de Ubicaci n de Consumo.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        /// Lista de cnsulta Ubicaci n de Consumo por Atencion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 26/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Ubicacion> ConsultarUbicacionConsumo(int identificadorAtencion, int identificadorTipoProducto, string usuario)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarUbicacionConsumo(identificadorAtencion, identificadorTipoProducto, usuario);
        }

        /// <summary>
        /// Método para consultar Ubicación por Nombre.
        /// </summary>
        /// <param name="ubicacion">The ubicacion.</param>
        /// <returns>Lista de Ubicaciones.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Ubicacion> ConsultarUbicacionPorNombre(Ubicacion ubicacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarUbicacionPorNombre(ubicacion);
        }

        /// <summary>
        /// Metodo para obtener el valor real del paquete encabezado.
        /// </summary>
        /// <param name="facturaPaquete">The factura paquete.</param>
        /// <returns>Lista de paquetes.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Paquete> ConsultarValorEncabezadoPaquetes(Paquete facturaPaquete)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarValorEncabezadoPaquetes(facturaPaquete);
        }

        /// <summary>
        /// Metodo para consultar los valores de los paquetes encabezados.
        /// </summary>
        /// <param name="facturaPaquete">The factura paquete.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<FacturaPaquete> ConsultarValorPaquetes(FacturaPaquete facturaPaquete)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarValorPaquetes(facturaPaquete);
        }

        /// <summary>
        /// Consulta los detalles de la Venta.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<VentaDetalle> ConsultarVentaDetalle(VentaDetalle detalleVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarVentaDetalles(detalleVenta);
        }

        /// <summary>
        /// Metodo para consultar el detalle de la venta x Número de Venta y Id de Transacción.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Detalle de la venta.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 17/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaDetalle> ConsultarVentaDetallesxIdTx(VentaDetalle detalleVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarVentaDetallesxIdTx(detalleVenta);
        }

        /// <summary>
        /// Permite Consultar las Ventas No Clinicas.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Ventas.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<VentaNoClinica>> ConsultarVentaNoClinica(Paginacion<VentaNoClinica> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarVentaNoClinica(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para consultar productos relacion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Conjunto de Datos Resultado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<VentaProductoRelacion>> ConsultarVentaProductosRelacion(Paginacion<VentaProductoRelacion> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarVentaProductosRelacion(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para Consultar Ventas de Medicamentos e Insumos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Detalle de Ventas Asociadas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<VentaProducto>> ConsultarVentaProductosTransaccion(Paginacion<VentaProducto> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarVentaProductosTransaccion(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar las ventas para facturacion por actividad.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Venta>> ConsultarVentasNumeroVenta(Paginacion<Venta> venta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarVentasNumeroVenta(venta);
            resultado.LongitudPagina = venta.LongitudPagina;
            resultado.PaginaActual = venta.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Metodo para Consultar Ventas Qx.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Ventas Asociadas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<VentaTransaccion>> ConsultarVentasTransaccion(Paginacion<VentaTransaccion> paginacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultarVentasTransaccion(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Permite Consultar las vinculaciones por atenci n.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<Vinculacion> ConsultarVinculaciones(Vinculacion atencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ConsultarVinculaciones(atencion);
        }

        /// <summary>
        /// Permite Consultar las ventas para facturacion por actividad.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Paginacion<List<Venta>> ConsultaVentasAtencion(Paginacion<Venta> venta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.ConsultaVentasAtencion(venta);
            resultado.LongitudPagina = venta.LongitudPagina;
            resultado.PaginaActual = venta.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Crea el objeto responsable de la factura.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <param name="componente">The componente.</param>
        /// <param name="responsable">The responsable.</param>
        /// <param name="resposableEstadoCuenta">The resposable estado cuenta.</param>
        /// <returns>
        /// Objeto responsable.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Responsable CrearInformacionResponsableFactura(FacturaCompuesta facturaCompuesta, VentaComponente componente, VentaResponsable responsable, Responsable resposableEstadoCuenta)
        {
            int identificadorResponsable = 0;

            if (responsable != null && responsable.Honorario != null && responsable.Honorario.ResponsableHonorario != null)
            {
                identificadorResponsable = responsable.Honorario.ResponsableHonorario.IdTercero;
            }
            else
            {
                identificadorResponsable = resposableEstadoCuenta.IdTercero;
            }

            var registroResponsable = new Responsable()
            {
                CodigoEntidad = facturaCompuesta.EncabezadoFactura.CodigoEntidad,
                Consecutivo = facturaCompuesta.EncabezadoFactura.CodigoSeccion,
                NumeroFactura = facturaCompuesta.EncabezadoFactura.NumeroFactura,
                IdTipoMovimiento = facturaCompuesta.EncabezadoFactura.IdTipoMovimiento,
                CodigoMovimientoSecuencial = facturaCompuesta.EncabezadoFactura.CodigoMovimiento,
                IdVenta = componente.IdTransaccion,
                NumeroVenta = componente.NumeroVenta,
                IdProducto = componente.IdProducto,
                LoteDetalleVenta = 0,
                Componente = componente.Componente,
                IdTercero = identificadorResponsable,
                PorcentajeDescuentoDetalle = 0,
                PorcentajeValorDetalle = 0,
                IdComponente = 1
            };

            return registroResponsable;
        }

        /// <summary>
        /// Elimina los productos no facturables de una factura, como parte del proceso de anulación.
        /// </summary>
        /// <param name="numeroFactura">El número de la factura a eliminar</param>
        /// <returns>El resultado de la operación.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 16/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int EliminarNoFacturables(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.EliminarNoFacturables(numeroFactura);
        }

        /// <summary>
        /// Elimina el contenido de la tabla de proceso dado el Id del Proceso
        /// </summary>
        /// <param name="identificadorProceso">Id del proceso que acaba de terminar</param>
        /// <returns>resultado de la operación</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 06/08/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int EliminarProcesoActual(int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.EliminarProcesoActual(identificadorProceso);
        }

        /// <summary>
        /// Elimina los productos de una tarifa.
        /// </summary>
        /// <param name="codigoEntidad">The codigo entidad.</param>
        /// <param name="identificadorTarifa">The identificador tarifa.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 13/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Elimina los productos de una tarifa.
        /// </remarks>
        public void EliminarProductosTarifa(string codigoEntidad, int identificadorTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.EliminarProductosTarifa(codigoEntidad, identificadorTarifa);
        }

        /// <summary>
        /// Filtrar Factores Qx.
        /// </summary>
        /// <param name="factoresQx">The factores qx.</param>
        /// <returns>Lista de factores qx.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Filtrar Factores Qx.
        /// </remarks>
        public List<FactoresQX> FiltrarFactoresQx(FactoresQX factoresQx)
        {
            var retorno = from
                            item in this.listaTodosFactoresQx
                          where
                            item.CodigoEntidad == factoresQx.CodigoEntidad && item.Componente == factoresQx.Componente
                            && item.IdManual == factoresQx.IdManual && factoresQx.FechaVigencia >= item.FechaInicial && factoresQx.FechaVigencia <= item.FechaFinal
                          select
                            item;
            return retorno.ToList();
        }

        /// <summary>
        /// Metodo para Generar el estado de Cuenta.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Estado de Cuenta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 18/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado GenerarEstadoCuenta(ProcesoFactura procesoFactura)
        {
            return this.IniciarProceso(procesoFactura);
        }

        /// <summary>
        /// Metodo para guardar ventas asociadas.
        /// </summary>
        /// <param name="ventaProductoRelacion">The venta producto relacion.</param>
        /// <returns>Indica Si Se Realiza con exito la operacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool GuardarActualizarVentaProductoRelacion(List<VentaProductoRelacion> ventaProductoRelacion)
        {
            var resultado = 0;
            foreach (var item in ventaProductoRelacion)
            {
                resultado = 0;
                if (item.IdVentaProductoRelacion > 0)
                {
                    resultado = this.ActualizarVentaProductoRelacion(item);
                }
                else
                {
                    resultado = this.GuardarVentaProductoRelacion(item);
                }

                if (resultado <= 0)
                {
                    throw new Excepcion.Negocio(ReglasNegocio.VentasProductoRelacion);
                }
            }

            return true;
        }

        /// <summary>
        /// Metodo para guardar la factura anulada en notas credito.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id de la nota credito.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarAnulacionFactura(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            notaCredito.IdNumeroNotaCredito = fachada.GuardarAnulacionFactura(notaCredito);
            this.CrearInformacionDetalleAnulacionFactura(notaCredito);

            return notaCredito.IdNumeroNotaCredito;
        }

        /// <summary>
        /// Metodo para guardar la factura anulada en notas credito - Facturación No Clínica.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id de la nota credito.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 05/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarAnulacionFacturaNC(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();

            notaCredito.IdTipoMovimiento = 176; // SE INSERTA TIPO DE MOVIMIENTO RFNC-Rever Fact No Clinica
            DataTable tabla = fachada.GuardarAnulacionFacturaNC(notaCredito);

            notaCredito.IdNumeroNotaCredito = Convert.ToInt32(tabla.Rows[0][0].ToString());
            notaCredito.CodigoMovimiento = tabla.Rows[0][1].ToString();
            notaCredito.CodigoMovimientoFactura = tabla.Rows[0][2].ToString();
            notaCredito.IdTipoMovimientoFactura = notaCredito.IdTipoMovimiento;

            this.CrearInformacionDetalleAnulacionFacturaNC(notaCredito);

            return notaCredito.IdNumeroNotaCredito;
        }

        /// <summary>
        /// Metodo para guardar el Cliente.
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <returns>
        /// Id del Cliente generado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarCliente(Cliente cliente)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCliente(cliente);
        }

        /// <summary>
        /// Método para guardar la información del concepto de cobro.
        /// </summary>
        /// <param name="conceptoCobro">The concepto cobro.</param>
        /// <returns>Id del concepto de cobro generado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarConceptoCobro(ConceptoCobro conceptoCobro)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarConceptoCobro(conceptoCobro);
        }

        /// <summary>
        /// Guarda la informacion de la condicion de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>
        /// Id condicion cubrimiento creado.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCondicionCubrimiento(condicionCubrimiento);
        }

        /// <summary>
        /// Metodo para guardar Tarifas.
        /// </summary>
        /// <param name="condicionTarifa">The tarifas.</param>
        /// <returns>
        /// Id de la Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarCondicionTarifa(CondicionTarifa condicionTarifa)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCondicionTarifa(condicionTarifa);
        }

        /// <summary>
        /// Metodo para guardar el convenio.
        /// </summary>
        /// <param name="convenioNoClinico">The convenio no clinico.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 09/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarConvenioNoClinico(ConvenioNoClinico convenioNoClinico)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarConvenioNoClinico(convenioNoClinico);
        }

        /// <summary>
        /// Guarda la informacion del cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Id cubrimiento creado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarCubrimientos(Cubrimiento cubrimiento)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCubrimientos(cubrimiento);
        }

        /// <summary>
        /// Metodo para guardar las causales de la factura anulada.
        /// </summary>
        /// <param name="detalleNotaCredito">The detalle nota credito.</param>
        /// <returns>Id de la nota credito aplicada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarDetalleAnulacionFactura(DetalleNotaCredito detalleNotaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarDetalleAnulacionFactura(detalleNotaCredito);
        }

        /// <summary>
        /// Metodo para almacenar la informacion de contabilidad del estado de cuenta.
        /// </summary>
        /// <param name="contabilidad">The contabilidad.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarEstadoCuentaContabilidad(Contabilidad contabilidad)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarEstadoCuentaContabilidad(contabilidad);
        }

        /// <summary>
        /// Metodo que almacena la informacion de la exclusi n de contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de la exclusi n insertada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarExclusionContrato(Exclusion exclusion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarExclusionContrato(exclusion);
        }

        /// <summary>
        /// Metodo que inserta la informacion del pago de la factura.
        /// </summary>
        /// <param name="facturaPago">The factura pago.</param>
        /// <returns>Valor de guardado.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarFacturaPago(FacturaPago facturaPago)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarFacturaPago(facturaPago);
        }

        /// <summary>
        ///  Metodo que inserta la informacion del detalle del pago de la factura.
        /// </summary>
        /// <param name="facturaPagoDetalle">The factura pago detalle.</param>
        /// <returns>Valor de Guardado.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarFacturaPagoDetalle(FacturaPagoDetalle facturaPagoDetalle)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarFacturaPagoDetalle(facturaPagoDetalle);
        }

        /// <summary>
        /// Guarda la informacion de la cuenta de cartera.
        /// </summary>
        /// <param name="cuentaCartera">The cuenta cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarInformacionCuentaCartera(CuentaCartera cuentaCartera)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCuentaCartera(cuentaCartera);
        }

        /// <summary>
        /// Método para almacenar la información de la cuenta cartera - Facturación No Clínica
        /// </summary>
        /// <param name="cuentaCartera">The cuenta cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 04/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarInformacionCuentaCarteraNC(CuentaCartera cuentaCartera)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarCuentaCarteraNC(cuentaCartera);
        }

        /// <summary>
        /// Guarda el detalle de la factura.
        /// </summary>
        /// <param name="detalleFactura">The detalle factura.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void GuardarInformacionDetalleFactura(EstadoCuentaDetallado detalleFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarDetalleFactura(detalleFactura);
        }

        /// <summary>
        /// Método que almacena la información detallada de la factura no clínica.
        /// </summary>
        /// <param name="detalleFactura">The detalle factura.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void GuardarInformacionDetalleFacturaNC(EstadoCuentaDetallado detalleFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarDetalleFacturaNC(detalleFactura);
        }

        /// <summary>
        /// Metodo que Guardar Informacion Encabezado Factura.
        /// </summary>
        /// <param name="detalleMovimientoCartera">The detalle movimiento cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarInformacionDetalleMovimientoCartera(DetalleMovimientoCartera detalleMovimientoCartera)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarDetalleMovimientoCartera(detalleMovimientoCartera);
        }

        /// <summary>
        /// Metodo que Guardar Informacion Encabezado Factura.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <returns>Numero de factura.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarInformacionEncabezadoFactura(EncabezadoFactura encabezadoFactura)
        {
            try
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                return fachada.GuardarInformacionEncabezadoFactura(encabezadoFactura);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Método que almacena la información de la cabecera de la factura no clínica.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="identificadorModulo">El identificador del módulo.</param>
        /// <returns>Numero de factura.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pïno - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarInformacionEncabezadoFacturaNC(EncabezadoFactura encabezadoFactura, int identificadorModulo)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarInformacionEncabezadoFacturaNC(encabezadoFactura, identificadorModulo);
        }

        /// <summary>
        /// Metodo para almacenar las atenciones a facturar.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Indica el proceso donde se realizaran las operaciones de almacenamiento.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFactura(ProcesoFactura procesoFactura)
        {
            try
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                procesoFactura.IdProceso = fachada.GuardarEncabezadoProceso(procesoFactura);

                foreach (var detalle in procesoFactura.Detalles)
                {
                    detalle.IdProceso = procesoFactura.IdProceso;
                    detalle.IdProcesoDetalle = fachada.GuardarDetalleProceso(detalle);
                }

                return this.IniciarProceso(procesoFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guardar Informacion Factura Actividades Paquetes.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Estado Cuenta Encabezado.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Factura Actividades Paquetes.
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFacturaActividadesPaquetes(FacturaCompuesta facturaCompuesta)
        {
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;

            try
            {
                if (this.ValidarParticular(estadoCuenta))
                {
                    if (estadoCuenta != null && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdTercero > 0)
                    {
                        encabezadoFactura.IdTerceroResponsable = estadoCuenta.Responsable.IdTercero;
                        encabezadoFactura.TipoResultadoTercero = "T";
                    }
                    else if (estadoCuenta != null && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdCliente > 0)
                    {
                        encabezadoFactura.IdTerceroResponsable = estadoCuenta.Responsable.IdCliente;
                        encabezadoFactura.TipoResultadoTercero = "C";
                    }
                }

                if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionPaquete)
                {
                    encabezadoFactura.VentasFactura = this.CrearVentaFacturaPaquete(estadoCuenta);
                }
                else
                {
                    encabezadoFactura.VentasFactura = this.CrearVentaFactura(estadoCuenta);
                }

                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<VentaFactura>));
                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(memStream);
                xmlSerializer.Serialize(streamWriter, encabezadoFactura.VentasFactura);
                string ventas = this.RetornaXml(memStream, 5);

                memStream.Close();
                streamWriter.Close();

                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(EncabezadoFactura));
                memStream = new System.IO.MemoryStream();
                streamWriter = new System.IO.StreamWriter(memStream);
                xmlSerializer.Serialize(streamWriter, encabezadoFactura);
                string encabezado = this.RetornaXml(memStream, 1);

                memStream.Close();
                streamWriter.Close();

                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EstadoCuentaDetallado>));
                memStream = new System.IO.MemoryStream();
                streamWriter = new System.IO.StreamWriter(memStream);
                xmlSerializer.Serialize(streamWriter, estadoCuenta.EstadoCuentaDetallado);
                string detalles = this.RetornaXml(memStream, 2);

                memStream.Close();
                streamWriter.Close();

                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EstadoCuentaDetallado>));
                memStream = new System.IO.MemoryStream();
                streamWriter = new System.IO.StreamWriter(memStream);
                xmlSerializer.Serialize(streamWriter, estadoCuenta.EstadoCuentaDetalladoComponentesMaestro);
                string mestroCompuesto = this.RetornaXml(memStream, 3);

                memStream.Close();
                streamWriter.Close();

                string[] valores = this.GuardarInformacionTodaFactura(encabezado, detalles, mestroCompuesto, ventas);

                encabezadoFactura.NumeroFactura = Convert.ToInt32(valores[0]);
                estadoCuenta.NumeroFactura = string.Format("{0}{1}", valores[1], valores[0]);
                estadoCuenta.NumeroFacturaSinPrefijo = valores[0];

                if (estadoCuenta.FacturaAtencion != null)
                {
                    foreach (var item in encabezadoFactura.NoFacturables)
                    {
                        var itemNoFacturable = new NoFacturable()
                        {
                            IdAtencion = item.IdAtencion,
                            ApellidoCliente = string.Empty,
                            NombreCliente = string.Empty,
                            NumeroDocumento = string.Empty,
                            IdExclusion = item.IdExclusion,
                            IdProcesoDetalle = item.IdProcesoDetalle,
                            IdProducto = item.IdProducto,
                            CodigoProducto = item.CodigoProducto,
                            NombreProducto = item.NombreProducto,
                            IdVenta = item.IdVenta,
                            NumeroVenta = item.NumeroVenta,
                            CantidadProducto = item.CantidadProducto,
                            NumeroFactura = encabezadoFactura.NumeroFactura
                        };
                        this.GuardarInformacionNoFacturable(itemNoFacturable);
                    }
                }

                var movimientosTesoreria = new List<FacturaAtencionMovimiento>();
                if (estadoCuenta.FacturaAtencion != null && estadoCuenta.FacturaAtencion.Count > 0)
                {
                    FachadaFacturacion fachada = new FachadaFacturacion();
                    List<FacturaAtencionMovimiento> movientosTesoreriaActual = fachada.ConsultarMovimientosTesoreria(estadoCuenta.IdProceso);

                    var conceptosCobro = this.HomologarConceptosCobro(estadoCuenta.AtencionActiva, estadoCuenta);
                    estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro = conceptosCobro;
                    estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria = movientosTesoreriaActual;
                    estadoCuenta.TipoFacturacion = facturaCompuesta.TipoFacturacion;
                    this.CruzarConceptosCobro(estadoCuenta.FacturaAtencion.FirstOrDefault(), estadoCuenta);

                    if (estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro != null
                        && estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro.Count > 0)
                    {
                        foreach (var item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro)
                        {
                            item.NumeroFactura = encabezadoFactura.NumeroFactura;
                            if (item.Actualizar == true && item.DepositoParticular == false)
                            {
                                this.ActualizarConceptosCobro(item);
                            }
                        }
                    }

                    if (estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria != null
                        && estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count > 0)
                    {
                        foreach (var item in estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria)
                        {
                            if (item.Actualizar == true)
                            {
                                this.ActualizarMovimientosTesoreria(item);
                            }
                        }
                    }
                }

                if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionPaquete)
                {
                    if (estadoCuenta.FacturaPaquetes != null)
                    {
                        this.GuardarInformacionPaquetesFactura(encabezadoFactura, estadoCuenta, facturaCompuesta);
                    }
                }

                this.ValidarEstadoCuentaEncabezadoParticular(facturaCompuesta, estadoCuenta);

                this.ActualizarEstadoProcesoFactura(new ProcesoFactura() { IdProceso = facturaCompuesta.EstadoCuentaEncabezado.IdProceso, IdEstado = (int)ProcesoFactura.EstadoProceso.Facturado });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return facturaCompuesta.EstadoCuentaEncabezado;
        }

        /// <summary>
        /// M?todo que almacena la informacion de la cabezera de la factura.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>
        /// Encabezado estado de cuentas.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFacturaCompuesta(FacturaCompuesta facturaCompuesta)
        {
            // Validación información de da data 
            if (this.ValidarFacturaCompuesta(facturaCompuesta))
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 15, 0)))
                {
                    try
                    {
                        // declaración de variables
                        var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
                        var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;

                        encabezadoFactura.VentasFactura = this.CrearVentaFactura(estadoCuenta);
                        encabezadoFactura.NumeroFactura = this.GuardarInformacionEncabezadoFactura(encabezadoFactura);
                        estadoCuenta.NumeroFactura = string.Format("{0}{1}", encabezadoFactura.PrefijoNumeracion, encabezadoFactura.NumeroFactura);
                        estadoCuenta.NumeroFacturaSinPrefijo = encabezadoFactura.NumeroFactura.ToString();

                        // Ajuste del proceso de iteración 
                        bool resultadoProcesoDetallado = this.CuentaDetallado(facturaCompuesta);

                        if (!resultadoProcesoDetallado)
                        {
                            throw new Exception();
                        }

                        if (estadoCuenta.FacturaAtencion != null)
                        {
                            bool retornoProcesoEstadoCuenta = this.EstadoCuenta(facturaCompuesta);
                        }

                        bool retornoProcesoComplementario = this.ProcesoAdicionalFacturacionComplementaria(facturaCompuesta);

                        if (!retornoProcesoComplementario)
                        {
                            throw new Exception();
                        }

                        this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);
                        this.GuardarInformacionFacturaResponsable(facturaCompuesta, estadoCuenta);
                        this.ActualizaEstadoVenta(estadoCuenta);
                        this.CrearEstadoCuentaContabilidadFactura(estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);
                        this.ActualizarIdCuentaFactura(estadoCuenta.ConsecutivoCartera, encabezadoFactura.NumeroFactura);
                        this.CrearCuentasCartera(facturaCompuesta);
                        this.ActualizarEstadoProcesoFactura(new ProcesoFactura() { IdProceso = facturaCompuesta.EstadoCuentaEncabezado.IdProceso, IdEstado = (int)ProcesoFactura.EstadoProceso.Facturado });

                        transaccion.Complete();
                    }
                    catch (Exception)
                    {
                        transaccion.Dispose();
                        this.ActualizarEstadoProcesoFactura(new ProcesoFactura() { IdProceso = facturaCompuesta.EstadoCuentaEncabezado.IdProceso, IdEstado = (int)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta });
                        throw new Exception(string.Format("Ocurrio un problema, con la generación de la factura. Por favor intente de nuevo."));
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("Ocurrio un problema, con la generación de la factura. Por favor intente de nuevo."));
            }

            return facturaCompuesta.EstadoCuentaEncabezado;
        }

        /// <summary>
        /// Método que almacena la informacion de la cabezera de la factura - Facturación No Clínico.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>
        /// Encabezado estado de cuentas.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 24/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFacturaCompuestaNC(FacturaCompuesta facturaCompuesta)
        {
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;

            try
            {
                encabezadoFactura.VentasFactura = this.CrearVentaFactura(estadoCuenta);
                encabezadoFactura.NumeroFactura = this.GuardarInformacionEncabezadoFacturaNC(encabezadoFactura, estadoCuenta.InformacionFactura.IdModulo);
                estadoCuenta.NumeroFactura = string.Format("{0}{1}", encabezadoFactura.PrefijoNumeracion, encabezadoFactura.NumeroFactura);
                estadoCuenta.NumeroFacturaSinPrefijo = encabezadoFactura.NumeroFactura.ToString();

                foreach (var item in estadoCuenta.EstadoCuentaDetallado)
                {
                    item.CodigoEntidad = encabezadoFactura.CodigoEntidad;
                    item.CodigoSeccion = encabezadoFactura.CodigoSeccion;
                    item.CodigoConcepto = String.Empty;
                    item.CodigoUnidad = String.Empty;
                    item.CodigoTipoRelacion = String.Empty;
                    item.MetodoConfiguracion = String.Empty;
                    item.CantidadFacturaDetalle = item.Cantidad;
                    item.IdTransaccion = encabezadoFactura.VentasFactura.FirstOrDefault<VentaFactura>().IdTransaccion;
                    item.NumeroFactura = encabezadoFactura.NumeroFactura;
                    item.IdTipoMovimiento = encabezadoFactura.IdTipoMovimiento;
                    item.CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoFacturacion;
                    item.IdTercero = encabezadoFactura.IdTercero;
                    item.IdManual = estadoCuenta.FacturaAtencion.FirstOrDefault<FacturaAtencion>().IdManual;
                    item.VigenciaTarifa = estadoCuenta.FacturaAtencion.FirstOrDefault<FacturaAtencion>().VigenciaManual;
                    this.GuardarInformacionDetalleFacturaNC(item);
                }

                foreach (var item in encabezadoFactura.VentasFactura)
                {
                    this.GuardarInformacionVentaFactura(item, estadoCuenta.NumeroFacturaSinPrefijo);
                }

                this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);

                this.ActualizaEstadoVenta(estadoCuenta);

                this.CrearCuentasCarteraNC(facturaCompuesta);
                this.ActualizarEstadoProcesoFacturaNC(new ProcesoFactura() { IdProceso = facturaCompuesta.EstadoCuentaEncabezado.IdProceso, IdEstado = (int)ProcesoFactura.EstadoProceso.Facturado });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return facturaCompuesta.EstadoCuentaEncabezado;
        }

        /// <summary>
        /// Metodo para Almacenar el Encabezado del Proceso para Facturación No Clínica.
        /// </summary>
        /// <param name="procesoFactura">Entidad que contiene todos los parámetros.</param>
        /// <returns>
        /// Indica el proceso donde se realizaran las operaciones de almacenamiento.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFacturaNC(ProcesoFactura procesoFactura)
        {
            try
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                procesoFactura.IdProceso = fachada.GuardarEncabezadoProcesoNC(procesoFactura);

                foreach (var detalle in procesoFactura.Detalles)
                {
                    detalle.IdProceso = procesoFactura.IdProceso;
                    detalle.IdProcesoDetalle = fachada.GuardarDetalleProcesoNC(detalle);
                }

                return this.IniciarProceso(procesoFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guardar Informacion Factura Actividades Paquetes.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Estado Cuenta Encabezado.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Factura Actividades Paquetes.
        /// </remarks>
        public EstadoCuentaEncabezado GuardarInformacionFacturaPaquetes(FacturaCompuesta facturaCompuesta)
        {
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    facturaCompuesta = this.InicializarEncabezadoGuardadoPaquetes(facturaCompuesta);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw new Exception(ex.Message);
                }
            }

            return facturaCompuesta.EstadoCuentaEncabezado;
        }

        /// <summary>
        /// Guarda la informacion del movimiento de cartera.
        /// </summary>
        /// <param name="movimientoCartera">The movimiento cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarInformacionMovimientoCartera(MovimientoCartera movimientoCartera)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarMovimientoCartera(movimientoCartera);
        }

        /// <summary>
        /// Guarda la informacion de los No Facturable.
        /// </summary>
        /// <param name="itemNoFacturable">The no facturable.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void GuardarInformacionNoFacturable(NoFacturable itemNoFacturable)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarNoFacturable(itemNoFacturable);
        }

        /// <summary>
        /// Metodo para almacenar las atenciones a facturar segun la vinculación.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 07/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<EstadoCuentaEncabezado> GuardarInformacionProceso(ProcesoFactura procesoFactura)
        {
            try
            {
                if (procesoFactura.IdProceso == 0)
                {
                    FachadaFacturacion fachada = new FachadaFacturacion();
                    procesoFactura.IdProceso = fachada.GuardarEncabezadoProceso(procesoFactura);

                    foreach (var detalle in procesoFactura.Detalles)
                    {
                        detalle.IdProceso = procesoFactura.IdProceso;
                        detalle.IdProcesoDetalle = fachada.GuardarDetalleProceso(detalle);
                    }
                }

                return this.IniciarProcesoMultiple(procesoFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para almacenar las atenciones a facturar segun la vinculación.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 07/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<EstadoCuentaEncabezado> GuardarInformacionProcesoPaquetes(ProcesoFactura procesoFactura)
        {
            try
            {
                if (procesoFactura.IdProceso == 0)
                {
                    FachadaFacturacion fachada = new FachadaFacturacion();
                    procesoFactura.IdProceso = fachada.GuardarEncabezadoProceso(procesoFactura);

                    foreach (var detalle in procesoFactura.Detalles)
                    {
                        detalle.IdProceso = procesoFactura.IdProceso;
                        detalle.IdProcesoDetalle = fachada.GuardarDetalleProceso(detalle);
                    }
                }

                return this.IniciarProcesoPaquete(procesoFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guardar Informacion Toda Factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Cadena String.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Toda Factura.
        /// </remarks>
        public string[] GuardarInformacionTodaFactura(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarInformacionTodaFactura(encabezado, detalle, maestroComponentes, ventas);
        }

        /// <summary>
        /// Guardar Informacion Toda Factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Cadena String.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Toda Factura.
        /// </remarks>
        public string[] GuardarInformacionTodaFacturaPaquetes(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarInformacionTodaFacturaPaquetes(encabezado, detalle, maestroComponentes, ventas);
        }

        /// <summary>
        /// Guarda la venta de la factura.
        /// </summary>
        /// <param name="ventaFactura">The venta factura.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void GuardarInformacionVentaFactura(VentaFactura ventaFactura, string numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarVentaFactura(ventaFactura, numeroFactura);
        }

        /// <summary>
        /// Metodo para Modificar El tercero.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Id del Tercero Modificado.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarModificacionTercero(Tercero tercero)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarModificacionTercero(tercero);
        }

        /// <summary>
        /// Metodo para guardar ventas y sus productos.
        /// </summary>
        /// <param name="productoAuditar">The producto auditar.</param>
        /// <param name="productoventa">The productoventa.</param>
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 14/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarModificacionVentas(List<ProductoVenta> productoAuditar, List<ProductoVenta> productoventa)
        {
            int resultado = 0;
            foreach (ProductoVenta producto in productoAuditar)
            {
                AuditoriaVentaDetalle ventadetalle = new AuditoriaVentaDetalle()
                {
                    IdProducto = producto.IdProducto,
                    IdTransaccion = producto.IdTransaccion,
                    Cantidad = Convert.ToInt32(producto.CantidadProducto),
                    Usuario = producto.Usuario,
                    Fecha = DateTime.Now,
                    CodigoEntidad = producto.CodigoEntidad,
                    Estado = producto.Estado,
                    NumeroVenta = producto.NumeroVenta
                };
                var resultado3 = this.AuditoriaVentaDetalle(ventadetalle);
            }

            var agregar = from
                            item in productoventa
                          where
                            item.Estado == ReglasNegocio.EstadoAgregado
                          select
                            item;
            foreach (ProductoVenta producto in agregar)
            {
                VentaDetalle detalle = new VentaDetalle()
                {
                    CodigoEntidad = producto.CodigoEntidad,
                    IdTransaccion = producto.IdTransaccion,
                    NumeroVenta = producto.NumeroVenta,
                    IdProducto = producto.IdProducto,
                    Cantidad = producto.CantidadProducto,
                    ValorVenta = producto.Valor,
                    IdTipoProducto = producto.IdTipoProducto,
                    IdTercero = producto.HonorarioMedico.ResponsableHonorario.IdTercero,
                    IdPersonal = producto.HonorarioMedico.IdPersonal
                };

                var resultado2 = this.GuardarVentaDetalle(detalle);
            }

            var borrar = from
                            item in productoAuditar
                         where
                           item.Estado == ReglasNegocio.EstadoBorrado
                         select
                           item;
            foreach (ProductoVenta producto in borrar)
            {
                VentaDetalle detalle = new VentaDetalle()
                {
                    CodigoEntidad = producto.CodigoEntidad,
                    IdTransaccion = producto.IdTransaccion,
                    NumeroVenta = producto.NumeroVenta,
                    IdProducto = producto.IdProducto,
                    Cantidad = producto.CantidadProducto,
                    ValorVenta = producto.Valor,
                    IdTipoProducto = producto.IdTipoProducto,
                    IdTercero = producto.HonorarioMedico.ResponsableHonorario.IdTercero,
                    IdPersonal = producto.HonorarioMedico.IdPersonal
                };
                var resultado2 = this.BorrarVentaDetalle(detalle);
            }

            return resultado;
        }

        /// <summary>
        /// Actualiza las ventas y sus productos sin pasar por lo métodos de auditoría.
        /// </summary>
        /// <param name="detalleVenta">Lista de VentaDetalle conteniendo todos los nuevos productos asociados a la venta.</param>
        /// <param name="detalleVentaBorrar">The detalle venta borrar.</param>
        /// <returns>
        /// Resultado de la operación.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 18/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarModificacionVentasSinAuditoria(List<VentaDetalle> detalleVenta, List<VentaDetalle> detalleVentaBorrar)
        {
            int resultado = 1;

            foreach (VentaDetalle detalle in detalleVenta)
            {
                try
                {
                    var resultado2 = this.GuardarVentaDetalle(detalle);

                    if (resultado2 == 0)
                    {
                        resultado = resultado2;
                    }
                }
                catch (Exception)
                {
                    resultado = 0;
                    return resultado;
                }
            }

            foreach (VentaDetalle detalle in detalleVentaBorrar)
            {
                try
                {
                    var resultado2 = this.BorrarVentaDetallesNC(detalle);

                    if (resultado2 == 0)
                    {
                        resultado = resultado2;
                    }
                }
                catch (Exception)
                {
                    resultado = 0;
                    return resultado;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Método que guarda los movimientos de ajuste de saldo de cartera de la factura anulada.
        /// </summary>
        /// <param name="numeroFactura">Número de la factura a anular.</param>
        /// <param name="usuario">Nombre de usuario que realiza la anulación.</param>
        /// <param name="identificadorNotaCredito">The id nota credito.</param>
        /// <returns>
        /// Resultado de la operación.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 07/10/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarMovimientoCarteraAjusteSaldo(int numeroFactura, string usuario, int identificadorNotaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarMovimientoCarteraAjusteSaldo(numeroFactura, usuario, identificadorNotaCredito);
        }

        /// <summary>
        /// Metodo que guarda los movimientos de cartera de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id del ultimo movimiento guardado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 02/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarMovimientoCarteraAnulacion(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarMovimientoCarteraAnulacion(notaCredito);
        }

        /// <summary>
        /// Metodo para guardar recargo.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>
        /// Indica si Se realiza la actualizaci n.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez - INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarRecargo(Recargo recargo)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var resultado = fachada.GuardarRecargo(recargo);
            return resultado;
        }

        /// <summary>
        /// Guarda la informacion detallada del responsable.
        /// </summary>
        /// <param name="responsable">The responsable.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarResponsableFactura(Responsable responsable)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarResponsableFactura(responsable);
        }

        /// <summary>
        /// Metodo Para Guardar El Tercero.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Id Tercero Creado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarTercero(Tercero tercero)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarTercero(tercero);
        }

        /// <summary>
        /// Metodo para guardar ventas.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 12/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarVenta(Venta venta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarVenta(venta);
        }

        /// <summary>
        /// Metodo Para Guardar El Tercero.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>
        /// Id Tercero Creado.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarVentaDetalle(VentaDetalle venta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarVentaDetalle(venta);
        }

        /// <summary>
        /// Metodo para guardar ventas y susu productos.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 13/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarVentas(Venta venta, List<VentaDetalle> detalle)
        {
            int resultado;
            FachadaFacturacion fachada = new FachadaFacturacion();

            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    resultado = fachada.GuardarVenta(venta);

                    foreach (VentaDetalle producto in detalle)
                    {
                        producto.NumeroVenta = resultado;
                        var resultado2 = fachada.GuardarVentaDetalle(producto);
                    }

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para insertar la vinculaci n.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Registro insertado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int GuardarVinculacion(Vinculacion vinculacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarVinculacion(vinculacion);
        }

        /// <summary>
        /// Metodo para guardar las ventas vinculadas.
        /// </summary>
        /// <param name="vinculaciones">The vinculacion.</param>
        /// <returns>Id de la Vinculacion de la Venta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public bool GuardarVinculacionVentas(List<VinculacionVenta> vinculaciones)
        {
            int identificadorCreacionLog = 0;
            FachadaFacturacion fachada = new FachadaFacturacion();
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    foreach (var item in vinculaciones)
                    {
                        identificadorCreacionLog = fachada.GuardarVinculacionVentas(item);
                        var indicadorExito = fachada.ActualizarVinculacionVentas(item);

                        if (identificadorCreacionLog == 0 || indicadorExito <= 0)
                        {
                            transactionScope.Dispose();
                            throw new Excepcion.Negocio(string.Format(ReglasNegocio.VinculacionVentas, item.NumeroVenta, item.IdAtencionPadre, item.IdAtencionVinculada));
                        }
                    }

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }
            }

            return true;
        }

        /// <summary>
        /// Métod para guardar los productos del paquete armado.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Retorna numero de venta.</returns>
        public int InsertarVentaPaquete(EstadoCuentaEncabezado estadoCuenta)
        {
            return new FachadaFacturacion().InsertarVentaPaquete(estadoCuenta);
        }

        /// <summary>
        /// Insertars the venta paquetes detalle.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="paquete">The paquete.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <param name="boolEsPyG">if set to <c>true</c> [es py G].</param>
        /// <returns>Retorna numero de lote.</returns>
        public int InsertarVentaPaquetesDetalle(EstadoCuentaEncabezado estadoCuenta, Paquete paquete, int numeroVenta, bool boolEsPyG)
        {
            return new FachadaFacturacion().InsertarVentaPaquetesDetalle(estadoCuenta, paquete, numeroVenta, boolEsPyG);
        }

        /// <summary>
        /// Consulta datos de un cliente.
        /// </summary>
        /// <param name="identificadorCliente">The id cliente.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 16/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Tercero ObtenerCliente(int identificadorCliente)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerCliente(identificadorCliente);
        }

        /// <summary>
        /// Consulta las exclusiones por contrato y manuales filtradas por CodigoEntidad, IdContrato, IdTercero y IdPlan.
        /// </summary>
        /// <param name="exclusion">Entidad con los propiedades que se necesitan como parametros para hacer la consulta.</param>
        /// <returns>Lista de exclusiones.</returns>
        public List<ExclusionFacturaActividades> ObtenerExclusiones(ExclusionFacturaActividades exclusion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerExclusiones(exclusion);
        }

        /// <summary>
        /// Método para obtener el ID del módulo para el proceso de Facturación.
        /// </summary>
        /// <returns>Identificador del Módulo.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 05/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public int ObtenerIdModulo()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerIdModulo();
        }

        /// <summary>
        /// Método para obtener el ID del tipo de Movimiento para Facturación No Clínica.
        /// </summary>
        /// <returns>Identificador del Tipo de Movimiento.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 05/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public int ObtenerIdTipoMovimiento()
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerIdTipoMovimiento();
        }

        /// <summary>
        /// Obteners the reimpresion factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="agrupada">if set to <c>true</c> [agrupada].</param>
        /// <returns>Retorna objeto.</returns>
        public Object ObtenerReimpresionFactura(int numeroFactura, bool agrupada)
        {
            return new FachadaFacturacion().ObtenerReimpresionFactura(numeroFactura, agrupada);
        }

        /// <summary>
        /// Obtener Responsable Venta.
        /// </summary>
        /// <param name="responsablesVentas">The responsables ventas.</param>
        /// <param name="identificadorProducto">The id producto.</param>
        /// <param name="identificadorTransaccion">The id transaccion.</param>
        /// <param name="identificadorVenta">The id venta.</param>
        /// <returns>Venta Responsable.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Obtener Responsable Venta.
        /// </remarks>
        public VentaResponsable ObtenerResponsableVenta(List<VentaResponsable> responsablesVentas, int identificadorProducto, int identificadorTransaccion, int identificadorVenta)
        {
            var resultadoVenta = from item in responsablesVentas
                                 where item.IdProducto == identificadorProducto && item.IdTransaccion == identificadorTransaccion && item.NumeroVenta == identificadorVenta
                                 select item;

            return resultadoVenta.FirstOrDefault();
        }

        /// <summary>
        /// Obtener Responsable Venta.
        /// </summary>
        /// <param name="responsablesVentas">The responsables ventas.</param>
        /// <param name="identificadorProducto">The id producto.</param>
        /// <param name="identificadorTransaccion">The id transaccion.</param>
        /// <param name="identificadorVenta">The id venta.</param>
        /// <param name="componente">The componente.</param>
        /// <returns>
        /// Venta Responsable.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Obtener Responsable Venta.
        /// </remarks>
        public VentaResponsable ObtenerResponsableVentaComponentes(List<VentaResponsable> responsablesVentas, int identificadorProducto, int identificadorTransaccion, int identificadorVenta, string componente)
        {
            var resultadoVenta = from item in responsablesVentas
                                 where item.IdProducto == identificadorProducto && item.IdTransaccion == identificadorTransaccion && item.NumeroVenta == identificadorVenta && item.Componente.CodigoComponente == componente
                                 select item;

            return resultadoVenta.FirstOrDefault();
        }

        /// <summary>
        /// Obtener Responsable Venta.
        /// </summary>
        /// <param name="responsablesVentas">The responsables ventas.</param>
        /// <param name="identificadorProducto">The id producto.</param>
        /// <param name="identificadorTransaccion">The id transaccion.</param>
        /// <param name="identificadorVenta">The id venta.</param>
        /// <returns>
        /// Venta Responsable.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Obtener Responsable Venta.
        /// </remarks>
        public VentaResponsable ObtenerResponsableVentaProductos(List<VentaResponsable> responsablesVentas, int identificadorProducto, int identificadorTransaccion, int identificadorVenta)
        {
            var resultadoVenta = from item in responsablesVentas
                                 where item.IdProducto == identificadorProducto && item.IdTransaccion == identificadorTransaccion && item.NumeroVenta == identificadorVenta
                                 select item;

            return resultadoVenta.FirstOrDefault();
        }

        /// <summary>
        /// Consulta datos de un tercero.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Objeto de tipo tercero.</returns>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 09/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Tercero ObtenerTercero(int identificadorTercero)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerTercero(identificadorTercero);
        }

        /// <summary>
        /// Consulta todos los números de venta asociados a una factura.
        /// Se hace como paso previo a la actualización de los estados de una venta.
        /// </summary>
        /// <param name="notaCredito">Objeto NotaCredito.</param>
        /// <returns>Lista con los números de venta asociados a una factura.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public List<int> ObtenerVentasPorFactura(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ObtenerVentasPorFactura(notaCredito);
        }

        /// <summary>
        /// Metodo de Redondeo.
        /// </summary>
        /// <param name="valor">The valor.</param>
        /// <param name="contrato">The contrato.</param>
        /// <param name="identificadorManual">The id manual.</param>
        /// <returns>
        /// Valor Redondeado.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 21/06/2013.
        /// UltimaModificacionPor: (Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo).
        /// FechaDeUltimaModificacion: (21/08/2014).
        /// DescripcionModificacion: Se agrega el parámetro idManual ya que en ciertos casos el contrato
        /// puede tener configurados varios Redondeos de acuerdo al manual. En esta situación, el redondeo
        /// se aplica mal debido a la posible ambiguedad de los tipos de redondeo. Se modifican también
        /// todas las referencias a este método para incluir el id del manual.
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public decimal Redondeo(decimal valor, int contrato, int identificadorManual)
        {
            decimal retorno = valor;

            if (this.contratoRedondeo != contrato)
            {
                if (identificadorManual != 0)
                {
                    this.ArrRedondeo = this.RedondeoCountryxID(contrato, identificadorManual);

                    if (this.ArrRedondeo == null || this.ArrRedondeo.Count == 0)
                    {
                        this.ArrRedondeo = this.RedondeoCountry(contrato);
                    }
                }
                else
                {
                    this.ArrRedondeo = this.RedondeoCountry(contrato);
                }

                this.contratoRedondeo = contrato;
            }
            else if (contrato == 0 && this.contratoRedondeo == 0)
            {
                this.ArrRedondeo = this.RedondeoCountry(contrato);
            }

            for (int i = 0; i < this.ArrRedondeo.Count; i++)
            {
                List<int> lista = (List<int>)this.ArrRedondeo[i];
                decimal min = lista[0];
                decimal max = lista[1];
                decimal val = lista[2];
                double digito = lista[3];

                double pos = Math.Pow(10, digito);
                double valorFraccion = Convert.ToDouble(valor) / pos;

                decimal faccionDcto = Convert.ToDecimal(valorFraccion - (int)valorFraccion) * 10;

                if (faccionDcto > 9 && faccionDcto < 10)
                {
                    faccionDcto = 9;
                }

                if (faccionDcto >= Convert.ToDecimal(0.000001) && faccionDcto < 1)
                {
                    faccionDcto = 1;
                }

                faccionDcto = Math.Round(faccionDcto, 0);

                ////Aproxima descuento
                if (faccionDcto != 0)
                {
                    if (faccionDcto >= min && faccionDcto <= max)
                    {
                        decimal valsum = Convert.ToDecimal(val) / 10;
                        if (digito == 0)
                        {
                            valsum = Convert.ToDecimal(val);
                        }

                        decimal value = (int)valorFraccion + valsum;
                        retorno = value * Convert.ToDecimal(pos);
                    }
                }
            }

            return retorno;
        }

        /// <summary>
        /// Metodo Redondeo.
        /// </summary>
        /// <param name="valor">Parametro valor.</param>
        /// <param name="cantidadDigitos">The cantidad digitos.</param>
        /// <param name="nombreAproximacion">The nombre aproximacion.</param>
        /// <returns>
        /// Retorna decimal.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Metodo Redondeo.
        /// </remarks>
        public decimal Redondeo(decimal valor, int cantidadDigitos, string nombreAproximacion)
        {
            try
            {
                if (cantidadDigitos <= Convert.ToInt64(valor).ToString().Length || valor == 0)
                {
                    if (cantidadDigitos > 0)
                    {
                        var aproximaciones = this.ConsultarAproximaciones();
                        var aproximacionesFiltrada = this.BuscarAproximacionxNombre(aproximaciones, nombreAproximacion);

                        string sb = "0";

                        if (valor != 0)
                        {
                            if (Convert.ToInt64(valor).ToString().Length == 1 || Convert.ToInt64(valor).ToString().Length < cantidadDigitos)
                            {
                                sb = Convert.ToInt64(valor).ToString();
                            }
                            else if (Convert.ToInt64(valor).ToString().Length > 1 && Convert.ToInt64(valor).ToString().Length >= cantidadDigitos)
                            {
                                sb = Convert.ToInt64(valor).ToString().Substring(Convert.ToInt64(valor).ToString().Length - cantidadDigitos, 1);
                            }
                        }
                        else
                        {
                            sb = Convert.ToInt32(valor).ToString();
                        }

                        var valoraprox = (from
                                             item in aproximacionesFiltrada
                                          where
                                              item.NombreAproximacion == nombreAproximacion
                                              && Convert.ToInt64(sb) >= item.ValorMinimo
                                              && Convert.ToInt64(sb) <= item.ValorMaximo
                                          select
                                              item).FirstOrDefault();

                        long valorR = Convert.ToInt64(sb);
                        long verificar;

                        if (Convert.ToInt32(valoraprox.ValorAproximado.ToString().PadRight(cantidadDigitos, '0')).ToString().StartsWith("1"))
                        {
                            verificar = Convert.ToInt64(Math.Pow(10, cantidadDigitos));
                        }
                        else
                        {
                            verificar = Convert.ToInt64(valoraprox.ValorAproximado.ToString().PadRight(cantidadDigitos, '0'));
                        }

                        long valorfinal = 0;

                        if (valor != 0)
                        {
                            if (Convert.ToInt64(valor).ToString().Length == 1 || Convert.ToInt64(valor).ToString().Length < cantidadDigitos)
                            {
                                valorfinal = Convert.ToInt64(Convert.ToInt32(valor).ToString().PadRight(Convert.ToInt64(valor).ToString().Length, '0'));
                            }
                            else if (Convert.ToInt64(valor).ToString().Length > 1 && Convert.ToInt64(valor).ToString().Length > cantidadDigitos)
                            {
                                valorfinal = Convert.ToInt64(Convert.ToInt64(valor).ToString().Substring(
                                    0,
                                  Convert.ToInt64(valor).ToString().Length - cantidadDigitos).PadRight(Convert.ToInt64(valor).ToString().Length, '0'));
                            }
                        }
                        else
                        {
                            valorfinal = 0;
                        }

                        if (valorfinal.ToString().Length > 1)
                        {
                            return valorfinal + verificar;
                        }
                        else
                        {
                            return verificar;
                        }
                    }
                    else
                    {
                        return valor;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Redondeo Country.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <returns>Array Redondeo.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public System.Collections.ArrayList RedondeoCountry(int contrato)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.RedondeoCountry(contrato);
        }

        /// <summary>
        /// Redondeo Country.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <param name="identificadorManual">identificador del manual</param>
        /// <returns>Array Redondeo.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino- INTERGRUPO\gocampo
        /// FechaDeCreacion: 21/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public System.Collections.ArrayList RedondeoCountryxID(int contrato, int identificadorManual)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.RedondeoCountryxID(contrato, identificadorManual);
        }

        /// <summary>
        /// Validars the atencion bloqueada.
        /// </summary>
        /// <param name="identificadorAtencion">El identificador atencion.</param>
        /// <returns>Retorna la atención bloqueada.</returns>
        public string ValidarAtencionBloqueada(int identificadorAtencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ValidarAtencionBloqueada(identificadorAtencion);
        }

        /// <summary>
        /// Validar condiciones de ajuste de tarifa.
        /// </summary>
        /// <param name="condicionesTarifa">The condiciones tarifa.</param>
        /// <param name="tipoTarifa">The tipo tarifa.</param>
        /// <returns>Condicion de tarifa.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 19/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public CondicionTarifa ValidarCondicionesAjusteTarifa(List<CondicionTarifa> condicionesTarifa, TipoTarifa tipoTarifa)
        {
            if (condicionesTarifa == null)
            {
                condicionesTarifa = new List<CondicionTarifa>();
            }

            var resultado = from condicion in condicionesTarifa
                            where condicion.TipoCondicionTarifa.Equals(tipoTarifa)
                            select condicion;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Validar condiciones de cubrimiento.
        /// </summary>
        /// <param name="condicionesCubrimiento">The condiciones cubrimiento.</param>
        /// <param name="tipoCubrimiento">The tipo cubrimiento.</param>
        /// <returns>
        /// Condicion de cubrimiento.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 20/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public CondicionTarifa ValidarCondicionesCubrimiento(List<CondicionCubrimiento> condicionesCubrimiento, TipoCubrimiento tipoCubrimiento)
        {
            if (condicionesCubrimiento == null)
            {
                condicionesCubrimiento = new List<CondicionCubrimiento>();
            }

            var resultado = from condicion in condicionesCubrimiento
                            where condicion.TipoCondicionTarifa.Equals(tipoCubrimiento)
                            select condicion;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Método que Valida si existen movimientos de Resta al Saldo diferentes al 181
        /// para facturas que están en estado P en ATRCUENTASCARTERA.
        /// </summary>
        /// <param name="numeroFactura">Número de la Factura a Validar.</param>
        /// <returns>Indicador de existencia de los movimientos.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 07/10/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ValidarMovimientosRS(int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ValidarMovimientosRS(numeroFactura);
        }

        /// <summary>
        /// Valida si el usuario tiene un rol especifico.
        /// </summary>
        /// <param name="usuario">Parametro usuario.</param>
        /// <param name="rol">Parametro rol.</param>
        /// <returns>
        /// Retorna un entero.
        /// </returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom.
        /// FechaDeCreacion: 08/05/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ValidarRolUsuario(string usuario, string rol)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ValidarRolUsuario(usuario, rol);
        }

        #endregion Metodos Publicos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para actualizar los pagos realizados.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ActualizacionPagos(List<FacturaAtencion> atenciones, EstadoCuentaEncabezado estadoCuenta)
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
                this.CruzarConceptosCobro(atencion, estadoCuenta);
            }
        }

        /// <summary>
        /// Actualiza el estado de la venta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ActualizaEstadoVenta(EstadoCuentaEncabezado estadoCuenta)
        {
            try
            {
                var atencionesVentas = from
                                           item in estadoCuenta.FacturaAtencion
                                       from
                                           detalle in item.Detalle
                                       group detalle by
                                       new
                                       {
                                           IdAtencion = item.IdAtencion,
                                           NumeroVenta = detalle.NumeroVenta,
                                           IdVenta = detalle.IdTransaccion
                                       }
                                           into ventas
                                           select
                                                ventas;

                foreach (var atenciones in atencionesVentas)
                {
                    if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionNoClinico)
                    {
                        VentaEstadoFactura ventaEstadoFactura = new VentaEstadoFactura
                        {
                            CodigoEntidad = estadoCuenta.CodigoEntidad,
                            IdAtencion = atenciones.Key.IdAtencion,
                            IdTransaccion = atenciones.Key.IdVenta,
                            NumeroVenta = atenciones.Key.NumeroVenta,
                            EstadoVenta = "T"
                        };

                        this.ActualizarEstadoVentaFacturaNC(ventaEstadoFactura);
                    }
                    else
                    {
                        FachadaFacturacion fachada = new FachadaFacturacion();
                        bool ventaTerminada = fachada.ValidarVentaTerminada(Convert.ToInt32(estadoCuenta.NumeroFacturaSinPrefijo), atenciones.Key.NumeroVenta, atenciones.Key.IdVenta, estadoCuenta.CodigoEntidad, estadoCuenta.IdContrato);

                        if (ventaTerminada)
                        {
                            VentaEstadoFactura ventaEstadoFactura = new VentaEstadoFactura
                            {
                                CodigoEntidad = estadoCuenta.CodigoEntidad,
                                IdAtencion = atenciones.Key.IdAtencion,
                                IdTransaccion = atenciones.Key.IdVenta,
                                NumeroVenta = atenciones.Key.NumeroVenta,
                                EstadoVenta = "T"
                            };

                            this.ActualizarEstadoVentaFactura(ventaEstadoFactura);
                        }
                        else
                        {
                            VentaEstadoFactura ventaEstadoFactura = new VentaEstadoFactura
                            {
                                CodigoEntidad = estadoCuenta.CodigoEntidad,
                                IdAtencion = atenciones.Key.IdAtencion,
                                IdTransaccion = atenciones.Key.IdVenta,
                                NumeroVenta = atenciones.Key.NumeroVenta,
                                EstadoVenta = "P"
                            };

                            this.ActualizarEstadoVentaFactura(ventaEstadoFactura);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Actualizas the estado venta.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <remarks>
        /// Autor: Sin información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void ActualizaEstadoVentaPaquetes(string numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.ActualizaEstadoVentaPaquetes(numeroFactura);
        }

        /// <summary>
        /// Metodo para actualizar ventas asociada.
        /// </summary>
        /// <param name="ventaProductoRelacion">The venta producto relacion.</param>
        /// <returns>
        /// Id del registro modificado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int ActualizarVentaProductoRelacion(VentaProductoRelacion ventaProductoRelacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.ActualizarVentaProductoRelacion(ventaProductoRelacion);
        }

        /// <summary>
        /// Adicionar Producto Paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="atencion">The atencion.</param>
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 13/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Adicionar Producto Paquete.
        /// </remarks>
        private FacturaAtencionDetalle AdicionarProductoPaquete(Paquete paquete, FacturaAtencion atencion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            FacturaAtencionDetalle detallePaquete;
            GrupoProducto grupoProducto = new GrupoProducto();
            Producto productoPaquete = new Producto();
            productoPaquete.IdProducto = paquete.IdPaquete;
            productoPaquete = fachada.ConsultarProductoxIdProducto(productoPaquete);
            grupoProducto.IdGrupo = productoPaquete.IdGrupoProducto;
            grupoProducto = fachada.ConsultarGrupoProductoxIdGrupoProducto(grupoProducto);

            detallePaquete = new FacturaAtencionDetalle()
            {
                IdAtencion = atencion.IdAtencion,
                IdPlan = atencion.IdPlan,
                IdTipoAtencion = atencion.IdTipoAtencion,
                IdProducto = productoPaquete.IdProducto,
                IdProductoRelacion = productoPaquete.IdProducto,
                IdTipoProducto = productoPaquete.IdTipoProducto,
                NombreProducto = productoPaquete.Nombre,
                IdGrupoProducto = (short)productoPaquete.IdGrupoProducto,
                NombreGrupo = grupoProducto.Nombre,
                ValorUnitario = 0,
                ValorTotal = 0,
                ValorProductos = 0,
                CantidadProducto = paquete.Cantidad,
                CodigoProducto = productoPaquete.CodigoProducto,
                CodigoGrupo = grupoProducto.CodigoGrupo,
                Componente = "NA",
                ValorRecargo = 0,
                ValorTotalRecargo = 0,
                ValorDescuento = 0,
                ValorTotalDescuento = 0,
                FechaVenta = DateTime.Now,
                NumeroVenta = 0,
                IndPaquete = 1,
                ValorPaquete = 0,
                VentaComponentes = new List<VentaComponente>()
            };
            return detallePaquete;
        }

        /// <summary>
        /// Aplica la tarifa ya sea del manual principal, del alterno o del institucional.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <param name="identificadorManual">The id manual.</param>
        /// <param name="fechaVigencia">The fecha vigencia.</param>
        /// <returns>
        /// Detalle de Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private DetalleTarifa AplicaCondicionTarifa(BaseValidacion detalle, CondicionTarifa condicionTarifa, int identificadorManual, DateTime? fechaVigencia)
        {
            IEnumerable<DetalleTarifa> tarifa = null;
            DetalleTarifa detalleTarifa = null;

            var detalleTarifaManual = this.ConsultarDetalleTarifaXManual(new DetalleTarifa()
            {
                IdManual = identificadorManual,
                IdProducto = detalle.IdProducto
            });

            if (condicionTarifa != null)
            {
                tarifa = from
                             item in detalleTarifaManual
                         orderby
                             item.FechaFinal descending
                         where
                             (!fechaVigencia.HasValue || item.FechaVigenciaTarifa.Date == fechaVigencia.Value.Date)
                             && detalle.FechaVenta.Date >= condicionTarifa.VigenciaCondicion
                         select
                             item;

                detalleTarifa = tarifa.FirstOrDefault();
            }
            else
            {
                if (fechaVigencia != null)
                {
                    tarifa = from item in detalleTarifaManual
                             where
                                 detalle.FechaVenta.Date >= item.FechaInicial.Date && detalle.FechaVenta.Date <= item.FechaFinal.Date
                                 && item.FechaVigenciaTarifa >= fechaVigencia
                             select
                                 item;

                    if ((tarifa != null) && (tarifa.ToList().Count() == 0))
                    {
                        tarifa = from item in detalleTarifaManual
                                 where
                                     detalle.FechaVenta.Date >= item.FechaInicial.Date && detalle.FechaVenta.Date <= item.FechaFinal.Date
                                 select
                                     item;
                    }
                }
                else
                {
                    tarifa = from item in detalleTarifaManual
                             where
                                 detalle.FechaVenta.Date >= item.FechaInicial.Date && detalle.FechaVenta.Date <= item.FechaFinal.Date
                             select
                                 item;
                }

                detalleTarifa = tarifa.FirstOrDefault();
            }

            return detalleTarifa;
        }

        /// <summary>
        /// Metodo para aplicar ajuste de tarifa.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <param name="porcentaje">If set to <c>true</c> [porcentaje].</param>
        /// <returns>
        /// Detalle de tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dpreciado.
        /// FechaDeCreacion: 20/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private DetalleTarifa AplicarAjusteTarifa(CondicionContrato condicionContrato, BaseValidacion detalle, TipoFacturacion tipoFacturacion, CondicionTarifa condicionTarifa, bool porcentaje)
        {
            DetalleTarifa detalleTarifa = null;

            detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManual, condicionContrato.FechaVigencia);
            this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManual);

            if (detalleTarifa == null)
            {
                detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManualAlterno, condicionContrato.FechaVigenciaAlterna);
                this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManualAlterno);

                if (detalleTarifa == null)
                {
                    detalleTarifa = this.AplicaCondicionTarifa(detalle, condicionTarifa, condicionContrato.IdManualInstitucional, null);
                    this.CondicionContratoResultado(condicionContrato, condicionContrato.IdManualInstitucional);

                    if (detalleTarifa == null)
                    {
                        throw new Excepcion.Negocio(string.Format(ReglasNegocio.DetalleTarifa_AjusteTarifa, detalle.IdProducto, detalle.NombreProducto, detalle.IdAtencion));
                    }
                }
            }

            return detalleTarifa;
        }

        /// <summary>
        /// Permite calcular el valor una vez obtenidos los conceptos de descuentos y recargos del producto.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarCalculos(BaseValidacion detalle, CondicionContrato condicionContrato)
        {
            decimal valorProducto = 0;

            TipoTarifa tipoTarifa = new TipoTarifa();

            if (detalle.CondicionesTarifa == null)
            {
                valorProducto = this.ObtenerValorTarifa(detalle, condicionContrato, ref tipoTarifa) * (condicionContrato.Porcentaje / 100);
            }
            else
            {
                valorProducto = this.ObtenerValorTarifa(detalle, condicionContrato, ref tipoTarifa);
            }

            detalle.ValorUnitario = this.Redondeo(valorProducto, condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorRecargo = this.Redondeo(this.ObtenerValorRecargo(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorDescuento = this.Redondeo(this.ObtenerValorDescuento(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorProductos = detalle.ValorUnitario * detalle.CantidadFacturar;
            detalle.ValorTotalRecargo = detalle.ValorRecargo * detalle.CantidadFacturar;
            detalle.ValorTotalDescuento = detalle.ValorDescuento * detalle.CantidadFacturar;
            detalle.ValorTotal = (detalle.ValorProductos + detalle.ValorTotalRecargo) - detalle.ValorTotalDescuento;
        }

        /// <summary>
        /// Permite calcular el valor una vez obtenidos los conceptos de descuentos y recargos del producto. Facturación por Relación.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 26/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarCalculosRel(BaseValidacion detalle, CondicionContrato condicionContrato)
        {
            decimal valorProducto = 0, valorUnitario = 0;

            TipoTarifa tipoTarifa = new TipoTarifa();

            if (detalle.CondicionesTarifa == null)
            {
                valorProducto = this.ObtenerValorTarifa(detalle, condicionContrato, ref tipoTarifa) * (condicionContrato.Porcentaje / 100);
            }
            else
            {
                valorProducto = this.ObtenerValorTarifa(detalle, condicionContrato, ref tipoTarifa);
            }

            valorUnitario = this.CalcularValorUnitario(detalle.DetalleTarifa, condicionContrato, detalle);
            valorProducto = valorUnitario > 0 ? valorUnitario : valorProducto;

            if (tipoTarifa == TipoTarifa.ValorPropio || tipoTarifa == TipoTarifa.AjusteTarifas || tipoTarifa == TipoTarifa.NoDefinido)
            {
                if (valorProducto == 0)
                {
                    valorProducto = detalle.ValorOriginal;
                }
            }

            detalle.ValorUnitario = this.Redondeo(valorProducto, condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorRecargo = this.Redondeo(this.ObtenerValorRecargo(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorDescuento = this.Redondeo(this.ObtenerValorDescuento(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);
            detalle.ValorProductos = detalle.ValorUnitario * detalle.CantidadFacturar;
            detalle.ValorTotalRecargo = detalle.ValorRecargo * detalle.CantidadFacturar;
            detalle.ValorTotalDescuento = detalle.ValorDescuento * detalle.CantidadFacturar;
            detalle.ValorTotal = (detalle.ValorProductos + detalle.ValorTotalRecargo) - detalle.ValorTotalDescuento;
        }

        /// <summary>
        /// Meotodo para Aplicar las Condiciones de Factura.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <param name="productoVinculacionActual">The producto vinculacion actual.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void AplicarCondicionesFactura(FacturaAtencionDetalle detalle, CondicionTarifa condicionFactura, List<FacturaAtencionDetalle> productoVinculacionActual, List<FacturaAtencionDetalle> detalleVinculacion)
        {
            // Evaluar condiciones de factura
            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                if (condicionFactura.ValorPropio >= 1)
                {
                    decimal cantidadVinculacion = 1;
                    cantidadVinculacion = Math.Floor(condicionFactura.ValorPropio / detalle.ValorBruto);

                    if (cantidadVinculacion == 0)
                    {
                        detalleVinculacion.Add(detalle);
                        productoVinculacionActual.Remove(detalle);
                    }
                    else
                    {
                        if (cantidadVinculacion > detalle.CantidadFacturar)
                        {
                            cantidadVinculacion = detalle.CantidadFacturar;
                        }

                        if (condicionFactura.ValorPropio >= (detalle.ValorBruto == 0 ? detalle.ValorOriginal : detalle.ValorBruto))
                        {
                            if (detalle.CantidadFacturar > 1)
                            {
                                condicionFactura.ValorPropio -= detalle.ValorBruto * cantidadVinculacion;
                                if ((detalle.CantidadProducto - cantidadVinculacion) > 0)
                                {
                                    var copiaDetalle = detalle.CopiarObjeto();

                                    copiaDetalle.CantidadProducto = detalle.CantidadProducto - cantidadVinculacion;
                                    this.CalcularValoresProducto(copiaDetalle);
                                    detalleVinculacion.Add(copiaDetalle);
                                }

                                this.CalcularValoresProducto(detalle);
                                productoVinculacionActual.Add(detalle);
                            }
                            else
                            {
                                condicionFactura.ValorPropio -= detalle.ValorBruto * cantidadVinculacion;
                                this.CalcularValoresProducto(detalle);
                                productoVinculacionActual.Add(detalle);
                            }
                        }
                        else
                        {
                            if (detalle.CantidadFacturar > 1)
                            {
                                condicionFactura.ValorPropio -= detalle.ValorBruto * cantidadVinculacion;
                                if ((detalle.CantidadProducto - cantidadVinculacion) > 0)
                                {
                                    var copiaDetalle = detalle.CopiarObjeto();

                                    copiaDetalle.CantidadProducto = detalle.CantidadProducto - cantidadVinculacion;
                                    this.CalcularValoresProducto(copiaDetalle);
                                    detalleVinculacion.Add(copiaDetalle);
                                }

                                detalle.CantidadProducto = cantidadVinculacion;
                                this.CalcularValoresProducto(detalle);
                                productoVinculacionActual.Add(detalle);
                            }
                            else
                            {
                                condicionFactura.ValorPropio -= detalle.ValorBruto * cantidadVinculacion;
                                if ((detalle.CantidadProducto - cantidadVinculacion) > 0)
                                {
                                    var copiaDetalle = detalle.CopiarObjeto();
                                    copiaDetalle.CantidadProducto = detalle.CantidadProducto - cantidadVinculacion;
                                    this.CalcularValoresProducto(copiaDetalle);
                                    detalleVinculacion.Add(copiaDetalle);
                                }

                                detalle.CantidadProducto = cantidadVinculacion;
                                this.CalcularValoresProducto(detalle);
                                productoVinculacionActual.Add(detalle);
                            }
                        }
                    }
                }
                else
                {
                    detalleVinculacion.Add(detalle);
                    productoVinculacionActual.Remove(detalle);
                }
            }
        }

        /// <summary>
        /// Metodo para actualizar conceptos de cobro.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarReglaConceptoCobro(FacturaAtencion atencion)
        {
            FacturaAtencionConceptoCobro conceptoCobro = null;
            var valorConceptoCobro = atencion.ConceptosCobro.Sum(item => item.ValorConcepto);

            if (atencion.ValorConcepto != valorConceptoCobro)
            {
                if (atencion.ConceptosCobro.Count == 1)
                {
                    conceptoCobro = atencion.ConceptosCobro.FirstOrDefault();
                    conceptoCobro.ValorConcepto = atencion.ValorConcepto;
                    conceptoCobro.ValorCruzado = atencion.ValorConcepto;
                    conceptoCobro.ValorSaldo = atencion.ValorConcepto;
                }

                conceptoCobro.Actualizar = true;
            }
        }

        /// <summary>
        /// Aplica las condiciones de cubrimiento que tengan lugar.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="identificadorClaseCubrimiento">The id clase cubrimiento.</param>
        /// <returns>
        /// Condici n de cubrimiento.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CondicionCubrimiento AplicarReglaCondicionCubrimiento(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle, int identificadorClaseCubrimiento)
        {
            CondicionCubrimiento objCubrimiento = new CondicionCubrimiento();
            List<CondicionCubrimiento> listaCondicionesCubrimiento = new List<CondicionCubrimiento>();

            var condicionesCubrimiento = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.CondicionesCubrimiento);

            if (condicionesCubrimiento != null && condicionesCubrimiento.Objeto != null)
            {
                var condiCubrimiento = this.FiltrarCondicionesCubrimiento(condicionesCubrimiento.Objeto as List<CondicionCubrimiento>, detalle.FechaVenta, identificadorClaseCubrimiento);
                condiCubrimiento = condiCubrimiento == null ? condiCubrimiento = new List<CondicionCubrimiento>() : condiCubrimiento;

                detalle.Cubrimiento.CondicionesCubrimiento = new CondicionCubrimiento();

                foreach (var condicion in condiCubrimiento)
                {
                    if (!this.ValidarCondicionCubrimiento(condicion, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerCondicionesCubrimiento(condicionesCubrimiento.NodosValidacion, atencion, detalle, condicion, listaCondicionesCubrimiento);
                }

                if (listaCondicionesCubrimiento.Count > 0)
                {
                    var condiCubrimientosFil = from
                                                    item in listaCondicionesCubrimiento
                                               orderby
                                                   item.IdAtencion descending, item.IdPlan descending,
                                                   item.IdContrato descending, item.IdServicio descending,
                                                   item.IdTipoAtencion descending, item.IdTipoRelacion descending,
                                                   item.IdProducto descending, item.IdGrupoProducto descending,
                                                   item.IdTipoProducto descending, item.OrdenComponentes descending,
                                                   item.IdElemento
                                               select
                                                   item;

                    objCubrimiento = condiCubrimientosFil.FirstOrDefault();
                }
            }

            return objCubrimiento;
        }

        /// <summary>
        /// Metodo para Aplicar condiciones de facturacion.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Lista de condiciones.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 11/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CondicionTarifa> AplicarReglaCondicionFacturacion(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle)
        {
            detalle.CondicionesTarifa = new List<CondicionTarifa>();
            var condicionesTarifa = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.CondicionesTarifa);

            if (condicionesTarifa != null && condicionesTarifa.Objeto != null)
            {
                var condiTar = this.FiltrarCondicionesTarifa(condicionesTarifa.Objeto as List<CondicionTarifa>, detalle.FechaVenta);
                condiTar = condiTar == null ? condiTar = new List<CondicionTarifa>() : condiTar;

                detalle.CondicionesTarifa = new List<CondicionTarifa>();

                foreach (var condicionTarifa in condiTar)
                {
                    if (!this.ValidarCondicionTarifa(condicionTarifa, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerCondicionesTarifaAtencion(condicionesTarifa.NodosValidacion, atencion, detalle, condicionTarifa);
                }

                if (detalle.CondicionesTarifa.Count() > 0)
                {
                    var condiTarFil = (from
                                           item in detalle.CondicionesTarifa
                                       where
                                           item.CodTip == "F"
                                       orderby
                                           item.IdAtencion descending, item.IdPlan descending,
                                           item.IdContrato descending, item.IdServicio descending,
                                           item.IdTipoAtencion descending, item.IdTipoRelacion descending,
                                           item.IdProducto descending, item.IdGrupoProducto descending,
                                           item.IdTipoProducto descending, item.OrdenComponentes descending,
                                           item.IdElemento
                                       select
                                           item).FirstOrDefault();

                    detalle.CondicionesTarifa.Clear();
                    detalle.CondicionesTarifa.Add(condiTarFil);
                }
            }

            return detalle.CondicionesTarifa;
        }

        /// <summary>
        /// Aplica las condiciones de tarifa que tengan lugar.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>
        /// Condicion de tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CondicionTarifa> AplicarReglaCondicionTarifa(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle, TipoFacturacion tipoFacturacion)
        {
            detalle.CondicionesTarifa = new List<CondicionTarifa>();

            var condicionesTarifa = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.CondicionesTarifa);

            if (condicionesTarifa != null && condicionesTarifa.Objeto != null)
            {
                var condiTar = this.FiltrarCondicionesTarifa(condicionesTarifa.Objeto as List<CondicionTarifa>, detalle.FechaVenta);

                if (condiTar == null)
                {
                    condiTar = new List<CondicionTarifa>();
                }

                foreach (var condicionTarifa in condiTar)
                {
                    if (!this.ValidarCondicionTarifa(condicionTarifa, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerCondicionesTarifaAtencion(condicionesTarifa.NodosValidacion, atencion, detalle, condicionTarifa);
                }

                if (detalle.CondicionesTarifa.Count() > 0 && (tipoFacturacion == TipoFacturacion.FacturacionRelacion || tipoFacturacion == TipoFacturacion.FacturacionActividad))
                {
                    var condiTarFil = (from
                                           item in detalle.CondicionesTarifa
                                       where
                                           item.CodTip == "T"
                                       orderby
                                          item.IdAtencion descending, item.IdPlan descending,
                                           item.IdContrato descending, item.IdServicio descending,
                                           item.IdTipoAtencion descending, item.IdTipoRelacion descending,
                                           item.IdProducto descending, item.IdGrupoProducto descending,
                                           item.IdTipoProducto descending, item.OrdenComponentes descending,
                                           item.IdElemento
                                       select
                                           item).FirstOrDefault();

                    detalle.CondicionesTarifa.Clear();

                    if (condiTarFil != null)
                    {
                        detalle.CondicionesTarifa.Add(condiTarFil);
                    }
                }
            }

            return detalle.CondicionesTarifa;
        }

        /// <summary>
        /// Metodo para aplicar las reglas de validaci n de costos asociados.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicionContrato.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarReglaCostosAsociados(List<CondicionProceso> condiciones, FacturaAtencion atencion, VentaComponente detalle, CondicionContrato condicionContrato)
        {
            detalle.CostosAsociados = new List<CostoAsociado>();
            var costosAsociados = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.CostoAsociado);

            if (costosAsociados != null && costosAsociados.Objeto != null)
            {
                var listaCostoAsociado = this.FiltrarCostosAsociados(costosAsociados.Objeto as List<CostoAsociado>, detalle.FechaVenta, detalle.Componente, condicionContrato);
                listaCostoAsociado = listaCostoAsociado == null ? listaCostoAsociado = new List<CostoAsociado>() : listaCostoAsociado;

                detalle.CostosAsociados = new List<CostoAsociado>();
                foreach (var condicion in listaCostoAsociado)
                {
                    if (!this.ValidarCostoAsociado(condicion, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerCostosAsociados(costosAsociados.NodosValidacion, atencion, detalle, condicion);
                }

                if (detalle.CostosAsociados.Count() > 0)
                {
                    CostoAsociado indicadorCostoAsociado = null;

                    if (indicadorCostoAsociado == null)
                    {
                        indicadorCostoAsociado = this.ObtenerCostoAsociado(detalle.CostosAsociados, condicionContrato.IdManual);
                    }

                    if (indicadorCostoAsociado == null)
                    {
                        indicadorCostoAsociado = this.ObtenerCostoAsociado(detalle.CostosAsociados, condicionContrato.IdManualAlterno);
                    }

                    if (indicadorCostoAsociado == null)
                    {
                        indicadorCostoAsociado = this.ObtenerCostoAsociado(detalle.CostosAsociados, condicionContrato.IdManualInstitucional);
                    }

                    if (indicadorCostoAsociado != null)
                    {
                        detalle.CostosAsociados.Clear();
                        detalle.CostosAsociados.Add(indicadorCostoAsociado);
                    }
                }
            }
        }

        /// <summary>
        /// Aplica los descuentos x contrato.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarReglaDescuentos(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle)
        {
            var descuentosContrato = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.Descuentos);
            if (descuentosContrato != null)
            {
                var descuentos = this.FiltrarDescuentos(descuentosContrato.Objeto as List<Descuento>, detalle.FechaVenta);
                descuentos = descuentos == null ? descuentos = new List<Descuento>() : descuentos;
                detalle.Descuentos = new List<Descuento>();

                foreach (var descuento in descuentos)
                {
                    if (!this.ValidarDescuento(descuento, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerDescuentosContrato(descuentosContrato.NodosValidacion, atencion, detalle, descuento);
                }

                if (detalle.Descuentos != null && detalle.Descuentos.Count() > 0)
                {
                    var desc = (from
                                    item in detalle.Descuentos
                                orderby
                                    item.IdAtencion descending, item.IdPlan descending,
                                    item.IdContrato descending, item.IdServicio descending,
                                    item.IdTipoAtencion descending, item.IdTipoProducto descending,
                                    item.IdGrupoProducto descending, item.IdProducto descending,
                                    item.OrdenComponentes descending,
                                    item.IdElemento
                                select
                                    item).FirstOrDefault();

                    detalle.Descuentos.Clear();
                    detalle.Descuentos.Add(desc);
                }
                else
                {
                    if (detalle is FacturaAtencionDetalle)
                    {
                        detalle.Descuentos.Clear();
                    }
                    else if (detalle is VentaComponente)
                    {
                        detalle.Descuentos.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Aplica las exclisiones x contrato.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica si se aplica la exclusion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaExclusionContrato(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool excluyente = false;

            var exclusiones = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.Exclusiones);

            if (exclusiones != null && exclusiones.Objeto != null)
            {
                foreach (var exclusion in exclusiones.Objeto as List<Exclusion>)
                {
                    if (!this.ValidarExclusionContrato(exclusion, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerExclusionesAtencion(exclusiones.NodosValidacion, atencion, detalle, exclusion);
                    if (detalle.Exclusion != null)
                    {
                        if (detalle.Exclusion.NumeroVenta > 0 && detalle.Exclusion.NumeroVenta == detalle.NumeroVenta)
                        {
                            excluyente = true;
                        }
                        else if (detalle.Exclusion.NumeroVenta > 0 && detalle.Exclusion.NumeroVenta != detalle.NumeroVenta)
                        {
                            excluyente = false;
                        }
                        else
                        {
                            excluyente = true;
                        }

                        break;
                    }
                }
            }

            return excluyente;
        }

        /// <summary>
        /// Aplica las Exclusiones x Manual.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>
        /// Indica Si Se Aplica la Exclusion.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaExclusionManual(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle, TipoFacturacion tipoFacturacion)
        {
            bool excluyente = false;

            var exclusionesManual = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.ExclusionesManual);

            if (exclusionesManual != null && exclusionesManual.Objeto != null)
            {
                foreach (var exclusion in exclusionesManual.Objeto as List<ExclusionManual>)
                {
                    if (!this.ValidarExclusionManual(exclusion, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerExclusionesManual(exclusionesManual.NodosValidacion, atencion, detalle, exclusion);

                    if (detalle.ExclusionManual != null)
                    {
                        switch (tipoFacturacion)
                        {
                            case TipoFacturacion.FacturacionRelacion:
                                excluyente = true;
                                break;

                            default:

                                excluyente = this.EvaluarNivelComplejidad(detalle);
                                break;
                        }

                        if (excluyente)
                        {
                            break;
                        }
                    }
                }
            }

            return excluyente;
        }

        /// <summary>
        /// Aplica la exclusi n de manual por ventas asociadas.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>Indica Si Se Aplica la Exclusion.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 07/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaExclusionManualVentaAsociada(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle, TipoFacturacion tipoFacturacion)
        {
            bool excluyente = false;

            var exclusionesManual = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.ExclusionesManual);

            if (exclusionesManual != null && exclusionesManual.Objeto != null)
            {
                foreach (var exclusion in exclusionesManual.Objeto as List<ExclusionManual>)
                {
                    switch (tipoFacturacion)
                    {
                        case TipoFacturacion.FacturacionActividad:
                            if (!this.ValidarExclusionManual(exclusion, atencion, detalle))
                            {
                                continue;
                            }

                            this.ObtenerExclusionesManual(exclusionesManual.NodosValidacion, atencion, detalle, exclusion);

                            break;
                    }

                    if (detalle.ExclusionManual != null)
                    {
                        switch (tipoFacturacion)
                        {
                            case TipoFacturacion.FacturacionRelacion:
                                excluyente = true;
                                break;

                            default:

                                if (detalle.ExclusionManual.IdNivelInicial > 0 && detalle.ExclusionManual.IdNivelFinal > 0)
                                {
                                    excluyente = this.EvaluarNivelComplejidad(detalle);

                                    if (!excluyente)
                                    {
                                        detalle.ExclusionManual = null;
                                    }
                                }

                                break;
                        }

                        if (excluyente)
                        {
                            break;
                        }
                    }
                }
            }

            return excluyente;
        }

        /// <summary>
        /// Metodo para aplicar las reglas de negocio de factores QX.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The venta componente.</param>
        /// <returns>
        /// Validacion de factores QX.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaFactoresQX(FacturaAtencion atencion, VentaComponente detalle)
        {
            bool respuesta = false;
            short bilateralidad = 0;
            short via = 0;
            short especialidad = 0;
            int ordenFactor = this.OrdenFactorQX;

            detalle.FactoresQx = new List<FactoresQX>();

            ////Validar Fecha que debe recibir sql server
            DateTime dte = detalle.FechaVenta;
            DateTime minVal = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            if (dte < minVal)
            {
                detalle.FechaVenta = minVal;
            }

            ////Fin validacion fecha
            var factores = new FactoresQX()
            {
                Componente = detalle.ComponenteBase,
                IdManual = atencion.IdManual,
                FechaVigencia = detalle.FechaVenta,
                CodigoEntidad = this.CodigoEntidad
            };

            var factoresQx = this.FiltrarFactoresQx(factores);
            if (factoresQx != null && factoresQx.Count > 0)
            {
                if (factoresQx[0].MaxOrd < this.OrdenFactorQX)
                {
                    ordenFactor = factoresQx[0].MaxOrd;
                }

                if (detalle.TipoProductos == TipoProd.Quirurjicos)
                {
                    if (factoresQx[0].OrdenGeneral == 1)
                    {
                        if (detalle.Bilateralidad != 0)
                        {
                            bilateralidad = 1;
                        }

                        var factor = (from
                                          item in factoresQx
                                      where
                                          item.OrdenProcedimiento == ordenFactor
                                          && item.Bilateralidad == bilateralidad
                                          && item.Componente == detalle.ComponenteBase
                                      select
                                          item).FirstOrDefault();

                        if (factor != null)
                        {
                            this.ValBilateralidad = detalle.Bilateralidad;
                            this.ValEspecialidad = detalle.IdEspecialidad;
                            this.ValVia = detalle.IdVia;
                            detalle.FactoresQx.Add(factor);
                        }
                    }
                    else
                    {
                        if (this.OrdenFactorQX == 1)
                        {
                            if (detalle.Bilateralidad > 0)
                            {
                                bilateralidad = 1;
                            }

                            if (detalle.IdEspecialidad > 0)
                            {
                                especialidad = 1;
                                this.ValEspecialidad = detalle.IdEspecialidad;
                            }

                            if (detalle.IdVia > 0)
                            {
                                via = 1;
                                this.ValVia = detalle.IdVia;
                            }
                        }
                        else
                        {
                            if (detalle.Bilateralidad == 1)
                            {
                                if (this.ValEspecialidad == detalle.IdEspecialidad)
                                {
                                    especialidad = 1;
                                }

                                if (this.ValVia == detalle.IdVia)
                                {
                                    via = 1;
                                }

                                bilateralidad = 1;
                            }
                            else
                            {
                                if (this.ValEspecialidad == detalle.IdEspecialidad)
                                {
                                    especialidad = 1;
                                }

                                if (this.ValVia == detalle.IdVia)
                                {
                                    via = 1;
                                }

                                bilateralidad = 0;
                            }
                        }

                        var factor2 = (from
                                           item in factoresQx
                                       where
                                           item.OrdenProcedimiento == ordenFactor
                                           && item.Bilateralidad == bilateralidad
                                           && item.IdVia == via
                                           && item.IdEspecialidad == especialidad
                                           && item.Componente == detalle.ComponenteBase
                                       select
                                           item).FirstOrDefault();

                        if (factor2 != null)
                        {
                            detalle.FactoresQx.Add(factor2);
                        }
                    }
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Aplica reglas de niveles de complejidad.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="componente">The componente.</param>
        /// <param name="manual">The manual.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Confirma la aplicacion de la regla de niveles de complejidad.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaNivelesComplejidad(List<CondicionProceso> condiciones, FacturaAtencion atencion, VentaComponente componente, int manual, FacturaAtencionDetalle detalle)
        {
            componente.NivelesComplejidad = null;
            var nivelesComplejidad = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.NivelesComplejidad);

            componente.IdTipoAtencion = detalle.IdTipoAtencion;

            var niveles = new NivelComplejidad()
            {
                IndHabilitado = 1,
                VigenciaManual = componente.CondicionesTarifa.FirstOrDefault() != null && componente.CondicionesTarifa.FirstOrDefault().VigenciaTarifa != new DateTime() ? componente.CondicionesTarifa.FirstOrDefault().VigenciaTarifa : componente.FechaVenta,
                Producto = componente.IdProductoRelacion,
                OrdenNivel = componente.NivelOrden,
                IdManual = manual
            };

            nivelesComplejidad.Objeto = this.ConsultarNivelesComplejidad(niveles);

            if (nivelesComplejidad != null && nivelesComplejidad.Objeto != null)
            {
                foreach (var complejidad in nivelesComplejidad.Objeto as List<NivelComplejidad>)
                {
                    if (!this.ValidarNivelesComplejidad(complejidad, atencion, componente))
                    {
                        continue;
                    }

                    this.ObtenerNivelesComplejidad(nivelesComplejidad.NodosValidacion, atencion, componente, complejidad);
                }

                if (nivelesComplejidad.Objeto != null)
                {
                    List<NivelComplejidad> listaNiveles = nivelesComplejidad.Objeto as List<NivelComplejidad>;

                    if ((listaNiveles != null && listaNiveles.Count > 1) && (listaNiveles.FirstOrDefault().TipoValorComponente == "M" || listaNiveles.FirstOrDefault().TipoValorComponente == "F") && componente is VentaComponente)
                    {
                        componente.NivelesComplejidad = this.BuscarNivelComplejidadxValor(nivelesComplejidad.Objeto as List<NivelComplejidad>, detalle.DetalleTarifa, componente);
                    }
                    else if ((listaNiveles != null && listaNiveles.Count > 1) && (listaNiveles.FirstOrDefault().TipoValorComponente == "T") && componente is VentaComponente)
                    {
                        componente.NivelesComplejidad = this.BuscarNivelComplejidadxTiempo(nivelesComplejidad.Objeto as List<NivelComplejidad>, componente as VentaComponente);
                    }
                    else if ((listaNiveles != null && listaNiveles.Count > 0) && componente is VentaComponente)
                    {
                        componente.NivelesComplejidad = (nivelesComplejidad.Objeto as List<NivelComplejidad>).FirstOrDefault();
                    }
                }
            }

            return componente.NivelesComplejidad != null ? true : false;
        }

        /// <summary>
        /// Aplica los recargos de contrato.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Verfificacion de regla recargo.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool AplicarReglaRecargosContrato(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle)
        {
            detalle.RecargoContrato = new List<Recargo>();
            var recargosContrato = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.Recargos);

            if (recargosContrato != null)
            {
                var recargos = this.FiltrarRecargoContrato(recargosContrato.Objeto as List<Recargo>, detalle);
                recargos = recargos == null ? recargos = new List<Recargo>() : recargos;

                detalle.RecargoContrato = new List<Recargo>();

                foreach (var recargo in recargos)
                {
                    if (!this.ValidarRecargoContrato(recargo, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerRecargosContrato(recargosContrato.NodosValidacion, atencion, detalle, recargo);
                }

                if (detalle.RecargoContrato.Count() > 0)
                {
                    var recargo = (from
                                       item in detalle.RecargoContrato
                                   orderby
                                       Convert.ToInt16(item.Festivo) descending,
                                       item.IdAtencion descending, item.IdPlan descending,
                                       item.IdContrato descending, item.IdServicio descending,
                                       item.IdTipoAtencion descending, item.IdTipoRelacion descending,
                                       item.IdProducto descending, item.IdGrupoProducto descending,
                                       item.IdTipoProducto descending, item.OrdenComponentes descending,
                                       item.IdElemento
                                   select
                                       item).FirstOrDefault();

                    detalle.RecargoContrato.Clear();
                    detalle.RecargoContrato.Add(recargo);
                    Debug.WriteLine(recargo.Id);
                }
            }

            return detalle.RecargoContrato.Count > 0 ? true : false;
        }

        /// <summary>
        /// Aplica los recargos de manual.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AplicarReglaRecargosManual(List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion detalle)
        {
            detalle.RecargoManual = new List<RecargoManual>();
            var recargosManual = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.RecargosManual);

            if (recargosManual != null)
            {
                var recargos = this.FiltrarRecargoManual(recargosManual.Objeto as List<RecargoManual>, detalle);
                recargos = recargos == null ? recargos = new List<RecargoManual>() : recargos;

                detalle.RecargoManual = new List<RecargoManual>();

                foreach (var recargoManual in recargos)
                {
                    if (!this.ValidarRecargoManual(recargoManual, atencion, detalle))
                    {
                        continue;
                    }

                    this.ObtenerRecargosManual(recargosManual.NodosValidacion, atencion, detalle, recargoManual);
                }

                if (detalle.RecargoManual.Count() > 0)
                {
                    var recar = (from
                                     item in detalle.RecargoManual
                                 orderby
                                     Convert.ToInt16(item.Festivo) descending,
                                    item.IdAtencion descending, item.IdPlan descending,
                                       item.IdContrato descending, item.IdServicio descending,
                                       item.IdTipoAtencion descending, item.IdTipoRelacion descending,
                                       item.IdProducto descending, item.IdGrupoProducto descending,
                                       item.IdTipoProducto descending, item.OrdenComponentes descending,
                                       item.IdElemento
                                 select
                                     item).FirstOrDefault();

                    detalle.RecargoManual.Clear();
                    detalle.RecargoManual.Add(recar);
                    Debug.WriteLine(recar.Id);
                }
            }
        }

        /// <summary>
        /// Escribe el archivo.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="proceso">The nom proceso.</param>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: Dario Preciado - INTERGRUPO\dpreciado.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void Archivo(int identificadorProceso, string proceso, string detalle)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine("D:\\Logs", string.Format("Proceso_{0}_{1}.txt", identificadorProceso, proceso)), true))
            {
                writer.WriteLine(detalle);
                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        /// Asocia los productos que se encuentren cubiertos dentro del paquete.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="atencion">Parametro atencion.</param>
        /// <param name="list">Parametro list.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void AsociarProductoPaquete(int identificadorProceso, FacturaAtencion atencion, List<Paquete> list, CondicionContrato condicionContrato)
        {
            var exclusiones = from exclusion in atencion.Detalle
                              where exclusion.Exclusion != null
                              && exclusion.ExclusionManual != null
                              select exclusion;

            if (exclusiones != null && exclusiones.Count() == 0)
            {
                foreach (var paquete in list)
                {
                    if (this.EvaluarCantidadProducto(paquete.Productos.Count()))
                    {
                        if (paquete.Productos != null)
                        {
                            this.HomologarProductosPaquetes(identificadorProceso, paquete, atencion.Detalle, condicionContrato);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para borrar ventas no marcadas.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void BorraVentasNoMarcadas(ProcesoFactura procesoFactura, FacturaAtencion atencion)
        {
            atencion.VentasNoMarcadas = procesoFactura.VentasNoMarcadas;
            List<FacturaAtencionDetalle> listFa = new List<FacturaAtencionDetalle>();

            foreach (var venta in atencion.VentasNoMarcadas)
            {
                foreach (FacturaAtencionDetalle detalle in atencion.Detalle)
                {
                    if (venta == detalle.NumeroVenta)
                    {
                        listFa.Add(detalle);
                    }
                }
            }

            atencion.Detalle = listFa;
        }

        /// <summary>
        /// Obtener aproximaciones.
        /// </summary>
        /// <param name="aproximaciones">The aproximaciones.</param>
        /// <param name="nombreAproximacion">The nombre aproximacion.</param>
        /// <returns>Lista de aproximaciones.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Aproximacion> BuscarAproximacionxNombre(List<Aproximacion> aproximaciones, string nombreAproximacion)
        {
            var aprox = from
                            item in aproximaciones
                        where
                            item.NombreAproximacion == nombreAproximacion
                        select
                            item;

            return aprox.ToList();
        }

        /// <summary>
        /// Busca el cliente asociado a la atención.
        /// </summary>
        /// <param name="clientes">The clientes.</param>
        /// <param name="identificadorCliente">The id cliente.</param>
        /// <returns>Cliente de la atencion buscado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Cliente BuscarClientexIdCliente(List<Cliente> clientes, int identificadorCliente)
        {
            var cliente = from
                              item in clientes
                          where
                              item.IdCliente == identificadorCliente
                          select
                              item;

            return cliente.FirstOrDefault();
        }

        /// <summary>
        /// Carga los componentes asociados a los productos dentro del detalle de la atención.
        /// </summary>
        /// <param name="componentesProductos">The componentes productos.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Lista de datos.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<VentaComponente> BuscarComponentesPorProducto(List<VentaComponente> componentesProductos, BaseValidacion detalle)
        {
            FacturaAtencionDetalle validacion = new FacturaAtencionDetalle();

            var ventasComponentes = from
                                        componente in componentesProductos
                                    where
                                        componente.IdProducto == detalle.IdProducto
                                        && componente.NumeroVenta == detalle.NumeroVenta
                                        && componente.IdTransaccion == detalle.IdTransaccion
                                    select
                                    new VentaComponente()
                                    {
                                        Bilateralidad = detalle.Bilateralidad,
                                        CantidadComponente = componente.CantidadComponente,
                                        CantidadComponenteFacturada = componente.CantidadComponenteFacturada,
                                        Componente = componente.Componente,
                                        ComponenteBase = componente.Componente,
                                        IdAtencion = componente.IdAtencion,
                                        IdComponente = componente.IdComponente,
                                        IdEspecialidad = detalle.IdEspecialidad,
                                        IdGrupoProducto = componente.IdGrupoProducto,
                                        IdPlan = detalle.IdPlan,
                                        IdProducto = componente.IdProducto,
                                        IdProductoRelacion = componente.IdComponente,
                                        IdServicio = detalle.IdServicio,
                                        IdTipoProducto = componente.IdTipoProducto,
                                        IdTransaccion = componente.IdTransaccion,
                                        IdVia = detalle.IdVia,
                                        NivelesComplejidad = detalle.NivelesComplejidad,
                                        NivelOrden = detalle.NivelOrden,
                                        NumeroVenta = componente.NumeroVenta,
                                        Tiempo = componente.Tiempo,
                                        TipoProductos = detalle.TipoProductos,
                                        NombreComponente = componente.NombreComponente,
                                        NombreProducto = componente.NombreProducto,
                                        TipoValorComponente = componente.TipoValorComponente,
                                        NombreResponsableHonorario = componente.NombreResponsableHonorario,
                                        Responsable = componente.Responsable
                                    };

            return ventasComponentes.Count() == 0 ? new List<VentaComponente>() : ventasComponentes.ToList();
        }

        /// <summary>
        /// Metodo para buscar los componentes de la venta.
        /// </summary>
        /// <param name="componentes">The componentes.</param>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Lista de componentes.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<VentaComponente> BuscarComponenteVenta(List<VentaComponente> componentes, BaseValidacion detalleVenta)
        {
            var resultado = from
                                item in componentes
                            where
                                item.IdProducto == detalleVenta.IdProducto
                                && item.IdTransaccion == detalleVenta.IdTransaccion
                                && item.NumeroVenta == detalleVenta.NumeroVenta
                            select
                                item;

            return resultado.Count() > 0 ? resultado.ToList() : new List<VentaComponente>();
        }

        /// <summary>
        /// Busca los conceptos de Cobro Asociados a la atencion.
        /// </summary>
        /// <param name="conceptosCobro">The conceptos cobro.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Conceptos de cobro.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaAtencionConceptoCobro> BuscarConceptoCobroxAtencion(List<FacturaAtencionConceptoCobro> conceptosCobro, int identificadorAtencion)
        {
            var conceptos = from
                                item in conceptosCobro
                            where
                                item.IdAtencion == identificadorAtencion
                            select
                                item;

            return conceptos.ToList();
        }

        /// <summary>
        /// Metodo para realizar la Búsqueda del detalle.
        /// </summary>
        /// <param name="detalles">The detalles.</param>
        /// <param name="buscar">The buscar.</param>
        /// <returns>Detalle de la Factura.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private FacturaAtencionDetalle BuscarDetalle(List<FacturaAtencionDetalle> detalles, FacturaAtencionDetalle buscar)
        {
            var resultado = from
                                item in detalles
                            where
                                item.NumeroVenta == buscar.NumeroVenta
                                && item.IdTransaccion == buscar.IdTransaccion
                                && item.IdProducto == buscar.IdProducto
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Busca los movimientos asociados al numero de atencion.
        /// </summary>
        /// <param name="movimientosTesoreria">The movimientos tesoreria.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Movimientos de tesoreria.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaAtencionMovimiento> BuscarMovimientosTesoreriaxAtencion(List<FacturaAtencionMovimiento> movimientosTesoreria, int identificadorAtencion)
        {
            var movimientos = from
                                  item in movimientosTesoreria
                              where
                                  item.IdAtencion == identificadorAtencion
                              select
                                  item;

            return movimientos.ToList();
        }

        /// <summary>
        /// Metodo para buscar nivel de complejidad x tiempo.
        /// </summary>
        /// <param name="niveles">The niveles.</param>
        /// <param name="componente">The componente.</param>
        /// <returns>Nivel de complejidad.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private NivelComplejidad BuscarNivelComplejidadxTiempo(List<NivelComplejidad> niveles, VentaComponente componente)
        {
            var tipoAtencion = componente.IdTipoAtencion;

            var nivel = from
                            item in niveles
                        where
                            (componente.Tiempo >= item.ValorMinimo && componente.Tiempo <= item.ValorMaximo)
                            && (componente.IdTipoAtencion == item.IdTipoAtencion || item.IdTipoAtencion == 0)
                        select
                            item;

            return nivel.OrderByDescending(c => c.IdTipoAtencion).FirstOrDefault();
        }

        /// <summary>
        /// Método para Buscar Nivel de Complejidad por Tipo de Atención.
        /// </summary>
        /// <param name="niveles">The niveles.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Nivel de complejidad.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\IVAN J.
        /// FechaDeCreacion: 06/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private NivelComplejidad BuscarNivelComplejidadxTipoAtencion(List<NivelComplejidad> niveles, FacturaAtencionDetalle detalle)
        {
            var nivel = from
                            item in niveles
                        where
                            item.IdTipoAtencion == detalle.IdTipoAtencion
                        select
                            item;

            return nivel.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para Buscar Nivel de Complejidad por Valor.
        /// </summary>
        /// <param name="niveles">The niveles.</param>
        /// <param name="detalleTarifa">The detalle tarifa.</param>
        /// <param name="componente">The componente.</param>
        /// <returns>
        /// Nivel de Complejidad.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private NivelComplejidad BuscarNivelComplejidadxValor(List<NivelComplejidad> niveles, DetalleTarifa detalleTarifa, VentaComponente componente)
        {
            if (detalleTarifa != null)
            {
                var nivel = from
                                item in niveles
                            where
                                (detalleTarifa.ValorTarifa >= item.ValorMinimo && detalleTarifa.ValorTarifa <= item.ValorMaximo)
                                && (componente.IdTipoAtencion == item.IdTipoAtencion || item.IdTipoAtencion == 0)
                            orderby item.IdTipoAtencion descending
                            select
                                item;

                return nivel.FirstOrDefault();
            }
            else
            {
                var nivel = from
                                item in niveles
                            where componente.IdTipoAtencion == item.IdTipoAtencion || item.IdTipoAtencion == 0
                            orderby item.IdTipoAtencion descending
                            select
                                item;

                return nivel.FirstOrDefault();
            }
        }

        /// <summary>
        /// Buscar Producto.
        /// </summary>
        /// <param name="detalles">The detalles.</param>
        /// <param name="filtro">The filtro.</param>
        /// <returns>Factura Atencion Detalle.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Buscar Producto.
        /// </remarks>
        private FacturaAtencionDetalle BuscarProducto(List<FacturaAtencionDetalle> detalles, FacturaAtencionDetalle filtro)
        {
            var resultado = from
                                item in detalles
                            where
                                item.IdProducto == filtro.IdProducto
                                && item.IdTransaccion == filtro.IdTransaccion
                                && item.NumeroVenta == filtro.NumeroVenta
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para realizar la Busqueda de Productos Asociados.
        /// </summary>
        /// <param name="productos">The productos.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Producto Asociado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private VentaProductoRelacion BuscarProductoAsociado(List<VentaProductoRelacion> productos, FacturaAtencionDetalle detalle)
        {
            var resultado = from
                                producto in productos
                            where
                                producto.IdProducto == detalle.IdProducto
                                && producto.NumeroVenta == detalle.NumeroVenta
                            select
                                producto;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para realizar el ordenamiento de los valores.
        /// </summary>
        /// <param name="productos">The productos.</param>
        /// <param name="identificadorProducto">The id producto.</param>
        /// <returns>
        /// Producto de la venta.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private FacturaAtencionDetalle BuscarProductoIdProducto(List<FacturaAtencionDetalle> productos, int identificadorProducto)
        {
            var resultado = from
                                item in productos
                            where
                                item.IdProducto == identificadorProducto
                            select
                                item;
            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para Buscar Unidad de Tarifa.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="codigoUnidad">The codigo unidad.</param>
        /// <param name="fechaVigencia">The fecha vigencia.</param>
        /// <returns>
        /// Unidad de Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private TarifaUnidad BuscarUnidad(CondicionContrato condicionContrato, string codigoUnidad, DateTime fechaVigencia)
        {
            ////Cargar tarifa unidad una ola vez ya que siempre trae todos los registros
            if (this.tarifaUnidad == null)
            {
                this.tarifaUnidad = this.ConsultarTarifaUnidad(new TarifaUnidad());
            }

            var resultado = from
                              item in this.tarifaUnidad
                            where
                                item.CodigoUnidad == codigoUnidad
                                && item.FechaVigencia.Date == fechaVigencia.Date
                                && item.IdManual == condicionContrato.IdManualResultado
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Buscar Venta Componente.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="filtro">The filtro.</param>
        /// <returns>Venta Componente.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Buscar Venta Componente.
        /// </remarks>
        private VentaComponente BuscarVentaComponente(FacturaAtencionDetalle detalle, VentaComponente filtro)
        {
            var resultado = from
                                item in detalle.VentaComponentes
                            where
                                item.IdComponente == filtro.IdComponente
                                && item.IdProducto == filtro.IdProducto
                                && item.IdTransaccion == filtro.IdTransaccion
                                && item.NumeroVenta == filtro.NumeroVenta
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo Para Calcular Los pagos Realizados.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Pagos Realizados en la factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 20/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal CalcularPagosRealizados(List<FacturaAtencion> facturaAtencion)
        {
            decimal pagosRealizados = 0;

            var atencionConcepto = (from
                                        atencion in facturaAtencion
                                    where
                                        atencion.Cruzar
                                    select atencion).ToList();

            foreach (var atencion in atencionConcepto)
            {
                if (atencion != null && atencion.ConceptosCobro.Count > 0 && atencion.ConceptosCobro.FirstOrDefault().IndHabilitado == 1)
                {
                    pagosRealizados += atencion.ValorConcepto;
                }
            }

            return pagosRealizados;
        }

        /// <summary>
        /// Metodo para aplicar los calculos a los componentes.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="componente">The componente.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CalcularValorComponente(FacturaAtencion atencion, FacturaAtencionDetalle detalle, VentaComponente componente, CondicionContrato condicionContrato, List<CondicionProceso> condiciones)
        {
            TipoTarifa tipoTarifa = TipoTarifa.NoDefinido;
            bool valorPropio = false;
            bool complejidad = false;
            if (componente.CondicionesTarifa != null && componente.CondicionesTarifa.Count > 0)
            {
                var ajusteTarifa = this.ValidarCondicionesAjusteTarifa(detalle.CondicionesTarifa, TipoTarifa.AjusteTarifas);
                var condicionTarifaComponente = this.ValidarCondicionesAjusteTarifa(componente.CondicionesTarifa, TipoTarifa.ValorPropio);

                if (condicionTarifaComponente != null)
                {
                    valorPropio = true;
                    componente.ValorUnitario = condicionTarifaComponente.ValorPropio;
                }
            }

            if (!valorPropio)
            {
                if (componente.NivelesComplejidad != null && componente.NivelesComplejidad.IdManual > 0)
                {
                    componente.ValorUnitario = this.CalcularValorUnitario(atencion, detalle, componente, condicionContrato, condiciones);

                    if (componente.FactoresQx != null && componente.FactoresQx.Count > 0 &&
                        componente.FactoresQx[0].Valor > 0)
                    {
                        var factorActual = componente.FactoresQx.FirstOrDefault();
                        componente.ValorUnitario = (componente.ValorUnitario * factorActual.Valor) / 100;
                    }
                }

                if (componente.CostosAsociados != null && componente.CostosAsociados.Count() > 0)
                {
                    var costoasociado = componente.CostosAsociados.FirstOrDefault();

                    if (detalle.CondicionesTarifa != null && detalle.CondicionesTarifa.Count > 0)
                    {
                        var ajusteTarifa = this.ValidarCondicionesAjusteTarifa(detalle.CondicionesTarifa, TipoTarifa.AjusteTarifas);

                        if (ajusteTarifa != null)
                        {
                            componente.ValorUnitario = (ajusteTarifa.ValorPropio * costoasociado.ValorComponente) / 100;
                        }
                    }
                    else
                    {
                        componente.ValorUnitario = costoasociado.ValorComponente;
                    }

                    if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Principal)
                    {
                        componente.ValorUnitario = (componente.ValorUnitario * condicionContrato.Porcentaje) / 100;
                    }
                    else if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Alterno)
                    {
                        try
                        {
                            decimal porcentajeAlterno = this.ConsultarPorcentajeAlterno(condicionContrato.IdContrato, condicionContrato.IdManualAlterno, condicionContrato.IdManual);

                            componente.ValorUnitario = (componente.ValorUnitario * porcentajeAlterno) / 100;
                        }
                        catch
                        {
                            throw new Exception(String.Format("No se pudo obtener el porcentaje alterno definido para el componente: {0}. Por favor revise.\r\n Id Atencion: {1}", componente.NombreComponente, atencion.IdAtencion));
                        }
                    }
                }

                complejidad = true;
            }

            if (!complejidad && !valorPropio)
            {
                if (detalle.CondicionesTarifa != null && detalle.CondicionesTarifa.Count > 0)
                {
                    var ajusteTarifa = this.ValidarCondicionesAjusteTarifa(detalle.CondicionesTarifa, TipoTarifa.AjusteTarifas);

                    if (ajusteTarifa != null)
                    {
                        componente.ValorUnitario = (ajusteTarifa.ValorPropio * componente.ValorUnitario) / 100;
                    }
                }

                componente.ValorUnitario = this.ObtenerValorTarifa(componente, condicionContrato, ref tipoTarifa);
                componente.ValorUnitario = this.CalcularValorUnitario(componente.DetalleTarifa, condicionContrato, componente);
            }

            if (componente.RecargoContrato != null && componente.RecargoContrato.Count() > 0)
            {
                componente.ValorRecargo = this.ObtenerValorRecargo(componente);
            }
            else if (detalle.RecargoContrato != null && detalle.RecargoContrato.Count() > 0)
            {
                componente.ValorRecargo = this.ObtenerValorRecargo(detalle);
            }
            else if (componente.RecargoManual != null && componente.RecargoManual.Count() > 0)
            {
                componente.ValorRecargo = this.ObtenerValorRecargo(componente);
            }
            else if (detalle.RecargoManual != null && detalle.RecargoManual.Count() > 0)
            {
                componente.ValorRecargo = this.ObtenerValorRecargo(detalle);
            }

            if (componente.Descuentos != null && componente.Descuentos.Count() > 0)
            {
                componente.ValorDescuento = this.ObtenerValorDescuento(componente);
            }
            else if (detalle.Descuentos != null && detalle.Descuentos.Count() > 0)
            {
                componente.Descuentos = detalle.Descuentos;
                componente.ValorDescuento = this.ObtenerValorDescuento(componente);
            }
            else
            {
                componente.ValorDescuento = 0;
                detalle.ValorDescuento = 0;
            }

            if (componente.CantidadFacturar == 0)
            {
                componente.CantidadProducto = componente.CantidadComponente;
            }

            componente.ValorProductos = this.Redondeo(componente.ValorUnitario * componente.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
            componente.ValorTotalRecargo = this.Redondeo(componente.ValorRecargo * componente.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
            componente.ValorTotalDescuento = this.Redondeo(componente.ValorDescuento * componente.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
            componente.ValorTotal = componente.ValorProductos + componente.ValorTotalRecargo - componente.ValorTotalDescuento;
        }

        /// <summary>
        /// Metodo para aplicar los calculos a los componentes.
        /// </summary>
        /// <param name="componente">The componente.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="identificadorContrato">The id contrato.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CalcularValorComponenteFactQX(VentaComponente componente, BaseValidacion detalle, int identificadorContrato)
        {
            if (componente.FactoresQx != null && componente.FactoresQx.Count > 0 && componente.FactoresQx[0].Valor > 0)
            {
                var factorActual = componente.FactoresQx.FirstOrDefault();
                componente.ValorUnitario = (componente.ValorUnitario * factorActual.Valor) / 100;
            }

            if (componente.Descuentos != null && componente.Descuentos.Count() > 0)
            {
                componente.ValorDescuento = this.ObtenerValorDescuento(componente);
            }
            else if (detalle.Descuentos != null && detalle.Descuentos.Count() > 0)
            {
                componente.Descuentos = detalle.Descuentos;
                componente.ValorDescuento = this.ObtenerValorDescuento(componente);
            }
            else
            {
                componente.ValorDescuento = 0;
                detalle.ValorDescuento = 0;
            }

            int identificadorManual = componente.CondicionesTarifa != null ? (componente.CondicionesTarifa.Count > 0 ? componente.CondicionesTarifa.FirstOrDefault().IdManual : 0) : 0;

            componente.ValorProductos = this.Redondeo(componente.ValorUnitario * componente.CantidadFacturar, identificadorContrato, identificadorManual);
            componente.ValorTotalRecargo = this.Redondeo(componente.ValorRecargo * componente.CantidadFacturar, identificadorContrato, identificadorManual);
            componente.ValorTotalDescuento = this.Redondeo(componente.ValorDescuento * componente.CantidadFacturar, identificadorContrato, identificadorManual);
            componente.ValorTotal = componente.ValorProductos + componente.ValorTotalRecargo - componente.ValorTotalDescuento;
        }

        /// <summary>
        /// Metodo que calcula los valores de tarifa de los paquetes.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="paquetes">The paquetes.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <returns>
        /// Lista de paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Paquete> CalcularValoresPaquetes(int identificadorProceso, List<Paquete> paquetes, CondicionContrato condicionContrato, FacturaAtencion atencion, List<CondicionProceso> condiciones, CondicionTarifa condicionFactura)
        {
            if (paquetes == null)
            {
                paquetes = new List<Paquete>();
            }

            List<Paquete> lstItemsPaquete = new List<Paquete>();

            foreach (Paquete paquete in paquetes)
            {
                Paquete aux = new Paquete();
                aux.Cantidad = paquete.Cantidad;
                aux.CodigoPaquete = paquete.CodigoPaquete;
                aux.IdPaquete = paquete.IdPaquete;
                aux.IndHabilitado = paquete.IndHabilitado;
                aux.NombrePaquete = paquete.NombrePaquete;
                aux.NombreTipoProducto = paquete.NombreTipoProducto != null ? paquete.NombreTipoProducto : string.Empty;
                aux.PerdidaGanancia = paquete.PerdidaGanancia;
                aux.Productos = this.CalcularValoresPaquetesProductosComponentes(paquete, condicionContrato, atencion.Detalle, condiciones, condicionFactura).ToList();
                aux.ValorPaquete = this.CalcularValorPaquete(condicionContrato, paquete);
                aux.ProductoPaquete = this.AdicionarProductoPaquete(paquete, atencion);
                lstItemsPaquete.Add(aux);
            }

            return lstItemsPaquete.Count() > 0 ? lstItemsPaquete.ToList() : new List<Paquete>();
        }

        /// <summary>
        /// Método para calcular el valor de los productos del paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="condicionContrato">The condicion Contrato.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <returns>
        /// Lista de productos del paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<PaqueteProducto> CalcularValoresPaquetesProductos(Paquete paquete, CondicionContrato condicionContrato, List<FacturaAtencionDetalle> detalle, List<CondicionProceso> condiciones, CondicionTarifa condicionFactura)
        {
            List<PaqueteProducto> listaProductosPaq = null;

            this.CalcularValoresPaquetesProductosComponentes(paquete, condicionContrato, detalle, condiciones, condicionFactura);

            if (listaProductosPaq == null)
            {
                listaProductosPaq = this.ObtenerValoresPaquetesProductos(paquete, condicionContrato, condicionContrato.IdManual, condicionContrato.FechaVigencia, detalle);
            }

            if (listaProductosPaq == null)
            {
                listaProductosPaq = this.ObtenerValoresPaquetesProductos(paquete, condicionContrato, condicionContrato.IdManualAlterno, condicionContrato.FechaVigenciaAlterna, detalle);
            }

            if (listaProductosPaq == null)
            {
                listaProductosPaq = this.ObtenerValoresPaquetesProductos(paquete, condicionContrato, condicionContrato.IdManualInstitucional, condicionContrato.FechaVigenciaAlterna, detalle);
            }

            if (listaProductosPaq != null)
            {
                var listaValores = listaProductosPaq.ToList();

                if (listaValores.Count() == 0)
                {
                    listaValores = paquete.Productos;
                }

                return listaValores;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para calcular el valor de los productos del paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="condicionContrato">The condicion Contrato.</param>
        /// <param name="detalleFactura">The detalle factura.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <returns>
        /// Lista de productos del paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<PaqueteProducto> CalcularValoresPaquetesProductosComponentes(Paquete paquete, CondicionContrato condicionContrato, List<FacturaAtencionDetalle> detalleFactura, List<CondicionProceso> condiciones, CondicionTarifa condicionFactura)
        {
            FacturaAtencion atencion = new FacturaAtencion();

            atencion.IdAtencion = detalleFactura.FirstOrDefault().IdAtencion;
            atencion.IdContrato = condicionContrato.IdContrato;

            List<VentaComponente> componentesProductos = this.ComponentesPorProductos(atencion);
            List<FacturaAtencionDetalle> detalleFacturaAux = new List<FacturaAtencionDetalle>();
            bool aplicaCondicionFactura = false;

            foreach (PaqueteProducto itemDetalle in paquete.Productos)
            {
                itemDetalle.VentaComponentes = new List<VentaComponente>();

                foreach (VentaComponente itemComponente in componentesProductos)
                {
                    if (itemDetalle.IdProducto == itemComponente.IdProducto && itemDetalle.NumeroVenta == itemComponente.NumeroVenta && itemDetalle.IdTransaccion == itemComponente.IdTransaccion)
                    {
                        itemDetalle.VentaComponentes.Add(itemComponente);
                    }
                }
            }

            foreach (PaqueteProducto itemDetalle in paquete.Productos)
            {
                foreach (FacturaAtencionDetalle item in detalleFactura)
                {
                    if (itemDetalle.IdPaquete == item.idPaquete && itemDetalle.IdProducto == item.IdProducto && itemDetalle.NumeroVenta == item.NumeroVenta && itemDetalle.IdTransaccion == item.IdTransaccion)
                    {
                        item.CantidadProducto = itemDetalle.CantidadAsignada;
                        item.VentaComponentes = itemDetalle.VentaComponentes;
                        detalleFacturaAux.Add(item);
                        break;
                    }
                }
            }

            List<VentaComponente> componenteVinculacion = new List<VentaComponente>();

            Paginacion<VentaProductoRelacion> paginacion = new Paginacion<VentaProductoRelacion>();
            CondicionContrato condicionContratoBase = null;

            List<FacturaAtencionDetalle> detalleVinculacion = new List<FacturaAtencionDetalle>();

            paginacion.LongitudPagina = 0;
            paginacion.PaginaActual = 0;

            var productosAsociados = this.ConsultarVentaProductosRelacion(paginacion);

            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                aplicaCondicionFactura = true;
            }

            foreach (var detalle in detalleFacturaAux)
            {
                if (componenteVinculacion == null)
                {
                    componenteVinculacion = new List<VentaComponente>();
                }

                var atencionProductoAsociado = this.BuscarProductoAsociado(productosAsociados.Item, detalle);

                // Obtener valor de la venta
                if (detalle.ValorUnitario == 0)
                {
                    detalle.ValorUnitario = detalle.ValorOriginal;
                }

                condicionContratoBase = condicionContrato;

                // Buscar componentes asociados a un producto
                detalle.VentaComponentes = this.BuscarComponentesPorProducto(componentesProductos, detalle);

                // Validar reglas productos componentes
                condicionContrato = this.ValidarReglasProductoComponente(TipoFacturacion.FacturacionPaquete, null, atencion, detalle, condiciones, Tipo.Producto, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);

                if (atencionProductoAsociado == null)
                {
                    if (!detalle.Excluido && detalle.CondicionSeparacion.NoCubrimiento == false)
                    {
                        foreach (var componente in detalle.VentaComponentes)
                        {
                            componente.FechaVenta = detalle.FechaVenta;
                            componente.HoraVenta = detalle.HoraVenta;

                            // Obtener valor de la venta
                            if (componente.ValorUnitario == 0)
                            {
                                componente.ValorUnitario = componente.ValorOriginal;
                            }

                            this.ValidarReglasProductoComponente(TipoFacturacion.FacturacionPaquete, detalle, atencion, componente, condiciones, Tipo.Componente, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);
                            componente.CodigoProducto = detalle.CodigoProducto;
                            componente.NombreProducto = detalle.NombreProducto;
                            componente.CodigoGrupo = detalle.CodigoGrupo;
                            componente.NombreGrupo = detalle.NombreGrupo;

                            this.CalcularValorComponente(atencion, detalle, componente, condicionContrato, condiciones);
                        }
                    }
                }

                this.CalcularValorProducto(detalle, condicionContrato);
                detalle.FechaVigenciaContrato = condicionContrato.FechaVigenciaResultado;
                condicionContrato = condicionContratoBase;
                var productoVinculacion = detalle.CopiarObjeto();
            }

            detalleFacturaAux.ForEach(c =>
            {
                PaqueteProducto itemDetalle = paquete.Productos.Where(d => d.IdProducto == c.IdProducto &&
                                             d.NumeroVenta == c.NumeroVenta &&
                                             d.IdTransaccion == c.IdTransaccion).FirstOrDefault();
                if (itemDetalle != null)
                {
                    itemDetalle.ValorPaqueteProducto = c.ValorTotal;
                    itemDetalle.ValorOriginal = c.ValorOriginal;
                    itemDetalle.ValorDescuento = c.ValorDescuento;
                    itemDetalle.ValorBruto = c.ValorBruto;
                    itemDetalle.ValorBrutoTotal = c.ValorBrutoTotal;
                    itemDetalle.ValorRecargo = c.ValorRecargo;
                    itemDetalle.ValorUnitario = c.ValorUnitario;
                    itemDetalle.ValorTotalRecargo = c.ValorTotalRecargo;
                    itemDetalle.ValorTotalDescuentos = c.ValorTotalDescuento;
                    itemDetalle.ValorTotal = c.ValorTotal;
                    itemDetalle.VentaComponentes = c.VentaComponentes;
                }
            });

            paquete.Productos.ForEach(c =>
            {
                ////Si existe un producto con componentes, realiza
                if ((c.VentaComponentes != null) && (c.VentaComponentes.Count > 0))
                {
                    c.ValorPaqueteProducto = c.VentaComponentes.Sum(x => x.ValorTotal);
                    c.ValorOriginal = c.VentaComponentes.Sum(x => x.ValorOriginal);
                    c.ValorDescuento = c.VentaComponentes.Sum(x => x.ValorDescuento);
                    c.ValorBruto = c.VentaComponentes.Sum(x => x.ValorBruto);
                    c.ValorBrutoTotal = c.VentaComponentes.Sum(x => x.ValorBrutoTotal);
                    c.ValorRecargo = c.VentaComponentes.Sum(x => x.ValorRecargo);
                    c.ValorUnitario = c.VentaComponentes.Sum(x => x.ValorUnitario);
                    c.ValorTotalRecargo = c.VentaComponentes.Sum(x => x.ValorTotalRecargo);
                    c.ValorTotalDescuentos = c.VentaComponentes.Sum(x => x.ValorTotalDescuento);
                    c.ValorTotal = c.VentaComponentes.Sum(x => x.ValorTotal);
                }
            });

            return paquete.Productos;
        }

        /// <summary>
        /// Calcular valores producto.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Calcular valores producto.
        /// </remarks>
        private void CalcularValoresProducto(BaseValidacion detalle)
        {
            detalle.ValorProductos = detalle.ValorUnitario * detalle.CantidadFacturar;
            detalle.ValorTotal = (detalle.ValorProductos + detalle.ValorTotalRecargo) - (detalle.CantidadFacturar * detalle.ValorDescuento);
            detalle.ValorTotalDescuento = detalle.CantidadFacturar * detalle.ValorDescuento;
        }

        /// <summary>
        /// Método para establecer el valor calculado del paquete armado.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="paquete">The paquete.</param>
        /// <returns>
        /// Paquete con el valor establecido.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private decimal CalcularValorPaquete(CondicionContrato condicionContrato, Paquete paquete)
        {
            decimal valorPaquete;

            var valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
                                          where (item.IdManual == condicionContrato.IdManual)
                                          select item).FirstOrDefault();

            if (valorEncabezadoPaquete == null)
            {
                valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
                                          where (item.IdManual == condicionContrato.IdManualAlterno)
                                          select item).FirstOrDefault();
            }

            if (valorEncabezadoPaquete == null)
            {
                valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
                                          where (item.IdManual == condicionContrato.IdManualInstitucional)
                                          select item).FirstOrDefault();
            }

            // valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
            //                          where (item.IdManual == condicionContrato.IdManual && (item.FechaVigenciaTarifa <= condicionContrato.FechaVigencia))
            //                          select item).FirstOrDefault();

            // if (valorEncabezadoPaquete == null)
            // {
            //    valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
            //                              where (item.IdManual == condicionContrato.IdManualAlterno && (item.FechaVigenciaTarifa <= condicionContrato.FechaVigenciaAlterna))
            //                              select item).FirstOrDefault();

            // if (valorEncabezadoPaquete != null)
            //    {
            //        condicionContrato.IdManual = condicionContrato.IdManualAlterno;
            //    }
            // }

            // if (valorEncabezadoPaquete == null)
            // {
            //    valorEncabezadoPaquete = (from item in condicionContrato.DetalleTarifa
            //                              where (item.IdManual == condicionContrato.IdManualResultado && (item.FechaVigenciaTarifa <= condicionContrato.FechaVigenciaResultado))
            //                              select item).FirstOrDefault();

            // if (valorEncabezadoPaquete != null)
            //    {
            //        condicionContrato.IdManual = condicionContrato.IdManualResultado;
            //    }
            // }
            var listaValoresPaquetes = this.ConsultarValorEncabezadoPaquetes(new Paquete()
            {
                IdPaquete = paquete.IdPaquete,
                IdManual = condicionContrato.IdManual,
                IndHabilitado = true
            }).ToList();

            if (listaValoresPaquetes.Count > 0)
            {
                DateTime fechaVigenciaReal = new DateTime();

                if (condicionContrato.RenovacionAutomaticaTarifa)
                {
                    List<DateTime> listFechas = new List<DateTime>();
                    listFechas.Add(condicionContrato.FechaVigencia);
                    listFechas.Add(condicionContrato.FechaVigenciaAlterna);
                    listFechas.Add(condicionContrato.FechaVigenciaResultado);

                    fechaVigenciaReal = listFechas.Max(c => c.Date);
                }

                var lista = from item in listaValoresPaquetes
                            where (item.VigenciaManual == fechaVigenciaReal)
                            select item;

                valorPaquete = lista.Sum(c => c.ValorPaquete);
            }
            else
            {
                valorPaquete = 0;
            }

            return valorPaquete;
        }

        /// <summary>
        /// Metodo para Calcular el Valor del Producto.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CalcularValorProducto(FacturaAtencionDetalle detalle, CondicionContrato condicionContrato)
        {
            decimal valorProducto = 0;
            decimal valorUnitario = 0;

            TipoTarifa tipoTarifa = new TipoTarifa();

            if (detalle != null && detalle.VentaComponentes != null && detalle.VentaComponentes.Count > 0)
            {
                this.CalcularValorProductoComponente(detalle);
            }
            else
            {
                valorProducto = this.ObtenerValorTarifa(detalle, condicionContrato, ref tipoTarifa);
                valorUnitario = this.CalcularValorUnitario(detalle.DetalleTarifa, condicionContrato, detalle);
                valorProducto = valorUnitario > 0 ? valorUnitario : valorProducto;

                if (tipoTarifa == TipoTarifa.ValorPropio || tipoTarifa == TipoTarifa.AjusteTarifas || tipoTarifa == TipoTarifa.NoDefinido)
                {
                    if (valorProducto == 0)
                    {
                        valorProducto = detalle.ValorOriginal;
                    }
                    else
                    {
                        if (detalle.TipoProductos != TipoProd.Quirurjicos)
                        {
                            detalle.ValorUnitario = valorProducto;
                        }

                        if (detalle.CondicionesTarifa == null)
                        {
                            detalle.CondicionesTarifa = new List<CondicionTarifa>();
                        }

                        if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Principal && detalle.CondicionesTarifa.Count > 0 && tipoTarifa != TipoTarifa.AjusteTarifas)
                        {
                            detalle.ValorUnitario = (valorProducto * condicionContrato.Porcentaje) / 100;
                        }
                        else if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Alterno && (detalle.CondicionesTarifa == null || detalle.CondicionesTarifa.Count == 0))
                        {
                            try
                            {
                                decimal porcentajeAlterno = this.ConsultarPorcentajeAlterno(condicionContrato.IdContrato, condicionContrato.IdManualAlterno, condicionContrato.IdManual);

                                if (porcentajeAlterno != 0)
                                {
                                    detalle.ValorUnitario = (valorProducto * porcentajeAlterno) / 100;
                                }
                            }
                            catch
                            {
                                throw new Exception(String.Format("No se pudo obtener el porcentaje alterno definido para el producto: {0}. Por favor revise.\r\n Id Atencion: {1}", detalle.NombreProducto, detalle.IdAtencion));
                            }
                        }

                        detalle.ValorUnitario = this.Redondeo(detalle.ValorUnitario, condicionContrato.IdContrato, condicionContrato.IdManual);
                    }
                }

                detalle.ValorRecargo = this.Redondeo(this.ObtenerValorRecargo(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);
                detalle.ValorDescuento = this.Redondeo(this.ObtenerValorDescuento(detalle), condicionContrato.IdContrato, condicionContrato.IdManual);

                detalle.ValorProductos = this.Redondeo(detalle.ValorUnitario * detalle.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
                detalle.ValorTotalRecargo = this.Redondeo(detalle.ValorRecargo * detalle.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
                detalle.ValorTotalDescuento = this.Redondeo(detalle.ValorDescuento * detalle.CantidadFacturar, condicionContrato.IdContrato, condicionContrato.IdManual);
                detalle.ValorTotal = this.Redondeo((detalle.ValorProductos + detalle.ValorTotalRecargo) - detalle.ValorTotalDescuento, condicionContrato.IdContrato, condicionContrato.IdManual);
            }
        }

        /// <summary>
        /// Metodo para Aplicar el Calculo de los Componente del Producto.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CalcularValorProductoComponente(FacturaAtencionDetalle detalle)
        {
            var resultado = from item in detalle.VentaComponentes
                            group item by item.IdProducto into sumaComponentes
                            select new
                            {
                                ValorBruto = sumaComponentes.Sum(a => a.ValorUnitario * a.CantidadComponente),
                                ValorDescuento = sumaComponentes.Sum(a => a.ValorDescuento),
                                ValorOriginal = sumaComponentes.Sum(a => a.ValorOriginal),
                                ValorProductos = sumaComponentes.Sum(a => a.ValorProductos),
                                ValorRecargo = sumaComponentes.Sum(a => a.ValorRecargo),
                                ValorTotal = sumaComponentes.Sum(a => a.ValorTotal),
                                ValorTotalDescuento = sumaComponentes.Sum(a => a.ValorTotalDescuento),
                                ValorTotalRecargo = sumaComponentes.Sum(a => a.ValorTotalRecargo),
                                ValorUnitario = sumaComponentes.Sum(a => a.ValorUnitario)
                            };

            var sumatoria = resultado.FirstOrDefault();

            if (sumatoria != null)
            {
                detalle.ValorUnitario = sumatoria.ValorUnitario;
                detalle.ValorRecargo = sumatoria.ValorRecargo;
                detalle.ValorDescuento = sumatoria.ValorDescuento;
                detalle.ValorProductos = sumatoria.ValorProductos;
                detalle.ValorTotalRecargo = sumatoria.ValorTotalRecargo;
                detalle.ValorTotalDescuento = sumatoria.ValorTotalDescuento;
                detalle.ValorTotal = sumatoria.ValorTotal;
            }
        }

        /// <summary>
        /// Metodo para realizar el calculo del valor del producto.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="producto">The producto.</param>
        /// <returns>
        /// Valor del Producto.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 26/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal CalcularValorUnitario(DetalleTarifa detalle, CondicionContrato condicionContrato, BaseValidacion producto)
        {
            TarifaUnidad unidad = null;
            decimal resultado = 0;

            if (detalle != null)
            {
                if (condicionContrato.FechaVigenciaResultado < detalle.FechaVigenciaTarifa)
                {
                    unidad = this.BuscarUnidad(condicionContrato, detalle.CodigoUnidad, detalle.FechaVigenciaTarifa);
                }
                else
                {
                    unidad = this.BuscarUnidad(condicionContrato, detalle.CodigoUnidad, condicionContrato.FechaVigenciaResultado);
                }

                if (unidad == null)
                {
                    unidad = new TarifaUnidad { ValorUnidad = 1 };
                }

                if (producto is FacturaAtencionDetalle && (producto.CondicionesTarifa == null || producto.CondicionesTarifa.Count == 0))
                {
                    if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Principal)
                    {
                        if (condicionContrato.Porcentaje > 100 || condicionContrato.Porcentaje < 100)
                        {
                            resultado = detalle.ValorTarifa * unidad.ValorUnidad * (condicionContrato.Porcentaje / 100);
                        }
                        else if (condicionContrato == null || condicionContrato.Porcentaje == 100 ||
                                 condicionContrato.Porcentaje == 0)
                        {
                            resultado = detalle.ValorTarifa * unidad.ValorUnidad;
                        }
                    }
                }
                else if (producto.CondicionesTarifa == null || producto.CondicionesTarifa.Count == 0)
                {
                    if (condicionContrato.Porcentaje > 100 || condicionContrato.Porcentaje < 100)
                    {
                        resultado = producto.ValorBruto * unidad.ValorUnidad * (condicionContrato.Porcentaje / 100);
                    }
                    else if (condicionContrato == null || condicionContrato.Porcentaje == 100 ||
                             condicionContrato.Porcentaje == 0)
                    {
                        resultado = producto.ValorBruto * unidad.ValorUnidad;
                    }
                }
                else
                {
                    if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Principal)
                    {
                        resultado = ((detalle.ValorTarifa * producto.CondicionesTarifa.FirstOrDefault().ValorPropio) / 100) * unidad.ValorUnidad;
                    }
                    else if (condicionContrato.TipoCondicionAplicada == CondicionContrato.TipoCondicion.Alterno)
                    {
                        resultado = detalle.ValorTarifa * unidad.ValorUnidad;
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para calcular los valores Unitarios.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="componente">The componente.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <returns>
        /// El valor unitario.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 02/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal CalcularValorUnitario(FacturaAtencion atencion, FacturaAtencionDetalle detalle, VentaComponente componente, CondicionContrato condicionContrato, List<CondicionProceso> condiciones)
        {
            TarifaUnidad unidad = null;
            if (detalle.DetalleTarifa != null)
            {
                unidad = this.BuscarUnidad(condicionContrato, detalle.DetalleTarifa.CodigoUnidad, detalle.DetalleTarifa.FechaVigenciaTarifa);
            }

            if (unidad == null)
            {
                unidad = new TarifaUnidad { ValorUnidad = 1 };
            }

            var nivelComplejidad = componente.NivelesComplejidad;

            // Si el componente tiene un manual configurado, el valor unitario se debe calcular en base a ese manual y no siempre en base al configurado para la atención.
            int identificadorManualEvaluar = 0;

            if (nivelComplejidad != null && nivelComplejidad.IdManual > 0)
            {
                identificadorManualEvaluar = nivelComplejidad.IdManual;
            }
            else
            {
                identificadorManualEvaluar = atencion.IdManual;
            }

            switch (identificadorManualEvaluar)
            {
                case 2: // MANUAL ISS

                    if (nivelComplejidad.TipoValorComponente == "M")
                    {
                        if (unidad != null && nivelComplejidad != null)
                        {
                            var tarifacomponente2 = detalle.DetalleTarifa != null ?
                                (nivelComplejidad.ValorComponente * unidad.ValorUnidad * detalle.DetalleTarifa.ValorTarifa * (condicionContrato.Porcentaje / 100))
                                : (nivelComplejidad.ValorComponente * unidad.ValorUnidad * (condicionContrato.Porcentaje / 100));
                            componente.ValorUnitario = (componente.TipoProductos == TipoProd.Insumos || componente.TipoProductos == TipoProd.Medicamentos) ? tarifacomponente2 : this.Redondeo(tarifacomponente2, condicionContrato.IdContrato, condicionContrato.IdManual);
                        }
                    }
                    else
                    {
                        if (unidad != null && nivelComplejidad != null)
                        {
                            var tarifacomponente3 = nivelComplejidad.ValorComponente * unidad.ValorUnidad * (condicionContrato.Porcentaje / 100);
                            componente.ValorUnitario = (componente.TipoProductos == TipoProd.Insumos || componente.TipoProductos == TipoProd.Medicamentos) ? tarifacomponente3 : this.Redondeo(tarifacomponente3, condicionContrato.IdContrato, condicionContrato.IdManual);
                        }
                    }

                    break;

                case 3: // MANUAL SOAT

                    if (unidad != null && nivelComplejidad != null)
                    {
                        var tarifacomponente = detalle.DetalleTarifa != null ?
                            nivelComplejidad.ValorComponente * unidad.ValorUnidad * detalle.DetalleTarifa.ValorTarifa * (condicionContrato.Porcentaje / 100)
                            : nivelComplejidad.ValorComponente * unidad.ValorUnidad * (condicionContrato.Porcentaje / 100);
                        componente.ValorUnitario = (componente.TipoProductos == TipoProd.Insumos || componente.TipoProductos == TipoProd.Medicamentos) ? tarifacomponente : this.Redondeo(tarifacomponente, condicionContrato.IdContrato, condicionContrato.IdManual);
                    }

                    break;

                default: // TODOS LOS DEMAS MANUALES

                    List<string> listTiposComponentes = new FachadaFacturacion().ObtenerTiposComponentes();

                    string tipoComponente = null;

                    if (listTiposComponentes != null && listTiposComponentes.Count > 0)
                    {
                        tipoComponente = listTiposComponentes.Where(c => c.Contains(componente.ComponenteBase)).FirstOrDefault();
                    }

                    // if (componente.ComponenteBase == "DS" || componente.ComponenteBase == "AY" || componente.ComponenteBase == "DO" || componente.ComponenteBase == "AN" || componente.ComponenteBase == "DE" || componente.ComponenteBase == "CP" || componente.ComponenteBase == "IN" || componente.ComponenteBase == "RA")
                    if (!string.IsNullOrEmpty(tipoComponente))
                    {
                        if (nivelComplejidad != null)
                        {
                            componente.ValorUnitario = nivelComplejidad.ValorComponente;
                        }
                        else
                        {
                            var tarifacomponente4 = detalle.CondicionesTarifa == null ? componente.ValorUnitario : componente.ValorUnitario * (condicionContrato.Porcentaje / 100);
                            componente.ValorUnitario = (componente.TipoProductos == TipoProd.Insumos || componente.TipoProductos == TipoProd.Medicamentos) ? tarifacomponente4 : this.Redondeo(tarifacomponente4, condicionContrato.IdContrato, condicionContrato.IdManual);
                        }
                    }
                    else
                    {
                        var tarifacomponente4 = detalle.CondicionesTarifa == null ? componente.ValorUnitario : componente.ValorUnitario * (condicionContrato.Porcentaje / 100);
                        componente.ValorUnitario = (componente.TipoProductos == TipoProd.Insumos || componente.TipoProductos == TipoProd.Medicamentos) ? tarifacomponente4 : this.Redondeo(tarifacomponente4, condicionContrato.IdContrato, condicionContrato.IdManual);
                    }

                    break;
            }

            var condicionTarifaComponente = this.ValidarCondicionesAjusteTarifa(componente.CondicionesTarifa, TipoTarifa.AjusteTarifas);

            if (condicionTarifaComponente != null)
            {
                componente.ValorUnitario = (condicionTarifaComponente.ValorPropio / 100) * componente.ValorUnitario;
            }

            return componente.ValorUnitario;
        }

        /// <summary>
        /// Realiza la carga de las reglas de prioridad de validaciones.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Lista las condiciones de proceso.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CondicionProceso> CargarCondicionesProceso(ProcesoFactura procesoFactura)
        {
            object resultado = null;

            ////se carga solo una vez
            if (this.CondicProceso == null)
            {
                this.CondicProceso = FachadaFacturacionXML.ConsultarCondicionesProceso();
            }

            var condicionesProceso = this.CondicProceso;

            foreach (var item in condicionesProceso)
            {
                item.NodosValidacion = FachadaFacturacionXML.CargarArbolValidaciones(item.Nombre, item.DocumentoXML);
                resultado = Enum.Parse(typeof(CondicionProceso.TipoObjeto), item.Id.ToString());

                if (resultado != null)
                {
                    item.Tipo = (CondicionProceso.TipoObjeto)resultado;
                    this.CargarReglasValidaciones(procesoFactura, item);
                }
            }

            return condicionesProceso;
        }

        /// <summary>
        /// Carga los Objetos dependientes.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CargarDetallesFactura(List<FacturaAtencion> atenciones, int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var clientes = fachada.ConsultarClientesAtencion(identificadorProceso);
            var conceptosCobro = fachada.ConsultarConceptosCobro(identificadorProceso);
            var movimientosTesoreria = fachada.ConsultarMovimientosTesoreria(identificadorProceso);
            var detallesVenta = fachada.ConsultarDetallesVenta(identificadorProceso);

            foreach (var atencion in atenciones)
            {
                atencion.Cliente = this.BuscarClientexIdCliente(clientes, atencion.IdCliente);
                atencion.ConceptosCobro = this.BuscarConceptoCobroxAtencion(conceptosCobro, atencion.IdAtencion);
                atencion.MovimientosTesoreria = this.BuscarMovimientosTesoreriaxAtencion(movimientosTesoreria, atencion.IdAtencion);

                foreach (var detalle in atencion.Detalle)
                {
                    detalle.DetalleVenta = this.FiltrarDetalleVenta(detallesVenta, detalle);
                }
            }
        }

        /// <summary>
        /// Carga detalles de la factura de forma individual.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="responsablesVentas">The responsables ventas.</param>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 07/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarDetallesFactura(FacturaAtencion atencion, int identificadorProceso, List<VentaResponsable> responsablesVentas)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var clientes = fachada.ConsultarClientesAtencion(identificadorProceso);
            var conceptosCobro = fachada.ConsultarConceptosCobro(identificadorProceso);
            var movimientosTesoreria = fachada.ConsultarMovimientosTesoreria(identificadorProceso);

            ////Solo cargar una vez, siempre carga la misma informacion
            if (this.gblVentaDetalle == null)
            {
                this.gblVentaDetalle = fachada.ConsultarDetallesVenta(identificadorProceso);
            }

            var detallesVenta = this.gblVentaDetalle;

            atencion.Cliente = this.BuscarClientexIdCliente(clientes, atencion.IdCliente);
            atencion.ConceptosCobro = this.BuscarConceptoCobroxAtencion(conceptosCobro, atencion.IdAtencion);
            atencion.MovimientosTesoreria = this.BuscarMovimientosTesoreriaxAtencion(movimientosTesoreria, atencion.IdAtencion);

            foreach (var detalle in atencion.Detalle)
            {
                detalle.DetalleVenta = this.FiltrarDetalleVenta(detallesVenta, detalle);
                foreach (var componente in detalle.VentaComponentes)
                {
                    var responsable = this.ObtenerResponsableVenta(responsablesVentas, componente.IdProducto, componente.IdTransaccion, componente.NumeroVenta);

                    if (responsable != null)
                    {
                        componente.Responsable = responsable;
                    }
                }
            }
        }

        /// <summary>
        /// Carga los Objetos dependientes - Facturación No Clínica.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CargarDetallesFacturaNC(ref List<FacturaAtencion> atenciones, int identificadorProceso)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            var clientes = fachada.ConsultarClientesAtencionNC(identificadorProceso);
            var conceptosCobro = fachada.ConsultarConceptosCobroNC(identificadorProceso);
            var movimientosTesoreria = fachada.ConsultarMovimientosTesoreriaNC(identificadorProceso);
            var detallesVenta = fachada.ConsultarDetallesVentaNC(identificadorProceso);

            foreach (var atencion in atenciones)
            {
                atencion.Cliente = this.BuscarClientexIdCliente(clientes, atencion.IdCliente);
                atencion.ConceptosCobro = this.BuscarConceptoCobroxAtencion(conceptosCobro, atencion.IdAtencion);
                atencion.MovimientosTesoreria = this.BuscarMovimientosTesoreriaxAtencion(movimientosTesoreria, atencion.IdAtencion);

                foreach (var detalle in atencion.Detalle)
                {
                    detalle.DetalleVenta = this.FiltrarDetalleVenta(detallesVenta, detalle);
                }
            }
        }

        /// <summary>
        /// Carga la informacion correspondiente al tipo de objeto.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="tipoObjeto">The tipo objeto.</param>
        /// <returns>
        /// Objeto resultado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CondicionProceso CargarInformacionTipo(List<CondicionProceso> condiciones, CondicionProceso.TipoObjeto tipoObjeto)
        {
            var resultado = from
                                item in condiciones
                            where
                                item.Tipo == tipoObjeto
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para cargas las reglas de validacion.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="condicionProceso">The condicion proceso.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CargarReglasValidaciones(ProcesoFactura procesoFactura, CondicionProceso condicionProceso)
        {
            FachadaFacturacion fachadaFacturacion = new FachadaFacturacion();

            switch (condicionProceso.Tipo)
            {
                case CondicionProceso.TipoObjeto.Exclusiones:

                    var exclusion = new Exclusion()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdTercero = procesoFactura.IdTercero,
                        IdContrato = procesoFactura.IdContrato,
                        IndicadorContratoActivo = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarExclusionesContrato(exclusion);
                    break;

                case CondicionProceso.TipoObjeto.ExclusionesManual:

                    var tarifaExclusion = new ExclusionManual()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdTercero = procesoFactura.IdTercero,
                        IdContrato = procesoFactura.IdContrato
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarExclusionesManual(tarifaExclusion);
                    break;

                case CondicionProceso.TipoObjeto.CondicionesTarifa:
                    var condicionTarifa = new CondicionTarifa()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdContrato = procesoFactura.IdContrato,
                        IdTercero = procesoFactura.IdTercero,
                        TipoRelacion = procesoFactura.TipoRelacion,
                        IndHabilitado = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarCondicionesTarifas(condicionTarifa);
                    break;

                case CondicionProceso.TipoObjeto.Recargos:
                    var recargo = new Recargo()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdContrato = procesoFactura.IdContrato,
                        IdTercero = procesoFactura.IdTercero,
                        IndicadorActivo = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarRecargosContrato(recargo);
                    break;

                case CondicionProceso.TipoObjeto.RecargosManual:
                    var recargoManual = new RecargoManual()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdContrato = procesoFactura.IdContrato,
                        IdTercero = procesoFactura.IdTercero,
                        IndicadorActivo = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarRecargosManual(recargoManual);
                    break;

                case CondicionProceso.TipoObjeto.Descuentos:
                    var descuento = new Descuento()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdContrato = procesoFactura.IdContrato,
                        IdTercero = procesoFactura.IdTercero,
                        IndicadorActivo = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarDescuentosContrato(descuento);
                    break;

                case CondicionProceso.TipoObjeto.CostoAsociado:
                    var costoAsociado = new CostoAsociado()
                    {
                        IdAtencion = procesoFactura.Detalles[0].IdAtencion
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarCostoAsociado(costoAsociado);
                    break;

                case CondicionProceso.TipoObjeto.DefinirCubrimiento:
                    var cubrimiento = new Cubrimiento()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdAtencion = procesoFactura.Detalles[0].IdAtencion,
                        IdContrato = procesoFactura.IdContrato,
                        IdPlan = procesoFactura.Detalles[0].IdPlan,
                        IndHabilitado = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarProductosCubrimientos(cubrimiento);
                    break;

                case CondicionProceso.TipoObjeto.CondicionesCubrimiento:
                    var condicionCubrimiento = new CondicionCubrimiento()
                    {
                        CodigoEntidad = procesoFactura.CodigoEntidad,
                        IdAtencion = procesoFactura.Detalles[0].IdAtencion,
                        IdContrato = procesoFactura.IdContrato,
                        IdPlan = procesoFactura.Detalles[0].IdPlan,
                        IndHabilitado = procesoFactura.IndHabilitado
                    };

                    condicionProceso.Objeto = fachadaFacturacion.ConsultarProductosCondicionCubrimientos(condicionCubrimiento);
                    break;
            }
        }

        /// <summary>
        /// Carga los responsables en base al xml.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Condici n de proceso para Responsable.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Paginacion<List<Honorario>> CargarResponsableHonorariosXML(Paginacion<Honorario> honorario)
        {
            Paginacion<List<Honorario>> responsablesAplicar = new Paginacion<List<Honorario>>();
            var condicionesProceso = FachadaFacturacionXML.ConsultarCondicionesProceso();

            var condicion = (from
                                 item in condicionesProceso
                             where
                                 item.Id == CondicionProceso.TipoObjeto.Responsable.GetHashCode()
                             select new CondicionProceso()
                             {
                                 Id = item.Id,
                                 NodosValidacion = FachadaFacturacionXML.CargarArbolValidaciones(item.Nombre, item.DocumentoXML),
                                 Objeto = this.ConsultarHonorariosMedicosxProducto(honorario)
                             }).FirstOrDefault();

            if (condicion != null)
            {
                var responsables = condicion.Objeto as Paginacion<List<Honorario>>;
                responsables = responsables == null ? responsables = new Paginacion<List<Honorario>>() : responsables;

                var honorarios = this.FiltrarHonorariosResponsables(responsables.Item, honorario.Item);

                foreach (var item in honorarios)
                {
                    var honorarioResultado = this.ObtenerResponsableVentas(condicion.NodosValidacion, honorario.Item, item);

                    if (honorarioResultado != null)
                    {
                        if (responsablesAplicar.Item.Where(c => c.IdPersonal == honorarioResultado.IdPersonal).Count() == 0)
                        {
                            responsablesAplicar.Item.Add(honorarioResultado);
                        }
                    }
                }

                honorario.LongitudPagina = responsablesAplicar.Item.Count;
                honorario.TotalRegistros = responsablesAplicar.Item.Count;
            }

            return responsablesAplicar != null ? responsablesAplicar : new Paginacion<List<Honorario>>();
        }

        /// <summary>
        /// Carga toda la informacion para tarifas.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Objeto condicion contrato.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CondicionContrato CargueInformacionCondicionContrato(ProcesoFactura procesoFactura)
        {
            var condicionContrato = this.ConsultarCondicionContrato(new CondicionContrato { IdContrato = procesoFactura.IdContrato });

            ////Se carga una sola vez siempre tiene el mismo idproceso
            if (this.homologaProductos == null)
            {
                this.homologaProductos = this.ConsultarHomologacionProducto(procesoFactura.IdProceso);
            }

            ////Se carga una sola vez siempre tiene el mismo idproceso
            if (this.detalleTarifaProductos == null)
            {
                this.detalleTarifaProductos = this.ConsultarDetalleTarifa(procesoFactura.IdProceso);
            }

            condicionContrato.HomologacionProductos = this.homologaProductos;
            condicionContrato.DetalleTarifa = this.detalleTarifaProductos;

            return condicionContrato;
        }

        /// <summary>VentasNoMarcadas
        /// Clonar objeto de tipo Base Validacion.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <returns>Objeto clonado de tipo BaseValidacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion ClonarObjeto(BaseValidacion baseValidacion)
        {
            BaseValidacion baseCalculo = null;

            if (baseValidacion is FacturaAtencionDetalle)
            {
                baseCalculo = (baseValidacion as FacturaAtencionDetalle).CopiarObjeto();
            }
            else
            {
                baseCalculo = (baseValidacion as VentaComponente).CopiarObjeto();
            }

            return baseCalculo;
        }

        /// <summary>
        /// Metodo para realizar los cambios en la seleccion de parametros de la Condicion del Contrato.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="identificadorManual">The id manual.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CondicionContratoResultado(CondicionContrato condicionContrato, int identificadorManual)
        {
            if (condicionContrato.IdManual == identificadorManual)
            {
                condicionContrato.TipoCondicionAplicada = CondicionContrato.TipoCondicion.Principal;
                condicionContrato.IdManualResultado = condicionContrato.IdManual;
                condicionContrato.FechaVigenciaResultado = condicionContrato.FechaVigencia;
            }
            else if (condicionContrato.IdManualAlterno == identificadorManual)
            {
                condicionContrato.TipoCondicionAplicada = CondicionContrato.TipoCondicion.Alterno;
                condicionContrato.IdManualResultado = condicionContrato.IdManualAlterno;
                condicionContrato.FechaVigenciaResultado = condicionContrato.FechaVigenciaAlterna;
            }
            else if (condicionContrato.IdManualInstitucional == identificadorManual)
            {
                condicionContrato.TipoCondicionAplicada = CondicionContrato.TipoCondicion.Institucional;
                condicionContrato.IdManualResultado = condicionContrato.IdManualInstitucional;
                condicionContrato.FechaVigenciaResultado = DateTime.Now;
            }
        }

        /// <summary>
        /// Crea un objeto de movimiento para el abono.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="cuenta">The cuenta.</param>
        /// <returns>Objeto Movimiento Cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private MovimientoCartera CrearAbono(EstadoCuentaEncabezado encabezado, CuentaCartera cuenta)
        {
            FacturaAtencionConceptoCobro concepto = (from item in encabezado.FacturaAtencion.FirstOrDefault().ConceptosCobro
                                                     where item.IdContrato == encabezado.IdContrato && item.IdPlan == encabezado.IdPlan
                                                     select item).FirstOrDefault();

            var movimiento = new MovimientoCartera()
            {
                CodigoEntidad = encabezado.CodigoEntidad,
                IdMovimientoDocumento = ClaseMovimiento.Abono.GetHashCode(),
                CodigoMovimientoSecuencial = encabezado.InformacionFactura.MovimientoAbono,
                NumeroMovimientoPrevio = string.Empty,
                FechaRegistro = encabezado.FechaFactura,
                FechaMovimiento = encabezado.FechaFactura,
                IndHabilitado = 1,
                CodigoUsuario = encabezado.Usuario,
                IdSede = 1,
                DetalleMovimientoCartera = new DetalleMovimientoCartera()
                {
                    CodigoEntidad = cuenta.CodigoEntidad,
                    CodigoMovimiento = encabezado.InformacionFactura.MovimientoAbono,
                    NumeroDocumento = string.Empty,
                    ValorMonto = (encabezado.TipoFacturacion != TipoFacturacion.FacturacionRelacion && this.ValidarParticular(encabezado) && concepto != null && concepto.DepositoParticular) ? concepto.ValorConcepto : cuenta.ValorAbono,
                    ValorAfectado = (encabezado.TipoFacturacion != TipoFacturacion.FacturacionRelacion && this.ValidarParticular(encabezado) && concepto != null && concepto.DepositoParticular) ? concepto.ValorSaldo : cuenta.ValorSaldo,
                    InteresCuenta = 0,
                    DescuentoMovimiento = 0,
                    PorcentajeInteres = 0,
                    PorcentajeDescuento = 0,
                    DescuentoConcepto = 0,
                    InteresConcepto = 0,
                    SaldoInicial = 0,
                    SaldoFinal = 0,
                    IdTercero = cuenta.IdTercero,
                    CodigoEnlaceContable = string.Empty
                }
            };

            return movimiento;
        }

        /// <summary>
        /// Ingresa los parametros para la consulta de Productos relacionados a la venta.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <returns>
        /// Consulta paginada.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 17/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Paginacion<VentaProductoRelacion> CrearConsultaProductosRelacionados(int identificadorAtencion, int numeroVenta)
        {
            var paginacion = new Paginacion<VentaProductoRelacion>()
            {
                Item = new VentaProductoRelacion()
                {
                    IdAtencion = identificadorAtencion,
                    NumeroVenta = numeroVenta
                }
            };

            return paginacion;
        }

        /// <summary>
        /// Crea el objeto con informacion a registrar en contabilidad.
        /// </summary>
        /// <param name="cuenta">Parametro cuenta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="detalleMovimiento">The detalle movimiento.</param>
        /// <param name="identificadorMovimientoDocumento">The id movimiento documento.</param>
        /// <returns>
        /// Objeto Contabilidad.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Contabilidad CrearContabilidad(CuentaCartera cuenta, EstadoCuentaEncabezado estadoCuenta, DetalleMovimientoCartera detalleMovimiento, int identificadorMovimientoDocumento)
        {
            var contabilidad = new Contabilidad()
            {
                CodigoEntidad = cuenta.CodigoEntidad,
                CodigoMovimiento = detalleMovimiento.CodigoMovimiento,
                CodigoSecuencial = cuenta.CodigoEntidad,
                FechaRegistro = cuenta.FechaRegistroCuenta,
                FechaRegistroEstado = cuenta.FechaRegistroCuenta,
                IdTipoMovimiento = identificadorMovimientoDocumento,
                TipoMovimiento = this.EstablecerTipoMovimiento(estadoCuenta.TipoFacturacion, identificadorMovimientoDocumento),
                TipoRegistro = this.EstablecerTipoRegistroFactura(estadoCuenta.TipoFacturacion),
                NumeroFactura = detalleMovimiento.IdTipoMovimiento,
                ValorRegistro = cuenta.ValorAbono
            };

            contabilidad.EstadoRegistro = contabilidad.EstadoRegistroPendiente;
            contabilidad.TipoRegistro = contabilidad.TipoRegistroAbono;

            return contabilidad;
        }

        /// <summary>
        /// Crea la cuenta de cartera para la factura.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Objeto cuenta de cartera.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CuentaCartera CrearCuentaCarteraFactura(EstadoCuentaEncabezado estadoCuenta, int numeroFactura)
        {
            Cliente clienteAtencion = this.ConsultarClientexAtencion(estadoCuenta.IdAtencion);
            int identificadorCliente = 0;

            if (clienteAtencion != null)
            {
                identificadorCliente = clienteAtencion.IdCliente;
            }

            int terceroResponsable = 0;
            int clienteResponsable = 0;

            if (!this.ValidarParticular(estadoCuenta))
            {
                terceroResponsable = estadoCuenta.IdTercero;
                clienteResponsable = 0;
            }
            else if (this.ValidarParticular(estadoCuenta) && (estadoCuenta.Responsable == null || (estadoCuenta.Responsable.IdTercero == 0 && estadoCuenta.Responsable.IdCliente == 0)))
            {
                clienteResponsable = estadoCuenta.IdTercero;
                terceroResponsable = 0;
            }
            else if (this.ValidarParticular(estadoCuenta) && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdTercero > 0)
            {
                clienteResponsable = 0;
                terceroResponsable = estadoCuenta.Responsable.IdTercero;
            }
            else if (this.ValidarParticular(estadoCuenta) && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdCliente > 0)
            {
                clienteResponsable = estadoCuenta.Responsable.IdCliente;
                terceroResponsable = 0;
            }

            CuentaCartera cuentaFactura = new CuentaCartera()
            {
                CodigoEntidad = estadoCuenta.CodigoEntidad,
                CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                CodigoSeccion = estadoCuenta.CodigoSeccion,
                CodigoUsuario = estadoCuenta.Usuario,
                CuentaInicio = 1,
                DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                EstadoAnteriorCuenta = this.ValidarParticular(estadoCuenta) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.A.ToString(),
                EstadoCuenta = this.ValidarParticular(estadoCuenta) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.A.ToString(),
                FechaRadicacion = estadoCuenta.FechaFactura,
                FechaRegistroCuenta = estadoCuenta.FechaFactura,
                HoraRegistroCuenta = DateTime.Now,
                IdAtencion = this.ValidarParticular(estadoCuenta) ? estadoCuenta.IdAtencion : 0,
                IdCliente = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
                IdConcepto = estadoCuenta.InformacionFactura.ConceptoFacturaGenerada,
                IdContrato = estadoCuenta.IdContrato,
                IdPlan = this.ValidarParticular(estadoCuenta) ? estadoCuenta.IdPlan : 0,
                IdSede = 1,
                IdTercero = estadoCuenta.IdTercero,
                IdTerceroResponsable = terceroResponsable,
                IdClienteResponsable = clienteResponsable,
                IdTipoCartera = 0,
                IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                IndDetalleFactura = 1,
                IndHabilitado = 1,
                IndRadicacion = 0,
                IndRelacionRadicado = 0,
                NumeroResultadoDocumento = numeroFactura,
                Observaciones = estadoCuenta.Observaciones,
                ValorMonto = this.ValidarParticular(estadoCuenta) ? estadoCuenta.ValorTotalFacturado : estadoCuenta.ValorSaldo,
                ValorSaldo = estadoCuenta.ValorSaldo
            };

            return cuentaFactura;
        }

        /// <summary>
        /// Crea la cuenta de cartera para la factura.
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Objeto cuenta de cartera.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CuentaCartera CrearCuentaCarteraFacturaNC(CuentaCartera cuenta, EstadoCuentaEncabezado estadoCuenta, int numeroFactura)
        {
            Cliente clienteAtencion = this.ConsultarClientexAtencion(estadoCuenta.IdAtencion);
            int identificadorCliente = 0;

            if (clienteAtencion != null)
            {
                identificadorCliente = clienteAtencion.IdCliente;
            }

            int terceroResponsable = 0;
            int clienteResponsable = 0;

            if (!this.ValidarParticularNC(estadoCuenta))
            {
                terceroResponsable = estadoCuenta.IdTercero;
                clienteResponsable = 0;
            }
            else if (this.ValidarParticularNC(estadoCuenta) && (estadoCuenta.Responsable == null || (estadoCuenta.Responsable.IdTercero == 0 && estadoCuenta.Responsable.IdCliente == 0)))
            {
                clienteResponsable = estadoCuenta.IdTercero;
                terceroResponsable = 0;
            }
            else if (this.ValidarParticularNC(estadoCuenta) && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdTercero > 0)
            {
                clienteResponsable = 0;
                terceroResponsable = estadoCuenta.Responsable.IdTercero;
            }
            else if (this.ValidarParticularNC(estadoCuenta) && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdCliente > 0)
            {
                clienteResponsable = estadoCuenta.Responsable.IdCliente;
                terceroResponsable = 0;
            }

            cuenta.IdCliente = 9999999;
            cuenta.IdTerceroResponsable = terceroResponsable;
            cuenta.IdClienteResponsable = clienteResponsable;
            cuenta.IdAtencion = 0;

            bool boolEsParticular = this.ValidarParticularNC(estadoCuenta);

            if (!boolEsParticular)
            {
                cuenta.EstadoCuenta = "A";
                cuenta.EstadoAnteriorCuenta = "A";
            }

            // CuentaCartera cuentaFactura = new CuentaCartera()
            // {
            //    CodigoEntidad = estadoCuenta.CodigoEntidad,
            //    CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
            //    CodigoSeccion = estadoCuenta.CodigoSeccion,
            //    CodigoUsuario = estadoCuenta.Usuario,
            //    CuentaInicio = 1,
            //    DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
            //    EstadoAnteriorCuenta = this.ValidarParticular(estadoCuenta) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.A.ToString(),
            //    EstadoCuenta = this.ValidarParticular(estadoCuenta) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.A.ToString(),
            //    FechaRadicacion = estadoCuenta.FechaFactura,
            //    FechaRegistroCuenta = estadoCuenta.FechaFactura,
            //    HoraRegistroCuenta = DateTime.Now,
            //    IdAtencion = this.ValidarParticular(estadoCuenta) ? estadoCuenta.IdAtencion : 0,
            //    IdCliente = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
            //    IdConcepto = estadoCuenta.InformacionFactura.ConceptoFacturaGenerada,
            //    IdContrato = estadoCuenta.IdContrato,
            //    IdPlan = this.ValidarParticular(estadoCuenta) ? estadoCuenta.IdPlan : 0,
            //    IdSede = 1,
            //    IdTercero = estadoCuenta.IdTercero,
            //    IdTerceroResponsable = terceroResponsable,
            //    IdClienteResponsable = clienteResponsable,
            //    IdTipoCartera = 0,
            //    IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
            //    IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
            //    IndDetalleFactura = 1,
            //    IndHabilitado = 1,
            //    IndRadicacion = 0,
            //    IndRelacionRadicado = 0,
            //    NumeroResultadoDocumento = numeroFactura,
            //    Observaciones = estadoCuenta.Observaciones,
            //    ValorMonto = this.ValidarParticular(estadoCuenta) ? estadoCuenta.ValorTotalFacturado : estadoCuenta.ValorSaldo,
            //    ValorSaldo = estadoCuenta.ValorSaldo
            // };
            return cuenta;
        }

        /// <summary>
        /// Inserta un nuevo registro inicial de encabezado de cuenta cartera.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Cuenta principal.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CuentaCartera CrearCuentaCarteraPrincipal(EstadoCuentaEncabezado estadoCuenta)
        {
            Cliente clienteAtencion = this.ConsultarClientexAtencion(estadoCuenta.IdAtencion);
            int identificadorCliente = 0;

            if (clienteAtencion != null)
            {
                identificadorCliente = clienteAtencion.IdCliente;
            }

            CuentaCartera cuentaPrincipal = null;
            var atenciones = from
                                 item in estadoCuenta.FacturaAtencion
                             where
                                 item.Cruzar
                                 && item.ConceptosCobro.Count() > 0
                                 && item.Detalle.Where(sp => sp.Exclusion == null && sp.ExclusionManual == null).Count() > 0
                             from
                                 concepto in item.ConceptosCobro
                             where
                                 concepto.ValorSaldo == 0
                             select new
                             {
                                 IdAtencion = item.IdAtencion,
                                 IdCliente = item.IdCliente,
                                 Concepto = concepto,
                                 Venta = item.Detalle
                             };

            if (atenciones.Count() > 0)
            {
                decimal sumatoria = 0;

                if (estadoCuenta.IdPlan != 482)
                {
                    sumatoria = atenciones.Sum(sp => sp.Concepto.ValorConcepto);
                }
                else if (estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion)
                {
                    if (atenciones.FirstOrDefault().Concepto.DepositoParticular)
                    {
                        sumatoria = atenciones.Sum(sp => sp.Concepto.ValorConcepto);
                    }
                }

                cuentaPrincipal = new CuentaCartera()
                {
                    CodigoEntidad = estadoCuenta.CodigoEntidad,
                    CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                    CodigoSeccion = estadoCuenta.CodigoSeccion,
                    CodigoUsuario = estadoCuenta.Usuario,
                    CuentaInicio = 1,
                    DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                    EstadoAnteriorCuenta = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.P.ToString(),
                    EstadoCuenta = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.P.ToString(),
                    FechaRadicacion = estadoCuenta.FechaFactura,
                    FechaRegistroCuenta = estadoCuenta.FechaFactura,
                    HoraRegistroCuenta = DateTime.Now,
                    IdAtencion = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? estadoCuenta.IdAtencion : 0,
                    IdCliente = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
                    IdClienteResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
                    IdConcepto = estadoCuenta.InformacionFactura.IdConceptoAbono,
                    IdContrato = estadoCuenta.InformacionFactura.IdContratoParticular,
                    IdPlan = estadoCuenta.InformacionFactura.IdPlanParticular,
                    IdSede = 1,
                    IdTercero = estadoCuenta.InformacionFactura.IdTerceroParticular,
                    IdTerceroResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? 0 : estadoCuenta.IdTercero,
                    IdTipoCartera = 0,
                    IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                    IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                    IndDetalleFactura = 1,
                    IndHabilitado = 1,
                    IndRadicacion = 1,
                    IndRelacionRadicado = 1,
                    NumeroResultadoDocumento = 0,
                    Observaciones = estadoCuenta.Observaciones,
                    ValorMonto = sumatoria,
                    ValorSaldo = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? sumatoria : 0
                };
            }

            return cuentaPrincipal;
        }

        /// <summary>
        /// Inserta un nuevo registro inicial de encabezado de cuenta cartera. - Facturación No Clínica
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Cuenta principal.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 01/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CuentaCartera CrearCuentaCarteraPrincipalNC(EstadoCuentaEncabezado estadoCuenta)
        {
            int identificadorCliente = estadoCuenta.IdTercero;

            CuentaCartera cuentaPrincipal = null;

            var atenciones = from
                                 item in estadoCuenta.FacturaAtencion
                             where
                                 item.Cruzar
                                 && item.Detalle.Where(sp => sp.Exclusion == null && sp.ExclusionManual == null).Count() > 0
                             select new
                             {
                                 IdAtencion = item.IdAtencion,
                                 IdCliente = item.IdCliente,
                                 Venta = item.Detalle
                             };

            if (atenciones.Count() > 0)
            {
                decimal sumatoria = 0;

                foreach (var fac in atenciones)
                {
                    foreach (var facAte in fac.Venta)
                    {
                        sumatoria += facAte.ValorSubTotal;
                    }
                }

                cuentaPrincipal = new CuentaCartera()
                {
                    CodigoEntidad = estadoCuenta.CodigoEntidad,
                    CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoFacturacion,
                    CodigoSeccion = estadoCuenta.CodigoSeccion,
                    CodigoUsuario = estadoCuenta.Usuario,
                    CuentaInicio = 1,
                    DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                    EstadoAnteriorCuenta = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.P.ToString(),
                    EstadoCuenta = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? CuentaCartera.EstadoRegistro.R.ToString() : CuentaCartera.EstadoRegistro.P.ToString(),
                    FechaRadicacion = estadoCuenta.FechaFactura,
                    FechaRegistroCuenta = estadoCuenta.FechaFactura,
                    HoraRegistroCuenta = DateTime.Now,
                    IdAtencion = 0,
                    IdCliente = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
                    IdClienteResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : 0,
                    IdConcepto = estadoCuenta.InformacionFactura.IdConceptoAbono,
                    IdContrato = 0,
                    IdPlan = estadoCuenta.InformacionFactura.IdPlanParticular,
                    IdSede = 1,
                    IdTercero = estadoCuenta.InformacionFactura.IdTerceroParticular,
                    IdTerceroResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? 0 : estadoCuenta.IdTercero,
                    IdTipoCartera = 1,
                    IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                    IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                    IndDetalleFactura = 1,
                    IndHabilitado = 1,
                    IndRadicacion = 1,
                    IndRelacionRadicado = 1,
                    NumeroResultadoDocumento = 0,
                    Observaciones = estadoCuenta.Observaciones,
                    ValorMonto = sumatoria,
                    ValorSaldo = sumatoria,
                };
            }

            return cuentaPrincipal;
        }

        /// <summary>
        /// Inserta los detalles de cuenta cartera que cumplen ciertos criterios.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 12/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearCuentasCartera(FacturaCompuesta facturaCompuesta)
        {
            try
            {
                var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
                var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;
                List<CuentaCartera> cuentas = new List<CuentaCartera>();

                var cuentaPrincipal = this.CrearCuentaCarteraPrincipal(estadoCuenta);
                if (cuentaPrincipal != null)
                {
                    cuentas.Add(cuentaPrincipal);
                }

                cuentas.AddRange(this.CrearCuentasCarteraDiferencia(estadoCuenta));

                foreach (var cuenta in cuentas)
                {
                    cuenta.NumeroResultadoDocumento = encabezadoFactura.NumeroFactura;
                    cuenta.IdPlan = encabezadoFactura.IdPlan;
                    cuenta.IdAtencion = encabezadoFactura.IdAtencion;
                    cuenta.IdCuenta = this.GuardarInformacionCuentaCartera(cuenta);
                    cuenta.MovimientosCartera = this.CrearMovimientosCartera(estadoCuenta, cuenta);
                    this.CrearMovimientos(cuenta, encabezadoFactura, estadoCuenta);

                    if (cuenta.MovimientosCartera.Count == 2)
                    {
                        var movimiento = this.ObtenerIdMovimiento(cuenta, estadoCuenta.InformacionFactura.MovimientoAbono);
                        this.GuardarEstadoCuentaContabilidad(this.CrearContabilidad(cuenta, estadoCuenta, movimiento.DetalleMovimientoCartera, movimiento.IdMovimientoDocumento));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Retorna las cuentas de cartera que se van a insertar en movimientos cartera.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Lista de cuentas de cartera.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CuentaCartera> CrearCuentasCarteraDiferencia(EstadoCuentaEncabezado estadoCuenta)
        {
            Cliente clienteAtencion = this.ConsultarClientexAtencion(estadoCuenta.IdAtencion);
            int identificadorCliente = 0;

            if (clienteAtencion != null)
            {
                identificadorCliente = clienteAtencion.IdCliente;
            }

            var atencionesDetalleConMovimientos = (from
                                                       item in estadoCuenta.FacturaAtencion
                                                   from
                                                       concepto in item.ConceptosCobro
                                                   where
                                                       item.Cruzar == true
                                                       && item.Detalle.Where(sp => sp.Exclusion == null && sp.ExclusionManual == null).Count() > 0
                                                       && item.ConceptosCobro != null
                                                       && item.ConceptosCobro.Count > 0
                                                       && item.ConceptosCobro.Sum(sp => sp.ValorConcepto) > 0
                                                       && concepto.ValorSaldo > 0
                                                   select new CuentaCartera()
                                                   {
                                                       CodigoEntidad = estadoCuenta.CodigoEntidad,
                                                       CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                                                       CodigoSeccion = estadoCuenta.CodigoSeccion,
                                                       CodigoUsuario = estadoCuenta.Usuario,
                                                       CuentaInicio = 1,
                                                       DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                                                       EstadoCuenta = CuentaCartera.EstadoRegistro.R.ToString(),
                                                       EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.R.ToString(),
                                                       FechaRadicacion = DateTime.Now,
                                                       FechaRegistroCuenta = DateTime.Now,
                                                       HoraRegistroCuenta = DateTime.Now,
                                                       IdAtencion = item.IdAtencion,
                                                       IdCliente = item.IdCliente,
                                                       IdClienteResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : item.IdCliente,
                                                       IdConcepto = estadoCuenta.InformacionFactura.IdConceptoAbono,
                                                       IdContrato = estadoCuenta.InformacionFactura.IdContratoParticular,
                                                       IdPlan = estadoCuenta.InformacionFactura.IdPlanParticular,
                                                       IdSede = 1,
                                                       IdTercero = estadoCuenta.InformacionFactura.IdTerceroParticular,
                                                       IdTerceroResponsable = 0,
                                                       IdTipoCartera = 0,
                                                       IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                                                       IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                                                       IndDetalleFactura = 1,
                                                       IndHabilitado = 1,
                                                       IndRadicacion = 1,
                                                       IndRelacionRadicado = 1,
                                                       NumeroResultadoDocumento = 0,
                                                       TipoCuentaCartera = item.MovimientosTesoreria.Count > 0 ? CuentaCartera.TipoCuenta.AbonoRadicacion : CuentaCartera.TipoCuenta.Radicacion,
                                                       ValorMonto = this.ValidarParticular(estadoCuenta) ? estadoCuenta.AtencionActiva.Deposito.TotalDeposito : concepto.ValorConcepto,
                                                       ValorSaldo = concepto.ValorSaldo
                                                   }).ToList();
            return atencionesDetalleConMovimientos;
        }

        /// <summary>
        /// Retorna las cuentas de cartera que se van a insertar en movimientos cartera - Facturación No Clínica
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>
        /// Lista de cuentas de cartera.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 01/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CuentaCartera> CrearCuentasCarteraDiferenciaNC(EstadoCuentaEncabezado estadoCuenta)
        {
            int identificadorCliente = estadoCuenta.IdTercero;

            var atencionesDetalleConMovimientos = (from
                                                       item in estadoCuenta.FacturaAtencion
                                                   from
                                                       concepto in item.ConceptosCobro
                                                   where
                                                       item.Cruzar == true
                                                       && item.Detalle.Where(sp => sp.Exclusion == null && sp.ExclusionManual == null).Count() > 0
                                                       && item.ConceptosCobro != null
                                                       && item.ConceptosCobro.Count > 0
                                                       && item.ConceptosCobro.Sum(sp => sp.ValorConcepto) > 0
                                                       && concepto.ValorSaldo > 0
                                                   select new CuentaCartera()
                                                   {
                                                       CodigoEntidad = estadoCuenta.CodigoEntidad,
                                                       CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                                                       CodigoSeccion = estadoCuenta.CodigoSeccion,
                                                       CodigoUsuario = estadoCuenta.Usuario,
                                                       CuentaInicio = 1,
                                                       DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                                                       EstadoCuenta = CuentaCartera.EstadoRegistro.R.ToString(),
                                                       EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.R.ToString(),
                                                       FechaRadicacion = DateTime.Now,
                                                       FechaRegistroCuenta = DateTime.Now,
                                                       HoraRegistroCuenta = DateTime.Now,
                                                       IdAtencion = item.IdAtencion,
                                                       IdCliente = item.IdCliente,
                                                       IdClienteResponsable = estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion ? identificadorCliente : item.IdCliente,
                                                       IdConcepto = estadoCuenta.InformacionFactura.IdConceptoAbono,
                                                       IdContrato = estadoCuenta.InformacionFactura.IdContratoParticular,
                                                       IdPlan = estadoCuenta.InformacionFactura.IdPlanParticular,
                                                       IdSede = 1,
                                                       IdTercero = estadoCuenta.InformacionFactura.IdTerceroParticular,
                                                       IdTerceroResponsable = 0,
                                                       IdTipoCartera = 0,
                                                       IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                                                       IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                                                       IndDetalleFactura = 1,
                                                       IndHabilitado = 1,
                                                       IndRadicacion = 1,
                                                       IndRelacionRadicado = 1,
                                                       NumeroResultadoDocumento = 0,
                                                       TipoCuentaCartera = item.MovimientosTesoreria.Count > 0 ? CuentaCartera.TipoCuenta.AbonoRadicacion : CuentaCartera.TipoCuenta.Radicacion,
                                                       ValorMonto = this.ValidarParticular(estadoCuenta) ? estadoCuenta.AtencionActiva.Deposito.TotalDeposito : concepto.ValorConcepto,
                                                       ValorSaldo = concepto.ValorSaldo
                                                   }).ToList();
            return atencionesDetalleConMovimientos;
        }

        /// <summary>
        /// Inserta los detalles de cuenta cartera que cumplen ciertos criterios - Facturación No Clínica
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 01/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearCuentasCarteraNC(FacturaCompuesta facturaCompuesta)
        {
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;
            List<CuentaCartera> cuentas = new List<CuentaCartera>();

            var cuentaPrincipal = this.CrearCuentaCarteraPrincipalNC(estadoCuenta);

            if (cuentaPrincipal != null)
            {
                cuentas.Add(cuentaPrincipal);
            }

            int numeroCuenta = 0;

            foreach (var cuenta in cuentas)
            {
                numeroCuenta = this.CrearEstadoCuentaContabilidadFacturaNC(cuenta, estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);

                cuenta.NumeroResultadoDocumento = encabezadoFactura.NumeroFactura;
                cuenta.IdPlan = encabezadoFactura.IdPlan;
                cuenta.IdAtencion = 0;
                cuenta.IdCuenta = numeroCuenta;

                if (cuenta.MovimientosCartera.Count == 2)
                {
                    var movimiento = this.ObtenerIdMovimiento(cuenta, estadoCuenta.InformacionFactura.MovimientoAbono);
                    this.GuardarEstadoCuentaContabilidad(this.CrearContabilidad(cuenta, estadoCuenta, movimiento.DetalleMovimientoCartera, movimiento.IdMovimientoDocumento));
                }
            }
        }

        /// <summary>
        /// Crea objeto de movimiento de cartera para Desrradicacion.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <param name="cuenta">The cuenta.</param>
        /// <returns>Objeto Movimiento Cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private MovimientoCartera CrearDesrradicado(NotaCredito notaCredito, CuentaCartera cuenta)
        {
            var movimiento = new MovimientoCartera()
            {
                CodigoEntidad = notaCredito.CodigoEntidad,
                IdMovimientoDocumento = ClaseMovimiento.Abono.GetHashCode(),
                CodigoMovimientoSecuencial = "DESRA",
                NumeroMovimientoPrevio = string.Empty,
                FechaRegistro = notaCredito.FechaNota,
                FechaMovimiento = notaCredito.FechaNota,
                IndHabilitado = 1,
                CodigoUsuario = notaCredito.CodigoUsuario,
                IdSede = 1,
                DetalleMovimientoCartera = new DetalleMovimientoCartera()
                {
                    CodigoEntidad = cuenta.CodigoEntidad,
                    CodigoMovimiento = "DESRA",
                    NumeroDocumento = string.Empty,
                    ValorMonto = cuenta.ValorAbono,
                    ValorAfectado = cuenta.ValorSaldo,
                    InteresCuenta = 0,
                    DescuentoMovimiento = 0,
                    PorcentajeInteres = 0,
                    PorcentajeDescuento = 0,
                    DescuentoConcepto = 0,
                    InteresConcepto = 0,
                    SaldoInicial = 0,
                    SaldoFinal = 0,
                    IdTercero = cuenta.IdTercero,
                    CodigoEnlaceContable = string.Empty
                }
            };

            return movimiento;
        }

        /// <summary>
        /// Inserta los detalles de cuenta cartera que cumplen ciertos criterios.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 12/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearDetalleCuentaCartera(List<FacturaAtencion> atenciones, FacturaCompuesta facturaCompuesta)
        {
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;
            List<CuentaCartera> cuentas = new List<CuentaCartera>();

            cuentas = this.DetalleAtencionesCartera(atenciones, estadoCuenta);
        }

        /// <summary>
        /// Metodo de creaci n del objeto de contabilidad para la factura.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearEstadoCuentaContabilidadFactura(EstadoCuentaEncabezado estadoCuenta, int numeroFactura, FacturaCompuesta facturaCompuesta)
        {
            try
            {
                FacturaAtencionConceptoCobro concepto = (from item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro
                                                         where item.IdContrato == estadoCuenta.IdContrato && item.IdPlan == estadoCuenta.IdPlan
                                                         select item).FirstOrDefault();

                var cuentaFactura = this.CrearCuentaCarteraFactura(estadoCuenta, numeroFactura);

                if (concepto != null && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion && concepto.DepositoParticular)
                {
                    cuentaFactura.ValorSaldo = cuentaFactura.ValorMonto - concepto.ValorConcepto;

                    if (cuentaFactura.ValorSaldo > 0)
                    {
                        cuentaFactura.EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.R.ToString();
                        cuentaFactura.EstadoCuenta = CuentaCartera.EstadoRegistro.R.ToString();
                    }
                    else
                    {
                        cuentaFactura.EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.P.ToString();
                        cuentaFactura.EstadoCuenta = CuentaCartera.EstadoRegistro.P.ToString();
                    }
                }

                cuentaFactura.NumeroResultadoDocumento = numeroFactura;
                cuentaFactura.IdPlan = estadoCuenta.IdPlan;
                cuentaFactura.IdAtencion = estadoCuenta.IdAtencion;
                cuentaFactura.IdCuenta = this.GuardarInformacionCuentaCartera(cuentaFactura);
                this.ActualizarIdCuentaFactura(cuentaFactura.IdCuenta, numeroFactura);
                estadoCuenta.ConsecutivoCartera = cuentaFactura.IdCuenta;

                var contabilidad = new Contabilidad()
                {
                    CodigoEntidad = estadoCuenta.CodigoEntidad,
                    IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                    CodigoSecuencial = estadoCuenta.CodigoEntidad,
                    CodigoMovimiento = cuentaFactura.CodigoMovimiento,
                    NumeroFactura = numeroFactura,
                    FechaRegistro = estadoCuenta.FechaFactura,
                    ValorRegistro = estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? cuentaFactura.ValorAbono : cuentaFactura.ValorMonto,
                    FechaRegistroEstado = estadoCuenta.FechaFactura,
                    TipoRegistro = this.EstablecerTipoRegistroFactura(estadoCuenta.TipoFacturacion),
                    TipoMovimiento = string.Empty
                };

                if (estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion
                    && ((concepto != null && concepto.DepositoParticular) || concepto == null) && this.ValidarParticular(estadoCuenta))
                {
                    contabilidad.EstadoRegistro = contabilidad.EstadoRegistroPendiente;
                    this.GuardarEstadoCuentaContabilidad(contabilidad);

                    if (concepto == null)
                    {
                        cuentaFactura.TipoCuentaCartera = CuentaCartera.TipoCuenta.Radicacion;
                    }

                    var encabezadoFactura = facturaCompuesta.EncabezadoFactura;

                    cuentaFactura.MovimientosCartera = this.CrearMovimientosCartera(estadoCuenta, cuentaFactura);
                    this.CrearMovimientos(cuentaFactura, encabezadoFactura, estadoCuenta);
                    if (cuentaFactura.MovimientosCartera.Count == 2)
                    {
                        var movimiento = this.ObtenerIdMovimiento(cuentaFactura, estadoCuenta.InformacionFactura.MovimientoAbono);
                        this.GuardarEstadoCuentaContabilidad(this.CrearContabilidad(cuentaFactura, estadoCuenta, movimiento.DetalleMovimientoCartera, movimiento.IdMovimientoDocumento));
                    }
                }
                else
                {
                    contabilidad.EstadoRegistro = contabilidad.EstadoRegistroPendiente;
                    this.GuardarEstadoCuentaContabilidad(contabilidad);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Metodo de creaci n del objeto de contabilidad para la factura.
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Estado cuenta.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int CrearEstadoCuentaContabilidadFacturaNC(CuentaCartera cuenta, EstadoCuentaEncabezado estadoCuenta, int numeroFactura, FacturaCompuesta facturaCompuesta)
        {
            int numeroCuenta = 0;

            try
            {
                FacturaAtencionConceptoCobro concepto = (from item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro
                                                         where item.IdContrato == estadoCuenta.IdContrato && item.IdPlan == estadoCuenta.IdPlan
                                                         select item).FirstOrDefault();

                var cuentaFactura = this.CrearCuentaCarteraFacturaNC(cuenta, estadoCuenta, numeroFactura);

                if (concepto != null && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion && concepto.DepositoParticular)
                {
                    cuentaFactura.ValorSaldo = cuentaFactura.ValorMonto - concepto.ValorConcepto;

                    if (cuentaFactura.ValorSaldo > 0)
                    {
                        cuentaFactura.EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.R.ToString();
                        cuentaFactura.EstadoCuenta = CuentaCartera.EstadoRegistro.R.ToString();
                    }
                    else
                    {
                        cuentaFactura.EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.P.ToString();
                        cuentaFactura.EstadoCuenta = CuentaCartera.EstadoRegistro.P.ToString();
                    }
                }

                cuentaFactura.NumeroResultadoDocumento = numeroFactura;
                cuentaFactura.IdPlan = estadoCuenta.IdPlan;
                cuentaFactura.IdAtencion = estadoCuenta.IdAtencion;
                cuentaFactura.IdCuenta = this.GuardarInformacionCuentaCarteraNC(cuentaFactura);
                this.ActualizarIdCuentaFactura(cuentaFactura.IdCuenta, numeroFactura);
                estadoCuenta.ConsecutivoCartera = cuentaFactura.IdCuenta;

                var contabilidad = new Contabilidad()
                {
                    CodigoEntidad = estadoCuenta.CodigoEntidad,
                    IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                    CodigoSecuencial = estadoCuenta.CodigoEntidad,
                    CodigoMovimiento = cuentaFactura.CodigoMovimiento,
                    NumeroFactura = numeroFactura,
                    FechaRegistro = estadoCuenta.FechaFactura,
                    ValorRegistro = estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? cuentaFactura.ValorAbono : cuentaFactura.ValorMonto,
                    FechaRegistroEstado = estadoCuenta.FechaFactura,
                    TipoRegistro = this.EstablecerTipoRegistroFactura(estadoCuenta.TipoFacturacion),
                    TipoMovimiento = string.Empty
                };

                if (estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion
                    && ((concepto != null && concepto.DepositoParticular) || concepto == null) && this.ValidarParticular(estadoCuenta))
                {
                    contabilidad.EstadoRegistro = contabilidad.EstadoRegistroPendiente;
                    this.GuardarEstadoCuentaContabilidad(contabilidad);

                    if (concepto == null)
                    {
                        cuentaFactura.TipoCuentaCartera = CuentaCartera.TipoCuenta.Radicacion;
                    }

                    var encabezadoFactura = facturaCompuesta.EncabezadoFactura;

                    cuentaFactura.MovimientosCartera = this.CrearMovimientosCartera(estadoCuenta, cuentaFactura);

                    if (this.ValidarParticularNC(estadoCuenta))
                    {
                        this.CrearMovimientos(cuentaFactura, encabezadoFactura, estadoCuenta);
                    }

                    if (cuentaFactura.MovimientosCartera.Count == 2)
                    {
                        var movimiento = this.ObtenerIdMovimiento(cuentaFactura, estadoCuenta.InformacionFactura.MovimientoAbono);
                        this.GuardarEstadoCuentaContabilidad(this.CrearContabilidad(cuentaFactura, estadoCuenta, movimiento.DetalleMovimientoCartera, movimiento.IdMovimientoDocumento));
                    }
                }
                else
                {
                    contabilidad.EstadoRegistro = contabilidad.EstadoRegistroPendiente;
                    this.GuardarEstadoCuentaContabilidad(contabilidad);
                }

                numeroCuenta = cuentaFactura.IdCuenta;
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return numeroCuenta;
        }

        /// <summary>
        /// Metodo que inserta la informacion del pago de la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearFacturaPago(int numeroFactura, EstadoCuentaEncabezado estadoCuenta)
        {
            try
            {
                var facturasPagoDetalle = this.CrearFacturaPagoDetalle(numeroFactura, estadoCuenta);

                if (facturasPagoDetalle.Count > 0)
                {
                    var facturaPago = new FacturaPago()
                    {
                        CodigoConceptoPago = facturasPagoDetalle.ElementAt(0).CodigoConceptoPago,
                        CodigoEntidad = estadoCuenta.CodigoEntidad,
                        CodigoMovimiento = facturasPagoDetalle.ElementAt(0).CodigoMovimiento,
                        CodigoSeccion = estadoCuenta.CodigoSeccion,
                        EstadoPagoFactura = facturasPagoDetalle.ElementAt(0).EstadoPagoFactura,
                        FacturaPagoDetalle = facturasPagoDetalle,
                        IdTipoMovimiento = facturasPagoDetalle.ElementAt(0).IdTipoMovimiento,
                        NumeroFactura = numeroFactura,
                        ValorPagoFactura = facturasPagoDetalle.Sum(sp => sp.ValorPagoFactura),
                        ValorPagoFacturaCruzado = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                                 || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                                 && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? 0 : facturasPagoDetalle.Sum(sp => sp.ValorPagoFacturaCruzado)
                    };

                    this.GuardarFacturaPago(facturaPago);

                    foreach (var item in facturasPagoDetalle)
                    {
                        this.GuardarFacturaPagoDetalle(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Metodo que inserta la informacion del detalle del pago de la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Listado de Factura Pago Detalle.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaPagoDetalle> CrearFacturaPagoDetalle(int numeroFactura, EstadoCuentaEncabezado estadoCuenta)
        {
            var facturaPagoDetalle = from
                                         atencion in estadoCuenta.FacturaAtencion
                                     from
                                         detalle in atencion.Detalle
                                     where
                                         detalle.Exclusion == null
                                         && detalle.ExclusionManual == null
                                         && atencion.ConceptosCobro.Count > 0
                                         && atencion.Cruzar == true
                                         && estadoCuenta.TotalPagos > 0
                                     group atencion by
                                     new
                                     {
                                         IdAtencion = atencion.IdAtencion,
                                         ConceptosCobro = atencion.ConceptosCobro
                                     }

                                         into atenciones
                                         select new FacturaPagoDetalle()
                                         {
                                             CodigoEntidad = estadoCuenta.CodigoEntidad,
                                             CodigoSeccion = estadoCuenta.CodigoSeccion,
                                             NumeroFactura = numeroFactura,
                                             IdTipoMovimiento = estadoCuenta.InformacionFactura.IdTipoMovimiento,
                                             CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                                             CodigoConceptoPago = ((estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) && this.ValidarParticular(estadoCuenta)) ? "NA" : atenciones.Key.ConceptosCobro.ElementAt(0).CodigoConcepto,
                                             IdAtencion = atenciones.Key.IdAtencion,
                                             EstadoPagoFactura = -1,
                                             ValorPagoFactura = atenciones.Key.ConceptosCobro.ElementAt(0).ValorConcepto,
                                             ValorPagoFacturaCruzado = ((estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria == null
                                             || estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count == 0)
                                             && estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion) ? 0 : atenciones.Key.ConceptosCobro.Sum(sp => sp.ValorDiferencia)
                                         };

            return facturaPagoDetalle.ToList();
        }

        /// <summary>
        /// Metodo de validaciones de negocio de la anulaci n de factura.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearInformacionDetalleAnulacionFactura(NotaCredito notaCredito)
        {
            if (notaCredito.DetalleCausales != null)
            {
                foreach (var causal in notaCredito.DetalleCausales)
                {
                    causal.IdNumeroNotaCredito = notaCredito.IdNumeroNotaCredito;
                    causal.CodigoMovimiento = notaCredito.CodigoMovimiento;
                    this.GuardarDetalleAnulacionFactura(causal);
                }
            }

            this.ActualizarSaldosMovimientos(notaCredito);

            NotaCredito notaCreditoAjusteSaldo = notaCredito.CopiarObjeto();

            // Si existe movimiento 181 y NO existen más movimientos de resta al saldo.
            // se debe insertar movimiento 136 de ajuste al saldo.
            if (this.ValidarMovimientosRS(notaCreditoAjusteSaldo.NumeroFactura) > 0)
            {
                // Se realiza el envio del ID de la nota crédito 
                this.GuardarMovimientoCarteraAjusteSaldo(notaCreditoAjusteSaldo.NumeroFactura, notaCreditoAjusteSaldo.CodigoUsuario, notaCreditoAjusteSaldo.IdNumeroNotaCredito);
            }

            this.ActualizarEstadoCuentaCartera(notaCredito);
            this.GuardarMovimientoCarteraAnulacion(notaCredito);

            this.ActualizarEstadoFactura(notaCredito);

            // this.ActualizarConceptoContabilidad(notaCredito);
            this.ActualizarEstadoVentasAnulacion(notaCredito);

            List<int> listaVentas = this.ObtenerVentasPorFactura(notaCredito);

            foreach (int venta in listaVentas)
            {
                this.ActualizarEstadoVentaAnulacion(notaCredito, venta);
            }

            this.EliminarNoFacturables(notaCredito.NumeroFactura);
        }

        /// <summary>
        /// Metodo de validaciones de negocio de la anulación de factura - Facturación No Clínica
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 06/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearInformacionDetalleAnulacionFacturaNC(NotaCredito notaCredito)
        {
            if (notaCredito.DetalleCausales != null)
            {
                foreach (var causal in notaCredito.DetalleCausales)
                {
                    causal.IdNumeroNotaCredito = notaCredito.IdNumeroNotaCredito;
                    causal.CodigoMovimiento = notaCredito.CodigoMovimiento;
                    causal.IdTipoMovimiento = notaCredito.IdTipoMovimiento;
                    this.GuardarDetalleAnulacionFactura(causal);
                }
            }

            this.ProcesoComplementarioAnulacionNC(notaCredito);
            this.ActualizarEstadoVentaAnulacionNC(notaCredito);
        }

        /// <summary>
        /// Realiza las operaciones de guardado de detalle de cuentas de cartera y sus movimientos.
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearMovimientos(CuentaCartera cuenta, EncabezadoFactura encabezadoFactura, EstadoCuentaEncabezado estadoCuenta)
        {
            foreach (var movimiento in cuenta.MovimientosCartera)
            {
                int identificadorMovimiento = this.GuardarInformacionMovimientoCartera(movimiento);
                movimiento.DetalleMovimientoCartera.IdMovimiento = identificadorMovimiento;
                movimiento.DetalleMovimientoCartera.IdCuenta = cuenta.IdCuenta;
                movimiento.DetalleMovimientoCartera.IdTipoMovimiento = this.GuardarInformacionDetalleMovimientoCartera(movimiento.DetalleMovimientoCartera);
            }
        }

        /// <summary>
        /// Realiza las operaciones de guardado de detalle de cuentas de cartera y sus movimientos de la factura anulada..
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CrearMovimientosAnulacion(CuentaCartera cuenta)
        {
            foreach (var movimiento in cuenta.MovimientosCartera)
            {
                int identificadorMovimiento = this.GuardarInformacionMovimientoCartera(movimiento);
                movimiento.DetalleMovimientoCartera.IdMovimiento = identificadorMovimiento;
                movimiento.DetalleMovimientoCartera.IdCuenta = cuenta.IdCuenta;
                movimiento.DetalleMovimientoCartera.IdTipoMovimiento = this.GuardarInformacionDetalleMovimientoCartera(movimiento.DetalleMovimientoCartera);
            }
        }

        /// <summary>
        /// Retorna los movimientos de cartera a registrar.
        /// </summary>
        /// <param name="cuentaCartera">The cuenta cartera.</param>
        /// <returns>Listado de movimientos de cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<MovimientoCartera> CrearMovimientosCartera(CuentaCartera cuentaCartera)
        {
            var movimientosCartera = from
                                         item in cuentaCartera.MovimientosCartera
                                     select
                                        new MovimientoCartera()
                                        {
                                            CodigoEntidad = cuentaCartera.CodigoEntidad,
                                            IdTipoMovimiento = cuentaCartera.IdTipoMovimiento,
                                            IdMovimientoDocumento = 0,
                                            CodigoMovimientoSecuencial = cuentaCartera.CodigoMovimiento,
                                            CodigoSecuencia = cuentaCartera.CodigoEntidad,
                                            NumeroMovimientoPrevio = string.Empty,
                                            FechaRegistro = DateTime.Now,
                                            DescripcionMovimiento = string.Empty,
                                            FechaMovimiento = DateTime.Now,
                                            IndHabilitado = 1,
                                            CodigoUsuario = cuentaCartera.CodigoUsuario,
                                            IdSede = 1,
                                            DetalleMovimientoCartera = new DetalleMovimientoCartera()
                                            {
                                                CodigoEnlaceContable = string.Empty,
                                                CodigoEntidad = cuentaCartera.CodigoEntidad,
                                                DescuentoConcepto = 0,
                                                DescuentoMovimiento = 0,
                                                EstadoFinal = string.Empty,
                                                EstadoInicial = string.Empty,
                                                IdTercero = cuentaCartera.IdTercero,
                                                IdTipoMovimiento = cuentaCartera.IdTipoMovimiento,
                                                InteresConcepto = 0,
                                                InteresCuenta = 0,
                                                NumeroDocumento = string.Empty,
                                                PorcentajeDescuento = 0,
                                                PorcentajeInteres = 0,
                                                SaldoFinal = cuentaCartera.ValorSaldo,
                                                SaldoInicial = cuentaCartera.ValorSaldo,
                                                ValorAfectado = 0,
                                                ValorMonto = 0
                                            },
                                        };

            return movimientosCartera.ToList();
        }

        /// <summary>
        /// Crea los objetos correspondientes al tema de movimientos de cartera y detalle.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="cuenta">The cuenta.</param>
        /// <returns>Lista de movimientos de cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<MovimientoCartera> CrearMovimientosCartera(EstadoCuentaEncabezado encabezado, CuentaCartera cuenta)
        {
            List<MovimientoCartera> movimientosCartera = new List<MovimientoCartera>();
            if (cuenta.TipoCuentaCartera == CuentaCartera.TipoCuenta.Radicacion)
            {
                movimientosCartera.Add(this.CrearRadicacion(encabezado, cuenta));
            }
            else
            {
                movimientosCartera.Add(this.CrearRadicacion(encabezado, cuenta));

                if (encabezado.TipoFacturacion == TipoFacturacion.FacturacionRelacion
                    || encabezado.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count > 0)
                {
                    movimientosCartera.Add(this.CrearAbono(encabezado, cuenta));
                }
            }

            return movimientosCartera;
        }

        /// <summary>
        /// Crear proceso factura.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Objeto de proceso factura.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private ProcesoFactura CrearProceso(TipoProductoCompuesto producto)
        {
            List<ProcesoFacturaDetalle> detalles = new List<ProcesoFacturaDetalle>();

            detalles.Add(
                new ProcesoFacturaDetalle()
                {
                    IdAtencion = producto.Atencion.IdAtencion,
                    IdContrato = producto.VinculacionActiva.Contrato.Id,
                    IdPlan = producto.VinculacionActiva.Plan.Id,
                    NoFacturable = new NoFacturable()
                    {
                        IdAtencion = producto.Atencion.IdAtencion,
                        ApellidoCliente = producto.Atencion.ApellidoCliente,
                        NombreCliente = producto.Atencion.NombreCliente,
                        NumeroDocumento = producto.Atencion.NumeroDocumento,
                        IdProducto = producto.Producto.IdProducto,
                        CodigoProducto = producto.Producto.CodigoProducto,
                        NombreProducto = producto.Producto.Nombre,
                        IdVenta = producto.IdTransaccion,
                        NumeroVenta = producto.ProductosVenta.Count > 0 ? producto.ProductosVenta.FirstOrDefault().NumeroVenta : 0
                    }
                });

            var proceso = new ProcesoFactura()
            {
                CodigoEntidad = producto.CodigoEntidad,
                IdContrato = producto.CondicionContrato.IdContrato,
                IdTercero = producto.IdTercero,
                IdTipoAtencion = producto.Atencion.IdAtencionTipo,
                IndHabilitado = 1,
                TipoRelacion = producto.TipoRelacion,
                Detalles = detalles
            };

            return proceso;
        }

        /// <summary>
        /// Crea un objeto de movimiento para el abono.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="cuenta">The cuenta.</param>
        /// <returns>Objeto Movimiento Cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private MovimientoCartera CrearRadicacion(EstadoCuentaEncabezado encabezado, CuentaCartera cuenta)
        {
            var movimiento = new MovimientoCartera()
            {
                CodigoEntidad = encabezado.CodigoEntidad,
                IdMovimientoDocumento = ClaseMovimiento.Radicado.GetHashCode(),
                CodigoMovimientoSecuencial = encabezado.InformacionFactura.MovimientoRadicado,
                NumeroMovimientoPrevio = string.Empty,
                FechaRegistro = encabezado.FechaFactura,
                FechaMovimiento = encabezado.FechaFactura,
                IndHabilitado = 1,
                CodigoUsuario = encabezado.Usuario,
                IdSede = 1,
                DetalleMovimientoCartera = new DetalleMovimientoCartera()
                {
                    CodigoEnlaceContable = string.Empty,
                    CodigoEntidad = cuenta.CodigoEntidad,
                    CodigoMovimiento = encabezado.InformacionFactura.MovimientoRadicado,
                    DescuentoConcepto = 0,
                    DescuentoMovimiento = 0,
                    IdTercero = cuenta.IdTercero,
                    InteresConcepto = 0,
                    InteresCuenta = 0,
                    NumeroDocumento = string.Empty,
                    PorcentajeDescuento = 0,
                    PorcentajeInteres = 0,
                    SaldoFinal = 0,
                    SaldoInicial = 0,
                    ValorAfectado = 0,
                    ValorMonto = 0
                }
            };

            return movimiento;
        }

        /// <summary>
        /// Creacion de Ventas Facturadas.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Listado de VEntas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<VentaFactura> CrearVentaFactura(EstadoCuentaEncabezado estadoCuenta)
        {
            var ventas = from
                             atencion in estadoCuenta.FacturaAtencion
                         from
                             detalle in atencion.Detalle
                         where
                             detalle.Exclusion == null
                             && detalle.ExclusionManual == null
                         select new VentaFactura()
                         {
                             Cantidad = detalle.CantidadFacturar,
                             CodigoEntidad = estadoCuenta.CodigoEntidad,
                             IdContrato = estadoCuenta.IdContrato,
                             IdLote = estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? 0 : detalle.IdLote,
                             IdPlan = detalle.IdPlan,
                             IdProducto = detalle.IdProducto,
                             IdTransaccion = detalle.IdTransaccion,
                             NumeroVenta = detalle.NumeroVenta,
                             ValorVenta = detalle.ValorTotal
                         };

            return ventas.ToList();
        }

        /// <summary>
        /// Creacion de Ventas Facturadas.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Listado de VEntas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<VentaFactura> CrearVentaFacturaPaquete(EstadoCuentaEncabezado estadoCuenta)
        {
            var ventas = from
                             atencion in estadoCuenta.FacturaAtencion
                         from
                             detalle in atencion.Detalle
                         where
                             detalle.Exclusion == null
                             && detalle.ExclusionManual == null
                             && detalle.esPaquete == false
                         select new VentaFactura()
                         {
                             Cantidad = detalle.CantidadFacturar,
                             CodigoEntidad = estadoCuenta.CodigoEntidad,
                             IdContrato = estadoCuenta.IdContrato,
                             IdLote = estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion ? 0 : detalle.IdLote,
                             IdPlan = detalle.IdPlan,
                             IdProducto = detalle.IdProducto,
                             IdTransaccion = detalle.IdTransaccion,
                             NumeroVenta = detalle.NumeroVenta,
                             ValorVenta = detalle.ValorTotal
                         };

            return ventas.ToList();
        }

        /// <summary>
        /// Metodo para cruzar los conceptos de cobro.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void CruzarConceptosCobro(FacturaAtencion atencion, EstadoCuentaEncabezado estadoCuenta)
        {
            if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionRelacion)
            {
                this.AplicarReglaConceptoCobro(atencion);
            }

            if (atencion.MovimientosTesoreria != null && atencion.MovimientosTesoreria.Count > 0)
            {
                foreach (var movimiento in atencion.MovimientosTesoreria.OrderBy(c => c.IdMovimiento))
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
                            concepto.ValorCruzado = concepto.ValorSaldo;
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
                    concepto.ValorSaldo = concepto.ValorCruzado;
                }
            }
        }

        /// <summary>
        /// Realiza el proceso de cuenta detallado 
        /// </summary>
        /// <param name="facturaCompuesta">Factura compuesta.</param>
        /// <returns>Cuenta detallado.</returns>
        private bool CuentaDetallado(FacturaCompuesta facturaCompuesta)
        {
            bool resultado = true;
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;

            // Realiza la iteración de la factura 
            try
            {
                foreach (var item in facturaCompuesta.EstadoCuentaEncabezado.EstadoCuentaDetallado)
                {
                    item.CodigoEntidad = encabezadoFactura.CodigoEntidad;
                    item.CodigoSeccion = encabezadoFactura.CodigoSeccion;
                    item.CodigoConcepto = String.Empty;
                    item.CodigoUnidad = String.Empty;
                    item.CodigoTipoRelacion = String.Empty;
                    item.MetodoConfiguracion = String.Empty;
                    item.CantidadFacturaDetalle = item.Cantidad;
                    item.IdTransaccion = encabezadoFactura.VentasFactura.Where(c => c.NumeroVenta == item.NumeroVenta).FirstOrDefault().IdTransaccion;
                    item.NumeroFactura = encabezadoFactura.NumeroFactura;
                    item.IdTipoMovimiento = encabezadoFactura.IdTipoMovimiento;
                    item.CodigoMovimiento = facturaCompuesta.EstadoCuentaEncabezado.InformacionFactura.CodigoMovimiento;
                    item.IdTercero = encabezadoFactura.IdTercero;
                    item.IdManual = facturaCompuesta.EstadoCuentaEncabezado.FacturaAtencion.FirstOrDefault<FacturaAtencion>().IdManual;
                    item.VigenciaTarifa = facturaCompuesta.EstadoCuentaEncabezado.FacturaAtencion.FirstOrDefault<FacturaAtencion>().VigenciaManual;
                    this.GuardarInformacionDetalleFactura(item);
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Retorna las cuentas de cartera que se van a insertar en movimientos cartera.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Lista de cuentas de cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CuentaCartera> DetalleAtencionesCartera(List<FacturaAtencion> atenciones, EstadoCuentaEncabezado estadoCuenta)
        {
            var atencionesDetalleConMovimientos = (from
                                                       item in atenciones
                                                   from
                                                       concepto in item.ConceptosCobro
                                                   where
                                                       item.Cruzar == true
                                                       && item.ConceptosCobro.Count > 0
                                                       && concepto.ValorSaldo > 0
                                                   select new CuentaCartera()
                                                   {
                                                       CodigoEntidad = estadoCuenta.CodigoEntidad,
                                                       CodigoMovimiento = estadoCuenta.InformacionFactura.CodigoMovimiento,
                                                       CodigoUsuario = estadoCuenta.Usuario,
                                                       CuentaInicio = 1,
                                                       DireccionCuenta = estadoCuenta.Direccion,
                                                       DocumentoPrefijo = estadoCuenta.InformacionFactura.PrefijoFactura,
                                                       EstadoAnteriorCuenta = CuentaCartera.EstadoRegistro.P.ToString(),
                                                       EstadoCuenta = CuentaCartera.EstadoRegistro.P.ToString(),
                                                       FechaRadicacion = DateTime.Now,
                                                       FechaRegistroCuenta = DateTime.Now,
                                                       HoraRegistroCuenta = DateTime.Now,
                                                       IdAtencion = item.IdAtencion,
                                                       IdCliente = item.IdCliente,
                                                       IdClienteResponsable = item.IdCliente,
                                                       IdConcepto = estadoCuenta.InformacionFactura.ConceptoFacturaGenerada,
                                                       IdContrato = item.IdContrato,
                                                       IdPlan = estadoCuenta.IdPlan,
                                                       IdSede = 1,
                                                       IdTercero = item.IdTercero,
                                                       IdTerceroResponsable = 0,
                                                       IdTipoCartera = 0,
                                                       IdTipoMovimiento = estadoCuenta.IdTipoMovimiento,
                                                       IdTipoRegimen = estadoCuenta.InformacionFactura.IdPlanAtencion,
                                                       IndDetalleFactura = 1,
                                                       IndHabilitado = 1,
                                                       IndRadicacion = 1,
                                                       IndRelacionRadicado = 1,
                                                       NumeroResultadoDocumento = 0,
                                                       Observaciones = estadoCuenta.Observaciones,
                                                       ObservacionMuestra = estadoCuenta.Observaciones,
                                                       ValorMonto = estadoCuenta.ValorSaldo,
                                                       ValorMontoMinimo = 0,
                                                       ValorSaldo = 0
                                                   }).ToList();

            return atencionesDetalleConMovimientos;
        }

        /// <summary>
        /// Establece el campo de tipo de movimiento de contabilidad seg n tipo de facturaci n.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="claseMovimiento">The clase movimiento.</param>
        /// <returns>
        /// Tipo de movimiento.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 12/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private string EstablecerTipoMovimiento(TipoFacturacion tipoFacturacion, int claseMovimiento)
        {
            string tipoMovimiento = string.Empty;

            switch (tipoFacturacion)
            {
                case TipoFacturacion.FacturacionRelacion:
                    tipoMovimiento = this.ObtenerMovimientoDocumento(claseMovimiento);
                    break;

                case TipoFacturacion.FacturacionActividad:
                    tipoMovimiento = this.ObtenerMovimientoDocumento(claseMovimiento);
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    tipoMovimiento = this.ObtenerMovimientoDocumento(claseMovimiento);
                    break;

                case TipoFacturacion.FacturacionNoClinico:
                    tipoMovimiento = this.ObtenerMovimientoDocumento(claseMovimiento);
                    break;
            }

            return tipoMovimiento;
        }

        /// <summary>
        /// Obtiene el tipo de registro que debe llevar el estado de cuenta al guardarlo.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>Tipo de Registro.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private string EstablecerTipoRegistroFactura(TipoFacturacion tipoFacturacion)
        {
            string tipoRegistro = string.Empty;

            switch (tipoFacturacion)
            {
                case TipoFacturacion.FacturacionRelacion:
                    tipoRegistro = Constantes.TipoFacturacion_Relacion;
                    break;

                case TipoFacturacion.FacturacionActividad:
                    tipoRegistro = Constantes.TipoFacturacion_Actividades;
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    tipoRegistro = Constantes.TipoFacturacion_Paquetes;
                    break;

                case TipoFacturacion.FacturacionNoClinico:
                    tipoRegistro = Constantes.TipoFacturacion_NoClinicas;
                    break;
            }

            return tipoRegistro;
        }

        /// <summary>
        /// Realiza el proceso Estado cuenta.
        /// </summary>
        /// <param name="facturaCompuesta">Factura compuesta.</param>
        /// <returns>True si operación exitosa.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 28/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private bool EstadoCuenta(FacturaCompuesta facturaCompuesta)
        {
            bool retorno = true;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;

            try
            {
                foreach (var item in estadoCuenta.FacturaAtencion)
                {
                    foreach (var detalle in item.Detalle)
                    {
                        if (detalle.Exclusion != null)
                        {
                            var itemNoFacturable = new NoFacturable()
                            {
                                IdAtencion = item.IdAtencion,
                                ApellidoCliente = string.Empty,
                                NombreCliente = string.Empty,
                                NumeroDocumento = string.Empty,
                                IdExclusion = detalle.Exclusion.Id,
                                IdProcesoDetalle = detalle.IdProcesoDetalle,
                                IdProducto = detalle.IdProducto,
                                CodigoProducto = detalle.CodigoProducto,
                                NombreProducto = detalle.NombreProducto,
                                IdVenta = detalle.IdTransaccion,
                                NumeroVenta = detalle.NumeroVenta
                            };
                            this.GuardarInformacionNoFacturable(itemNoFacturable);
                        }
                    }
                }
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Evalua la cantidad de productos del paquete.
        /// </summary>
        /// <param name="cantidadProductos">The cantidad productos.</param>
        /// <returns>Indica si tiene o no productos dentros del paquete.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool EvaluarCantidadProducto(int cantidadProductos)
        {
            if (cantidadProductos > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Evaluar condiciones de contrato para obtener los componentes.
        /// </summary>
        /// <param name="condicion">The condicion.</param>
        /// <param name="componentes">The componentes.</param>
        /// <returns>
        /// Lista de componentes.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private List<Componente> EvaluarCondicionContrato(CondicionContrato condicion, List<Componente> componentes)
        {
            var resultado = from
                                item in componentes
                            select
                                item;

            return resultado.Count() > 0 ? resultado.ToList() : new List<Componente>();
        }

        /// <summary>
        /// Metodo para evaluar las condiciones de Manuales del contrato.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Condicion Contrato.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CondicionContrato EvaluarCondicionContratoManuales(CondicionContrato condicionContrato, CondicionTarifa condicionTarifa)
        {
            CondicionContrato resultado = condicionContrato;

            if (condicionTarifa != null)
            {
                resultado = new CondicionContrato()
                {
                    DetalleTarifa = condicionContrato.DetalleTarifa,
                    DigitoRedondeo = condicionContrato.DigitoRedondeo,
                    FechaVigencia = condicionTarifa.VigenciaTarifa,
                    FechaVigenciaAlterna = condicionTarifa.VigenciaTarifaAlterna,
                    IdContrato = condicionContrato.IdContrato,
                    IdManual = condicionTarifa.IdManual,
                    IdManualAlterno = condicionTarifa.IdManualAlterno,
                    IdManualInstitucional = condicionContrato.IdManualInstitucional,
                    NombreAproximacion = condicionContrato.NombreAproximacion,
                    Porcentaje = condicionContrato.Porcentaje,
                    PorcentajeAlterno = condicionContrato.PorcentajeAlterno
                };
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene para evaluar la relaci n entre las exclusiones del manual con las ventas asociadas.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Ventas asociadas de la exclusi n del manual.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<VentaProductoRelacion> EvaluarExclusionManualVentaAsociada(BaseValidacion detalle)
        {
            var paginacion = this.CrearConsultaProductosRelacionados(detalle.IdAtencion, 0);
            var productosAsociados = this.ConsultarVentaProductosRelacion(paginacion);

            var ventaProductosAsociados = from
                                              producto in productosAsociados.Item
                                          where
                                              producto.IdAtencion == detalle.IdAtencion
                                              && producto.NumeroVentaRelacion == detalle.NumeroVenta
                                          select
                                              producto;

            return ventaProductosAsociados.ToList();
        }

        /// <summary>
        /// Evalua nivel de complejidad de la exclusi n del manual.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica si aplica nivel de complejidad en el manual.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool EvaluarNivelComplejidad(BaseValidacion detalle)
        {
            bool resultado = false;

            var ventasAsociadas = this.EvaluarExclusionManualVentaAsociada(detalle).FirstOrDefault();

            if (ventasAsociadas != null)
            {
                if (ventasAsociadas.NivelComplejidad >= detalle.ExclusionManual.IdNivelInicial && ventasAsociadas.NivelComplejidad <= detalle.ExclusionManual.IdNivelFinal)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            else
            {
                if (detalle.ExclusionManual.IdNivelInicial > 0 && detalle.ExclusionManual.IdNivelFinal > 0)
                {
                    resultado = false;
                }
                else
                {
                    if ((detalle.ExclusionManual.IdGrupoProductoAlterno == detalle.IdGrupoProducto) && (detalle.ExclusionManual.IdProductoAlterno == detalle.IdProducto))
                    {
                        resultado = true;
                    }
                    else if (detalle.ExclusionManual.IdGrupoProductoAlterno == detalle.IdGrupoProducto)
                    {
                        resultado = true;
                    }
                    else if ((detalle.ExclusionManual.IdGrupoProducto == detalle.IdGrupoProducto) && (detalle.ExclusionManual.IdProducto == detalle.IdProducto))
                    {
                        resultado = true;
                    }
                    else if (detalle.ExclusionManual.IdGrupoProducto == detalle.IdGrupoProducto)
                    {
                        resultado = true;
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para determinar Costos asociados o Niveles de Complejidad.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="ventaComponente">The venta componente.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void EvaluarNivelesComplejidadCostosAsociados(FacturaAtencion atencion, CondicionContrato condicionContrato, VentaComponente ventaComponente, List<CondicionProceso> condiciones, FacturaAtencionDetalle detalle)
        {
            switch (ventaComponente.TipoProductos)
            {
                case TipoProd.Quirurjicos:

                    bool resultado = this.AplicarReglaNivelesComplejidad(condiciones, atencion, ventaComponente, condicionContrato.IdManual, detalle);

                    if (!resultado)
                    {
                        resultado = this.AplicarReglaNivelesComplejidad(condiciones, atencion, ventaComponente, condicionContrato.IdManualAlterno, detalle);
                    }

                    if (!resultado)
                    {
                        resultado = this.AplicarReglaNivelesComplejidad(condiciones, atencion, ventaComponente, condicionContrato.IdManualInstitucional, detalle);
                    }

                    break;

                case TipoProd.NoQuirurjico:
                    this.AplicarReglaCostosAsociados(condiciones, atencion, ventaComponente, condicionContrato);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Metodo para validar condiciones de tarifa adicionadas al objeto.
        /// </summary>
        /// <param name="identificadorElemento">The id elemento.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <returns>Indica si existe la condicion de tafifa en el listado.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ExisteCondicionTarifa(int identificadorElemento, List<CondicionTarifa> condiciones)
        {
            var resultado = from
                                item in condiciones
                            where
                                item.IdElemento == identificadorElemento
                            select
                                item;

            return resultado.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Metodo para filtrar los conceptos de cobro a actualizar.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>Conceptos de Cobro a Actualizar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaAtencionConceptoCobro> FiltrarConceptoCobroActualizar(List<FacturaAtencion> atenciones)
        {
            var conceptos = from
                                atencion in atenciones
                            where
                                atencion.ConceptosCobro.Count > 0
                                && atencion.Cruzar
                            from
                                concepto in atencion.ConceptosCobro
                            where
                                concepto.Actualizar
                            select
                                concepto;

            return conceptos.ToList();
        }

        /// <summary>
        /// Filtrar Condicion Cubrimiento.
        /// </summary>
        /// <param name="list">Parametro list.</param>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Filtrar Condicion Cubrimiento.
        /// </remarks>
        private Cubrimiento FiltrarCondicionCubrimiento(List<Cubrimiento> list, Cubrimiento cubrimiento)
        {
            var cubrimientos = from item in list
                               where item.IdCubrimiento == cubrimiento.IdCubrimiento
                               && item.CondicionesCubrimiento.Id == cubrimiento.CondicionesCubrimiento.Id
                               select item;

            if (cubrimientos.Count() == 0)
            {
                list.Add(cubrimiento);
            }

            return cubrimientos.Count() > 0 ? cubrimientos.FirstOrDefault() : cubrimiento;
        }

        /// <summary>
        /// Obtiene las Condiciones de cubrimiento Filtradas por fecha de venta.
        /// </summary>
        /// <param name="condicionesCubrimiento">The condiciones cubrimiento.</param>
        /// <param name="fechaVenta">The fecha venta.</param>
        /// <param name="identificadorClaseCubrimiento">The id clase cubrimiento.</param>
        /// <returns>
        /// Lsita de Condiciones de cubrimiento.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CondicionCubrimiento> FiltrarCondicionesCubrimiento(List<CondicionCubrimiento> condicionesCubrimiento, DateTime fechaVenta, int identificadorClaseCubrimiento)
        {
            var condiciones = from
                                  item in condicionesCubrimiento
                              where
                                  fechaVenta.Date >= item.VigenciaCondicion.Date
                                  && item.Cubrimiento.IdClaseCubrimiento == identificadorClaseCubrimiento
                              select
                                  item;

            return condiciones.ToList();
        }

        /// <summary>
        /// Obtiene las Condiciones de Tarifa Filtradas por fecha de venta.
        /// </summary>
        /// <param name="condicionesTarifa">The condiciones tarifa.</param>
        /// <param name="fechaVenta">The fecha venta.</param>
        /// <returns>
        /// Lista de Condiciones de Tarifa Filtradas.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CondicionTarifa> FiltrarCondicionesTarifa(List<CondicionTarifa> condicionesTarifa, DateTime fechaVenta)
        {
            var condiciones = from
                                  item in condicionesTarifa
                              where
                                  fechaVenta.Date >= item.VigenciaCondicion.Date
                              select
                                  item;

            return condiciones.ToList();
        }

        /// <summary>
        /// Filtrar Condicion Tarifa.
        /// </summary>
        /// <param name="list">Parametro list.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Condicion Tarifa.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias. 
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Filtrar Condicion Tarifa.
        /// </remarks>
        private CondicionTarifa FiltrarCondicionTarifa(List<CondicionTarifa> list, CondicionTarifa condicionTarifa)
        {
            var resultado = from item in list
                            where item.Id == condicionTarifa.Id
                            select item;

            if (resultado.Count() == 0)
            {
                list.Add(condicionTarifa);
            }

            return resultado.Count() > 0 ? resultado.FirstOrDefault() : condicionTarifa;
        }

        /// <summary>
        /// Obtiene los datos de los costos asociados.
        /// </summary>
        /// <param name="costosAsociados">The costos asociados.</param>
        /// <param name="fechaVenta">The fechaVenta.</param>
        /// <param name="componente">The componente.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <returns>
        /// Lista de costos asociados.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 06/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<CostoAsociado> FiltrarCostosAsociados(List<CostoAsociado> costosAsociados, DateTime fechaVenta, string componente, CondicionContrato condicionContrato)
        {
            var condiciones = from
                                  item in costosAsociados
                              where
                                  (fechaVenta.Date >= item.VigenciaManual.Date && fechaVenta.Date <= item.FechaFinal.Date)
                                  && item.IndHabilitado == 1
                              select
                                  item;

            return condiciones.ToList();
        }

        /// <summary>
        /// Metodo para filtrar los descuentos.
        /// </summary>
        /// <param name="descuentos">The descuentos.</param>
        /// <param name="fechaVenta">The fecha venta.</param>
        /// <returns>Listado de Descuentos Filtrados.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Descuento> FiltrarDescuentos(List<Descuento> descuentos, DateTime fechaVenta)
        {
            var descuento = from
                                item in descuentos
                            where
                                (fechaVenta.Date >= item.FechaInicial.Date && fechaVenta.Date <= item.FechaFinal.Date)
                                || (fechaVenta >= item.FechaInicial && item.FechaInicial.Date > item.FechaFinal.Date)
                            select
                                item;

            return descuento.ToList();
        }

        /// <summary>
        /// Filtra la Venta Asociada a la atencion x Producto.
        /// </summary>
        /// <param name="detallesVenta">The detalles venta.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Detalle de la VEnta Realizada.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private VentaDetalle FiltrarDetalleVenta(List<VentaDetalle> detallesVenta, FacturaAtencionDetalle detalle)
        {
            var movimientos = from
                                  item in detallesVenta
                              where
                                  item.IdAtencion == detalle.IdAtencion
                                  && item.IdProducto == detalle.IdProducto
                                  && item.IdTransaccion == detalle.IdTransaccion
                                  && item.NumeroVenta == detalle.NumeroVenta
                              select
                                  item;

            return movimientos.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para filtrar honorarios responsables.
        /// </summary>
        /// <param name="honorarios">The honorarios.</param>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Resultado de Honorarios.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private IEnumerable<Honorario> FiltrarHonorariosResponsables(IEnumerable<Honorario> honorarios, Honorario honorario)
        {
            var resultado = from
                                item in honorarios
                            where
                                (honorario.IdTipoProducto == 0 || item.IdTipoProducto == honorario.IdTipoProducto)
                                && (honorario.IdGrupoProducto == 0 || item.IdGrupoProducto == honorario.IdGrupoProducto)
                                && (honorario.IdProducto == 0 || item.IdProducto == honorario.IdProducto)
                                && (honorario.IdServicio == 0 || item.IdServicio == honorario.IdServicio)
                                && (honorario.IdTipoAtencion == 0 || item.IdTipoAtencion == honorario.IdTipoAtencion)
                                && (honorario.Componente == null || item.Componente.Equals(honorario.Componente))
                                && ((honorario.FechaInicial == DateTime.MinValue || honorario.FechaFinal == DateTime.MinValue)
                                || (honorario.FechaInicial >= item.FechaInicial && honorario.FechaFinal <= item.FechaFinal))
                            select
                                item;

            return resultado;
        }

        /// <summary>
        /// Metodo para filtrar los movimientos a actualizar.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>Lista de Movimientos a Actualizar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaAtencionMovimiento> FiltrarMovimientoTesoreriaActualizar(List<FacturaAtencion> atenciones)
        {
            var movimientosTesoreria = from
                                           atencion in atenciones
                                       where
                                           atencion.Cruzar
                                           && atencion.ConceptosCobro.Count > 0
                                           && atencion.MovimientosTesoreria.Count > 0
                                       from
                                           movimiento in atencion.MovimientosTesoreria
                                       where
                                           movimiento.Actualizar
                                       select
                                           movimiento;

            return movimientosTesoreria.ToList();
        }

        /// <summary>
        /// Filtra los niveles de Complejidad.
        /// </summary>
        /// <param name="niveles">The niveles.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista de Filtros de Niveles de Complejidad.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<NivelComplejidad> FiltrarNivelesComplejidad(List<NivelComplejidad> niveles, BaseValidacion detalle)
        {
            var nivelesComplejidad = from
                                         item in niveles
                                     where
                                         detalle.FechaVenta == item.VigenciaComponente
                                     select
                                         item;

            return nivelesComplejidad.ToList();
        }

        /// <summary>
        /// Filtra los recargos del Contrato.
        /// </summary>
        /// <param name="recargos">The recargos.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Lista de Recargos.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Recargo> FiltrarRecargoContrato(List<Recargo> recargos, BaseValidacion detalle)
        {
            var recargoContrato = from
                                      item in recargos
                                  where
                                        (detalle.HoraVenta.Tiempo() >= item.HoraInicial.Tiempo()
                                             &&
                                             ((item.HoraFinal.Tiempo() > (new DateTime(0001, 1, 1, 0, 0, 0)).Tiempo()
                                                    && item.HoraFinal.Tiempo() <= (new DateTime(0001, 1, 1, 6, 00, 0)).Tiempo()
                                                    && (
                                                        detalle.HoraVenta.Tiempo() <= new DateTime(0001, 1, 1, 23, 59, 59).Tiempo()
                                                        || detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()))
                                            ||
                                                detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo())
                                        ||
                                        (detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()
                                            && (
                                            (item.HoraFinal.Tiempo() > (new DateTime(0001, 1, 1, 0, 0, 0)).Tiempo()
                                                && item.HoraFinal.Tiempo() <= (new DateTime(0001, 1, 1, 6, 00, 0)).Tiempo()
                                                && (
                                                    detalle.HoraVenta.Tiempo() <= new DateTime(0001, 1, 1, 23, 59, 59).Tiempo()
                                                    || detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()))
                                          ||
                                            detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo())))
                                        && detalle.Festivo == item.Festivo
                                  select
                                      item;

            return recargoContrato.ToList();
        }

        /// <summary>
        /// Filtra los Recargos por Manual.
        /// </summary>
        /// <param name="recargos">The recargos.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista los Recargos Aplicados.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<RecargoManual> FiltrarRecargoManual(List<RecargoManual> recargos, BaseValidacion detalle)
        {
            var recargoManual = from
                                     item in recargos
                                where
                                     (detalle.HoraVenta.Tiempo() >= item.HoraInicial.Tiempo()
                                      && ((item.HoraFinal.Tiempo() > (new DateTime(0001, 1, 1, 0, 0, 0)).Tiempo()
                                      && item.HoraFinal.Tiempo() <= (new DateTime(0001, 1, 1, 6, 00, 0)).Tiempo()
                                      && (
                                          detalle.HoraVenta.Tiempo() <= new DateTime(0001, 1, 1, 23, 59, 59).Tiempo()
                                          || detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()))
                                      ||
                                          detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo())
                                      ||
                                      (detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()
                                          && ((item.HoraFinal.Tiempo() > (new DateTime(0001, 1, 1, 0, 0, 0)).Tiempo()
                                          && item.HoraFinal.Tiempo() <= (new DateTime(0001, 1, 1, 6, 00, 0)).Tiempo()
                                          && (
                                              detalle.HoraVenta.Tiempo() <= new DateTime(0001, 1, 1, 23, 59, 59).Tiempo()
                                              || detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo()))
                                          ||
                                              detalle.HoraVenta.Tiempo() <= item.HoraFinal.Tiempo())))
                                      && detalle.Festivo == item.Festivo
                                select
                                    item;

            return recargoManual.ToList();
        }

        /// <summary>
        /// Metodo que genera el log de paquetes aplicados.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="facturaPaquetes">The factura paquetes.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void GenerarLogPaquetes(int identificadorProceso, List<FacturaPaquete> facturaPaquetes)
        {
            string strEncabezado = "Id Atencion:{0} Id Paquete:{1} Nombre Paquete:{2} Valor Paquete:{3} IdManual: {4} Vigencia:{5}";
            foreach (var registroPaquete in facturaPaquetes)
            {
                var encabezadoPaquete = this.ConsultarValorPaquetes(new FacturaPaquete()
                {
                    IdManual = registroPaquete.IdManual,
                    VigenciaTarifa = registroPaquete.VigenciaTarifa
                }).Where(c => c.IdProducto == registroPaquete.IdProducto).FirstOrDefault();

                if (encabezadoPaquete != null)
                {
                    this.Archivo(
                        identificadorProceso,
                        "Paquetes",
                        string.Format(strEncabezado, registroPaquete.IdAtencion, registroPaquete.PaqueteEncabezado.IdPaquete, registroPaquete.PaqueteEncabezado.NombrePaquete, encabezadoPaquete.ValorPaquete, encabezadoPaquete.IdManual, encabezadoPaquete.VigenciaTarifa));
                }
            }
        }

        /// <summary>
        /// Guardar Detalle Factura PyG.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void GuardarDetalleFacturaPyG(Paquete paquete, int numeroFactura, int numeroVenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarDetalleFacturaPyG(paquete, numeroFactura, numeroVenta);
        }

        /// <summary>
        /// Guardar Detalle Factura PyG Componentes.
        /// </summary>
        /// <param name="ventaComponente">The venta componente.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <remarks>
        /// Autor: Sin Información. 
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void GuardarDetalleFacturaPyGComponentes(VentaComponente ventaComponente, int numeroFactura)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarDetalleFacturaPyGComponentes(ventaComponente, numeroFactura);
        }

        /// <summary>
        /// Métod para guardar los productos del paquete armado.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="detallePaquete">The detalle paquete.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="detallePaqueteCompleto">The detalle paquete completo.</param>
        /// <returns>
        /// Cantidad registros.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private int GuardarDetallePaqueteFactura(int identificadorAtencion, string numeroFactura, PaqueteProducto detallePaquete, EstadoCuentaEncabezado estadoCuenta, Paquete detallePaqueteCompleto)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarDetallePaqueteFactura(identificadorAtencion, numeroFactura, detallePaquete, estadoCuenta, detallePaqueteCompleto);
        }

        /// <summary>
        /// Metodo que verifica los componentes y los registra.
        /// </summary>
        /// <param name="item">Parametro item.</param>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 14/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void GuardarFactoresComponentes(EstadoCuentaDetallado item)
        {
            var descuento = item.Descuentos != null ? item.Descuentos.FirstOrDefault() : null;
            FacturaComponentes facturaComponentes = new FacturaComponentes()
            {
                Entidad = item.CodigoEntidad,
                Unidadfuncional = item.CodigoEntidad,
                NumeroFactura = item.NumeroFactura,
                IdTransaccion = item.IdTipoMovimiento,
                CodigoNumeracion = item.CodigoMovimiento,
                TransaccionVenta = item.IdTransaccion,
                NumeroVenta = item.NumeroVenta,
                IdProducto = item.IdProducto,
                Lote = 0,
                IdComponente = item.Componente,
                ComponenteHomologado = item.Componente,
                NombreComponenteHomologado = item.NombreComponente == null ? String.Empty : item.NombreComponente,
                CantidadComponente = (decimal)item.Cantidad,
                PorcentajeFactorQXAplicado = item.FactorQx,
                ValorUnitario = item.ValorUnitario,
                IdDescuento = descuento != null ? descuento.Id : 0,
                ValorDescuento = item.ValorDescuento,
                ValorRecargo = item.ValorRecargo,
                CantidadGlozasa = (decimal)0,
                FacDetTicCanDis = (decimal)item.CantidadDisponible,
                FacDetTicIndVal = (short)1,
                FacDetTicTipVal = string.Empty,
                FacDetTicRecIde = 0,
                FacDetTicRecTip = string.Empty,
                FacDetTicValMax = (decimal)0,
                FacdetTicValDif = (decimal)0
            };

            this.GuardarInformacionComponente(facturaComponentes);
        }

        /// <summary>
        /// Guardar Factura Relacion Encabezado.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <param name="lote">Parámetro lote.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void GuardarFacturaRelacionEncabezado(int numeroFactura, int identificadorPaquete, int numeroVenta, int lote)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarFacturaRelacionEncabezado(numeroFactura, identificadorPaquete, numeroVenta, lote);
        }

        /// <summary>
        /// Guardar Factura Relacion Encabezado Detalle.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="numeroFacturaPyG">The numero factura py G.</param>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void GuardarFacturaRelacionEncabezadoDetalle(int numeroFactura, int numeroFacturaPyG, int identificadorPaquete)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.GuardarFacturaRelacionEncabezadoDetalle(numeroFactura, numeroFacturaPyG, identificadorPaquete);
        }

        /// <summary>
        /// Guarda la informacion del Componente en la Factura.
        /// </summary>
        /// <param name="facturaComponentes">The factura componentes.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 14/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int GuardarInformacionComponente(FacturaComponentes facturaComponentes)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarFacturaComponente(facturaComponentes);
        }

        /// <summary>
        /// Inserta la informacion del responsable.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void GuardarInformacionFacturaResponsable(FacturaCompuesta facturaCompuesta, EstadoCuentaEncabezado estadoCuenta)
        {
            try
            {
                if (estadoCuenta.TipoFacturacion != TipoFacturacion.FacturacionRelacion && (estadoCuenta.Responsable != null || estadoCuenta.Responsable.IdTercero != 0))
                {
                    foreach (var item in estadoCuenta.FacturaAtencion)
                    {
                        foreach (var detalle in item.Detalle)
                        {
                            foreach (var ventaComponente in detalle.VentaComponentes)
                            {
                                this.GuardarResponsableFactura(this.CrearInformacionResponsableFactura(facturaCompuesta, ventaComponente, ventaComponente.Responsable, estadoCuenta.Responsable));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Método que guarda la información de los paquetes armados para la atención.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarInformacionPaquetesFactura(EncabezadoFactura encabezadoFactura, EstadoCuentaEncabezado estadoCuenta, FacturaCompuesta facturaCompuesta)
        {
            foreach (var paquete in estadoCuenta.FacturaPaquetes)
            {
                int facturaPyg = int.MinValue;
                int numeroVenta = this.InsertarVentaPaquete(estadoCuenta);
                int numeroLotePyG = this.InsertarVentaPaquetesDetalle(estadoCuenta, paquete, numeroVenta, true);
                int numeroLotePaquete = this.InsertarVentaPaquetesDetalle(estadoCuenta, paquete, numeroVenta, false);

                facturaPyg = new FachadaFacturacion().GuardarInformacionPaquetesFactura(int.Parse(estadoCuenta.NumeroFacturaSinPrefijo), paquete.PerdidaGanancia.ValorPaquetePG);

                foreach (var detallePaquete in paquete.Productos)
                {
                    ////Paquete
                    detallePaquete.IdPaquete = paquete.IdPaquete;
                    this.GuardarDetallePaqueteFactura(estadoCuenta.IdAtencion, facturaPyg.ToString(), detallePaquete, estadoCuenta, paquete);
                    ////Inserta los componentes
                    this.InsertarComponentes(detallePaquete, estadoCuenta, facturaPyg);
                }

                ////Inserta PyG
                this.GuardarDetalleFacturaPyG(paquete, facturaPyg, numeroVenta);
                ////Insertar Factura Relación encabezado
                this.GuardarFacturaRelacionEncabezado(int.Parse(estadoCuenta.NumeroFacturaSinPrefijo), paquete.IdPaquete, numeroVenta, numeroLotePaquete);
                ////Inserta el detalle de la relación del encabezado
                this.GuardarFacturaRelacionEncabezadoDetalle(int.Parse(estadoCuenta.NumeroFacturaSinPrefijo), facturaPyg, paquete.IdPaquete);
            }
        }

        /// <summary>
        /// Método para guardar el paquete armado de la factura.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="paquete">The paquete.</param>
        /// <returns>Cantidad registros.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private int GuardarPaqueteFactura(int identificadorAtencion, string numeroFactura, Paquete paquete)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarPaqueteFactura(identificadorAtencion, numeroFactura, paquete);
        }

        /// <summary>
        /// Metodo para guardar ventas asociada.
        /// </summary>
        /// <param name="ventaProductoRelacion">The venta producto relacion.</param>
        /// <returns>
        /// Id del Registro Creado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int GuardarVentaProductoRelacion(VentaProductoRelacion ventaProductoRelacion)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            return fachada.GuardarVentaProductoRelacion(ventaProductoRelacion);
        }

        /// <summary>
        /// Metodo para Homologar los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="homologacionProductos">The homologacion productos.</param>
        /// <returns>Hologacion de Producto.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private HomologacionProducto HomologacionProducto(BaseValidacion detalle, List<HomologacionProducto> homologacionProductos)
        {
            var productoHomologado = (from
                                          item in homologacionProductos
                                      orderby
                                          item.Homologado descending, item.FechaInicial descending
                                      where
                                          item.IdProducto == detalle.IdProductoRelacion
                                          && (detalle.FechaVenta.Date >= item.FechaInicial.Date && detalle.FechaVenta.Date <= item.FechaFinal.Date)
                                      select
                                          item).FirstOrDefault();

            return productoHomologado;
        }

        /// <summary>
        /// Homologar conceptos de cobro de facturacion por actividades.
        /// </summary>
        /// <param name="atencionCliente">The atencion cliente.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez.
        /// FechaDeCreacion: 18/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private List<FacturaAtencionConceptoCobro> HomologarConceptosCobro(AtencionCliente atencionCliente, EstadoCuentaEncabezado estadoCuenta)
        {
            List<FacturaAtencionConceptoCobro> listaConceptos = new List<FacturaAtencionConceptoCobro>();

            if (atencionCliente != null && atencionCliente.Deposito != null
                && atencionCliente.Deposito.Concepto != null
                && atencionCliente.Deposito.Concepto.Count > 0)
            {
                foreach (ConceptoCobro concepto in atencionCliente.Deposito.Concepto)
                {
                    if (concepto.IdContrato == estadoCuenta.IdContrato
                        && concepto.IdPlan == estadoCuenta.IdPlan
                        && concepto.IdAtencion == estadoCuenta.IdAtencion)
                    {
                        listaConceptos.Add(
                        new FacturaAtencionConceptoCobro()
                        {
                            IdAtencion = concepto.IdAtencion,
                            IdAtencionConcepto = concepto.IdConcepto,
                            ValorConcepto = concepto.ValorConcepto,
                            ValorSaldo = concepto.ValorSaldo,
                            ValorCruzado = concepto.ValorConcepto,
                            IndHabilitado = concepto.IndHabilitado,
                            CodigoConcepto = concepto.CodigoConcepto,
                            DepositoParticular = concepto.DepositoParticular,
                            IdContrato = concepto.IdContrato,
                            IdPlan = concepto.IdPlan
                        });
                    }
                }
            }

            return listaConceptos;
        }

        /// <summary>
        /// Metodo que genera el log de paquetes aplicados.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="registroPaquete">The registro paquete.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void HomologarProductosPaquetes(int identificadorProceso, Paquete registroPaquete, List<FacturaAtencionDetalle> detalle, CondicionContrato condicionContrato)
        {
            List<Paquete> paquetesAplicados = new List<Paquete>();

            var registro = new Paquete()
            {
                IdPaquete = registroPaquete.IdPaquete,
                CodigoPaquete = registroPaquete.CodigoPaquete,
                IndHabilitado = registroPaquete.IndHabilitado,
                NombrePaquete = registroPaquete.NombrePaquete,
                NombreTipoProducto = registroPaquete.NombreTipoProducto,
                PerdidaGanancia = registroPaquete.PerdidaGanancia,
                Cantidad = registroPaquete.Cantidad,
                Productos = registroPaquete.Productos,
                ValorPaquete = registroPaquete.ValorPaquete < registroPaquete.Productos.Sum(c => c.ValorPaqueteProducto) ? (registroPaquete.Productos.Sum(c => c.ValorPaqueteProducto) - registroPaquete.ValorPaquete) * registroPaquete.Cantidad : registroPaquete.ValorPaquete * registroPaquete.Cantidad,
                ProductoPaquete = registroPaquete.ProductoPaquete,
            };

            registro.ProductoPaquete.ValorOriginal = registroPaquete.ValorPaquete;

            List<FacturaAtencionDetalle> detallePaquetes = new List<FacturaAtencionDetalle>();
            FacturaAtencionDetalle productoDetalleVenta = null;
            foreach (var registroDetalle in registro.Productos)
            {
                var detalleFiltrado = detalle.Where(c => c.IdProducto == registroDetalle.IdProducto).FirstOrDefault();

                productoDetalleVenta = new FacturaAtencionDetalle()
                {
                    IdProducto = registroDetalle.IdProducto,
                    NombreProducto = registroDetalle.NombreProducto,
                    IdGrupoProducto = Convert.ToInt16(registroDetalle.IdGrupo),
                    NombreGrupo = registroPaquete.NombrePaquete,
                    ValorUnitario = registroDetalle.ValorPaqueteProducto,
                    ValorTotal = registroDetalle.ValorPaqueteProducto - ((detalleFiltrado != null ? detalleFiltrado.ValorDescuento : 0) * registro.Cantidad),
                    ValorProductos = registroDetalle.ValorPaqueteProducto,
                    CantidadProducto = registro.Cantidad,
                    CodigoProducto = detalleFiltrado != null ? detalleFiltrado.CodigoProducto : string.Empty,
                    CodigoGrupo = registroPaquete.PerdidaGanancia.CodigoPaquete,
                    ValorRecargo = detalleFiltrado != null ? detalleFiltrado.ValorRecargo : 0,
                    ValorTotalRecargo = detalleFiltrado != null ? detalleFiltrado.ValorTotalRecargo : 0,
                    ValorDescuento = detalleFiltrado != null ? detalleFiltrado.ValorDescuento : 0,
                    ValorOriginal = registro.ValorPaquete,
                    ValorTotalDescuento = detalleFiltrado != null ? detalleFiltrado.ValorTotalDescuento : 0,
                    FechaVenta = DateTime.Now,

                    DetalleVenta = new VentaDetalle()
                    {
                        TerceroVenta = new Tercero()
                        {
                            Id = detalleFiltrado != null ? detalleFiltrado.IdTerceroVenta : 0,
                            Nombre = detalleFiltrado != null ? detalleFiltrado.DetalleVenta.TerceroVenta.Nombre : string.Empty,
                            NumeroDocumento = detalleFiltrado != null ? detalleFiltrado.DetalleVenta.TerceroVenta.NumeroDocumento : string.Empty
                        },
                        ValorRecargo = 0,
                        ValorDescuento = 0
                    },
                    DetalleTarifa = new DetalleTarifa()
                    {
                        CodigoUnidad = string.Empty,
                        FechaVigenciaTarifa = DateTime.Now,
                        FechaInicial = DateTime.Now,
                        FechaFinal = DateTime.Now
                    },
                    Descuentos = detalleFiltrado != null ? detalleFiltrado.Descuentos : null,
                    NumeroVenta = detalleFiltrado != null ? detalleFiltrado.NumeroVenta : 0,
                    CondicionesTarifa = detalleFiltrado != null ? detalleFiltrado.CondicionesTarifa : null,
                    IndPaquete = 1,
                    ValorPaquete = registro.ValorPaquete,
                    VentaComponentes = new List<VentaComponente>()
                };

                List<FacturaAtencionDetalle> productoExistentePaquete = detalle.Where(c => c.IdProducto == productoDetalleVenta.IdProducto).ToList();
            }

            registroPaquete.ProductoPaquete.IndPaquete = 0;
            registroPaquete.ProductoPaquete.Orden = 1;
            detalle.Add(registroPaquete.ProductoPaquete);

            paquetesAplicados.Add(registro);
        }

        /// <summary>
        /// Guardar inforamción factura paquetes.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Factura Compuesta.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 28/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private FacturaCompuesta InicializarEncabezadoAuxiliarPaquetes(EncabezadoFactura encabezadoFactura, EstadoCuentaEncabezado estadoCuenta, FacturaCompuesta facturaCompuesta)
        {
            var movimientosTesoreria = new List<FacturaAtencionMovimiento>();
            if (estadoCuenta.FacturaAtencion != null && estadoCuenta.FacturaAtencion.Count > 0)
            {
                FachadaFacturacion fachada = new FachadaFacturacion();
                List<FacturaAtencionMovimiento> movientosTesoreriaActual = fachada.ConsultarMovimientosTesoreria(estadoCuenta.IdProceso);

                var conceptosCobro = this.HomologarConceptosCobro(estadoCuenta.AtencionActiva, estadoCuenta);
                estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro = conceptosCobro;
                estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria = movientosTesoreriaActual;
                estadoCuenta.TipoFacturacion = facturaCompuesta.TipoFacturacion;
                this.CruzarConceptosCobro(estadoCuenta.FacturaAtencion.FirstOrDefault(), estadoCuenta);

                if (estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro != null
                    && estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro.Count > 0)
                {
                    foreach (var item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro)
                    {
                        item.NumeroFactura = encabezadoFactura.NumeroFactura;
                        if (item.Actualizar == true && item.DepositoParticular == false)
                        {
                            this.ActualizarConceptosCobro(item);
                        }
                    }
                }

                if (estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria != null
                    && estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria.Count > 0)
                {
                    foreach (var item in estadoCuenta.FacturaAtencion.FirstOrDefault().MovimientosTesoreria)
                    {
                        if (item.Actualizar == true)
                        {
                            this.ActualizarMovimientosTesoreria(item);
                        }
                    }
                }
            }

            if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionPaquete)
            {
                if (estadoCuenta.FacturaPaquetes != null)
                {
                    this.GuardarInformacionPaquetesFactura(encabezadoFactura, estadoCuenta, facturaCompuesta);
                }
            }

            this.ValidarEstadoCuentaEncabezadoParticularPaquete(facturaCompuesta, estadoCuenta);

            this.ActualizarEstadoProcesoFactura(new ProcesoFactura() { IdProceso = facturaCompuesta.EstadoCuentaEncabezado.IdProceso, IdEstado = (int)ProcesoFactura.EstadoProceso.Facturado });

            return facturaCompuesta;
        }

        /// <summary>
        /// Guardar inforamción factura paquetes.
        /// </summary>
        /// <param name="facturaCompuesta">Factura Compuesta.</param>
        /// <returns>Retorna Factura Compuesta.</returns>
        private FacturaCompuesta InicializarEncabezadoGuardadoPaquetes(FacturaCompuesta facturaCompuesta)
        {
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;

            if (this.ValidarParticular(estadoCuenta))
            {
                if (estadoCuenta != null && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdTercero > 0)
                {
                    encabezadoFactura.IdTerceroResponsable = estadoCuenta.Responsable.IdTercero;
                    encabezadoFactura.TipoResultadoTercero = "T";
                }
                else if (estadoCuenta != null && estadoCuenta.Responsable != null && estadoCuenta.Responsable.IdCliente > 0)
                {
                    encabezadoFactura.IdTerceroResponsable = estadoCuenta.Responsable.IdCliente;
                    encabezadoFactura.TipoResultadoTercero = "C";
                }
            }

            if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionPaquete)
            {
                encabezadoFactura.VentasFactura = this.CrearVentaFacturaPaquete(estadoCuenta);
            }
            else
            {
                encabezadoFactura.VentasFactura = this.CrearVentaFactura(estadoCuenta);
            }

            return this.InicializarEncabezadoGuardadoXMLPaquetes(encabezadoFactura, estadoCuenta, facturaCompuesta);
        }

        /// <summary>
        /// Guardar inforamción factura paquetes.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Factura Compuesta.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 28/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private FacturaCompuesta InicializarEncabezadoGuardadoXMLPaquetes(EncabezadoFactura encabezadoFactura, EstadoCuentaEncabezado estadoCuenta, FacturaCompuesta facturaCompuesta)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<VentaFactura>));
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(memStream);
            xmlSerializer.Serialize(streamWriter, encabezadoFactura.VentasFactura);
            string ventas = this.RetornaXml(memStream, 5);

            memStream.Close();
            streamWriter.Close();

            xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(EncabezadoFactura));
            memStream = new System.IO.MemoryStream();
            streamWriter = new System.IO.StreamWriter(memStream);
            xmlSerializer.Serialize(streamWriter, encabezadoFactura);
            string encabezado = this.RetornaXml(memStream, 1);

            memStream.Close();
            streamWriter.Close();

            xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EstadoCuentaDetallado>));
            memStream = new System.IO.MemoryStream();
            streamWriter = new System.IO.StreamWriter(memStream);
            xmlSerializer.Serialize(streamWriter, estadoCuenta.EstadoCuentaDetallado);
            string detalles = this.RetornaXml(memStream, 2);

            memStream.Close();
            streamWriter.Close();

            xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EstadoCuentaDetallado>));
            memStream = new System.IO.MemoryStream();
            streamWriter = new System.IO.StreamWriter(memStream);
            xmlSerializer.Serialize(streamWriter, estadoCuenta.EstadoCuentaDetalladoComponentesMaestro);
            string mestroCompuesto = this.RetornaXml(memStream, 3);

            memStream.Close();
            streamWriter.Close();

            string[] valores = this.GuardarInformacionTodaFacturaPaquetes(encabezado, detalles, mestroCompuesto, ventas);

            encabezadoFactura.NumeroFactura = Convert.ToInt32(valores[0]);
            estadoCuenta.NumeroFactura = string.Format("{0}{1}", valores[1], valores[0]);
            estadoCuenta.NumeroFacturaSinPrefijo = valores[0];

            return this.InicializarEncabezadoGuardarNoFacturablePaquetes(encabezadoFactura, estadoCuenta, facturaCompuesta);
        }

        /// <summary>
        /// Guardar inforamción factura paquetes.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Factura Compuesta.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 28/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private FacturaCompuesta InicializarEncabezadoGuardarNoFacturablePaquetes(EncabezadoFactura encabezadoFactura, EstadoCuentaEncabezado estadoCuenta, FacturaCompuesta facturaCompuesta)
        {
            if (estadoCuenta.FacturaAtencion != null)
            {
                foreach (var item in encabezadoFactura.NoFacturables)
                {
                    var itemNoFacturable = new NoFacturable()
                    {
                        IdAtencion = item.IdAtencion,
                        ApellidoCliente = string.Empty,
                        NombreCliente = string.Empty,
                        NumeroDocumento = string.Empty,
                        IdExclusion = item.IdExclusion,
                        IdProcesoDetalle = item.IdProcesoDetalle,
                        IdProducto = item.IdProducto,
                        CodigoProducto = item.CodigoProducto,
                        NombreProducto = item.NombreProducto,
                        IdVenta = item.IdVenta,
                        NumeroVenta = item.NumeroVenta,
                        CantidadProducto = item.CantidadProducto,
                        NumeroFactura = encabezadoFactura.NumeroFactura
                    };
                    this.GuardarInformacionNoFacturable(itemNoFacturable);
                }
            }

            this.InicializarEncabezadoAuxiliarPaquetes(encabezadoFactura, estadoCuenta, facturaCompuesta);

            return facturaCompuesta;
        }

        /// <summary>
        /// Inicia Proceso para aplicar validaciones.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Factura Atencion.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private EstadoCuentaEncabezado IniciarProceso(ProcesoFactura procesoFactura)
        {
            EstadoCuentaEncabezado resultado = null;
            List<EstadoCuentaEncabezado> listaResultado = new List<EstadoCuentaEncabezado>();
            switch (procesoFactura.TipoFactura)
            {
                case TipoFacturacion.FacturacionRelacion:
                    resultado = this.ProcesoFacturaRelacion(procesoFactura);
                    break;

                case TipoFacturacion.FacturacionNoClinico:
                    resultado = this.ProcesoFacturaNoClinica(procesoFactura);
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    break;

                default:
                    return null;
            }

            return resultado;
        }

        /// <summary>
        /// Inicia Proceso para aplicar validaciones multiples.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Lista de estadoCuentaEncabezado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<EstadoCuentaEncabezado> IniciarProcesoMultiple(ProcesoFactura procesoFactura)
        {
            List<FacturaAtencionDetalle> listaFinal = new List<FacturaAtencionDetalle>();
            List<VentaComponente> listaFinalComponente = new List<VentaComponente>();
            FacturaAtencion objFactura;
            EstadoCuentaEncabezado resultado = null;
            List<EstadoCuentaEncabezado> listaResultado = new List<EstadoCuentaEncabezado>();
            List<FacturaAtencionDetalle> detallesSeparar = new List<FacturaAtencionDetalle>();

            //// Facturas seleccionadas a xml nodo item para envio a sp
            int[] arr = procesoFactura.VentasSeleccionadas.ToArray();
            XDocument doc = new XDocument();
            doc.Add(new XElement("root", arr.Select(x => new XElement("item", x))));

            objFactura = this.ConsultarGeneralFacturacion(new FacturaAtencion() { IdProceso = procesoFactura.IdProceso }, doc).FirstOrDefault();

            //// lquinterom: para facturacion por paquetes se toma la consulta realizada almacenada en objFactura de ventas y productos 
            //// y se cruza contra el paquete que se armo de esta armamos un solo objeto para procesarlo y generar estado de cuenta
            if (procesoFactura.TipoFactura == TipoFacturacion.FacturacionPaquete)
            {
                //// primero se arma la lista con las ventas y productos que quedaron por fuera del paquete 
                //// esta lista se arma cruzando lo que el usuario dejo por fuera del paquete con los datos
                //// que vienen de la base de datos de facturacion para completar la informacion
                List<FacturaAtencionDetalle> listFacturaAtencionDetalle = objFactura.Detalle;

                if (procesoFactura.Paquetes == null)
                {
                    throw new Exception("Debe seleccionar un paquete que contenga productos.");
                }
                else if (procesoFactura.Paquetes.Count() == 0)
                {
                    throw new Exception("Debe seleccionar un paquete que contenga productos.");
                }

                foreach (var item in procesoFactura.Paquetes)
                {
                    if (item.Productos.Count == 0)
                    {
                        throw new Exception("Debe seleccionar un paquete que contenga productos.");
                    }
                }

                var productosFueraPaquete = (from pv in listFacturaAtencionDetalle
                                             join pfp in procesoFactura.ProductosFueraPaquete on new { pv.IdProducto, pv.NumeroVenta } equals new { pfp.IdProducto, pfp.NumeroVenta }
                                             select pv).ToList().CopiarObjeto();

                foreach (var pfp in productosFueraPaquete)
                {
                    foreach (ProductoVenta pv in procesoFactura.ProductosFueraPaquete)
                    {
                        if (pfp.IdProducto == pv.IdProducto && pfp.NumeroVenta == pv.NumeroVenta)
                        {
                            pfp.CantidadProducto = pv.CantidadDisponible;
                            pfp.CantidadProductoFacturada = 0;
                            pfp.esPaquete = false;
                            break;
                        }
                    }
                }

                var productosPaquete = new List<FacturaAtencionDetalle>();
                var productosPaqueteAux = new List<FacturaAtencionDetalle>();

                // asignamos los el id del paquete a los productos que fueron empaquetados
                foreach (Paquete paq in procesoFactura.Paquetes)
                {
                    foreach (PaqueteProducto pp in paq.Productos)
                    {
                        pp.IdPaquete = paq.IdPaquete;
                    }
                }

                //// despues se arma la lista con las ventas y productos que quedaron dentro del paquete 
                //// esta lista se arma cruzando lo que el usuario dejo por fuera del paquete con los datos
                //// que vienen de la base de datos de facturacion para completar la informacion

                foreach (Paquete paq in procesoFactura.Paquetes)
                {
                    productosPaqueteAux = (from pv in listFacturaAtencionDetalle
                                           join pp in paq.Productos on new { pv.IdProducto, pv.NumeroVenta } equals new { pp.IdProducto, pp.NumeroVenta }
                                           select pv).ToList().CopiarObjeto();

                    foreach (var prodpaqdet in productosPaqueteAux)
                    {
                        foreach (PaqueteProducto prod in paq.Productos)
                        {
                            if (prodpaqdet.IdProducto == prod.IdProducto && prodpaqdet.NumeroVenta == prod.NumeroVenta)
                            {
                                if (prodpaqdet.idPaquete == 0)
                                {
                                    prodpaqdet.CantidadProducto = prod.CantidadAsignada;
                                    prodpaqdet.CantidadProductoFacturada = 0;
                                    prodpaqdet.esPaquete = true;
                                    prodpaqdet.idPaquete = paq.IdPaquete;
                                }

                                break;
                            }
                        }
                    }

                    productosPaquete.AddRange(productosPaqueteAux);
                }

                //// unimos ambas listas en una y con esta hacemos todo el procesamiento necesario para el estado de cuenta
                listFacturaAtencionDetalle = new List<FacturaAtencionDetalle>();
                listFacturaAtencionDetalle.AddRange(productosFueraPaquete);
                listFacturaAtencionDetalle.AddRange(productosPaquete);
                objFactura.Detalle = listFacturaAtencionDetalle;
            }

            try
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Procesando;
                this.ActualizarEstadoProcesoFactura(procesoFactura);

                foreach (var vinculacion in procesoFactura.Vinculaciones)
                {
                    FacturaAtencion atencion = objFactura.CopiarObjeto();

                    procesoFactura.IdTercero = vinculacion.Tercero.Id;
                    procesoFactura.IdContrato = vinculacion.Contrato.Id;
                    objFactura.IdManual = vinculacion.IdManual;
                    objFactura.IdTercero = vinculacion.Tercero.Id;
                    objFactura.IdContrato = vinculacion.Contrato.Id;
                    atencion.IdPlan = vinculacion.Plan.Id;

                    if (vinculacion.Orden != 1)
                    {
                        atencion.IdContrato = vinculacion.Contrato.Id;
                        atencion.IdManual = vinculacion.IdManual;
                        atencion.IdTercero = vinculacion.Tercero.Id;

                        if (detallesSeparar != null && detallesSeparar.Count > 0)
                        {
                            detallesSeparar.RemoveAll(a => a.CondicionSeparacion.Omitir == true);

                            foreach (FacturaAtencionDetalle item in detallesSeparar)
                            {
                                var itemModificar = this.BuscarDetalle(detallesSeparar, item);
                                if (itemModificar != null)
                                {
                                    itemModificar.CondicionSeparacion.NoCubrimiento = false;

                                    itemModificar.VentaComponentes.RemoveAll(a => a.CondicionSeparacion.Omitir == true);

                                    foreach (var itemComponente in itemModificar.VentaComponentes)
                                    {
                                        itemComponente.CondicionSeparacion.NoCubrimiento = false;
                                    }
                                }
                            }

                            objFactura.Detalle = detallesSeparar.CopiarObjeto();
                            atencion.Detalle = objFactura.Detalle;
                            detallesSeparar = null;
                        }
                        else
                        {
                            atencion.Detalle = null;
                        }
                    }

                    if (atencion.Detalle != null && atencion.Detalle.Count > 0)
                    {
                        resultado = this.ProcesoFacturaGeneralAtencion(procesoFactura, atencion, vinculacion, out detallesSeparar);

                        listaFinal = atencion.Detalle.CopiarObjeto();
                        foreach (FacturaAtencionDetalle item in atencion.Detalle)
                        {
                            var detalleS = this.BuscarDetalle(detallesSeparar, item);
                            if ((detalleS == null) && item.CondicionSeparacion.NoCubrimiento == true)
                            {
                                item.CondicionSeparacion.Omitir = false;
                                detallesSeparar.Add(item);
                            }

                            foreach (var itemComp in item.VentaComponentes)
                            {
                                if (detalleS == null && itemComp.CondicionSeparacion.NoCubrimiento == true)
                                {
                                    item.CondicionSeparacion.Omitir = false;
                                    detallesSeparar.Add(item);
                                    break;
                                }
                            }
                        }

                        listaFinal.RemoveAll(a => a.CondicionSeparacion.Omitir == true || a.CondicionSeparacion.NoCubrimiento == true || ((a.VentaComponentes == null || a.VentaComponentes.Count == 0) && a.CantidadProducto == 0));
                        atencion.Detalle = listaFinal;

                        foreach (FacturaAtencionDetalle item in atencion.Detalle)
                        {
                            listaFinalComponente = item.VentaComponentes.CopiarObjeto();
                            foreach (var itemComp in item.VentaComponentes)
                            {
                                listaFinalComponente.RemoveAll(a => (a.ValorUnitario == 0 || a.CondicionSeparacion.NoCubrimiento == true || a.CantidadProducto == 0) && a.Excluido == false);
                            }

                            item.VentaComponentes = listaFinalComponente;
                        }

                        if (listaFinal != null && listaFinal.Count > 0)
                        {
                            if (vinculacion.IndGenerar == 1)
                            {
                                resultado.Observaciones = vinculacion.Observacion;
                                listaResultado.Add(resultado);
                            }
                        }
                    }
                }

                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Finalizado;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
            }
            catch (Exception ex)
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
                throw ex;
            }

            return listaResultado;
        }

        /// <summary>
        /// Inicia Proceso para aplicar validaciones multiples.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Lista de estadoCuentaEncabezado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<EstadoCuentaEncabezado> IniciarProcesoPaquete(ProcesoFactura procesoFactura)
        {
            FacturaAtencion objFactura;

            //// Facturas seleccionadas a xml nodo item para envio a sp
            int[] arr = procesoFactura.VentasSeleccionadas.ToArray();
            XDocument doc = new XDocument();
            doc.Add(new XElement("root", arr.Select(x => new XElement("item", x))));
            objFactura = this.ConsultarGeneralFacturacion(new FacturaAtencion() { IdProceso = procesoFactura.IdProceso }, doc).FirstOrDefault();

            List<EstadoCuentaEncabezado> listaResultado = this.VerificacioinAuxInicio(procesoFactura, objFactura);

            return listaResultado;
        }

        /// <summary>
        /// Insertar Componentes.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private void InsertarComponentes(PaqueteProducto producto, EstadoCuentaEncabezado estadoCuenta, int numeroFactura)
        {
            EstadoCuentaDetallado tmpEstadoCuentaDetallado = estadoCuenta.EstadoCuentaRelacionPaquete.Where(c => c.NumeroVenta == producto.NumeroVenta &&
                                                               c.IdTransaccion == producto.IdTransaccion &&
                                                               c.IdProducto == producto.IdProducto).FirstOrDefault();

            if (tmpEstadoCuentaDetallado != null && tmpEstadoCuentaDetallado.VentaComponentes != null && tmpEstadoCuentaDetallado.VentaComponentes.Count() > 0)
            {
                ////Recorre la lista de los componentes
                tmpEstadoCuentaDetallado.VentaComponentes.ForEach(c =>
                {
                    if (c.Excluido == false)
                    {
                        this.GuardarDetalleFacturaPyGComponentes(c, numeroFactura);
                    }
                });
            }
        }

        /// <summary>
        /// Obtiene las condiciones de cubrimiento de la atencion.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <param name="listaCondicionesCubrimiento">The lista condiciones cubrimiento.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerCondicionesCubrimiento(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, CondicionCubrimiento condicionCubrimiento, List<CondicionCubrimiento> listaCondicionesCubrimiento)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            try
            {
                foreach (var item in nodos)
                {
                    condicionCubrimiento.IdElemento = 0;

                    if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                    {
                        valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                    }
                    else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                    {
                        valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                    }

                    if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                    {
                        valorComparacion = condicionCubrimiento.Propiedad(item.PropiedadEvaluar);
                    }

                    Debug.WriteLine("Nombre:{0} Id Atencion:{1} Id Detalle:{2} Id Plan:{3}", item.Nombre, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, condicionCubrimiento.IdPlan);
                    Debug.WriteLine("Propiedad {0} VE:{1} VC:{2} Id Elemento {3}", item.PropiedadEvaluar, valorEntrada, valorComparacion, item.IdElemento);

                    if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                    {
                        if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                        {
                            if (item.TieneElementosDependientes)
                            {
                                this.ObtenerCondicionesCubrimiento(item.Elementos, facturaAtencion, detalle, condicionCubrimiento, listaCondicionesCubrimiento);
                            }
                            else
                            {
                                condicionCubrimiento.IdElemento = item.IdElemento;
                                if (!listaCondicionesCubrimiento.Contains<CondicionCubrimiento>(condicionCubrimiento))
                                {
                                    listaCondicionesCubrimiento.Add(condicionCubrimiento);
                                }

                                Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Plan:{2} ", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, condicionCubrimiento.IdPlan);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// Obtiene las condiciones de tarifa de la atencion.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionTarifa">The condicion.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 22/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerCondicionesTarifaAtencion(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, CondicionTarifa condicionTarifa)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                condicionTarifa.IdElemento = 0;

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = condicionTarifa.Propiedad(item.PropiedadEvaluar);
                }

                Debug.WriteLine("Nombre:{0} Id Atencion:{1} Id Detalle:{2} Id Plan:{3}", item.Nombre, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, condicionTarifa.IdPlan);
                Debug.WriteLine("Propiedad {0} VE:{1} VC:{2} Id Elemento {3}", item.PropiedadEvaluar, valorEntrada, valorComparacion, item.IdElemento);

                if (item.Validador == null || (item.Validador != null && valorEntrada != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerCondicionesTarifaAtencion(item.Elementos, facturaAtencion, detalle, condicionTarifa);
                        }
                        else
                        {
                            condicionTarifa.IdElemento = item.IdElemento;
                            if (!detalle.CondicionesTarifa.Contains<CondicionTarifa>(condicionTarifa))
                            {
                                detalle.CondicionesTarifa.Add(condicionTarifa);
                            }

                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Plan:{2} ", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, condicionTarifa.IdPlan);

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la condicion de factura.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 19/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private CondicionTarifa ObtenerCondicionFactura(ProcesoFactura procesoFactura, int identificadorAtencion, Vinculacion vinculacion)
        {
            CondicionTarifa condicionFactura = null;
            var condicionFacturacion = new CondicionTarifa()
            {
                CodigoEntidad = procesoFactura.CodigoEntidad,
                IdContrato = procesoFactura.IdContrato,
                IdTercero = procesoFactura.IdTercero,
                IdPlan = vinculacion.Plan.Id,
                IdAtencion = identificadorAtencion,
            };
            var condicionesFacturacion = this.ConsultarCondicionesFacturacion(condicionFacturacion);
            if (condicionesFacturacion.Count > 0)
            {
                condicionFactura = this.ValidarCondicionFactura(condicionesFacturacion, identificadorAtencion, vinculacion);
            }

            return condicionFactura;
        }

        /// <summary>
        /// Obtiene un Registro de Costo Asociado.
        /// </summary>
        /// <param name="costosAsociados">The costos asociados.</param>
        /// <param name="identificadorManual">The id manual.</param>
        /// <returns>Costo Asociado.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 06/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CostoAsociado ObtenerCostoAsociado(List<CostoAsociado> costosAsociados, int identificadorManual)
        {
            var resultado = from
                                item in costosAsociados
                            where
                                item.IdManual == identificadorManual
                            orderby
                                item.IdAtencion descending,
                                item.IdServicio descending,
                                item.IdTipoAtencion descending,
                                item.IdGrupo descending,
                                item.IdProducto descending
                            select
                                item;
            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los costos asociados de la atenci n.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="costoAsociado">The costo asociado.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerCostosAsociados(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, VentaComponente detalle, CostoAsociado costoAsociado)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                costoAsociado.IdElemento = 0;

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = costoAsociado.Propiedad(item.PropiedadEvaluar);
                }

                Debug.WriteLine("Nombre:{0} Id Atencion:{1} Id Detalle:{2}", item.Nombre, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle);
                Debug.WriteLine("Propiedad {0} VE:{1} VC:{2} Id Elemento {3}", item.PropiedadEvaluar, valorEntrada, valorComparacion, item.IdElemento);

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerCostosAsociados(item.Elementos, facturaAtencion, detalle, costoAsociado);
                        }
                        else
                        {
                            costoAsociado.IdElemento = item.IdElemento;
                            detalle.CostosAsociados.Add(costoAsociado);
                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtener cubrimientos asociados a una atencion.
        /// </summary>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Cubrimiento ObtenerCubrimientos(IEnumerable<Cubrimiento> condiciones, BaseValidacion baseValidacion, FacturaAtencion atencion)
        {
            var resultado = from
                                item in condiciones
                            where
                                (item.IdProducto.Equals(baseValidacion.IdProducto) || item.IdProducto.Equals(0))
                                && (item.IdGrupoProducto.Equals(baseValidacion.IdGrupoProducto) || item.IdGrupoProducto.Equals(0))
                                && (item.IdTipoProducto.Equals(baseValidacion.IdTipoProducto) || item.IdTipoProducto.Equals(0))
                                && (item.IdContrato.Equals(atencion.IdContrato) || item.IdContrato.Equals(0))
                                && (item.IdPlan.Equals(atencion.IdPlan) || item.IdPlan.Equals(0))
                                && (item.IdAtencion.Equals(atencion.IdAtencion) || item.IdAtencion.Equals(0))
                                && (item.Componente.Equals(baseValidacion.Componente) || item.Componente.Equals("NA"))
                            orderby
                                 item.IdAtencion descending, item.IdProducto descending,
                                 item.IdGrupoProducto descending, item.IdTipoProducto,
                                 item.IdPlan descending, item.IdContrato descending
                            select
                                item;
            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Aplica las validaciones de descuentos por contrato.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="descuento">The descuento.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerDescuentosContrato(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, Descuento descuento)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            if (detalle.Descuentos == null)
            {
                detalle.Descuentos = new List<Descuento>();
            }

            foreach (var item in nodos)
            {
                if (detalle.Descuentos != null && detalle.Descuentos.Where(s => s.Id == descuento.Id).Count() > 0)
                {
                    continue;
                }

                descuento.IdElemento = 0;

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = descuento.Propiedad(item.PropiedadEvaluar);
                }

                if (item.Validador == null || (item.Validador != null && valorEntrada != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper().Equals(item.ValorTodos.ToUpper())))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerDescuentosContrato(item.Elementos, facturaAtencion, detalle, descuento);
                        }
                        else
                        {
                            descuento.IdElemento = item.IdElemento;
                            if (!detalle.Descuentos.Contains<Descuento>(descuento))
                            {
                                detalle.Descuentos.Add(descuento);
                            }

                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Descuento:{2} IdElemento:{3} Id Producto:{4}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, descuento.Id, descuento.IdElemento, detalle.IdProducto);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene las exclusiones de la atencion.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="exclusion">The exclusion.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerExclusionesAtencion(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, Exclusion exclusion)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                if (detalle.Exclusion != null)
                {
                    break;
                }

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = exclusion.Propiedad(item.PropiedadEvaluar);
                }

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    // Esta validación de tipos se hace porque se detectó una diferencia de tipos en algunos casos.
                    // Específicamente se detectaron valores de entrada tipo Int16 y valores de comparacion tipo Int32 (falla el Object.Equals)
                    // Por lo tanto se trata de hacer un mapeo siempre a Int32 (mayor espacio) para que la comparación no falle (se utiliza operador ==).
                    // Si se detectaran tipos diferentes a Int32 y a Int16 se debería agregar el código correspondiente (ejm: Decimal vs Int)
                    Type tipoValorEntrada = valorEntrada.GetType();
                    Type tipoValorComparacion = valorComparacion.GetType();

                    // Si los tipos de datos son diferentes
                    if (!tipoValorEntrada.Equals(tipoValorComparacion))
                    {
                        int valEnt = 0, valCom = 0;

                        if (tipoValorEntrada.Equals(typeof(int)))
                        {
                            valEnt = (int)Convert.ChangeType(valorEntrada, tipoValorEntrada);
                            valCom = (int)Convert.ChangeType(valorComparacion, tipoValorEntrada);
                        }
                        else if (tipoValorEntrada.Equals(typeof(short)) && tipoValorComparacion.Equals(typeof(int)))
                        {
                            valEnt = (int)Convert.ChangeType(valorEntrada, tipoValorComparacion);
                            valCom = (int)Convert.ChangeType(valorComparacion, tipoValorComparacion);
                        }

                        if (valEnt == valCom || (item.Todos && valCom.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                        {
                            if (item.TieneElementosDependientes)
                            {
                                this.ObtenerExclusionesAtencion(item.Elementos, facturaAtencion, detalle, exclusion);
                            }
                            else
                            {
                                detalle.Exclusion = exclusion;
                                Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                                Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                                break;
                            }
                        }
                        else if (item.EsAlterno && exclusion.IdProductoAlterno > 0 && detalle.IdProducto == exclusion.IdProducto)
                        {
                            bool exclusionAplicada = this.ObtenerProductoAlterno(facturaAtencion, detalle, valEnt, exclusion);

                            if (exclusionAplicada)
                            {
                                detalle.Exclusion = exclusion;
                                Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                                Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                        {
                            if (item.TieneElementosDependientes)
                            {
                                this.ObtenerExclusionesAtencion(item.Elementos, facturaAtencion, detalle, exclusion);
                            }
                            else
                            {
                                detalle.Exclusion = exclusion;
                                Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                                Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                                break;
                            }
                        }
                        else if (item.EsAlterno && exclusion.IdProductoAlterno > 0 && detalle.IdProducto == exclusion.IdProducto)
                        {
                            bool exclusionAplicada = this.ObtenerProductoAlterno(facturaAtencion, detalle, valorEntrada, exclusion);

                            if (exclusionAplicada)
                            {
                                detalle.Exclusion = exclusion;
                                Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                                Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene las exclusiones que son no marcadas de Exclusion de contrato.
        /// </summary>
        /// <param name="lstNoMarcadas">The no marcadas.</param>
        /// <param name="condicion">The condicion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 17/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Exclusion> ObtenerExclusionesContratoMarcadas(List<NoMarcada> lstNoMarcadas, CondicionProceso condicion)
        {
            var listaExclusiones = from
                                       exclusion in condicion.Objeto as List<Exclusion>
                                   where
                                       this.ValidarExclusion(lstNoMarcadas, exclusion, TipoExclusion.Contrato.ToString()) == false
                                   select
                                       exclusion;
            return listaExclusiones.Count() == 0 ? new List<Exclusion>() : listaExclusiones.ToList();
        }

        /// <summary>
        /// Aplica las validaciones de exclusiones por Manual.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="exclusion">The exclusion.</param>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerExclusionesManual(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, ExclusionManual exclusion)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                if (detalle.ExclusionManual != null)
                {
                    break;
                }

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = exclusion.Propiedad(item.PropiedadEvaluar);
                }

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorComparacion != null && (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper())))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerExclusionesManual(item.Elementos, facturaAtencion, detalle, exclusion);
                        }
                        else
                        {
                            detalle.ExclusionManual = exclusion;
                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                            Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                            break;
                        }
                    }
                    else if (item.EsAlterno && exclusion.IdProductoAlterno > 0 && detalle.IdProducto == exclusion.IdProducto)
                    {
                        bool exclusionAplicada = this.ObtenerProductoAlterno(facturaAtencion, detalle, valorEntrada, exclusion);

                        if (exclusionAplicada)
                        {
                            detalle.ExclusionManual = exclusion;
                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Exclusion:{2}", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, exclusion.Id);
                            Debug.WriteLine("Id Producto:{0} Id Producto Alt:{1} Nombre:{2} Id Elemento: {3} Es Alterno: {4} Prioridad: {5}", detalle.IdProducto, exclusion.IdProductoAlterno, item.Nombre, item.IdElemento, item.EsAlterno, item.Prioridad);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene las exclusiones que son no marcadas de Exclusion de manual.
        /// </summary>
        /// <param name="lstNoMarcadas">The no marcadas.</param>
        /// <param name="condicion">The condicion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 17/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<ExclusionManual> ObtenerExclusionesManualMarcadas(List<NoMarcada> lstNoMarcadas, CondicionProceso condicion)
        {
            var listaExclusiones = from
                                       exclusionManual in condicion.Objeto as List<ExclusionManual>
                                   where
                                       this.ValidarExclusion(lstNoMarcadas, exclusionManual, TipoExclusion.Manual.ToString()) == false
                                   select
                                       exclusionManual;
            return listaExclusiones.Count() == 0 ? new List<ExclusionManual>() : listaExclusiones.ToList();
        }

        /// <summary>
        /// Obtiene Informaci n del movimiento de cartera.
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <param name="codigoMovimiento">The codigo movimiento.</param>
        /// <returns>Informaci n del movimiento de cartera.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private MovimientoCartera ObtenerIdMovimiento(CuentaCartera cuenta, string codigoMovimiento)
        {
            var movimiento = from
                                 item in cuenta.MovimientosCartera
                             where
                                 item.CodigoMovimientoSecuencial == codigoMovimiento
                             select
                                 item;
            return movimiento.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene la informacion del plan para el particular.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Id del Contrato.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int ObtenerInformacionContratoParticular(FacturaAtencion atencion)
        {
            var particular = from
                                 detalle in atencion.Detalle
                             where
                                 detalle.DetalleVenta != null
                             select
                                 detalle.DetalleVenta;
            return particular.FirstOrDefault().IdContrato;
        }

        /// <summary>
        /// Obtiene la informacion del plan para el particular.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Id del plan.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private int ObtenerInformacionPlanParticular(FacturaAtencion atencion)
        {
            var particular = from
                                 detalle in atencion.Detalle
                             where
                                 detalle.DetalleVenta != null
                             select
                                 detalle.DetalleVenta;
            return particular.FirstOrDefault().IdPlan;
        }

        /// <summary>
        /// Establece el movimiento de documento.
        /// </summary>
        /// <param name="movimientoDocumento">The movimiento documento.</param>
        /// <returns>Movimiento de documento.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private string ObtenerMovimientoDocumento(int movimientoDocumento)
        {
            string movimientoDoc = string.Empty;
            if (movimientoDocumento == ClaseMovimiento.Abono.GetHashCode())
            {
                movimientoDoc = Constantes.MovimientoContabilidad_CRAB;
            }
            else
            {
                movimientoDoc = Constantes.MovimientoContabilidad_BIL;
            }

            return movimientoDoc;
        }

        /// <summary>
        /// Obtiene Niveles de Complejidad.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="nivelesComplejidad">The niveles complejidad.</param>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerNivelesComplejidad(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, VentaComponente detalle, NivelComplejidad nivelesComplejidad)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                if (detalle.NivelesComplejidad != null && detalle.NivelesComplejidad.IdManual > 0)
                {
                    break;
                }

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = nivelesComplejidad.Propiedad(item.PropiedadEvaluar);
                }

                Debug.WriteLine("Nombre:{0} Id Atencion:{1} Id Detalle:{2}", item.Nombre, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle);
                Debug.WriteLine("Propiedad {0} VE:{1} VC:{2} Id Elemento {3}", item.PropiedadEvaluar, valorEntrada, valorComparacion, item.IdElemento);

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerNivelesComplejidad(item.Elementos, facturaAtencion, detalle, nivelesComplejidad);
                        }
                        else
                        {
                            nivelesComplejidad.Producto = item.IdElemento;
                            detalle.NivelesComplejidad = nivelesComplejidad;
                            Debug.WriteLine("Id Atencion:{0} Id Detalle:{1} Id Producto:{2} ", facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, nivelesComplejidad.Producto);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Establece dentro de las validaciones si existe el producto alterno.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="atencionDetalle">The atencion detalle.</param>
        /// <param name="valorEntrada">The valor entrada.</param>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>
        /// Devuelve true si el valor de entrada evaluado es alterno.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Establece dentro de las validaciones si existe el producto alterno.
        /// </remarks>
        private bool ObtenerProductoAlterno(FacturaAtencion facturaAtencion, BaseValidacion atencionDetalle, object valorEntrada, Exclusion exclusion)
        {
            var existe = from
                             detalle in facturaAtencion.Detalle
                         where
                             detalle.IdProducto == exclusion.IdProductoAlterno
                             && detalle.NumeroVenta == atencionDetalle.NumeroVenta
                             && detalle.IdTransaccion == atencionDetalle.IdTransaccion
                         select
                             detalle;
            return existe.Count() > 0 ? false : true;
        }

        /// <summary>
        /// Obtiene los recargos del contrato.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="recargo">The recargo.</param>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 25/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerRecargosContrato(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, Recargo recargo)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                if (detalle.RecargoContrato != null && detalle.RecargoContrato.Where(s => s.Id == recargo.Id).Count() > 0)
                {
                    continue;
                }

                recargo.IdElemento = 0;

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = recargo.Propiedad(item.PropiedadEvaluar);
                }

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerRecargosContrato(item.Elementos, facturaAtencion, detalle, recargo);
                        }
                        else
                        {
                            recargo.IdElemento = item.IdElemento;
                            detalle.RecargoContrato.Add(recargo);
                            Debug.WriteLine("Propiedad:{0} Prioridad:{1} Id Atencion:{2} Id Detalle:{3} Id Contrato:{4} Id Producto:{5} Id Grupo:{6} Id Tipo:{7} Id Recargo:{8} Elemento:{9}", item.PropiedadEvaluar, item.Prioridad, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, detalle.IdProducto, detalle.IdGrupoProducto, detalle.IdTipoProducto, recargo.IdContrato, recargo.Id, item.IdElemento);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene los recargos del manual.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="recargoManual">The recargo manual.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ObtenerRecargosManual(ObservableCollection<NodoValidacion> nodos, FacturaAtencion facturaAtencion, BaseValidacion detalle, RecargoManual recargoManual)
        {
            object valorEntrada = null;
            object valorComparacion = null;
            foreach (var item in nodos)
            {
                if (detalle.RecargoManual != null && detalle.RecargoManual.Where(s => s.Id == recargoManual.Id).Count() > 0)
                {
                    continue;
                }

                recargoManual.IdElemento = 0;

                if (!string.IsNullOrEmpty(item.PropiedadAtencion))
                {
                    valorEntrada = facturaAtencion.Propiedad(item.PropiedadAtencion);
                }
                else if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                {
                    valorEntrada = detalle.Propiedad(item.PropiedadDetalle);
                }

                if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                {
                    valorComparacion = recargoManual.Propiedad(item.PropiedadEvaluar);
                }

                if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                {
                    if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                    {
                        if (item.TieneElementosDependientes)
                        {
                            this.ObtenerRecargosManual(item.Elementos, facturaAtencion, detalle, recargoManual);
                        }
                        else
                        {
                            recargoManual.IdElemento = item.IdElemento;
                            detalle.RecargoManual.Add(recargoManual);
                            Debug.WriteLine("Propiedad:{0} Prioridad:{1} Id Atencion:{2} Id Detalle:{3} Id Contrato:{4} Id Producto:{5} Id Grupo:{6} Id Tipo:{7} Id Recargo:{8} Elemento:{9}", item.PropiedadEvaluar, item.Prioridad, facturaAtencion.IdAtencion, detalle.IdProcesoDetalle, detalle.IdProducto, detalle.IdGrupoProducto, detalle.IdTipoProducto, recargoManual.IdContrato, recargoManual.Id, item.IdElemento);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion de consulta de terceros responsables segun validaciones.
        /// </summary>
        /// <param name="nodos">The nodos.</param>
        /// <param name="honorarioBuscado">The honorario buscado.</param>
        /// <param name="honorarioAplicar">The honorario aplicar.</param>
        /// <returns>
        /// Retorna Honorario.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Honorario ObtenerResponsableVentas(ObservableCollection<NodoValidacion> nodos, Honorario honorarioBuscado, Honorario honorarioAplicar)
        {
            Honorario resultado = null;
            object valorEntrada = null;
            object valorComparacion = null;
            string error = string.Empty;
            int cont = 0;
            try
            {
                foreach (var item in nodos)
                {
                    cont = cont + 1;

                    if (!string.IsNullOrEmpty(item.PropiedadDetalle))
                    {
                        valorEntrada = honorarioBuscado.Propiedad(item.PropiedadDetalle);
                    }

                    if (!string.IsNullOrEmpty(item.PropiedadEvaluar))
                    {
                        valorComparacion = honorarioAplicar.Propiedad(item.PropiedadEvaluar);
                    }

                    if (valorEntrada == null)
                    {
                        valorEntrada = string.Empty;
                    }

                    if (item.Validador == null || (item.Validador != null && item.Validador.IsMatch(valorEntrada.ToString())))
                    {
                        if (valorEntrada.Equals(valorComparacion) || (item.Todos && valorComparacion.ToString().ToUpper() == item.ValorTodos.ToUpper()))
                        {
                            if (item.TieneElementosDependientes)
                            {
                                resultado = this.ObtenerResponsableVentas(item.Elementos, honorarioBuscado, honorarioAplicar);
                            }
                            else
                            {
                                resultado = honorarioAplicar;
                                break;
                            }
                        }

                        if (resultado != null)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                error = err.ToString();
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el valor del Descuento.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Valor Descuento.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal ObtenerValorDescuento(BaseValidacion detalle)
        {
            decimal retornoDescuento = 0;
            int identificadorManual = 0;
            if (detalle.Descuentos != null && detalle.Descuentos.Count() > 0)
            {
                var descuento = detalle.Descuentos.FirstOrDefault();

                identificadorManual = detalle.CondicionesTarifa != null ? (detalle.CondicionesTarifa.Count > 0 ? detalle.CondicionesTarifa.FirstOrDefault().IdManual : 0) : 0;

                if (descuento.Porcentaje)
                {
                    retornoDescuento = this.Redondeo(((detalle.ValorUnitario + detalle.ValorRecargo) * descuento.ValorDescuento) / 100, descuento.IdContrato, identificadorManual);
                }
                else
                {
                    retornoDescuento = descuento.ValorDescuento;
                }
            }

            return retornoDescuento;
        }

        /// <summary>
        /// Método para obtener los valores calculados de los productos del paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="condicionContrato">The condicion Contrato.</param>
        /// <param name="identificadorManual">The id manual.</param>
        /// <param name="fechaVigencia">The fecha vigencia.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Lista de productos del paquete.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<PaqueteProducto> ObtenerValoresPaquetesProductos(Paquete paquete, CondicionContrato condicionContrato, int identificadorManual, DateTime fechaVigencia, List<FacturaAtencionDetalle> detalle)
        {
            var itemsPaquetesProductos = from producto in paquete.Productos
                                         join tarifa in condicionContrato.DetalleTarifa
                                         on producto.IdProducto equals tarifa.IdProducto
                                         where (tarifa.IdManual == identificadorManual)
                                         && (tarifa.FechaVigenciaTarifa == fechaVigencia)
                                         select new PaqueteProducto()
                                         {
                                             IdProducto = producto.IdProducto,
                                             IdTransaccion = producto.IdTransaccion,
                                             CodigoEntidad = producto.CodigoEntidad,
                                             IdGrupo = producto.IdGrupo,
                                             IdPaquete = producto.IdPaquete,
                                             IndHabilitado = producto.IndHabilitado,
                                             CantidadAsignada = producto.CantidadAsignada,
                                             CantidadMaxima = producto.CantidadMaxima,
                                             NombreGrupo = producto.NombreGrupo != null ? producto.NombreGrupo : string.Empty,
                                             NombreProducto = producto.NombreProducto != null ? producto.NombreProducto : string.Empty,
                                             ProductosAsociados = producto.ProductosAsociados,
                                             ValorPaqueteProducto = tarifa.ValorTarifa,
                                             NumeroVenta = producto.NumeroVenta,
                                             FechaVenta = producto.FechaVenta,
                                             ValorDescuento = (detalle.Where(c => c.IdProducto == producto.IdProducto) != null
                                                               && detalle.Where(c => c.IdProducto == producto.IdProducto).ToList().Count > 0)
                                                               ? detalle.Where(c => c.IdProducto == producto.IdProducto).FirstOrDefault().ValorDescuento : 0,
                                             ValorRecargo = (detalle.Where(c => c.IdProducto == producto.IdProducto) != null
                                             && detalle.Where(c => c.IdProducto == producto.IdProducto).ToList().Count > 0)
                                             ? detalle.Where(c => c.IdProducto == producto.IdProducto).FirstOrDefault().ValorRecargo : 0,
                                         };

            var listaValores = itemsPaquetesProductos != null ? itemsPaquetesProductos.ToList() : null;
            return (listaValores == null || listaValores.Count() == 0) ? null : listaValores;
        }

        /// <summary>
        /// Obtener Valores Paquetes Productos Componentes.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="identificadorManual">The identificador manual.</param>
        /// <param name="fechaVigencia">The fecha vigencia.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Listado venta componente.</returns>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 04/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private List<VentaComponente> ObtenerValoresPaquetesProductosComponentes(Paquete paquete, CondicionContrato condicionContrato, int identificadorManual, DateTime fechaVigencia, List<VentaComponente> detalle)
        {
            var itemsPaquetesProductos = from producto in detalle
                                         join tarifa in condicionContrato.DetalleTarifa
                                         on producto.IdComponente equals tarifa.IdProducto
                                         where (tarifa.IdManual == identificadorManual)
                                         && (tarifa.FechaVigenciaTarifa == fechaVigencia)
                                         select new VentaComponente()
                                         {
                                             IdProducto = producto.IdProducto,
                                             IdTransaccion = producto.IdTransaccion,
                                             CodigoEntidad = producto.CodigoEntidad,
                                             IdGrupoProducto = producto.IdGrupoProducto,
                                             IdComponente = producto.IdComponente,
                                             IndHabilitado = producto.IndHabilitado,
                                             CantidadComponente = producto.CantidadComponente,
                                             NombreGrupo = producto.NombreGrupo != null ? producto.NombreGrupo : string.Empty,
                                             NombreProducto = producto.NombreProducto != null ? producto.NombreProducto : string.Empty,
                                             ValorUnitario = tarifa.ValorTarifa,
                                             NumeroVenta = producto.NumeroVenta,

                                             // FechaVenta = producto.FechaVenta,
                                             // ValorDescuento = (detalle.Where(c => c.IdProducto == producto.IdProducto) != null
                                             //                  && detalle.Where(c => c.IdProducto == producto.IdProducto).ToList().Count > 0)
                                             //                  ? detalle.Where(c => c.IdProducto == producto.IdProducto).FirstOrDefault().ValorDescuento : 0,
                                             // ValorRecargo = (detalle.Where(c => c.IdProducto == producto.IdProducto) != null
                                             // && detalle.Where(c => c.IdProducto == producto.IdProducto).ToList().Count > 0)
                                             // ? detalle.Where(c => c.IdProducto == producto.IdProducto).FirstOrDefault().ValorRecargo : 0,
                                         };

            var listaValores = itemsPaquetesProductos != null ? itemsPaquetesProductos.ToList() : null;
            return (listaValores == null || listaValores.Count() == 0) ? null : listaValores;
        }

        /// <summary>
        /// Obtiene el valor del Recargo.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Valor de Recargo.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal ObtenerValorRecargo(BaseValidacion detalle)
        {
            decimal retornoRecargo = 0;
            if (detalle.RecargoContrato != null && detalle.RecargoContrato.Count() > 0)
            {
                var recargo = detalle.RecargoContrato.FirstOrDefault();
                retornoRecargo = (detalle.ValorUnitario * recargo.PorcentajeRecargo) / 100;
            }
            else if (detalle.RecargoManual != null && detalle.RecargoManual.Count() > 0)
            {
                var recargo = detalle.RecargoManual.FirstOrDefault();
                retornoRecargo = (detalle.ValorUnitario * recargo.PorcentajeRecargo) / 100;
            }

            return retornoRecargo;
        }

        /// <summary>
        /// Obtiene el valor de la tarifa ajustada.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <param name="condicionesContrato">The condiciones contrato.</param>
        /// <param name="tipoTarifa">The tipo tarifa.</param>
        /// <returns>
        /// Valor de Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal ObtenerValorTarifa(BaseValidacion detalle, CondicionContrato condicionesContrato, ref TipoTarifa tipoTarifa)
        {
            decimal retornoTarifa = 0;
            if (detalle.CondicionesTarifa != null && detalle.CondicionesTarifa.Count() > 0)
            {
                var condicionTarifa = detalle.CondicionesTarifa.FirstOrDefault();
                tipoTarifa = condicionTarifa.TipoCondicionTarifa;
                if (condicionTarifa.Porcentaje && detalle.DetalleTarifa != null)
                {
                    retornoTarifa = detalle.DetalleTarifa.ValorTarifa;
                }
                else if (condicionTarifa.TipoCondicionTarifa == TipoTarifa.ValorMaxPorcentaje || condicionTarifa.TipoCondicionTarifa == TipoTarifa.Cantidad)
                {
                    retornoTarifa = detalle.ValorBruto;
                }
                else
                {
                    retornoTarifa = condicionTarifa.ValorPropio;
                }
            }
            else
            {
                if (detalle.DetalleTarifa != null && detalle is FacturaAtencionDetalle)
                {
                    retornoTarifa = detalle.DetalleTarifa.ValorTarifa;
                }
                else
                {
                    retornoTarifa = detalle.ValorBruto;
                }
            }

            return retornoTarifa;
        }

        /// <summary>
        /// Obtiene la informacion del tercero para el particular.
        /// </summary>
        /// <param name="atencion">Parametro atencion.</param>
        /// <param name="info">Parametro info.</param>
        /// <returns>Informaci n particular del tercero.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private Tercero ObternerInformacionTerceroParticular(FacturaAtencion atencion, InformacionFactura info)
        {
            if (atencion.Detalle.FirstOrDefault().DetalleVenta != null)
            {
                var particular = atencion.Detalle.FirstOrDefault().DetalleVenta.TerceroVenta;
                return particular;
            }
            else
            {
                return new Tercero();
            }
        }

        /// <summary>
        /// Omitir Detalles x Negocio.
        /// </summary>
        /// <param name="vinculacionActual">The vinculacion actual.</param>
        /// <param name="original">The original.</param>
        /// <param name="producto">The producto.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias .
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Omitir Detalles x Negocio.
        /// </remarks>
        private void OmitirDetallesxNegocio(List<FacturaAtencionDetalle> vinculacionActual, List<FacturaAtencionDetalle> original, FacturaAtencionDetalle producto)
        {
            FacturaAtencionDetalle productoResultado = null;
            productoResultado = this.BuscarProducto(original, producto);
            if (!producto.CondicionSeparacion.Omitir)
            {
                vinculacionActual.Add(productoResultado);
            }

            foreach (var componente in producto.VentaComponentes)
            {
                if (componente.CondicionSeparacion.Omitir)
                {
                    var componenteResultado = this.BuscarVentaComponente(productoResultado, componente);
                    productoResultado.VentaComponentes.Remove(componenteResultado);
                }
            }
        }

        /// <summary>
        /// Metodo que omite las exclusiones no marcadas en el proceso.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 17/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void OmitirExclusionesNoMarcadas(ProcesoFactura procesoFactura, List<CondicionProceso> condiciones)
        {
            if (procesoFactura.ExclusionesNoMarcadas != null)
            {
                var exclusionesContrato = condiciones.Where(item => item.Tipo == CondicionProceso.TipoObjeto.Exclusiones).FirstOrDefault();
                exclusionesContrato.Objeto = this.ObtenerExclusionesContratoMarcadas(procesoFactura.ExclusionesNoMarcadas, exclusionesContrato);
                var exclusionesManual = condiciones.Where(item => item.Tipo == CondicionProceso.TipoObjeto.ExclusionesManual).FirstOrDefault();
                exclusionesManual.Objeto = this.ObtenerExclusionesManualMarcadas(procesoFactura.ExclusionesNoMarcadas, exclusionesManual);
            }
        }

        /// <summary>
        /// Metodo para realizar el orden de los factores.
        /// </summary>
        /// <param name="productos">The productos.</param>
        /// <param name="componentesProductos">The componentes productos.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void OrdenarFactor(List<FacturaAtencionDetalle> productos, List<VentaComponente> componentesProductos)
        {
            FacturaAtencionDetalle detalleVenta = null;
            var consolidado = from
                                  producto in productos
                              orderby
                                  producto.VentaComponentes.Sum(item => item.ValorBruto) descending,
                                  producto.NombreProducto ascending
                              select
                                  new
                                  {
                                      IdProducto = producto.IdProducto,
                                      Valor = producto.VentaComponentes.Sum(item => item.ValorBruto)
                                  };
            short ordenFactor = 1;
            foreach (var item in consolidado)
            {
                detalleVenta = this.BuscarProductoIdProducto(productos, item.IdProducto);
                detalleVenta.OrdenQX = ordenFactor;
                ordenFactor += 1;
            }
        }

        /// <summary>
        /// Metodo para Calcular el Valor del Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Producto venta.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 06/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private TipoProductoCompuesto ProcesarProductoVenta(TipoProductoCompuesto producto, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool porcentaje = false;
            CondicionContrato condicionContratoBase;
            CondicionContrato condicionContrato = producto.CondicionContrato;
            var condiciones = this.CargarCondicionesProceso(this.CrearProceso(producto));
            try
            {
                condicionContratoBase = condicionContrato;

                var condicionTarifa = this.AplicarReglaCondicionTarifa(condiciones, atencion, detalle, TipoFacturacion.FacturacionRelacion).FirstOrDefault();
                condicionContrato = this.EvaluarCondicionContratoManuales(condicionContrato, condicionTarifa);

                porcentaje = condicionTarifa != null ? condicionTarifa.Porcentaje : false;

                if (condicionTarifa == null || porcentaje)
                {
                    detalle.DetalleTarifa = this.AplicarAjusteTarifaVenta(condicionContrato, detalle, TipoFacturacion.FacturacionRelacion, condicionTarifa, porcentaje);
                }

                bool recargoAplicado = this.AplicarReglaRecargosContrato(condiciones, atencion, detalle);

                if (!recargoAplicado)
                {
                    this.AplicarReglaRecargosManual(condiciones, atencion, detalle);
                }

                this.AplicarReglaDescuentos(condiciones, atencion, detalle);

                condicionContrato = condicionContratoBase;

                if (condicionTarifa != null)
                {
                    producto.CondicionTarifa = condicionTarifa;
                }

                if (detalle.Descuentos != null && detalle.Descuentos.Count > 0)
                {
                    producto.Descuento = detalle.Descuentos.FirstOrDefault();
                }

                if (detalle.RecargoContrato != null && detalle.RecargoContrato.Count > 0)
                {
                    producto.Recargo = detalle.RecargoContrato.FirstOrDefault();
                }
                else if (detalle.RecargoManual != null && detalle.RecargoManual.Count > 0)
                {
                    producto.RecargoManual = detalle.RecargoManual.FirstOrDefault();
                }

                this.AplicarCalculos(detalle, condicionContrato);
                producto.Tarifa = detalle.DetalleTarifa;
                producto.ValorTotal = detalle.ValorUnitario;
                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Proceso Adicional Facturacion Complementaria.
        /// </summary>
        /// <param name="facturaCompuesta">Factura Compuesta.</param>
        /// <returns>True si el proceso es exitoso.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 28/01/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        private bool ProcesoAdicionalFacturacionComplementaria(FacturaCompuesta facturaCompuesta)
        {
            bool retorno = true;
            var estadoCuenta = facturaCompuesta.EstadoCuentaEncabezado;
            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;
            var conceptosCobro = this.FiltrarConceptoCobroActualizar(estadoCuenta.FacturaAtencion);

            try
            {
                foreach (var item in conceptosCobro)
                {
                    item.NumeroFactura = encabezadoFactura.NumeroFactura;
                    this.ActualizarConceptosCobro(item);
                }

                var movimientosTesoreria = this.FiltrarMovimientoTesoreriaActualizar(estadoCuenta.FacturaAtencion);

                foreach (var item in movimientosTesoreria)
                {
                    this.ActualizarMovimientosTesoreria(item);
                }

                foreach (var item in encabezadoFactura.VentasFactura)
                {
                    this.GuardarInformacionVentaFactura(item, estadoCuenta.NumeroFacturaSinPrefijo);
                }

                if (estadoCuenta.TipoFacturacion == TipoFacturacion.FacturacionPaquete)
                {
                    if (estadoCuenta.FacturaPaquetes != null)
                    {
                        this.GuardarInformacionPaquetesFactura(encabezadoFactura, estadoCuenta, facturaCompuesta);
                    }
                }
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Realiza los subprocesos complementarios del proceso de Anulación de Facturas No Clínicas
        /// </summary>
        /// <param name="notaCredito">Entidad que contiene los parámetros de una nota crédito</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 06/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ProcesoComplementarioAnulacionNC(NotaCredito notaCredito)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            fachada.ProcesoComplementarioAnulacionNC(notaCredito);
        }

        /// <summary>
        /// Inicia el proceso de validaci n para Facturaci n por Actividades.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <returns>
        /// Encabezado del estado de cuenta.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private EstadoCuentaEncabezado ProcesoFacturaGeneralAtencion(ProcesoFactura procesoFactura, FacturaAtencion atencion, Vinculacion vinculacion, out List<FacturaAtencionDetalle> detalleVinculacion)
        {
            EstadoCuentaEncabezado estadoCuenta = null;

            // Obtiene las condiciones de proceso
            var condiciones = this.CargarCondicionesProceso(procesoFactura);

            // Obtiene las condiciones de factura
            CondicionTarifa condicionFactura = this.ObtenerCondicionFactura(procesoFactura, atencion.IdAtencion, vinculacion);

            // Obtiene las condiciones de contrato
            this.CodigoEntidad = procesoFactura.CodigoEntidad;
            var condicionContrato = this.CargueInformacionCondicionContrato(procesoFactura);

            // Define las ventas no marcadas
            this.VentasNoMarcadas(procesoFactura, atencion);

            // Define las exclusiones no marcadas
            this.OmitirExclusionesNoMarcadas(procesoFactura, condiciones);
            Debug.WriteLine("Inicia Ejecucion {0}", DateTime.Now);
            try
            {
                // Buscar tercero venta componente
                //// Se consulta una sola vez por que el numero de atencion es el mismo siempre
                if (this.gblResponsablesComponente == null)
                {
                    this.gblResponsablesComponente = this.ConsultarTerceroComponente(atencion.IdAtencion);
                }

                List<VentaResponsable> responsablesComponente = this.gblResponsablesComponente;
                this.CargarDetallesFactura(atencion, procesoFactura.IdProceso, responsablesComponente);

                // Omitir productos que no tienen venta
                atencion.Detalle.RemoveAll(item => item.DetalleVenta == null);

                Debug.WriteLine("Finaliza Ejecucion {0}", DateTime.Now);

                estadoCuenta = this.ConsultarEstadoCuentaEncabezadoMultiple(new EstadoCuentaEncabezado()
                {
                    IdProceso = procesoFactura.IdProceso,
                    IdUsuarioFirma = procesoFactura.IdUsuarioFirma,
                    IdContrato = vinculacion.Contrato.Id,
                    IdAtencion = vinculacion.IdAtencion,
                    IdPlan = vinculacion.Plan.Id
                });

                if (procesoFactura.Responsable != null)
                {
                    estadoCuenta.Responsable = procesoFactura.Responsable;
                }

                estadoCuenta.Observaciones = procesoFactura.Observacion;
                estadoCuenta.FacturaAtencion = new List<FacturaAtencion>();
                estadoCuenta.FacturaAtencion.Add(atencion);

                if (procesoFactura.TipoFactura != TipoFacturacion.FacturacionPaquete && procesoFactura.TipoFactura != TipoFacturacion.FacturacionActividad)
                {
                    estadoCuenta.TipoFacturacion = procesoFactura.TipoFactura;
                    this.ActualizacionPagos(estadoCuenta.FacturaAtencion, estadoCuenta);
                    estadoCuenta.TotalPagos = this.CalcularPagosRealizados(estadoCuenta.FacturaAtencion);
                }

                // Configuración de paquetes
                if (procesoFactura.TipoFactura == TipoFacturacion.FacturacionPaquete && vinculacion.Orden == 1)
                {
                    if (procesoFactura.Paquetes != null && procesoFactura.Paquetes.Count() != 0)
                    {
                        estadoCuenta.FacturaPaquetes = new List<Paquete>();
                        estadoCuenta.FacturaPaquetes = this.CalcularValoresPaquetes(procesoFactura.IdProceso, procesoFactura.Paquetes, condicionContrato, atencion, condiciones, condicionFactura);
                        this.AsociarProductoPaquete(procesoFactura.IdProceso, atencion, estadoCuenta.FacturaPaquetes, condicionContrato);
                    }
                }

                this.ValidarProcesoGeneralAtencion(procesoFactura.TipoFactura, atencion, condiciones, condicionContrato, condicionFactura, out detalleVinculacion, vinculacion);

                if (estadoCuenta.FacturaPaquetes != null && vinculacion.Orden == 1)
                {
                    foreach (Paquete paq in estadoCuenta.FacturaPaquetes)
                    {
                        decimal valorProductosPaquetes = 0;
                        decimal valorDescuentoPyG = 0;
                        decimal valorPyG = 0;
                        int cont = 0;

                        foreach (FacturaAtencionDetalle det in atencion.Detalle)
                        {
                            foreach (PaqueteProducto p in paq.Productos)
                            {
                                if (det.IdProducto == p.IdProducto && det.NumeroVenta == p.NumeroVenta && det.idPaquete == p.IdPaquete)
                                {
                                    if (det.esPaquete && !det.Excluido)
                                    {
                                        valorProductosPaquetes += p.ValorPaqueteProducto * p.CantidadAsignada;
                                        cont += 1;
                                    }

                                    break;
                                }
                            }
                        }

                        decimal valorPaquete = 0;

                        valorPaquete = paq.ProductoPaquete.ValorUnitario;

                        valorPyG = valorPaquete - valorProductosPaquetes;

                        decimal valorDescuento = 0;

                        if (paq.ProductoPaquete.Descuentos.Count != 0)
                        {
                            valorDescuento = paq.ProductoPaquete.Descuentos[0].ValorDescuento;
                        }

                        valorDescuentoPyG = this.Redondeo((valorDescuento * valorPyG) / 100, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);

                        foreach (FacturaAtencionDetalle det in atencion.Detalle)
                        {
                            if (det.esPaquete && !det.Excluido)
                            {
                                if (det.Descuentos == null)
                                {
                                    det.Descuentos = new List<Descuento>();
                                    det.Descuentos.Add(new Descuento());
                                }

                                if (det.Descuentos.Count != 0)
                                {
                                    det.Descuentos[0].ValorDescuento = valorDescuento;
                                }

                                det.ValorDescuento = this.Redondeo((det.ValorUnitario * det.CantidadFacturar) * valorDescuento / 100, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);
                                det.ValorTotalDescuento = det.ValorDescuento;
                                det.ValorTotal = this.Redondeo((det.ValorBruto * det.CantidadFacturar) - det.ValorDescuento, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);
                            }
                        }

                        FacturaAtencionDetalle detallePyG = new FacturaAtencionDetalle();

                        detallePyG.Orden = 1;
                        detallePyG.CodigoGrupo = "PYG";
                        detallePyG.CodigoProducto = "PYG";
                        detallePyG.NombreProducto = "PYG PYG - PAQUETES - " + paq.ProductoPaquete.NombreProducto;
                        detallePyG.Componente = "NA";
                        detallePyG.Excluido = false;
                        detallePyG.FechaVenta = DateTime.Now;
                        detallePyG.IdAtencion = estadoCuenta.IdAtencion;
                        detallePyG.NumeroVenta = paq.ProductoPaquete.NumeroVenta;
                        detallePyG.CantidadProducto = paq.ProductoPaquete.CantidadProducto;
                        detallePyG.ValorRecargo = paq.ProductoPaquete.ValorRecargo;
                        detallePyG.ValorTotalRecargo = paq.ProductoPaquete.ValorRecargo;
                        detallePyG.ValorDescuento = valorDescuentoPyG;
                        detallePyG.ValorTotalDescuento = valorDescuentoPyG;
                        detallePyG.ValorProductos = valorPyG;
                        detallePyG.idPaquete = paq.IdPaquete;
                        detallePyG.esPaquete = true;
                        detallePyG.ValorUnitario = valorPyG;
                        detallePyG.IdPlan = estadoCuenta.IdPlan;
                        detallePyG.ValorTotal = valorPyG + paq.ProductoPaquete.ValorRecargo - valorDescuentoPyG;
                        detallePyG.ValorPaquete = valorPyG;
                        detallePyG.IndPaquete = 0;

                        atencion.Detalle.Add(detallePyG);
                    }
                }

                return estadoCuenta;
            }
            catch (Exception ex)
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
                throw ex;
            }
        }

        /// <summary>
        /// Inicia el proceso de validaci n para Facturaci n por Actividades.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <returns>
        /// Encabezado del estado de cuenta.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private EstadoCuentaEncabezado ProcesoFacturaGeneralAtencionPaquetes(ProcesoFactura procesoFactura, FacturaAtencion atencion, Vinculacion vinculacion, out List<FacturaAtencionDetalle> detalleVinculacion)
        {
            EstadoCuentaEncabezado estadoCuenta = null;

            // Obtiene las condiciones de proceso
            var condiciones = this.CargarCondicionesProceso(procesoFactura);

            // Obtiene las condiciones de factura
            CondicionTarifa condicionFactura = this.ObtenerCondicionFactura(procesoFactura, atencion.IdAtencion, vinculacion);

            // Obtiene las condiciones de contrato
            this.CodigoEntidad = procesoFactura.CodigoEntidad;
            var condicionContrato = this.CargueInformacionCondicionContrato(procesoFactura);

            // Define las ventas no marcadas
            this.VentasNoMarcadas(procesoFactura, atencion);

            // Define las exclusiones no marcadas
            this.OmitirExclusionesNoMarcadas(procesoFactura, condiciones);
            Debug.WriteLine("Inicia Ejecucion {0}", DateTime.Now);
            try
            {
                // Buscar tercero venta componente
                //// Se consulta una sola vez por que el numero de atencion es el mismo siempre
                if (this.gblResponsablesComponente == null)
                {
                    this.gblResponsablesComponente = this.ConsultarTerceroComponente(atencion.IdAtencion);
                }

                List<VentaResponsable> responsablesComponente = this.gblResponsablesComponente;
                this.CargarDetallesFactura(atencion, procesoFactura.IdProceso, responsablesComponente);

                // Omitir productos que no tienen venta
                atencion.Detalle.RemoveAll(item => item.DetalleVenta == null);

                Debug.WriteLine("Finaliza Ejecucion {0}", DateTime.Now);

                estadoCuenta = this.ConsultarEstadoCuentaEncabezadoMultiple(new EstadoCuentaEncabezado()
                {
                    IdProceso = procesoFactura.IdProceso,
                    IdUsuarioFirma = procesoFactura.IdUsuarioFirma,
                    IdContrato = vinculacion.Contrato.Id,
                    IdAtencion = vinculacion.IdAtencion,
                    IdPlan = vinculacion.Plan.Id
                });

                if (procesoFactura.Responsable != null)
                {
                    estadoCuenta.Responsable = procesoFactura.Responsable;
                }

                estadoCuenta.Observaciones = procesoFactura.Observacion;
                estadoCuenta.FacturaAtencion = new List<FacturaAtencion>();
                estadoCuenta.FacturaAtencion.Add(atencion);

                if (procesoFactura.TipoFactura != TipoFacturacion.FacturacionPaquete && procesoFactura.TipoFactura != TipoFacturacion.FacturacionActividad)
                {
                    estadoCuenta.TipoFacturacion = procesoFactura.TipoFactura;
                    this.ActualizacionPagos(estadoCuenta.FacturaAtencion, estadoCuenta);
                    estadoCuenta.TotalPagos = this.CalcularPagosRealizados(estadoCuenta.FacturaAtencion);
                }

                // Configuración de paquetes
                if (procesoFactura.TipoFactura == TipoFacturacion.FacturacionPaquete && vinculacion.Orden == 1)
                {
                    if (procesoFactura.Paquetes != null && procesoFactura.Paquetes.Count() != 0)
                    {
                        estadoCuenta.FacturaPaquetes = new List<Paquete>();
                        estadoCuenta.FacturaPaquetes = this.CalcularValoresPaquetes(procesoFactura.IdProceso, procesoFactura.Paquetes, condicionContrato, atencion, condiciones, condicionFactura);
                        this.AsociarProductoPaquete(procesoFactura.IdProceso, atencion, estadoCuenta.FacturaPaquetes, condicionContrato);
                    }
                }

                this.ValidarProcesoGeneralAtencionPaquetes(procesoFactura.TipoFactura, atencion, condiciones, condicionContrato, condicionFactura, out detalleVinculacion, vinculacion);

                if (estadoCuenta.FacturaPaquetes != null && vinculacion.Orden == 1)
                {
                    foreach (Paquete paq in estadoCuenta.FacturaPaquetes)
                    {
                        decimal valorProductosPaquetes = 0;
                        decimal valorDescuentoPyG = 0;
                        decimal valorPyG = 0;
                        int cont = 0;

                        foreach (FacturaAtencionDetalle det in atencion.Detalle)
                        {
                            foreach (PaqueteProducto p in paq.Productos)
                            {
                                if (det.IdProducto == p.IdProducto && det.NumeroVenta == p.NumeroVenta && det.idPaquete == p.IdPaquete)
                                {
                                    if (det.esPaquete && !det.Excluido)
                                    {
                                        valorProductosPaquetes += det.ValorUnitario * det.CantidadFacturar;
                                        cont += 1;
                                    }

                                    break;
                                }
                            }
                        }

                        decimal valorPaquete = 0;

                        valorPaquete = paq.ProductoPaquete.ValorUnitario;

                        valorPyG = valorPaquete - valorProductosPaquetes;

                        paq.PerdidaGanancia.ValorPaquetePG = valorPyG;

                        decimal valorDescuento = 0;

                        if ((paq.ProductoPaquete.Descuentos != null) && (paq.ProductoPaquete.Descuentos.Count != 0))
                        {
                            valorDescuento = paq.ProductoPaquete.Descuentos[0].ValorDescuento;
                        }

                        valorDescuentoPyG = this.Redondeo((valorDescuento * valorPyG) / 100, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);

                        foreach (FacturaAtencionDetalle det in atencion.Detalle)
                        {
                            if (det.esPaquete && !det.Excluido)
                            {
                                if (det.Descuentos == null)
                                {
                                    det.Descuentos = new List<Descuento>();
                                    det.Descuentos.Add(new Descuento());
                                }

                                if (det.Descuentos.Count != 0)
                                {
                                    det.Descuentos[0].ValorDescuento = valorDescuento;
                                }

                                // det.ValorDescuento = this.Redondeo((det.ValorUnitario * det.CantidadFacturar) * valorDescuento / 100, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);
                                // det.ValorTotalDescuento = det.ValorDescuento;
                                // det.ValorTotal = this.Redondeo((det.ValorBruto * det.CantidadFacturar) - det.ValorDescuento, estadoCuenta.IdContrato, estadoCuenta.FacturaAtencion[0].IdManual);
                                det.ValorDescuento = (det.ValorUnitario * det.CantidadFacturar) * valorDescuento / 100;
                                det.ValorTotalDescuento = det.ValorDescuento;
                                det.ValorTotal = (det.ValorBruto * det.CantidadFacturar) - det.ValorDescuento;

                                if (det.VentaComponentes != null && det.VentaComponentes.Count > 0)
                                {
                                    foreach (VentaComponente item in det.VentaComponentes)
                                    {
                                        item.ValorDescuento = (item.ValorUnitario * item.CantidadFacturar) * valorDescuento / 100;
                                        item.ValorTotalDescuento = item.ValorDescuento;
                                        item.ValorTotal = (item.ValorBruto * item.CantidadFacturar) - item.ValorDescuento;
                                    }
                                }
                            }
                        }

                        FacturaAtencionDetalle detallePyG = new FacturaAtencionDetalle();

                        detallePyG.Orden = 1;
                        detallePyG.NombreGrupo = "PYG - PAQUETES - " + paq.ProductoPaquete.NombreProducto;
                        detallePyG.CodigoGrupo = "PYG";
                        detallePyG.CodigoProducto = "PYG";
                        detallePyG.NombreProducto = "PYG - PAQUETES - " + paq.ProductoPaquete.NombreProducto;
                        detallePyG.Componente = "NA";
                        detallePyG.Excluido = false;
                        detallePyG.FechaVenta = DateTime.Now;
                        detallePyG.IdAtencion = estadoCuenta.IdAtencion;
                        detallePyG.NumeroVenta = paq.ProductoPaquete.NumeroVenta;
                        detallePyG.CantidadProducto = paq.ProductoPaquete.CantidadProducto;
                        detallePyG.ValorRecargo = paq.ProductoPaquete.ValorRecargo;
                        detallePyG.ValorTotalRecargo = paq.ProductoPaquete.ValorRecargo;
                        detallePyG.ValorDescuento = valorDescuentoPyG;
                        detallePyG.ValorTotalDescuento = valorDescuentoPyG;
                        detallePyG.ValorProductos = valorPyG;
                        detallePyG.idPaquete = paq.IdPaquete;
                        detallePyG.esPaquete = true;
                        detallePyG.ValorUnitario = valorPyG;
                        detallePyG.IdPlan = estadoCuenta.IdPlan;
                        detallePyG.ValorTotal = valorPyG + paq.ProductoPaquete.ValorRecargo - valorDescuentoPyG;
                        detallePyG.ValorPaquete = valorPyG;
                        detallePyG.IndPaquete = 0;

                        atencion.Detalle.Add(detallePyG);
                    }
                }

                return estadoCuenta;
            }
            catch (Exception ex)
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
                throw ex;
            }
        }

        /// <summary>
        /// Inicia el proceso de validacion para Facturacion No Clinica.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jos  Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 06/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private EstadoCuentaEncabezado ProcesoFacturaNoClinica(ProcesoFactura procesoFactura)
        {
            EstadoCuentaEncabezado estadoCuenta = null;
            var atenciones = this.ConsultarProductosNC(new FacturaAtencion() { IdProceso = procesoFactura.IdProceso });

            this.CodigoEntidad = procesoFactura.CodigoEntidad;

            procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Procesando;
            this.ActualizarEstadoProcesoFacturaNC(procesoFactura);
            Debug.WriteLine("Inicia Ejecucion {0}", DateTime.Now);
            try
            {
                foreach (var atencionT in atenciones)
                {
                    foreach (var detalle in atencionT.Detalle)
                    {
                        detalle.Descuentos = new List<Descuento>();

                        DetalleTarifa detTar = new DetalleTarifa();
                        detTar.IdProducto = Convert.ToInt32(detalle.IdProducto);
                        detTar.CodigoUnidad = String.Empty;
                        detTar.IdManual = atencionT.IdManual;
                        detTar.FechaInicial = DateTime.Now;

                        detalle.DetalleTarifa = detTar;
                    }
                }

                this.CargarDetallesFacturaNC(ref atenciones, procesoFactura.IdProceso);

                Debug.WriteLine("Finaliza Ejecucion {0}", DateTime.Now);
                estadoCuenta = this.ConsultarEstadoCuentaEncabezadoNC(new EstadoCuentaEncabezado()
                {
                    IdProceso = procesoFactura.IdProceso,
                    IdUsuarioFirma = procesoFactura.IdUsuarioFirma
                });

                estadoCuenta.Observaciones = procesoFactura.Observacion;
                estadoCuenta.FacturaAtencion = atenciones;
                estadoCuenta.TipoFacturacion = procesoFactura.TipoFactura;
                this.ActualizacionPagos(estadoCuenta.FacturaAtencion, estadoCuenta);
                estadoCuenta.TotalPagos = this.CalcularPagosRealizados(estadoCuenta.FacturaAtencion);
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Finalizado;
                this.ActualizarEstadoProcesoFacturaNC(procesoFactura);

                return estadoCuenta;
            }
            catch (Exception ex)
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                this.ActualizarEstadoProcesoFacturaNC(procesoFactura);
                throw ex;
            }
        }

        /// <summary>
        /// Inicia el proceso de validaci n para Facturaci n por Relaci n.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Encabezado del estado de cuenta.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private EstadoCuentaEncabezado ProcesoFacturaRelacion(ProcesoFactura procesoFactura)
        {
            CondicionContrato condicionContratoBase;
            EstadoCuentaEncabezado estadoCuenta = null;

            bool porcentaje = false;
            var condiciones = this.CargarCondicionesProceso(procesoFactura);
            var condicionContrato = this.CargueInformacionCondicionContrato(procesoFactura);
            var atenciones = this.ConsultarAtencionesPendientesxProcesar(new FacturaAtencion() { IdProceso = procesoFactura.IdProceso });

            procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Procesando;

            this.ActualizarEstadoProcesoFactura(procesoFactura);

            Debug.WriteLine("Inicia Ejecucion {0}", DateTime.Now);

            using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 15, 0)))
            {
                try
                {
                    foreach (var atencion in atenciones)
                    {
                        foreach (var detalle in atencion.Detalle)
                        {
                            // Obtener valor de la venta
                            if (detalle.ValorUnitario == 0)
                            {
                                detalle.ValorUnitario = detalle.ValorOriginal;
                            }

                            condicionContratoBase = condicionContrato;

                            this.RegistrarHomologacion(detalle, condicionContrato, Tipo.Producto);

                            if (this.AplicarReglaExclusionContrato(condiciones, atencion, detalle))
                            {
                                detalle.Excluido = true;
                                continue;
                            }
                            else if (this.AplicarReglaExclusionManual(condiciones, atencion, detalle, TipoFacturacion.FacturacionRelacion))
                            {
                                detalle.Excluido = true;
                                continue;
                            }

                            var condicionTarifa = this.AplicarReglaCondicionTarifa(condiciones, atencion, detalle, TipoFacturacion.FacturacionRelacion).FirstOrDefault();
                            condicionContrato = this.EvaluarCondicionContratoManuales(condicionContrato, condicionTarifa);

                            porcentaje = condicionTarifa != null ? condicionTarifa.Porcentaje : false;

                            if (condicionTarifa == null || porcentaje)
                            {
                                detalle.DetalleTarifa = this.AplicarAjusteTarifa(condicionContrato, detalle, TipoFacturacion.FacturacionRelacion, condicionTarifa, porcentaje);
                            }

                            if (detalle.DetalleTarifa != null)
                            {
                                detalle.DetalleTarifa.ValorTarifa = this.CalcularValorUnitario(detalle.DetalleTarifa, condicionContrato, detalle);
                            }

                            bool recargoAplicado = this.AplicarReglaRecargosContrato(condiciones, atencion, detalle);

                            if (!recargoAplicado)
                            {
                                this.AplicarReglaRecargosManual(condiciones, atencion, detalle);
                            }

                            this.AplicarReglaDescuentos(condiciones, atencion, detalle);
                            this.AplicarCalculosRel(detalle, condicionContrato);
                            condicionContrato = condicionContratoBase;
                        }
                    }

                    this.CargarDetallesFactura(atenciones, procesoFactura.IdProceso);

                    Debug.WriteLine("Finaliza Ejecucion {0}", DateTime.Now);
                    estadoCuenta = this.ConsultarEstadoCuentaEncabezado(new EstadoCuentaEncabezado()
                    {
                        IdProceso = procesoFactura.IdProceso,
                        IdUsuarioFirma = procesoFactura.IdUsuarioFirma
                    });

                    estadoCuenta.Observaciones = procesoFactura.Observacion;
                    estadoCuenta.FacturaAtencion = atenciones;
                    estadoCuenta.TipoFacturacion = procesoFactura.TipoFactura;
                    this.ActualizacionPagos(estadoCuenta.FacturaAtencion, estadoCuenta);
                    estadoCuenta.TotalPagos = this.CalcularPagosRealizados(estadoCuenta.FacturaAtencion);
                    procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Finalizado;
                    this.ActualizarEstadoProcesoFactura(procesoFactura);
                    transaccion.Complete();

                    return estadoCuenta;
                }
                catch (Exception ex)
                {
                    transaccion.Dispose();
                    procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                    this.ActualizarEstadoProcesoFactura(procesoFactura);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Registra la Homologaci n.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="tipo">Parametro tipo.</param>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 28/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void RegistrarHomologacion(BaseValidacion baseValidacion, CondicionContrato condicionContrato, Tipo tipo)
        {
            if (condicionContrato.HomologacionProductos != null)
            {
                var productoHomologado = this.HomologacionProducto(baseValidacion, condicionContrato.HomologacionProductos);

                if (productoHomologado == null)
                {
                    throw new Excepcion.Negocio(string.Format(ReglasNegocio.HomologacionProducto_SinDato, baseValidacion.IdProducto, baseValidacion.IdAtencion));
                }

                if (tipo == Tipo.Componente)
                {
                    baseValidacion.NombreComponente = productoHomologado.NombreProducto + "\r\n" + "  " + baseValidacion.NombreResponsableHonorario;
                    (baseValidacion as VentaComponente).ComponenteHomologado = productoHomologado.CodigoProducto;
                }

                baseValidacion.CodigoProducto = productoHomologado.CodigoProducto;
                baseValidacion.NombreProducto = productoHomologado.NombreProducto;
                baseValidacion.CodigoGrupo = productoHomologado.CodigoGrupo;
                baseValidacion.NombreGrupo = productoHomologado.NombreGrupo;
            }
        }

        /// <summary>
        /// Validar Reglas Condiciones de Cubrimiento.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <param name="componenteVinculacion">The componente vinculacion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ReglasCondicionesCubrimiento(FacturaAtencion atencion, BaseValidacion baseValidacion, List<FacturaAtencionDetalle> detalleVinculacion, ref List<VentaComponente> componenteVinculacion, FacturaAtencionDetalle detalle)
        {
            BaseValidacion restante = null;
            var cubrimiento = this.FiltrarCondicionCubrimiento(atencion.Cubrimientos, baseValidacion.Cubrimiento);
            switch (cubrimiento.CondicionesCubrimiento.TipoCondicionCubrimiento)
            {
                case TipoCubrimiento.ValorMaximo:
                    restante = this.SepararValorCubrimiento(baseValidacion, cubrimiento);
                    break;

                case TipoCubrimiento.ValorMaxPorcentaje:
                    restante = this.SepararPorcentajeCubrimiento(baseValidacion, cubrimiento);
                    break;

                case TipoCubrimiento.Cantidad:
                    restante = this.SepararCantidadCubrimiento(baseValidacion, cubrimiento);
                    break;
            }

            if (restante is FacturaAtencionDetalle && restante != null)
            {
                detalleVinculacion.Add(restante as FacturaAtencionDetalle);

                if (baseValidacion.CantidadFacturar == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }
            else if (restante is VentaComponente && restante != null)
            {
                if (componenteVinculacion == null)
                {
                    componenteVinculacion = new List<VentaComponente>();
                }

                componenteVinculacion.Add(restante as VentaComponente);
                if (baseValidacion.CantidadFacturar == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }
        }

        /// <summary>
        /// Reglas Condiciones Tarifa Cantidad Valor.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <param name="componenteVinculacion">The componente vinculacion.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Reglas Condiciones Tarifa Cantidad Valor.
        /// </remarks>
        private void ReglasCondicionesTarifaCantidadValor(FacturaAtencion atencion, BaseValidacion baseValidacion, List<FacturaAtencionDetalle> detalleVinculacion, ref List<VentaComponente> componenteVinculacion)
        {
            BaseValidacion restante = null;
            if (atencion.CondicionesTarifa == null)
            {
                atencion.CondicionesTarifa = new List<CondicionTarifa>();
            }

            foreach (CondicionTarifa item in baseValidacion.CondicionesTarifa)
            {
                var tarifa = this.FiltrarCondicionTarifa(atencion.CondicionesTarifa, item);

                switch (tarifa.TipoCondicionTarifa)
                {
                    case TipoTarifa.ValorMaximo:
                        restante = this.SepararValorMaximoTarifa(baseValidacion, tarifa);
                        break;

                    case TipoTarifa.ValorMaxPorcentaje:
                        restante = this.SepararPorcentajeMaximoTarifa(baseValidacion, tarifa);
                        break;

                    case TipoTarifa.Cantidad:
                        restante = this.SepararCantidadTarifa(baseValidacion, tarifa);
                        break;
                }
            }

            if (restante is FacturaAtencionDetalle && restante != null)
            {
                detalleVinculacion.Add(restante as FacturaAtencionDetalle);
                if (baseValidacion.CantidadFacturar == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }
            else if (restante is VentaComponente && restante != null)
            {
                if (componenteVinculacion == null)
                {
                    componenteVinculacion = new List<VentaComponente>();
                }

                componenteVinculacion.Add(restante as VentaComponente);
                if (baseValidacion.CantidadFacturar == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }
        }

        /// <summary>
        /// Aplicar reglas de condiciones de tarifa.
        /// </summary>
        /// <param name="tipo">Parametro tipo.</param>
        /// <param name="condicionContrato">Parametro condicion contrato.</param>
        /// <param name="condiciones">Parametro condiciones.</param>
        /// <param name="atencion">Parametro atencion.</param>
        /// <param name="baseValidacion">Parametro base validacion.</param>
        /// <param name="tipoFacturacion">Parametro tipo facturacion.</param>
        /// <returns>
        /// Base Validacion.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 20/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion ReglasCondicionesTarifaCubrimiento(Tipo tipo, ref CondicionContrato condicionContrato, List<CondicionProceso> condiciones, FacturaAtencion atencion, BaseValidacion baseValidacion, TipoFacturacion tipoFacturacion)
        {
            CondicionTarifa ajusteTarifa = null;
            CondicionTarifa cantidad = null;
            CondicionTarifa valorPropio = null;
            var condicionesTarifas = this.AplicarReglaCondicionTarifa(condiciones, atencion, baseValidacion, tipoFacturacion).CopiarObjeto();
            var condicionesTarifaActual = condicionesTarifas.CopiarObjeto();
            BaseValidacion productoSiguienteVinculacion = null;
            foreach (var item in condicionesTarifaActual)
            {
                switch (item.TipoCondicionTarifa)
                {
                    case TipoTarifa.NoCubrimiento:
                        ajusteTarifa = this.ValidarCondicionesAjusteTarifa(condicionesTarifaActual, TipoTarifa.AjusteTarifas);
                        cantidad = this.ValidarCondicionesAjusteTarifa(condicionesTarifaActual, TipoTarifa.Cantidad);

                        if (ajusteTarifa != null || cantidad != null)
                        {
                            condicionesTarifas.Remove(item);
                        }
                        else
                        {
                            condicionesTarifas.Clear();

                            if (!baseValidacion.EsPaquete)
                            {
                                productoSiguienteVinculacion = baseValidacion;
                                baseValidacion.CondicionSeparacion.NoCubrimiento = true;
                            }
                        }

                        break;

                    case TipoTarifa.AjusteTarifas:
                        valorPropio = this.ValidarCondicionesAjusteTarifa(condicionesTarifaActual, TipoTarifa.ValorPropio);
                        ajusteTarifa = this.ValidarCondicionesAjusteTarifa(condicionesTarifaActual, TipoTarifa.AjusteTarifas);

                        if (valorPropio != null)
                        {
                            var mensaje = string.Format("El producto {0} - {1} tiene configurado un ajuste de tarifa y un valor propio", baseValidacion.IdProducto, baseValidacion.NombreProducto);
                            Exception ex = new Exception(mensaje);
                            throw ex;
                        }
                        else
                        {
                            condicionContrato = this.EvaluarCondicionContratoManuales(condicionContrato, ajusteTarifa);
                            baseValidacion.DetalleTarifa = this.AplicarAjusteTarifa(condicionContrato, baseValidacion, tipoFacturacion, ajusteTarifa, true);
                        }

                        break;

                    case TipoTarifa.ValorPropio:
                        valorPropio = this.ValidarCondicionesAjusteTarifa(condicionesTarifaActual, TipoTarifa.ValorPropio);

                        if (valorPropio != null && tipo == Tipo.Producto && (baseValidacion as FacturaAtencionDetalle).VentaComponentes.Count > 0)
                        {
                            var mensaje = string.Format("El producto {0} tiene configurado un valor propio, y tiene componentes asociados", baseValidacion.IdProducto);
                            throw new Excepcion.Negocio(mensaje);
                        }
                        else
                            if (valorPropio == null)
                            {
                                baseValidacion.DetalleTarifa = this.AplicarAjusteTarifa(condicionContrato, baseValidacion, tipoFacturacion, null, true);
                            }

                        break;
                }
            }

            baseValidacion.CondicionesTarifa.Clear();
            baseValidacion.CondicionesTarifa = condicionesTarifas;
            if (baseValidacion.CondicionSeparacion.NoCubrimiento == false &&
                (condicionesTarifas == null || condicionesTarifas.Count == 0))
            {
                baseValidacion.DetalleTarifa = this.AplicarAjusteTarifa(condicionContrato, baseValidacion, tipoFacturacion, null, true);
            }

            return productoSiguienteVinculacion;
        }

        /// <summary>
        /// Relaciona las Atenciones con el Detalle de la Venta.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Retorna Atenciones.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<FacturaAtencion> RelacionDetallexAtencion(int numeroFactura)
        {
            var atenciones = this.ConsultarFacturaDetalle(numeroFactura);
            var detallesFactura = this.ConsultarFacturaDetallexNumeroFactura(numeroFactura);
            var clientes = this.ConsultarDetalleClienteFactura(numeroFactura);
            var detallesVentas = this.ConsultarDetalleVentaFactura(numeroFactura);
            var componentes = this.ConsultarFacturaComponentes(numeroFactura);
            var paquetes = this.ConsultarFacturaDetallePaquetes(numeroFactura);
            List<Paquete> listaPaquetesFacturados = new List<Paquete>();
            if (paquetes != null && paquetes.Count() > 0)
            {
                foreach (var paq in paquetes)
                {
                    if (listaPaquetesFacturados.Where(c => c.IdPaquete == paq.IdPaquete).Count() == 0)
                    {
                        Paquete paquete = new Paquete()
                        {
                            IdPaquete = paq.IdPaquete,
                            NumeroFactura = paq.NumeroFactura,
                            ValorPaquete = this.Redondeo(paq.ValorPaquete, atenciones[0].IdContrato, atenciones[0].IdManual),
                            ValorDescuento = this.Redondeo((paq.ValorDescuento * paq.ValorPaquete) / 100, atenciones[0].IdContrato, atenciones[0].IdManual),
                            Productos = this.ConsultarFacturaDetallePaquetes(numeroFactura).Where(d => d.IdPaquete == paq.IdPaquete).ToList()
                        };

                        listaPaquetesFacturados.Add(paquete);
                    }
                }
            }

            foreach (var atencion in atenciones)
            {
                atencion.Cliente = this.BuscarClientexIdCliente(clientes, atencion.IdCliente);

                atencion.Detalle = (from
                                        item in detallesFactura
                                    where
                                        item.IdAtencion == atencion.IdAtencion
                                    select
                                        item).ToList();

                foreach (var detalle in atencion.Detalle)
                {
                    detalle.DetalleVenta = (from
                                                item in detallesVentas
                                            where
                                                item.IdAtencion == atencion.IdAtencion
                                                && item.NumeroVenta == detalle.NumeroVenta
                                                && item.IdTransaccion == detalle.IdTransaccion
                                                && item.IdProducto == detalle.IdProducto
                                            select
                                                item).FirstOrDefault();

                    detalle.VentaComponentes = this.BuscarComponenteVenta(componentes, detalle);
                }
            }

            return atenciones;
        }

        /// <summary>
        /// Relaciona las Atenciones con el Detalle de la Venta.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Retorna Atenciones.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private List<Paquete> RelacionDetallexPaquete(int numeroFactura)
        {
            var atenciones = this.ConsultarFacturaDetalle(numeroFactura);
            var detallesFactura = this.ConsultarFacturaDetallexNumeroFactura(numeroFactura);
            var clientes = this.ConsultarDetalleClienteFactura(numeroFactura);
            var detallesVentas = this.ConsultarDetalleVentaFactura(numeroFactura);
            var componentes = this.ConsultarFacturaComponentes(numeroFactura);
            var paquetes = this.ConsultarFacturaDetallePaquetes(numeroFactura);
            List<Paquete> listaPaquetesFacturados = new List<Paquete>();
            if (paquetes != null && paquetes.Count() > 0)
            {
                foreach (var paq in paquetes)
                {
                    if (listaPaquetesFacturados.Where(c => c.IdPaquete == paq.IdPaquete).Count() == 0)
                    {
                        Paquete paquete = new Paquete()
                        {
                            IdPaquete = paq.IdPaquete,
                            NumeroFactura = paq.NumeroFactura,
                            ValorPaquete = this.Redondeo(paq.ValorPaquete, atenciones[0].IdContrato, atenciones[0].IdManual),
                            ValorDescuento = this.Redondeo((paq.ValorDescuento * paq.ValorPaquete) / 100, atenciones[0].IdContrato, atenciones[0].IdManual),
                            Productos = this.ConsultarFacturaDetallePaquetes(numeroFactura).Where(d => d.IdPaquete == paq.IdPaquete).ToList(),
                            PerdidaGanancia = new PaqueteEncabezado()
                            {
                                IdPaquete = paq.IdPaquete,
                                NombrePaquete = paq.NombrePaquete,
                                NombrePG = paq.NombrePG,
                                IndHabilitado = paq.IndHabilitado,
                                ValorPaquetePG = paq.ValorPG,
                                CodigoPaquete = paq.CodigoPaquete,
                                CodigoPG = paq.CodigoPG
                            }
                        };

                        listaPaquetesFacturados.Add(paquete);
                    }
                }
            }

            foreach (var atencion in atenciones)
            {
                atencion.Cliente = this.BuscarClientexIdCliente(clientes, atencion.IdCliente);

                atencion.Detalle = (from
                                        item in detallesFactura
                                    where
                                        item.IdAtencion == atencion.IdAtencion
                                    select
                                        item).ToList();

                foreach (var detalle in atencion.Detalle)
                {
                    detalle.DetalleVenta = (from
                                                item in detallesVentas
                                            where
                                                item.IdAtencion == atencion.IdAtencion
                                                && item.NumeroVenta == detalle.NumeroVenta
                                                && item.IdTransaccion == detalle.IdTransaccion
                                                && item.IdProducto == detalle.IdProducto
                                            select
                                                item).FirstOrDefault();

                    detalle.VentaComponentes = this.BuscarComponenteVenta(componentes, detalle);
                }

                if (listaPaquetesFacturados != null && listaPaquetesFacturados.Count() > 0)
                {
                    atencion.Paquetes = new List<Paquete>();

                    foreach (var encabezadoPaquete in listaPaquetesFacturados)
                    {
                        foreach (var paquete in encabezadoPaquete.Productos)
                        {
                            var detalleFactura = new FacturaAtencionDetalle()
                            {
                                IdProducto = paquete.IdProducto,
                                NombreProducto = paquete.NombreProducto,
                                IdGrupoProducto = Convert.ToInt16(paquete.IdGrupo),
                                NombreGrupo = paquete.NombrePaquete,
                                ValorUnitario = paquete.ValorPaqueteProducto,
                                ValorTotal = paquete.ValorPaqueteProducto * paquete.Cantidad,
                                ValorProductos = paquete.ValorPaqueteProducto,
                                CantidadProducto = paquete.Cantidad,
                                CodigoProducto = paquete.CodigoPaquete,
                                CodigoGrupo = paquete.CodigoPG,
                                FechaVenta = DateTime.Now,
                                ValorPaquete = paquete.ValorPaquete,
                                ValorRecargo = paquete.ValorRecargo,
                                ValorTotalRecargo = paquete.ValorRecargo,
                                ValorDescuento = paquete.ValorDescuento,
                                ValorTotalDescuento = paquete.ValorDescuento,
                                DetalleVenta = new VentaDetalle()
                                {
                                    TerceroVenta = new Tercero()
                                    {
                                        Id = paquete.IdTerceroVenta,
                                        Nombre = paquete.NombreTercero,
                                        NumeroDocumento = paquete.NumeroDocumento
                                    }
                                },
                                DetalleTarifa = new DetalleTarifa()
                                {
                                    CodigoUnidad = string.Empty,
                                    FechaVigenciaTarifa = DateTime.Now,
                                    FechaInicial = DateTime.Now,
                                    FechaFinal = DateTime.Now
                                },
                                NumeroVenta = paquete.NumeroVenta,
                                VentaComponentes = new List<VentaComponente>(),
                                IndPaquete = 1
                            };

                            atencion.Detalle.Add(detalleFactura);
                        }

                        atencion.Paquetes.Add(encabezadoPaquete);
                    }
                }
            }

            return listaPaquetesFacturados;
        }

        /// <summary>
        /// M?todo que almacena la informacion de la cabezera de la factura.
        /// </summary>
        /// <param name="memStream">The mem stream.</param>
        /// <param name="tipo">Parametro tipo.</param>
        /// <returns>
        /// Encabezado estado de cuentas.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private string RetornaXml(System.IO.MemoryStream memStream, int tipo)
        {
            string[] encabezado = { "CodigoEntidad", "CodigoSeccion", "NumeroFactura", "IdTipoMovimiento", "CodigoMovimiento", "IdContrato", "IdPlan", "ConsecutivoFacturacion", "FechaFactura", "TipoFacturacion", "PrefijoNumeracion", "Estado", "IdTercero", "CodigoUsuario", "IdAtencion", "IdCodigoFactura", "ValorCodigoFactura", "IdDocumentoFactura", "NumeroDocumentoFactura", "ValorDescuento", "IdTipoServicio", "IdUbicacion", "FechaInicial", "FechaFinal", "HoraFactura", "DescuentoValorFactura", "IdDescuento", "ValorTotalDebito", "ValorTotalFactura", "Observaciones", "IdTerceroResponsable", "TipoResultadoTercero", "ConsecutivoCartera", "ValorSaldo", "FechaSaldo", "EstadoAuxiliar", "FechaFacturaDesde", "FechaFacturaHasta", "IndicadorImpresion", "IdSede", "IndicadorIngresoPropio" };
            string[] detalle = { "CodigoEntidad", "CodigoSeccion", "NumeroFactura", "IdTipoMovimiento", "CodigoMovimiento", "IdTransaccion", "NumeroVenta", "IdProducto", "IdLote", "IdAtencion", "CodigoProducto", "NombreProducto", "ValorUnitario", "IdDescuento", "ValorDescuento", "ValorRecargo", "ValorImpuesto", "SubTotal", "CantidadFacturaDetalle", "CantidadGlosasFacturaDetalle", "CantidadDisponible", "IdTercero", "CantidadVenta", "CodigoConcepto", "ValorPagina", "NivelOrden", "VigenciaTarifa", "IdManual", "ValorProductos", "CodigoUnidad", "IdRelacion", "CodigoTipoRelacion", "ValorPorcentajeParametro", "DescuentoPorcentajeParametro", "MetodoConfiguracion", "ValorTasas", "VentaTasas", "ValorMaximo", "ValorDiferencia", "EsComponente", "Componente", "ComponenteHomolgado", "NombreComponente", "Cantidad", "FactorQx", "CodigoGrupo", "IdComponente" };
            string[] maestro = { "CodigoEntidad", "CodigoSeccion", "NumeroFactura", "IdTipoMovimiento", "CodigoMovimiento", "IdTransaccion", "NumeroVenta", "IdProducto", "IdLote", "IdAtencion", "CodigoProducto", "NombreProducto", "ValorUnitario", "IdDescuento", "ValorDescuento", "ValorRecargo", "ValorImpuesto", "SubTotal", "CantidadFacturaDetalle", "CantidadGlosasFacturaDetalle", "CantidadDisponible", "IdTercero", "CantidadVenta", "CodigoConcepto", "ValorPagina", "ValorUnitario", "NivelOrden", "VigenciaTarifa", "IdManual", "ValorProductos", "CodigoUnidad", "IdRelacion", "CodigoTipoRelacion", "ValorPorcentajeParametro", "DescuentoPorcentajeParametro", "MetodoConfiguracion", "ValorTasas", "VentaTasas", "ValorMaximo", "ValorDiferencia", "EsComponente", "CodigoGrupo", "Cantidad" };
            string[] lstNoFacturable = { "IdAtencion", "IdExclusion", "IdProcesoDetalle", "IdProducto", "IdVenta", "NumeroVenta" };
            string[] ventas = { "CodigoEntidad", "IdTransaccion", "NumeroVenta", "IdProducto", "IdLote", "IdContrato", "IdPlan", "Cantidad", "ValorVenta" };

            memStream.Position = 0;
            System.IO.StreamReader streamReader = new System.IO.StreamReader(memStream);
            System.Xml.XmlDocument serializedXML = new System.Xml.XmlDocument();
            serializedXML.Load(streamReader);

            string nodo = string.Empty;
            string[] arrGrl = null;
            switch (tipo)
            {
                case 1:
                    nodo = "/EncabezadoFactura";
                    arrGrl = encabezado;
                    break;
                case 2:
                    nodo = "/ArrayOfuen/EstadoCuentaDetallado";
                    arrGrl = detalle;
                    break;
                case 3:
                    nodo = "/ArrayOfEstadoCuentaDetallado/EstadoCuentaDetallado";
                    arrGrl = maestro;
                    break;
                case 4:
                    nodo = "/ArrayOfNoFacturable/NoFacturable";
                    arrGrl = lstNoFacturable;
                    break;
                case 5:
                    nodo = "/ArrayOfVentaFactura/VentaFactura";
                    arrGrl = ventas;
                    break;
            }

            if (serializedXML.SelectNodes(nodo).Count != 0)
            {
                System.Xml.XmlDocument serializedXML2 = (System.Xml.XmlDocument)serializedXML.Clone();
                System.Xml.XmlNodeList nodes = serializedXML2.SelectNodes(nodo)[0].ChildNodes;
                foreach (System.Xml.XmlNode node in nodes)
                {
                    Array results = Array.FindAll(arrGrl, s => s.Equals(node.LocalName));
                    if (results.Length == 0)
                    {
                        System.Xml.XmlNodeList nodesDel = serializedXML.SelectNodes("//" + node.LocalName);
                        foreach (System.Xml.XmlNode nodeDel in nodesDel)
                        {
                            nodeDel.ParentNode.RemoveChild(nodeDel);
                        }
                    }
                }
            }

            foreach (System.Xml.XmlNode node in serializedXML)
            {
                if (node.NodeType == System.Xml.XmlNodeType.XmlDeclaration)
                {
                    serializedXML.RemoveChild(node);
                }
            }

            streamReader.Close();
            return serializedXML.OuterXml;
        }

        /// <summary>
        /// Separa valor de acuerdo a la cantidad cubrimiento.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararCantidadCubrimiento(BaseValidacion baseValidacion, Cubrimiento cubrimiento)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            if (cubrimiento.CondicionesCubrimiento.ValorPropio > 0)
            {
                if (baseValidacion.CantidadFacturar >= cubrimiento.CondicionesCubrimiento.ValorPropio)
                {
                    baseCalculo.CantidadProducto -= cubrimiento.CondicionesCubrimiento.ValorPropio;
                    this.CalcularValoresProducto(baseCalculo);
                    baseValidacion.CantidadProducto -= baseCalculo.CantidadProducto;
                    this.CalcularValoresProducto(baseValidacion);
                    cubrimiento.CondicionesCubrimiento.ValorPropio -= baseValidacion.CantidadFacturar;

                    if (baseCalculo.CantidadProducto == 0)
                    {
                        baseCalculo = null;
                    }
                }
                else
                {
                    cubrimiento.CondicionesCubrimiento.ValorPropio -= baseValidacion.CantidadFacturar;
                    baseCalculo = null;
                }
            }
            else
            {
                baseValidacion.CantidadProducto = 0;
                this.CalcularValoresProducto(baseValidacion);
                baseValidacion.CondicionSeparacion.Omitir = true;
            }

            return baseCalculo;
        }

        /// <summary>
        /// Separar factura de acuerdo a cantidad.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararCantidadTarifa(BaseValidacion baseValidacion, CondicionTarifa condicionTarifa)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            if (condicionTarifa.ValorPropio > 0)
            {
                if (baseValidacion.CantidadFacturar != condicionTarifa.ValorPropio)
                {
                    if (baseValidacion.CantidadFacturar >= condicionTarifa.ValorPropio)
                    {
                        baseCalculo.CantidadProducto -= condicionTarifa.ValorPropio;
                        this.CalcularValoresProducto(baseCalculo);
                        baseValidacion.CantidadProducto -= baseCalculo.CantidadProducto;

                        this.CalcularValoresProducto(baseValidacion);
                        condicionTarifa.ValorPropio -= baseValidacion.CantidadFacturar;
                        if (baseCalculo.CantidadProducto == 0)
                        {
                            baseCalculo = null;
                        }
                    }
                    else
                    {
                        condicionTarifa.ValorPropio -= baseValidacion.CantidadFacturar;
                        baseCalculo = null;
                    }
                }
                else
                {
                    baseCalculo.CondicionSeparacion.Omitir = true;
                }
            }
            else
            {
                baseValidacion.CantidadProducto = 0;
                this.CalcularValoresProducto(baseValidacion);
                baseValidacion.CondicionSeparacion.Omitir = true;
            }

            return baseCalculo;
        }

        /// <summary>
        /// Separa cantidad de acuerdo al porcentaje de cubrimiento.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararPorcentajeCubrimiento(BaseValidacion baseValidacion, Cubrimiento cubrimiento)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            baseValidacion.CantidadProducto = Math.Round(baseValidacion.CantidadProducto * (cubrimiento.CondicionesCubrimiento.ValorPorcentaje / 100), 10);
            this.CalcularValoresProducto(baseValidacion);
            baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
            this.CalcularValoresProducto(baseCalculo);
            return cubrimiento.CondicionesCubrimiento.ValorPorcentaje >= 100 ? null : baseCalculo;
        }

        /// <summary>
        /// Separar factura de acuerdo a porcentaje Maximo.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararPorcentajeMaximoTarifa(BaseValidacion baseValidacion, CondicionTarifa condicionTarifa)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            baseValidacion.CantidadProducto = Math.Round(baseValidacion.CantidadProducto * (condicionTarifa.ValorPropio / 100), 10);
            this.CalcularValoresProducto(baseValidacion);
            baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
            this.CalcularValoresProducto(baseCalculo);
            return condicionTarifa.ValorPropio >= 100 ? null : baseCalculo;
        }

        /// <summary>
        /// Separar cubrimiento de acuerdo al valor.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 22/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararValorCubrimiento(BaseValidacion baseValidacion, Cubrimiento cubrimiento)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            decimal cantidadBaseFacturar = baseCalculo.CantidadFacturar;
            decimal porcentajeCubrimiento = 0;
            if (cubrimiento.CondicionesCubrimiento.ValorPropio >= 1)
            {
                if (cubrimiento.CondicionesCubrimiento.ValorPropio >= baseValidacion.ValorBrutoTotal)
                {
                    cubrimiento.CondicionesCubrimiento.ValorPropio -= baseValidacion.ValorBrutoTotal;
                    baseCalculo = null;
                }
                else
                {
                    porcentajeCubrimiento = Math.Round(cubrimiento.CondicionesCubrimiento.ValorPropio / baseValidacion.ValorBrutoTotal, 10);

                    baseValidacion.CantidadProducto = baseValidacion.CantidadFacturar * porcentajeCubrimiento;
                    this.CalcularValoresProducto(baseValidacion);
                    baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
                    this.CalcularValoresProducto(baseCalculo);
                    cubrimiento.CondicionesCubrimiento.ValorPropio -= baseValidacion.ValorBrutoTotal;
                }
            }
            else
            {
                porcentajeCubrimiento = 0;
                baseValidacion.CantidadProducto = baseValidacion.CantidadFacturar * porcentajeCubrimiento;
                this.CalcularValoresProducto(baseValidacion);
                baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
                this.CalcularValoresProducto(baseCalculo);

                if (baseValidacion.CantidadProducto == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }

            return baseCalculo;
        }

        /// <summary>
        /// Separa factura de acuerdo a condicion de Valor maximo.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private BaseValidacion SepararValorMaximoTarifa(BaseValidacion baseValidacion, CondicionTarifa condicionTarifa)
        {
            BaseValidacion baseCalculo = this.ClonarObjeto(baseValidacion);
            decimal cantidadBaseFacturar = baseCalculo.CantidadFacturar;
            decimal porcentajeCubrimiento = 0;
            if (condicionTarifa.ValorPropio > 0)
            {
                if (condicionTarifa.ValorPropio >= baseValidacion.ValorBrutoTotal)
                {
                    condicionTarifa.ValorPropio -= baseValidacion.ValorBrutoTotal;
                    baseCalculo = null;
                }
                else
                {
                    porcentajeCubrimiento = Math.Round(condicionTarifa.ValorPropio / baseValidacion.ValorBruto, 10);
                    baseValidacion.CantidadProducto = Math.Round(baseValidacion.CantidadFacturar * porcentajeCubrimiento, 10);
                    this.CalcularValoresProducto(baseValidacion);
                    baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
                    this.CalcularValoresProducto(baseCalculo);
                }
            }
            else
            {
                porcentajeCubrimiento = 0;
                baseValidacion.CantidadProducto = baseValidacion.CantidadFacturar * porcentajeCubrimiento;
                this.CalcularValoresProducto(baseValidacion);
                baseCalculo.CantidadProducto -= baseValidacion.CantidadProducto;
                this.CalcularValoresProducto(baseCalculo);

                if (baseValidacion.CantidadProducto == 0)
                {
                    baseValidacion.CondicionSeparacion.Omitir = true;
                }
            }

            return baseCalculo;
        }

        /// <summary>
        /// Realiza la sumatoria de los valores de conceptos de las atenciones cruzadas.
        /// </summary>
        /// <param name="atenciones">The atenciones.</param>
        /// <returns>Suma total de los conceptos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private decimal SumatoriaValoresConceptos(List<FacturaAtencion> atenciones)
        {
            var atencionesSumadas = from
                                        item in atenciones
                                    from
                                        concepto in item.ConceptosCobro
                                    where
                                        item.Cruzar == true
                                        && item.ConceptosCobro.Count > 0
                                        && item.MovimientosTesoreria.Count > 0
                                        && item.Detalle.Where(sp => sp.Exclusion != null && sp.ExclusionManual != null).Count() > 0
                                        && concepto.ValorSaldo == 0
                                    select
                                        item;
            var sumaConcepto = atencionesSumadas.Sum(sp => sp.ValorConcepto);
            return sumaConcepto;
        }

        /// <summary>
        /// Validar si las condiciones de tarifa contienen valor propio o ajustes de tarifa.
        /// </summary>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="ajusteTarifaValorPropio">If set to <c>true</c> [ajuste tarifa valor propio].</param>
        /// <returns>
        /// Retorna CondicionTarifa.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 18/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private CondicionTarifa ValidarAjusteTarifaValorPropio(BaseValidacion baseValidacion, bool ajusteTarifaValorPropio)
        {
            var listaCondiciones = from item in baseValidacion.CondicionesTarifa
                                   where (ajusteTarifaValorPropio && (item.IdTipoRelacion == 1 || item.IdTipoRelacion == 3))
                                      || (!ajusteTarifaValorPropio && item.IdTipoRelacion != 1 && item.IdTipoRelacion != 3)
                                   select item;
            return listaCondiciones.FirstOrDefault();
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para la Condici n de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((condicionCubrimiento.IdAtencion > 0 && !condicionCubrimiento.IdAtencion.Equals(atencion.IdAtencion)) ||
                (condicionCubrimiento.IdProducto > 0 && !condicionCubrimiento.IdProducto.Equals(detalle.IdProducto)) ||
                (condicionCubrimiento.IdGrupoProducto > 0 && !condicionCubrimiento.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (condicionCubrimiento.IdTipoProducto > 0 && !condicionCubrimiento.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (condicionCubrimiento.IdContrato > 0 && !condicionCubrimiento.IdContrato.Equals(atencion.IdContrato)) ||
                (condicionCubrimiento.IdServicio > 0 && !condicionCubrimiento.IdServicio.Equals(atencion.IdServicio)) ||
                (condicionCubrimiento.IdPlan > 0 && !condicionCubrimiento.IdPlan.Equals(detalle.IdPlan)) ||
                (condicionCubrimiento.IdTipoAtencion > 0 && !condicionCubrimiento.IdTipoAtencion.Equals(atencion.IdTipoAtencion)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para la Condicion de factura.
        /// </summary>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 19/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private CondicionTarifa ValidarCondicionFactura(List<CondicionTarifa> condicionFactura, int identificadorAtencion, Vinculacion vinculacion)
        {
            var resultado = from
                                item in condicionFactura
                            where
                                (item.IdContrato == vinculacion.Contrato.Id || item.IdContrato == 0)
                                && (item.IdPlan == vinculacion.Plan.Id || item.IdPlan == 0)
                                && (item.IdAtencion == identificadorAtencion || item.IdAtencion == 0)
                            orderby
                                 item.IdAtencion descending,
                                 item.IdPlan descending,
                                 item.IdContrato descending
                            select
                                 item;
            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para la Condicion de tarifa.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarCondicionTarifa(CondicionTarifa condicionTarifa, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((condicionTarifa.IdAtencion > 0 && !condicionTarifa.IdAtencion.Equals(atencion.IdAtencion)) ||
                (condicionTarifa.IdProducto > 0 && !condicionTarifa.IdProducto.Equals(detalle.IdProducto)) ||
                (condicionTarifa.IdGrupoProducto > 0 && !condicionTarifa.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (condicionTarifa.IdTipoProducto > 0 && !condicionTarifa.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (condicionTarifa.IdContrato > 0 && !condicionTarifa.IdContrato.Equals(atencion.IdContrato)) ||
                (condicionTarifa.IdServicio > 0 && !condicionTarifa.IdServicio.Equals(atencion.IdServicio)) ||
                (condicionTarifa.IdPlan > 0 && !condicionTarifa.IdPlan.Equals(atencion.IdPlan)) ||
                (condicionTarifa.IdTipoAtencion > 0 && !condicionTarifa.IdTipoAtencion.Equals(atencion.IdTipoAtencion)) ||
                (condicionTarifa.Componente != "NA" && !condicionTarifa.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para el costo asociado.
        /// </summary>
        /// <param name="costoAsociado">The costo asociado.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarCostoAsociado(CostoAsociado costoAsociado, FacturaAtencion atencion, VentaComponente detalle)
        {
            bool continuarProceso = true;
            if ((costoAsociado.IdProducto > 0 && !costoAsociado.IdProducto.Equals(detalle.IdProducto)) ||
                (costoAsociado.Componente != string.Empty && !costoAsociado.Componente.Equals(detalle.ComponenteBase)) ||
                (costoAsociado.IdAtencion > 0 && !costoAsociado.IdAtencion.Equals(atencion.IdAtencion)) ||
                (costoAsociado.IdServicio > 0 && !costoAsociado.IdServicio.Equals(atencion.IdServicio)) ||
                (costoAsociado.IdTipoAtencion > 0 && !costoAsociado.IdTipoAtencion.Equals(atencion.IdTipoAtencion)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para un cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarCubrimiento(Cubrimiento cubrimiento, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((cubrimiento.IdAtencion > 0 && !cubrimiento.IdAtencion.Equals(atencion.IdAtencion)) ||
                (cubrimiento.IdProducto > 0 && !cubrimiento.IdProducto.Equals(detalle.IdProducto)) ||
                (cubrimiento.IdTipoProducto > 0 && !cubrimiento.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (cubrimiento.IdContrato > 0 && !cubrimiento.IdContrato.Equals(atencion.IdContrato)) ||
                (cubrimiento.IdPlan > 0 && !cubrimiento.IdPlan.Equals(detalle.IdPlan)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para el Descuento.
        /// </summary>
        /// <param name="descuento">The descuento.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarDescuento(Descuento descuento, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((descuento.IdAtencion > 0 && !descuento.IdAtencion.Equals(atencion.IdAtencion)) ||
                (descuento.IdPlan > 0 && !descuento.IdPlan.Equals(atencion.IdPlan)) ||
                (descuento.IdContrato > 0 && !descuento.IdContrato.Equals(atencion.IdContrato)) ||
                (descuento.IdProducto > 0 && !descuento.IdProducto.Equals(detalle.IdProducto)) ||
                (descuento.IdGrupoProducto > 0 && !descuento.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (descuento.IdTipoProducto > 0 && !descuento.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (descuento.IdServicio > 0 && !descuento.IdServicio.Equals(atencion.IdServicio)) ||
                (descuento.IdTipoAtencion > 0 && !descuento.IdTipoAtencion.Equals(atencion.IdTipoAtencion))
                || (descuento.Componente != "NA" && !descuento.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Método para validar los copagos y cruces de depositos entre entidades y particulares.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\IVAN J.
        /// FechaDeCreacion: 28/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void ValidarEstadoCuentaEncabezadoParticular(FacturaCompuesta facturaCompuesta, EstadoCuentaEncabezado estadoCuenta)
        {
            FacturaAtencionConceptoCobro concepto = (from item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro
                                                     where item.IdContrato == estadoCuenta.IdContrato && item.IdPlan == estadoCuenta.IdPlan
                                                     select item).FirstOrDefault();

            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;

            if (this.ValidarParticular(estadoCuenta))
            {
                this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);

                this.GuardarInformacionFacturaResponsable(facturaCompuesta, estadoCuenta);

                this.ActualizaEstadoVenta(estadoCuenta);

                this.CrearEstadoCuentaContabilidadFactura(estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);
                this.ActualizarIdCuentaFactura(estadoCuenta.ConsecutivoCartera, encabezadoFactura.NumeroFactura);

                if (concepto != null && concepto.DepositoParticular == false)
                {
                    this.CrearCuentasCartera(facturaCompuesta);
                }
            }
            else
            {
                this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);
                this.GuardarInformacionFacturaResponsable(facturaCompuesta, estadoCuenta);

                this.ActualizaEstadoVenta(estadoCuenta);

                this.CrearEstadoCuentaContabilidadFactura(estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);
                this.ActualizarIdCuentaFactura(estadoCuenta.ConsecutivoCartera, encabezadoFactura.NumeroFactura);
                this.CrearCuentasCartera(facturaCompuesta);
            }
        }

        /// <summary>
        /// Validars the estado cuenta encabezado particular.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        private void ValidarEstadoCuentaEncabezadoParticularPaquete(FacturaCompuesta facturaCompuesta, EstadoCuentaEncabezado estadoCuenta)
        {
            FacturaAtencionConceptoCobro concepto = (from item in estadoCuenta.FacturaAtencion.FirstOrDefault().ConceptosCobro
                                                     where item.IdContrato == estadoCuenta.IdContrato && item.IdPlan == estadoCuenta.IdPlan
                                                     select item).FirstOrDefault();

            var encabezadoFactura = facturaCompuesta.EncabezadoFactura;

            if (this.ValidarParticular(estadoCuenta))
            {
                this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);

                this.GuardarInformacionFacturaResponsable(facturaCompuesta, estadoCuenta);

                this.ActualizaEstadoVentaPaquetes(estadoCuenta.NumeroFacturaSinPrefijo);

                this.CrearEstadoCuentaContabilidadFactura(estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);
                this.ActualizarIdCuentaFactura(estadoCuenta.ConsecutivoCartera, encabezadoFactura.NumeroFactura);

                if (concepto != null && concepto.DepositoParticular == false)
                {
                    this.CrearCuentasCartera(facturaCompuesta);
                }
            }
            else
            {
                this.CrearFacturaPago(encabezadoFactura.NumeroFactura, estadoCuenta);
                this.GuardarInformacionFacturaResponsable(facturaCompuesta, estadoCuenta);

                this.ActualizaEstadoVentaPaquetes(estadoCuenta.NumeroFacturaSinPrefijo);

                this.CrearEstadoCuentaContabilidadFactura(estadoCuenta, encabezadoFactura.NumeroFactura, facturaCompuesta);
                this.ActualizarIdCuentaFactura(estadoCuenta.ConsecutivoCartera, encabezadoFactura.NumeroFactura);
                this.CrearCuentasCartera(facturaCompuesta);
            }
        }

        /// <summary>
        /// Valida las exclusiones no marcadas.
        /// </summary>
        /// <param name="lstNoMarcadas">The no marcadas.</param>
        /// <param name="exclusion">The exclusion.</param>
        /// <param name="tipoExclusion">The tipo exclusion.</param>
        /// <returns>Indica si existe o no exclusiones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarExclusion(List<NoMarcada> lstNoMarcadas, Exclusion exclusion, string tipoExclusion)
        {
            var resultado = from
                                item in lstNoMarcadas
                            where
                                item.TipoExclusion == tipoExclusion
                                && item.IdExclusion == exclusion.Id
                            select
                                item;
            return resultado.Count() == 0 ? false : true;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para las Exclusiones.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarExclusionContrato(Exclusion exclusion, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((exclusion.IdAtencion > 0 && !exclusion.IdAtencion.Equals(atencion.IdAtencion)) ||
                (exclusion.IdProducto > 0 && !exclusion.IdProducto.Equals(detalle.IdProducto)) ||
                (exclusion.IdGrupoProducto > 0 && !exclusion.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (exclusion.IdTipoProducto > 0 && !exclusion.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (exclusion.IdContrato > 0 && !exclusion.IdContrato.Equals(atencion.IdContrato)) ||
                (exclusion.IdServicio > 0 && !exclusion.IdServicio.Equals(atencion.IdServicio)) ||
                (exclusion.IdPlan > 0 && !exclusion.IdPlan.Equals(detalle.IdPlan)) ||
                (exclusion.IdTipoAtencion > 0 && !exclusion.IdTipoAtencion.Equals(atencion.IdTipoAtencion)) ||
                (exclusion.Componente != "NA" && !exclusion.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para las Exclusiones Manual.
        /// </summary>
        /// <param name="exclusion">The exclusion manual.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarExclusionManual(ExclusionManual exclusion, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((exclusion.IdAtencion > 0 && !exclusion.IdAtencion.Equals(atencion.IdAtencion)) ||
                (exclusion.IdProducto > 0 && !exclusion.IdProducto.Equals(detalle.IdProducto)) ||
                (exclusion.IdGrupoProducto > 0 && !exclusion.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (exclusion.IdTipoProducto > 0 && !exclusion.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (exclusion.IdContrato > 0 && !exclusion.IdContrato.Equals(atencion.IdContrato)) ||
                (exclusion.IdServicio > 0 && !exclusion.IdServicio.Equals(atencion.IdServicio)) ||
                (exclusion.IdPlan > 0 && !exclusion.IdPlan.Equals(detalle.IdPlan)) ||
                (exclusion.IdTipoAtencion > 0 && !exclusion.IdTipoAtencion.Equals(atencion.IdTipoAtencion)) ||
                (exclusion.Componente != null && exclusion.Componente != "NA" && !exclusion.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida los Factores QX.
        /// </summary>
        /// <param name="factores">The factores.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>La Validacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarFactoresQX(FactoresQX factores, FacturaAtencion atencion, VentaComponente detalle)
        {
            bool continuarProceso = true;
            if (!factores.Componente.Equals(detalle.Componente) ||
                (!factores.IdManual.Equals(atencion.IdManual)) ||
                (!factores.FechaVigencia.Equals(detalle.FechaVenta)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Realiza la validación de que existe la data
        /// suficiente para realizar el proceso de facturación 
        /// </summary>
        /// <param name="facturaCompuesta">Factura compuesta.</param>
        /// <returns>Validación factura compuesta.</returns>
        private bool ValidarFacturaCompuesta(FacturaCompuesta facturaCompuesta)
        {
            bool resultado = true;

            // Validación para mirar si tiene registros 
            if ((facturaCompuesta == null) ||
                (facturaCompuesta.EstadoCuentaEncabezado == null) ||
                (facturaCompuesta.EstadoCuentaEncabezado.EstadoCuentaDetallado == null) ||
                (facturaCompuesta.EncabezadoFactura == null) ||
                (facturaCompuesta.EstadoCuentaEncabezado.EstadoCuentaDetallado.Count == 0))
            {
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Valida los niveles de Complejidad.
        /// </summary>
        /// <param name="niveles">The niveles.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>La validadcion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 31/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarNivelesComplejidad(NivelComplejidad niveles, FacturaAtencion atencion, VentaComponente detalle)
        {
            bool continuarProceso = true;
            if ((niveles.Producto > 0 && !niveles.Producto.Equals(detalle.IdComponente)) || (!niveles.OrdenNivel.Equals(detalle.NivelOrden)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Metodo para validar numero de venta.
        /// </summary>
        /// <param name="numerosVenta">The numeros venta.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <returns>Indica Si la venta esta excluida.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 27/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private bool ValidarNumerosVenta(List<int> numerosVenta, int numeroVenta)
        {
            var resultado = from
                                item in numerosVenta
                            where
                                item == numeroVenta
                            select
                                item;
            return resultado.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Valida si el tercero es particular para validación de estado de cuenta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Retorna true si es particular.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\IVAN J.
        /// FechaDeCreacion: 28/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private bool ValidarParticular(EstadoCuentaEncabezado estadoCuenta)
        {
            if (estadoCuenta.IdContrato == 0 || estadoCuenta.IdContrato == 473)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Valida si el tercero es particular para validación de estado de cuenta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Retorna true si es particular.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\IVAN J.
        /// FechaDeCreacion: 28/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private bool ValidarParticularNC(EstadoCuentaEncabezado estadoCuenta)
        {
            FachadaFacturacion fachada = new FachadaFacturacion();
            string identificadorNaturaleza = fachada.ValidarNaturaleza(estadoCuenta.IdTercero);
            bool boolEsParticular = false;

            if (identificadorNaturaleza == "4")
            {
                boolEsParticular = true;
            }
            else if (identificadorNaturaleza == "3")
            {
                boolEsParticular = false;
            }

            return boolEsParticular;
        }

        /// <summary>
        /// Metodo de preinicio de proceso de facturaci n por actividades.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ValidarProcesoGeneralAtencion(TipoFacturacion tipoFacturacion, FacturaAtencion atencion, List<CondicionProceso> condiciones, CondicionContrato condicionContrato, CondicionTarifa condicionFactura, out List<FacturaAtencionDetalle> detalleVinculacion, Vinculacion vinculacion)
        {
            List<FacturaAtencionDetalle> productoVinculacionActual = new List<FacturaAtencionDetalle>();
            List<VentaComponente> componenteVinculacion = new List<VentaComponente>();
            detalleVinculacion = new List<FacturaAtencionDetalle>();
            List<FacturaAtencionDetalle> detalleActual = new List<FacturaAtencionDetalle>();
            CondicionContrato condicionContratoBase = null;
            this.OrdenFactorQX = 1;
            bool aplicaCondicionFactura = false;

            // Solo se carga la primera vez
            if (this.listaTodosFactoresQx == null)
            {
                this.listaTodosFactoresQx = this.ConsultarFactoresQx();
            }

            // Obtiene los componentes asociados a los productos
            var componentesProductos = this.ComponentesPorProductos(atencion);

            foreach (var componente in componentesProductos)
            {
                var responsable = this.ObtenerResponsableVentaComponentes(this.ConsultarTerceroComponente(atencion.IdAtencion), componente.IdProducto, componente.IdTransaccion, componente.NumeroVenta, componente.Componente);

                componente.Responsable = responsable;
            }

            // Obtiene los productos relaciones
            var paginacion = this.CrearConsultaProductosRelacionados(atencion.IdAtencion, 0);

            // Obtiene productos asociados
            var productosAsociados = this.ConsultarVentaProductosRelacion(paginacion);

            // Validar si cumple condicion de factura
            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                aplicaCondicionFactura = true;
            }

            decimal porcentaje = 0;

            // 1. Condiciones de facturacion
            foreach (var detalle in atencion.Detalle)
            {
                if (componenteVinculacion == null)
                {
                    componenteVinculacion = new List<VentaComponente>();
                }

                var atencionProductoAsociado = this.BuscarProductoAsociado(productosAsociados.Item, detalle);

                // Obtener valor de la venta
                if (detalle.ValorUnitario == 0)
                {
                    detalle.ValorUnitario = detalle.ValorOriginal;
                }

                condicionContratoBase = condicionContrato;

                // Buscar componentes asociados a un producto
                if (vinculacion.Orden == 1)
                {
                    detalle.VentaComponentes = this.BuscarComponentesPorProducto(componentesProductos, detalle);
                }

                // Validar reglas productos componentes
                condicionContrato = this.ValidarReglasProductoComponente(tipoFacturacion, null, atencion, detalle, condiciones, Tipo.Producto, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);

                if (atencionProductoAsociado == null)
                {
                    if (!detalle.Excluido && detalle.CondicionSeparacion.NoCubrimiento == false)
                    {
                        foreach (var componente in detalle.VentaComponentes)
                        {
                            componente.FechaVenta = detalle.FechaVenta;
                            componente.HoraVenta = detalle.HoraVenta;

                            // Obtener valor de la venta
                            if (componente.ValorUnitario == 0)
                            {
                                componente.ValorUnitario = componente.ValorOriginal;
                            }

                            this.ValidarReglasProductoComponente(tipoFacturacion, detalle, atencion, componente, condiciones, Tipo.Componente, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);
                            componente.CodigoProducto = detalle.CodigoProducto;
                            componente.NombreProducto = detalle.NombreProducto;
                            componente.CodigoGrupo = detalle.CodigoGrupo;
                            componente.NombreGrupo = detalle.NombreGrupo;

                            this.CalcularValorComponente(atencion, detalle, componente, condicionContrato, condiciones);

                            if (componente.CantidadComponente - componente.CantidadComponenteFacturada == 0)
                            {
                                componente.CondicionSeparacion.Omitir = true;
                            }
                            else if (componente.CantidadComponente - componente.CantidadComponenteFacturada != componente.CantidadProducto && componente.CantidadComponenteFacturada != 0)
                            {
                                componente.CantidadProducto = componente.CantidadComponente - componente.CantidadComponenteFacturada;
                            }
                        }
                    }
                }

                this.CalcularValorProducto(detalle, condicionContrato);
                detalle.FechaVigenciaContrato = condicionContrato.FechaVigenciaResultado;
                condicionContrato = condicionContratoBase;
                var productoVinculacion = detalle.CopiarObjeto();
                productoVinculacion.VentaComponentes.RemoveAll(a => a.CondicionSeparacion.Omitir == true);

                if (componenteVinculacion != null && componenteVinculacion.Count > 0)
                {
                    var detalleSeparar = detalle.CopiarObjeto();

                    var detalleS = this.BuscarDetalle(detalleVinculacion, detalleSeparar);
                    if (detalleS == null)
                    {
                        var componenteProducto = from item in componentesProductos
                                                 where item.NumeroVenta == detalle.NumeroVenta
                                                 && item.IdProducto == detalle.IdProducto
                                                 select item;

                        var ventCompo = from item in componenteVinculacion
                                        select item;

                        porcentaje = ventCompo.Sum(c => c.CantidadFacturar) / componenteProducto.Sum(c => c.CantidadComponente);
                        detalleSeparar.CantidadProducto = Math.Round(porcentaje, 2);

                        detalleSeparar.VentaComponentes.Clear();
                        detalleSeparar.VentaComponentes = componenteVinculacion;

                        if (detalleSeparar.CantidadFacturar == 0)
                        {
                            detalleSeparar.CondicionSeparacion.Omitir = true;
                        }

                        detalleVinculacion.Add(detalleSeparar);
                    }
                    else
                    {
                        detalleS.VentaComponentes.Clear();
                        detalleS.VentaComponentes = componenteVinculacion;
                    }

                    componenteVinculacion = null;
                }

                if (productoVinculacion.CondicionSeparacion.NoCubrimiento == false)
                {
                    if (productoVinculacion.VentaComponentes.Count > 0)
                    {
                        if (productoVinculacion.CantidadProductoFacturada > 0 && (productoVinculacion.CodigoGrupo == "PRQX" || productoVinculacion.CodigoGrupo == "ANGP"))
                        {
                            foreach (VentaComponente vc in productoVinculacion.VentaComponentes)
                            {
                                if (vc.CantidadComponente - vc.CantidadComponenteFacturada == 0)
                                {
                                    vc.CondicionSeparacion.Omitir = true;
                                }
                            }

                            productoVinculacion.CantidadProducto = productoVinculacion.CantidadFacturar;
                            productoVinculacion.CantidadProductoFacturada = 0;
                        }
                        else
                        {
                            var componenteProducto = from item in componentesProductos
                                                     where item.NumeroVenta == detalle.NumeroVenta
                                                     && item.IdProducto == detalle.IdProducto
                                                     select item;

                            var ventCompo = from item in productoVinculacion.VentaComponentes
                                            select item;

                            porcentaje = ventCompo.Sum(c => c.CantidadFacturar) / componenteProducto.Sum(c => c.CantidadComponente);
                            productoVinculacion.CantidadProducto = Math.Round(porcentaje, 2);
                        }
                    }

                    detalleActual.Add(productoVinculacion);
                }
            }

            if (detalleActual.Count > 0)
            {
                atencion.Detalle = detalleActual;
            }

            List<FacturaAtencionDetalle> listaProductoVentas = new List<FacturaAtencionDetalle>();
            List<FacturaAtencionDetalle> listaProductoFactorQX = atencion.Detalle.CopiarObjeto();
            var productosQx = from item in atencion.Detalle
                              where item.TipoProductos == TipoProd.Quirurjicos
                              select item;
            int ventaActual = 0;
            foreach (var itemDetalle in productosQx)
            {
                if (ventaActual == 0)
                {
                    ventaActual = itemDetalle.NumeroVenta;

                    listaProductoVentas = (from item in productosQx.CopiarObjeto()
                                           where item.NumeroVenta == ventaActual && item.TipoProductos == TipoProd.Quirurjicos
                                           select item).ToList();
                    this.OrdenarFactor(listaProductoVentas, componentesProductos);

                    listaProductoFactorQX.RemoveAll(item => item.NumeroVenta == ventaActual);

                    foreach (var detallesVenta in listaProductoVentas.OrderBy(item => item.OrdenQX))
                    {
                        foreach (var componenteVenta in detallesVenta.VentaComponentes)
                        {
                            componenteVenta.OrdenQX = detallesVenta.OrdenQX;
                            this.OrdenFactorQX = detallesVenta.OrdenQX;
                            this.AplicarReglaFactoresQX(atencion, componenteVenta);
                            this.CalcularValorComponenteFactQX(componenteVenta, detallesVenta, atencion.IdContrato);
                        }

                        listaProductoFactorQX.Add(detallesVenta);
                    }
                }
                else if (ventaActual > 0 && ventaActual != itemDetalle.NumeroVenta)
                {
                    ventaActual = itemDetalle.NumeroVenta;

                    listaProductoVentas = (from item in productosQx.CopiarObjeto()
                                           where item.NumeroVenta == ventaActual && item.TipoProductos == TipoProd.Quirurjicos
                                           select item).ToList();
                    this.OrdenarFactor(listaProductoVentas, componentesProductos);

                    listaProductoFactorQX.RemoveAll(item => item.NumeroVenta == ventaActual);

                    foreach (var detallesVenta in listaProductoVentas.OrderBy(item => item.OrdenQX))
                    {
                        foreach (var componenteVenta in detallesVenta.VentaComponentes)
                        {
                            componenteVenta.OrdenQX = detallesVenta.OrdenQX;
                            this.OrdenFactorQX = detallesVenta.OrdenQX;
                            this.AplicarReglaFactoresQX(atencion, componenteVenta);
                            this.CalcularValorComponenteFactQX(componenteVenta, detallesVenta, atencion.IdContrato);
                        }

                        listaProductoFactorQX.Add(detallesVenta);
                    }
                }
            }

            atencion.Detalle.Clear();
            List<FacturaAtencionDetalle> detalleOrdenado = (from fe in listaProductoFactorQX.CopiarObjeto() orderby fe.FechaVenta ascending, fe.HoraVenta ascending select fe).ToList();
            atencion.Detalle = detalleOrdenado;

            // Aplicar condición de factura
            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                foreach (var itemDetalle in detalleOrdenado)
                {
                    if (itemDetalle != null && itemDetalle.VentaComponentes != null && itemDetalle.VentaComponentes.Count > 0)
                    {
                        this.CalcularValorProductoComponente(itemDetalle);
                    }

                    if (condicionFactura.ValorPropio >= 1
                        && (itemDetalle.VentaComponentes == null || itemDetalle.VentaComponentes.Count == 0)
                        && (itemDetalle.CondicionSeparacion.NoCubrimiento == false)
                        && (itemDetalle.CondicionSeparacion.Omitir == false)
                        && ((itemDetalle.ValorBruto == 0 ? itemDetalle.ValorOriginal : itemDetalle.ValorBruto) > 0))
                    {
                        if ((itemDetalle.TipoProductos == TipoProd.Medicamentos || itemDetalle.TipoProductos == TipoProd.Insumos) && (!itemDetalle.Excluido))
                        {
                            this.AplicarCondicionesFactura(itemDetalle, condicionFactura, productoVinculacionActual, detalleVinculacion);
                        }
                        else
                        {
                            if ((condicionFactura.ValorPropio >= (itemDetalle.ValorBruto == 0 ? itemDetalle.ValorOriginal : itemDetalle.ValorBruto) * itemDetalle.CantidadFacturar) && (!itemDetalle.Excluido))
                            {
                                condicionFactura.ValorPropio -= itemDetalle.ValorBruto *
                                                                itemDetalle.CantidadFacturar;
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else if (itemDetalle.Excluido)
                            {
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else
                            {
                                detalleVinculacion.Add(itemDetalle);
                                productoVinculacionActual.Remove(itemDetalle);
                            }
                        }
                    }
                    else if (condicionFactura.ValorPropio >= 1
                             && (itemDetalle.CondicionSeparacion.NoCubrimiento == false)
                             && (itemDetalle.CondicionSeparacion.Omitir == false))
                    {
                        FacturaAtencionDetalle productoCalculado = new FacturaAtencionDetalle();
                        productoCalculado = itemDetalle.CopiarObjeto();

                        if (productoCalculado != null && productoCalculado.VentaComponentes != null &&
                            productoCalculado.VentaComponentes.Count > 0)
                        {
                            this.CalcularValorProductoComponente(productoCalculado);
                        }

                        if ((condicionFactura.ValorPropio >=
                             (productoCalculado.ValorBruto == 0
                                 ? productoCalculado.ValorOriginal
                                 : productoCalculado.ValorBruto)) && (!itemDetalle.Excluido))
                        {
                            condicionFactura.ValorPropio -= productoCalculado.ValorBruto;
                            productoVinculacionActual.Add(productoCalculado);
                        }
                        else if (itemDetalle.Excluido)
                        {
                            productoVinculacionActual.Add(itemDetalle);
                        }
                        else
                        {
                            detalleVinculacion.Add(itemDetalle);
                            productoVinculacionActual.Remove(itemDetalle);
                        }
                    }

                    if (productoVinculacionActual.Count > 0)
                    {
                        var proVinculacionActual = this.BuscarDetalle(productoVinculacionActual, itemDetalle);
                        var proOtraVinculacion = this.BuscarDetalle(detalleVinculacion, itemDetalle);
                        if (proVinculacionActual == null && proOtraVinculacion == null)
                        {
                            if (itemDetalle.Excluido)
                            {
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else
                            {
                                detalleVinculacion.Add(itemDetalle);
                            }
                        }
                    }
                }

                atencion.Detalle = productoVinculacionActual;
            }
        }

        /// <summary>
        /// Metodo de preinicio de proceso de facturaci n por actividades.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="condicionFactura">The condicion factura.</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void ValidarProcesoGeneralAtencionPaquetes(TipoFacturacion tipoFacturacion, FacturaAtencion atencion, List<CondicionProceso> condiciones, CondicionContrato condicionContrato, CondicionTarifa condicionFactura, out List<FacturaAtencionDetalle> detalleVinculacion, Vinculacion vinculacion)
        {
            List<FacturaAtencionDetalle> productoVinculacionActual = new List<FacturaAtencionDetalle>();
            List<VentaComponente> componenteVinculacion = new List<VentaComponente>();
            detalleVinculacion = new List<FacturaAtencionDetalle>();
            List<FacturaAtencionDetalle> detalleActual = new List<FacturaAtencionDetalle>();
            CondicionContrato condicionContratoBase = null;
            this.OrdenFactorQX = 1;
            bool aplicaCondicionFactura = false;

            // Solo se carga la primera vez
            if (this.listaTodosFactoresQx == null)
            {
                this.listaTodosFactoresQx = this.ConsultarFactoresQx();
            }

            // Obtiene los componentes asociados a los productos
            var componentesProductos = this.ComponentesPorProductos(atencion);

            foreach (var componente in componentesProductos)
            {
                var responsable = this.ObtenerResponsableVentaComponentes(this.ConsultarTerceroComponente(atencion.IdAtencion), componente.IdProducto, componente.IdTransaccion, componente.NumeroVenta, componente.Componente);

                componente.Responsable = responsable;
            }

            // Obtiene los productos relaciones
            var paginacion = this.CrearConsultaProductosRelacionados(atencion.IdAtencion, 0);

            // Obtiene productos asociados
            var productosAsociados = this.ConsultarVentaProductosRelacion(paginacion);

            // Validar si cumple condicion de factura
            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                aplicaCondicionFactura = true;
            }

            decimal porcentaje = 0;

            // 1. Condiciones de facturacion
            foreach (var detalle in atencion.Detalle)
            {
                if (componenteVinculacion == null)
                {
                    componenteVinculacion = new List<VentaComponente>();
                }

                var atencionProductoAsociado = this.BuscarProductoAsociado(productosAsociados.Item, detalle);

                // Obtener valor de la venta
                if (detalle.ValorUnitario == 0)
                {
                    detalle.ValorUnitario = detalle.ValorOriginal;
                }

                condicionContratoBase = condicionContrato;

                // Buscar componentes asociados a un producto
                if (vinculacion.Orden == 1)
                {
                    detalle.VentaComponentes = this.BuscarComponentesPorProducto(componentesProductos, detalle);
                }

                // Validar reglas productos componentes
                condicionContrato = this.ValidarReglasProductoComponente(tipoFacturacion, null, atencion, detalle, condiciones, Tipo.Producto, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);

                if (atencionProductoAsociado == null)
                {
                    if (!detalle.Excluido && detalle.CondicionSeparacion.NoCubrimiento == false)
                    {
                        foreach (var componente in detalle.VentaComponentes)
                        {
                            componente.FechaVenta = detalle.FechaVenta;
                            componente.HoraVenta = detalle.HoraVenta;

                            // Obtener valor de la venta
                            if (componente.ValorUnitario == 0)
                            {
                                componente.ValorUnitario = componente.ValorOriginal;
                            }

                            this.ValidarReglasProductoComponente(tipoFacturacion, detalle, atencion, componente, condiciones, Tipo.Componente, condicionContrato, aplicaCondicionFactura, detalleVinculacion, ref componenteVinculacion);
                            componente.CodigoProducto = detalle.CodigoProducto;
                            componente.NombreProducto = detalle.NombreProducto;
                            componente.CodigoGrupo = detalle.CodigoGrupo;
                            componente.NombreGrupo = detalle.NombreGrupo;

                            this.CalcularValorComponente(atencion, detalle, componente, condicionContrato, condiciones);

                            if (componente.CantidadComponente - componente.CantidadComponenteFacturada == 0)
                            {
                                componente.CondicionSeparacion.Omitir = true;
                            }
                            else if (componente.CantidadComponente - componente.CantidadComponenteFacturada != componente.CantidadProducto && componente.CantidadComponenteFacturada != 0)
                            {
                                componente.CantidadProducto = componente.CantidadComponente - componente.CantidadComponenteFacturada;
                            }
                        }
                    }
                }

                this.CalcularValorProducto(detalle, condicionContrato);
                detalle.FechaVigenciaContrato = condicionContrato.FechaVigenciaResultado;
                condicionContrato = condicionContratoBase;
                var productoVinculacion = detalle.CopiarObjeto();
                productoVinculacion.VentaComponentes.RemoveAll(a => a.CondicionSeparacion.Omitir == true);

                if (componenteVinculacion != null && componenteVinculacion.Count > 0)
                {
                    var detalleSeparar = detalle.CopiarObjeto();

                    var detalleS = this.BuscarDetalle(detalleVinculacion, detalleSeparar);
                    if (detalleS == null)
                    {
                        var componenteProducto = from item in componentesProductos
                                                 where item.NumeroVenta == detalle.NumeroVenta
                                                 && item.IdProducto == detalle.IdProducto
                                                 select item;

                        var ventCompo = from item in componenteVinculacion
                                        select item;

                        porcentaje = ventCompo.Sum(c => c.CantidadFacturar) / componenteProducto.Sum(c => c.CantidadComponente);
                        detalleSeparar.CantidadProducto = Math.Round(porcentaje, 2);

                        detalleSeparar.VentaComponentes.Clear();
                        detalleSeparar.VentaComponentes = componenteVinculacion;

                        if (detalleSeparar.CantidadFacturar == 0)
                        {
                            detalleSeparar.CondicionSeparacion.Omitir = true;
                        }

                        detalleVinculacion.Add(detalleSeparar);
                    }
                    else
                    {
                        detalleS.VentaComponentes.Clear();
                        detalleS.VentaComponentes = componenteVinculacion;
                    }

                    componenteVinculacion = null;
                }

                if (productoVinculacion.esPaquete && productoVinculacion.CondicionSeparacion != null && productoVinculacion.CondicionSeparacion.NoCubrimiento)
                {
                    productoVinculacion.CondicionSeparacion.NoCubrimiento = false;
                }

                if (productoVinculacion.CondicionSeparacion.NoCubrimiento == false)
                {
                    if (productoVinculacion.VentaComponentes.Count > 0)
                    {
                        if (productoVinculacion.CantidadProductoFacturada > 0 && (productoVinculacion.CodigoGrupo == "PRQX" || productoVinculacion.CodigoGrupo == "ANGP"))
                        {
                            foreach (VentaComponente vc in productoVinculacion.VentaComponentes)
                            {
                                if (vc.CantidadComponente - vc.CantidadComponenteFacturada == 0)
                                {
                                    vc.CondicionSeparacion.Omitir = true;
                                }
                            }

                            productoVinculacion.CantidadProducto = productoVinculacion.CantidadFacturar;
                            productoVinculacion.CantidadProductoFacturada = 0;
                        }
                        else
                        {
                            var componenteProducto = from item in componentesProductos
                                                     where item.NumeroVenta == detalle.NumeroVenta
                                                     && item.IdProducto == detalle.IdProducto
                                                     select item;

                            var ventCompo = from item in productoVinculacion.VentaComponentes
                                            select item;

                            porcentaje = ventCompo.Sum(c => c.CantidadFacturar) / componenteProducto.Sum(c => c.CantidadComponente);
                            productoVinculacion.CantidadProducto = Math.Round(porcentaje, 2);
                        }
                    }

                    detalleActual.Add(productoVinculacion);
                }
            }

            // Cierra el  for mirar la lista VentaComponentes
            if (detalleActual.Count > 0)
            {
                atencion.Detalle = detalleActual;
            }

            List<FacturaAtencionDetalle> listaProductoVentas = new List<FacturaAtencionDetalle>();
            List<FacturaAtencionDetalle> listaProductoFactorQX = atencion.Detalle.CopiarObjeto();
            var productosQx = from item in atencion.Detalle
                              where item.TipoProductos == TipoProd.Quirurjicos
                              select item;
            int ventaActual = 0;
            foreach (var itemDetalle in productosQx)
            {
                if (ventaActual == 0)
                {
                    ventaActual = itemDetalle.NumeroVenta;

                    listaProductoVentas = (from item in productosQx.CopiarObjeto()
                                           where item.NumeroVenta == ventaActual && item.TipoProductos == TipoProd.Quirurjicos
                                           select item).ToList();
                    this.OrdenarFactor(listaProductoVentas, componentesProductos);

                    listaProductoFactorQX.RemoveAll(item => item.NumeroVenta == ventaActual);

                    foreach (var detallesVenta in listaProductoVentas.OrderBy(item => item.OrdenQX))
                    {
                        foreach (var componenteVenta in detallesVenta.VentaComponentes)
                        {
                            componenteVenta.OrdenQX = detallesVenta.OrdenQX;
                            this.OrdenFactorQX = detallesVenta.OrdenQX;
                            this.AplicarReglaFactoresQX(atencion, componenteVenta);
                            this.CalcularValorComponenteFactQX(componenteVenta, detallesVenta, atencion.IdContrato);
                        }

                        listaProductoFactorQX.Add(detallesVenta);
                    }
                }
                else if (ventaActual > 0 && ventaActual != itemDetalle.NumeroVenta)
                {
                    ventaActual = itemDetalle.NumeroVenta;

                    listaProductoVentas = (from item in productosQx.CopiarObjeto()
                                           where item.NumeroVenta == ventaActual && item.TipoProductos == TipoProd.Quirurjicos
                                           select item).ToList();
                    this.OrdenarFactor(listaProductoVentas, componentesProductos);

                    listaProductoFactorQX.RemoveAll(item => item.NumeroVenta == ventaActual);

                    foreach (var detallesVenta in listaProductoVentas.OrderBy(item => item.OrdenQX))
                    {
                        foreach (var componenteVenta in detallesVenta.VentaComponentes)
                        {
                            componenteVenta.OrdenQX = detallesVenta.OrdenQX;
                            this.OrdenFactorQX = detallesVenta.OrdenQX;
                            this.AplicarReglaFactoresQX(atencion, componenteVenta);
                            this.CalcularValorComponenteFactQX(componenteVenta, detallesVenta, atencion.IdContrato);
                        }

                        listaProductoFactorQX.Add(detallesVenta);
                    }
                }
            }

            atencion.Detalle.Clear();
            List<FacturaAtencionDetalle> detalleOrdenado = (from fe in listaProductoFactorQX.CopiarObjeto() orderby fe.FechaVenta ascending, fe.HoraVenta ascending select fe).ToList();
            atencion.Detalle = detalleOrdenado;

            // Aplicar condición de factura
            if (condicionFactura != null && condicionFactura.Id > 0)
            {
                foreach (var itemDetalle in detalleOrdenado)
                {
                    if (itemDetalle != null && itemDetalle.VentaComponentes != null && itemDetalle.VentaComponentes.Count > 0)
                    {
                        this.CalcularValorProductoComponente(itemDetalle);
                    }

                    if (condicionFactura.ValorPropio >= 1
                        && (itemDetalle.VentaComponentes == null || itemDetalle.VentaComponentes.Count == 0)
                        && (itemDetalle.CondicionSeparacion.NoCubrimiento == false)
                        && (itemDetalle.CondicionSeparacion.Omitir == false)
                        && ((itemDetalle.ValorBruto == 0 ? itemDetalle.ValorOriginal : itemDetalle.ValorBruto) > 0))
                    {
                        if ((itemDetalle.TipoProductos == TipoProd.Medicamentos || itemDetalle.TipoProductos == TipoProd.Insumos) && (!itemDetalle.Excluido))
                        {
                            this.AplicarCondicionesFactura(itemDetalle, condicionFactura, productoVinculacionActual, detalleVinculacion);
                        }
                        else
                        {
                            if ((condicionFactura.ValorPropio >= (itemDetalle.ValorBruto == 0 ? itemDetalle.ValorOriginal : itemDetalle.ValorBruto) * itemDetalle.CantidadFacturar) && (!itemDetalle.Excluido))
                            {
                                condicionFactura.ValorPropio -= itemDetalle.ValorBruto *
                                                                itemDetalle.CantidadFacturar;
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else if (itemDetalle.Excluido)
                            {
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else
                            {
                                detalleVinculacion.Add(itemDetalle);
                                productoVinculacionActual.Remove(itemDetalle);
                            }
                        }
                    }
                    else if (condicionFactura.ValorPropio >= 1
                             && (itemDetalle.CondicionSeparacion.NoCubrimiento == false)
                             && (itemDetalle.CondicionSeparacion.Omitir == false))
                    {
                        FacturaAtencionDetalle productoCalculado = new FacturaAtencionDetalle();
                        productoCalculado = itemDetalle.CopiarObjeto();

                        if (productoCalculado != null && productoCalculado.VentaComponentes != null &&
                            productoCalculado.VentaComponentes.Count > 0)
                        {
                            this.CalcularValorProductoComponente(productoCalculado);
                        }

                        if ((condicionFactura.ValorPropio >=
                             (productoCalculado.ValorBruto == 0
                                 ? productoCalculado.ValorOriginal
                                 : productoCalculado.ValorBruto)) && (!itemDetalle.Excluido))
                        {
                            condicionFactura.ValorPropio -= productoCalculado.ValorBruto;
                            productoVinculacionActual.Add(productoCalculado);
                        }
                        else if (itemDetalle.Excluido)
                        {
                            productoVinculacionActual.Add(itemDetalle);
                        }
                        else
                        {
                            detalleVinculacion.Add(itemDetalle);
                            productoVinculacionActual.Remove(itemDetalle);
                        }
                    }

                    if (productoVinculacionActual.Count > 0)
                    {
                        var proVinculacionActual = this.BuscarDetalle(productoVinculacionActual, itemDetalle);
                        var proOtraVinculacion = this.BuscarDetalle(detalleVinculacion, itemDetalle);
                        if (proVinculacionActual == null && proOtraVinculacion == null)
                        {
                            if (itemDetalle.Excluido)
                            {
                                productoVinculacionActual.Add(itemDetalle);
                            }
                            else
                            {
                                detalleVinculacion.Add(itemDetalle);
                            }
                        }
                    }
                }

                atencion.Detalle = productoVinculacionActual;
            }
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para el Recargo del Contrato.
        /// </summary>
        /// <param name="recargo">The recargo contrato.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarRecargoContrato(Recargo recargo, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((recargo.IdAtencion > 0 && !recargo.IdAtencion.Equals(atencion.IdAtencion)) ||
                (recargo.IdProducto > 0 && !recargo.IdProducto.Equals(detalle.IdProducto)) ||
                (recargo.IdGrupoProducto > 0 && !recargo.IdGrupoProducto.Equals(detalle.IdGrupoProducto)) ||
                (recargo.IdTipoProducto > 0 && !recargo.IdTipoProducto.Equals(detalle.IdTipoProducto)) ||
                (recargo.IdContrato > 0 && !recargo.IdContrato.Equals(atencion.IdContrato)) ||
                (recargo.IdServicio > 0 && !recargo.IdServicio.Equals(atencion.IdServicio)) ||
                (recargo.IdPlan > 0 && !recargo.IdPlan.Equals(detalle.IdPlan)) ||
                (recargo.IdTipoAtencion > 0 && !recargo.IdTipoAtencion.Equals(atencion.IdTipoAtencion)) ||
                (recargo.Componente != "NA" && !recargo.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Valida Si Cumple las Especificaciones Para el Recargo del Manual.
        /// </summary>
        /// <param name="recargoManual">The recargo manual.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Indica Si Cumple los requesitos para Continuar.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidarRecargoManual(RecargoManual recargoManual, FacturaAtencion atencion, BaseValidacion detalle)
        {
            bool continuarProceso = true;
            if ((recargoManual.IdAtencion > 0 && !recargoManual.IdAtencion.Equals(atencion.IdAtencion))
                || (recargoManual.IdProducto > 0 && !recargoManual.IdProducto.Equals(detalle.IdProducto))
                || (recargoManual.IdGrupoProducto > 0 && !recargoManual.IdGrupoProducto.Equals(detalle.IdGrupoProducto))
                || (recargoManual.IdTipoProducto > 0 && !recargoManual.IdTipoProducto.Equals(detalle.IdTipoProducto))
                || (recargoManual.IdContrato > 0 && !recargoManual.IdContrato.Equals(atencion.IdContrato))
                || (recargoManual.IdServicio > 0 && !recargoManual.IdServicio.Equals(atencion.IdServicio))
                || (recargoManual.IdPlan > 0 && !recargoManual.IdPlan.Equals(detalle.IdPlan))
                || (recargoManual.IdTipoAtencion > 0 && !recargoManual.IdTipoAtencion.Equals(atencion.IdTipoAtencion))
                || (recargoManual.Componente != "NA" && !recargoManual.Componente.Equals(detalle.Componente)))
            {
                continuarProceso = false;
            }

            return continuarProceso;
        }

        /// <summary>
        /// Metodo para aplicar validaci n por producto o por componente.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="atencion">The atencion.</param>
        /// <param name="baseValidacion">The base validacion.</param>
        /// <param name="condiciones">The condiciones.</param>
        /// <param name="tipo">Parametro tipo.</param>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <param name="aplicaCondicionFactura">If set to <c>true</c> [aplica condicion factura].</param>
        /// <param name="detalleVinculacion">The detalle vinculacion.</param>
        /// <param name="componenteVinculacion">The componente vinculacion.</param>
        /// <returns>
        /// Condicion Aplicada al Producto principal.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 16/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private CondicionContrato ValidarReglasProductoComponente(TipoFacturacion tipoFacturacion, FacturaAtencionDetalle detalle, FacturaAtencion atencion, BaseValidacion baseValidacion, List<CondicionProceso> condiciones, Tipo tipo, CondicionContrato condicionContrato, bool aplicaCondicionFactura, List<FacturaAtencionDetalle> detalleVinculacion, ref List<VentaComponente> componenteVinculacion)
        {
            bool aplicaExclusion = false;
            Cubrimiento cubrimiento = new Cubrimiento();
            try
            {

                List<String> codigosPaquetes = this.ConsultarGrupoPaquetes();
                bool validarGrupo = false;

                foreach (string item in codigosPaquetes)
                {
                    if (baseValidacion.CodigoGrupo == item)
                    {
                        validarGrupo = true;
                        break;
                    }
                }

                // Homologación de productos
                if (!validarGrupo)
                {
                    this.RegistrarHomologacion(baseValidacion, condicionContrato, tipo);
                }

                // 2. Exclusiones del contrato
                aplicaExclusion = this.AplicarReglaExclusionContrato(condiciones, atencion, baseValidacion);

                if (!aplicaExclusion)
                {
                    ////  3. Exclusiones del manual
                    switch (baseValidacion.TipoProductos)
                    {
                        case TipoProd.Quirurjicos:
                            aplicaExclusion = this.AplicarReglaExclusionManualVentaAsociada(condiciones, atencion, baseValidacion, tipoFacturacion);
                            break;

                        default:
                            aplicaExclusion = this.AplicarReglaExclusionManual(condiciones, atencion, baseValidacion, tipoFacturacion);
                            break;
                    }
                }

                if (aplicaExclusion)
                {
                    ////baseValidacion.CondicionSeparacion.Omitir = true;
                    baseValidacion.Excluido = true;
                    return condicionContrato;
                }

                // 6. Recargo del contrato
                bool recargoAplicado = this.AplicarReglaRecargosContrato(condiciones, atencion, baseValidacion);

                if (!recargoAplicado)
                {
                    // 7. Recargo del manual
                    this.AplicarReglaRecargosManual(condiciones, atencion, baseValidacion);
                }

                // Validar reglas condiciones de tarifa
                var condTarifaCubrimiento = this.ReglasCondicionesTarifaCubrimiento(tipo, ref condicionContrato, condiciones, atencion, baseValidacion, tipoFacturacion);

                if (condTarifaCubrimiento != null && condTarifaCubrimiento.CondicionSeparacion.NoCubrimiento == true)
                {
                    if (condTarifaCubrimiento is FacturaAtencionDetalle)
                    {
                        detalleVinculacion.Add(condTarifaCubrimiento as FacturaAtencionDetalle);
                    }
                    else
                    {
                        componenteVinculacion.Add(condTarifaCubrimiento as VentaComponente);
                    }

                    return condicionContrato;
                }

                // 5. Niveles de complejidad y/o Costos asociados
                if (tipo == Tipo.Componente)
                {
                    var ventaComponente = baseValidacion as VentaComponente;
                    this.EvaluarNivelesComplejidadCostosAsociados(atencion, condicionContrato, ventaComponente, condiciones, detalle);
                    this.CalcularValorComponente(atencion, detalle, ventaComponente, condicionContrato, condiciones);
                }
                else
                {
                    this.CalcularValorProducto(baseValidacion as FacturaAtencionDetalle, condicionContrato);
                }

                // 9. Condiciones de cubrimiento
                if (!aplicaCondicionFactura)
                {
                    var cubrimientos = this.CargarInformacionTipo(condiciones, CondicionProceso.TipoObjeto.DefinirCubrimiento);

                    if (cubrimientos.Objeto != null)
                    {
                        baseValidacion.Cubrimiento = this.ObtenerCubrimientos(cubrimientos.Objeto as IEnumerable<Cubrimiento>, baseValidacion, atencion);

                        if (baseValidacion.Cubrimiento != null)
                        {
                            baseValidacion.Cubrimiento.CondicionesCubrimiento = this.AplicarReglaCondicionCubrimiento(condiciones, atencion, baseValidacion, baseValidacion.Cubrimiento.IdClaseCubrimiento);

                            if (atencion.Cubrimientos == null)
                            {
                                atencion.Cubrimientos = new List<Cubrimiento>();
                            }

                            atencion.Cubrimientos.Add(baseValidacion.Cubrimiento);

                            // Aplicar reglas condiciones de cubrimiento
                            this.ReglasCondicionesCubrimiento(atencion, baseValidacion, detalleVinculacion, ref componenteVinculacion, detalle);
                        }
                    }
                    else
                    {
                        baseValidacion.CondicionSeparacion.Omitir = true;
                    }
                }

                // Validar cantidad o valor maximo
                this.ReglasCondicionesTarifaCantidadValor(atencion, baseValidacion, detalleVinculacion, ref componenteVinculacion);

                // 8. Descuentos
                this.AplicarReglaDescuentos(condiciones, atencion, baseValidacion);

                if (tipo == Tipo.Componente)
                {
                    var ventaComponente = baseValidacion as VentaComponente;
                    this.CalcularValorComponente(atencion, detalle, ventaComponente, condicionContrato, condiciones);
                }
                else
                {
                    this.CalcularValorProducto(baseValidacion as FacturaAtencionDetalle, condicionContrato);
                }

                return condicionContrato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para borrar las Ventas No Marcadas.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 03/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void VentasNoMarcadas(ProcesoFactura procesoFactura, FacturaAtencion atencion)
        {
            FacturaAtencionDetalle itemEncontrado = null;
            atencion.VentasNoMarcadas = procesoFactura.VentasNoMarcadas;
            if (atencion.VentasNoMarcadas != null && atencion.VentasNoMarcadas.Count > 0)
            {
                var detalles = atencion.Detalle.CopiarObjeto();

                foreach (var detalle in detalles)
                {
                    itemEncontrado = null;

                    if (this.ValidarNumerosVenta(atencion.VentasNoMarcadas, detalle.NumeroVenta))
                    {
                        itemEncontrado = this.BuscarDetalle(atencion.Detalle, detalle);
                        atencion.Detalle.Remove(itemEncontrado);
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para borrar las Ventas No Marcadas.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="atencion">The atencion.</param>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 03/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private void VentasSeleccionadas(ProcesoFactura procesoFactura, FacturaAtencion atencion)
        {
            if (procesoFactura.VentasSeleccionadas != null && procesoFactura.VentasSeleccionadas.Count > 0)
            {
                var detalles = atencion.Detalle.CopiarObjeto().OrderBy(a => a.FechaVenta);
                atencion.Detalle.Clear();
                foreach (var detalle in detalles)
                {
                    if (this.ValidarNumerosVenta(procesoFactura.VentasSeleccionadas, detalle.NumeroVenta))
                    {
                        atencion.Detalle.Add(detalle);
                    }
                }
            }
        }

        /// <summary>
        /// Verificación Aux Inicio.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="objFactura">The object factura.</param>
        /// <returns>Listado Estado Cuentas.</returns>
        /// <exception cref="System.Exception">
        /// Debe seleccionar un paquete que contenga productos.
        /// or
        /// Debe seleccionar un paquete que contenga productos.
        /// or
        /// Debe seleccionar un paquete que contenga productos.
        /// </exception>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private List<EstadoCuentaEncabezado> VerificacioinAuxInicio(ProcesoFactura procesoFactura, FacturaAtencion objFactura)
        {
            //// primero se arma la lista con las ventas y productos que quedaron por fuera del paquete 
            //// esta lista se arma cruzando lo que el usuario dejo por fuera del paquete con los datos
            //// que vienen de la base de datos de facturacion para completar la informacion
            List<FacturaAtencionDetalle> listFacturaAtencionDetalle = objFactura.Detalle;

            var productosPaquete = new List<FacturaAtencionDetalle>();
            var productosPaqueteAux = new List<FacturaAtencionDetalle>();

            //// lquinterom: para facturacion por paquetes se toma la consulta realizada almacenada en objFactura de ventas y productos 
            //// y se cruza contra el paquete que se armo de esta armamos un solo objeto para procesarlo y generar estado de cuenta
            if (procesoFactura.TipoFactura == TipoFacturacion.FacturacionPaquete)
            {
                if (procesoFactura.Paquetes == null)
                {
                    throw new Exception("Debe seleccionar un paquete que contenga productos.");
                }
                else if (procesoFactura.Paquetes.Count() == 0)
                {
                    throw new Exception("Debe seleccionar un paquete que contenga productos.");
                }

                foreach (var item in procesoFactura.Paquetes)
                {
                    if (item.Productos.Count == 0)
                    {
                        throw new Exception("Debe seleccionar un paquete que contenga productos.");
                    }
                }

                var productosFueraPaquete = (from pv in listFacturaAtencionDetalle
                                             join pfp in procesoFactura.ProductosFueraPaquete on new { pv.IdProducto, pv.NumeroVenta } equals new { pfp.IdProducto, pfp.NumeroVenta }
                                             select pv).ToList().CopiarObjeto();

                productosFueraPaquete.ForEach(c =>
                {
                    ProductoVenta pv = procesoFactura.ProductosFueraPaquete.Where(d => (d.IdProducto == c.IdProducto) &&
                                                                                       (d.NumeroVenta == c.NumeroVenta)).FirstOrDefault();
                    if (pv != null)
                    {
                        c.CantidadProducto = pv.CantidadDisponible;
                        c.CantidadProductoFacturada = 0;
                        c.esPaquete = false;
                    }
                });

                // asignamos los el id del paquete a los productos que fueron empaquetados
                procesoFactura.Paquetes.ForEach(c => c.Productos.ForEach(d => d.IdPaquete = c.IdPaquete));

                //// despues se arma la lista con las ventas y productos que quedaron dentro del paquete 
                //// esta lista se arma cruzando lo que el usuario dejo por fuera del paquete con los datos
                //// que vienen de la base de datos de facturacion para completar la informacion

                foreach (Paquete paq in procesoFactura.Paquetes)
                {
                    productosPaqueteAux = (from pv in listFacturaAtencionDetalle
                                           join pp in paq.Productos on new { pv.IdProducto, pv.NumeroVenta } equals new { pp.IdProducto, pp.NumeroVenta }
                                           select pv).ToList().CopiarObjeto();

                    productosPaqueteAux.ForEach(c =>
                    {
                        PaqueteProducto prod = paq.Productos.Where(d => (d.IdProducto == c.IdProducto) &&
                                                                        (d.NumeroVenta == c.NumeroVenta) &&
                                                                         (c.idPaquete == 0)).FirstOrDefault();
                        if (prod != null)
                        {
                            c.CantidadProducto = prod.CantidadAsignada;
                            c.CantidadProductoFacturada = 0;
                            c.esPaquete = true;
                            c.EsPaquete = true;
                            c.idPaquete = paq.IdPaquete;
                        }
                    });

                    productosPaquete.AddRange(productosPaqueteAux);
                }

                //// unimos ambas listas en una y con esta hacemos todo el procesamiento necesario para el estado de cuenta
                listFacturaAtencionDetalle = new List<FacturaAtencionDetalle>();
                listFacturaAtencionDetalle.AddRange(productosFueraPaquete);
                listFacturaAtencionDetalle.AddRange(productosPaquete);
                objFactura.Detalle = listFacturaAtencionDetalle;
            }

            List<EstadoCuentaEncabezado> listaResultado = this.VerificacioinAuxProceso(procesoFactura, objFactura);

            return listaResultado;
        }

        /// <summary>
        /// Verificación Aux Proceso.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <param name="objFactura">The object factura.</param>
        /// <returns>Listado Estado Cuentas.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private List<EstadoCuentaEncabezado> VerificacioinAuxProceso(ProcesoFactura procesoFactura, FacturaAtencion objFactura)
        {
            EstadoCuentaEncabezado resultado = null;
            List<FacturaAtencionDetalle> detallesSeparar = new List<FacturaAtencionDetalle>();
            List<FacturaAtencionDetalle> listaFinal = new List<FacturaAtencionDetalle>();
            List<VentaComponente> listaFinalComponente = new List<VentaComponente>();
            List<EstadoCuentaEncabezado> listaResultado = new List<EstadoCuentaEncabezado>();

            try
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Procesando;
                this.ActualizarEstadoProcesoFactura(procesoFactura);

                foreach (var vinculacion in procesoFactura.Vinculaciones)
                {
                    FacturaAtencion atencion = objFactura.CopiarObjeto();

                    procesoFactura.IdTercero = vinculacion.Tercero.Id;
                    procesoFactura.IdContrato = vinculacion.Contrato.Id;
                    objFactura.IdManual = vinculacion.IdManual;
                    objFactura.IdTercero = vinculacion.Tercero.Id;
                    objFactura.IdContrato = vinculacion.Contrato.Id;
                    atencion.IdPlan = vinculacion.Plan.Id;

                    if (vinculacion.Orden != 1)
                    {
                        atencion.IdContrato = vinculacion.Contrato.Id;
                        atencion.IdManual = vinculacion.IdManual;
                        atencion.IdTercero = vinculacion.Tercero.Id;

                        if (detallesSeparar != null && detallesSeparar.Count > 0)
                        {
                            detallesSeparar.RemoveAll(a => a.CondicionSeparacion.Omitir == true || a.esPaquete == true);

                            foreach (FacturaAtencionDetalle item in detallesSeparar)
                            {
                                var itemModificar = this.BuscarDetalle(detallesSeparar, item);
                                if (itemModificar != null)
                                {
                                    itemModificar.CondicionSeparacion.NoCubrimiento = false;

                                    itemModificar.VentaComponentes.RemoveAll(a => a.CondicionSeparacion.Omitir == true);

                                    foreach (var itemComponente in itemModificar.VentaComponentes)
                                    {
                                        itemComponente.CondicionSeparacion.NoCubrimiento = false;
                                    }
                                }
                            }

                            objFactura.Detalle = detallesSeparar.CopiarObjeto();
                            atencion.Detalle = objFactura.Detalle;
                            detallesSeparar = null;
                        }
                        else
                        {
                            atencion.Detalle = null;
                        }
                    }

                    if (atencion.Detalle != null && atencion.Detalle.Count > 0)
                    {
                        resultado = this.ProcesoFacturaGeneralAtencionPaquetes(procesoFactura, atencion, vinculacion, out detallesSeparar);

                        listaFinal = atencion.Detalle.CopiarObjeto();
                        foreach (FacturaAtencionDetalle item in atencion.Detalle)
                        {
                            var detalleS = this.BuscarDetalle(detallesSeparar, item);
                            if ((detalleS == null) && item.CondicionSeparacion.NoCubrimiento == true)
                            {
                                item.CondicionSeparacion.Omitir = false;
                                detallesSeparar.Add(item);
                            }

                            foreach (var itemComp in item.VentaComponentes)
                            {
                                if (detalleS == null && itemComp.CondicionSeparacion.NoCubrimiento == true)
                                {
                                    item.CondicionSeparacion.Omitir = false;
                                    detallesSeparar.Add(item);
                                    break;
                                }
                            }
                        }

                        listaFinal.RemoveAll(a => a.CondicionSeparacion.Omitir == true || a.CondicionSeparacion.NoCubrimiento == true || ((a.VentaComponentes == null || a.VentaComponentes.Count == 0) && a.CantidadProducto == 0));
                        atencion.Detalle = listaFinal;

                        foreach (FacturaAtencionDetalle item in atencion.Detalle)
                        {
                            listaFinalComponente = item.VentaComponentes.CopiarObjeto();
                            foreach (var itemComp in item.VentaComponentes)
                            {
                                listaFinalComponente.RemoveAll(a => (a.ValorUnitario == 0 || a.CondicionSeparacion.NoCubrimiento == true || a.CantidadProducto == 0) && a.Excluido == false);
                            }

                            item.VentaComponentes = listaFinalComponente;
                        }

                        if (listaFinal != null && listaFinal.Count > 0)
                        {
                            if (vinculacion.IndGenerar == 1)
                            {
                                resultado.Observaciones = vinculacion.Observacion;
                                listaResultado.Add(resultado);
                            }
                        }
                    }
                }

                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.Finalizado;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
            }
            catch (Exception ex)
            {
                procesoFactura.IdEstado = (byte)ProcesoFactura.EstadoProceso.ErrorGenerandoEstadoCuenta;
                this.ActualizarEstadoProcesoFactura(procesoFactura);
                throw ex;
            }

            return listaResultado;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}