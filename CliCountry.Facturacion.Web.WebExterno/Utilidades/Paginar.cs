// --------------------------------
// <copyright file="Paginar.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Utilidades
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Utilidades.Paginador
    /// </summary>
    [Serializable]
    [DataContract]
    public class Paginador
    {
        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece page count
        /// </summary>
        [DataMember]
        public int CantidadPaginas { get; set; }

        /// <summary>
        /// Obtiene o establece registros pagina
        /// </summary>
        [DataMember]
        public int LongitudPagina { get; set; }

        /// <summary>
        /// Obtiene o establece maximo paginas
        /// </summary>
        [DataMember]
        public short MaximoPaginas { get; set; }

        /// <summary>
        /// Obtiene o establece numero pagina
        /// </summary>
        [DataMember]
        public int PaginaActual { get; set; }

        /// <summary>
        /// Obtiene o establece total registro
        /// </summary>
        [DataMember]
        public int TotalRegistros { get; set; }

        #endregion Propiedades Publicas

        #endregion Propiedades
    }
}