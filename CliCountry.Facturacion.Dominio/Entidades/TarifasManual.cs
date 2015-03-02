// --------------------------------
// <copyright file="TarifasManual.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Tarifas Manual.</summary>
// --------------------------------

namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.Entidades.TarifasManual.
    /// </summary>
    [DataContract]
    [Serializable]
    public class TarifasManual
    {
        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.TarifasManual"/>
        /// </summary>
        public TarifasManual()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.TarifasManual"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public TarifasManual(DataRow fila)
        {
            this.CodigoManual = Convert.ToInt16(fila[RecursosDominio.Manual_CodigoManual]);
            this.NombreTarifa = fila[RecursosDominio.Manual_NombreTarifa].ToString();
            this.Vigencia = Convert.ToDateTime(fila[RecursosDominio.Manual_Vigencia]);
            this.Estado = fila[RecursosDominio.Manual_IndActivo].ToString();
            this.CodigoEntidad = fila[RecursosDominio.Manual_EntCod].ToString();
            this.CodigoCodificacion = fila[RecursosDominio.Manual_TarMalCod].ToString();
            this.VigenciaCodificacion = fila[RecursosDominio.Manual_TarMalVig].ToString();
            this.ValorFijo = Convert.ToInt32(fila[RecursosDominio.Manual_TarIndValFij]);
            this.Cantidad = Convert.ToInt32(fila[RecursosDominio.Manual_TarCanViao]);
        }

        #endregion Constructores

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece ValorFijo.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public int Cantidad { get; set; }

        /// <summary>
        /// Obtiene o establece CodigoCodificacion.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public String CodigoCodificacion { get; set; }

        /// <summary>
        /// Obtiene o establece CodigoEntidad.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoEntidad { get; set; }

        /// <summary>
        /// Obtiene o establece codigo manual.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int CodigoManual { get; set; }

        /// <summary>
        /// Obtiene o establece estado.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Estado { get; set; }

        /// <summary>
        /// Obtiene o establece nombre manual.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreManual { get; set; }

        /// <summary>
        /// Obtiene o establece nombre tarifa.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreTarifa { get; set; }

        /// <summary>
        /// Obtiene o establece ValorFijo.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int ValorFijo { get; set; }

        /// <summary>
        /// Obtiene o establece vigencia.
        /// </summary>
        /// <value>
        /// Tipo Dato DateTime.
        /// </value>
        [DataMember]
        public DateTime Vigencia { get; set; }

        /// <summary>
        /// Obtiene o establece CodigoCodificacion.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public String VigenciaCodificacion { get; set; }

        #endregion Propiedades Publicas

        #endregion Propiedades
    }
}