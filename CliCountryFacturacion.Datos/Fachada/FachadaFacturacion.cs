// --------------------------------
// <copyright file="FachadaFacturacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Fachada Configuracion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.Fachada
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using CliCountry.Facturacion.Datos.DAO;
    using CliCountry.Facturacion.Datos.Recursos;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.SAHI.Comun.Linq;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Auditoria;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using CliCountry.SAHI.Dominio.Recursos;

    /// <summary>
    /// Se encarga del llamado de la persistencia de datos relacionados con las causales de anulación.
    /// </summary>
    public class FachadaFacturacion
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// Referencia a la clase que resuelve las necesidades de datos de facturacion.
        /// </summary>
        private DAOFacturacion daoFacturacion = new DAOFacturacion(OperacionesBaseDatos.ConexionFacturacion);

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Actualizas the estado venta paquetes.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        public void ActualizaEstadoVentaPaquetes(string numeroFactura)
        {
            this.daoFacturacion.ActualizaEstadoVentaPaquetes(numeroFactura);
        }

        /// <summary>
        /// Metodo para realizar la actualización dela Atencion.
        /// </summary>
        /// <param name="atencionCliente">The atencion cliente.</param>
        /// <returns>Indica el Resultado de la Actualizacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarAtencion(AtencionCliente atencionCliente)
        {
            var resultado = this.daoFacturacion.ActualizarAtencion(atencionCliente);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Actualiza la venta con el usuario para bloquear o desbloquear la atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Filas Afectadas.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom.
        /// FechaDeCreacion: 07/05/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarBloquearAtencion(int identificadorAtencion, string usuario)
        {
            return this.daoFacturacion.ActualizarBloquearAtencion(identificadorAtencion, usuario);
        }

        /// <summary>
        /// Método para actualizar el indicador del concepto en contabilidad de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Número de la factura anulada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarConceptoContabilidad(NotaCredito notaCredito)
        {
            return this.daoFacturacion.ActualizarConceptoContabilidad(notaCredito);
        }

        /// <summary>
        /// Metodo para actualizar Conceptos de Cobro.
        /// </summary>
        /// <param name="conceptoCobro">The concepto cobro.</param>
        /// <returns>Indica si se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarConceptosCobro(FacturaAtencionConceptoCobro conceptoCobro)
        {
            var resultado = this.daoFacturacion.ActualizarConceptosCobro(conceptoCobro);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Actualiza la información de la condición de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Id condición cubrimiento actualizado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            return this.daoFacturacion.ActualizarCondicionCubrimiento(condicionCubrimiento);
        }

        /// <summary>
        /// Metodo para realizar la actualización de Condicion de Tarifa.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Indica si Se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarCondicionTarifa(CondicionTarifa condicionTarifa)
        {
            return this.daoFacturacion.ActualizarCondicionTarifa(condicionTarifa);
        }

        /// <summary>
        /// Método para actualizar el convenio.
        /// </summary>
        /// <param name="convenioNoClinico">The convenio no clinico.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 09/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarConvenioNoClinico(ConvenioNoClinico convenioNoClinico)
        {
            return this.daoFacturacion.ActualizarConvenioNoClinico(convenioNoClinico);
        }

        /// <summary>
        /// Actualiza la información del cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Id cubrimiento actualizado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarCubrimientos(Cubrimiento cubrimiento)
        {
            return this.daoFacturacion.ActualizarCubrimientos(cubrimiento);
        }

        /// <summary>
        /// Metodo para realizar la actualizacion del descuento.
        /// </summary>
        /// <param name="descuento">The descuentos.</param>
        /// <returns>Indica si se afecto el registro en la base de datos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarDescuento(DescuentoConfiguracion descuento)
        {
            var resultado = this.daoFacturacion.ActualizarDescuento(descuento);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Método para actualizar el estado de la cuenta de cartera de l afactura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Número de la nota credito.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarEstadoCuentaCartera(NotaCredito notaCredito)
        {
            return this.daoFacturacion.ActualizarEstadoCuentaCartera(notaCredito);
        }

        /// <summary>
        /// Método para realizar la actualización del estado de la factura a eliminado.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Numero de factura eliminada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarEstadoFactura(NotaCredito notaCredito)
        {
            return this.daoFacturacion.ActualizarEstadoFactura(notaCredito);
        }

        /// <summary>
        /// Metodo Para Actualizar el Estado de proceso.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>Indica Si Se Actualiza el estado de proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarEstadoProcesoFactura(ProcesoFactura procesoFactura)
        {
            int valor = this.daoFacturacion.ActualizarEstadoProcesoFactura(procesoFactura);
            return valor > 0 ? true : false;
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
            int valor = this.daoFacturacion.ActualizarEstadoProcesoFacturaNC(procesoFactura);
            return valor > 0 ? true : false;
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
            return this.daoFacturacion.ActualizarEstadoVentaAnulacion(notacredito, numeroVenta);
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
            return this.daoFacturacion.ActualizarEstadoVentaAnulacionNC(notacredito);
        }

        /// <summary>
        /// Método que actualiza el estado de las ventas.
        /// </summary>
        /// <param name="ventaEstadoFactura">The detalle factura.</param>
        /// <returns>Devuelve 1 si la operación se ejecutó con exito, 0 si es lo contrario.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarEstadoVentaFactura(VentaEstadoFactura ventaEstadoFactura)
        {
            return this.daoFacturacion.ActualizarEstadoVentaFactura(ventaEstadoFactura);
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
            return this.daoFacturacion.ActualizarEstadoVentaFacturaNC(ventaEstadoFactura);
        }

        /// <summary>
        /// Método que Actualiza y/o elimina información de ventas de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 02/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarEstadoVentasAnulacion(NotaCredito notaCredito)
        {
            return this.daoFacturacion.ActualizarEstadoVentasAnulacion(notaCredito);
        }

        /// <summary>
        /// Método que actualiza l información de la exclusión del contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de la exclusión actualizada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarExclusionContrato(Exclusion exclusion)
        {
            return this.daoFacturacion.ActualizarExclusionContrato(exclusion);
        }

        /// <summary>
        /// Método que actualiza el Id de la cuenta en la factura.
        /// </summary>
        /// <param name="identificadorCuenta">The id cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Indica si se actualizo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarIdCuentaFactura(int identificadorCuenta, int numeroFactura)
        {
            return this.daoFacturacion.ActualizarIdCuentaFactura(identificadorCuenta, numeroFactura);
        }

        /// <summary>
        /// Metodo para actualizar Movimientos en tesoreria.
        /// </summary>
        /// <param name="movimiento">The movimiento.</param>
        /// <returns>Indica si se realiza la actualización.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public bool ActualizarMovimientosTesoreria(FacturaAtencionMovimiento movimiento)
        {
            var resultado = this.daoFacturacion.ActualizarMovimientosTesoreria(movimiento);
            return resultado > 0 ? true : false;
        }

        /// <summary>
        /// Método que actualiza el número de factura en cuenta cartera.
        /// </summary>
        /// <param name="identificadorCuenta">The id cuenta.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Indica si se actualizo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 12/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarNumeroFacturaCuentaCartera(int identificadorCuenta, int numeroFactura)
        {
            return this.daoFacturacion.ActualizarNumeroFacturaCuentaCartera(identificadorCuenta, numeroFactura);
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
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarRecargo(Recargo recargo)
        {
            return this.daoFacturacion.ActualizarRecargo(recargo);
        }

        /// <summary>
        /// Método para actualizar los saldos afectados en los movimientos de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Número de factura anulada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarSaldosMovimientos(NotaCredito notaCredito)
        {
            return this.daoFacturacion.ActualizarSaldosMovimientos(notaCredito);
        }

        /// <summary>
        /// Metodo para actualizar ventas asociada.
        /// </summary>
        /// <param name="ventaRelacion">The venta relacion.</param>
        /// <returns>
        /// Id del registro modificado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarVentaProductoRelacion(VentaProductoRelacion ventaRelacion)
        {
            return this.daoFacturacion.ActualizarVentaProductoRelacion(ventaRelacion);
        }

        /// <summary>
        /// Método para actualizar una vinculación.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Registro actualizado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarVinculacion(Vinculacion vinculacion)
        {
            return this.daoFacturacion.ActualizarVinculacion(vinculacion);
        }

        /// <summary>
        /// Metodo de actualizacion de la vinculacion de la venta.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Indica Si Se realiza la actualizacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarVinculacionVentas(VinculacionVenta vinculacion)
        {
            return this.daoFacturacion.ActualizarVinculacionVentas(vinculacion);
        }

        /// <summary>
        /// Realiza la anulación de una venta no clínica.
        /// </summary>
        /// <param name="key">Id de la Venta.</param>
        /// <param name="value">Id de la transacción.</param>
        /// <returns>Cadena con el resultado de la operación.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 14/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public string AnularVentaNoClinica(int key, int value)
        {
            return this.daoFacturacion.AnularVentaNoClinica(key, value);
        }

        /// <summary>
        /// Método de Auditoria de la Venta.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de verificacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int AuditoriaVentaDetalle(AuditoriaVentaDetalle detalleVenta)
        {
            return this.daoFacturacion.AuditoriaVentaDetalles(detalleVenta);
        }

        /// <summary>
        /// Método de Borrado de productos de BilVentasDetalle.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Verificacion de confirmacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int BorrarVentasDetalle(VentaDetalle detalleVenta)
        {
            return this.daoFacturacion.BorrarVentaDetalles(detalleVenta);
        }

        /// <summary>
        /// Método para Borrar productos de BilVentasDetalle - Facturación No Clínica
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de Confirmacion.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 01/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int BorrarVentasDetallesNC(VentaDetalle detalleVenta)
        {
            return this.daoFacturacion.BorrarVentaDetallesNC(detalleVenta);
        }

        /// <summary>
        /// Cierres the facturacion.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <returns>Retorna un entero.</returns>
        public int CierreFacturacion(string tipoFacturacion)
        {
            return this.daoFacturacion.CierreFacturacion(tipoFacturacion);
        }

        /// <summary>
        /// Permite consultar las aproximaciones.
        /// </summary>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Aproximacion> ConsultarAproximaciones()
        {
            IEnumerable<Aproximacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarAproximaciones())
            {
                registros = from fila in filas.Select()
                            select new Aproximacion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite consultar las aproximaciones.
        /// </summary>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Aproximacion> ConsultarAproximacionesActivas()
        {
            IEnumerable<Aproximacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarAproximacionesActivas())
            {
                registros = from fila in filas.Select()
                            select new Aproximacion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar atencion por cliente de facturación actividad.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Objeto tipo AtencionCliente.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public AtencionCliente ConsultarAtencionCliente(int identificadorAtencion)
        {
            IEnumerable<AtencionCliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarAtencionCliente(identificadorAtencion))
            {
                registros = from fila in filas.Select()
                            select new AtencionCliente(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Permite Consultar las atenciones pendientes por procesar.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Lista de datos de Atencion Atenciones por Procesar.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public List<FacturaAtencion> ConsultarAtencionesPendientesxProcesar(FacturaAtencion facturaAtencion)
        {
            ObservableCollection<FacturaAtencion> atenciones = null;
            IEnumerable<FacturaAtencionDetalle> atencionesDetalle = null;
            using (DataTable filas = this.daoFacturacion.ConsultarAtencionesPendientesxProcesar(facturaAtencion))
            {
                var atencionesEvaluar = from fila in filas.Select()
                                        select new FacturaAtencion(fila);

                atencionesDetalle = from fila in filas.Select()
                                    select new FacturaAtencionDetalle(fila, 1);

                atenciones = new ObservableCollection<FacturaAtencion>(atencionesEvaluar.Distinct(new CompararPropiedad<FacturaAtencion>(RecursosDominio.General_IdAtencion_Entidad)));

                Parallel.ForEach<FacturaAtencion>(
                    atenciones,
                    item =>
                    {
                        item.Detalle = (from detalle in atencionesDetalle
                                        orderby detalle.FechaVenta ascending, detalle.HoraVenta ascending
                                        where detalle.IdAtencion == item.IdAtencion
                                        select detalle).ToList();
                    });
            }

            return atenciones.ToList();
        }

        /// <summary>
        /// Lista las atenciones que cumplen los Criterios de Búsqueda.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Lista de atenciones de facturacion relacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista las atenciones que cumplen los Criterios de Búsqueda.
        /// </remarks>
        public List<FacturaAtencionRelacion> ConsultarAtencionFacturacionRelacion(FacturaAtencionRelacion facturaAtencion)
        {
            IEnumerable<FacturaAtencionRelacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarAtenciones(facturaAtencion))
            {
                registros = from fila in filas.Select()
                            select new FacturaAtencionRelacion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta el detalle de causales paginado.
        /// </summary>
        /// <param name="causalDetalle">The causal detalle.</param>
        /// <returns>Causales detalle.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<CausalDetalle>> ConsultarCausalesDetallePaginado(Paginacion<CausalDetalle> causalDetalle)
        {
            IEnumerable<Paginacion<CausalDetalle>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCausalesDetallePaginado(causalDetalle))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<CausalDetalle>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<CausalDetalle>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<CausalDetalle>>();
        }

        /// <summary>
        /// Permite consultar las clases de cubrimiento.
        /// </summary>
        /// <param name="claseCubrimiento">The clase cubrimiento.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<ClaseCubrimiento> ConsultarClasesCubrimiento(ClaseCubrimiento claseCubrimiento)
        {
            IEnumerable<ClaseCubrimiento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarClasesCubrimiento(claseCubrimiento))
            {
                registros = from fila in filas.Select()
                            select new ClaseCubrimiento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar Clientes.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>
        /// Clientes de Facturacion.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Cliente> ConsultarClientesAtencion(int identificadorProceso)
        {
            IEnumerable<Cliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarClientesAtencion(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new Cliente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar Clientes - Facturación No Clínicos.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Clientes de Facturacion NC.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Cliente> ConsultarClientesAtencionNC(int identificadorProceso)
        {
            IEnumerable<Cliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarClientesAtencionNC(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new Cliente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultar Cliente por Atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez.
        /// FechaDeCreacion: 19/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Cliente por Atencion.
        /// </remarks>
        public Cliente ConsultarClientexAtencion(int identificadorAtencion)
        {
            IEnumerable<Cliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarClientexAtencion(identificadorAtencion))
            {
                registros = from fila in filas.Select()
                            select new Cliente(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Método de consulta de componentes.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<TipoComponente> ConsultarComponentes(int identificadorAtencion, int identificadorTipoProducto)
        {
            IEnumerable<TipoComponente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarComponentes(identificadorAtencion, identificadorTipoProducto))
            {
                registros = from fila in filas.Select()
                            select new TipoComponente(fila);
            }

            registros = from
                            item in registros
                        orderby
                            item.NombreComponente
                        select
                            item;

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para realizar la Consulta de Componentes No Qx.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Componente> ConsultarComponentesNoQx(ProductoVenta producto)
        {
            IEnumerable<Componente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarComponentesNoQx(producto))
            {
                registros = from fila in filas.Select()
                            select new Componente(fila, Componente.ClaseComponente.NoQX);
            }

            registros = from
                            item in registros
                        orderby
                            item.NombreComponente
                        select
                            item;

            return registros.ToList();
        }

        /// <summary>
        /// Consulat de Componentes X Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 22/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Componente> ConsultarComponentesQx(ProductoVenta producto)
        {
            IEnumerable<Componente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarComponentesQx(producto))
            {
                registros = from fila in filas.Select()
                            select new Componente(fila, Componente.ClaseComponente.QX);
            }

            registros = from
                            item in registros
                        orderby
                            item.NombreComponente
                        select
                            item;

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar conceptos asociados a una atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Conceptos de la atención.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 24/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<ConceptoCobro> ConsultarConceptos(Atencion atencion)
        {
            IEnumerable<ConceptoCobro> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarConceptos(atencion))
            {
                registros = from fila in filas.Select()
                            select new ConceptoCobro(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar los conceptos de cartera.
        /// </summary>
        /// <param name="conceptoCartera">The concepto cartera.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<ConceptoCartera> ConsultarConceptosCartera(ConceptoCartera conceptoCartera)
        {
            IEnumerable<ConceptoCartera> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarConceptosCartera(conceptoCartera))
            {
                registros = from fila in filas.Select()
                            select new ConceptoCartera(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodos para Consultar Conceptos de Cobro.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>
        /// Conceptos de Cobro.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencionConceptoCobro> ConsultarConceptosCobro(int identificadorProceso)
        {
            IEnumerable<FacturaAtencionConceptoCobro> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarConceptosCobro(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new FacturaAtencionConceptoCobro(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodos para Consultar Conceptos de Cobro - Facturación No Clínica.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Conceptos de Cobro.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencionConceptoCobro> ConsultarConceptosCobroNC(int identificadorProceso)
        {
            IEnumerable<FacturaAtencionConceptoCobro> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarConceptosCobroNC(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new FacturaAtencionConceptoCobro(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite consultar la primera condición de un contrato.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <returns>Objeto condición de contrato.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public CondicionContrato ConsultarCondicionesContrato(CondicionContrato condicionContrato)
        {
            IEnumerable<CondicionContrato> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCondicionesContrato(condicionContrato))
            {
                registros = from fila in filas.Select()
                            select new CondicionContrato(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Permite consultar las condiciones de cubrimiento aplicadas a una vinculación.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<CondicionCubrimiento>> ConsultarCondicionesCubrimiento(Paginacion<CondicionCubrimiento> paginacion)
        {
            IEnumerable<Paginacion<CondicionCubrimiento>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCondicionesCubrimiento(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<CondicionCubrimiento>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<CondicionCubrimiento>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<CondicionCubrimiento>>();
        }

        /// <summary>
        /// Consultar condiciones de facturacion.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Lista de condiciones de facturacion.</returns>
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
            IEnumerable<CondicionTarifa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCondicionesFacturacion(condicionTarifa))
            {
                registros = from fila in filas.Select()
                            select new CondicionTarifa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Lista de datos de Condiciones Tarifa.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </remarks>
        public List<CondicionTarifa> ConsultarCondicionesTarifas(CondicionTarifa condicionTarifa)
        {
            IEnumerable<CondicionTarifa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCondicionesTarifas(condicionTarifa))
            {
                registros = from fila in filas.Select()
                            select new CondicionTarifa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar los planes o contratos en general.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<ContratoPlan>> ConsultarContratoPlan(Paginacion<ContratoPlan> atencion)
        {
            IEnumerable<Paginacion<ContratoPlan>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarContratoPlan(atencion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<ContratoPlan>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<ContratoPlan>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<ContratoPlan>>();
        }

        /// <summary>
        /// Metodo para realizar la consulta de COnvenios No Clinicos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de COnvenios No Clinicos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<ConvenioNoClinico>> ConsultarConvenioNoClinico(Paginacion<ConvenioNoClinico> paginacion)
        {
            IEnumerable<Paginacion<ConvenioNoClinico>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarConvenioNoClinico(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<ConvenioNoClinico>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<ConvenioNoClinico>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<ConvenioNoClinico>>();
        }

        /// <summary>
        /// Permite Consultar los Costos Asociados.
        /// </summary>
        /// <param name="costoAsociado">El costo asociado.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 23/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<CostoAsociado> ConsultarCostoAsociado(CostoAsociado costoAsociado)
        {
            IEnumerable<CostoAsociado> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCostoAsociado(costoAsociado))
            {
                registros = from fila in filas.Select()
                            select new CostoAsociado(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método de consulta de cubrimientos paginado.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Cubrimiento>> ConsultarCubrimientos(Paginacion<Cubrimiento> paginacion)
        {
            IEnumerable<Paginacion<Cubrimiento>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCubrimientos(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Cubrimiento>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Cubrimiento>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Cubrimiento>>();
        }

        /// <summary>
        /// Metodo para consultar las cuentas de cartera de la factura anulada.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Lista de cuentas de cartera.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<CuentaCartera> ConsultarCuentasCartera(int identificadorAtencion)
        {
            IEnumerable<CuentaCartera> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarCuentasCartera(identificadorAtencion))
            {
                registros = from fila in filas.Select()
                            select new CuentaCartera(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultars the datos cierre.
        /// </summary>
        /// <returns>Retorna un DataSet.</returns>
        public DataSet ConsultarDatosCierre()
        {
            return this.daoFacturacion.ConsultarDatosCierre();
        }

        /// <summary>
        /// Método para consultar depositos asociados a una atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Depositos de la atención.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 24/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Deposito> ConsultarDepositos(Atencion atencion)
        {
            IEnumerable<Deposito> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDepositos(atencion))
            {
                registros = from fila in filas.Select()
                            select new Deposito(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar llos Descuentos.
        /// </summary>
        /// <param name="descuentos">The descuentos.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Descuento>> ConsultarDescuentos(Paginacion<Descuento> descuentos)
        {
            IEnumerable<Paginacion<Descuento>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDescuentos(descuentos))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Descuento>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Descuento>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Descuento>>();
        }

        /// <summary>
        /// Lista los descuentos que cumplen con los criterios de busqueda.
        /// </summary>
        /// <param name="descuento">The descuento.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista los descuentos que cumplen con los criterios de busqueda.
        /// </remarks>
        public List<Descuento> ConsultarDescuentosContrato(Descuento descuento)
        {
            IEnumerable<Descuento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDescuentosContrato(descuento))
            {
                registros = from fila in filas.Select()
                            select new Descuento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta los Clientes Asociados a la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Listado de Clientes de Factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Cliente> ConsultarDetalleClienteFactura(int numeroFactura)
        {
            IEnumerable<Cliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleClienteFactura(numeroFactura))
            {
                registros = from fila in filas.Select()
                            select new Cliente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar los productos no facturables.
        /// </summary>
        /// <param name="parametroNoFacturable">The no facturable.</param>
        /// <returns>Lista de productos que quedaron en no facturables.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<NoFacturable> ConsultarDetalleNoFacturable(NoFacturable parametroNoFacturable)
        {
            IEnumerable<NoFacturable> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleNoFacturable(parametroNoFacturable))
            {
                registros = from fila in filas.Select()
                            select new NoFacturable(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar el detalle de la venta x Identificador de Proceso.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>
        /// Lista de Detalles de LAs Ventas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaDetalle> ConsultarDetallesVenta(int identificadorProceso)
        {
            IEnumerable<VentaDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetallesVenta(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new VentaDetalle(fila, 0);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar el detalle de la venta x Identificador de Proceso - Facturación No Clínica.
        /// </summary>
        /// <param name="identificadorProceso">Id del proceso.</param>
        /// <returns>Lista de Detalles de Las Ventas.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 22/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaDetalle> ConsultarDetallesVentaNC(int identificadorProceso)
        {
            IEnumerable<VentaDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetallesVentaNC(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new VentaDetalle(fila, 0);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar los Detalle Tarifa.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Lista de datos de Detalle Tarifa.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifa(int identificadorProceso)
        {
            IEnumerable<DetalleTarifa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleTarifa(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new DetalleTarifa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para realizar la consulta de detalle de tarifa de manual.
        /// </summary>
        /// <param name="detalleTarifa">The detalle tarifa.</param>
        /// <returns>
        /// Listado de Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifaManual(DetalleTarifa detalleTarifa)
        {
            IEnumerable<DetalleTarifa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleTarifaManual(detalleTarifa))
            {
                registros = from fila in filas.Select()
                            select new DetalleTarifa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para realizar la consulta de detalle de tarifa de manual.
        /// </summary>
        /// <param name="detalleTarifa">The detalle tarifa.</param>
        /// <returns>
        /// Listado de Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 18/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<DetalleTarifa> ConsultarDetalleTarifaXManual(DetalleTarifa detalleTarifa)
        {
            IEnumerable<DetalleTarifa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleTarifaXManual(detalleTarifa))
            {
                registros = from fila in filas.Select()
                            select new DetalleTarifa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta los detalles de la Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>DEtalles de la factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)..
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaDetalle> ConsultarDetalleVentaFactura(int numeroFactura)
        {
            IEnumerable<VentaDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarDetalleVentaFactura(numeroFactura))
            {
                registros = from fila in filas.Select()
                            select new VentaDetalle(fila, 0);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para realizar la consulta del estado de cuenta Encabezado.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Estado de Cuenta Encabezado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarEstadoCuentaEncabezado(EstadoCuentaEncabezado estadoCuenta)
        {
            IEnumerable<EstadoCuentaEncabezado> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarEstadoCuentaEncabezado(estadoCuenta))
            {
                registros = from fila in filas.Select()
                            select new EstadoCuentaEncabezado(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Consultar encabezado estado de cuenta multiple.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Encabezado Estado Cuenta.</returns>
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
            IEnumerable<EstadoCuentaEncabezado> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarEstadoCuentaEncabezadoMultiple(estadoCuenta))
            {
                registros = from fila in filas.Select()
                            select new EstadoCuentaEncabezado(fila);
            }

            return registros.FirstOrDefault();
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
            IEnumerable<EstadoCuentaEncabezado> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarEstadoCuentaEncabezadoNC(estadoCuenta))
            {
                registros = from fila in filas.Select()
                            select new EstadoCuentaEncabezado(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Retorna el estado del proceso
        /// </summary>
        /// <param name="identificadorProceso">The identifier proceso.</param>
        /// <returns>Retorna un entero.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public int ConsultarEstadoProceso(int identificadorProceso)
        {
            int retorno = int.MinValue;
            using (DataTable filas = this.daoFacturacion.ConsultarEstadoProceso(identificadorProceso))
            {
                retorno = (filas.Rows.Count > 0) ? int.Parse(filas.Rows[0][0].ToString()) : 0;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo para consultar exclusiones del Contrato x Atencion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de exclusiones por atencion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 30/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Exclusion>> ConsultarExclusionesAtencionContrato(Paginacion<Exclusion> paginacion)
        {
            IEnumerable<Paginacion<Exclusion>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarExclusionesAtencionContrato(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Exclusion>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Exclusion>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Exclusion>>();
        }

        /// <summary>
        /// Lista las exclusiones que cumplen con los criterios de busqueda.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista las exclusiones que cumplen con los criterios de busqueda.
        /// </remarks>
        public List<Exclusion> ConsultarExclusionesContrato(Exclusion exclusion)
        {
            IEnumerable<Exclusion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarExclusionesContrato(exclusion))
            {
                registros = from fila in filas.Select()
                            select new Exclusion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Lista las exclusiones de tarifa que cumplen con los criterios de busqueda.
        /// </summary>
        /// <param name="tarifaExclusion">The tarifa exclusion.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista las exclusiones de tarifa que cumplen con los criterios de busqueda.
        /// </remarks>
        public List<ExclusionManual> ConsultarExclusionesManual(ExclusionManual tarifaExclusion)
        {
            IEnumerable<ExclusionManual> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarExclusionesManual(tarifaExclusion))
            {
                registros = from fila in filas.Select()
                            select new ExclusionManual(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultar Factores QX.
        /// </summary>
        /// <returns>Factores Qx.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias. 
        /// FechaDeCreacion: 24/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Factores QX.
        /// </remarks>
        public List<FactoresQX> ConsultarFactoresQX()
        {
            IEnumerable<FactoresQX> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFactoresQX())
            {
                registros = from fila in filas.Select()
                            select new FactoresQX(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar los Factores QX de un manual.
        /// </summary>
        /// <param name="factoresQX">The factores QX.</param>
        /// <returns>Lista de Factores QX.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FactoresQX> ConsultarFactoresQX(FactoresQX factoresQX)
        {
            IEnumerable<FactoresQX> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFactoresQX(factoresQX))
            {
                registros = from fila in filas.Select()
                            select new FactoresQX(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar las causales aplicadas en la factura anulada.
        /// </summary>
        /// <param name="detalleNotaCredito">The detalle Nota Credito.</param>
        /// <returns>Lista de causales de la factura.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<DetalleNotaCredito> ConsultarFacturaAnuladaDetalle(DetalleNotaCredito detalleNotaCredito)
        {
            IEnumerable<DetalleNotaCredito> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaAnuladaDetalle(detalleNotaCredito))
            {
                registros = from
                                fila in filas.Select()
                            select new
                                DetalleNotaCredito(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar los componentes de la factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Lista de Componentes.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaComponente> ConsultarFacturaComponentes(int numeroFactura)
        {
            IEnumerable<VentaComponente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaComponentes(numeroFactura))
            {
                registros = from fila in filas.Select()
                            select new VentaComponente(fila, 0);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultar Factura Detalle.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Detalles de la factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencion> ConsultarFacturaDetalle(int numeroFactura)
        {
            IEnumerable<FacturaAtencion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaDetalle(numeroFactura))
            {
                registros = from
                                fila in filas.Select()
                            select new
                                FacturaAtencion(fila);
            }

            return registros.ToList();
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
            IEnumerable<PaqueteProducto> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaDetallePaquetes(numeroFactura))
            {
                registros = from
                                fila in filas.Select()
                            select new
                                PaqueteProducto(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta los detalles de la Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Detalles de la Factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencionDetalle> ConsultarFacturaDetallexNumeroFactura(int numeroFactura)
        {
            IEnumerable<FacturaAtencionDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaDetallexNumeroFactura(numeroFactura))
            {
                registros = from
                                fila in filas.Select()
                            select new
                                FacturaAtencionDetalle(fila, 0);
            }

            return registros.ToList();
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
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public EstadoCuentaEncabezado ConsultarFacturaEncabezado(int numeroFactura, short identificadorUsuarioFirma)
        {
            IEnumerable<EstadoCuentaEncabezado> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturaEncabezado(numeroFactura, identificadorUsuarioFirma))
            {
                registros = from
                                fila in filas.Select()
                            select new
                                EstadoCuentaEncabezado(fila, 0);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Permite Consultar las Facturas.
        /// </summary>
        /// <param name="facturaResultado">The factura resultado.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<FacturaResultado>> ConsultarFacturas(Paginacion<FacturaResultado> facturaResultado)
        {
            IEnumerable<Paginacion<FacturaResultado>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturas(facturaResultado))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<FacturaResultado>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<FacturaResultado>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<FacturaResultado>>();
        }

        /// <summary>
        /// Método para consulta en notas credito las facturas anuladas.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de facturas anuladas.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 23/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<NotaCredito>> ConsultarFacturasAnuladas(Paginacion<NotaCredito> paginacion)
        {
            IEnumerable<Paginacion<NotaCredito>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturasAnuladas(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<NotaCredito>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<NotaCredito>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<NotaCredito>>();
        }

        /// <summary>
        /// Permite Consultar las Facturas - Facturación No Clínica
        /// </summary>
        /// <param name="facturaResultado">The factura resultado.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 04/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<FacturaResultado>> ConsultarFacturasNC(Paginacion<FacturaResultado> facturaResultado)
        {
            IEnumerable<Paginacion<FacturaResultado>> registros = null;

            using (DataTable filas = this.daoFacturacion.ConsultarFacturasNC(facturaResultado))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<FacturaResultado>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<FacturaResultado>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<FacturaResultado>>();
        }

        /// <summary>
        /// Permite Consultar las facturas para reimprimir.
        /// </summary>
        /// <param name="paginacion">The tarifas.</param>
        /// <returns>
        /// Lista facturas para reimprimir.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 18/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<ReimprimirFactura>> ConsultarFacturasReimpresion(Paginacion<ReimprimirFactura> paginacion)
        {
            IEnumerable<Paginacion<ReimprimirFactura>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarFacturasReimpresion(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<ReimprimirFactura>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<ReimprimirFactura>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<ReimprimirFactura>>();
        }

        /// <summary>
        /// Permite Consultar las atenciones pendientes por procesar.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <param name="xml">Parametro XML.</param>
        /// <returns>
        /// Lista de datos de Atencion Atenciones por Procesar.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public List<FacturaAtencion> ConsultarGeneralFacturacion(FacturaAtencion facturaAtencion, XDocument xml)
        {
            ObservableCollection<FacturaAtencion> atenciones = new ObservableCollection<FacturaAtencion>();
            IEnumerable<FacturaAtencionDetalle> atencionesDetalle = null;

            FacturaAtencion atencion = new FacturaAtencion();

            DataSet ds = this.daoFacturacion.ConsultarGeneralFacturacion(facturaAtencion, xml);

            atencion.IdAtencion = Convert.ToInt32(ds.Tables[0].Rows[0]["AteIde"]);
            atencion.Cruzar = Convert.ToBoolean(ds.Tables[0].Rows[0]["Cruzar"]);
            atencion.IdCliente = Convert.ToInt32(ds.Tables[0].Rows[0]["CliIde"]);
            atencion.IdContrato = Convert.ToInt32(ds.Tables[0].Rows[0]["ConIde"]);
            atencion.IdManual = Convert.ToInt32(ds.Tables[0].Rows[0]["IdManual"]);
            atencion.IdPlan = Convert.ToInt32(ds.Tables[0].Rows[0]["IdPlan"]);
            atencion.IdProceso = Convert.ToInt32(ds.Tables[0].Rows[0]["IdProceso"]);
            atencion.IdServicio = Convert.ToInt32(ds.Tables[0].Rows[0]["IdServicio"]);
            atencion.IdTercero = Convert.ToInt32(ds.Tables[0].Rows[0]["TerIde"]);
            atencion.IdTipoAtencion = Convert.ToInt16(ds.Tables[0].Rows[0]["TisIde"]);
            atencion.IdUbicacion = Convert.ToInt16(ds.Tables[0].Rows[0]["UbiIde"]);

            atenciones.Add(atencion);

            using (DataTable filas = ds.Tables[1])
            {
                atencionesDetalle = from fila in filas.Select()
                                    select new FacturaAtencionDetalle(fila, 1);

                Parallel.ForEach<FacturaAtencion>(
                    atenciones,
                    item =>
                    {
                        item.Detalle = (from
                                            detalle in atencionesDetalle
                                        select
                                            detalle).ToList();
                    });
            }

            return atenciones.ToList();
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
            List<String> registros = new List<string>();
            using (DataTable filas = this.daoFacturacion.ConsultarGrupoPaquetes())
            {
                foreach (DataRow item in filas.Rows)
                {
                    registros.Add(item.ItemArray[0].ToString());
                }
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultar Grupo producto.
        /// </summary>
        /// <param name="grupoProducto">The grupo producto.</param>
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
        public GrupoProducto ConsultarGrupoProductoxIdGrupoProducto(GrupoProducto grupoProducto)
        {
            IEnumerable<GrupoProducto> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarGrupoProductoxIdGrupoProducto(grupoProducto))
            {
                registros = from fila in filas.Select()
                            select new GrupoProducto(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Permite Consultar los Producto Homologados.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Listado de datos de Hologacion Producto.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<HomologacionProducto> ConsultarHomologacionProducto(int identificadorProceso)
        {
            IEnumerable<HomologacionProducto> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarHomologacionProducto(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new HomologacionProducto(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo que consulta los honorarios medicos.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Lista de honorarios.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Honorario>> ConsultarHonorariosMedicos(Paginacion<Honorario> honorario)
        {
            IEnumerable<Paginacion<Honorario>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarHonorariosMedicos(honorario))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Honorario>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Honorario>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Honorario>>();
        }

        /// <summary>
        /// Metodo para consultar la informacion basica del tercero.
        /// </summary>
        /// <returns>Informacion Basica del Tercero.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public InformacionBasicaTercero ConsultarInformacionBasicaTercero()
        {
            IEnumerable<InformacionBasicaTercero> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarInformacionBasicaTercero())
            {
                registros = from fila in filas.Select()
                            select new InformacionBasicaTercero(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para consultar informacion de la factura x Identificador de Proceso.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <param name="identificadorTipoMovimiento">The id tipo movimiento.</param>
        /// <param name="identificadorTipoFacturacion">The id tipo facturacion.</param>
        /// <returns>
        /// Informacion de la factura.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public InformacionFactura ConsultarInformacionFactura(int identificadorProceso, int identificadorTipoMovimiento, short identificadorTipoFacturacion)
        {
            IEnumerable<InformacionFactura> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarInformacionFactura(identificadorProceso, identificadorTipoMovimiento, identificadorTipoFacturacion))
            {
                registros = from fila in filas.Select()
                            select new InformacionFactura(fila);
            }

            return registros.FirstOrDefault();
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
            IEnumerable<InformacionFactura> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarInformacionFacturaNC(identificadorProceso, identificadorTipoMovimiento, identificadorTipoFacturacion))
            {
                registros = from fila in filas.Select()
                            select new InformacionFactura(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para realizar la Consulta de Multiples Atenciones.
        /// </summary>
        /// <param name="numerosAtencion">The numeros atencion.</param>
        /// <returns>Lista de Atenciones.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<AtencionCliente> ConsultarListaAtenciones(List<int> numerosAtencion)
        {
            IEnumerable<AtencionCliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarListaAtenciones(numerosAtencion))
            {
                registros = from fila in filas.Select()
                            select new AtencionCliente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar las atenciones que s eencuentra en proceso.
        /// </summary>
        /// <param name="numerosAtencion">The numeros atencion.</param>
        /// <returns>Lista de Atenciones en Proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<AtencionCliente> ConsultarListaAtencionesProceso(List<int> numerosAtencion)
        {
            IEnumerable<AtencionCliente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarListaAtencionesProceso(numerosAtencion))
            {
                registros = from fila in filas.Select()
                            select new AtencionCliente(fila);
            }

            return registros.ToList();
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
            return this.daoFacturacion.ConsultarLogFacturacion(mes, año, estado);
        }

        /// <summary>
        /// Consulta las Maestras.
        /// </summary>
        /// <param name="identificadorMaestra">The id maestra.</param>
        /// <param name="identificadorPagina">The id pagina.</param>
        /// <returns>
        /// Lista de las Maestras.
        /// </returns>
        /// <remarks>
        /// Autor: alex mattos.
        /// FechaDeCreacion: (31/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las Maestras.
        /// </remarks>
        public List<Maestras> ConsultarMaestras(int identificadorMaestra, int identificadorPagina)
        {
            IEnumerable<Maestras> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina))
            {
                registros = from fila in filas.Select()
                            select new Maestras(fila);
            }

            return registros.OrderBy(item => item.NombreMaestroDetalle).ToList();
        }

        /// <summary>
        /// Permite Consultar lAs tARIFAS.
        /// </summary>
        /// <param name="manuales">The manuales.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<ManualesBasicos>> ConsultarManualesBasicos(Paginacion<ManualesBasicos> manuales)
        {
            IEnumerable<Paginacion<ManualesBasicos>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarManualesBasicos(manuales))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<ManualesBasicos>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<ManualesBasicos>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<ManualesBasicos>>();
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
            return this.daoFacturacion.ConsultarManualesBasicosContrato(identificadorContrato);
        }

        /// <summary>
        /// Metodo para consultar movimientos de Tesoreria Identificador de Proceso.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>
        /// Movimientos de Tesoreria.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencionMovimiento> ConsultarMovimientosTesoreria(int identificadorProceso)
        {
            IEnumerable<FacturaAtencionMovimiento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarMovimientosTesoreria(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new FacturaAtencionMovimiento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar movimientos de Tesoreria x Identificador de Proceso - Facturación No Clínica.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Movimientos de Tesoreria.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaAtencionMovimiento> ConsultarMovimientosTesoreriaNC(int identificadorProceso)
        {
            IEnumerable<FacturaAtencionMovimiento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarMovimientosTesoreriaNC(identificadorProceso))
            {
                registros = from fila in filas.Select()
                            select new FacturaAtencionMovimiento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para obtener el tipo de movimiento del usuario.
        /// </summary>
        /// <param name="movimientoUsuario">The movimiento usuario.</param>
        /// <returns>Tipo de movimiento del usuario.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 31/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public MovimientoUsuario ConsultarMovimientoUsuario(MovimientoUsuario movimientoUsuario)
        {
            IEnumerable<MovimientoUsuario> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarMovimientoUsuario(movimientoUsuario))
            {
                registros = from fila in filas.Select()
                            select new MovimientoUsuario(fila);
            }

            return registros.FirstOrDefault();
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
            IEnumerable<NivelComplejidad> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarNivelComplejidadProducto(producto))
            {
                registros = from fila in filas.Select()
                            select new NivelComplejidad(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar los niveles de complejidad de una exclusión de manual.
        /// </summary>
        /// <param name="nivelComplejidad">The nivel complejidad.</param>
        /// <returns>Niveles de complejidad de una exclusión del manual.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<NivelComplejidad> ConsultarNivelesComplejidad(NivelComplejidad nivelComplejidad)
        {
            IEnumerable<NivelComplejidad> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarNivelesComplejidad(nivelComplejidad))
            {
                registros = from fila in filas.Select()
                            select new NivelComplejidad(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método que consulta Paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <returns>Lsita de Paquetes.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Paquete>> ConsultarPaquete(Paginacion<Paquete> paquete)
        {
            IEnumerable<Paginacion<Paquete>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarPaquete(paquete))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Paquete>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Paquete>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Paquete>>();
        }

        /// <summary>
        /// Método que consulta Paquete.
        /// </summary>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Lsita de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<PaqueteProducto> ConsultarPaqueteDetallado(int identificadorPaquete, int identificadorAtencion, string ventas)
        {
            List<PaqueteProducto> registros = new List<PaqueteProducto>();
            using (DataTable filas = this.daoFacturacion.ConsultarPaqueteDetallado(identificadorPaquete, identificadorAtencion, ventas))
            {
                registros = (from fila in filas.Select()
                             select new PaqueteProducto(fila)).ToList();
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultar Paquete Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>
        /// Paquete Factura.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 24/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Paquete Factura.
        /// </remarks>
        public List<EstadoCuentaDetallado> ConsultarPaqueteFactura(int numeroFactura)
        {
            List<EstadoCuentaDetallado> list = new List<EstadoCuentaDetallado>();

            DataSet datasetTodo = this.daoFacturacion.ConsultarPaqueteFactura(numeroFactura);
            DataTable datasetPyG = datasetTodo.Tables[0];
            DataTable datasetDetalles = datasetTodo.Tables[1];

            IEnumerable<EstadoCuentaDetallado> registros = null;
            using (DataTable filas = datasetPyG)
            {
                registros = from fila in filas.Select()
                            select new EstadoCuentaDetallado(fila);
            }

            IEnumerable<EstadoCuentaDetallado> registrosDetalle = null;
            using (DataTable filas = datasetDetalles)
            {
                registrosDetalle = from fila in filas.Select()
                                   select new EstadoCuentaDetallado(fila);
            }

            list = registros.ToList();
            list.AddRange(registrosDetalle.ToList());

            return list;
        }

        /// <summary>
        /// Método que consulta Paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <returns>
        /// Lsita de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<PaqueteEncabezado>> ConsultarPaqueteProducto(Paginacion<PaqueteEncabezado> paquete)
        {
            IEnumerable<Paginacion<PaqueteEncabezado>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarPaqueteProducto(paquete))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<PaqueteEncabezado>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<PaqueteEncabezado>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<PaqueteEncabezado>>();
        }

        /// <summary>
        /// Método que consulta Paquete.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lsita de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<PaqueteProducto>> ConsultarPaqueteProductoDetallado(Paginacion<PaqueteProducto> paquete, int identificadorAtencion)
        {
            IEnumerable<Paginacion<PaqueteProducto>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarPaqueteProductoDetallado(paquete, identificadorAtencion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<PaqueteProducto>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<PaqueteProducto>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<PaqueteProducto>>();
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
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los contratos.
        /// </remarks>
        public List<Plan> ConsultarPlanes(Contrato contrato)
        {
            IEnumerable<Plan> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarPlanes(contrato))
            {
                registros = from fila in filas.Select()
                            select new Plan(fila);
            }

            registros = from
                            item in registros
                        orderby
                            item.Nombre
                        select
                            item;

            return registros.ToList();
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
            return this.daoFacturacion.ConsultarPorcentajeAlterno(identificadorContrato, identificadorManualAlterno, identificadorManual);
        }

        /// <summary>
        /// Permite consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaComponente> ConsultarProductoComponentes(VentaComponente detalle)
        {
            IEnumerable<VentaComponente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductoComponentes(detalle))
            {
                registros = from fila in filas.Select()
                            select new VentaComponente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaComponente> ConsultarProductoComponentesReimpresion(VentaComponente detalle)
        {
            IEnumerable<VentaComponente> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductoComponentesReimpresion(detalle))
            {
                registros = from fila in filas.Select()
                            select new VentaComponente(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar en condiciones de cubrimientos los productos que pertenezcan a una clase de cubrimiento para una atención.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicionCubrimiento.</param>
        /// <returns>Productos de la clase de cubrimiento.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<CondicionCubrimiento> ConsultarProductosCondicionCubrimientos(CondicionCubrimiento condicionCubrimiento)
        {
            IEnumerable<CondicionCubrimiento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductosCondicionCubrimientos(condicionCubrimiento))
            {
                registros = from fila in filas.Select()
                            select new CondicionCubrimiento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar en cubrimientos los productos que pertenezcan a una clase de cubrimiento para una atención.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Productos de la clase de cubrimiento.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Cubrimiento> ConsultarProductosCubrimientos(Cubrimiento cubrimiento)
        {
            IEnumerable<Cubrimiento> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductosCubrimientos(cubrimiento))
            {
                registros = from fila in filas.Select()
                            select new Cubrimiento(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar las productos de una venta no clinica.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Listado de productos de Facturas.</returns>
        /// <remarks>
        /// Autor: Diana Cárdenas- INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 26/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public List<FacturaAtencion> ConsultarProductosNC(FacturaAtencion facturaAtencion)
        {
            ObservableCollection<FacturaAtencion> atenciones = null;
            IEnumerable<FacturaAtencionDetalle> atencionesDetalle = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductosNC(facturaAtencion))
            {
                var atencionesEvaluar = from fila in filas.Select()
                                        select new FacturaAtencion(fila);

                atencionesDetalle = from fila in filas.Select()
                                    select new FacturaAtencionDetalle(fila, 0);

                atenciones = new ObservableCollection<FacturaAtencion>(atencionesEvaluar.Distinct(new CompararPropiedad<FacturaAtencion>(RecursosDominio.General_IdAtencion_Entidad)));

                Parallel.ForEach<FacturaAtencion>(
                    atenciones,
                    item =>
                    {
                        item.Detalle = (from
                                            detalle in atencionesDetalle
                                        where
                                            detalle.IdAtencion == item.IdAtencion
                                        select
                                            detalle).ToList();
                    });
            }

            return atenciones.ToList();
        }

        /// <summary>
        /// Metodo para realizar las consulta de productos de la venta.
        /// </summary>
        /// <param name="productoVenta">The producto venta.</param>
        /// <returns>Retorna los productos de la Venta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<ProductoVenta> ConsultarProductosVenta(ProductoVenta productoVenta)
        {
            IEnumerable<ProductoVenta> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductosVenta(productoVenta))
            {
                registros = from fila in filas.Select()
                            select new ProductoVenta(fila);
            }

            return registros.ToList();
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
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<ProductoVenta> ConsultarProductosVentaPaquete(int identificadorAtencion, int identificadorPaquete)
        {
            IEnumerable<ProductoVenta> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductosVentaPaquete(identificadorAtencion, identificadorPaquete))
            {
                registros = from fila in filas.Select()
                            select new ProductoVenta(fila);
            }

            return registros.ToList();
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
            IEnumerable<Producto> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarProductoxIdProducto(producto))
            {
                registros = from fila in filas.Select()
                            select new Producto(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Metodo de Consultar recargos.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez- INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Recargo>> ConsultarRecargos(Paginacion<Recargo> recargo)
        {
            IEnumerable<Paginacion<Recargo>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarRecargos(recargo))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Recargo>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Recargo>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Recargo>>();
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el contrato.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>Lista de datos Recargos Contrato.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar los recargos aplicados en el contrato a un producto.
        /// </remarks>
        public List<Recargo> ConsultarRecargosContrato(Recargo recargo)
        {
            IEnumerable<Recargo> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarRecargosContrato(recargo))
            {
                registros = from fila in filas.Select()
                            select new Recargo(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el manual.
        /// </summary>
        /// <param name="recargoManual">The recargo manual.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<RecargoManual> ConsultarRecargosManual(RecargoManual recargoManual)
        {
            IEnumerable<RecargoManual> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarRecargosManual(recargoManual))
            {
                registros = from fila in filas.Select()
                            select new RecargoManual(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar lAs tARIFAS.
        /// </summary>
        /// <param name="tarifas">The tarifas.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<CondicionTarifa>> ConsultarTarifas(Paginacion<CondicionTarifa> tarifas)
        {
            IEnumerable<Paginacion<CondicionTarifa>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTarifas(tarifas))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<CondicionTarifa>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<CondicionTarifa>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<CondicionTarifa>>();
        }

        /// <summary>
        /// Método de consultar Unidad de.
        /// </summary>
        /// <param name="facturaUnidad">The factura unidad.</param>
        /// <returns>Lista de tarifas.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 20/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<TarifaUnidad> ConsultarTarifaUnidad(TarifaUnidad facturaUnidad)
        {
            IEnumerable<TarifaUnidad> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTarifaUnidad(facturaUnidad))
            {
                registros = from fila in filas.Select()
                            select new TarifaUnidad(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para realizar la consulta de la tasa de Impuestos.
        /// </summary>
        /// <returns>
        /// Listado de Tasas de Impuestos.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Tasa> ConsultarTasa()
        {
            IEnumerable<Tasa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTasa())
            {
                registros = from fila in filas.Select()
                            select new Tasa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta datos basicos de un tercero.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 05/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public FacturaNoClinicaReporte ConsultarTercero(int identificadorTercero)
        {
            IEnumerable<FacturaNoClinicaReporte> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTercero(identificadorTercero))
            {
                registros = from fila in filas.Select()
                            select new FacturaNoClinicaReporte(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Consultar terceros responsables de un componente.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Lista de terceros responsables de un componente.</returns>
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
            IEnumerable<VentaResponsable> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTerceroComponente(identificadorAtencion))
            {
                registros = from fila in filas.Select()
                            select new VentaResponsable(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar los tipos de empresa.
        /// </summary>
        /// <param name="tipoEmpresa">The tipo empresa.</param>
        /// <returns>Listado de tipos de empresa.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<TipoEmpresa> ConsultarTipoEmpresa(TipoEmpresa tipoEmpresa)
        {
            IEnumerable<TipoEmpresa> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTipoEmpresa(tipoEmpresa))
            {
                registros = from fila in filas.Select()
                            select new TipoEmpresa(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consultars the tipo factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna un string.</returns>
        public string ConsultarTipoFactura(int numeroFactura)
        {
            return this.daoFacturacion.ConsultarTipoFactura(numeroFactura);
        }

        /// <summary>
        /// Método para consultar Tipo Producto.
        /// </summary>
        /// <param name="tipoProducto">The tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        /// Lista de Tipo Producto por Usuario.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<TipoProducto> ConsultarTipoProducto(TipoProducto tipoProducto, string usuario)
        {
            IEnumerable<CliCountry.SAHI.Dominio.Entidades.Productos.TipoProducto> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTipoProducto(tipoProducto, usuario))
            {
                registros = from fila in filas.Select()
                            select new CliCountry.SAHI.Dominio.Entidades.Productos.TipoProducto(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Consulta Transacciones.
        /// </summary>
        /// <param name="transaccion">The transaccion.</param>
        /// <returns>Retorna Lista.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Transaccion> ConsultarTransaccion(Transaccion transaccion)
        {
            IEnumerable<Transaccion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTransaccion(transaccion))
            {
                registros = from fila in filas.Select()
                            select new Transaccion(fila);
            }

            return registros.ToList();
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
            IEnumerable<Transaccion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarTransaccionesVentasQx(usuario))
            {
                registros = from fila in filas.Select()
                            select new Transaccion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar Ubicación.
        /// </summary>
        /// <param name="ubicacion">The ubicacion.</param>
        /// <returns>Lista de Ubicación por Usuario.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Ubicacion> ConsultarUbicacion(Ubicacion ubicacion)
        {
            IEnumerable<Ubicacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarUbicacion(ubicacion))
            {
                registros = from fila in filas.Select()
                            select new Ubicacion(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Método para consultar Ubicación.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        /// Lista de Ubicación de Consulmo por Atencion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 26/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Ubicacion> ConsultarUbicacionConsumo(int identificadorAtencion, int identificadorTipoProducto, string usuario)
        {
            IEnumerable<Ubicacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarUbicacionConsumo(identificadorAtencion, identificadorTipoProducto, usuario))
            {
                registros = from fila in filas.Select()
                            select new Ubicacion(fila);
            }

            return registros.ToList();
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
            IEnumerable<Ubicacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarUbicacionPorNombre(ubicacion))
            {
                registros = from fila in filas.Select()
                            select new Ubicacion(fila);
            }

            return registros.ToList();
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
            IEnumerable<Paquete> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarValorEncabezadoPaquetes(facturaPaquete))
            {
                registros = from fila in filas.Select()
                            select new Paquete(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Metodo para consultar los valores de los paquetes encabezados.
        /// </summary>
        /// <param name="facturaPaquete">The factura paquete.</param>
        /// <returns>Lista de paquetes.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 27/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<FacturaPaquete> ConsultarValorPaquetes(FacturaPaquete facturaPaquete)
        {
            IEnumerable<FacturaPaquete> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarValorPaquetes(facturaPaquete))
            {
                registros = from fila in filas.Select()
                            select new FacturaPaquete(fila);
            }

            return registros.ToList();
        }

        /// <summary>
        ///  Metodo para consultar el detalle de la venta x Identificador de la venta.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<VentaDetalle> ConsultarVentaDetalles(VentaDetalle detalleVenta)
        {
            IEnumerable<VentaDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentaDetalles(detalleVenta))
            {
                registros = from fila in filas.Select()
                            select new VentaDetalle(fila, 0);
            }

            return registros.ToList();
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
            IEnumerable<VentaDetalle> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentaDetallesxIdTx(detalleVenta))
            {
                registros = from fila in filas.Select()
                            select new VentaDetalle(fila, 0);
            }

            return registros.ToList();
        }

        /// <summary>
        /// Permite Consultar las Ventas No Clinicas.
        /// </summary>
        /// <param name="ventaNoClinica">The venta no clinica.</param>
        /// <returns>Lista de Ventas.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<VentaNoClinica>> ConsultarVentaNoClinica(Paginacion<VentaNoClinica> ventaNoClinica)
        {
            IEnumerable<Paginacion<VentaNoClinica>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentaNoClinica(ventaNoClinica))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<VentaNoClinica>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<VentaNoClinica>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<VentaNoClinica>>();
        }

        /// <summary>
        /// Metodo para consultar productos relacion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Conjunto de Datos Resultado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<VentaProductoRelacion>> ConsultarVentaProductosRelacion(Paginacion<VentaProductoRelacion> paginacion)
        {
            IEnumerable<Paginacion<VentaProductoRelacion>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentaProductosRelacion(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<VentaProductoRelacion>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<VentaProductoRelacion>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<VentaProductoRelacion>>();
        }

        /// <summary>
        /// Metodo para Consultar Productos de la Venta.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Detalle de Ventas Asociadas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<VentaProducto>> ConsultarVentaProductosTransaccion(Paginacion<VentaProducto> paginacion)
        {
            IEnumerable<Paginacion<VentaProducto>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentaProductosTransaccion(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<VentaProducto>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<VentaProducto>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<VentaProducto>>();
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
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Venta>> ConsultarVentasNumeroVenta(Paginacion<Venta> venta)
        {
            IEnumerable<Paginacion<Venta>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentasNumeroVenta(venta))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Venta>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Venta>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Venta>>();
        }

        /// <summary>
        /// Metodo para Consultar Ventas Qx.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Ventas Asociadas.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<VentaTransaccion>> ConsultarVentasTransaccion(Paginacion<VentaTransaccion> paginacion)
        {
            IEnumerable<Paginacion<VentaTransaccion>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentasTransaccion(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<VentaTransaccion>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<VentaTransaccion>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<VentaTransaccion>>();
        }

        /// <summary>
        /// Permite Consultar las vinculaciones por atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Vinculacion> ConsultarVinculaciones(Vinculacion atencion)
        {
            IEnumerable<Vinculacion> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVinculaciones(atencion))
            {
                registros = from fila in filas.Select()
                            select new Vinculacion(fila);
            }

            return registros.ToList();
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
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<Venta>> ConsultaVentasAtencion(Paginacion<Venta> venta)
        {
            IEnumerable<Paginacion<Venta>> registros = null;
            using (DataTable filas = this.daoFacturacion.ConsultarVentasAtencion(venta))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<Venta>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<Venta>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<Venta>>();
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
            return this.daoFacturacion.EliminarNoFacturables(numeroFactura);
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
            return this.daoFacturacion.EliminarProcesoActual(identificadorProceso);
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
            this.daoFacturacion.EliminarProductosTarifa(codigoEntidad, identificadorTarifa);
        }

        /// <summary>
        /// Método para guardar la factura anulada en notas credito.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id de la nota credito.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarAnulacionFactura(NotaCredito notaCredito)
        {
            return this.daoFacturacion.GuardarAnulacionFactura(notaCredito);
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
        public DataTable GuardarAnulacionFacturaNC(NotaCredito notaCredito)
        {
            return this.daoFacturacion.GuardarAnulacionFacturaNC(notaCredito);
        }

        /// <summary>
        /// Metodo para guardar el Cliente.
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <returns>Id del Cliente generado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCliente(Cliente cliente)
        {
            return this.daoFacturacion.GuardarCliente(cliente);
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
            return this.daoFacturacion.GuardarConceptoCobro(conceptoCobro);
        }

        /// <summary>
        /// Guarda la información de la condición de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Id condición cubrimiento creado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            return this.daoFacturacion.GuardarCondicionCubrimiento(condicionCubrimiento);
        }

        /// <summary>
        /// Metodo para guardar tarifas.
        /// </summary>
        /// <param name="tarifas">The tarifas.</param>
        /// <returns>
        /// Indica el id de la tarifa almacenada.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCondicionTarifa(CondicionTarifa tarifas)
        {
            return this.daoFacturacion.GuardarCondicionTarifa(tarifas);
        }

        /// <summary>
        /// Método para guardar el convenio.
        /// </summary>
        /// <param name="convenioNoClinico">The convenio no clinico.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 09/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarConvenioNoClinico(ConvenioNoClinico convenioNoClinico)
        {
            return this.daoFacturacion.GuardarConvenioNoClinico(convenioNoClinico);
        }

        /// <summary>
        /// Guarda la información del cubrimiento.
        /// </summary>
        /// <param name="cubrimiento">The cubrimiento.</param>
        /// <returns>Id cubrimiento creado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCubrimientos(Cubrimiento cubrimiento)
        {
            return this.daoFacturacion.GuardarCubrimientos(cubrimiento);
        }

        /// <summary>
        /// Método para almacenar la información de la cuenta de cartera.
        /// </summary>
        /// <param name="cuentaCartera">The cuenta cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCuentaCartera(CuentaCartera cuentaCartera)
        {
            return this.daoFacturacion.GuardarCuentaCartera(cuentaCartera);
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
        public int GuardarCuentaCarteraNC(CuentaCartera cuentaCartera)
        {
            return this.daoFacturacion.GuardarCuentaCarteraNC(cuentaCartera);
        }

        /// <summary>
        /// Método para almacenar los descuentos.
        /// </summary>
        /// <param name="descuento">The descuentos.</param>
        /// <returns>
        /// Id del Descuento.
        /// </returns>
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio).
        /// FechaDeCreacion: (dd/MM/yyyy).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDescuento(DescuentoConfiguracion descuento)
        {
            return this.daoFacturacion.GuardarDescuento(descuento);
        }

        /// <summary>
        /// Método para guardar las causales de la factura anulada.
        /// </summary>
        /// <param name="detalleNotaCredito">The detalle nota credito.</param>
        /// <returns>Id de la nota credito aplicada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 30/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleAnulacionFactura(DetalleNotaCredito detalleNotaCredito)
        {
            return this.daoFacturacion.GuardarDetalleAnulacionFactura(detalleNotaCredito);
        }

        /// <summary>
        /// Método para almacenar el detalle de la factura.
        /// </summary>
        /// <param name="detalleFactura">The detalle factura.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleFactura(EstadoCuentaDetallado detalleFactura)
        {
            try
            {
                return this.daoFacturacion.GuardarDetalleFactura(detalleFactura);
            }
            catch (Exception ex)
            {
                string excepcion = ex.Message;
                throw new Exception(excepcion);
            }
        }

        /// <summary>
        /// Método que almacena la información detallada de la factura no clínica.
        /// </summary>
        /// <param name="detalleFactura">The detalle factura.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 25/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleFacturaNC(EstadoCuentaDetallado detalleFactura)
        {
            try
            {
                return this.daoFacturacion.GuardarDetalleFacturaNC(detalleFactura);
            }
            catch (Exception ex)
            {
                string excepcion = ex.Message;
                throw new Exception(excepcion);
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
        public void GuardarDetalleFacturaPyG(Paquete paquete, int numeroFactura, int numeroVenta)
        {
            this.daoFacturacion.GuardarDetalleFacturaPyG(paquete, numeroFactura, numeroVenta);
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
        public void GuardarDetalleFacturaPyGComponentes(VentaComponente ventaComponente, int numeroFactura)
        {
            this.daoFacturacion.GuardarDetalleFacturaPyGComponentes(ventaComponente, numeroFactura);
        }

        /// <summary>
        /// Método para almacenar la información de detalle del movimiento de cartera.
        /// </summary>
        /// <param name="detalleMovimientoCartera">The detalle movimiento cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleMovimientoCartera(DetalleMovimientoCartera detalleMovimientoCartera)
        {
            return this.daoFacturacion.GuardarDetalleMovimientoCartera(detalleMovimientoCartera);
        }

        /// <summary>
        /// Método para guardar los productos del paquete armado.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="detallePaquete">The detalle paquete.</param>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="detallePaqueteCompleto">The detalle paquete completo.</param>
        /// <returns>
        /// Filas Afectadas.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetallePaqueteFactura(int identificadorAtencion, string numeroFactura, PaqueteProducto detallePaquete, EstadoCuentaEncabezado estadoCuenta, Paquete detallePaqueteCompleto)
        {
            return this.daoFacturacion.GuardarDetallePaqueteFactura(identificadorAtencion, numeroFactura, detallePaquete, estadoCuenta, detallePaqueteCompleto);
        }

        /// <summary>
        /// Metodo para Almacenar el Detalle del Proceso.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Indica el id del Detalle.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleProceso(ProcesoFacturaDetalle detalle)
        {
            return this.daoFacturacion.GuardarDetalleProceso(detalle);
        }

        /// <summary>
        /// Metodo para Almacenar el Detalle del Proceso para Facturación No Clínica.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>
        /// Indica el Id del Detalle.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDetalleProcesoNC(ProcesoFacturaDetalle detalle)
        {
            return this.daoFacturacion.GuardarDetalleProcesoNC(detalle);
        }

        /// <summary>
        /// Metodo para Almacenar el Encabezado del Proceso.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Indica el id del Encabezado.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarEncabezadoProceso(ProcesoFactura procesoFactura)
        {
            return this.daoFacturacion.GuardarEncabezadoProceso(procesoFactura);
        }

        /// <summary>
        /// Metodo para Almacenar el Encabezado del Proceso para Facturación No Clínica.
        /// </summary>
        /// <param name="procesoFactura">Entidad que contiene todos los parámetros.</param>
        /// <returns>
        /// Indica el id del Encabezado.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarEncabezadoProcesoNC(ProcesoFactura procesoFactura)
        {
            return this.daoFacturacion.GuardarEncabezadoProcesoNC(procesoFactura);
        }

        /// <summary>
        /// Método para almacenar la información de contabilidad del estado de cuenta.
        /// </summary>
        /// <param name="contabilidad">The contabilidad.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarEstadoCuentaContabilidad(Contabilidad contabilidad)
        {
            return this.daoFacturacion.GuardarEstadoCuentaContabilidad(contabilidad);
        }

        /// <summary>
        /// Método que almacena la información de la exclusión del contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de la exclusión actualizada.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarExclusionContrato(Exclusion exclusion)
        {
            return this.daoFacturacion.GuardarExclusionContrato(exclusion);
        }

        /// <summary>
        /// Método para almacenar ls Componentes de la factura.
        /// </summary>
        /// <param name="facturaComponentes">The factura componentes.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 14/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarFacturaComponente(FacturaComponentes facturaComponentes)
        {
            return this.daoFacturacion.GuardarFacturaComponentes(facturaComponentes);
        }

        /// <summary>
        /// Método que inserta la información del pago de la factura.
        /// </summary>
        /// <param name="facturaPago">The factura pago.</param>
        /// <returns>Devuelve 1 si la operación se ejecutó con exito, 0 si es lo contrario.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarFacturaPago(FacturaPago facturaPago)
        {
            return this.daoFacturacion.GuardarFacturaPago(facturaPago);
        }

        /// <summary>
        /// Método que inserta la información del detalle del pago de la factura.
        /// </summary>
        /// <param name="facturaPagoDetalle">The factura pago detalle.</param>
        /// <returns>Devuelve 1 si la operación se ejecutó con exito, 0 si es lo contrario.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarFacturaPagoDetalle(FacturaPagoDetalle facturaPagoDetalle)
        {
            return this.daoFacturacion.GuardarFacturaPagoDetalle(facturaPagoDetalle);
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
        public void GuardarFacturaRelacionEncabezado(int numeroFactura, int identificadorPaquete, int numeroVenta, int lote)
        {
            this.daoFacturacion.GuardarFacturaRelacionEncabezado(numeroFactura, identificadorPaquete, numeroVenta, lote);
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
        public void GuardarFacturaRelacionEncabezadoDetalle(int numeroFactura, int numeroFacturaPyG, int identificadorPaquete)
        {
            this.daoFacturacion.GuardarFacturaRelacionEncabezadoDetalle(numeroFactura, numeroFacturaPyG, identificadorPaquete);
        }

        /// <summary>
        /// Método para almacenar el encabezado de la factura.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <returns>Numero de factura.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarInformacionEncabezadoFactura(EncabezadoFactura encabezadoFactura)
        {
            return this.daoFacturacion.GuardarInformacionEncabezadoFactura(encabezadoFactura);
        }

        /// <summary>
        /// Método que almacena la información de la cabecera de la factura no clínica.
        /// </summary>
        /// <param name="encabezadoFactura">The enbezado factura.</param>
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
            return this.daoFacturacion.GuardarInformacionEncabezadoFacturaNC(encabezadoFactura, identificadorModulo);
        }

        /// <summary>
        /// Guardars the informacion paquetes factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="valorPyG">The valor py g.</param>
        /// <returns>Retorna el numero de la factura PyG.</returns>
        public int GuardarInformacionPaquetesFactura(int numeroFactura, decimal valorPyG)
        {
            return this.daoFacturacion.GuardarInformacionPaquetesFactura(numeroFactura, valorPyG);
        }

        /// <summary>
        /// Guardar Informacion Toda Factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Cadena string.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Toda Factura.
        /// </remarks>
        public string[] GuardarInformacionTodaFactura(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            return this.daoFacturacion.GuardarInformacionTodaFactura(encabezado, detalle, maestroComponentes, ventas);
        }

        /// <summary>
        /// Guardar Informacion Toda Factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Cadena string.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Guardar Informacion Toda Factura.
        /// </remarks>
        public string[] GuardarInformacionTodaFacturaPaquetes(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            return this.daoFacturacion.GuardarInformacionTodaFacturaPaquetes(encabezado, detalle, maestroComponentes, ventas);
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
            return this.daoFacturacion.GuardarModificacionTercero(tercero);
        }

        /// <summary>
        /// Método para almacenar la información del movimiento de cartera.
        /// </summary>
        /// <param name="movimientoCartera">The movimiento cartera.</param>
        /// <returns>Indica si se guardo el registro.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 04/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarMovimientoCartera(MovimientoCartera movimientoCartera)
        {
            return this.daoFacturacion.GuardarMovimientoCartera(movimientoCartera);
        }

        /// <summary>
        /// Guardars the movimiento cartera ajuste saldo.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="usuario">The usuario.</param>
        /// <param name="identificadorNotaCredito">The id nota credito.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarMovimientoCarteraAjusteSaldo(int numeroFactura, string usuario, int identificadorNotaCredito)
        {
            return this.daoFacturacion.GuardarMovimientoCarteraAjusteSaldo(numeroFactura, usuario, identificadorNotaCredito);
        }

        /// <summary>
        /// Método que guarda los movimientos de cartera de la factura anulada.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>Id del ultimo movimiento guardado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 02/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarMovimientoCarteraAnulacion(NotaCredito notaCredito)
        {
            return this.daoFacturacion.GuardarMovimientoCarteraAnulacion(notaCredito);
        }

        /// <summary>
        /// Guardars the no facturable.
        /// </summary>
        /// <param name="parametroNoFacturable">The parametro no facturable.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarNoFacturable(NoFacturable parametroNoFacturable)
        {
            return this.daoFacturacion.GuardarNoFacturable(parametroNoFacturable);
        }

        /// <summary>
        /// Método para guardar el paquete armado de la factura.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="paquete">The paquete.</param>
        /// <returns>Filas Afectadas.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarPaqueteFactura(int identificadorAtencion, string numeroFactura, Paquete paquete)
        {
            return this.daoFacturacion.GuardarPaqueteFactura(identificadorAtencion, numeroFactura, paquete);
        }

        /// <summary>
        /// Metodo para realizar insertar recargo.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>
        /// Indica si Se realiza la actualización.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez - INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarRecargo(Recargo recargo)
        {
            return this.daoFacturacion.GuardarRecargo(recargo);
        }

        /// <summary>
        /// Método para guardar el responsable de la factura.
        /// </summary>
        /// <param name="responsable">The responsable.</param>
        /// <returns>Id del tercero.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarResponsableFactura(Responsable responsable)
        {
            return this.daoFacturacion.GuardarResponsableFactura(responsable);
        }

        /// <summary>
        /// Metodo Para Guardar El Tercero.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Id Tercero Creado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarTercero(Tercero tercero)
        {
            return this.daoFacturacion.GuardarTercero(tercero);
        }

        /// <summary>
        /// Método para almacenar el detalle de venta.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 12/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarVenta(Venta venta)
        {
            return this.daoFacturacion.GuardarVenta(venta);
        }

        /// <summary>
        /// Método para almacenar el detalle de venta.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarVentaDetalle(VentaDetalle venta)
        {
            return this.daoFacturacion.GuardarVentaDetalle(venta);
        }

        /// <summary>
        /// Guardars the venta factura.
        /// </summary>
        /// <param name="ventaFactura">The venta factura.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarVentaFactura(VentaFactura ventaFactura, string numeroFactura)
        {
            return this.daoFacturacion.GuardarVentaFactura(ventaFactura, numeroFactura);
        }

        /// <summary>
        /// Guardars the venta producto relacion.
        /// </summary>
        /// <param name="ventaRelacion">The venta relacion.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarVentaProductoRelacion(VentaProductoRelacion ventaRelacion)
        {
            return this.daoFacturacion.GuardarVentaProductoRelacion(ventaRelacion);
        }

        /// <summary>
        /// Método para insertar una vinculación.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Registro insertado.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarVinculacion(Vinculacion vinculacion)
        {
            return this.daoFacturacion.GuardarVinculacion(vinculacion);
        }

        /// <summary>
        /// Metodo para guardar las ventas vinculadas.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Id de la Vinculacion de la Venta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarVinculacionVentas(VinculacionVenta vinculacion)
        {
            return this.daoFacturacion.GuardarVinculacionVentas(vinculacion);
        }

        /// <summary>
        /// Insertars the venta paquete.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Retorna numero de venta.</returns>
        public int InsertarVentaPaquete(EstadoCuentaEncabezado estadoCuenta)
        {
            return this.daoFacturacion.InsertarVentaPaquete(estadoCuenta);
        }

        /// <summary>
        /// Insertars the venta paquetes detalle.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <param name="paquete">The paquete.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <param name="pyg">if set to <c>true</c> [pyg].</param>
        /// <returns>Retorna el numero de lote.</returns>
        public int InsertarVentaPaquetesDetalle(EstadoCuentaEncabezado estadoCuenta, Paquete paquete, int numeroVenta, bool pyg)
        {
            return this.daoFacturacion.InsertarVentaPaquetesDetalle(estadoCuenta, paquete, numeroVenta, pyg);
        }

        /// <summary>
        /// Consulta datos de un cliente.
        /// </summary>
        /// <param name="identificadorCliente">The id cliente.</param>
        /// <returns>Objeto de tipo cliente.</returns>
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
            IEnumerable<Tercero> registros = null;
            using (DataTable filas = this.daoFacturacion.ObtenerCliente(identificadorCliente))
            {
                registros = from fila in filas.Select()
                            select new Tercero(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Consulta las exclusiones por contrato y manuales filtradas por CodigoEntidad, IdContrato, IdTercero y IdPlan.
        /// </summary>
        /// <param name="exclusion">Entidad con los propiedades que se necesitan como parametros para hacer la consulta.</param>
        /// <returns>Listado exclusion factura actividades.</returns>
        public List<ExclusionFacturaActividades> ObtenerExclusiones(ExclusionFacturaActividades exclusion)
        {
            IEnumerable<ExclusionFacturaActividades> registros = null;
            using (DataTable filas = this.daoFacturacion.ObtenerExclusiones(exclusion))
            {
                registros = from fila in filas.Select()
                            select new ExclusionFacturaActividades(fila);
            }

            return registros.ToList();
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
            return this.daoFacturacion.ObtenerIdModulo();
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
            return this.daoFacturacion.ObtenerIdTipoMovimiento();
        }

        /// <summary>
        /// Obteners the reimpresion factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="agrupada">if set to <c>true</c> [agrupada].</param>
        /// <returns>Retorna un objeto.</returns>
        public object ObtenerReimpresionFactura(int numeroFactura, bool agrupada)
        {
            IEnumerable<EstadoCuentaAgrupado> listaEstadoCuentaAgrupada = null;
            IEnumerable<EstadoCuentaDetallado> listaEstadoCuentaDetallado = null;
            object objeto = null;

            using (DataTable filas = this.daoFacturacion.ObtenerReimpresionFactura(numeroFactura, agrupada))
            {
                if (agrupada)
                {
                    listaEstadoCuentaAgrupada = new List<EstadoCuentaAgrupado>();

                    listaEstadoCuentaAgrupada = from fila in filas.Select()
                                                select new EstadoCuentaAgrupado(fila);

                    objeto = listaEstadoCuentaAgrupada.ToList();
                }
                else
                {
                    listaEstadoCuentaDetallado = new List<EstadoCuentaDetallado>();

                    listaEstadoCuentaDetallado = from fila in filas.Select()
                                                 select new EstadoCuentaDetallado(fila);

                    objeto = listaEstadoCuentaDetallado.ToList();
                }
            }

            return objeto;
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
            IEnumerable<Tercero> registros = null;
            using (DataTable filas = this.daoFacturacion.ObtenerTercero(identificadorTercero))
            {
                registros = from fila in filas.Select()
                            select new Tercero(fila);
            }

            return registros.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los tipos de componentes 
        /// </summary>
        /// <returns>Lista con todos los componentes</returns>
        public List<string> ObtenerTiposComponentes()
        {
            return this.daoFacturacion.ObtenerTiposComponentes();
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
            return this.daoFacturacion.ObtenerVentasPorFactura(notaCredito);
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
        public void ProcesoComplementarioAnulacionNC(NotaCredito notaCredito)
        {
            this.daoFacturacion.ProcesoComplementarioAnulacionNC(notaCredito);
        }

        /// <summary>
        /// Redondeo Country.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <returns>Retorna Redondeo Country.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 24/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Redondeo Country.
        /// </remarks>
        public System.Collections.ArrayList RedondeoCountry(int contrato)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            DataTable dt = this.daoFacturacion.RedondeoCountry(contrato);
            foreach (DataRow dr in dt.Rows)
            {
                List<int> valores = new List<int>();
                valores.Add(Convert.ToInt32(dr[0]));
                valores.Add(Convert.ToInt32(dr[1]));
                valores.Add(Convert.ToInt32(dr[2]));
                valores.Add(Convert.ToInt32(dr[3]));
                list.Add(valores);
            }

            return list;
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
            System.Collections.ArrayList list = new System.Collections.ArrayList();

            DataTable dt = this.daoFacturacion.RedondeoCountryxID(contrato, identificadorManual);

            foreach (DataRow dr in dt.Rows)
            {
                List<int> valores = new List<int>();
                valores.Add(Convert.ToInt32(dr[0]));
                valores.Add(Convert.ToInt32(dr[1]));
                valores.Add(Convert.ToInt32(dr[2]));
                valores.Add(Convert.ToInt32(dr[3]));
                list.Add(valores);
            }

            return list;
        }

        /// <summary>
        /// Valida si la atencion esta bloqueada.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <returns>
        /// Retorna Validacion.
        /// </returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom.
        /// FechaDeCreacion: 07/05/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public string ValidarAtencionBloqueada(int identificadorAtencion)
        {
            return this.daoFacturacion.ValidarAtencionBloqueada(identificadorAtencion);
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
            return this.daoFacturacion.ValidarMovimientosRS(numeroFactura);
        }

        /// <summary>
        /// Validars the naturaleza.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Retorna un string.</returns>
        public string ValidarNaturaleza(int identificadorTercero)
        {
            return this.daoFacturacion.ValidarNaturaleza(identificadorTercero);
        }

        /// <summary>
        /// Validars the rol usuario.
        /// </summary>
        /// <param name="usuario">Parámetro usuario.</param>
        /// <param name="rol">Parámetro rol.</param>
        /// <returns>Retorna un entero.</returns>
        public int ValidarRolUsuario(string usuario, string rol)
        {
            return this.daoFacturacion.ValidarRolUsuario(usuario, rol);
        }

        /// <summary>
        /// Validars the venta terminada.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <param name="identificadorTransaccion">The identificador transaccion.</param>
        /// <param name="codigoEntidad">The codigo entidad.</param>
        /// <param name="identificadorContrato">The id contrato.</param>
        /// <returns>Retorna un booleano.</returns>
        public bool ValidarVentaTerminada(int numeroFactura, int numeroVenta, int identificadorTransaccion, string codigoEntidad, int identificadorContrato)
        {
            bool resultado = false;
            var ventaTerminada = (from fila in this.daoFacturacion.ValidarVentaTerminada(numeroFactura, numeroVenta, identificadorTransaccion, codigoEntidad, identificadorContrato).AsEnumerable()
                                  select fila.Field<bool>("VentaTerminada")).SingleOrDefault();
            resultado = Convert.ToBoolean(ventaTerminada);
            return resultado;
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}