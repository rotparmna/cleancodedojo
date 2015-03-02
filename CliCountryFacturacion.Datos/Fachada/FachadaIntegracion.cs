// --------------------------------
// <copyright file="FachadaIntegracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Se encarga del llamado de la persistencia de datos relacionados con los servicios transversales.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.Fachada
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Localizacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using WSSAHIGeneral;

    /// <summary>
    /// Se encarga del llamado de la persistencia de datos relacionados con los servicios transversales.
    /// </summary>
    public class FachadaIntegracion : IDisposable
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// The client.
        /// </summary>
        private GeneralClient clienteGeneral;

        #endregion Variables 

        #endregion Declaraciones Locales 

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
        public Paginacion<ObservableCollection<Atencion>> ConsultarAtenciones(Paginacion<Atencion> paginacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarAtenciones(paginacion);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public Paginacion<ObservableCollection<Cliente>> ConsultarClientes(Paginacion<Cliente> paginacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarClientes(paginacion);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
            this.clienteGeneral = new GeneralClient();
            return this.clienteGeneral.ConsultarContratoEntidad(contrato);
        }

        /// <summary>
        /// Consultars the contratos.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de Contratos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 01/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las listas de contrator por tercero.
        /// </remarks>
        public List<Contrato> ConsultarContratos(Tercero tercero)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Contrato>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarContratos(tercero);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public Paginacion<ObservableCollection<Pais>> ConsultarDivisionPolitica(Paginacion<Pais> paginacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarDivisionPolitica(paginacion);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
        }

        /// <summary>
        /// Consultars the entidades.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Lista de entidades.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 01/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta la lista de entidades.
        /// </remarks>
        public List<Tercero> ConsultarEntidades(Tercero tercero)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Tercero>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarEntidades(tercero);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
        }

        /// <summary>
        /// Consultars the formatos.
        /// </summary>
        /// <returns>Lista de Formatos de Facturación.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Lista los formatos.
        /// </remarks>
        public List<Formato> ConsultarFormatos()
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Formato>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarFormatos();
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
        }

        /// <summary>
        /// Metodo para realizar Consulta de Listas.
        /// </summary>
        /// <param name="baseLista">The base lista.</param>
        /// <returns>Lista de Informacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public ObservableCollection<Basica> ConsultarGenListas(Basica baseLista)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarGenListas(baseLista);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
        }

        /// <summary>
        /// Consulta los grupos de producto existentes.
        /// </summary>
        /// <param name="paginacion">The grupo producto.</param>
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
        public Paginacion<ObservableCollection<GrupoProducto>> ConsultarGrupoProducto(Paginacion<GrupoProducto> paginacion, int identificadorAtencion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarGrupoProducto(paginacion, identificadorAtencion);
            return respuesta.Objeto;
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
        public ObservableCollection<Nivel> ConsultarNiveles(Nivel nivel)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarNiveles(nivel);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public ObservableCollection<Ocupacion> ConsultarOcupaciones(Ocupacion ocupacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarOcupaciones(ocupacion);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
        }

        /// <summary>
        /// Consulta los productos existentes.
        /// </summary>
        /// <param name="producto">The producto.</param>
        /// <returns>Lista los productos existentes.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 15/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los productos existentes.
        /// </remarks>
        public List<Producto> ConsultarProducto(Producto producto)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Producto>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarProducto(producto);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public Paginacion<ObservableCollection<TipoProductoCompuesto>> ConsultarProductosxUbicacion(Paginacion<TipoProductoCompuesto> paginacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarProductosxUbicacion(paginacion);
            if (respuesta.Ejecuto == false)
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public List<Seccion> ConsultarSecciones(Seccion seccion)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Seccion>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarSecciones(seccion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public ObservableCollection<Sede> ConsultarSede(Sede sede)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarSede(sede);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public Paginacion<ObservableCollection<Tercero>> ConsultarTercero(Paginacion<Tercero> paginacion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarTercero(paginacion);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public ObservableCollection<TipoDocumento> ConsultarTipoDocumento(TipoDocumento tipoDocumento)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarTipoDocumento(tipoDocumento);
            if (!string.IsNullOrEmpty(respuesta.Mensaje))
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
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
        public List<TipoFactura> ConsultarTipoFactura()
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<TipoFactura>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarTipoFactura();
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public List<TipoProducto> ConsultarTipoProducto(TipoProducto tipoProducto, int identificadorAtencion)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<TipoProducto>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarTipoProducto(tipoProducto, identificadorAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public Paginacion<ObservableCollection<TipoProductoCompuesto>> ConsultarTipoProductoCompuesto(Paginacion<TipoProductoCompuesto> paginacion, int identificadorAtencion)
        {
            this.clienteGeneral = new GeneralClient();
            var respuesta = this.clienteGeneral.ConsultarTipoProductoCompuesto(paginacion, identificadorAtencion);
            if (respuesta.Ejecuto == false)
            {
                throw new Exception(respuesta.Mensaje);
            }

            return respuesta.Objeto;
        }

        /// <summary>
        /// Consultars the tipo atencion.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista los tipos de atención.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de atención.
        /// </remarks>
        public List<TipoAtencion> ConsultarTiposAtencion(TipoAtencion tipoAtencion)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<TipoAtencion>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarTiposAtencion(tipoAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
        }

        /// <summary>
        /// Consultars the tipos movimiento.
        /// </summary>
        /// <param name="tipoMovimiento">The tipo movimiento.</param>
        /// <returns>Lista de consulta de movimiento.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 01/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta los tipos de movimiento.
        /// </remarks>
        public List<TipoMovimiento> ConsultarTiposMovimiento(TipoMovimiento tipoMovimiento)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<TipoMovimiento>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarTiposMovimiento(tipoMovimiento);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
        }

        /// <summary>
        /// Consultars the ubicaciones.
        /// </summary>
        /// <param name="tipoAtencion">The tipo atencion.</param>
        /// <returns>Lista las ubicaciones.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano.
        /// FechaDeCreacion: (30/01/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Consulta las ubicaciones filtradas por tipo atención.
        /// </remarks>
        public List<Ubicacion> ConsultarUbicaciones(TipoAtencion tipoAtencion)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<ObservableCollection<Ubicacion>> resultado = null;

            try
            {
                resultado = this.clienteGeneral.ConsultarUbicaciones(tipoAtencion);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto.ToList();
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
        public int CrearResponsableFactura(string codigoUsuario, int numeroFactura)
        {
            this.clienteGeneral = new GeneralClient();
            Resultado<int> resultado = null;

            try
            {
                resultado = this.clienteGeneral.CrearResponsableFactura(codigoUsuario, numeroFactura);
                resultado.Ejecuto = true;
            }
            catch (Exception ex)
            {
                resultado.Ejecuto = false;
                throw new Exception(ex.Message.ToString(), ex);
            }

            return resultado.Objeto;
        }

        /// <summary>
        /// Metodo de Limpiar Memoria.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 27/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void Dispose()
        {
            if (this.clienteGeneral != null)
            {
                this.clienteGeneral.Close();
            }

            GC.SuppressFinalize(true);
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}