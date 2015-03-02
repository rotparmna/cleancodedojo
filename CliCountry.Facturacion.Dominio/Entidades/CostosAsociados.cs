// --------------------------------
// <copyright file="CostosAsociados.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Costos Asociados.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.CostosAsociados.
    /// </summary>
    [DataContract]
    [Serializable]
    public class CostosAsociados
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CostosAsociados"/>
        /// </summary>
        public CostosAsociados()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CostosAsociados"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public CostosAsociados(DataRow fila)
        {
            this.Idgrupo = Convert.ToInt32(fila[RecursosDominio.CostosAsociados_IdGrupo_Entidad]);
            this.NomGrupo = fila[RecursosDominio.CostosAsociados_NomGrupo_Entidad].ToString();
            this.IdProducto = Convert.ToInt32(fila[RecursosDominio.CostosAsociados_IdProducto_Entidad]);
            this.CodProducto = fila[RecursosDominio.CostosAsociados_CodProducto_Entidad].ToString();
            this.NomProducto = fila[RecursosDominio.CostosAsociados_NomProducto_Entidad].ToString();
            this.Incluye = Convert.ToBoolean(fila[RecursosDominio.CostosAsociados_Incluye_Entidad]);
            this.Activo = Convert.ToBoolean(fila[RecursosDominio.CostosAsociados_Activo_Entidad]);
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece Idgrupo.
        /// </summary>
        /// <value>
        /// Tipo Dato Bool.
        /// </value>
        [DataMember]
        public bool Activo { get; set; }

        /// <summary>
        /// Obtiene o establece codigo.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Codigo { get; set; }

        /// <summary>
        /// Obtiene o establece codigo manual.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int CodigoManual { get; set; }

        /// <summary>
        /// Obtiene o establece CodProducto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodProducto { get; set; }

        /// <summary>
        /// Obtiene o establece Idgrupo.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int Idgrupo { get; set; }

        /// <summary>
        /// Obtiene o establece IdProducto.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdProducto { get; set; }

        /// <summary>
        /// Obtiene o establece Idgrupo.
        /// </summary>
        /// <value>
        /// Tipo Dato Bool.
        /// </value>
        [DataMember]
        public bool Incluye { get; set; }

        /// <summary>
        /// Obtiene o establece NombreManual.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreManual { get; set; }

        /// <summary>
        /// Obtiene o establece NomGrupo.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NomGrupo { get; set; }

        /// <summary>
        /// Obtiene o establece NomProducto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NomProducto { get; set; }

        /// <summary>
        /// Obtiene o establece Tarifa.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Tarifa { get; set; }

        /// <summary>
        /// Obtiene o establece Vigencia.
        /// </summary>
        /// <value>
        /// Tipo Dato DateTime.
        /// </value>
        [DataMember]
        public DateTime Vigencia { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}