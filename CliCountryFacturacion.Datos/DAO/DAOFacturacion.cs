// --------------------------------
// <copyright file="DAOFacturacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>DAO Facturacion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using CliCountry.Facturacion.Datos.Recursos;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.SAHI.Comun.AccesoDatos;
    using CliCountry.SAHI.Comun.Aspectos;
    using CliCountry.SAHI.Comun.AuditoriaBase;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Auditoria;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion.Ventas;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Se encarga de la persistencia de datos relacionados con la atención de facturación por relación.
    /// </summary>
    public class DAOFacturacion : OperacionesBase
    {
        #region Constructores 

        /// <summary>
        /// Constructor DAOFacturacion.
        /// </summary>
        /// <param name="nombreConexion">Nombre de la cadena de conexión a la base de datos.</param>
        public DAOFacturacion(string nombreConexion)
            : base(nombreConexion)
        {
        }

        #endregion Constructores 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Actualizas the estado venta paquetes.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        public void ActualizaEstadoVentaPaquetes(string numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, int.Parse(numeroFactura)));
            this.Actualizar("FacActualizarEstadosVentas", parametros);
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
        public int ActualizarAtencion(AtencionCliente atencionCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IndMovimiento_Param, DbType.Boolean, atencionCliente.IndMovimiento));
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdAtencion_Param, DbType.Int32, atencionCliente.IdAtencion));
            return this.Actualizar(OperacionesBaseDatos.Atencion_Actualizar, parametros);
        }

        /// <summary>
        /// Actualiza la venta con el usuario para bloquear o desbloquear la atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Indicador de exito de operacion.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturacionActividades_Atencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturacionActividades_Usuario_Param, DbType.String, usuario));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.FacturacionActividades_BloquearAtencion, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimiento_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ConceptoContabilidad_Actualizar, parametros));
        }

        /// <summary>
        /// Metodo Para Actualizar Conceptos de Cobro.
        /// </summary>
        /// <param name="conceptoCobro">The concepto cobro.</param>
        /// <returns>Indica Si Se realiza la inserción.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarConceptosCobro(FacturaAtencionConceptoCobro conceptoCobro)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_IdAtencion_Param, DbType.String, conceptoCobro.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_IdAtencionConcepto_Param, DbType.Int32, conceptoCobro.IdAtencionConcepto));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_NumeroFactura_Param, DbType.Int32, conceptoCobro.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_ValorConcepto_Param, DbType.Decimal, Math.Truncate(conceptoCobro.ValorConcepto)));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_ValorSaldo_Param, DbType.Decimal, Math.Truncate(conceptoCobro.ValorSaldo)));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionConceptoCobro_IndHabilitado_Param, DbType.Decimal, conceptoCobro.IndHabilitado));
            return this.Actualizar(OperacionesBaseDatos.Facturacion_ActualizarConcepto, parametros);
        }

        /// <summary>
        /// Método para actualizar la condición de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Valor de Confirmacion.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodigoEntidad_Param, DbType.String, condicionCubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdCondicionTarifa_Param, DbType.Int32, condicionCubrimiento.Id));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoRelacion_Param, DbType.Int16, condicionCubrimiento.NumeroTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTercero_Param, DbType.Int32, condicionCubrimiento.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdContrato_Param, DbType.Int32, condicionCubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdPlan_Param, DbType.Int32, condicionCubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdAtencion_Param, DbType.Int32, condicionCubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodAteConIde_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodAteTecIde_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_ValorPropio_Param, DbType.Decimal, condicionCubrimiento.ValorPropio));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_VigenciaCondicion_Param, DbType.DateTime, condicionCubrimiento.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_DescripcionCondicion_Param, DbType.String, condicionCubrimiento.DescripcionCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TipoCondicion_Param, DbType.String, condicionCubrimiento.Tipo));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IndHabilitado_Param, DbType.Int16, condicionCubrimiento.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodTisIde_Param, DbType.Int32, condicionCubrimiento.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodSerIde_Param, DbType.Int32, condicionCubrimiento.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdGrupoProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TicCod_Param, DbType.String, condicionCubrimiento.Componente));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TarManCod_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TarVig_Param, DbType.DateTime, condicionCubrimiento.VigenciaTarifa));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodIndFac_Param, DbType.Int16, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodFec_Param, DbType.DateTime, condicionCubrimiento.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodUsuCod_Param, DbType.String, string.Empty));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_Porcentaje_Param, DbType.Decimal, condicionCubrimiento.ValorPorcentaje));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdClaseCubrimiento_Param, DbType.Int32, condicionCubrimiento.Cubrimiento.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_AltTarManCod_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_AltTarVig_Param, DbType.DateTime, DateTime.Now));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodIndUnd_Param, DbType.Int16, 0));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.CondicionCubrimiento_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_CodigoEntidad_Param, DbType.String, condicionTarifa.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdCondicionTarifa_Param, DbType.Int32, condicionTarifa.Id));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoRelacion_Param, DbType.Int16, condicionTarifa.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTercero_Param, DbType.Int32, condicionTarifa.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdPlan_Param, DbType.Int32, condicionTarifa.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencion_Param, DbType.Int32, condicionTarifa.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencionContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencionPlan_Param, DbType.Int32, condicionTarifa.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_ValorPropio_Param, DbType.Decimal, condicionTarifa.ValorPropio));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaCondicion_Param, DbType.DateTime, condicionTarifa.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Descripcion_Param, DbType.String, condicionTarifa.DescripcionCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Tipo_Param, DbType.String, condicionTarifa.Tipo));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndHabilitado_Param, DbType.Int16, condicionTarifa.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoAtencion_Param, DbType.Int32, condicionTarifa.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdServicio_Param, DbType.Int32, condicionTarifa.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoProducto_Param, DbType.Int32, condicionTarifa.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdGrupoProducto_Param, DbType.Int16, condicionTarifa.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdProducto_Param, DbType.Int32, condicionTarifa.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Componente_Param, DbType.String, condicionTarifa.Componente));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdManual_Param, DbType.Int32, condicionTarifa.IdManual));

            if (condicionTarifa.VigenciaTarifa.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaManual_Param, DbType.DateTime, condicionTarifa.VigenciaTarifa));
            }

            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndUnidad_Param, DbType.Int16, condicionTarifa.SoloUnidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndFacturacion_Param, DbType.Int16, 1));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Usuario_Param, DbType.String, condicionTarifa.Usuario));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdManualAlt_Param, DbType.Int32, condicionTarifa.IdManualAlterno));

            if (condicionTarifa.VigenciaTarifaAlterna.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaManualAlt_Param, DbType.DateTime, condicionTarifa.VigenciaTarifaAlterna));
            }

            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Porcentaje_Param, DbType.Decimal, condicionTarifa.ValorPorcentaje));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdCubrimiento_Param, DbType.Int32, 0));
            return Convert.ToInt32(this.Actualizar(OperacionesBaseDatos.CondicionTarifa_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_CodigoEntidad_Param, DbType.String, convenioNoClinico.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdTercero_Param, DbType.Int32, convenioNoClinico.Tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_NombreAproximacion_Param, DbType.String, convenioNoClinico.NombreAproximacion));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_PorcentajeTarifa_Param, DbType.Decimal, convenioNoClinico.Porcentaje));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_DigitoVerificacion_Param, DbType.Int16, convenioNoClinico.NumeroDigitos));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IndHabilitado_Param, DbType.Int16, convenioNoClinico.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdConceptoCartera_Param, DbType.Int32, convenioNoClinico.IdConceptoCartera));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_VigenciaTarifa_Param, DbType.DateTime, convenioNoClinico.VigenciaManual));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdManual_Param, DbType.Int32, convenioNoClinico.IdManual));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdTipoEmpresa_Param, DbType.Int32, convenioNoClinico.IdTipoEmpresa));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IndRenovacion_Param, DbType.Boolean, convenioNoClinico.RenovacionAutomatica));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ConvenioNoClinico_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, cubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdContrato_Param, DbType.Int32, cubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdPlan_Param, DbType.Int32, cubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdAtencion_Param, DbType.Int32, cubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdTipoProducto_Param, DbType.Int32, cubrimiento.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdGrupoProducto_Param, DbType.Int32, cubrimiento.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdProducto_Param, DbType.Int32, cubrimiento.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoComponente_Param, DbType.String, cubrimiento.Componente));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdClaseCubrimiento_Param, DbType.Int32, cubrimiento.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, cubrimiento.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoUsuario_param, DbType.String, cubrimiento.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdCubrimiento_Param, DbType.String, cubrimiento.IdCubrimiento));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Cubrimientos_Actualizar, parametros));
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
        public int ActualizarDescuento(DescuentoConfiguracion descuento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoEntidad_Param, DbType.String, descuento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdDescuento_Param, DbType.Int32, descuento.Id));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTercero_Parm, DbType.Int32, descuento.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContrato_Param, DbType.Int32, descuento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlan_Param, DbType.Int32, descuento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoAtencion_Param, DbType.Int32, descuento.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdServicio_Parm, DbType.Int32, descuento.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProductoTipo_Param, DbType.Int16, descuento.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdGrupoProducto_Param, DbType.Int16, descuento.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProducto_Param, DbType.Int32, descuento.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdAtencion_Param, DbType.Int32, descuento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContratoA_Parm, DbType.Int32, descuento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlanAtencion_Parm, DbType.Int32, descuento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_FechaIni_Param, DbType.DateTime, descuento.FechaInicial));

            if (descuento.FechaFinal.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.Descuentos_FechaFin_Param, DbType.DateTime, descuento.FechaFinal));
            }

            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoRelacion_Param, DbType.Int16, descuento.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_ValosDescuento_Parm, DbType.Decimal, descuento.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IndHabilitado_Param, DbType.Int16, descuento.IndicadorActivo));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoComponente_Param, DbType.String, descuento.Componente));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_DecIndFac_Parm, DbType.Int32, descuento.IndicadorFacturaDescuento));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IndVisualizacion, DbType.String, descuento.IndVisualizacion));
            return Convert.ToInt32(this.Actualizar(OperacionesBaseDatos.Descuentos_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimiento_Param, DbType.String, notaCredito.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNumeroNotaCredito_Param, DbType.Int32, notaCredito.IdNumeroNotaCredito));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.EstadoCuentaCartera_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            return this.Actualizar(OperacionesBaseDatos.EstadoFactura_Actualizar, parametros);
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
        public int ActualizarEstadoProcesoFactura(ProcesoFactura procesoFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdProceso_Param, DbType.String, procesoFactura.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdEstado_Param, DbType.Byte, procesoFactura.IdEstado));
            return this.Actualizar(OperacionesBaseDatos.ProcesoFactura_ActualizarEstado, parametros);
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
        public int ActualizarEstadoProcesoFacturaNC(ProcesoFactura procesoFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdProceso_Param, DbType.String, procesoFactura.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdEstado_Param, DbType.Byte, procesoFactura.IdEstado));
            return this.Actualizar("FACActualizarEstadoProcesoFacturaNC", parametros);
        }

        /// <summary>
        /// Método que actualiza el estado de las ventas.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <param name="numeroVenta">El número de venta asociado.</param>
        /// <returns>
        /// El resultado de la operación.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 29/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaAnulacion(NotaCredito notaCredito, int numeroVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroVenta_Param, DbType.Int32, numeroVenta));

            return this.Actualizar(OperacionesBaseDatos.AnulacionFactura_ActualizarEstadoVentaAnulacion, parametros);
        }

        /// <summary>
        /// Actualiza el estado de cada venta asociada a una factura durante el proceso de anulación - Facturación No Clínico.
        /// </summary>
        /// <param name="notaCredito">The nota credito.</param>
        /// <returns>
        /// El resultado de la operación.
        /// </returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo.
        /// FechaDeCreacion: 06/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public int ActualizarEstadoVentaAnulacionNC(NotaCredito notaCredito)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));

            return this.Actualizar("FACActualizarEstadoVentaAnulacionNC", parametros);
        }

        /// <summary>
        /// Método que actualiza el estado de las ventas.
        /// </summary>
        /// <param name="ventaEstadoFactura">The detalle factura.</param>
        /// <returns>Indica si se actualizo bien el Estado de Venta.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_CodigoEntidad_Param, DbType.String, ventaEstadoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_IdAtencion_Param, DbType.Int32, ventaEstadoFactura.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_IdVenta_Param, DbType.Int32, ventaEstadoFactura.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_NumeroVenta_Param, DbType.Int32, ventaEstadoFactura.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_Estado_Param, DbType.String, ventaEstadoFactura.EstadoVenta));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VentaEstadoFactura_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_CodigoEntidad_Param, DbType.String, ventaEstadoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_IdAtencion_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_IdVenta_Param, DbType.Int32, ventaEstadoFactura.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_NumeroVenta_Param, DbType.Int32, ventaEstadoFactura.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_Estado_Param, DbType.String, ventaEstadoFactura.EstadoVenta));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VentaEstadoFactura_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IndReenvio_Param, DbType.Int16, notaCredito.IndReenvio));
            return this.Actualizar(OperacionesBaseDatos.EstadoVentasAnulacion_Actualizar, parametros);
        }

        /// <summary>
        /// Método para actualizar la información de la exclusión de contrato.
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_CodigoEntidad_Param, DbType.String, exclusion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdExclusion_Param, DbType.Int32, exclusion.Id));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdTercero_Param, DbType.Int32, exclusion.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdTipoAtencion_Param, DbType.Int32, exclusion.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdServicio_Param, DbType.Int32, exclusion.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdManual_Param, DbType.Int32, exclusion.IdManual));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_VigenciaManual_Param, DbType.DateTime, exclusion.VigenciaTarifa));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdProductoTipo_Param, DbType.Int32, exclusion.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdGrupo_Param, DbType.Int32, exclusion.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdProducto_Param, DbType.Int32, exclusion.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_CodigoComponente_Param, DbType.String, exclusion.Componente));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IndHabilitado_Param, DbType.Int16, exclusion.IndicadorContratoActivo));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdContrato_Param, DbType.Int32, exclusion.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdPlan_Param, DbType.Int32, exclusion.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdAtencion_Param, DbType.Int32, exclusion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdVenta_Param, DbType.Int32, exclusion.IdVenta));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_NumeroVenta_Param, DbType.Int32, exclusion.NumeroVenta));
            parametros.Add(this.CrearParametro("IndAplicarSiempre", DbType.Int16, exclusion.IndicadorContratoAplicado));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ExclusionContrato_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCuenta_Param, DbType.Int32, identificadorCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.FacturaNumeroCuenta_Actualizar, parametros));
        }

        /// <summary>
        /// Metodo para actualizar movimientos en tesoreria.
        /// </summary>
        /// <param name="movimiento">The movimiento.</param>
        /// <returns>Indica Si Se realiza la inserción.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 13/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ActualizarMovimientosTesoreria(FacturaAtencionMovimiento movimiento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionMovimiento_IdAtencion_Param, DbType.String, movimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionMovimiento_IdMovimiento_Param, DbType.Int32, movimiento.IdMovimiento));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionMovimiento_ValorSaldo_Param, DbType.Decimal, Math.Truncate(movimiento.ValorSaldo)));
            return this.Actualizar(OperacionesBaseDatos.Facturacion_ActualizarMovimiento, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCuenta_Param, DbType.Int32, identificadorCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_NumeroResultadoDocumento_Param, DbType.Int32, numeroFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.CuentaCarteraNumeroFactura_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdRecargo_Param, DbType.Int32, recargo.Id));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTercero_Param, DbType.Int32, recargo.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IDEntidad_Param, DbType.String, recargo.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdContrato_Param, DbType.Int32, recargo.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdPlan_Param, DbType.Int32, recargo.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTipoProducto_Param, DbType.Int32, recargo.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdNombreTipoProducto_Param, DbType.String, recargo.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdGrupoProducto_Param, DbType.Int16, recargo.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_NombreGrupoProducto_Param, DbType.String, recargo.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdProducto_Param, DbType.Int32, recargo.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_NombreProducto_Param, DbType.String, recargo.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_Componente_Param, DbType.String, recargo.Componente));
            parametros.Add(this.CrearParametro(Parametros.Recargo_TipoRecargo_Param, DbType.String, recargo.TipoRecargo));
            parametros.Add(this.CrearParametro(Parametros.Recargo_CodServicio_Param, DbType.Int32, recargo.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.Recargo_FechaVigenciaI_Param, DbType.DateTime, recargo.FechaInicial));
            parametros.Add(this.CrearParametro(Parametros.Recargo_FechaVigenciaf_Param, DbType.DateTime, recargo.FechaInicial));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IndHabilitado_Param, DbType.Int16, recargo.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTipoAtencion_Param, DbType.Int32, recargo.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Recargo_Porcentaje_Param, DbType.Double, recargo.PorcentajeRecargo));

            return Convert.ToInt32(this.Actualizar(OperacionesBaseDatos.Recargo_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimiento_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.MovimientosFactura_Actualizar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdVentaProductoRelacion_Param, DbType.Int32, ventaRelacion.IdVentaProductoRelacion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IndHabilitado_Param, DbType.Boolean, ventaRelacion.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_UsuarioModificacion_Param, DbType.String, ventaRelacion.Usuario));
            return this.Actualizar(OperacionesBaseDatos.VentaProductoRelacion_Actualizar, parametros);
        }

        /// <summary>
        /// Método para actualizar información de la vinculación.
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_CodigoEntidad_Param, DbType.String, vinculacion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdAtencion_Param, DbType.Int32, vinculacion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdContrato_Param, DbType.Int32, vinculacion.Contrato.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdPlan_Param, DbType.Int32, vinculacion.Plan.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdTipoAfiliacion_Param, DbType.Int32, vinculacion.IdTipoAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdEstado_Param, DbType.Int32, vinculacion.IdEstado));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_Orden_Param, DbType.Int16, vinculacion.Orden));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_NumeroAfiliacion_Param, DbType.String, vinculacion.NumeroAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IndHabilitado_Param, DbType.Int16, vinculacion.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_MontoEjecutado_Param, DbType.Decimal, vinculacion.MontoEjecutado));
            return this.Actualizar(OperacionesBaseDatos.AtencionVinculacion_Actualizar, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_CodigoEntidad_Param, DbType.String, vinculacion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdAtencionPadre_Param, DbType.Int32, vinculacion.IdAtencionPadre));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdAtencionVinculada_Param, DbType.Int32, vinculacion.IdAtencionVinculada));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdVenta_Param, DbType.Int32, vinculacion.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_NumeroVenta_Param, DbType.Int32, vinculacion.NumeroVenta));
            return this.Actualizar(OperacionesBaseDatos.VinculacionVentas_Actualizar, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("IdVenta", DbType.Int32, key));
            parametros.Add(this.CrearParametro("IdProceso", DbType.Int32, value));
            return Convert.ToString(this.Ejecutar("FACAnularVentaNoClinica", parametros));
        }

        /// <summary>
        /// Método de Auditoria de BilVentasDetalle.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de verficacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int AuditoriaVentaDetalles(AuditoriaVentaDetalle detalleVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_CodigoEntidad_Param, DbType.String, detalleVenta.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Usuario_Param, DbType.String, detalleVenta.Usuario));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Estado_Param, DbType.String, detalleVenta.Estado));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_NumeroVenta_Param, DbType.Int32, detalleVenta.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdTransaccion_Param, DbType.Int32, detalleVenta.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdProducto_Param, DbType.Int32, detalleVenta.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Cantidad_Param, DbType.Int32, detalleVenta.Cantidad));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.DetalleVenta_Auditoria, parametros));
        }

        /// <summary>
        /// Método para Borrar productos de BilVentasDetalle.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Valor de Confirmacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int BorrarVentaDetalles(VentaDetalle detalleVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_CodigoEntidad_Param, DbType.String, detalleVenta.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdTransaccion_Param, DbType.Int32, detalleVenta.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_NumeroVenta_Param, DbType.Int32, detalleVenta.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdProducto_Param, DbType.Int32, detalleVenta.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Cantidad_Param, DbType.Int32, detalleVenta.Cantidad));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.DetalleVenta_Borrar, parametros));
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
        public int BorrarVentaDetallesNC(VentaDetalle detalleVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_CodigoEntidad_Param, DbType.String, detalleVenta.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdTransaccion_Param, DbType.Int32, detalleVenta.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_NumeroVenta_Param, DbType.Int32, detalleVenta.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdProducto_Param, DbType.Int32, detalleVenta.IdProducto));

            return Convert.ToInt32(this.Ejecutar("FACBorrarDetalleVentaNC", parametros));
        }

        /// <summary>
        /// Realiza el cierre de facturacion.
        /// </summary>
        /// <param name="tipoCierre">The tipo cierre.</param>
        /// <returns>Retorna un entero.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public int CierreFacturacion(string tipoCierre)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("TipoCierre", DbType.String, tipoCierre));

            DataTable dt = this.Consultar("FACCierreFacturacion", parametros);

            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// Consultas the procedimientos QX atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="paginaActual">The pagina actual.</param>
        /// <param name="longitudPagina">The longitud pagina.</param>
        /// <returns>Retorna un DataTable.</returns>
        public DataTable ConsultaProcedimientosQXAtencion(int identificadorAtencion, int paginaActual, int longitudPagina)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, longitudPagina));

            return this.Consultar(OperacionesBaseDatos.ProcedimientosQXAtencion_Consultar, parametros);
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
        public DataTable ConsultarAproximaciones()
        {
            return this.Consultar(OperacionesBaseDatos.Aproximacion_ConsultarTodos);
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
        public DataTable ConsultarAproximacionesActivas()
        {
            return this.Consultar("FACSeleccionarAproximacionesActivas");
        }

        /// <summary>
        /// Metodo para consultar atencion por cliente de facturación actividad.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Lista de datos AtencionCliente.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarAtencionCliente(int identificadorAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            return this.Consultar(OperacionesBaseDatos.Atencion_Consultar, parametros);
        }

        /// <summary>
        /// Consulta las atenciones de factura por relacion.
        /// </summary>
        /// <param name="facturaAtencion">The factura atencion.</param>
        /// <returns>Lista de Datos de Atenciones.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las atenciones de factura por relacion.
        /// </remarks>
        public DataTable ConsultarAtenciones(FacturaAtencionRelacion facturaAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_Codigo_Param, DbType.String, facturaAtencion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_IdContrato_Param, DbType.Int32, facturaAtencion.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_IdTipoAtencion_Param, DbType.Int32, facturaAtencion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_AtencionOrdenPrioridad_Param, DbType.Int16, facturaAtencion.AtencionOrdenPrioridad));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_EstadoAtencion_Param, DbType.Int16, facturaAtencion.EstadoAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_IdSede_Param, DbType.Int32, facturaAtencion.IdSede));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_FechaInicial_Param, DbType.DateTime, facturaAtencion.FechaIngresoInicial));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_FechaFinal_Param, DbType.DateTime, facturaAtencion.FechaIngresoFinal));
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencionRelacion_Ubicaciones_Param, DbType.String, facturaAtencion.Ubicaciones));
            return (DataTable)this.EjecutarProcedimientoTipo2(OperacionesBaseDatos.Facturacion_ConsultarAtenciones, parametros);
        }

        /// <summary>
        /// Permite Consultar las atenciones pendientes por procesar.
        /// </summary>
        /// <param name="facturaAtencion">The proceso factura.</param>
        /// <returns>Lista de Datos de Atenciones Pendientes por Procesar.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public DataTable ConsultarAtencionesPendientesxProcesar(FacturaAtencion facturaAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencion_IdProceso_Param, DbType.String, facturaAtencion.IdProceso));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2(OperacionesBaseDatos.Facturacion_SeleccionarAtencionesPendientesxProcesar, parametros);
            return dt;
        }

        /// <summary>
        /// Consulta los detalles de causales existentes.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Retorna un datatable con las causales tipo existentes.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarCausalesDetallePaginado(Paginacion<CausalDetalle> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CausalDetalle_CodigoEntidad_Param, DbType.String, paginacion.Item.Codigo));
            parametros.Add(this.CrearParametro(Parametros.CausalDetalle_Id_Param, DbType.Int32, paginacion.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.CausalDetalle_Nombre_Param, DbType.String, paginacion.Item.Nombre));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.CausalDetalle_ConsultarTodos, parametros);
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
        public DataTable ConsultarClasesCubrimiento(ClaseCubrimiento claseCubrimiento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ClaseCubrimiento_CodigoEntidad_Param, DbType.String, claseCubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ClaseCubrimiento_IndHabilitado_Param, DbType.Int16, claseCubrimiento.IndHabilitado));
            return this.Consultar(OperacionesBaseDatos.ClaseCubrimiento_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para consultar Clientes.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Clientes de Facturacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarClientesAtencion(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarClientes, parametros);
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
        public DataTable ConsultarClientesAtencionNC(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar("FACSeleccionarAtencionClienteNC", parametros);
        }

        /// <summary>
        /// Consultar cliente asociado a una atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Silvia Lorena López Camacho - INTERGRUPO\slopez.
        /// FechaDeCreacion: 19/02/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarClientexAtencion(int identificadorAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            return this.Consultar(OperacionesBaseDatos.Cliente_Atencion_Consultar, parametros);
        }

        /// <summary>
        /// Permite consultar los componentes.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <returns>
        /// Lista de datos.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarComponentes(int identificadorAtencion, int identificadorTipoProducto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoProducto_Param, DbType.Int32, identificadorTipoProducto));
            return this.Consultar(OperacionesBaseDatos.Componentes_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para Consultar Productos No QX.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>
        /// Producto no Qx.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarComponentesNoQx(ProductoVenta producto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdProducto_Param, DbType.Int32, producto.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Venta_FechaVenta_Param, DbType.DateTime, producto.FechaVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdManual_Param, DbType.Int32, producto.Tarifa == null ? 0 : producto.Tarifa.IdManual));
            return this.Consultar(OperacionesBaseDatos.ComponentesNoQx, parametros);
        }

        /// <summary>
        /// Metodo para Consultar Componentes Qx.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>
        /// Componentes QX.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarComponentesQx(ProductoVenta producto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdProducto_Param, DbType.Int32, producto.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Venta_FechaVenta_Param, DbType.DateTime, producto.FechaVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdManual_Param, DbType.Int32, producto.Tarifa == null ? 0 : producto.Tarifa.IdManual));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_NivOrd, DbType.Int16, producto.Tarifa == null ? 0 : producto.Tarifa.NivelComplejidad));
            return this.Consultar(OperacionesBaseDatos.ComponentesQx, parametros);
        }

        /// <summary>
        /// Consultar Componentes X Producto.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>
        /// Resultado operacion.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 22/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarComponentesXProducto(ProductoVenta producto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Componente_Producto_Param, DbType.Int32, producto.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Componente_Fecha_Param, DbType.DateTime, producto.FechaVenta));
            return this.Consultar(OperacionesBaseDatos.ComponenteXProducto, parametros);
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
        public DataTable ConsultarConceptos(Atencion atencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdAtencion_Param, DbType.Int32, atencion.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.Concepto_ObtenerConcepto, parametros);
        }

        /// <summary>
        /// Método para consultar los conceptos de cartera.
        /// </summary>
        /// <param name="conceptoCartera">The concepto cartera.</param>
        /// <returns>Lista de conceptos de cartera.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarConceptosCartera(ConceptoCartera conceptoCartera)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ConceptoCartera_IdConcepto_Param, DbType.Int32, conceptoCartera.IdConcepto));
            return this.Consultar(OperacionesBaseDatos.ConceptoCartera_Consultar, parametros);
        }

        /// <summary>
        /// Metodos para Consultar Conceptos de Cobro.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Conceptos de Cobro.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarConceptosCobro(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarConceptosCobro, parametros);
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
        public DataTable ConsultarConceptosCobroNC(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar("FACSeleccionarAtencionConceptosCobroNC", parametros);
        }

        /// <summary>
        /// Permite consultar las condiciones de un contrato.
        /// </summary>
        /// <param name="condicionContrato">The condicion contrato.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarCondicionesContrato(CondicionContrato condicionContrato)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionContrato_IdContrato_Param, DbType.Int32, condicionContrato.IdContrato));
            return this.Consultar(OperacionesBaseDatos.CondicionContrato_Consultar, parametros);
        }

        /// <summary>
        /// Método para el cargue de las condiciones de cubrimiento.
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
        public DataTable ConsultarCondicionesCubrimiento(Paginacion<CondicionCubrimiento> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, paginacion.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdCondicionTarifa_Param, DbType.Int32, paginacion.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTercero_Param, DbType.Int32, paginacion.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoRelacion_Param, DbType.Int16, paginacion.Item.NumeroTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdPlan_Param, DbType.Int32, paginacion.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreTercero_Param, DbType.String, paginacion.Item.Tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreContrato_Param, DbType.String, paginacion.Item.Contrato.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombrePlan_Param, DbType.String, paginacion.Item.Cubrimiento.Plan.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdClaseCubrimiento_Param, DbType.Int32, paginacion.Item.Cubrimiento.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoComponente_Param, DbType.String, paginacion.Item.Cubrimiento.TipoComponente.CodigoComponente));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2(OperacionesBaseDatos.CondicionCubrimiento_Consultar, parametros);
            return dt;
        }

        /// <summary>
        /// Consulta las condiciones de facturacion.
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
        public DataTable ConsultarCondicionesFacturacion(CondicionTarifa condicionTarifa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionesFacturacion_IdCodigoEntidad_Param, DbType.String, condicionTarifa.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesFacturacion_IdContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesFacturacion_IdPlan_Param, DbType.Int32, condicionTarifa.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesFacturacion_IdTercero_Param, DbType.Int32, condicionTarifa.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesFacturacion_IdAtencion_Param, DbType.Int32, condicionTarifa.IdAtencion));

            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarCondicionesFacturacion, parametros);
        }

        /// <summary>
        /// Metodo para cargar las condiciones de proceso.
        /// </summary>
        /// <returns>Resultado de Condiciones de Proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarCondicionesProceso()
        {
            return this.Consultar(OperacionesBaseDatos.CondicionesProceso_Seleccionar);
        }

        /// <summary>
        /// Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Lista de Datos de Condiciones de Tarifa.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las condiciones de tarifa aplicadas en el contrato a un producto.
        /// </remarks>
        public DataTable ConsultarCondicionesTarifas(CondicionTarifa condicionTarifa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_CodigoEntidad_Param, DbType.String, condicionTarifa.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTercero_Param, DbType.Int32, condicionTarifa.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_TipoRelacion_Param, DbType.String, condicionTarifa.TipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndHabilitado_Param, DbType.Int32, condicionTarifa.IndHabilitado));

            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarCondicionesTarifas, parametros);
        }

        /// <summary>
        /// Permite Consultar los planes o contratos en general.
        /// </summary>
        /// <param name="paginacion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarContratoPlan(Paginacion<ContratoPlan> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdContrato_Param, DbType.Int32, paginacion.Item.Contrato.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_NombreContrato_Param, DbType.String, paginacion.Item.Contrato.Nombre));
            parametros.Add(this.CrearParametro(Parametros.ContratoPlan_IdTercero_Param, DbType.Int32, paginacion.Item.Tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.ContratoPlan_NombreTercero_Param, DbType.String, paginacion.Item.Tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdPlan_Param, DbType.Int32, paginacion.Item.Plan.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_NombrePlan_Param, DbType.String, paginacion.Item.Plan.Nombre));
            parametros.Add(this.CrearParametro(Parametros.ContratoPlan_IndHabilitado_Param, DbType.Boolean, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarContratoPlan, parametros);
        }

        /// <summary>
        /// Metodo para realizar la consulta de COnvenios No Clinicos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de Resultado Convenio No Clinico.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarConvenioNoClinico(Paginacion<ConvenioNoClinico> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdTercero_Param, DbType.Int32, paginacion.Item.Tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IndHabilitado_Param, DbType.Boolean, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_NombreManual_Param, DbType.String, paginacion.Item.NombreManual));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_NombreTercero_Param, DbType.String, paginacion.Item.Tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_NumeroDocumento_Param, DbType.String, paginacion.Item.Tercero.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.ConvenioNoClinico_Consultar, parametros);
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
        public DataTable ConsultarCostoAsociado(CostoAsociado costoAsociado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CostoAsociado_idAtencion_Param, DbType.Int32, costoAsociado.IdAtencion));

            return this.Consultar(OperacionesBaseDatos.ConsultarCostoAsociado, parametros);
        }

        /// <summary>
        /// Permite consultar los cubrimientos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
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
        public DataTable ConsultarCubrimientos(Paginacion<Cubrimiento> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, paginacion.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdCubrimiento_Param, DbType.String, paginacion.Item.IdCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdTercero_Param, DbType.Int32, paginacion.Item.Tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreTercero_Param, DbType.String, paginacion.Item.Tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreContrato_Param, DbType.String, paginacion.Item.Contrato.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdPlan_Param, DbType.Int32, paginacion.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_DesPlan, DbType.String, paginacion.Item.Plan.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdTipoProducto_Param, DbType.Int32, paginacion.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdGrupoProducto_Param, DbType.Int32, paginacion.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreTipoProducto_Param, DbType.String, paginacion.Item.TipoProducto.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreGrupoProducto_Param, DbType.String, paginacion.Item.GrupoProducto.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreProducto_Param, DbType.String, paginacion.Item.Producto.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdProducto_Param, DbType.Int32, paginacion.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoComponente_Param, DbType.String, paginacion.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdClaseCubrimiento_Param, DbType.Int32, paginacion.Item.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreComponente_Param, DbType.String, paginacion.Item.TipoComponente.NombreComponente));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_NombreCubrimiento_Param, DbType.String, paginacion.Item.ClaseCubrimiento.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Cubrimiento_Consultar, parametros);
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
        public DataTable ConsultarCuentasCartera(int identificadorAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_NumeroFactura_Param, DbType.Int32, identificadorAtencion));
            return this.Consultar(OperacionesBaseDatos.CuentaCartera_Consultar, parametros);
        }

        /// <summary>
        /// Consultars the datos cierre.
        /// </summary>
        /// <returns>Retorna un string.</returns>
        public DataSet ConsultarDatosCierre()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            DataSet dt = (DataSet)this.EjecutarProcedimientoTipo2DS("FacSeleccionarDatosCierre", parametros);
            return dt;
        }

        /// <summary>
        /// Método para consultar despositos asociados a una atención.
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
        public DataTable ConsultarDepositos(Atencion atencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdAtencion_Param, DbType.Int32, atencion.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.Deposito_ObtenerDeposito, parametros);
        }

        /// <summary>
        /// Permite Consultar llos Descuentos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
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
        public DataTable ConsultarDescuentos(Paginacion<Descuento> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdDescuento_Param, DbType.Int32, paginacion.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTercero_Param, DbType.Int32, paginacion.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlan_Param, DbType.Int32, paginacion.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoProducto_Param, DbType.Int32, paginacion.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_DescripcionTipoProducto_Param, DbType.String, paginacion.Item.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdGrupoProducto_Param, DbType.Int32, paginacion.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_DescripcionGrupoProducto_Param, DbType.String, paginacion.Item.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProducto_Param, DbType.Int32, paginacion.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_DescripcionProducto_Param, DbType.String, paginacion.Item.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoComponente_Param, DbType.String, paginacion.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoRelacion_Param, DbType.Int16, paginacion.Item.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_Activo_Param, DbType.Int16, paginacion.Item.IndicadorActivo));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarDescuentos, parametros);
        }

        /// <summary>
        /// Consulta los descuentos aplicados a un contrato.
        /// </summary>
        /// <param name="descuento">The descuento.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción:.
        /// </remarks>
        public DataTable ConsultarDescuentosContrato(Descuento descuento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoEntidad_Param, DbType.String, descuento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTercero_Parm, DbType.Int32, descuento.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContrato_Param, DbType.Int32, descuento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IndActivo_Param, DbType.Int16, descuento.IndicadorActivo));

            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarDescuentosContrato, parametros);
        }

        /// <summary>
        /// Consulta los detalles del cliente.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Lista los detalles de los clientes.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarDetalleClienteFactura(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleCliente_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.FacturaDetalleCliente_Consulta, parametros);
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
        public DataTable ConsultarDetalleNoFacturable(NoFacturable parametroNoFacturable)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdAtencion_Param, DbType.Int32, parametroNoFacturable.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.NoFacturable_Consultar, parametros);
        }

        /// <summary>
        /// Consulta el detalle de la cirugía por el Id 
        /// </summary>
        /// <param name="identificadorCx">Id identifica al procedimiento Qx</param>
        /// <returns>Retorna un DataTable.</returns>
        /// <remarks>
        /// Autor: Juan Carlos Reyes Guerrero - INTERGRUPO\JREYESG
        /// FechaDeCreacion: 25/11/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta el detalle de la cirugía por el Id
        /// </remarks>
        public DataTable ConsultarDetalleQx(int identificadorCx)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdCx_Param, DbType.Int32, identificadorCx));

            return this.Consultar(OperacionesBaseDatos.ProcedimientosQXDetalle_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para consultar el detalle de la venta x Identificador de Proceso.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Lista de Detalles de LAs Ventas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarDetallesVenta(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarDetallesVenta, parametros);
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
        public DataTable ConsultarDetallesVentaNC(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar("FACSeleccionarDetalleVentaNC", parametros);
        }

        /// <summary>
        /// Permite Consultar los Detalle Tarifa.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Listado de Datos Detalles Tarifa.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarDetalleTarifa(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.DetalleTarifa_Consultar, parametros);
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
        public DataTable ConsultarDetalleTarifaManual(DetalleTarifa detalleTarifa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdManual_Param, DbType.Int32, detalleTarifa.IdManual));
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdTipoProducto_Param, DbType.Int16, detalleTarifa.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdUbicacion_Param, DbType.Int32, detalleTarifa.IdUbicacion));
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IndHabilitado_Param, DbType.Boolean, detalleTarifa.IndHabilitado));
            return this.Consultar(OperacionesBaseDatos.DetalleTarifaManual_Consultar, parametros);
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
        public DataTable ConsultarDetalleTarifaXManual(DetalleTarifa detalleTarifa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdManual_Param, DbType.Int32, detalleTarifa.IdManual));
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdProducto_Param, DbType.Int32, detalleTarifa.IdProducto));
            return this.Consultar(OperacionesBaseDatos.DetalleTarifaXManual_Consultar, parametros);
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
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarDetalleVentaFactura(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVentas_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.DetalleVentaFactura_Consulta, parametros);
        }

        /// <summary>
        /// Consulta El Encabezado del Estado de Cuenta.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Encabezado del Estado de Cuenta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarEstadoCuentaEncabezado(EstadoCuentaEncabezado estadoCuenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdProceso_Param, DbType.Int32, estadoCuenta.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdUsuarioFirma_Param, DbType.Int16, estadoCuenta.IdUsuarioFirma));
            return this.Consultar(OperacionesBaseDatos.EstadoCuentaEncabezado_Consultar, parametros);
        }

        /// <summary>
        /// Consultar encabezado de estado de cuenta multiple.
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
        public DataTable ConsultarEstadoCuentaEncabezadoMultiple(EstadoCuentaEncabezado estadoCuenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdProceso_Param, DbType.Int32, estadoCuenta.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdUsuarioFirma_Param, DbType.Int16, estadoCuenta.IdUsuarioFirma));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdAtencion_Param, DbType.Int32, estadoCuenta.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdPlan_Param, DbType.Int32, estadoCuenta.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdContrato_Param, DbType.Int32, estadoCuenta.IdContrato));
            return this.Consultar(OperacionesBaseDatos.EstadoCuentaEncabezado_ConsultarMultiple, parametros);
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
        public DataTable ConsultarEstadoCuentaEncabezadoNC(EstadoCuentaEncabezado estadoCuenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdProceso_Param, DbType.Int32, estadoCuenta.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaEncabezado_IdUsuarioFirma_Param, DbType.Int16, estadoCuenta.IdUsuarioFirma));
            return this.Consultar("FACSeleccionarEstadoCuentaEncabezadoNC", parametros);
        }

        /// <summary>
        /// Retorna el estado del proceso
        /// </summary>
        /// <param name="identificadorProceso">The identifier proceso.</param>
        /// <returns>Retorna un DataTable.</returns>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public DataTable ConsultarEstadoProceso(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("idProceso", DbType.Int32, identificadorProceso));
            return this.Consultar("FACConsultarEstadoProceso", parametros);
        }

        /// <summary>
        /// Metodo para consulta las exclusiones del Contrato por Atencion.
        /// </summary>
        /// <param name="paginacion">The exclusion.</param>
        /// <returns>
        /// Listado de Exclusiones del COntrato.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 30/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarExclusionesAtencionContrato(Paginacion<Exclusion> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Exclusion_EntCod_Param, DbType.String, paginacion.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdTercero_Param, DbType.Int32, paginacion.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IndHabilitado_Param, DbType.Int16, paginacion.Item.IndicadorContratoActivo));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdPlan_Param, DbType.Int32, paginacion.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.ExclusionesAtencionContrato_Consultar, parametros);
        }

        /// <summary>
        /// Consulta las exclusiones aplicadas a un contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 13/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción:.
        /// </remarks>
        public DataTable ConsultarExclusionesContrato(Exclusion exclusion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Exclusion_EntCod_Param, DbType.String, exclusion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Exclusion_CoxTecIde_Param, DbType.Int32, exclusion.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Exclusion_CoxConIde_Param, DbType.Int32, exclusion.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Exclusion_CoxIndAct_Param, DbType.Int16, exclusion.IndicadorContratoActivo));

            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarExclusionesContrato, parametros);
        }

        /// <summary>
        /// Consulta las exclusiones aplicadas a una tarifa.
        /// </summary>
        /// <param name="tarifaExclusion">The tarifa exclusion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción:.
        /// </remarks>
        public DataTable ConsultarExclusionesManual(ExclusionManual tarifaExclusion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ExclusionManual_CodigoEntidad_Param, DbType.String, tarifaExclusion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ExclusionManual_IdContrato_Param, DbType.Int32, tarifaExclusion.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ExclusionManual_IdTercero_Param, DbType.Int32, tarifaExclusion.IdTercero));

            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarExclusionesManual, parametros);
        }

        /// <summary>
        /// Consulta Factores QX.
        /// </summary>
        /// <returns>Factores QX.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias. 
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta Factores QX.
        /// </remarks>
        public DataTable ConsultarFactoresQX()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            return (DataTable)this.EjecutarProcedimientoTipo2("uspFACSeleccionarTodosFactoresQX", parametros);
        }

        /// <summary>
        /// Método para consultar los Factores QX de un manual vigente.
        /// </summary>
        /// <param name="factoresQX">The factores QX.</param>
        /// <returns>Liata los Factores QX de un manual.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarFactoresQX(FactoresQX factoresQX)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FactoresQX_CodigoEntidad_Param, DbType.String, factoresQX.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.FactoresQX_Componente_Param, DbType.String, factoresQX.Componente));
            parametros.Add(this.CrearParametro(Parametros.FactoresQX_IdManual_Param, DbType.Int32, factoresQX.IdManual));
            parametros.Add(this.CrearParametro(Parametros.FactoresQX_Vigencia_Param, DbType.DateTime, factoresQX.FechaVigencia));
            return this.Consultar(OperacionesBaseDatos.ConsultarFactoresQx, parametros);
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
        public DataTable ConsultarFacturaAnuladaDetalle(DetalleNotaCredito detalleNotaCredito)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNumeroNotaCredito_Param, DbType.Int32, detalleNotaCredito.IdNumeroNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, detalleNotaCredito.IdTipoMovimiento));
            return this.Consultar(OperacionesBaseDatos.FacturaAnuladaDetalle_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para consultar el detalle de los Componentes.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>LIsta de Componentes de la Factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 22/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarFacturaComponentes(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVentas_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.FacturaComponentes_Consultar, parametros);
        }

        /// <summary>
        /// Consultar Factura Detalle.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Detalles de la factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarFacturaDetalle(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalle_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.FacturaDetalle_Consultar, parametros);
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
        public DataTable ConsultarFacturaDetallePaquetes(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalle_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.FacturaDetallePaquetes_Consultar, parametros);
        }

        /// <summary>
        /// Consulta los detalles de la Factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Detalles de la Factura.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarFacturaDetallexNumeroFactura(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_NumeroFactura_Param, DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.FacturaDetalleVenta_Consultar, parametros);
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
        public DataTable ConsultarFacturaEncabezado(int numeroFactura, short identificadorUsuarioFirma)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaEncabezado_NumeroFactura_Param, DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaEncabezado_IdUsuarioFirma_Param, DbType.Int16, identificadorUsuarioFirma));
            return this.Consultar(OperacionesBaseDatos.FacturaEncabezado_Consultar, parametros);
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
        public DataTable ConsultarFacturas(Paginacion<FacturaResultado> facturaResultado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NumeroFactura_Param, DbType.Int32, facturaResultado.Item.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_TipoFactura_Param, DbType.String, facturaResultado.Item.TipoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_IdAtencion_Param, DbType.Int32, facturaResultado.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NombreTercero_Param, DbType.String, facturaResultado.Item.NombreTercero));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_Contrato_Param, DbType.String, facturaResultado.Item.Contrato));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NumeroPagina_Param, DbType.Int32, facturaResultado.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_CantidadRegistrosPagina_Param, DbType.Int32, facturaResultado.LongitudPagina));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_IndAnulacion_Param, DbType.Boolean, facturaResultado.Item.IndAnulacion));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_FacEst_Param, DbType.String, facturaResultado.Item.EstadoFactura));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarFacturas, parametros);
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
        public DataTable ConsultarFacturasAnuladas(Paginacion<NotaCredito> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNumeroNotaCredito_Param, DbType.Int32, paginacion.Item.IdNumeroNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, paginacion.Item.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoUsuario_Param, DbType.String, paginacion.Item.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_FechaNota_Param, DbType.DateTime, paginacion.Item.FechaNota));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.NotasCredito_Consultar, parametros);
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
        public DataTable ConsultarFacturasNC(Paginacion<FacturaResultado> facturaResultado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NumeroFactura_Param, DbType.Int32, facturaResultado.Item.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_TipoFactura_Param, DbType.String, facturaResultado.Item.TipoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NombreTercero_Param, DbType.String, facturaResultado.Item.NombreTercero));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_NumeroPagina_Param, DbType.Int32, facturaResultado.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_CantidadRegistrosPagina_Param, DbType.Int32, facturaResultado.LongitudPagina));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_IndAnulacion_Param, DbType.Boolean, facturaResultado.Item.IndAnulacion));
            parametros.Add(this.CrearParametro(Parametros.FacturaResultado_FacEst_Param, DbType.String, facturaResultado.Item.EstadoFactura));

            return this.Consultar("FACSeleccionarFacturasNC", parametros);
        }

        /// <summary>
        /// Permite Consultar las facturas para reimprimir.
        /// </summary>
        /// <param name="paginacion">The reimprimir factura.</param>
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
        public DataTable ConsultarFacturasReimpresion(Paginacion<ReimprimirFactura> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_CodigoEntidad_Param, DbType.String, paginacion.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_NumeroFactura_Param, DbType.Int32, paginacion.Item.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_IdTercero_Param, DbType.Int32, paginacion.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_FechaDesde_Param, DbType.DateTime, paginacion.Item.FechaDesde));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_FechaHasta_Param, DbType.DateTime, paginacion.Item.FechaHasta));
            parametros.Add(this.CrearParametro(Parametros.Reimprimir_TipoFacturacion_Param, DbType.String, paginacion.Item.TipoFactura));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Reimprimir_ConsultarFacturasReimprimir, parametros);
        }

        /// <summary>
        /// Permite Consultar las atenciones pendientes por procesar.
        /// </summary>
        /// <param name="facturaAtencion">The proceso factura.</param>
        /// <param name="xml">Documento XML.</param>
        /// <returns>
        /// Lista de Datos de Atenciones Pendientes por Procesar.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar las atenciones pendientes por procesar.
        /// </remarks>
        public DataSet ConsultarGeneralFacturacion(FacturaAtencion facturaAtencion, XDocument xml)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("IdProceso", DbType.String, facturaAtencion.IdProceso));
            parametros.Add(this.CrearParametro("Seleccionados", DbType.Xml, xml.ToString()));
            DataSet dt = (DataSet)this.EjecutarProcedimientoTipo2DS("FACSeleccionarFacturacion", parametros);
            return dt;
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
        public DataTable ConsultarGrupoPaquetes()
        {
            return this.Consultar("FACConsultarGrupoPaquetes");
        }

        /// <summary>
        /// Consultars the grupo productox id grupo producto.
        /// </summary>
        /// <param name="grupoProducto">The grupo producto.</param>
        /// <returns>Retorna el DataTable.</returns>
        public DataTable ConsultarGrupoProductoxIdGrupoProducto(GrupoProducto grupoProducto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdGrupoProducto_Param, DbType.Int32, grupoProducto.IdGrupo));
            return this.Consultar(OperacionesBaseDatos.GrupoProducto_Consultar, parametros);
        }

        /// <summary>
        /// Permite Consultar los Producto Homologados.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Listado de Datos de Homologacion de Producto.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 28/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarHomologacionProducto(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.HomologacionProducto_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.HomologacionProducto_Consultar, parametros);
        }

        /// <summary>
        /// Permite consultar los honorarios medicos.
        /// </summary>
        /// <param name="honorario">The honorario.</param>
        /// <returns>Lista de Honorarios.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarHonorariosMedicos(Paginacion<Honorario> honorario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdHonorario_Param, DbType.Int32, honorario.Item.IdHonorario));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdTipoProducto_Param, DbType.Int32, honorario.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdGrupo_Param, DbType.Int32, honorario.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdProducto_Param, DbType.Int32, honorario.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IndHabilitado_Param, DbType.Int16, honorario.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdTercero_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreUsuario_Param, DbType.String, honorario.Item.NombreUsuario));
            parametros.Add(this.CrearParametro(Parametros.Honorario_ApellidoUsuario_Param, DbType.String, honorario.Item.ApellidoUsuario));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NumeroDocumento_Param, DbType.String, honorario.Item.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreTercero_Param, DbType.String, honorario.Item.NombreTercero));
            parametros.Add(this.CrearParametro(Parametros.Honorario_Componente_Param, DbType.String, honorario.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreTipoProducto_Param, DbType.String, honorario.Item.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreGrupoProducto_Param, DbType.String, honorario.Item.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreProducto_Param, DbType.String, honorario.Item.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_CodigoProducto_Param, DbType.String, honorario.Item.CodigoProducto));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreTipoAtencion_Param, DbType.String, honorario.Item.NombreTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Honorario_NombreClaseServicio_Param, DbType.String, honorario.Item.NombreClaseServicio));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, honorario.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, honorario.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.HonorariosMedicos_Consultar, parametros);
        }

        /// <summary>
        /// Metodo Para Consultar la Informacion Basica de Tercero.
        /// </summary>
        /// <returns>Informacion Basica Tercero.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarInformacionBasicaTercero()
        {
            return this.Consultar(OperacionesBaseDatos.InformacionBasicaTercero_Consultar);
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
        public DataTable ConsultarInformacionFactura(int identificadorProceso, int identificadorTipoMovimiento, short identificadorTipoFacturacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTipoMovimiento_Param, DbType.Int32, identificadorTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTipoFacturacion_Param, DbType.Int16, identificadorTipoFacturacion));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarInformacionFactura, parametros);
        }

        /// <summary>
        /// Consultars the informacion factura NC.
        /// </summary>
        /// <param name="identificadorProceso">The identificador proceso.</param>
        /// <param name="identificadorTipoMovimiento">The identificador tipo movimiento.</param>
        /// <param name="identificadorTipoFacturacion">The identificador tipo facturacion.</param>
        /// <returns>Retorna un DataTable.</returns>
        public DataTable ConsultarInformacionFacturaNC(int identificadorProceso, int identificadorTipoMovimiento, short identificadorTipoFacturacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTipoMovimiento_Param, DbType.Int32, identificadorTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTipoFacturacion_Param, DbType.Int16, identificadorTipoFacturacion));
            return this.Consultar("FACSeleccionarInformacionFacturaNC", parametros);
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
        public DataTable ConsultarListaAtenciones(List<int> numerosAtencion)
        {
            string atenciones = string.Join<int>(",", numerosAtencion);
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_Atencionciones_Param, DbType.String, atenciones));
            return this.Consultar(OperacionesBaseDatos.Atencion_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para consultar las atenciones que se encuentran en proceso.
        /// </summary>
        /// <param name="numerosAtencion">The numeros atencion.</param>
        /// <returns>Resultado de la Consulta.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarListaAtencionesProceso(List<int> numerosAtencion)
        {
            string atenciones = string.Join<int>(",", numerosAtencion);
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_Atencionciones_Param, DbType.String, atenciones));
            return this.Consultar(OperacionesBaseDatos.AtencionProceso_Consultar, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("Mes", DbType.Int32, mes));
            parametros.Add(this.CrearParametro("Año", DbType.Int32, año));
            parametros.Add(this.CrearParametro("Estado", DbType.String, estado));
            return this.Consultar("FACConsultarLogFacturacion", parametros);
        }

        /// <summary>
        /// Consulta Tablas  Maestras.
        /// </summary>
        /// <param name="identificadorMaestra">The id maestra.</param>
        /// <param name="identificadorPagina">The id pagina.</param>
        /// <returns>
        /// Retorna un datatable con la informacion solicitada.
        /// </returns>
        /// <remarks>
        /// Autor: Alex Mattos.
        /// FechaDeCreacion: (04/04/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción:Consulta Tablas Maestras.
        /// </remarks>
        public DataTable ConsultarMaestras(int identificadorMaestra, int identificadorPagina)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Maestras_IdMaestra_Parm, DbType.Int32, identificadorMaestra));
            parametros.Add(this.CrearParametro(Parametros.Maestras_IdPagina_Parm, DbType.Int32, identificadorPagina));
            return this.Consultar(Recursos.OperacionesBaseDatos.Maestras_Consultar, parametros);
        }

        /// <summary>
        /// Permite Consultar las Tarifas.
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
        public DataTable ConsultarManualesBasicos(Paginacion<ManualesBasicos> manuales)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ManualesBasicos_CodigoTarifa_Parm, DbType.Int32, manuales.Item.CodigoTarifa));
            parametros.Add(this.CrearParametro(Parametros.ManualesBasicos_NombreManualTarifa_Param, DbType.String, manuales.Item.NombreManualesTarifa));
            parametros.Add(this.CrearParametro(Parametros.ManualesBasicos_NombreTarifa_Parm, DbType.String, manuales.Item.NombreTarifa));

            if (manuales.Item.VigenciaTarifa.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.ManualesBasicos_VigenciaTarifa_Param, DbType.DateTime, manuales.Item.VigenciaTarifa));
            }

            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, manuales.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, manuales.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarManualesMaxVigencia, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("IdContrato", DbType.Int32, identificadorContrato));

            return this.Consultar("FACConsultarManualBasicoxContrato", parametros);
        }

        /// <summary>
        /// Metodo para consultar movimientos de Tesoreria Identificador de Proceso.
        /// </summary>
        /// <param name="identificadorProceso">The id proceso.</param>
        /// <returns>Movimientos de Tesoreria.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarMovimientosTesoreria(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarMovimientosTesoreria, parametros);
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
        public DataTable ConsultarMovimientosTesoreriaNC(int identificadorProceso)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdProceso_Param, DbType.Int32, identificadorProceso));
            return this.Consultar("FACSeleccionarAtencionMovimientosTesoreriaNC", parametros);
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
        public DataTable ConsultarMovimientoUsuario(MovimientoUsuario movimientoUsuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.MovimientoUsuario_IdTipoMovimiento_Param, DbType.Int32, movimientoUsuario.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.MovimientoUsuario_CodigoUsuario_Param, DbType.String, movimientoUsuario.CodigoUsuario));
            return this.Consultar(OperacionesBaseDatos.MovimientoUsuario_Consultar, parametros);
        }

        /// <summary>
        /// Método para evaluar el rol del usuario que consulta el movimiento de la atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <param name="codigoUsuario">The codigo usuario.</param>
        /// <returns>Objeto AtencionRolUsuario.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 22/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarMovimientoUsuarioAtencion(AtencionCliente atencion, string codigoUsuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdAtencion_Param, DbType.Int32, atencion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IndMovimiento_Param, DbType.Boolean, atencion.IndMovimiento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Usuario_Param, DbType.String, codigoUsuario));
            return this.Consultar(OperacionesBaseDatos.MovimientoUsuarioAtencion_Consultar, parametros);
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
        public DataTable ConsultarNivelComplejidadProducto(TipoProductoCompuesto producto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidadProducto_FecVenta, DbType.DateTime, producto.Fecha));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidadProducto_IdProducto, DbType.Int32, producto.Producto.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidadProducto_IdManual, DbType.Int32, producto.IdManual));

            return this.Consultar(OperacionesBaseDatos.VentasQX_ConsultarComplejidad, parametros);
        }

        /// <summary>
        /// Método para consultar los niveles de complejidad de un manual vigente.
        /// </summary>
        /// <param name="nivelComplejidad">The nivel complejidad.</param>
        /// <returns>Niveles de complejidad de la exclusión del manual.</returns>
        /// <remarks>
        /// Autor: Darío Fernando Preciado Barboza - INTERGRUPO\dpreciado.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarNivelesComplejidad(NivelComplejidad nivelComplejidad)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_OrdenNivel_Param, DbType.Int32, nivelComplejidad.OrdenNivel));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_VigenciaManual_Param, DbType.DateTime, nivelComplejidad.VigenciaManual));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_Producto_Param, DbType.Int32, nivelComplejidad.Producto));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_IndHabilitado_Param, DbType.Int16, nivelComplejidad.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.NivelComplejidad_Manual_Param, DbType.Int32, nivelComplejidad.IdManual));

            return this.Consultar(OperacionesBaseDatos.NivelesComplejidad_Consultar, parametros);
        }

        /// <summary>
        /// Método para consultar Paquetes.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <returns>
        /// Lista de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 17/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarPaquete(Paginacion<Paquete> paquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Paquete_Nombre_Param, DbType.String, paquete.Item.NombrePaquete));
            parametros.Add(this.CrearParametro(Parametros.Paquete_Codigo_Param, DbType.String, paquete.Item.CodigoPaquete));
            parametros.Add(this.CrearParametro(Parametros.Paquete_IndHabilitado_Param, DbType.Boolean, paquete.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Paquete_NumeroPagina_Param, DbType.Int32, paquete.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.Paquete_CantidadRegistrosPagina_Param, DbType.Int32, paquete.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Paquete_Consultar, parametros);
        }

        /// <summary>
        /// Método para consultar Paquetes.
        /// </summary>
        /// <param name="identificadorPaquete">The id paquete.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Lista de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 18/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarPaqueteDetallado(int identificadorPaquete, int identificadorAtencion, string ventas)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("IdAtencion", DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro("IdPaquete", DbType.Int32, identificadorPaquete));
            parametros.Add(this.CrearParametro("Ventas", DbType.Xml, ventas));
            return this.Consultar("FACArmarPaquete", parametros);
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
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar Paquete Factura.
        /// </remarks>
        public DataSet ConsultarPaqueteFactura(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            return this.EjecutarProcedimientoTipo2DS("FACConsultarPaqueteFactura", parametros);
        }

        /// <summary>
        /// Método para consultar Paquetes.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <returns>Lista de Paquetes.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 18/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarPaqueteProducto(Paginacion<PaqueteEncabezado> paquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_IdProducto_Param, DbType.Int32, paquete.Item.IdPaquete));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_NumeroPagina_Param, DbType.Int32, paquete.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_IndHabilitado_Param, DbType.Int16, paquete.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_CantidadRegistrosPagina_Param, DbType.Int32, paquete.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.PaqueteProducto_Consultar, parametros);
        }

        /// <summary>
        /// Método para consultar Paquetes.
        /// </summary>
        /// <param name="paquete">The paquete.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lista de Paquetes.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 18/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarPaqueteProductoDetallado(Paginacion<PaqueteProducto> paquete, int identificadorAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_IdProducto_Param, DbType.Int32, paquete.Item.IdPaquete));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_IndHabilitado_Param, DbType.Int16, paquete.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_NumeroPagina_Param, DbType.Int32, paquete.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_CantidadRegistrosPagina_Param, DbType.Int32, paquete.LongitudPagina));
            return this.Consultar("FACSeleccionarPaquetesProducto", parametros);
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
        public DataTable ConsultarPlanes(Contrato contrato)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTercero_Param, DbType.Int32, contrato.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdContrato_Param, DbType.Int32, contrato.Id));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarPlanesxContrato, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("IdContrato", DbType.Int32, identificadorContrato));
            parametros.Add(this.CrearParametro("IdManualAlterno", DbType.Int32, identificadorManualAlterno));
            parametros.Add(this.CrearParametro("IdManual", DbType.Int32, identificadorManual));

            return Convert.ToDecimal(this.Ejecutar("FACConsultarPorcentajeAlterno", parametros));
        }

        /// <summary>
        /// Método para consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Componentes de los productos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarProductoComponentes(VentaComponente detalle)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IdAtencion_Param, DbType.Int32, detalle.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IdContrato_Param, DbType.Int32, detalle.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IndHabilitado_Param, DbType.Int32, detalle.IndHabilitado));

            return this.Consultar(OperacionesBaseDatos.ProductoComponentes_Consultar, parametros);
        }

        /// <summary>
        /// Método para consultar los componentes de los productos.
        /// </summary>
        /// <param name="detalle">The detalle.</param>
        /// <returns>Componentes de los productos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarProductoComponentesReimpresion(VentaComponente detalle)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IdAtencion_Param, DbType.Int32, detalle.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IdContrato_Param, DbType.Int32, detalle.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.FacturaDetalleVenta_IndHabilitado_Param, DbType.Int32, detalle.IndHabilitado));

            return this.Consultar("FACSeleccionarComponentesProductoReimpresion", parametros);
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
        public DataTable ConsultarProductosCondicionCubrimientos(CondicionCubrimiento condicionCubrimiento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, condicionCubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdAtencion_Param, DbType.Int32, condicionCubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdContrato_Param, DbType.Int32, condicionCubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdPlan_Param, DbType.Int32, condicionCubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, condicionCubrimiento.IndHabilitado));

            return this.Consultar(OperacionesBaseDatos.CondicionCubrimientoProductos_Consultar, parametros);
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
        public DataTable ConsultarProductosCubrimientos(Cubrimiento cubrimiento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, cubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdAtencion_Param, DbType.Int32, cubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdContrato_Param, DbType.Int32, cubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdPlan_Param, DbType.Int32, cubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, cubrimiento.IndHabilitado));

            return this.Consultar(OperacionesBaseDatos.CubrimientosProductos_Consultar, parametros);
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
        public DataTable ConsultarProductosNC(FacturaAtencion facturaAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaAtencion_IdProceso_Param, DbType.String, facturaAtencion.IdProceso));
            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarProductosNC, parametros);
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
        public DataTable ConsultarProductosVenta(ProductoVenta productoVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProductoVenta_IdAtencion_Param, DbType.Int32, productoVenta.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.ProductosVenta_Consultar, parametros);
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
        public DataTable ConsultarProductosVentaPaquete(int identificadorAtencion, int identificadorPaquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProductoVenta_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro("IdPaquete", DbType.Int32, identificadorPaquete));
            return this.Consultar("FACSeleccionarProductosVentaPaquete", parametros);
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
        public DataTable ConsultarProductoxIdProducto(Producto producto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.PaqueteProducto_IdProducto_Param, DbType.Int32, producto.IdProducto));
            return this.Consultar(OperacionesBaseDatos.Productos_Seleccionar_IdProducto, parametros);
        }

        /// <summary>
        /// Metodo de Consultar recargos.
        /// </summary>
        /// <param name="recargos">The recargos.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Diana Cardenas Sanchez- INTERGRUPO\dcardenas.
        /// FechaDeCreacion: 03/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarRecargos(Paginacion<Recargo> recargos)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdRecargo_Param, DbType.Int32, recargos.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdTercero_Param, DbType.Int32, recargos.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Recargos_CodigoEntidad_Param, DbType.String, recargos.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdContrato_Param, DbType.Int32, recargos.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdPlan_Param, DbType.Int32, recargos.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdTipoProducto_Param, DbType.Int16, recargos.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_DescripcionTipoProducto_Param, DbType.String, recargos.Item.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdGrupoProducto_Param, DbType.Int16, recargos.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_DescripcionGrupoProducto_Param, DbType.String, recargos.Item.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_IdProducto_Param, DbType.Int32, recargos.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_DescripcionProducto_Param, DbType.String, recargos.Item.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargos_Componente_Param, DbType.String, recargos.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.Recargos_TipoRecargo_Param, DbType.Int16, recargos.Item.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.General_IndHabilitado_Param, DbType.Boolean, recargos.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, recargos.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, recargos.LongitudPagina));

            parametros.Add(this.CrearParametro("NombrePlan", DbType.String, recargos.Item.NombrePlan));
            parametros.Add(this.CrearParametro("NombreServicio", DbType.String, recargos.Item.NombreServicio));
            parametros.Add(this.CrearParametro("NombreTipoAtencion", DbType.String, recargos.Item.NomTipoAtecion));
            parametros.Add(this.CrearParametro("NombreTipoRecargo", DbType.String, recargos.Item.TipoRecargo));

            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarRecargosxContrato, parametros);
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el contrato.
        /// </summary>
        /// <param name="recargo">The recargo.</param>
        /// <returns>Lista de Datos Recargos Contrato.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Permite Consultar los recargos aplicados en el contrato a un producto.
        /// </remarks>
        public DataTable ConsultarRecargosContrato(Recargo recargo)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Recargo_CodigoEntidad_Param, DbType.String, recargo.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdentificadorContrato_Param, DbType.Int32, recargo.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdentificadorTercero_Param, DbType.Int32, recargo.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Recargo_EstadoRecargo_Param, DbType.Int32, recargo.IndicadorActivo));

            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarRecargosContrato, parametros);
        }

        /// <summary>
        /// Permite Consultar los recargos aplicados en el manual.
        /// </summary>
        /// <param name="recargoManual">The recargo manual.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 26/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarRecargosManual(RecargoManual recargoManual)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Recargo_CodigoEntidad_Param, DbType.String, recargoManual.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdentificadorContrato_Param, DbType.Int32, recargoManual.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdentificadorTercero_Param, DbType.Int32, recargoManual.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Recargo_EstadoRecargo_Param, DbType.Int32, recargoManual.IndicadorActivo));

            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarRecargosManual, parametros);
        }

        /// <summary>
        /// Permite Consultar llos Descuentos.
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
        public DataTable ConsultarTarifas(Paginacion<CondicionTarifa> tarifas)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdCondicionTarifa_Param, DbType.Int32, tarifas.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTercero_Param, DbType.Int32, tarifas.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_CodigoEntidad_Param, DbType.String, tarifas.Item.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdContrato_Param, DbType.Int32, tarifas.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdPlan_Param, DbType.Int32, tarifas.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencion_Param, DbType.Int32, tarifas.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoProducto_Param, DbType.Int16, tarifas.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_DescripcionTipoProducto_Param, DbType.String, tarifas.Item.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdGrupoProducto_Param, DbType.Int16, tarifas.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_DescripcionGrupoProducto_Param, DbType.String, tarifas.Item.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdProducto_Param, DbType.Int32, tarifas.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_DescripcionProducto_Param, DbType.String, tarifas.Item.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Componente_Param, DbType.String, string.IsNullOrEmpty(tarifas.Item.Componente) ? string.Empty : tarifas.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_TipoRelacion_Param, DbType.Int16, tarifas.Item.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndHabilitado_Param, DbType.Int16, tarifas.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Tipo_Param, DbType.String, tarifas.Item.Tipo));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdPagina_Param, DbType.Int32, tarifas.Item.Maestras.IdPagina));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, tarifas.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, tarifas.LongitudPagina));
            parametros.Add(this.CrearParametro("NombrePlan", DbType.String, string.IsNullOrEmpty(tarifas.Item.NombrePlan) ? string.Empty : tarifas.Item.NombrePlan));
            parametros.Add(this.CrearParametro("NombreServicio", DbType.String, string.IsNullOrEmpty(tarifas.Item.NombreServicio) ? string.Empty : tarifas.Item.NombreServicio));
            parametros.Add(this.CrearParametro("NombreTipoAtencion", DbType.String, string.IsNullOrEmpty(tarifas.Item.NomTipoAtecion) ? string.Empty : tarifas.Item.NomTipoAtecion));

            return this.Consultar(OperacionesBaseDatos.Facturacion_SeleccionarTarifas, parametros);
        }

        /// <summary>
        /// Método para consultar la unidades de Factura.
        /// </summary>
        /// <param name="tarifaUnidad">The factura unidad.</param>
        /// <returns>Lista de Unidades.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 20/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTarifaUnidad(TarifaUnidad tarifaUnidad)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.TarifaUnidad_IdManual_Entidad, DbType.Int32, tarifaUnidad.IdManual));
            return this.Consultar(OperacionesBaseDatos.TarifaUnidad_Consultar, parametros);
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
        public DataTable ConsultarTasa()
        {
            return this.Consultar(OperacionesBaseDatos.Tasa_Consultar);
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
        public DataTable ConsultarTercero(int identificadorTercero)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTercero_Param, DbType.Int32, identificadorTercero));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarTercero, parametros);
        }

        /// <summary>
        /// Consultar los terceros responsables de la venta de un componente.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Datatable con lista de terceros asociados a la venta de componentes.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 26/12/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTerceroComponente(int identificadorAtencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            return this.Consultar(OperacionesBaseDatos.TerceroComponente_Seleccionar, parametros);
        }

        /// <summary>
        /// Método para consultar los tipos de empresa.
        /// </summary>
        /// <param name="tipoEmpresa">The tipo empresa.</param>
        /// <returns>Lista de Tipos de empresa.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTipoEmpresa(TipoEmpresa tipoEmpresa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.TipoEmpresa_IdTipoEmpresa_Param, DbType.Int32, tipoEmpresa.IdTipoEmpresa));
            return this.Consultar(OperacionesBaseDatos.TipoEmpresa_Consultar, parametros);
        }

        /// <summary>
        /// Consultars the tipo factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna un string.</returns>
        public string ConsultarTipoFactura(int numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            return Convert.ToString(this.Ejecutar("FACConsultarTipoFactura", parametros));
        }

        /// <summary>
        /// Método para consultar tipo Producto.
        /// </summary>
        /// <param name="tipoProducto">The tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        /// Lista de tipo Producto.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTipoProducto(TipoProducto tipoProducto, string usuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.TipoProducto_Ubicacion_Param, DbType.String, tipoProducto.IdUbicacion));
            parametros.Add(this.CrearParametro(Parametros.Transaccion_Usuario_Param, DbType.String, usuario));
            return this.Consultar(OperacionesBaseDatos.TipoProducto_Consultar, parametros);
        }

        /// <summary>
        /// Consulta las Transacciones.
        /// </summary>
        /// <param name="transaccion">The transaccion.</param>
        /// <returns>Lista de Transacciones.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 05/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTransaccion(Transaccion transaccion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Transaccion_IdTransaccion_Param, DbType.String, transaccion.NombreTransaccion));
            parametros.Add(this.CrearParametro(Parametros.Transaccion_Usuario_Param, DbType.String, transaccion.Usuario));
            return this.Consultar(OperacionesBaseDatos.Transaccion_Consultar, parametros);
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
        public DataTable ConsultarTransaccionesVentasQx(string usuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Transaccion_Usuario_Param, DbType.String, usuario));
            return this.Consultar("FACSeleccionarTransaccionVentaQx", parametros);
        }

        /// <summary>
        /// Método para consultar Ubicación.
        /// </summary>
        /// <param name="ubicacion">The ubicacion.</param>
        /// <returns>Lista de Ubicaciones por Usuario.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarUbicacion(Ubicacion ubicacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Ubicacion_Usuario_Param, DbType.String, ubicacion.Usuario));
            parametros.Add(this.CrearParametro(Parametros.Ubicacion_Transaccion_Param, DbType.String, ubicacion.Nombre));
            return this.Consultar(OperacionesBaseDatos.Ubicacion_Usuario_Consultar, parametros);
        }

        /// <summary>
        /// Método para consultar Ubicación.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 26/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarUbicacionConsumo(int identificadorAtencion, int identificadorTipoProducto, string usuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.AtencionCliente_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoProducto_Param, DbType.Int32, identificadorTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Usuario_Param, DbType.String, usuario));
            return this.Consultar(OperacionesBaseDatos.UbicacionConsumo_Consultar, parametros);
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
        public DataTable ConsultarUbicacionPorNombre(Ubicacion ubicacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NombreUbicacion", DbType.String, ubicacion.Nombre));
            return this.Consultar("FACSeleccionarUbicacionxNombre", parametros);
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
        public DataTable ConsultarValorEncabezadoPaquetes(Paquete facturaPaquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Paquete_IdPaquete_Param, DbType.Int32, facturaPaquete.IdPaquete));
            parametros.Add(this.CrearParametro(Parametros.FacturaPaquete_IdManual_Param, DbType.Int32, facturaPaquete.IdManual));
            parametros.Add(this.CrearParametro(Parametros.General_IndHabilitado_Param, DbType.Int16, facturaPaquete.IndHabilitado ? 1 : 0));
            return this.Consultar(OperacionesBaseDatos.EncabezadoValoresPaquete_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para obtener el valor del paquete encabezado.
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
        public DataTable ConsultarValorPaquetes(FacturaPaquete facturaPaquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaPaquete_IdManual_Param, DbType.Int32, facturaPaquete.IdManual));
            parametros.Add(this.CrearParametro(Parametros.FacturaPaquete_VigenciaTarifa_Param, DbType.DateTime, facturaPaquete.VigenciaTarifa));
            return this.Consultar(OperacionesBaseDatos.ValorEncabezadoPaquete_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para consultar el detalle de la venta x Identificador de la venta.
        /// </summary>
        /// <param name="detalleVenta">The detalle venta.</param>
        /// <returns>Lista de Paquetes.</returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 16/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarVentaDetalles(VentaDetalle detalleVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_NumeroVenta_Param, DbType.Int32, detalleVenta.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdVenta_Param, DbType.String, detalleVenta.NombreTransaccion));
            return this.Consultar(OperacionesBaseDatos.DetalleVenta_Consultar, parametros);
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
        public DataTable ConsultarVentaDetallesxIdTx(VentaDetalle detalleVenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroVenta", DbType.Int32, detalleVenta.NumeroVenta));
            parametros.Add(this.CrearParametro("IdTransaccion", DbType.Int32, detalleVenta.IdTransaccion));
            return this.Consultar("FACSeleccionarVentaDetallexIdTx", parametros);
        }

        /// <summary>
        /// Método para consultar la Ventas No Clinicas.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Ventas.
        /// </returns>
        /// <remarks>
        /// Autor: Dario Fernando Preciado Barboza - INTERGRUPO\Dpreciado.
        /// FechaDeCreacion: 04/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarVentaNoClinica(Paginacion<VentaNoClinica> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaNoClinica_NumeroVenta_Param, DbType.Int32, paginacion.Item.Numero));
            parametros.Add(this.CrearParametro(Parametros.VentaNoClinica_Identificacion_Param, DbType.String, paginacion.Item.Identificacion));
            parametros.Add(this.CrearParametro(Parametros.VentaNoClinica_Cliente_Param, DbType.String, paginacion.Item.NombreTercero));
            parametros.Add(this.CrearParametro(Parametros.VentaNoClinica_EstadoVenta_Param, DbType.String, paginacion.Item.EstadoVenta));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            parametros.Add(this.CrearParametro(Parametros.VentaNoClinica_Transaccion_Param, DbType.String, paginacion.Item.Transaccion));
            return this.Consultar(OperacionesBaseDatos.VentaNoClinica_Consultar, parametros);
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
        public DataTable ConsultarVentaProductosRelacion(Paginacion<VentaProductoRelacion> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_NumeroVenta_Param, DbType.Int32, paginacion.Item.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.VentaProductoRelacion_Consultar, parametros);
        }

        /// <summary>
        /// Metodo para Consultar Productos de la Venta.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Detalle de Ventas Asociadas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarVentaProductosTransaccion(Paginacion<VentaProducto> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaProducto_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.VentaProducto_IndHabilitado_Param, DbType.Boolean, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.VentaProducto_NumeroVenta_Param, DbType.String, paginacion.Item.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaProducto_NumerosVenta_Para, DbType.String, paginacion.Item.NumerosVenta));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.VentaProducto_Consultar, parametros);
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
        public DataTable ConsultarVentasAtencion(Paginacion<Venta> venta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, venta.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, venta.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, venta.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarVenta, parametros);
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
        public DataTable ConsultarVentasNumeroVenta(Paginacion<Venta> venta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdAtencion_Param, DbType.Int32, venta.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Facturacion_NumeroVenta_Param, DbType.Int32, venta.Item.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, venta.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, venta.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarVentaNumeroVenta, parametros);
        }

        /// <summary>
        /// Metodo para Consultar Ventas Qx.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Ventas Asociadas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 02/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarVentasTransaccion(Paginacion<VentaTransaccion> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaTransaccion_IdAtencion_Param, DbType.Int32, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.VentaTransaccion_IndHabilitado_Param, DbType.Boolean, paginacion.Item.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.VentaTransaccion_Transacciones_Param, DbType.String, paginacion.Item.Transacciones));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            return this.Consultar(OperacionesBaseDatos.VentaTransaccion_Consultar, parametros);
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
        public DataTable ConsultarVinculaciones(Vinculacion atencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdAtencion_Param, DbType.Int32, atencion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IndHabilitado_Param, DbType.Int16, atencion.IndHabilitado));
            return this.Consultar(OperacionesBaseDatos.Facturacion_ConsultarVinculaciones, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));

            return this.Actualizar("FACEliminarNoFacturables", parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("IdProceso", DbType.Int32, identificadorProceso));
            return Convert.ToInt32(this.Ejecutar("FACEliminarProcesoActualNC", parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("CodigoEntidad", DbType.String, codigoEntidad));
            parametros.Add(this.CrearParametro("IdTarifa", DbType.Int32, identificadorTarifa));
            this.Ejecutar("FACEliminarProductosTarifa", parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_Consecutivo_Param, DbType.String, notaCredito.Consecutivo));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_PrefijoNotaCredito_Param, DbType.String, notaCredito.PrefijoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdMovimientoDocumento_Param, DbType.Int32, notaCredito.IdMovimientoDocumento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_DocumentoResultado_Param, DbType.String, notaCredito.DocumentoResultado));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IndReenvio_Param, DbType.Int16, notaCredito.IndReenvio));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_FechaNota_Param, DbType.DateTime, notaCredito.FechaNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoUsuario_Param, DbType.String, notaCredito.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_TipoNotaCredito_Param, DbType.String, notaCredito.TipoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_MotivoNota_Param, DbType.String, notaCredito.MotivoNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_ValorAjuste_Param, DbType.Decimal, notaCredito.ValorAjuste));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdGlosa_Param, DbType.Int32, notaCredito.IdGlosa));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCuenta_Param, DbType.Int32, notaCredito.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCastigoCausal_Param, DbType.Int32, notaCredito.IdCastigoCausal));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNotaCredito_Param, DbType.Int32, notaCredito.IdNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdSedeNota_Param, DbType.Int32, notaCredito.IdSedeNota));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.AnulacionFactura_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_Consecutivo_Param, DbType.String, notaCredito.Consecutivo));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_PrefijoNotaCredito_Param, DbType.String, notaCredito.PrefijoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdMovimientoDocumento_Param, DbType.Int32, notaCredito.IdMovimientoDocumento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_DocumentoResultado_Param, DbType.String, notaCredito.DocumentoResultado));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IndReenvio_Param, DbType.Int16, notaCredito.IndReenvio));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_FechaNota_Param, DbType.DateTime, notaCredito.FechaNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoUsuario_Param, DbType.String, notaCredito.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_TipoNotaCredito_Param, DbType.String, notaCredito.TipoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_MotivoNota_Param, DbType.String, notaCredito.MotivoNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_ValorAjuste_Param, DbType.Decimal, notaCredito.ValorAjuste));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdGlosa_Param, DbType.Int32, notaCredito.IdGlosa));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCuenta_Param, DbType.Int32, notaCredito.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCastigoCausal_Param, DbType.Int32, notaCredito.IdCastigoCausal));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNotaCredito_Param, DbType.Int32, notaCredito.IdNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdSedeNota_Param, DbType.Int32, notaCredito.IdSedeNota));

            DataTable tabla = this.Consultar("FACInsertarNotaCreditoNC", parametros);

            return tabla;
        }

        /// <summary>
        /// Metodo para guardar el Cliente.
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <returns>Id del Cliente Creado.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdCliente_Param, DbType.Int32, cliente.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Nombres_Param, DbType.String, cliente.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Apellidos_Param, DbType.String, cliente.Apellido));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdTipoDocumento_Param, DbType.Byte, cliente.IdTipoDocumento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_NumeroDocumento_Param, DbType.String, cliente.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdSexo_Param, DbType.Int16, cliente.IdSexo));
            parametros.Add(this.CrearParametro(Parametros.Cliente_FechaNacimiento_Param, DbType.DateTime, cliente.FechaNacimiento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdLugarNacimiento_Param, DbType.Int32, cliente.IdLugarNacimiento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdEstadoCivil_Param, DbType.Int16, cliente.IdEstadoCivil));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdLugarCliente_Param, DbType.Int32, cliente.IdLugarCliente));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdZona_Param, DbType.Int16, cliente.IdZona));
            parametros.Add(this.CrearParametro(Parametros.Cliente_DireccionCasa_Param, DbType.String, cliente.DireccionCasa));
            parametros.Add(this.CrearParametro(Parametros.Cliente_DireccionOficina_Param, DbType.String, cliente.DireccionOficina));
            parametros.Add(this.CrearParametro(Parametros.Cliente_TelefonoCasa_Param, DbType.String, cliente.TelefonoCasa));
            parametros.Add(this.CrearParametro(Parametros.Cliente_TelefonoOficina_Param, DbType.String, cliente.TelefonoOficina));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdOcupacion_Param, DbType.Int16, cliente.IdOcupacion));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdAfiliaciontipo_Param, DbType.Int16, cliente.IdAfiliaciontipo));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdNivel_Param, DbType.Byte, cliente.IdNivel));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdReligion_Param, DbType.Int32, cliente.IdReligion));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IndHabilitado_Param, DbType.Boolean, cliente.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdLugarDocumento_Param, DbType.Int32, cliente.IdLugarDocumento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Cod_DipoNac_Param, DbType.String, cliente.CodigoCiudadNacimiento));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Cod_dipoCliente_Param, DbType.String, cliente.CodigoCiudadCliente));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Usuario_Param, DbType.String, cliente.Usuario));
            parametros.Add(this.CrearParametro(Parametros.Cliente_CiudadAfiliacion_Param, DbType.String, cliente.CiudadAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdRegimenAfiliacion_Param, DbType.Int32, cliente.IdRegimenAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdTipoCliente_Param, DbType.Int16, cliente.IdTipoCliente));
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdSede_Param, DbType.Int32, cliente.IdSede));
            parametros.Add(this.CrearParametro(Parametros.Cliente_Email_Param, DbType.String, cliente.CorreoElectronico));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Cliente_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_CodigoEntidad_Param, DbType.String, conceptoCobro.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_IdAtencion_Param, DbType.Int32, conceptoCobro.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_IdContrato_Param, DbType.Int32, conceptoCobro.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_IdPlan_Param, DbType.Int32, conceptoCobro.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_FechaConcepto_Param, DbType.DateTime, conceptoCobro.FechaConcepto));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_CodigoConcepto_Param, DbType.String, conceptoCobro.CodigoConcepto));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_ValorConcepto_Param, DbType.Decimal, conceptoCobro.ValorConcepto));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_Porcentaje_Param, DbType.Decimal, conceptoCobro.Porcentaje));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_ValorContrato_Param, DbType.Decimal, conceptoCobro.ValorContrato));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_IndHabilitado_Param, DbType.Int16, conceptoCobro.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_ValorSaldo_Param, DbType.Decimal, conceptoCobro.ValorSaldo));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_ValorMaximo_Param, DbType.Decimal, conceptoCobro.ValorMaximo));
            parametros.Add(this.CrearParametro(Parametros.ConceptoCobro_NumeroFactura_Param, DbType.Int32, conceptoCobro.NumeroFactura));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ConceptoCobro_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodigoEntidad_Param, DbType.String, condicionCubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoRelacion_Param, DbType.Int16, condicionCubrimiento.NumeroTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTercero_Param, DbType.Int32, condicionCubrimiento.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdContrato_Param, DbType.Int32, condicionCubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdPlan_Param, DbType.Int32, condicionCubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdAtencion_Param, DbType.Int32, condicionCubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodAteConIde_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodAteTecIde_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_ValorPropio_Param, DbType.Decimal, condicionCubrimiento.ValorPropio));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_VigenciaCondicion_Param, DbType.DateTime, condicionCubrimiento.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_DescripcionCondicion_Param, DbType.String, condicionCubrimiento.DescripcionCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TipoCondicion_Param, DbType.String, condicionCubrimiento.Tipo));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IndHabilitado_Param, DbType.Int16, condicionCubrimiento.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodTisIde_Param, DbType.Int32, condicionCubrimiento.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodSerIde_Param, DbType.Int32, condicionCubrimiento.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdTipoProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdGrupoProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdProducto_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TicCod_Param, DbType.String, condicionCubrimiento.Componente));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TarManCod_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_TarVig_Param, DbType.DateTime, condicionCubrimiento.VigenciaTarifa));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodIndFac_Param, DbType.Int16, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodFec_Param, DbType.DateTime, condicionCubrimiento.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodUsuCod_Param, DbType.String, string.Empty));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_Porcentaje_Param, DbType.Decimal, condicionCubrimiento.ValorPorcentaje));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_IdClaseCubrimiento_Param, DbType.Int32, condicionCubrimiento.Cubrimiento.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_AltTarManCod_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_AltTarVig_Param, DbType.DateTime, DateTime.Now));
            parametros.Add(this.CrearParametro(Parametros.CondicionCubrimiento_CodIndUnd_Param, DbType.Int16, 0));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.CondicionCubrimiento_Insertar, parametros));
        }

        /// <summary>
        /// Método para almacenar la información de las Tarifas.
        /// </summary>
        /// <param name="condicionTarifa">The tarifas.</param>
        /// <returns>
        /// Indica si se guardo el registro.
        /// </returns>
        /// <remarks>
        /// Autor: Alex Mattos - INTERGRUPO\amattos.
        /// FechaDeCreacion: 09/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarCondicionTarifa(CondicionTarifa condicionTarifa)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_CodigoEntidad_Param, DbType.String, condicionTarifa.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdCondicionTarifa_Param, DbType.Int32, condicionTarifa.Id));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoRelacion_Param, DbType.Int16, condicionTarifa.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTercero_Param, DbType.Int32, condicionTarifa.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdPlan_Param, DbType.Int32, condicionTarifa.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencion_Param, DbType.Int32, condicionTarifa.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencionContrato_Param, DbType.Int32, condicionTarifa.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdAtencionPlan_Param, DbType.Int32, condicionTarifa.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_ValorPropio_Param, DbType.Decimal, condicionTarifa.ValorPropio));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaCondicion_Param, DbType.DateTime, condicionTarifa.VigenciaCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Descripcion_Param, DbType.String, condicionTarifa.DescripcionCondicion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Tipo_Param, DbType.String, condicionTarifa.Tipo));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndHabilitado_Param, DbType.Int16, condicionTarifa.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoAtencion_Param, DbType.Int32, condicionTarifa.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdServicio_Param, DbType.Int32, condicionTarifa.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdTipoProducto_Param, DbType.Int32, condicionTarifa.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdGrupoProducto_Param, DbType.Int16, condicionTarifa.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdProducto_Param, DbType.Int32, condicionTarifa.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Componente_Param, DbType.String, condicionTarifa.Componente));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdManual_Param, DbType.Int32, condicionTarifa.IdManual));

            if (condicionTarifa.VigenciaTarifa.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaManual_Param, DbType.DateTime, condicionTarifa.VigenciaTarifa));
            }

            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndUnidad_Param, DbType.Int16, condicionTarifa.SoloUnidad));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IndFacturacion_Param, DbType.Int16, 1));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Usuario_Param, DbType.String, condicionTarifa.Usuario));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdManualAlt_Param, DbType.Int32, condicionTarifa.IdManualAlterno));

            if (condicionTarifa.VigenciaTarifaAlterna.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_VigenciaManualAlt_Param, DbType.DateTime, condicionTarifa.VigenciaTarifaAlterna));
            }

            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_Porcentaje_Param, DbType.Decimal, condicionTarifa.ValorPorcentaje));
            parametros.Add(this.CrearParametro(Parametros.CondicionesTarifa_IdCubrimiento_Param, DbType.Int32, 0));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.CondicionesTarifa_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_CodigoEntidad_Param, DbType.String, convenioNoClinico.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdTercero_Param, DbType.Int32, convenioNoClinico.Tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_NombreAproximacion_Param, DbType.String, convenioNoClinico.NombreAproximacion));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_PorcentajeTarifa_Param, DbType.Decimal, convenioNoClinico.Porcentaje));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_DigitoVerificacion_Param, DbType.Int16, convenioNoClinico.NumeroDigitos));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IndHabilitado_Param, DbType.Int16, convenioNoClinico.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdConceptoCartera_Param, DbType.Int32, convenioNoClinico.IdConceptoCartera));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_VigenciaTarifa_Param, DbType.DateTime, convenioNoClinico.VigenciaManual));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdManual_Param, DbType.Int32, convenioNoClinico.IdManual));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IdTipoEmpresa_Param, DbType.Int32, convenioNoClinico.IdTipoEmpresa));
            parametros.Add(this.CrearParametro(Parametros.ConvenioNoClinico_IndRenovacion_Param, DbType.Boolean, convenioNoClinico.RenovacionAutomatica));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ConvenioNoClinico_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoEntidad_Param, DbType.String, cubrimiento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdContrato_Param, DbType.Int32, cubrimiento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdPlan_Param, DbType.Int32, cubrimiento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdAtencion_Param, DbType.Int32, cubrimiento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdTipoProducto_Param, DbType.Int32, cubrimiento.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdGrupoProducto_Param, DbType.Int32, cubrimiento.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdProducto_Param, DbType.Int32, cubrimiento.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoComponente_Param, DbType.String, cubrimiento.Componente));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IdClaseCubrimiento_Param, DbType.Int32, cubrimiento.IdClaseCubrimiento));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_IndHabilitado_Param, DbType.Int16, cubrimiento.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Cubrimiento_CodigoUsuario_param, DbType.String, cubrimiento.CodigoUsuario));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Cubrimientos_Insertar, parametros));
        }

        /// <summary>
        /// Método para almacenar la información de la cuenta cartera.
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoEntidad_Param, DbType.String, cuentaCartera.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoSeccion_Param, DbType.String, cuentaCartera.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCuenta_Param, DbType.Int32, cuentaCartera.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoMovimiento_Param, DbType.Int32, cuentaCartera.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTercero_Param, DbType.Int32, cuentaCartera.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdContrato_Param, DbType.Int32, cuentaCartera.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdPlan_Param, DbType.Int32, cuentaCartera.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoRegimen_Param, DbType.Int32, cuentaCartera.IdTipoRegimen));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCliente_Param, DbType.Int32, cuentaCartera.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_NumeroResultadoDocumento_Param, DbType.Int32, cuentaCartera.NumeroResultadoDocumento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_ValorMonto_Param, DbType.Decimal, Math.Truncate(cuentaCartera.ValorMonto)));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_ValorSaldo_Param, DbType.Decimal, Math.Truncate(cuentaCartera.ValorSaldo)));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_FechaRegistroCuenta_Param, DbType.DateTime, cuentaCartera.FechaRegistroCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_FechaRadicacion_Param, DbType.DateTime, cuentaCartera.FechaRadicacion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdConcepto_Param, DbType.Int32, cuentaCartera.IdConcepto));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoCartera_Param, DbType.Int32, cuentaCartera.IdTipoCartera));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_DocumentoPrefijo_Param, DbType.String, cuentaCartera.DocumentoPrefijo));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndRelacionRadicado_Param, DbType.Int16, cuentaCartera.IndRelacionRadicado));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoUsuario_Param, DbType.String, cuentaCartera.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndHabilitado_Param, DbType.Int16, cuentaCartera.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndRadicacion_Param, DbType.Int16, cuentaCartera.IndRadicacion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoMovimiento_Param, DbType.String, cuentaCartera.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_EstadoCuenta_Param, DbType.String, cuentaCartera.EstadoCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndDetalleFactura_Param, DbType.Int16, cuentaCartera.IndDetalleFactura));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_HoraRegistroCuenta_Param, DbType.DateTime, cuentaCartera.HoraRegistroCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CuentaInicio_Param, DbType.Int32, cuentaCartera.CuentaInicio));

            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdClienteResponsable_Param, DbType.Int32, cuentaCartera.IdClienteResponsable));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTerceroResponsable_Param, DbType.Int32, cuentaCartera.IdTerceroResponsable));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_Observaciones_Param, DbType.String, cuentaCartera.Observaciones));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdAtencion_Param, DbType.Int32, cuentaCartera.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdSede_Param, DbType.Int32, cuentaCartera.IdSede));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.CuentaCartera_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoEntidad_Param, DbType.String, cuentaCartera.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoSeccion_Param, DbType.String, cuentaCartera.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCuenta_Param, DbType.Int32, cuentaCartera.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoMovimiento_Param, DbType.Int32, cuentaCartera.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTercero_Param, DbType.Int32, cuentaCartera.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdContrato_Param, DbType.Int32, cuentaCartera.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdPlan_Param, DbType.Int32, cuentaCartera.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoRegimen_Param, DbType.Int32, cuentaCartera.IdTipoRegimen));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdCliente_Param, DbType.Int32, cuentaCartera.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_NumeroResultadoDocumento_Param, DbType.Int32, cuentaCartera.NumeroResultadoDocumento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_ValorMonto_Param, DbType.Decimal, Math.Truncate(cuentaCartera.ValorMonto)));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_ValorSaldo_Param, DbType.Decimal, Math.Truncate(cuentaCartera.ValorSaldo)));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_FechaRegistroCuenta_Param, DbType.DateTime, cuentaCartera.FechaRegistroCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_FechaRadicacion_Param, DbType.DateTime, cuentaCartera.FechaRadicacion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdConcepto_Param, DbType.Int32, cuentaCartera.IdConcepto));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTipoCartera_Param, DbType.Int32, cuentaCartera.IdTipoCartera));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_DocumentoPrefijo_Param, DbType.String, cuentaCartera.DocumentoPrefijo));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndRelacionRadicado_Param, DbType.Int16, cuentaCartera.IndRelacionRadicado));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoUsuario_Param, DbType.String, cuentaCartera.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndHabilitado_Param, DbType.Int16, cuentaCartera.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndRadicacion_Param, DbType.Int16, cuentaCartera.IndRadicacion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CodigoMovimiento_Param, DbType.String, cuentaCartera.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_EstadoCuenta_Param, DbType.String, cuentaCartera.EstadoCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IndDetalleFactura_Param, DbType.Int16, cuentaCartera.IndDetalleFactura));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_HoraRegistroCuenta_Param, DbType.DateTime, cuentaCartera.HoraRegistroCuenta));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_CuentaInicio_Param, DbType.Int32, cuentaCartera.CuentaInicio));

            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdClienteResponsable_Param, DbType.Int32, cuentaCartera.IdClienteResponsable));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdTerceroResponsable_Param, DbType.Int32, cuentaCartera.IdTerceroResponsable));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_Observaciones_Param, DbType.String, cuentaCartera.Observaciones));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdAtencion_Param, DbType.Int32, cuentaCartera.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.CuentaCartera_IdSede_Param, DbType.Int32, cuentaCartera.IdSede));
            return Convert.ToInt32(this.Ejecutar("FACInsertarCuentaCarteraNC", parametros));
        }

        /// <summary>
        /// Método para almacenar la información de los descuentos.
        /// </summary>
        /// <param name="descuento">The descuentos.</param>
        /// <returns>
        /// Indica si se guardo el registro.
        /// </returns>
        /// <remarks>
        /// Autor: Alex Mattos - INTERGRUPO\amattos.
        /// FechaDeCreacion: 09/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarDescuento(DescuentoConfiguracion descuento)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoEntidad_Param, DbType.String, descuento.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdDescuento_Param, DbType.Int32, descuento.Id));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTercero_Parm, DbType.Int32, descuento.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContrato_Param, DbType.Int32, descuento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlan_Param, DbType.Int32, descuento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoAtencion_Param, DbType.Int32, descuento.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdServicio_Parm, DbType.Int32, descuento.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProductoTipo_Param, DbType.Int16, descuento.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdGrupoProducto_Param, DbType.Int16, descuento.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProducto_Param, DbType.Int32, descuento.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdAtencion_Param, DbType.Int32, descuento.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContratoA_Parm, DbType.Int32, descuento.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlanAtencion_Parm, DbType.Int32, descuento.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_FechaIni_Param, DbType.DateTime, descuento.FechaInicial));

            if (descuento.FechaFinal.Year > 1)
            {
                parametros.Add(this.CrearParametro(Parametros.Descuentos_FechaFin_Param, DbType.DateTime, descuento.FechaFinal));
            }

            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoRelacion_Param, DbType.Int16, descuento.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_ValosDescuento_Parm, DbType.Decimal, descuento.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IndHabilitado_Param, DbType.Int16, descuento.IndicadorActivo));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoComponente_Param, DbType.String, descuento.Componente));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_DecIndFac_Parm, DbType.Int16, (short)0));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Descuentos_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_CodigoEntidad_Param, DbType.String, detalleNotaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_Consecutivo_Param, DbType.String, detalleNotaCredito.Consecutivo));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_IdTipoMovimiento_Param, DbType.Int32, detalleNotaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_CodigoMovimiento_Param, DbType.String, detalleNotaCredito.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_IdNumeroNotaCredito_Param, DbType.Int32, detalleNotaCredito.IdNumeroNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_IdCausalDetalle_Param, DbType.Int32, detalleNotaCredito.IdCausalDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleNotaCredito_IndHabilitado_Param, DbType.Int16, detalleNotaCredito.IndHabilitado));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.AnulacionFacturaDetalle_Insertar, parametros));
        }

        /// <summary>
        /// Método que almacena la información detallada de la factura.
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoEntidad_Param, DbType.String, detalleFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoConsecutivo_Param, DbType.String, detalleFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NumeroFactura_Param, DbType.Int32, detalleFactura.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdTipoMovimiento_Param, DbType.Int32, detalleFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoMovimiento_Param, DbType.String, detalleFactura.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdVenta_Param, DbType.Int32, detalleFactura.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NumeroVenta_Param, DbType.Int32, detalleFactura.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdProducto_Param, DbType.Int32, detalleFactura.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdLote_Param, DbType.Int32, detalleFactura.IdLote));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdAtencion_Param, DbType.Int32, detalleFactura.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoFacturaDetalle_Param, DbType.String, detalleFactura.CodigoProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NombreFacturaDetalle_Param, DbType.String, detalleFactura.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorFacturaDetalle_Param, DbType.Decimal, detalleFactura.ValorUnitario));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdDescuento_Param, DbType.Int32, detalleFactura.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDescuento_Param, DbType.Decimal, detalleFactura.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorRecargo_Param, DbType.Decimal, detalleFactura.ValorRecargo));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorImpuesto_Param, DbType.Decimal, detalleFactura.ValorImpuesto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_SubValorFacturaDetalle_Param, DbType.Decimal, detalleFactura.SubTotal));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadFacturaDetalle_Param, DbType.Decimal, detalleFactura.CantidadFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadGlosasFacturaDetalle_Param, DbType.Decimal, detalleFactura.CantidadGlosasFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadDisponible_Param, DbType.Decimal, detalleFactura.CantidadDisponible));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdTercero_Param, DbType.Int32, detalleFactura.IdTerceroDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadVenta_Param, DbType.Decimal, detalleFactura.CantidadVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoConcepto_Param, DbType.String, detalleFactura.CodigoConcepto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorPagina_Param, DbType.Decimal, detalleFactura.ValorPagina));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorProducto_Param, DbType.Decimal, detalleFactura.ValorUnitario));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NivelOrden_Param, DbType.Int16, detalleFactura.NivelOrden));

            if (!detalleFactura.VigenciaTarifa.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VigenciaTarifa_Param, DbType.DateTime, detalleFactura.VigenciaTarifa));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VigenciaTarifa_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdManual_Param, DbType.Int32, detalleFactura.IdManual));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorTarifa_Param, DbType.Decimal, detalleFactura.ValorProductos));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoUnidad_Param, DbType.String, detalleFactura.CodigoUnidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdRelacion_Param, DbType.Int32, detalleFactura.IdRelacion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoTipoRelacion_Param, DbType.String, detalleFactura.CodigoTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorPorcentajeParametro_Param, DbType.Decimal, detalleFactura.ValorPorcentajeParametro));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_DescuentoPorcentajeParametro_Param, DbType.Decimal, detalleFactura.DescuentoPorcentajeParametro));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_MetodoConfiguracion_Param, DbType.String, detalleFactura.MetodoConfiguracion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorTasas_Param, DbType.Decimal, detalleFactura.ValorTasas));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VentaTasas_Param, DbType.Decimal, detalleFactura.VentaTasas));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorMaximo_Param, DbType.Decimal, detalleFactura.ValorMaximo));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDiferencia_Param, DbType.Decimal, detalleFactura.ValorDiferencia));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.DetalleFactura_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoEntidad_Param, DbType.String, detalleFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoConsecutivo_Param, DbType.String, detalleFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NumeroFactura_Param, DbType.Int32, detalleFactura.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdTipoMovimiento_Param, DbType.Int32, detalleFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoMovimiento_Param, DbType.String, detalleFactura.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdVenta_Param, DbType.Int32, detalleFactura.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NumeroVenta_Param, DbType.Int32, detalleFactura.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdProducto_Param, DbType.Int32, detalleFactura.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdLote_Param, DbType.Int32, detalleFactura.IdLote));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdAtencion_Param, DbType.Int32, 0)); // Fact No Clínica no maneja # de Atención
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoFacturaDetalle_Param, DbType.String, detalleFactura.CodigoProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NombreFacturaDetalle_Param, DbType.String, detalleFactura.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorFacturaDetalle_Param, DbType.Decimal, detalleFactura.ValorUnitario));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdDescuento_Param, DbType.Int32, detalleFactura.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDescuento_Param, DbType.Decimal, detalleFactura.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorRecargo_Param, DbType.Decimal, detalleFactura.ValorRecargo));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorImpuesto_Param, DbType.Decimal, detalleFactura.ValorImpuesto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_SubValorFacturaDetalle_Param, DbType.Decimal, detalleFactura.SubTotal));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadFacturaDetalle_Param, DbType.Decimal, detalleFactura.CantidadFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadGlosasFacturaDetalle_Param, DbType.Decimal, detalleFactura.CantidadGlosasFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadDisponible_Param, DbType.Decimal, detalleFactura.CantidadFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdTercero_Param, DbType.Int32, detalleFactura.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CantidadVenta_Param, DbType.Decimal, detalleFactura.CantidadFacturaDetalle));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoConcepto_Param, DbType.String, detalleFactura.CodigoConcepto));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorPagina_Param, DbType.Decimal, detalleFactura.ValorPagina));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorProducto_Param, DbType.Decimal, detalleFactura.ValorUnitario));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_NivelOrden_Param, DbType.Int16, detalleFactura.NivelOrden));

            if (!detalleFactura.VigenciaTarifa.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VigenciaTarifa_Param, DbType.DateTime, detalleFactura.VigenciaTarifa));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VigenciaTarifa_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdManual_Param, DbType.Int32, detalleFactura.IdManual));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorTarifa_Param, DbType.Decimal, detalleFactura.ValorProductos));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoUnidad_Param, DbType.String, detalleFactura.CodigoUnidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdRelacion_Param, DbType.Int32, detalleFactura.IdRelacion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoTipoRelacion_Param, DbType.String, detalleFactura.CodigoTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorPorcentajeParametro_Param, DbType.Decimal, detalleFactura.ValorPorcentajeParametro));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_DescuentoPorcentajeParametro_Param, DbType.Decimal, detalleFactura.DescuentoPorcentajeParametro));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_MetodoConfiguracion_Param, DbType.String, detalleFactura.MetodoConfiguracion));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorTasas_Param, DbType.Decimal, detalleFactura.ValorTasas));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_VentaTasas_Param, DbType.Decimal, detalleFactura.VentaTasas));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorMaximo_Param, DbType.Decimal, detalleFactura.ValorMaximo));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDiferencia_Param, DbType.Decimal, detalleFactura.ValorDiferencia));

            return Convert.ToInt32(this.Ejecutar("FACInsertarDetalleFacturaNC", parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("ProIde", DbType.Int32, paquete.PerdidaGanancia.Ajuste));
            parametros.Add(this.CrearParametro("VenNum", DbType.Int32, numeroVenta));
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            this.Ejecutar("FacInsertarFacturaDetallePyG", parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("IdTransaccion", DbType.Int32, ventaComponente.IdTransaccion));
            parametros.Add(this.CrearParametro("NumeroVenta", DbType.Int32, ventaComponente.NumeroVenta));
            parametros.Add(this.CrearParametro("IdProducto", DbType.Int32, ventaComponente.IdProducto));
            parametros.Add(this.CrearParametro("IdLote", DbType.Int32, ventaComponente.IdLote));
            parametros.Add(this.CrearParametro("ComponenteBase", DbType.String, ventaComponente.ComponenteBase));
            parametros.Add(this.CrearParametro("ComponenteHomologado", DbType.String, ventaComponente.ComponenteHomologado));
            parametros.Add(this.CrearParametro("NombreComponente", DbType.String, ventaComponente.NombreComponente));
            parametros.Add(this.CrearParametro("IdComponente", DbType.Int32, ventaComponente.IdComponente));

            this.Ejecutar("FacInsertarFacturaDetallePyGComponente", parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_CodigoEntidad_Param, DbType.String, detalleMovimientoCartera.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_IdMovimiento_Param, DbType.Int32, detalleMovimientoCartera.IdMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_IdCuenta_Param, DbType.Int32, detalleMovimientoCartera.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_IdTipoMovimiento_Param, DbType.Int32, detalleMovimientoCartera.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_NumeroDocumento_Param, DbType.String, detalleMovimientoCartera.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_ValorMonto_Param, DbType.Decimal, detalleMovimientoCartera.ValorMonto));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_ValorAfectado_Param, DbType.Decimal, detalleMovimientoCartera.ValorAfectado));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_InteresCuenta_Param, DbType.Decimal, detalleMovimientoCartera.InteresCuenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_DescuentoMovimiento_Param, DbType.Decimal, detalleMovimientoCartera.DescuentoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_PorcentajeInteres_Param, DbType.Decimal, detalleMovimientoCartera.PorcentajeInteres));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_PorcentajeDescuento_Param, DbType.Decimal, detalleMovimientoCartera.PorcentajeDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_DescuentoConcepto_Param, DbType.Int32, detalleMovimientoCartera.DescuentoConcepto));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_InteresConcepto_Param, DbType.Decimal, detalleMovimientoCartera.InteresConcepto));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_IndCuentaEliminada_Param, DbType.Decimal, detalleMovimientoCartera.IndCuentaEliminada));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_EstadoInicial_Param, DbType.String, detalleMovimientoCartera.EstadoInicial));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_EstadoFinal_Param, DbType.String, detalleMovimientoCartera.EstadoFinal));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_SaldoInicial_Param, DbType.Decimal, detalleMovimientoCartera.SaldoInicial));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_SaldoFinal_Param, DbType.Decimal, detalleMovimientoCartera.SaldoFinal));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_IdTercero_Param, DbType.Int32, detalleMovimientoCartera.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_CodigoEnlaceContable_Param, DbType.String, detalleMovimientoCartera.CodigoEnlaceContable));
            parametros.Add(this.CrearParametro(Parametros.DetalleMovimientoCartera_CodigoMovimiento_Param, DbType.String, detalleMovimientoCartera.CodigoMovimiento));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.DetalleMovimientoCartera_Insertar, parametros));
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
        /// Identificador registro.
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_NumeroFactura_Param, DbType.Int32, Convert.ToInt32(numeroFactura)));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_IdPaquete_Param, DbType.Int32, detallePaquete.IdPaquete));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_IdGrupo_Param, DbType.Int32, detallePaquete.IdGrupo));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_NombreGrupo_Param, DbType.String, detallePaquete.NombreGrupo));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_IdProducto_Param, DbType.Int32, detallePaquete.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_NombreProducto_Param, DbType.String, detallePaquete.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_ValorPaqueteProducto_Param, DbType.Decimal, Math.Truncate(detallePaquete.ValorPaqueteProducto)));
            parametros.Add(this.CrearParametro(Parametros.DetallePaquete_IndHabilitado_Param, DbType.Int16, detallePaquete.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdTerceroVenta_Param, DbType.Int32, estadoCuenta.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NombreTercero_Param, DbType.String, estadoCuenta.NombreTercero));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NumeroDocumento_Param, DbType.String, estadoCuenta.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_NumeroVenta_Param, DbType.Int32, detallePaquete.NumeroVenta));
            parametros.Add(this.CrearParametro("FechaVenta", DbType.DateTime, detallePaquete.FechaVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDescuento_Param, DbType.Decimal, Math.Truncate(detallePaquete.ValorDescuento)));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorRecargo_Param, DbType.Decimal, Math.Truncate(detallePaquete.ValorRecargo)));
            parametros.Add(this.CrearParametro("IdTransaccion", DbType.String, detallePaquete.IdTransaccion));
            parametros.Add(this.CrearParametro("CantidadAsignada", DbType.Decimal, detallePaquete.CantidadAsignada));

            if (detallePaqueteCompleto.ProductoPaquete.Descuentos != null && detallePaqueteCompleto.ProductoPaquete.Descuentos.Count > 0)
            {
                parametros.Add(this.CrearParametro("IdDescuento", DbType.Int32, detallePaqueteCompleto.ProductoPaquete.Descuentos.FirstOrDefault().Id));
            }
            else
            {
                parametros.Add(this.CrearParametro("IdDescuento", DbType.Int32, 0));
            }

            parametros.Add(this.CrearParametro("CantidadDisponible", DbType.Decimal, detallePaquete.CantidadDisponible));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.DetallePaquete_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdProceso_Param, DbType.Int32, detalle.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdAtencion_Param, DbType.Int32, detalle.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdPlan_Param, DbType.Int16, detalle.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdCliente_Param, DbType.Int32, detalle.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdUbicacion_Param, DbType.Int16, detalle.IdUbicacion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_Cruzar_Param, DbType.Int32, detalle.Cruzar));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_ValorConcepto_Param, DbType.Decimal, detalle.ValorConcepto));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2(OperacionesBaseDatos.ProcesoFacturaDetalle_Insertar, parametros);
            return Convert.ToInt32(dt.Rows[0][0]);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdProceso_Param, DbType.Int32, detalle.IdProceso));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdAtencion_Param, DbType.Int32, detalle.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdPlan_Param, DbType.Int16, detalle.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdCliente_Param, DbType.Int32, detalle.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_IdUbicacion_Param, DbType.Int16, detalle.IdUbicacion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_Cruzar_Param, DbType.Int32, detalle.Cruzar));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFacturaDetalle_ValorConcepto_Param, DbType.Decimal, detalle.ValorConcepto));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2("FACInsertarProcesoFacturaDetalleNC", parametros);
            return Convert.ToInt32(dt.Rows[0][0]);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTipoMovimiento_Param, DbType.Int32, procesoFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTercero_Param, DbType.Int32, procesoFactura.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdContrato_Param, DbType.Int32, procesoFactura.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTipoAtencion_Param, DbType.Int16, procesoFactura.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_CodigoSeccion_Param, DbType.String, procesoFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_CodigoEntidad_Param, DbType.String, procesoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdFormato_Param, DbType.Byte, procesoFactura.IdFormato));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_FechaInicio_Param, DbType.DateTime, procesoFactura.FechaInicio));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_FechaFin_Param, DbType.DateTime, procesoFactura.FechaFin));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdEstado_Param, DbType.Byte, procesoFactura.IdEstado));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2(OperacionesBaseDatos.ProcesoFactura_Insertar, parametros);
            return Convert.ToInt32(dt.Rows[0][0]);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTipoMovimiento_Param, DbType.Int32, procesoFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTercero_Param, DbType.Int32, procesoFactura.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdContrato_Param, DbType.Int32, procesoFactura.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdTipoAtencion_Param, DbType.Int16, procesoFactura.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_CodigoSeccion_Param, DbType.String, procesoFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_CodigoEntidad_Param, DbType.String, procesoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdFormato_Param, DbType.Byte, procesoFactura.IdFormato));
            parametros.Add(this.CrearParametro(Parametros.ProcesoFactura_IdEstado_Param, DbType.Byte, procesoFactura.IdEstado));

            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2("FACInsertarProcesoFacturaNC", parametros);
            return Convert.ToInt32(dt.Rows[0][0]);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_CodigoEntidad_Param, DbType.String, contabilidad.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_IdTipoMovimiento_Param, DbType.Int32, contabilidad.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_CodigoSecuencial_Param, DbType.String, contabilidad.CodigoSecuencial));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_CodigoMovimiento_Param, DbType.String, contabilidad.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_NumeroFactura_Param, DbType.Int32, contabilidad.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_FechaRegistro_Param, DbType.DateTime, contabilidad.FechaRegistro));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_TipoRegistro_Param, DbType.String, contabilidad.TipoRegistro));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_ValorRegistro_Param, DbType.Decimal, Math.Truncate(contabilidad.ValorRegistro)));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_FechaRegistroEstado_Param, DbType.DateTime, contabilidad.FechaRegistroEstado));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_EstadoRegistro_Param, DbType.String, contabilidad.EstadoRegistro));
            parametros.Add(this.CrearParametro(Parametros.EstadoCuentaContabilidad_TipoMovimiento_Param, DbType.String, contabilidad.TipoMovimiento));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.EstadoCuentaContabilidad_Insertar, parametros));
        }

        /// <summary>
        /// Método para almacenar la información de la exclusión de contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de la exclusión insertada.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_CodigoEntidad_Param, DbType.String, exclusion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdTercero_Param, DbType.Int32, exclusion.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdTipoAtencion_Param, DbType.Int32, exclusion.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdServicio_Param, DbType.Int32, exclusion.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdManual_Param, DbType.Int32, exclusion.IdManual));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_VigenciaManual_Param, DbType.DateTime, exclusion.VigenciaTarifa));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdProductoTipo_Param, DbType.Int32, exclusion.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdGrupo_Param, DbType.Int32, exclusion.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdProducto_Param, DbType.Int32, exclusion.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_CodigoComponente_Param, DbType.String, exclusion.Componente));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IndHabilitado_Param, DbType.Int16, exclusion.IndicadorContratoActivo));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdContrato_Param, DbType.Int32, exclusion.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdPlan_Param, DbType.Int32, exclusion.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdAtencion_Param, DbType.Int32, exclusion.IdAtencion));

            if (exclusion.IdVenta > 0)
            {
                parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_IdVenta_Param, DbType.Int32, exclusion.IdVenta));
            }

            if (exclusion.NumeroVenta > 0)
            {
                parametros.Add(this.CrearParametro(Parametros.ExclusionContrato_NumeroVenta_Param, DbType.Int32, exclusion.NumeroVenta));
            }

            parametros.Add(this.CrearParametro("IndAplicarSiempre", DbType.Int16, exclusion.IndicadorContratoAplicado));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ExclusionContrato_Insertar, parametros));
        }

        /// <summary>
        /// Método para Guardar los Componentes.
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
        public int GuardarFacturaComponentes(FacturaComponentes facturaComponentes)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_EntCod_Param, DbType.String, facturaComponentes.Entidad));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacSecCod_Param, DbType.String, facturaComponentes.Unidadfuncional));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacNum_Param, DbType.Int32, facturaComponentes.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacTimIde_Param, DbType.Int32, facturaComponentes.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacMosCod_Param, DbType.String, facturaComponentes.CodigoNumeracion));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_VenTraIde_Param, DbType.Int32, facturaComponentes.TransaccionVenta));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_VenNum_Param, DbType.Int32, facturaComponentes.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_ProIde_Param, DbType.Int32, facturaComponentes.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_VenDetLot_Param, DbType.Int32, facturaComponentes.Lote));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_TicCod_Param, DbType.String, facturaComponentes.IdComponente));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicCod_Param, DbType.String, facturaComponentes.ComponenteHomologado));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicNom_Param, DbType.String, facturaComponentes.NombreComponenteHomologado));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicCan_Param, DbType.Decimal, facturaComponentes.CantidadComponente));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicPorApl_Param, DbType.Decimal, facturaComponentes.PorcentajeFactorQXAplicado));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicVal_Param, DbType.Decimal, Math.Truncate(facturaComponentes.ValorUnitario)));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_facDetTicDecIde_Param, DbType.Int32, facturaComponentes.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicValDes_Param, DbType.Decimal, Math.Truncate(facturaComponentes.ValorDescuento)));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicValRec_Param, DbType.Decimal, Math.Truncate(facturaComponentes.ValorRecargo)));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicCanGlo_Param, DbType.Decimal, facturaComponentes.CantidadGlozasa));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicCanDis_Param, DbType.Decimal, facturaComponentes.CantidadComponente));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicIndVal_Param, DbType.Int16, facturaComponentes.FacDetTicIndVal));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicTipVal_Param, DbType.String, facturaComponentes.FacDetTicTipVal));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicRecIde_Param, DbType.Int32, facturaComponentes.FacDetTicRecIde));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicRecTip_Param, DbType.String, facturaComponentes.FacDetTicRecTip));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacDetTicValMax_Param, DbType.Decimal, Math.Truncate(facturaComponentes.FacDetTicValMax)));
            parametros.Add(this.CrearParametro(Parametros.FacturaComponentes_FacdetTicValDif_Param, DbType.Decimal, Math.Truncate(facturaComponentes.FacdetTicValDif)));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Factura_Componenstes_Insertar, parametros));
        }

        /// <summary>
        /// Método que inserta la información del pago de la factura.
        /// </summary>
        /// <param name="facturaPago">The factura pago.</param>
        /// <returns>Indica si se guardo bien la Factura de Pago.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_CodigoEntidad_Param, DbType.String, facturaPago.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_CodigoSeccion_Param, DbType.String, facturaPago.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_NumeroFactura_Param, DbType.Int32, facturaPago.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_IdTipoMovimiento_Param, DbType.Int32, facturaPago.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_CodigoMovimiento_Param, DbType.String, facturaPago.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_CodigoConceptoPago_Param, DbType.String, facturaPago.CodigoConceptoPago));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_ValorPagoFactura_Param, DbType.Decimal, Math.Truncate(facturaPago.ValorPagoFactura)));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_ValorPagoFacturaCruzado_Param, DbType.Decimal, Math.Truncate(facturaPago.ValorPagoFacturaCruzado)));
            parametros.Add(this.CrearParametro(Parametros.FacturaPago_EstadoPagoFactura_Param, DbType.Int32, facturaPago.EstadoPagoFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.FacturaPago_Insertar, parametros));
        }

        /// <summary>
        /// Método que inserta la información del detalle del pago de la factura.
        /// </summary>
        /// <param name="facturaPagoDetalle">The factura pago detalle.</param>
        /// <returns>Indica si se guiardo bien Factura Pago Detalle.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_CodigoEntidad_Param, DbType.String, facturaPagoDetalle.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_CodigoSeccion_Param, DbType.String, facturaPagoDetalle.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_NumeroFactura_Param, DbType.Int32, facturaPagoDetalle.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_IdTipoMovimiento_Param, DbType.Int32, facturaPagoDetalle.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_CodigoMovimiento_Param, DbType.String, facturaPagoDetalle.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_CodigoConceptoPago_Param, DbType.String, facturaPagoDetalle.CodigoConceptoPago));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_IdAtencion_Param, DbType.Int32, facturaPagoDetalle.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_ValorPagoFactura_Param, DbType.Decimal, Math.Truncate(facturaPagoDetalle.ValorPagoFactura)));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_ValorPagoFacturaCruzado_Param, DbType.Decimal, Math.Truncate(facturaPagoDetalle.ValorPagoFacturaCruzado)));
            parametros.Add(this.CrearParametro(Parametros.FacturaPagoDetalle_EstadoPagoFactura_Param, DbType.Int32, facturaPagoDetalle.EstadoPagoFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.FacturaPagoDetalle_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("numeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("idPaquete", DbType.Int32, identificadorPaquete));
            parametros.Add(this.CrearParametro("numeroVenta", DbType.Int32, numeroVenta));
            parametros.Add(this.CrearParametro("lote", DbType.Int32, lote));

            this.Ejecutar("FacFacturaRelacionCabecera", parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("numeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("numeroFacturaPyG", DbType.Int32, numeroFacturaPyG));
            parametros.Add(this.CrearParametro("idPaquete", DbType.Int32, identificadorPaquete));

            this.Ejecutar("FacFacturaRelacionCabeceraDetalle", parametros);
        }

        /// <summary>
        /// Método que almacena la información de la cabezera de la factura.
        /// </summary>
        /// <param name="encabezadoFactura">The enbezado factura.</param>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoEntidad_Param, DbType.String, encabezadoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoSeccion_Param, DbType.String, encabezadoFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_NumeroFactura_Param, DbType.Int32, encabezadoFactura.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTipoMovimiento_Param, DbType.Int32, encabezadoFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoMovimiento_Param, DbType.String, encabezadoFactura.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdContrato_Param, DbType.Int32, encabezadoFactura.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdPlan_Param, DbType.Int32, encabezadoFactura.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ConsecutivoFacturacion_Param, DbType.Int32, encabezadoFactura.ConsecutivoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFactura_Param, DbType.DateTime, encabezadoFactura.FechaFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_TipoFacturacion_Param, DbType.String, encabezadoFactura.TipoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_PrefijoNumeracion_Param, DbType.String, encabezadoFactura.PrefijoNumeracion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_Estado_Param, DbType.String, encabezadoFactura.Estado));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTercero_Param, DbType.Int32, encabezadoFactura.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoUsuario_Param, DbType.String, encabezadoFactura.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdAtencion_Param, DbType.Int32, encabezadoFactura.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdCodigoFactura_Param, DbType.Int32, encabezadoFactura.IdCodigoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorCodigoFactura_Param, DbType.Decimal, encabezadoFactura.ValorCodigoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdDocumentoFactura_Param, DbType.Int32, encabezadoFactura.IdDocumentoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_NumeroDocumentoFactura_Param, DbType.String, encabezadoFactura.NumeroDocumentoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorDescuento_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorDescuento)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTipoServicio_Param, DbType.Int32, encabezadoFactura.IdTipoServicio));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdUbicacion_Param, DbType.Int32, encabezadoFactura.IdUbicacion));
            if (!encabezadoFactura.FechaInicial.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaInicial_Param, DbType.DateTime, encabezadoFactura.FechaInicial));
            }

            if (!encabezadoFactura.FechaFinal.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFinal_Param, DbType.DateTime, encabezadoFactura.FechaFinal));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_HoraFactura_Param, DbType.DateTime, encabezadoFactura.HoraFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_DescuentoValorFactura_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.DescuentoValorFactura)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdDescuento_Param, DbType.Int32, encabezadoFactura.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorTotalDebito_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorTotalDebito)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorTotalFactura_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorTotalFactura)));

            if (encabezadoFactura.Observaciones == null)
            {
                encabezadoFactura.Observaciones = string.Empty;
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_Observaciones_Param, DbType.String, encabezadoFactura.Observaciones));

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTerceroResponsable_Param, DbType.Int32, encabezadoFactura.IdTerceroResponsable));

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_TipoResultadoTercero_Param, DbType.String, encabezadoFactura.TipoResultadoTercero));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ConsecutivoCartera_Param, DbType.Int32, encabezadoFactura.ConsecutivoCartera));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorSaldo_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorSaldo)));

            if (!encabezadoFactura.FechaSaldo.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaSaldo_Param, DbType.DateTime, encabezadoFactura.FechaSaldo));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaSaldo_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_EstadoAuxiliar_Param, DbType.String, encabezadoFactura.EstadoAuxiliar));

            if (!encabezadoFactura.FechaFacturaDesde.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaDesde_Param, DbType.DateTime, encabezadoFactura.FechaFacturaDesde));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaDesde_Param, DbType.DateTime, DateTime.Now));
            }

            if (!encabezadoFactura.FechaFacturaHasta.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaHasta_Param, DbType.DateTime, encabezadoFactura.FechaFacturaHasta));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaHasta_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IndicadorImpresion_Param, DbType.Boolean, encabezadoFactura.IndicadorImpresion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdSede_Param, DbType.Int32, encabezadoFactura.IdSede));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IndicadorIngresoPropio_Param, DbType.Boolean, encabezadoFactura.IndicadorIngresoPropio));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.EncabezadoFactura_Insertar, parametros));
        }

        /// <summary>
        /// Método que almacena la información de la cabecera de la factura no clínica.
        /// </summary>
        /// <param name="encabezadoFactura">The encabezado factura.</param>
        /// <param name="identificadorModulo">El identificador del módulo</param>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoEntidad_Param, DbType.String, encabezadoFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoSeccion_Param, DbType.String, encabezadoFactura.CodigoSeccion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_NumeroFactura_Param, DbType.Int32, encabezadoFactura.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTipoMovimiento_Param, DbType.Int32, encabezadoFactura.IdTipoMovimiento));
            parametros.Add(this.CrearParametro("IdModulo", DbType.Int32, identificadorModulo));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdContrato_Param, DbType.Int32, 0)); // no debe ir # de contrato em Fact. No Clínica
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdPlan_Param, DbType.Int32, encabezadoFactura.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ConsecutivoFacturacion_Param, DbType.Int32, encabezadoFactura.ConsecutivoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFactura_Param, DbType.DateTime, encabezadoFactura.FechaFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_TipoFacturacion_Param, DbType.String, encabezadoFactura.TipoFacturacion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_PrefijoNumeracion_Param, DbType.String, encabezadoFactura.PrefijoNumeracion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_Estado_Param, DbType.String, encabezadoFactura.Estado));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTercero_Param, DbType.Int32, encabezadoFactura.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_CodigoUsuario_Param, DbType.String, encabezadoFactura.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdAtencion_Param, DbType.Int32, 0)); // no debe ir # de atención en Fact. No Clínica
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdCodigoFactura_Param, DbType.Int32, encabezadoFactura.IdCodigoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorCodigoFactura_Param, DbType.Decimal, encabezadoFactura.ValorCodigoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdDocumentoFactura_Param, DbType.Int32, encabezadoFactura.IdDocumentoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_NumeroDocumentoFactura_Param, DbType.String, encabezadoFactura.NumeroDocumentoFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorDescuento_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorDescuento)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTipoServicio_Param, DbType.Int32, encabezadoFactura.IdTipoServicio));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdUbicacion_Param, DbType.Int32, encabezadoFactura.IdUbicacion));
            if (!encabezadoFactura.FechaInicial.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaInicial_Param, DbType.DateTime, encabezadoFactura.FechaInicial));
            }

            if (!encabezadoFactura.FechaFinal.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFinal_Param, DbType.DateTime, encabezadoFactura.FechaFinal));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_HoraFactura_Param, DbType.DateTime, encabezadoFactura.HoraFactura));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_DescuentoValorFactura_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.DescuentoValorFactura)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdDescuento_Param, DbType.Int32, encabezadoFactura.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorTotalDebito_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorTotalDebito)));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorTotalFactura_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorTotalFactura)));

            if (encabezadoFactura.Observaciones == null)
            {
                encabezadoFactura.Observaciones = string.Empty;
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_Observaciones_Param, DbType.String, encabezadoFactura.Observaciones));

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdTerceroResponsable_Param, DbType.Int32, encabezadoFactura.IdTerceroResponsable));

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_TipoResultadoTercero_Param, DbType.String, encabezadoFactura.TipoResultadoTercero));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ConsecutivoCartera_Param, DbType.Int32, encabezadoFactura.ConsecutivoCartera));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_ValorSaldo_Param, DbType.Decimal, Math.Truncate(encabezadoFactura.ValorSaldo)));

            if (!encabezadoFactura.FechaSaldo.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaSaldo_Param, DbType.DateTime, encabezadoFactura.FechaSaldo));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaSaldo_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_EstadoAuxiliar_Param, DbType.String, encabezadoFactura.EstadoAuxiliar));

            if (!encabezadoFactura.FechaFacturaDesde.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaDesde_Param, DbType.DateTime, encabezadoFactura.FechaFacturaDesde));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaDesde_Param, DbType.DateTime, DateTime.Now));
            }

            if (!encabezadoFactura.FechaFacturaHasta.Equals(DateTime.MinValue))
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaHasta_Param, DbType.DateTime, encabezadoFactura.FechaFacturaHasta));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_FechaFacturaHasta_Param, DbType.DateTime, DateTime.Now));
            }

            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IndicadorImpresion_Param, DbType.Boolean, encabezadoFactura.IndicadorImpresion));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IdSede_Param, DbType.Int32, encabezadoFactura.IdSede));
            parametros.Add(this.CrearParametro(Parametros.EncabezadoFactura_IndicadorIngresoPropio_Param, DbType.Boolean, encabezadoFactura.IndicadorIngresoPropio));
            return Convert.ToInt32(this.Ejecutar("FACInsertarEncabezadoFacturaNC", parametros));
        }

        /// <summary>
        /// Guardars the informacion paquetes factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="valorPyG">The valor py g.</param>
        /// <returns>Retorna el numero de la factura PyG.</returns>
        public int GuardarInformacionPaquetesFactura(int numeroFactura, decimal valorPyG)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFacturaPadre", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("FacValPyG", DbType.Decimal, valorPyG));
            return Convert.ToInt32(this.Ejecutar("FacInsertarPaquetesPyG", parametros));
        }

        /// <summary>
        /// Método que almacena la información de la cabezera de la factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Numero de factura.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public string[] GuardarInformacionTodaFactura(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            string[] ret = new string[2];
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("Encabezado", DbType.Xml, encabezado));
            parametros.Add(this.CrearParametro("Detalle", DbType.Xml, detalle));
            parametros.Add(this.CrearParametro("MaestroComponentes", DbType.Xml, maestroComponentes));
            parametros.Add(this.CrearParametro("Ventas", DbType.Xml, ventas));
            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2("FACInsertarFacturaTodo", parametros);
            ret[0] = dt.Rows[0][0].ToString();
            ret[1] = dt.Rows[0][1].ToString();
            return ret;
        }

        /// <summary>
        /// Método que almacena la información de la cabezera de la factura.
        /// </summary>
        /// <param name="encabezado">The encabezado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="maestroComponentes">The maestro componentes.</param>
        /// <param name="ventas">The ventas.</param>
        /// <returns>
        /// Numero de factura.
        /// </returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public string[] GuardarInformacionTodaFacturaPaquetes(string encabezado, string detalle, string maestroComponentes, string ventas)
        {
            string[] ret = new string[2];
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("Encabezado", DbType.Xml, encabezado));
            parametros.Add(this.CrearParametro("Detalle", DbType.Xml, detalle));
            parametros.Add(this.CrearParametro("MaestroComponentes", DbType.Xml, maestroComponentes));
            parametros.Add(this.CrearParametro("Ventas", DbType.Xml, ventas));
            DataTable dt = (DataTable)this.EjecutarProcedimientoTipo2("FACInsertarFacturaTodo", parametros);
            ret[0] = dt.Rows[0][0].ToString();
            ret[1] = dt.Rows[0][1].ToString();
            return ret;
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("IdTercero", DbType.Int32, tercero.Id));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IdTipoDocumento_Param, DbType.Byte, tercero.IdTipoDocumento));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NumeroDocumento_Param, DbType.String, tercero.NumeroDocumento));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NombreTercero_Param, DbType.String, tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IdNaturaleza_Param, DbType.Int16, tercero.IdNaturaleza));
            parametros.Add(this.CrearParametro(Parametros.Tercero_DigitoVerificacion_Param, DbType.Int32, tercero.DigitoVerificacion));

            return Convert.ToInt32(this.Ejecutar("FACModificarTercero", parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_CodigoEntidad_Param, DbType.String, movimientoCartera.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_IdMovimientoDocumento_Param, DbType.Int32, movimientoCartera.IdMovimientoDocumento));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_CodigoMovimientoSecuencial_Param, DbType.String, movimientoCartera.CodigoMovimientoSecuencial));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_CodigoSecuencia_Param, DbType.String, movimientoCartera.CodigoSecuencia));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_NumeroMovimientoPrevio_Param, DbType.String, movimientoCartera.NumeroMovimientoPrevio));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_FechaRegistro_Param, DbType.DateTime, movimientoCartera.FechaRegistro));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_DescripcionMovimiento_Param, DbType.String, movimientoCartera.DescripcionMovimiento));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_FechaMovimiento_Param, DbType.DateTime, movimientoCartera.FechaMovimiento));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_IndHabilitado_Param, DbType.Int16, movimientoCartera.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_CodigoUsuario_Param, DbType.String, movimientoCartera.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.MovimientoCartera_IdSede_Param, DbType.Int32, movimientoCartera.IdSede));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.MovimientoCartera_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("Usuario", DbType.String, usuario));
            parametros.Add(this.CrearParametro("IDNumeroNotaCredito", DbType.Int32, identificadorNotaCredito));
            return Convert.ToInt32(this.Ejecutar("FACInsertarMovimientoCarteraAjusteSaldo", parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimiento_Param, DbType.String, notaCredito.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNumeroNotaCredito_Param, DbType.Int32, notaCredito.IdNumeroNotaCredito));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.MovimientoAnulacion_Insertar, parametros));
        }

        /// <summary>
        /// Método para almacenar la información de los No Facturable.
        /// </summary>
        /// <param name="parametroNoFacturable">The no facturable.</param>
        /// <returns>Indica si se guardaron bien los No Facturables.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 08/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarNoFacturable(NoFacturable parametroNoFacturable)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdAtencion_Param, DbType.Int32, parametroNoFacturable.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdExclusion_Param, DbType.Int32, parametroNoFacturable.IdExclusion));
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdProcesoDetalle_Param, DbType.Int32, parametroNoFacturable.IdProcesoDetalle));
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdProducto_Param, DbType.Int32, parametroNoFacturable.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_IdVenta_Param, DbType.Int32, parametroNoFacturable.IdVenta));
            parametros.Add(this.CrearParametro(Parametros.NoFacturable_NumeroVenta_Param, DbType.Int32, parametroNoFacturable.NumeroVenta));
            parametros.Add(this.CrearParametro("CantidadProducto", DbType.Decimal, parametroNoFacturable.CantidadProducto));
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, parametroNoFacturable.NumeroFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.NoFacturable_Insertar, parametros));
        }

        /// <summary>
        /// Guardars the paquete factura.
        /// </summary>
        /// <param name="identificadorAtencion">The identificador atencion.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="paquete">The paquete.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarPaqueteFactura(int identificadorAtencion, string numeroFactura, Paquete paquete)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Paquete_IdAtencion_Param, DbType.Int32, identificadorAtencion));
            parametros.Add(this.CrearParametro(Parametros.Paquete_NumeroFactura_Param, DbType.Int32, Convert.ToInt32(numeroFactura)));
            parametros.Add(this.CrearParametro(Parametros.Paquete_IdPaquete_Param, DbType.Int32, paquete.IdPaquete));
            parametros.Add(this.CrearParametro(Parametros.Paquete_NombrePaquete_Param, DbType.String, paquete.NombrePaquete));
            parametros.Add(this.CrearParametro(Parametros.Paquete_NombreTipoProducto_Param, DbType.String, paquete.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Paquete_CodigoPaquete_Param, DbType.String, paquete.CodigoPaquete));
            parametros.Add(this.CrearParametro(Parametros.Paquete_IndHabilitado_Param, DbType.Boolean, paquete.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Paquete_Cantidad_Param, DbType.Int32, paquete.Cantidad));
            parametros.Add(this.CrearParametro(Parametros.Paquete_Ajuste_Param, DbType.Int32, paquete.PerdidaGanancia.Ajuste));
            parametros.Add(this.CrearParametro(Parametros.Paquete_ValorPaquete_Param, DbType.Decimal, Math.Truncate(paquete.ValorPaquete)));
            parametros.Add(this.CrearParametro(Parametros.Paquete_CodigoPG_Param, DbType.String, paquete.PerdidaGanancia.CodigoPG));
            parametros.Add(this.CrearParametro(Parametros.Paquete_NombrePG_Param, DbType.String, paquete.PerdidaGanancia.NombrePG));
            parametros.Add(this.CrearParametro("ValorPG", DbType.Decimal, paquete.PerdidaGanancia.ValorPaquetePG));
            parametros.Add(this.CrearParametro("ValorDescuento", DbType.Decimal, Math.Truncate(paquete.ValorDescuento)));
            parametros.Add(this.CrearParametro("FechaVenta", DbType.DateTime, DateTime.Now));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Paquete_Insertar, parametros));
        }

        /// <summary>
        /// Metodo para realizar la insercion de recargo.
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTercero_Param, DbType.Int32, recargo.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IDEntidad_Param, DbType.String, recargo.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdContrato_Param, DbType.Int32, recargo.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdPlan_Param, DbType.Int32, recargo.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTipoProducto_Param, DbType.Int32, recargo.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdNombreTipoProducto_Param, DbType.String, recargo.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdGrupoProducto_Param, DbType.Int16, recargo.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_NombreGrupoProducto_Param, DbType.String, recargo.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdProducto_Param, DbType.Int32, recargo.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_NombreProducto_Param, DbType.String, recargo.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_Componente_Param, DbType.String, recargo.Componente));
            parametros.Add(this.CrearParametro(Parametros.Recargo_TipoRecargo_Param, DbType.String, recargo.TipoRecargo));
            parametros.Add(this.CrearParametro(Parametros.Recargo_CodServicio_Param, DbType.Int32, recargo.IdServicio));
            parametros.Add(this.CrearParametro(Parametros.Recargo_FechaVigenciaI_Param, DbType.DateTime, recargo.FechaInicial));
            parametros.Add(this.CrearParametro(Parametros.Recargo_FechaVigenciaf_Param, DbType.DateTime, recargo.FechaInicial));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IndHabilitado_Param, DbType.Int16, recargo.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdTipoAtencion_Param, DbType.Int32, recargo.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Recargo_Porcentaje_Param, DbType.Double, recargo.PorcentajeRecargo));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Recargo_Insertar, parametros));
        }

        /// <summary>
        /// Método que inserta un responsable.
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_CodigoEntidad_Param, DbType.String, responsable.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_CodigoSecuencial_Param, DbType.String, responsable.Consecutivo));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_NumeroFactura_Param, DbType.Int32, responsable.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_IdTipoMovimiento_Param, DbType.Int32, responsable.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_CodigoMovimiento_Param, DbType.String, responsable.CodigoMovimientoSecuencial));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_IdVenta_Param, DbType.Int32, responsable.IdVenta));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_NumeroVenta_Param, DbType.Int32, responsable.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_IdProducto_Param, DbType.Int32, responsable.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_LoteVenta_Param, DbType.Int32, responsable.LoteDetalleVenta));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_Componente_Param, DbType.String, responsable.Componente));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_IdTercero_Param, DbType.Int32, responsable.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_PorcentajeDescuento_Param, DbType.Decimal, responsable.PorcentajeDescuentoDetalle));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_PorcentajeValor_Param, DbType.Decimal, responsable.PorcentajeValorDetalle));
            parametros.Add(this.CrearParametro(Parametros.ResponsableFactura_IdComponente_Param, DbType.Int32, responsable.IdComponente));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.ResponsableFactura_Insertar, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Tercero_CodigoRegimen_Param, DbType.String, tercero.CodigoRegimen));
            parametros.Add(this.CrearParametro(Parametros.Tercero_DigitoVerificacion_Param, DbType.Int32, tercero.DigitoVerificacion));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IdNaturaleza_Param, DbType.Int16, tercero.IdNaturaleza));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IdTipoDocumento_Param, DbType.Byte, tercero.IdTipoDocumento));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IdTipoTercero_Param, DbType.Int16, tercero.IdTipoTercero));
            parametros.Add(this.CrearParametro(Parametros.Tercero_IndHabilitado_Param, DbType.Boolean, tercero.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NombreTercero_Param, DbType.String, tercero.Nombre));
            parametros.Add(this.CrearParametro(Parametros.Tercero_NumeroDocumento_Param, DbType.String, tercero.NumeroDocumento));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Tercero_Insertar, parametros));
        }

        /// <summary>
        ///  Método para almacenar el detalle de venta.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>Valor de Confirmacion.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Venta_CodigoEntidad_Param, DbType.String, venta.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTransaccion_Param, DbType.Int32, venta.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTercero_Param, DbType.Int32, venta.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Venta_FechaVenta_Param, DbType.DateTime, venta.FechaVenta));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTipoProducto_Param, DbType.Int32, venta.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdUbicacionConsumo_Param, DbType.Int32, venta.IdUbicacionConsumo));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdUbicacionEntrega_Param, DbType.Int32, venta.IdUbicacionEntrega));
            parametros.Add(this.CrearParametro(Parametros.Venta_EstadoVenta_Param, DbType.String, venta.EstadoVenta));
            parametros.Add(this.CrearParametro(Parametros.Venta_Usuario_Param, DbType.String, venta.Usuario));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTipoVenta_Param, DbType.Int32, venta.IdTipoVenta));
            parametros.Add(this.CrearParametro(Parametros.Venta_Observaciones_Param, DbType.String, venta.Observaciones));
            parametros.Add(this.CrearParametro(Parametros.Venta_DocumentoRespaldo_Param, DbType.String, venta.DocumentoRespaldo));
            parametros.Add(this.CrearParametro(Parametros.Venta_IndFactura_Param, DbType.Int16, 1));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdExclusion_Param, DbType.Int32, venta.IdExclusion));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdAtencion_Param, DbType.Int32, venta.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdContrato_Param, DbType.Int32, venta.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdPlan_Param, DbType.Int32, venta.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Venta_CodigoMovimiento_Param, DbType.String, venta.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.Venta_NombreTarifa_Param, DbType.String, venta.NombreTarifa));
            parametros.Add(this.CrearParametro(Parametros.Venta_NombreManual_Param, DbType.String, venta.NombreManual));
            parametros.Add(this.CrearParametro(Parametros.Venta_NombreContrato_Param, DbType.String, venta.NombreContrato));
            parametros.Add(this.CrearParametro(Parametros.Venta_UsuarioBloquea_Param, DbType.String, venta.UsuarioBloquea));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTipoAtencion_Param, DbType.Int32, venta.IdTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.Venta_UsuarioFactura_Param, DbType.String, venta.UsuarioFactura));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdCliente_Param, DbType.Int32, venta.IdCliente));
            parametros.Add(this.CrearParametro(Parametros.Venta_VenSerCon_Param, DbType.Int32, venta.VenSerCon));
            parametros.Add(this.CrearParametro(Parametros.Venta_VenSerEnt_Param, DbType.Int32, venta.VenSerEnt));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.Venta_Insertar, parametros));
        }

        /// <summary>
        /// Metodo para realizar el guardado del Componente.
        /// </summary>
        /// <param name="ventaComponente">The venta componente.</param>
        /// <returns>Indica si se almacena el Componente.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/10/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int GuardarVentaComponente(VentaComponente ventaComponente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_CodigoEntidad_Param, DbType.String, ventaComponente.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTransaccion_Param, DbType.Int32, ventaComponente.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_NumeroVenta_Param, DbType.Int32, ventaComponente.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdProducto_Param, DbType.Int32, ventaComponente.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdLote_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_Componente_Param, DbType.String, ventaComponente.Componente));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdDescuento_Param, DbType.Int32, ventaComponente.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdRecargo_Param, DbType.Int32, ventaComponente.IdRecargo));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_TipoRecargo_Param, DbType.String, ventaComponente.TipoRecargo));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IndValorTipoComponente_Param, DbType.Int16, (short)1));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorTipoComponente_Param, DbType.Decimal, ventaComponente.ValorTotal));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorRecargo_Param, DbType.Decimal, 0));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorDescuento_Param, DbType.Decimal, ventaComponente.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_CantidadTipoComponente_Param, DbType.Decimal, ventaComponente.CantidadComponente));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_CantidadNivelComponente_Param, DbType.Decimal, ventaComponente.CantidadComponente));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_PorcentajeAplicado_Param, DbType.Decimal, ventaComponente.Porcentaje));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_IdProductoComponente_Param, DbType.Int32, ventaComponente.IdComponente));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_TipoValorComponente_Param, DbType.String, ventaComponente.TipoValorComponente));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorOrigenComponente_Param, DbType.Decimal, ventaComponente.ValorTotal));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorDescuentoOrigen_Param, DbType.Decimal, ventaComponente.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.VentaComponenteBase_ValorRecargoOrigen_Param, DbType.Decimal, 0));
            return Convert.ToInt32(this.Crear(OperacionesBaseDatos.VentaComponente_Insertar, parametros));
        }

        /// <summary>
        /// Método para almacenar el detalle de venta.
        /// </summary>
        /// <param name="venta">The venta.</param>
        /// <returns>Valor de Confirmacion.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Ventas_Bilateralidad_Parametro, DbType.Byte, venta.Bilateralidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_Cantidad_Param, DbType.Decimal, venta.Cantidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_CodigoEntidad_Param, DbType.String, venta.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_NumeroVenta_Param, DbType.Int32, venta.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_CodigoUnidad_Param, DbType.String, venta.CodigoUnidad));
            parametros.Add(this.CrearParametro(Parametros.Ventas_Especialidad_Parametro, DbType.Int32, venta.IdEspecialidad));
            if (venta.FechaVigencia != new DateTime())
            {
                parametros.Add(this.CrearParametro(Parametros.VigenciaTarifa_FechaVigencia_Param, DbType.DateTime, venta.FechaVigencia));
            }
            else
            {
                parametros.Add(this.CrearParametro(Parametros.VigenciaTarifa_FechaVigencia_Param, DbType.DateTime, new DateTime(1753, 1, 1)));
            }

            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdDescuento_Param, DbType.Int32, venta.IdDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleHonorario_IdHonorario_Param, DbType.Int32, venta.IdHonorario));
            parametros.Add(this.CrearParametro(Parametros.Honorario_IdManual_Param, DbType.Int32, venta.IdManual));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdPersonal_Param, DbType.Int32, venta.IdPersonal));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdProducto_Param, DbType.Int32, venta.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Recargo_IdRecargo_Param, DbType.Int32, venta.IdRecargo));
            parametros.Add(this.CrearParametro(Parametros.Venta_IdTercero_Param, DbType.Int32, venta.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.DetalleTarifa_IdTipoProducto_Param, DbType.Int32, venta.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdTransaccion_Param, DbType.Int32, venta.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.Ventas_NivelComplejidad_Parametro, DbType.Int16, venta.NivelComplejidad));
            parametros.Add(this.CrearParametro(Parametros.VigenciaTarifa_NivelOrden_Param, DbType.Int16, venta.NivelOrden));
            parametros.Add(this.CrearParametro(Parametros.Ventas_SubTotal_Parametro, DbType.Decimal, venta.SubTotal));
            parametros.Add(this.CrearParametro(Parametros.Ventas_TiempoCirugia_Parametro, DbType.Int32, venta.TiempoCirugia));
            parametros.Add(this.CrearParametro(Parametros.Recargos_TipoRecargo_Param, DbType.String, venta.TipoRecargo));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorDescuento_Param, DbType.Decimal, venta.ValorDescuento));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorRecargo_Param, DbType.Decimal, venta.ValorRecargo));
            parametros.Add(this.CrearParametro(Parametros.Ventas_ValorUnitario_Parametro, DbType.Decimal, venta.ValorUnitario));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_ValorVenta_Param, DbType.Decimal, venta.ValorVenta));
            parametros.Add(this.CrearParametro(Parametros.Ventas_Via_Parametro, DbType.Int32, venta.IdVia));
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_ValorTasas_Param, DbType.Decimal, venta.ValorImpuesto));
            parametros.Add(this.CrearParametro(Parametros.Ventas_CodigoTasa_Parametro, DbType.String, venta.CodigoTasa));
            parametros.Add(this.CrearParametro(Parametros.Venta_FechaVenta_Param, DbType.DateTime, venta.FechaVenta));
            parametros.Add(this.CrearParametro("ValorTarifa", DbType.Decimal, venta.ValorTarifa));

            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VentaDetalle_Insertar, parametros));
        }

        /// <summary>
        /// Guardars the venta factura.
        /// </summary>
        /// <param name="ventaFactura">The venta factura.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarVentaFactura(VentaFactura ventaFactura, string numeroFactura)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_CodigoEntidad_Param, DbType.String, ventaFactura.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_IdVenta_Param, DbType.Int32, ventaFactura.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_NumeroVenta_Param, DbType.Int32, ventaFactura.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_IdProducto_Param, DbType.Int32, ventaFactura.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_IdLote_Param, DbType.Int32, ventaFactura.IdLote));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_IdContrato_Param, DbType.Int32, 0));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_IdPlan_Param, DbType.Int32, ventaFactura.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_Cantidad_Param, DbType.Decimal, ventaFactura.Cantidad));
            parametros.Add(this.CrearParametro(Parametros.VentaFactura_ValorVenta_Param, DbType.Decimal, Math.Truncate(ventaFactura.ValorVenta)));
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.String, numeroFactura));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VentaFactura_Insertar, parametros));
        }

        /// <summary>
        /// Guardars the venta producto relacion.
        /// </summary>
        /// <param name="ventaRelacion">The venta relacion.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarVentaProductoRelacion(VentaProductoRelacion ventaRelacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_CodigoEntidad_Param, DbType.String, ventaRelacion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdAtencion_Param, DbType.Int32, ventaRelacion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdLoteProducto_Param, DbType.Int32, ventaRelacion.Producto.IdLote));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdProducto_Param, DbType.Int32, ventaRelacion.Producto.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdTransaccion_Param, DbType.Int32, ventaRelacion.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IdTransaccionRelacion_Param, DbType.Int32, ventaRelacion.IdTransaccionRelacion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_IndHabilitado_Param, DbType.Boolean, ventaRelacion.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_NumeroVenta_Param, DbType.Int32, ventaRelacion.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_NumeroVentaRelacion_Param, DbType.Int32, ventaRelacion.NumeroVentaRelacion));
            parametros.Add(this.CrearParametro(Parametros.VentaProductoRelacion_UsuarioCreacion_Param, DbType.String, ventaRelacion.Usuario));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VentaProductoRelacion_Insertar, parametros));
        }

        /// <summary>
        /// Guardars the venta responsable.
        /// </summary>
        /// <param name="responsable">The responsable.</param>
        /// <returns>Retorna un entero.</returns>
        public int GuardarVentaResponsable(VentaResponsable responsable)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_CodigoEntidad_Param, DbType.String, responsable.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.DetalleVenta_IdTransaccion_Param, DbType.Int32, responsable.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_NumeroVenta_Param, DbType.Int32, responsable.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdProducto_Param, DbType.Int32, responsable.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdLote_Param, DbType.Int32, responsable.NumeroLote));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_Componente_Param, DbType.String, responsable.Componente.CodigoComponente));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdTipoComponente_Param, DbType.Int32, responsable.VenDetTicIde));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IndTipoComponente_Param, DbType.Int16, responsable.VenDetTicIndApl));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdPeriodo_Param, DbType.Int32, responsable.Honorario.IdPersonal));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdConfiguracionPeriodo_Param, DbType.Int32, responsable.Honorario.IdHonorario));
            parametros.Add(this.CrearParametro(Parametros.ResponsableVenta_IdTerceroVenta_Param, DbType.Int32, responsable.Honorario.ResponsableHonorario.IdTercero));
            return Convert.ToInt32(this.Crear(OperacionesBaseDatos.ResponsableVenta_Insertar, parametros));
        }

        /// <summary>
        /// Método para guardar información de la vinculación.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>Registro guardado.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_CodigoEntidad_Param, DbType.String, vinculacion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdAtencion_Param, DbType.Int32, vinculacion.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdContrato_Param, DbType.Int32, vinculacion.Contrato.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdPlan_Param, DbType.Int32, vinculacion.Plan.Id));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdTipoAfiliacion_Param, DbType.Int32, vinculacion.IdTipoAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IdEstado_Param, DbType.Int32, vinculacion.IdEstado));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_Orden_Param, DbType.Int16, vinculacion.Orden));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_NumeroAfiliacion_Param, DbType.String, vinculacion.NumeroAfiliacion));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_IndHabilitado_Param, DbType.Int16, vinculacion.IndHabilitado));
            parametros.Add(this.CrearParametro(Parametros.Vinculacion_MontoEjecutado_Param, DbType.Decimal, vinculacion.MontoEjecutado));
            return this.Actualizar(OperacionesBaseDatos.AtencionVinculacion_Insertar, parametros);
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_CodigoEntidad_Param, DbType.String, vinculacion.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdAtencionPadre_Param, DbType.Int32, vinculacion.IdAtencionPadre));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdAtencionVinculada_Param, DbType.Int32, vinculacion.IdAtencionVinculada));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_IdVenta_Param, DbType.Int32, vinculacion.IdTransaccion));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_NumeroVenta_Param, DbType.Int32, vinculacion.NumeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VinculacionVenta_Usuario_Param, DbType.String, vinculacion.Usuario));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.VinculacionVentas_Insertar, parametros));
        }

        /// <summary>
        /// Insertar Codigo Barras.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="codigoBarras">The codigo barras.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 09/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Insertar Codigo Barras.
        /// </remarks>
        public void InsertarCodigoBarras(int numeroFactura, byte[] codigoBarras)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("CodigoBarras", DbType.Binary, codigoBarras));
            this.Ejecutar("FacInsertarCodigoBarras", parametros);
        }

        /// <summary>
        /// Insertars the venta paquete.
        /// </summary>
        /// <param name="estadoCuenta">The estado cuenta.</param>
        /// <returns>Retorna numero de venta.</returns>
        public int InsertarVentaPaquete(EstadoCuentaEncabezado estadoCuenta)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("CodigoEntidad", DbType.String, estadoCuenta.CodigoEntidad));
            parametros.Add(this.CrearParametro("Usuario", DbType.String, estadoCuenta.Usuario));
            parametros.Add(this.CrearParametro("IdAtencion", DbType.Int32, estadoCuenta.IdAtencion));
            parametros.Add(this.CrearParametro("IdContrato", DbType.Int32, estadoCuenta.IdContrato));
            parametros.Add(this.CrearParametro("IdPlan", DbType.Int32, estadoCuenta.IdPlan));

            return Convert.ToInt32(this.Ejecutar("FACInsertarVentaPaquetes", parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("EntCod ", DbType.String, estadoCuenta.CodigoEntidad));
            parametros.Add(this.CrearParametro("VenNum", DbType.Int32, numeroVenta));

            if (pyg)
            {
                parametros.Add(this.CrearParametro("ProIde", DbType.Int32, paquete.PerdidaGanancia.Ajuste));
                parametros.Add(this.CrearParametro("VenDetCan", DbType.Decimal, paquete.Cantidad));
                parametros.Add(this.CrearParametro("VenDetCanNiv", DbType.Decimal, paquete.Cantidad));
                parametros.Add(this.CrearParametro("VenDetVal", DbType.Decimal, paquete.PerdidaGanancia.ValorPaquetePG));
                parametros.Add(this.CrearParametro("VenDetValDes", DbType.Decimal, 0));
                parametros.Add(this.CrearParametro("VenDetValPro", DbType.Decimal, paquete.PerdidaGanancia.ValorPaquetePG));
                parametros.Add(this.CrearParametro("VenDetDecIde", DbType.Int32, 0));
                parametros.Add(this.CrearParametro("VenDetValSub", DbType.Decimal, paquete.PerdidaGanancia.ValorPaquetePG));
                parametros.Add(this.CrearParametro("VenDetValOri", DbType.Decimal, paquete.PerdidaGanancia.ValorPaquetePG));
                parametros.Add(this.CrearParametro("VenDetValDesOri", DbType.Decimal, 0));
            }
            else
            {
                parametros.Add(this.CrearParametro("ProIde", DbType.Int32, paquete.IdPaquete));
                parametros.Add(this.CrearParametro("VenDetCan", DbType.Decimal, paquete.Cantidad));
                parametros.Add(this.CrearParametro("VenDetCanNiv", DbType.Decimal, paquete.Cantidad));
                parametros.Add(this.CrearParametro("VenDetVal", DbType.Decimal, paquete.ValorPaquete));
                parametros.Add(this.CrearParametro("VenDetValDes", DbType.Decimal, paquete.ValorDescuento));
                parametros.Add(this.CrearParametro("VenDetValPro", DbType.Decimal, paquete.ValorPaquete));
                parametros.Add(this.CrearParametro("VenDetDecIde", DbType.Decimal, paquete.ProductoPaquete.Descuentos.FirstOrDefault() != null ? paquete.ProductoPaquete.Descuentos.FirstOrDefault().Id : 0));
                parametros.Add(this.CrearParametro("VenDetValOri", DbType.Decimal, paquete.ValorPaquete));
                parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, int.Parse(estadoCuenta.NumeroFacturaSinPrefijo)));
            }

            return Convert.ToInt32(this.Ejecutar("FACInsertarVentaPaquetesDetalle", parametros));
        }

        /// <summary>
        /// Consulta datos de un cliente.
        /// </summary>
        /// <param name="identificadorCliente">The id cliente.</param>
        /// <returns>Objeto de tipo cliente.</returns>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 09/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ObtenerCliente(int identificadorCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Cliente_IdCliente_Param, DbType.Int32, identificadorCliente));
            return this.Consultar(OperacionesBaseDatos.ObtenerCliente, parametros);
        }

        /// <summary>
        /// Obtener Facturas PyG.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Retorna string.</returns>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 09/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public string ObtenerFacturasPyG(int numeroFactura)
        {
            StringBuilder retorno = new StringBuilder();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            DataSet dt = (DataSet)this.EjecutarProcedimientoTipo2DS("FACObtenerFacturasPyG", parametros);

            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in dt.Tables[0].Rows)
                {
                    retorno.AppendFormat("{0},", fila["NumeroFactura"].ToString());
                }
            }
            ////Remueve la ultima coma
            retorno = retorno.Remove(retorno.Length - 1, 1);
            return retorno.ToString();
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
            DataTable dt = this.Consultar("FACObtenerIdModulo");

            return Convert.ToInt32(dt.Rows[0][0].ToString());
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
            DataTable dt = this.Consultar("FACObtenerIdTipoMovimiento");

            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// Obteners the reimpresion factura.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="agrupada">if set to <c>true</c> [agrupada].</param>
        /// <returns>Retorna un DataTable.</returns>
        public DataTable ObtenerReimpresionFactura(int numeroFactura, bool agrupada)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            parametros.Add(this.CrearParametro("EsAgrupada", DbType.Boolean, agrupada));
            return this.Consultar("FACObtenerReimpresionFactura", parametros);
        }

        /// <summary>
        /// Consulta datos de un tercero.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 09/01/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ObtenerTercero(int identificadorTercero)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Facturacion_IdTercero_Param, DbType.Int32, identificadorTercero));
            return this.Consultar(OperacionesBaseDatos.ObtenerTercero, parametros);
        }

        /// <summary>
        /// Obteners the tipos componentes.
        /// </summary>
        /// <returns>Retorna una lista.</returns>
        public List<string> ObtenerTiposComponentes()
        {
            List<string> listRetorno = new List<string>();

            DataTable tmpTabla = this.Consultar("FACObtenerTiposComponentes") as DataTable;
            if (tmpTabla.Rows.Count > 0)
            {
                foreach (DataRow fila in tmpTabla.Rows)
                {
                    listRetorno.Add(fila[0].ToString().Trim());
                }
            }

            return listRetorno;
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
            List<int> resultado = new List<int>();

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimientoFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));

            DataTable tabla = this.Consultar(OperacionesBaseDatos.AnulacionFactura_ObtenerVentasPorFactura, parametros);

            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(Convert.ToInt32(fila[0].ToString()));
            }

            return resultado;
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoEntidad_Param, DbType.String, notaCredito.CodigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_Consecutivo_Param, DbType.String, notaCredito.Consecutivo));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimiento_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro("CodigoMovimiento", DbType.String, notaCredito.CodigoMovimiento));
            parametros.Add(this.CrearParametro("IdNumeroNotaCredito", DbType.Int32, notaCredito.IdNumeroNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_PrefijoNotaCredito_Param, DbType.String, notaCredito.PrefijoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdMovimientoDocumento_Param, DbType.Int32, notaCredito.IdMovimientoDocumento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_DocumentoResultado_Param, DbType.String, notaCredito.DocumentoResultado));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IndReenvio_Param, DbType.Int16, notaCredito.IndReenvio));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_FechaNota_Param, DbType.DateTime, notaCredito.FechaNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdTipoMovimientoFactura_Param, DbType.Int32, notaCredito.IdTipoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoMovimientoFactura_Param, DbType.String, notaCredito.CodigoMovimiento));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_NumeroFactura_Param, DbType.Int32, notaCredito.NumeroFactura));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_CodigoUsuario_Param, DbType.String, notaCredito.CodigoUsuario));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_TipoNotaCredito_Param, DbType.String, notaCredito.TipoNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_MotivoNota_Param, DbType.String, notaCredito.MotivoNota));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_ValorAjuste_Param, DbType.Decimal, notaCredito.ValorAjuste));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdGlosa_Param, DbType.Int32, notaCredito.IdGlosa));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCuenta_Param, DbType.Int32, notaCredito.IdCuenta));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdCastigoCausal_Param, DbType.Int32, notaCredito.IdCastigoCausal));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdNotaCredito_Param, DbType.Int32, notaCredito.IdNotaCredito));
            parametros.Add(this.CrearParametro(Parametros.NotaCredito_IdSedeNota_Param, DbType.Int32, notaCredito.IdSedeNota));

            this.Ejecutar("FACProcesoComplementarioAnulacionNC", parametros);
        }

        /// <summary>
        /// Redondeo Country.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <returns>Configuracion redondeo.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias. 
        /// FechaDeCreacion: 21/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Redondeo Country.
        /// </remarks>
        public DataTable RedondeoCountry(int contrato)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("CosConIde", DbType.Int32, contrato));
            return this.Consultar("uspFACRedondeoXContrato", parametros);
        }

        /// <summary>
        /// Redondeo Country.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <param name="identificadorManual">identificador del manual</param>
        /// <returns>Configuración Redondeo.</returns>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino- INTERGRUPO\gocampo
        /// FechaDeCreacion: 21/08/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public DataTable RedondeoCountryxID(int contrato, int identificadorManual)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("CosConIde", DbType.Int32, contrato));
            parametros.Add(this.CrearParametro("CosTarManCod", DbType.Int32, identificadorManual));
            return this.Consultar("uspFACRedondeoXContratoID", parametros);
        }

        /// <summary>
        /// Valida si la atencion esta bloqueada.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <returns>Direccion IP.</returns>
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
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturacionActividades_Atencion_Param, DbType.Int32, identificadorAtencion));
            return Convert.ToString(this.Ejecutar(OperacionesBaseDatos.FacturacionActividades_ValidarAtencionBloqueada, parametros));
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
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));

            return Convert.ToInt32(this.Ejecutar("FACValidarMovimientosRS", parametros));
        }

        /// <summary>
        /// Validars the naturaleza.
        /// </summary>
        /// <param name="identificadorTercero">The id tercero.</param>
        /// <returns>Retorna un string.</returns>
        public string ValidarNaturaleza(int identificadorTercero)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("idTercero", DbType.Int32, identificadorTercero));
            return Convert.ToString(this.Ejecutar("FACValidarNaturaleza", parametros));
        }

        /// <summary>
        /// Validars the rol usuario.
        /// </summary>
        /// <param name="usuario">Parámetro usuario.</param>
        /// <param name="rol">Parámetro rol.</param>
        /// <returns>Retorna un entero.</returns>
        public int ValidarRolUsuario(string usuario, string rol)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.FacturacionActividades_Usuario_Param, DbType.String, usuario));
            parametros.Add(this.CrearParametro(Parametros.FacturacionActividades_Rol_Param, DbType.String, rol));
            return Convert.ToInt32(this.Ejecutar(OperacionesBaseDatos.FacturacionActividades_ValidarRolUsuario, parametros));
        }

        /// <summary>
        /// Validars the venta terminada.
        /// </summary>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <param name="numeroVenta">The numero venta.</param>
        /// <param name="identificadorTransaccion">The identificador transaccion.</param>
        /// <param name="codigoEntidad">The codigo entidad.</param>
        /// <param name="identificadorContrato">The id contrato.</param>
        /// <returns>Retorna un DataTable.</returns>
        public DataTable ValidarVentaTerminada(int numeroFactura, int numeroVenta, int identificadorTransaccion, string codigoEntidad, int identificadorContrato)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_CodigoEntidad_Param, DbType.String, codigoEntidad));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_NumeroVenta_Param, DbType.Int32, numeroVenta));
            parametros.Add(this.CrearParametro(Parametros.VentaEstadoFactura_IdTransaccion_Param, DbType.Int32, identificadorTransaccion));
            parametros.Add(this.CrearParametro("IdContrato", DbType.Int32, identificadorContrato));
            parametros.Add(this.CrearParametro("NumeroFactura", DbType.Int32, numeroFactura));
            return this.Consultar(OperacionesBaseDatos.VentaEstadoFactura_ValidarVentaTerminada, parametros);
        }

        #endregion Metodos Publicos 
        #region Metodos Internos 

        /// <summary>
        /// Obteners the exclusiones.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Retorna DataTable.</returns>
        internal DataTable ObtenerExclusiones(ExclusionFacturaActividades exclusion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro("CodigoEntidad", DbType.String, exclusion.CodigoEntidad));
            parametros.Add(this.CrearParametro("IdContrato", DbType.Int32, exclusion.IdContrato));
            parametros.Add(this.CrearParametro("IdTercero", DbType.Int32, exclusion.IdTercero));
            parametros.Add(this.CrearParametro("IdPlan", DbType.Int32, exclusion.IdPlan));

            return this.Consultar("FACObtenerExclusiones", parametros);
        }

        #endregion Metodos Internos 

        #endregion Metodos 
    }
}