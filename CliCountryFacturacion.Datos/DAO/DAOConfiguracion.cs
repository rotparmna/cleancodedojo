// --------------------------------
// <copyright file="DAOConfiguracion.cs" company="InterGrupo S.A.">
// COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>DAO Configuracion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using CliCountry.Facturacion.Datos.Recursos;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.SAHI.Comun.AccesoDatos;
    using CliCountry.SAHI.Comun.Aspectos;
    using CliCountry.SAHI.Comun.AuditoriaBase;
    using CliCountry.SAHI.Comun.Interfaces.DAO;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Datos.DAO.this.daoConfiguracion.
    /// </summary>
    public class DAOConfiguracion : OperacionesBase
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Datos.DAO.DAOConfiguracion"/>
        /// </summary>
        /// <param name="nombreConexion">The nombre conexion.</param>
        public DAOConfiguracion(string nombreConexion)
            : base(nombreConexion)
        {
        }

        #endregion Constructores 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Consulta descuentos configuración.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado descuentos.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (04/09/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultaDescuentoConfiguracion(Paginacion<DescuentoConfiguracion> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_Plan_Param, DbType.String, paginacion.Item.NombrePlan));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_TipServ_Param, DbType.String, paginacion.Item.NombreServicio));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_Atencion_Param, DbType.String, paginacion.Item.NombreTipoAtencion));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_IdAtencion_Param, DbType.String, paginacion.Item.IdAtencion));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_TipProd_Param, DbType.String, paginacion.Item.NombreTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguraicon_GruProd_Param, DbType.String, paginacion.Item.NombreGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_Prod_Param, DbType.String, paginacion.Item.NombreProducto));
            parametros.Add(this.CrearParametro(Parametros.DescuentoConfiguracion_TipCom_Param, DbType.String, paginacion.Item.NombreTipoComponente));
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdDescuento_Param, DbType.Int32, paginacion.Item.Id));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTercero_Param, DbType.Int32, paginacion.Item.IdTercero));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdContrato_Param, DbType.Int32, paginacion.Item.IdContrato));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdPlan_Param, DbType.Int32, paginacion.Item.IdPlan));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoProducto_Param, DbType.Int32, paginacion.Item.IdTipoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdGrupoProducto_Param, DbType.Int32, paginacion.Item.IdGrupoProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdProducto_Param, DbType.Int32, paginacion.Item.IdProducto));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_CodigoComponente_Param, DbType.String, paginacion.Item.Componente));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_IdTipoRelacion_Param, DbType.Int16, paginacion.Item.IdTipoRelacion));
            parametros.Add(this.CrearParametro(Parametros.Descuentos_Activo_Param, DbType.Int16, paginacion.Item.IndicadorActivo));
            return this.Consultar(OperacionesBaseDatos.DescuentoConfiguracion_Consultar, parametros);
        }

        /// <summary>
        /// Consultar componente por producto.
        /// </summary>
        /// <param name="codigoTipo">The codigo tipo.</param>
        /// <returns>
        /// Resultado tabla BILTIPOSCOMPONENTES.
        /// </returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero.
        /// FechaDeCreacion: 05/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarComponentePorProdcuto(string codigoTipo)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.Componente_TipoProducto_Param, DbType.String, codigoTipo));
            return this.Consultar(OperacionesBaseDatos.Componente_ConsultarPorTipoProducto, parametros);
        }

        /// <summary>
        /// Consulta los servicios.
        /// </summary>
        /// <returns>Listado de servicios.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (28/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarServicios()
        {
            return this.Consultar(OperacionesBaseDatos.Servicio_Consultar);
        }

        /// <summary>
        /// Consultar tipo componente.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Tipo de componente.
        /// </returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (16/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarTipoComponente(Paginacion<TipoComponente> paginacion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.General_NumeroPagina_Param, DbType.Int32, paginacion.PaginaActual));
            parametros.Add(this.CrearParametro(Parametros.General_CantidadRegistrosPagina_Param, DbType.Int32, paginacion.LongitudPagina));
            parametros.Add(this.CrearParametro(Parametros.TipoComponente_Clasificacion_Param, DbType.String, paginacion.Item.Clasificacion));
            return this.Consultar(OperacionesBaseDatos.TipoComponente_Consultar, parametros);
        }

        /// <summary>
        /// Consultar Tipo Atenciones.
        /// </summary>
        /// <returns>Datos tabla admAtencionTipo.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (05/09/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultaTipoAtenciones()
        {
            return this.Consultar(OperacionesBaseDatos.TipoAtencion_Consultar);
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}