// --------------------------------
// <copyright file="CuentaTipoEmpresa.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Cuenta Tipo Empresa.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.Entidades.CuentaTipoEmpresa.
    /// </summary>
    [DataContract]
    [Serializable]
    public class CuentaTipoEmpresa
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CuentaTipoEmpresa"/>
        /// </summary>
        public CuentaTipoEmpresa()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CuentaTipoEmpresa"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public CuentaTipoEmpresa(DataRow fila)
        {
            this.TipoEmpresa = fila[RecursosDominio.CuentaTipoEmpresa_TipoEmpresa_Entidad].ToString();
            this.CodigoCuentaCredito = fila[RecursosDominio.CuentaTipoEmpresa_CodigoCuentaCredito_Entidad].ToString();
            this.NombreCuentaCredito = fila[RecursosDominio.CuentaTipoEmpresa_NombreCuentaCredito_Entidad].ToString();
            this.IdTipoEmpresa = Convert.ToInt32(fila[RecursosDominio.CuentaTipoEmpresa_IdTipoEmpresa_Entidad].ToString());
            this.CodigoCuentaDescuentos = fila[RecursosDominio.CuentaTipoEmpresa_CodigoCuentaDescuentos_Entidad].ToString();
            this.NombreCuentaDescuentos = fila[RecursosDominio.CuentaTipoEmpresa_NombreCuentaDescuentos_Entidad].ToString();
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece codigo cuenta credito.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoCuentaCredito { get; set; }

        /// <summary>
        /// Obtiene o establece codigo cuenta descuentos.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoCuentaDescuentos { get; set; }

        /// <summary>
        /// Obtiene o establece codigo entidad.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoEntidad { get; set; }

        /// <summary>
        /// Obtiene o establece id tipo empresa.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdTipoEmpresa { get; set; }

        /// <summary>
        /// Obtiene o establece nombre cuenta credito.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreCuentaCredito { get; set; }

        /// <summary>
        /// Obtiene o establece nombre cuenta descuentos.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreCuentaDescuentos { get; set; }

        /// <summary>
        /// Obtiene o establece tipo empresa.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string TipoEmpresa { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}