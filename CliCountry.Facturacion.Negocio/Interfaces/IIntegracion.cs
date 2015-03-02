// --------------------------------
// <copyright file="IIntegracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Interfaz IIntegracion.</summary>
// --------------------------------
namespace CliCountry.Facturacion.Negocio.Interfaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ServiceModel;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Localizacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Interfaz IIntegracion.
    /// </summary>
    public interface IIntegracion
    {
        #region Metodos 

        /// <summary>
        /// Metodo para realizar consulta de Atenciones.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de Atenciones.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<Atencion>>> ConsultarAtenciones(Paginacion<Atencion> paginacion);

        /// <summary>
        /// Metodo Para Consultar Clientes Paginados.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Clientes Paginados.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<Cliente>>> ConsultarClientes(Paginacion<Cliente> paginacion);

        /// <summary>
        ///  Consulta los contratos.
        /// </summary>
        /// <param name="contrato">The contrato.</param>
        /// <returns>Listado de contratos.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias.
        /// FechaDeCreacion: 30/08/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los contratos.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<ContratoEntidad>>> ConsultarContratoEntidad(Paginacion<ContratoEntidad> contrato);

        /// <summary>
        /// Consultars the contratos.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de Contratos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los contratos filtrados por el tercero.
        /// </remarks>
        Resultado<List<Contrato>> ConsultarContratos(Tercero tercero);

        /// <summary>
        /// Metodo para consultar Division Politica.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Pais, Departamento, Ciudad.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 26/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<Pais>>> ConsultarDivisionPolitica(Paginacion<Pais> paginacion);

        /// <summary>
        /// Consulta las entidades.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de las entidades.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las entidades.
        /// </remarks>
        Resultado<List<Tercero>> ConsultarEntidades(Tercero tercero);

        /// <summary>
        /// Consultar los formatos.
        /// </summary>
        /// <returns>Metodo para realizar la consulta del Formato.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar los formatos.
        /// </remarks>
        Resultado<List<Formato>> ConsultarFormatos();

        /// <summary>
        /// Metodo para Consultar Lista Generales.
        /// </summary>
        /// <param name="baseLista">The base lista.</param>
        /// <returns>
        /// Informacion Basica.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<ObservableCollection<Basica>> ConsultarGenListas(Basica baseLista);

        /// <summary>
        /// Consulta los grupos de producto existentes.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lista de los grupos de producto existentes.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los grupos de producto existentes.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<GrupoProducto>>> ConsultarGrupoProducto(Paginacion<GrupoProducto> paginacion, int identificadorAtencion);

        /// <summary>
        /// Metodo para consultar Nivels.
        /// </summary>
        /// <param name="nivel">The nivel.</param>
        /// <returns>Lista de Niveles.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<ObservableCollection<Nivel>> ConsultarNiveles(Nivel nivel);

        /// <summary>
        /// Metodo para consultar Ocupaciones.
        /// </summary>
        /// <param name="ocupacion">The ocupacion.</param>
        /// <returns>
        /// Lista de Ocupaciones.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<ObservableCollection<Ocupacion>> ConsultarOcupaciones(Ocupacion ocupacion);

        /// <summary>
        /// Consulta los productos existentes.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Lista de los productos existentes.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los productos existentes.
        /// </remarks>
        Resultado<List<Producto>> ConsultarProducto(Producto producto);

        /// <summary>
        /// Metodo para realizar la consulta de Productos x Ubicacion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de Productos paginado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> ConsultarProductosxUbicacion(Paginacion<TipoProductoCompuesto> paginacion);

        /// <summary>
        /// Consultars the secciones.
        /// </summary>
        /// <param name="seccion">The seccion.</param>
        /// <returns>Lista de secciones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista las secciones.
        /// </remarks>
        Resultado<List<Seccion>> ConsultarSecciones(Seccion seccion);

        /// <summary>
        /// Metodo para consultar Sedes.
        /// </summary>
        /// <param name="sede">The Sedes.</param>
        /// <returns>
        /// Lista de Sedes.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<ObservableCollection<Sede>> ConsultarSede(Sede sede);

        /// <summary>
        /// Metodo para consultar el tercero.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Tercero paginados.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<Tercero>>> ConsultarTercero(Paginacion<Tercero> paginacion);

        /// <summary>
        /// Metodo para consultar Tipos de Documentos.
        /// </summary>
        /// <param name="tipoDocumento">The tipo documento.</param>
        /// <returns>
        /// Lista de Tipos de Documentos.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<ObservableCollection<TipoDocumento>> ConsultarTipoDocumento(TipoDocumento tipoDocumento);

        /// <summary>
        /// Consulta los tipos de facturacion existentes.
        /// </summary>
        /// <returns>Listado de Tipo de Factura.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<List<TipoFactura>> ConsultarTipoFactura();

        /// <summary>
        /// Consulta los tipos de producto existentes.
        /// </summary>
        /// <param name="tipoProducto">The tipo producto.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lista de los tipos de producto existentes.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de producto existentes.
        /// </remarks>
        Resultado<List<TipoProducto>> ConsultarTipoProducto(TipoProducto tipoProducto, int identificadorAtencion);

        /// <summary>
        /// Consulta general de tipos y grupos de producto.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> ConsultarTipoProductoCompuesto(Paginacion<TipoProductoCompuesto> paginacion, int identificadorAtencion);

        /// <summary>
        /// Consultars the tipo atencion.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista de tipos de atención.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de atencion.
        /// </remarks>
        Resultado<List<TipoAtencion>> ConsultarTiposAtencion(TipoAtencion tipoAtencion);

        /// <summary>
        /// Lista los tipos de movimiento.
        /// </summary>
        /// <param name="tipoMovimiento">The tipo movimiento.</param>
        /// <returns>Tipos de Movimientos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista los tipos de movimiento.
        /// </remarks>
        Resultado<List<TipoMovimiento>> ConsultarTiposMovimiento(TipoMovimiento tipoMovimiento);

        /// <summary>
        /// Consulta las ubicaciones filtrados por tipo atencion.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista de Ubicaciones.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las ubicaciones filtrados por tipo atencion.
        /// </remarks>
        Resultado<List<Ubicacion>> ConsultarUbicaciones(TipoAtencion tipoAtencion);

        /// <summary>
        /// Crear responsable de una factura para trazabilidad.
        /// </summary>
        /// <param name="codigoUsuario">The codigo usuario.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>0 si no se se puede crear el responsable.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 24/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<int> CrearResponsableFactura(string codigoUsuario, int numeroFactura);

        #endregion Metodos 
    }
}
