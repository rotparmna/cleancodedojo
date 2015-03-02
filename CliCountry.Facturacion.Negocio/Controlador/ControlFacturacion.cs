// --------------------------------
// <copyright file="ControlFacturacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Archivo ControlConfiguracion.</summary>
// --------------------------------
namespace CliCountry.Facturacion.Negocio.Controlador
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using CliCountry.Facturacion.Negocio.Interfaces;
    using Dominio.Entidades;
    using SAHI.Comun.Aspectos;
    using SAHI.Comun.AuditoriaBase;
    using SAHI.Comun.Utilidades;
    using SAHI.Dominio.Entidades;
    using SAHI.Dominio.Entidades.Facturacion;
    using SAHI.Dominio.Entidades.Facturacion.Auditoria;
    using SAHI.Dominio.Entidades.Facturacion.Ventas;
    using SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Negocio.Controlador.ControlFacturacion.
    /// </summary>
    public class ControlFacturacion : IFacturacion
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// The facturacion.
        /// </summary>
        private Negocio.Base.Facturacion facturacion;

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para actualizar la atencion.
        /// </summary>
        /// <param name="atencionCliente">The atencion cliente.</param>
        /// <returns>Indica el resultado de la actualizacion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 25/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> ActualizarAtencion(AtencionCliente atencionCliente)
        {
            Resultado<bool> resultado = new Resultado<bool>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarAtencion(atencionCliente);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Actualiza la venta con el usuario para bloquear o desbloquear la atencion.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Retorna un objeto.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/05/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> ActualizarBloquearAtencion(int identificadorAtencion, string usuario)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarBloquearAtencion(identificadorAtencion, usuario);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para actualizar los conceptos de Cobro.
        /// </summary>
        /// <param name="conceptoCobro">The concepto cobro.</param>
        /// <returns>Indica si se realiza la actualizaci n.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 14/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> ActualizarConceptosCobro(FacturaAtencionConceptoCobro conceptoCobro)
        {
            Resultado<bool> resultado = new Resultado<bool>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarConceptosCobro(conceptoCobro);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Actualiza la informacion de la condicion de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Id condicion cubrimiento actualizado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> ActualizarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarCondicionCubrimiento(condicionCubrimiento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para realizar la actualizaci n de Tarifa.
        /// </summary>
        /// <param name="condicionTarifa">The condicion tarifa.</param>
        /// <returns>Indica si se realiza la actualizaci n.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> ActualizarCondicionTarifa(CondicionTarifa condicionTarifa)
        {
            Resultado<bool> resultado = new Resultado<bool>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarCondicionTarifa(condicionTarifa);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            return resultado;
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
        public Resultado<int> ActualizarCubrimientos(Cubrimiento cubrimiento)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarCubrimientos(cubrimiento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para realizar la actualizacion del descuento.
        /// </summary>
        /// <param name="descuento">The descuentos.</param>
        /// <returns>Indica si se afecto el registro en la base de datos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 29/05/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> ActualizarDescuento(DescuentoConfiguracion descuento)
        {
            Resultado<bool> resultado = new Resultado<bool>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Objeto = this.facturacion.ActualizarDescuento(descuento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo Para Actualizar el Estado de proceso.
        /// </summary>
        /// <param name="identificadorProcesoPar">The id proceso par.</param>
        /// <param name="identificadorEstadoPar">The id estado par.</param>
        /// <returns>
        /// Indica Si Se Actualiza el estado de proceso.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 03/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> ActualizarEstadoProcesoFactura(int identificadorProcesoPar, byte identificadorEstadoPar)
        {
            Resultado<bool> resultado = new Resultado<bool>();

            ////if (cantidad > 0)
            ////{
            ProcesoFactura procesoFactura = new ProcesoFactura() { IdProceso = identificadorProcesoPar, IdEstado = identificadorEstadoPar };
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarEstadoProcesoFactura(procesoFactura);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            ////}
            ////else {
            ////    resultado.Ejecuto = false;
            ////    resultado.Mensaje = "No hay datos para actualizar";
            ////    resultado.Objeto = false;
            ////}
            return resultado;
        }

        /// <summary>
        /// Actualiza la informacion de la exclusi n de contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de exclusi n actualizada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> ActualizarExclusionContrato(Exclusion exclusion)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarExclusionContrato(exclusion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para actualizar la vinculaci n.
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
        public Resultado<int> ActualizarVinculacion(Vinculacion vinculacion)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ActualizarVinculacion(vinculacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para consultar atencion por cliente de facturaci n actividad.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <returns>Lista de Datos tipo AtencionCliente.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public AtencionCliente ConsultarAtencionCliente(int identificadorAtencion)
        {
            this.facturacion = new Negocio.Base.Facturacion();
            return this.facturacion.ConsultarAtencionCliente(identificadorAtencion);
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
        public Resultado<List<ClaseCubrimiento>> ConsultarClasesCubrimiento(ClaseCubrimiento claseCubrimiento)
        {
            Resultado<List<ClaseCubrimiento>> resultado = new Resultado<List<ClaseCubrimiento>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarClasesCubrimiento(claseCubrimiento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Retorna la consulta de los componentes.
        /// </summary>
        /// <param name="identificadorAtencion">The id atencion.</param>
        /// <param name="identificadorTipoProducto">The id tipo producto.</param>
        /// <returns>
        /// Lista de Datos.
        /// </returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<List<TipoComponente>> ConsultarComponentes(int identificadorAtencion, int identificadorTipoProducto)
        {
            Resultado<List<TipoComponente>> resultado = new Resultado<List<TipoComponente>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarComponentes(identificadorAtencion, identificadorTipoProducto);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Permite Consultar Conceptos Asociados a una Atenci n.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de conceptos.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 04/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<List<ConceptoCobro>> ConsultarConceptos(Atencion atencion)
        {
            Resultado<List<ConceptoCobro>> resultado = new Resultado<List<ConceptoCobro>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarConceptos(atencion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo de consulta de condiciones de cubrimientos.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lsita de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 19/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<CondicionCubrimiento>>> ConsultarCondicionesCubrimiento(Paginacion<CondicionCubrimiento> paginacion)
        {
            Resultado<Paginacion<List<CondicionCubrimiento>>> resultado = new Resultado<Paginacion<List<CondicionCubrimiento>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarCondicionesCubrimiento(paginacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Permite Consultar los planes o contratos en general.
        /// </summary>
        /// <param name="paginacion">The atencion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<ContratoPlan>>> ConsultarContratoPlan(Paginacion<ContratoPlan> paginacion)
        {
            Resultado<Paginacion<List<ContratoPlan>>> resultado = new Resultado<Paginacion<List<ContratoPlan>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarContratoPlan(paginacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Retorna la consulta de los cubrimientos paginados.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 10/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<Cubrimiento>>> ConsultarCubrimientos(Paginacion<Cubrimiento> paginacion)
        {
            Resultado<Paginacion<List<Cubrimiento>>> resultado = new Resultado<Paginacion<List<Cubrimiento>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarCubrimientos(paginacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Permite Consultar Depositos Asociados a una Atenci n.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Lista de depositos.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 04/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<List<Deposito>> ConsultarDepositos(Atencion atencion)
        {
            Resultado<List<Deposito>> resultado = new Resultado<List<Deposito>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarDepositos(atencion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Consultar estado del proceso.
        /// </summary>
        /// <param name="identificadorProceso">The identificador proceso.</param>
        /// <returns>Estado del proceso.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public int ConsultarEstadoProceso(int identificadorProceso)
        {
            return new Negocio.Base.Facturacion().ConsultarEstadoProceso(identificadorProceso);
        }

        /// <summary>
        /// Metodo para filtrar exclusiones del contrato x atencion.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de exclusiones del contrato x atencion.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 30/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<Exclusion>>> ConsultarExclusionesAtencionContrato(Paginacion<Exclusion> paginacion)
        {
            Resultado<Paginacion<List<Exclusion>>> resultado = new Resultado<Paginacion<List<Exclusion>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarExclusionesAtencionContrato(paginacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Permite Consultar las exclusiones aplicadas en una tarifa.
        /// </summary>
        /// <param name="tarifaExclusion">The tarifa exclusion.</param>
        /// <returns>Lista de datos.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 14/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Permite Consultar las exclusiones aplicadas en una tarifa.
        /// </remarks>
        public Resultado<List<ExclusionManual>> ConsultarExclusionesManual(ExclusionManual tarifaExclusion)
        {
            Resultado<List<ExclusionManual>> resultado = new Resultado<List<ExclusionManual>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarExclusionesManual(tarifaExclusion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo Para Consultar la informacion basica del tercero.
        /// </summary>
        /// <returns>Informacion BAsica Tercero.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<InformacionBasicaTercero> ConsultarInformacionBasicaTercero()
        {
            Resultado<InformacionBasicaTercero> resultado = new Resultado<InformacionBasicaTercero>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarInformacionBasicaTercero();
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
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
            this.facturacion = new Negocio.Base.Facturacion();
            return this.facturacion.ConsultarInformacionFactura(identificadorProceso, identificadorTipoMovimiento, identificadorTipoFacturacion);
        }

        /// <summary>
        /// Consulta las Tablas  Maestras.
        /// </summary>
        /// <param name="identificadorMaestra">The id maestra.</param>
        /// <param name="identificadorPagina">The id pagina.</param>
        /// <returns>
        /// Lista de los productos existentes.
        /// </returns>
        /// <remarks>
        /// Autor: Alex Mattos - INTERGRUPO\amattos.
        /// FechaDeCreacion: 04/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: Consulta las Tablas  Maestras.
        /// </remarks>
        public Resultado<List<Maestras>> ConsultarMaestras(int identificadorMaestra, int identificadorPagina)
        {
            Resultado<List<Maestras>> resultado = new Resultado<List<Maestras>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Objeto = this.facturacion.ConsultarMaestras(identificadorMaestra, identificadorPagina);
                resultado.Ejecuto = true;
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo de Consultar tarifas.
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
        public Resultado<Paginacion<List<ManualesBasicos>>> ConsultarManualesBasicos(Paginacion<ManualesBasicos> manuales)
        {
            Resultado<Paginacion<List<ManualesBasicos>>> resultado = new Resultado<Paginacion<List<ManualesBasicos>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarManualesBasicos(manuales);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

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
        public Resultado<DataTable> ConsultarManualesBasicosContrato(int identificadorContrato)
        {
            Resultado<DataTable> resultado = new Resultado<DataTable>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarManualesBasicosContrato(identificadorContrato);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

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
        public Resultado<List<Plan>> ConsultarPlanes(Contrato contrato)
        {
            Resultado<List<Plan>> resultado = new Resultado<List<Plan>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarPlanes(contrato);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo de Consultar tarifas.
        /// </summary>
        /// <param name="tarifas">The tarifas.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: alex David Mattos rodriguez - INTERGRUPO\amattos.
        /// FechaDeCreacion: 05/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<CondicionTarifa>>> ConsultarTarifas(Paginacion<CondicionTarifa> tarifas)
        {
            Resultado<Paginacion<List<CondicionTarifa>>> resultado = new Resultado<Paginacion<List<CondicionTarifa>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarTarifas(tarifas);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
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
            if (this.facturacion == null)
            {
                this.facturacion = new Base.Facturacion();
            }

            return this.facturacion.ConsultarTerceroComponente(identificadorAtencion);
        }

        /// <summary>
        /// Permite Consultar las ventas para facturacion por actividad.
        /// </summary>
        /// <param name="venta">The id venta.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<Venta>>> ConsultarVentasAtencion(Paginacion<Venta> venta)
        {
            Resultado<Paginacion<List<Venta>>> resultado = new Resultado<Paginacion<List<Venta>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultaVentasAtencion(venta);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Permite Consultar las ventas para facturacion por actividad.
        /// </summary>
        /// <param name="venta">The id venta.</param>
        /// <returns>Lista de Datos.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 02/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<Paginacion<List<Venta>>> ConsultarVentasNumeroVenta(Paginacion<Venta> venta)
        {
            Resultado<Paginacion<List<Venta>>> resultado = new Resultado<Paginacion<List<Venta>>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarVentasNumeroVenta(venta);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

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
        public Resultado<List<Vinculacion>> ConsultarVinculaciones(Vinculacion atencion)
        {
            Resultado<List<Vinculacion>> resultado = new Resultado<List<Vinculacion>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ConsultarVinculaciones(atencion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para guardar el CLiente.
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <returns>Id del Cliente.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarCliente(Cliente cliente)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarCliente(cliente);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
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
        public Resultado<int> GuardarConceptoCobro(ConceptoCobro conceptoCobro)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarConceptoCobro(conceptoCobro);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Guarda la informacion de la condicion de cubrimiento.
        /// </summary>
        /// <param name="condicionCubrimiento">The condicion cubrimiento.</param>
        /// <returns>Id condicion cubrimiento creado.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 24/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarCondicionCubrimiento(CondicionCubrimiento condicionCubrimiento)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarCondicionCubrimiento(condicionCubrimiento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para guarda tarifas.
        /// </summary>
        /// <param name="tarifas">The tarifas.</param>
        /// <returns>
        /// Id de la Tarifa.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 28/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarCondicionTarifa(CondicionTarifa tarifas)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarCondicionTarifa(tarifas);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
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
        public Resultado<int> GuardarCubrimientos(Cubrimiento cubrimiento)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarCubrimientos(cubrimiento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para almacenar los descuentos.
        /// </summary>
        /// <param name="descuento">The descuento.</param>
        /// <returns>
        /// Id del Descuento.
        /// </returns>
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio).
        /// FechaDeCreacion: (dd/MM/yyyy).
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarDescuento(DescuentoConfiguracion descuento)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                resultado.Objeto = Negocio.Base.Facturacion.GuardarDescuento(descuento);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Guarda la informacion de la exclusi n de contrato.
        /// </summary>
        /// <param name="exclusion">The exclusion.</param>
        /// <returns>Id de exclusi n insertada.</returns>
        /// <remarks>
        /// Autor: Ivan Jose Pimienta Serrano - INTERGRUPO\Ipimienta.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarExclusionContrato(Exclusion exclusion)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarExclusionContrato(exclusion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message.Replace("\'", "\"");
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que almacena la informacion de la cabezera de la factura.
        /// </summary>
        /// <param name="facturaCompuesta">The factura compuesta.</param>
        /// <returns>Estado de Cuenta Encabezado.</returns>
        /// <remarks>
        /// Autor: Jhon Alberto Falcon Arellano - INTERGRUPO\Jfalcon.
        /// FechaDeCreacion: 01/03/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<EstadoCuentaEncabezado> GuardarInformacionFacturaActividadesPaquetes(FacturaCompuesta facturaCompuesta)
        {
            Resultado<EstadoCuentaEncabezado> resultado = new Resultado<EstadoCuentaEncabezado>();

            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Objeto = this.facturacion.GuardarInformacionFacturaActividadesPaquetes(facturaCompuesta);
                resultado.Ejecuto = true;
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        /// <summary>
        /// Guardar informacion relacionada a la factura de acuerdo a la vinculacion.
        /// </summary>
        /// <param name="procesoFactura">The proceso factura.</param>
        /// <returns>
        /// Lista EstadoCuentaEncabezado.
        /// </returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm.
        /// FechaDeCreacion: 07/11/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Resultado<List<EstadoCuentaEncabezado>> GuardarInformacionProceso(ProcesoFactura procesoFactura)
        {
            Resultado<List<EstadoCuentaEncabezado>> resultado = new Resultado<List<EstadoCuentaEncabezado>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Objeto = this.facturacion.GuardarInformacionProceso(procesoFactura);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
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
        public Resultado<int> GuardarModificacionTercero(Tercero tercero)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarModificacionTercero(tercero);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para Crear El tercero.
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <returns>Id del Tercero Creado.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 07/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<int> GuardarTercero(Tercero tercero)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarTercero(tercero);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
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
        public Resultado<int> GuardarVinculacion(Vinculacion vinculacion)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarVinculacion(vinculacion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para guardar la vinculacion de la venta.
        /// </summary>
        /// <param name="vinculaciones">The vinculacion.</param>
        /// <returns>Id del la Venta Vinculada.</returns>
        /// <remarks>
        /// Autor: David Mauricio Guti rrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 11/04/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripcion: (Descripcion detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public Resultado<bool> GuardarVinculacionVentas(List<VinculacionVenta> vinculaciones)
        {
            Resultado<bool> resultado = new Resultado<bool>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.GuardarVinculacionVentas(vinculaciones);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = false;
            }

            return resultado;
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
        public Tercero ObtenerCliente(int identificadorCliente)
        {
            this.facturacion = new Negocio.Base.Facturacion();
            return this.facturacion.ObtenerCliente(identificadorCliente);
        }

        /// <summary>
        /// Metodo para aplicar el redondeo.
        /// </summary>
        /// <param name="exclusion">Entidad con los propiedades que se necesitan como parametros para hacer la consulta.</param>
        /// <returns>Lista de exclusiones.</returns>
        public Resultado<List<ExclusionFacturaActividades>> ObtenerExclusiones(ExclusionFacturaActividades exclusion)
        {
            Resultado<List<ExclusionFacturaActividades>> resultado = new Resultado<List<ExclusionFacturaActividades>>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ObtenerExclusiones(exclusion);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = new List<ExclusionFacturaActividades>();
            }

            return resultado;
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
        /// Autor: Luis Fernando Quintero - INTERGRUPO\lquinterom.
        /// FechaDeCreacion: 29/07/2014.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Obtener Responsable Venta.
        /// </remarks>
        public VentaResponsable ObtenerResponsableVentaComponentes(List<VentaResponsable> responsablesVentas, int identificadorProducto, int identificadorTransaccion, int identificadorVenta, string componente)
        {
            return this.facturacion.ObtenerResponsableVentaComponentes(responsablesVentas, identificadorProducto, identificadorTransaccion, identificadorVenta, componente);
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
            this.facturacion = new Negocio.Base.Facturacion();
            return this.facturacion.ObtenerTercero(identificadorTercero);
        }

        /// <summary>
        /// Valida si la atencion esta bloqueada.
        /// </summary>
        /// <param name="identificadorAtencion">The unique identifier atencion.</param>
        /// <returns>Retorna Validacion.</returns>
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
            if (this.facturacion == null)
            {
                this.facturacion = new Negocio.Base.Facturacion();
            }

            return this.facturacion.ValidarAtencionBloqueada(identificadorAtencion);
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
        public Resultado<int> ValidarRolUsuario(string usuario, string rol)
        {
            Resultado<int> resultado = new Resultado<int>();
            try
            {
                this.facturacion = new Negocio.Base.Facturacion();
                resultado.Ejecuto = true;
                resultado.Objeto = this.facturacion.ValidarRolUsuario(usuario, rol);
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = 0;
            }

            return resultado;
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}
