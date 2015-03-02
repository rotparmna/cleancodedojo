// --------------------------------
// <copyright file="ExclusionesVinculacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Utilidades
{
    using System;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Utilidades.ExclusionesVinculacion
    /// </summary>
    public class ExclusionesVinculacion
    {
        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece check activo
        /// </summary>
        public bool CheckActivo { get; set; }

        /// <summary>
        /// Obtiene o establece check principal
        /// </summary>
        public bool CheckPrincipal { get; set; }

        /// <summary>
        /// Obtiene o establece codigo componente
        /// </summary>
        public string CodigoComponente { get; set; }

        /// <summary>
        /// Obtiene o establece grupo producto nombre
        /// </summary>
        public string GrupoProductoNombre { get; set; }

        /// <summary>
        /// Obtiene o establece id exclusion
        /// </summary>
        public int IdExclusion { get; set; }

        /// <summary>
        /// Obtiene o establece id manual
        /// </summary>
        public int IdManual { get; set; }

        /// <summary>
        /// Obtiene o establece manual contrato
        /// </summary>
        public string ManualContrato { get; set; }

        /// <summary>
        /// Obtiene o establece nombre componente
        /// </summary>
        public string NombreComponente { get; set; }

        /// <summary>
        /// Obtiene o establece nombre manual
        /// </summary>
        public string NombreManual { get; set; }

        /// <summary>
        /// Obtiene o establece nombre servicio exclusion
        /// </summary>
        public string NombreServicioExclusion { get; set; }

        /// <summary>
        /// Obtiene o establece producto alterno nombre
        /// </summary>
        public string ProductoAlternoNombre { get; set; }

        /// <summary>
        /// Obtiene o establece producto nombre
        /// </summary>
        public string ProductoNombre { get; set; }

        /// <summary>
        /// Obtiene o establece tipo atencion nombre
        /// </summary>
        public string TipoAtencionNombre { get; set; }

        /// <summary>
        /// Obtiene o establece tipo producto nombre
        /// </summary>
        public string TipoProductoNombre { get; set; }

        /// <summary>
        /// Obtiene o establece vigencia manual
        /// </summary>
        public DateTime VigenciaManual { get; set; }

        #endregion Propiedades Publicas

        #endregion Propiedades
    }
}