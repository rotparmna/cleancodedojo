// --------------------------------
// <copyright file="ConfiguracionNegocio.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Configuracion Negocio.</summary>
// --------------------------------
namespace CliCountry.Facturacion.Negocio.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using CliCountry.Facturacion.Datos.Fachada;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.Facturacion.Negocio.Comun.Recursos;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;
    using Excepcion = CliCountry.SAHI.Comun.Excepciones;

    /// <summary>
    /// Clase CliCountry.Facturacion.Negocio.Base.ConfiguracionNegocio.
    /// </summary>
    public class ConfiguracionNegocio
    {
        #region Metodos 

        #region Metodos Publicos Estaticos 

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
        public static Paginacion<List<DescuentoConfiguracion>> ConsultaDescuentoConfiguracion(Paginacion<DescuentoConfiguracion> paginacion)
        {
            FachadaConfiguracion fachada = new FachadaConfiguracion();
            var resultado = fachada.ConsultaDescuentoConfiguracion(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultar componente por producto.
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
        public static List<TipoComponente> ConsultarComponentePorProdcuto(string codigoTipo)
        {
            FachadaConfiguracion fachada = new FachadaConfiguracion();
            return fachada.ConsultarComponentePorProdcuto(codigoTipo);
        }

        /// <summary>
        /// Consultar Tipo Componente.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Tipo de componente.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (16/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static Paginacion<List<TipoComponente>> ConsultarTipoComponente(Paginacion<TipoComponente> paginacion)
        {
            FachadaConfiguracion fachada = new FachadaConfiguracion();
            var resultado = fachada.ConsultarTipoComponente(paginacion);
            resultado.LongitudPagina = paginacion.LongitudPagina;
            resultado.PaginaActual = paginacion.PaginaActual;
            return resultado;
        }

        /// <summary>
        /// Consultar servicios.
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
        public static List<Servicio> ConsultaServicios()
        {
            FachadaConfiguracion fachada = new FachadaConfiguracion();
            return fachada.ConsultarServicios();
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
        public static List<TipoAtencion> ConsultaTipoAtenciones()
        {
            FachadaConfiguracion fachada = new FachadaConfiguracion();
            return fachada.ConsultaTipoAtenciones();
        }

        #endregion Metodos Publicos Estaticos 

        #endregion Metodos 
    }
}