// --------------------------------
// <copyright file="Integracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Archivo Integracion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Negocio.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CliCountry.Facturacion.Datos.Fachada;
    using CliCountry.Facturacion.Negocio.Comun.Recursos;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Localizacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using Excepcion = CliCountry.SAHI.Comun.Excepciones;

    /// <summary>
    /// Implementa la interfaz de operaciones relacionados con las operaciones de SAHI.
    /// </summary>
    public class Integracion
    {
        #region Metodos 

        #region Metodos Publicos Estaticos 

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
        public static Paginacion<ObservableCollection<Atencion>> ConsultarAtenciones(Paginacion<Atencion> paginacion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarAtenciones(paginacion);
        }

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
        public static Paginacion<ObservableCollection<Cliente>> ConsultarClientes(Paginacion<Cliente> paginacion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarClientes(paginacion);
        }

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
        public static Resultado<Paginacion<ObservableCollection<ContratoEntidad>>> ConsultarContratoEntidad(Paginacion<ContratoEntidad> contrato)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarContratoEntidad(contrato);
        }

        /// <summary>
        /// Consultars the contratos.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de Contratos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static List<Contrato> ConsultarContratos(Tercero tercero)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarContratos(tercero);
        }

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
        public static Paginacion<ObservableCollection<Pais>> ConsultarDivisionPolitica(Paginacion<Pais> paginacion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarDivisionPolitica(paginacion);
        }

        /// <summary>
        /// Consultars the entidades.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de Terceros.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 21/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static List<Tercero> ConsultarEntidades(Tercero tercero)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarEntidades(tercero);
        }

        /// <summary>
        /// Consultars the tipo atencion.
        /// </summary>
        /// <returns>Lista de Formatos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consultar los formatos.
        /// </remarks>
        public static List<Formato> ConsultarFormatos()
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarFormatos();
        }

        /// <summary>
        /// Metodo para realizar la Consulta de Basicas.
        /// </summary>
        /// <param name="baseLista">The base lista.</param>
        /// <returns>Informacion Basica de Listas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static ObservableCollection<Basica> ConsultarGenListas(Basica baseLista)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarGenListas(baseLista);
        }

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
        public static Paginacion<ObservableCollection<GrupoProducto>> ConsultarGrupoProducto(Paginacion<GrupoProducto> paginacion, int identificadorAtencion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarGrupoProducto(paginacion, identificadorAtencion);
        }

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
        public static ObservableCollection<Nivel> ConsultarNiveles(Nivel nivel)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarNiveles(nivel);
        }

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
        public static ObservableCollection<Ocupacion> ConsultarOcupaciones(Ocupacion ocupacion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarOcupaciones(ocupacion);
        }

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
        public static List<Producto> ConsultarProducto(Producto producto)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarProducto(producto);
        }

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
        public static Paginacion<ObservableCollection<TipoProductoCompuesto>> ConsultarProductosxUbicacion(Paginacion<TipoProductoCompuesto> paginacion)
        {
            if (paginacion.Item.ConvenioNoClinico == null)
            {
                throw new Excepcion.Negocio(ReglasNegocio.ConvenioNoClinico_NoInstanciado);
            }
            else if (paginacion.Item.GrupoProducto == null)
            {
                throw new Excepcion.Negocio(ReglasNegocio.GrupoProducto_NoInstanciado);
            }
            else if (paginacion.Item.Producto == null)
            {
                throw new Excepcion.Negocio(ReglasNegocio.Producto_NoInstanciado);
            }

            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarProductosxUbicacion(paginacion);
        }

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
        public static List<Seccion> ConsultarSecciones(Seccion seccion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarSecciones(seccion);
        }

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
        public static ObservableCollection<Sede> ConsultarSede(Sede sede)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarSede(sede);
        }

        /// <summary>
        /// Metodo para consultar el tercero.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Tercero paginados.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static Paginacion<ObservableCollection<Tercero>> ConsultarTercero(Paginacion<Tercero> paginacion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTercero(paginacion);
        }

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
        public static ObservableCollection<TipoDocumento> ConsultarTipoDocumento(TipoDocumento tipoDocumento)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTipoDocumento(tipoDocumento);
        }

        /// <summary>
        /// Consulta los tipos de facturacion existentes.
        /// </summary>
        /// <returns>Lista con los tipos de facturacion existentes.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 19/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static List<TipoFactura> ConsultarTipoFactura()
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTipoFactura();
        }

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
        public static List<TipoProducto> ConsultarTipoProducto(TipoProducto tipoProducto, int identificadorAtencion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTipoProducto(tipoProducto, identificadorAtencion);
        }

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
        public static Paginacion<ObservableCollection<TipoProductoCompuesto>> ConsultarTipoProductoCompuesto(Paginacion<TipoProductoCompuesto> paginacion, int identificadorAtencion)
        {
            if (paginacion.Item.GrupoProducto == null)
            {
                throw new Excepcion.Negocio(ReglasNegocio.GrupoProducto_NoInstanciado);
            }
            else if (paginacion.Item.Producto == null)
            {
                throw new Excepcion.Negocio(ReglasNegocio.Producto_NoInstanciado);
            }

            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTipoProductoCompuesto(paginacion, identificadorAtencion);
        }

        /// <summary>
        /// Consultars the tipos movimiento.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista los tipos de atención.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de atencion.
        /// </remarks>
        public static List<TipoAtencion> ConsultarTiposAtencion(TipoAtencion tipoAtencion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTiposAtencion(tipoAtencion);
        }

        /// <summary>
        /// Metodo para consultar tipos de movimientos.
        /// </summary>
        /// <param name="tipoMovimiento">The tipo movimiento.</param>
        /// <returns>Lisado de tipos de movimientos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static List<TipoMovimiento> ConsultarTiposMovimiento(TipoMovimiento tipoMovimiento)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarTiposMovimiento(tipoMovimiento);
        }

        /// <summary>
        /// Consultars the ubicaciones.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista las Ubicaciones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las ubicaciones filtrados por tipo atencion.
        /// </remarks>
        public static List<Ubicacion> ConsultarUbicaciones(TipoAtencion tipoAtencion)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.ConsultarUbicaciones(tipoAtencion);
        }

        /// <summary>
        /// Creación de traza para el responsable de la factura- trazabilidad factura.
        /// </summary>
        /// <param name="codigoUsuario">The codigo usuario.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>0 si no se crea el registro.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 24/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static int CrearResponsableFactura(string codigoUsuario, int numeroFactura)
        {
            FachadaIntegracion fachada = new FachadaIntegracion();
            return fachada.CrearResponsableFactura(codigoUsuario, numeroFactura);
        }

        #endregion Metodos Publicos Estaticos 
        #region Metodos Privados Estaticos 

        /// <summary>
        /// Metodo para realizar la seleccion de detalles de tarifa.
        /// </summary>
        /// <param name="detallesTarifa">The detalles tarifa.</param>
        /// <param name="identificadorProducto">The id producto.</param>
        /// <returns>Detalle de Tarifa Asociado al Producto.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static DetalleTarifa BuscarDetalleTarifa(List<DetalleTarifa> detallesTarifa, int identificadorProducto)
        {
            var resultado = from
                                item in detallesTarifa
                            where
                                item.IdProducto == identificadorProducto
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para realizar la busqueda de tarifa Unidad.
        /// </summary>
        /// <param name="tarifasUnidad">The tarifas unidad.</param>
        /// <param name="codigoUnidad">The codigo unidad.</param>
        /// <returns>Tarifa que le aplica al producto.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static TarifaUnidad BuscarTarifaUnidad(List<TarifaUnidad> tarifasUnidad, string codigoUnidad)
        {
            var resultado = from
                                item in tarifasUnidad
                            where
                                item.CodigoUnidad.Trim().Equals(codigoUnidad.Trim())
                            orderby
                                item.IndHabilitado descending,
                                item.FechaVigencia descending
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo para buscar la tasa asociada al producto.
        /// </summary>
        /// <param name="tasas">The tasas.</param>
        /// <param name="codigoTasa">The codigo tasa.</param>
        /// <returns>Tasa de Impuesto del Producto.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static Tasa BuscarTasaProducto(List<Tasa> tasas, string codigoTasa)
        {
            var resultado = from
                                item in tasas
                            where

                                // item.CodigoTasa == codigoTasa
                            item.CodigoTasa.Trim().Equals(codigoTasa.Trim())
                            select
                                item;

            return resultado.FirstOrDefault();
        }

        #endregion Metodos Privados Estaticos 

        #endregion Metodos 
    }
}