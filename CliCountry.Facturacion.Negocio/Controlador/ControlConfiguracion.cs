// --------------------------------
// <copyright file="ControlConfiguracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Archivo ControlConfiguracion.</summary>
// --------------------------------

namespace CliCountry.Facturacion.Negocio.Controlador
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.Facturacion.Negocio.Base;
    using CliCountry.Facturacion.Negocio.Interfaces;
    using CliCountry.SAHI.Comun.Aspectos;
    using CliCountry.SAHI.Comun.AuditoriaBase;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Negocio.Controlador.ControlConfiguracion.
    /// </summary>
    public class ControlConfiguracion : IConfiguracion
    {
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
        [OperationContract]
        public Resultado<Paginacion<List<DescuentoConfiguracion>>> ConsultaDescuentoConfiguracion(Paginacion<DescuentoConfiguracion> paginacion)
        {
            Resultado<Paginacion<List<DescuentoConfiguracion>>> resultado = new Resultado<Paginacion<List<DescuentoConfiguracion>>>();
            try
            {
                resultado.Ejecuto = true;
                resultado.Objeto = ConfiguracionNegocio.ConsultaDescuentoConfiguracion(paginacion);
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
        /// Consultar componente por código.
        /// </summary>
        /// <param name="codigoTipo">The codigo tipo.</param>
        /// <returns>Listado componentes.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero.
        /// FechaDeCreacion: 05/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        [OperationContract]
        public Resultado<List<TipoComponente>> ConsultarComponentePorProdcuto(string codigoTipo)
        {
            Resultado<List<TipoComponente>> resultado = new Resultado<List<TipoComponente>>();
            try
            {
                resultado.Ejecuto = true;
                resultado.Objeto = ConfiguracionNegocio.ConsultarComponentePorProdcuto(codigoTipo);
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
        /// Consultar servicios.
        /// </summary>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (28/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        [OperationContract]
        public Resultado<List<Servicio>> ConsultarServicios()
        {
            Resultado<List<Servicio>> resultado = new Resultado<List<Servicio>>();
            try
            {
                resultado.Ejecuto = true;
                resultado.Objeto = ConfiguracionNegocio.ConsultaServicios();
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
        /// Consultar Tipo Componente.
        /// </summary>
        /// <param name="paginacion">Paginacion componentes.</param>
        /// <returns>Listado de componentes.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (16/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        [OperationContract]
        public Resultado<Paginacion<List<TipoComponente>>> ConsultarTipoComponente(Paginacion<TipoComponente> paginacion)
        {
            Resultado<Paginacion<List<TipoComponente>>> resultado = new Resultado<Paginacion<List<TipoComponente>>>();
            try
            {
                resultado.Ejecuto = true;
                resultado.Objeto = ConfiguracionNegocio.ConsultarTipoComponente(paginacion);
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
        [OperationContract]
        public Resultado<List<TipoAtencion>> ConsultaTipoAtenciones()
        {
            Resultado<List<TipoAtencion>> resultado = new Resultado<List<TipoAtencion>>();
            try
            {
                resultado.Ejecuto = true;
                resultado.Objeto = ConfiguracionNegocio.ConsultaTipoAtenciones();
            }
            catch (Exception exception)
            {
                resultado.Ejecuto = false;
                resultado.Mensaje = exception.Message;
                resultado.Objeto = null;
            }

            return resultado;
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}
