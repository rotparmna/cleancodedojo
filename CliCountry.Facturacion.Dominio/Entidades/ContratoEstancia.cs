// --------------------------------
// <copyright file="ContratoEstancia.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Contrato Estancia.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.UnidadMedida.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ContratoEstancia
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.UnidadMedida"/>
        /// </summary>
        public ContratoEstancia()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.UnidadMedida"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public ContratoEstancia(DataRow fila)
        {
            this.Id = Convert.ToInt32(fila[RecursosDominio.ContratoEstancia_Id_Entidad]);
            this.IdTercero = Convert.ToInt32(fila[RecursosDominio.ContratoEstancia_IdTercero_Entidad]);
            this.Contrato = Convert.ToInt32(fila[RecursosDominio.ContratoEstancia_Contrato_Entidad]);
            this.NombreContrato = fila[RecursosDominio.ContratoEstancia_NombreContrato_Entidad].ToString();
            this.Plan = Convert.ToInt32(fila[RecursosDominio.ContratoEstancia_Plan_Entidad]);
            this.NombrePlan = fila[RecursosDominio.ContratoEstancia_NombrePlan_Entidad].ToString();
            this.HoraCorte = fila[RecursosDominio.ContratoEstancia_HoraCorte_Entidad].ToString();
            this.HoraGracia = Convert.ToInt16(fila[RecursosDominio.ContratoEstancia_HoraGracia_Entidad]);
            this.TipoCobro = fila[RecursosDominio.ContratoEstancia_TipoCobro_Entidad].ToString();
            this.Activo = Convert.ToBoolean(fila[RecursosDominio.ContratoEstancia_Activo_Entidad]);
            this.Unidades = fila[RecursosDominio.ContratoEstancia_Unidades_Entidad].ToString();
            this.EstanciaMinima = Convert.ToInt16(fila[RecursosDominio.ContratoEstancia_Estancia_Entidad]);
            this.HoraGraciaEgreso = Convert.ToInt16(fila[RecursosDominio.ContratoEstancia_HoraGraciaEgreso_Entidad]);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.ContratoEstancia"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        /// <param name="tipo">Byte tipo.</param>
        public ContratoEstancia(DataRow fila, byte tipo)
        {
            this.Contrato = Convert.ToInt32(fila[RecursosDominio.ContratoEstancia_Contrato_Entidad]);
            this.NombreContrato = fila[RecursosDominio.ContratoEstancia_NombreContrato_Entidad].ToString();
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece Activo.
        /// </summary>
        /// <value>
        /// Tipo Dato Bool.
        /// </value>
        [DataMember]
        public bool Activo { get; set; }

        /// <summary>
        /// Obtiene o establece Codigo.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Codigo { get; set; }

        /// <summary>
        /// Obtiene o establece Contrato.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int Contrato { get; set; }

        /// <summary>
        /// Obtiene o establece EstanciaMinima.
        /// </summary>
        /// <value>
        /// Tipo Dato Short.
        /// </value>
        [DataMember]
        public short EstanciaMinima { get; set; }

        /// <summary>
        /// Obtiene o establece HoraCorte.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string HoraCorte { get; set; }

        /// <summary>
        /// Obtiene o establece HoraGracia.
        /// </summary>
        /// <value>
        /// Tipo Dato Short.
        /// </value>
        [DataMember]
        public short HoraGracia { get; set; }

        /// <summary>
        /// Obtiene o establece HoraGraciaEgreso.
        /// </summary>
        /// <value>
        /// Tipo Dato Short.
        /// </value>
        [DataMember]
        public short HoraGraciaEgreso { get; set; }

        /// <summary>
        /// Obtiene o establece Id.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece IdTercero.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdTercero { get; set; }

        /// <summary>
        /// Obtiene o establece NombreContrato.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreContrato { get; set; }

        /// <summary>
        /// Obtiene o establece NombrePlan.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombrePlan { get; set; }

        /// <summary>
        /// Obtiene o establece NombreTercero.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreTercero { get; set; }

        /// <summary>
        /// Obtiene o establece Plan.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int Plan { get; set; }

        /// <summary>
        /// Obtiene o establece TipoCobro.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string TipoCobro { get; set; }

        /// <summary>
        /// Obtiene o establece Unidades.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Unidades { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}