// --------------------------------
// <copyright file="ControlIntegracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Archivo ControlIntegracion.</summary>
// --------------------------------
namespace CliCountry.Facturacion.Negocio.Controlador
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CliCountry.Facturacion.Negocio.Interfaces;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Localizacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Negocio.Controlador.ControlIntegracion.
    /// </summary>
    public class ControlIntegracion : IIntegracion
    {
        #region Metodos 

        #region Metodos Publicos 

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
        public Resultado<Paginacion<ObservableCollection<Atencion>>> ConsultarAtenciones(Paginacion<Atencion> paginacion)
        {
            Resultado<Paginacion<ObservableCollection<Atencion>>> resultado = new Resultado<Paginacion<ObservableCollection<Atencion>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarAtenciones(paginacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para Consultar Clientes Paginado.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Consulta de Clientes Paginado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<Paginacion<ObservableCollection<Cliente>>> ConsultarClientes(Paginacion<Cliente> paginacion)
        {
            Resultado<Paginacion<ObservableCollection<Cliente>>> resultado = new Resultado<Paginacion<ObservableCollection<Cliente>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarClientes(paginacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<Paginacion<ObservableCollection<ContratoEntidad>>> ConsultarContratoEntidad(Paginacion<ContratoEntidad> contrato)
        {
            Resultado<Paginacion<ObservableCollection<ContratoEntidad>>> resultado = new Resultado<Paginacion<ObservableCollection<ContratoEntidad>>>();

            try
            {
                resultado = Negocio.Base.Integracion.ConsultarContratoEntidad(contrato);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consulta los contratos filtrados por el tercero.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los contratos filtrados por el tercero.
        /// </remarks>
        public Resultado<List<Contrato>> ConsultarContratos(Tercero tercero)
        {
            Resultado<List<Contrato>> resultado = new Resultado<List<Contrato>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarContratos(tercero);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar Division Politica.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>
        /// Lista de Pais, Departamento, Ciudad.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 26/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<Paginacion<ObservableCollection<Pais>>> ConsultarDivisionPolitica(Paginacion<Pais> paginacion)
        {
            Resultado<Paginacion<ObservableCollection<Pais>>> resultado = new Resultado<Paginacion<ObservableCollection<Pais>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarDivisionPolitica(paginacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consulta las entidades.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (31/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las entidades.
        /// </remarks>
        public Resultado<List<Tercero>> ConsultarEntidades(Tercero tercero)
        {
            Resultado<List<Tercero>> resultado = new Resultado<List<Tercero>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarEntidades(tercero);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consulta los formatos.
        /// </summary>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (31/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los formatos.
        /// </remarks>
        public Resultado<List<Formato>> ConsultarFormatos()
        {
            Resultado<List<Formato>> resultado = new Resultado<List<Formato>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarFormatos();
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar Tablas Basicas.
        /// </summary>
        /// <param name="baseLista">The base lista.</param>
        /// <returns>Listado de Tablas Basicas.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<ObservableCollection<Basica>> ConsultarGenListas(Basica baseLista)
        {
            Resultado<ObservableCollection<Basica>> resultado = new Resultado<ObservableCollection<Basica>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarGenListas(baseLista);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<Paginacion<ObservableCollection<GrupoProducto>>> ConsultarGrupoProducto(Paginacion<GrupoProducto> paginacion, int identificadorAtencion)
        {
            Resultado<Paginacion<ObservableCollection<GrupoProducto>>> resultado = new Resultado<Paginacion<ObservableCollection<GrupoProducto>>>();
            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarGrupoProducto(paginacion, identificadorAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar los Niveles.
        /// </summary>
        /// <param name="nivel">The nivel.</param>
        /// <returns>Listado de Niveles.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<ObservableCollection<Nivel>> ConsultarNiveles(Nivel nivel)
        {
            Resultado<ObservableCollection<Nivel>> resultado = new Resultado<ObservableCollection<Nivel>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarNiveles(nivel);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultas las ocupaciones.
        /// </summary>
        /// <param name="ocupacion">The ocupacion.</param>
        /// <returns>Listado de Ocupaciones.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<ObservableCollection<Ocupacion>> ConsultarOcupaciones(Ocupacion ocupacion)
        {
            Resultado<ObservableCollection<Ocupacion>> resultado = new Resultado<ObservableCollection<Ocupacion>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarOcupaciones(ocupacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<List<Producto>> ConsultarProducto(Producto producto)
        {
            Resultado<List<Producto>> resultado = new Resultado<List<Producto>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarProducto(producto);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para realizar la consulta de los Productos x Ubicacion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de Productos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 08/07/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> ConsultarProductosxUbicacion(Paginacion<TipoProductoCompuesto> paginacion)
        {
            Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> resultado = new Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarProductosxUbicacion(paginacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consultars the secciones.
        /// </summary>
        /// <param name="seccion">The seccion.</param>
        /// <returns>Lista de Secciones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista las secciones.
        /// </remarks>
        public Resultado<List<Seccion>> ConsultarSecciones(Seccion seccion)
        {
            Resultado<List<Seccion>> resultado = new Resultado<List<Seccion>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarSecciones(seccion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar las Sedes.
        /// </summary>
        /// <param name="sede">Parametro sede.</param>
        /// <returns>Listado de Sedes.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<ObservableCollection<Sede>> ConsultarSede(Sede sede)
        {
            Resultado<ObservableCollection<Sede>> resultado = new Resultado<ObservableCollection<Sede>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarSede(sede);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<Paginacion<ObservableCollection<Tercero>>> ConsultarTercero(Paginacion<Tercero> paginacion)
        {
            Resultado<Paginacion<ObservableCollection<Tercero>>> resultado = new Resultado<Paginacion<ObservableCollection<Tercero>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTercero(paginacion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar los tipos de documentos.
        /// </summary>
        /// <param name="tipoDocumento">The tipo documento.</param>
        /// <returns>Listado de Tipos de Documentos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<ObservableCollection<TipoDocumento>> ConsultarTipoDocumento(TipoDocumento tipoDocumento)
        {
            Resultado<ObservableCollection<TipoDocumento>> resultado = new Resultado<ObservableCollection<TipoDocumento>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTipoDocumento(tipoDocumento);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<List<TipoFactura>> ConsultarTipoFactura()
        {
            Resultado<List<TipoFactura>> resultado = new Resultado<List<TipoFactura>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTipoFactura();
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<List<TipoProducto>> ConsultarTipoProducto(TipoProducto tipoProducto, int identificadorAtencion)
        {
            Resultado<List<TipoProducto>> resultado = new Resultado<List<TipoProducto>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTipoProducto(tipoProducto, identificadorAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
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
        public Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> ConsultarTipoProductoCompuesto(Paginacion<TipoProductoCompuesto> paginacion, int identificadorAtencion)
        {
            Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>> resultado = new Resultado<Paginacion<ObservableCollection<TipoProductoCompuesto>>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTipoProductoCompuesto(paginacion, identificadorAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

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
        public Resultado<List<TipoAtencion>> ConsultarTiposAtencion(TipoAtencion tipoAtencion)
        {
            Resultado<List<TipoAtencion>> resultado = new Resultado<List<TipoAtencion>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTiposAtencion(tipoAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consulta los tipos de movimiento.
        /// </summary>
        /// <param name="tipoMovimiento">Tipo movimiento.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano.
        /// FechaDeCreacion: (31/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de movimiento.
        /// </remarks>
        public Resultado<List<TipoMovimiento>> ConsultarTiposMovimiento(TipoMovimiento tipoMovimiento)
        {
            Resultado<List<TipoMovimiento>> resultado = new Resultado<List<TipoMovimiento>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarTiposMovimiento(tipoMovimiento);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Consultars the ubicaciones.
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
        public Resultado<List<Ubicacion>> ConsultarUbicaciones(TipoAtencion tipoAtencion)
        {
            Resultado<List<Ubicacion>> resultado = new Resultado<List<Ubicacion>>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.ConsultarUbicaciones(tipoAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = null;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Crear responsable inicial de la factura.
        /// </summary>
        /// <param name="codigoUsuario">The codigo usuario.</param>
        /// <param name="numeroFactura">The numero factura.</param>
        /// <returns>Identificador del registro ingresado.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 19/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<int> CrearResponsableFactura(string codigoUsuario, int numeroFactura)
        {
            Resultado<int> resultado = new Resultado<int>();

            try
            {
                resultado.Objeto = Negocio.Base.Integracion.CrearResponsableFactura(codigoUsuario, numeroFactura);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Objeto = 0;
                resultado.Ejecuto = false;
            }

            return resultado;
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}
