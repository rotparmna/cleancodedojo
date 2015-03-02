// --------------------------------
// <copyright file="FachadaConfiguracion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>DAO Fachada Configuracion.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.Fachada
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using CliCountry.Facturacion.Datos.DAO;
    using CliCountry.Facturacion.Datos.Recursos;
    using CliCountry.Facturacion.Dominio.Entidades;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Datos.Fachada.FachadaConfiguracion.
    /// </summary>
    public class FachadaConfiguracion
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// The DAO configuracion.
        /// </summary>
        private DAOConfiguracion daoConfiguracion = new DAOConfiguracion(OperacionesBaseDatos.ConexionFacturacion);

        #endregion Variables 

        #endregion Declaraciones Locales 

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
        public Paginacion<List<DescuentoConfiguracion>> ConsultaDescuentoConfiguracion(Paginacion<DescuentoConfiguracion> paginacion)
        {
            IEnumerable<Paginacion<DescuentoConfiguracion>> registros = null;
            using (DataTable filas = this.daoConfiguracion.ConsultaDescuentoConfiguracion(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<DescuentoConfiguracion>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<DescuentoConfiguracion>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<DescuentoConfiguracion>>();
        }

        /// <summary>
        /// Consultar Tipo Componente Lista.
        /// </summary>
        /// <param name="codigoTipo">The codigo tipo.</param>
        /// <returns>
        /// Tipos componentes.
        /// </returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero.
        /// FechaDeCreacion: 05/09/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<TipoComponente> ConsultarComponentePorProdcuto(string codigoTipo)
        {
            IEnumerable<TipoComponente> componentes = null;
            using (DataTable filas = this.daoConfiguracion.ConsultarComponentePorProdcuto(codigoTipo))
            {
                componentes = from fila in filas.Select()
                              select new TipoComponente(fila);
            }

            return componentes.ToList();
        }

        /// <summary>
        /// Consultar servicios.
        /// </summary>
        /// <returns>Listado serivios.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela.
        /// FechaDeCreacion: (28/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public List<Servicio> ConsultarServicios()
        {
            IEnumerable<Servicio> resultado = null;
            using (DataTable filas = this.daoConfiguracion.ConsultarServicios())
            {
                resultado = from fila in filas.Select()
                            select new Servicio(fila);
            }

            return resultado.ToList();
        }

        /// <summary>
        /// Consultar Tipo Componente.
        /// </summary>
        /// <param name="paginacion">The paginacion.</param>
        /// <returns>Listado de componentes.</returns>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\Aforero.
        /// FechaDeCreacion: (16/08/2013).
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public Paginacion<List<TipoComponente>> ConsultarTipoComponente(Paginacion<TipoComponente> paginacion)
        {
            IEnumerable<Paginacion<TipoComponente>> registros = null;
            using (DataTable filas = this.daoConfiguracion.ConsultarTipoComponente(paginacion))
            {
                registros = from fila in filas.Select()
                            select new Paginacion<TipoComponente>(fila);
            }

            return registros != null && registros.Count() > 0 ? Paginacion<TipoComponente>.ObtenerItemPaginado(registros.ToList()) : new Paginacion<List<TipoComponente>>();
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
        public List<TipoAtencion> ConsultaTipoAtenciones()
        {
            IEnumerable<TipoAtencion> registros = null;
            using (DataTable filas = this.daoConfiguracion.ConsultaTipoAtenciones())
            {
                registros = from fila in filas.Select()
                            select new TipoAtencion(fila);
            }

            return registros.ToList();
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}