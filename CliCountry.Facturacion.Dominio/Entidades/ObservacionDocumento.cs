// --------------------------------
// <copyright file="ObservacionDocumento.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Observacion Documento.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.Entidades.ObservacionDocumento.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ObservacionDocumento
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The TEXTOCOTIZACIONES.
        /// </summary>
        private const string TEXTOCOTIZACIONES = "Cotizaciones";

        /// <summary>
        /// The TEXTOFACTURAS.
        /// </summary>
        private const string TEXTOFACTURAS = "Facturas";

        /// <summary>
        /// The VALORCOTIZACIONES.
        /// </summary>
        private const string VALORCOTIZACIONES = "COTI";

        /// <summary>
        /// The VALORFACTURAS.
        /// </summary>
        private const string VALORFACTURAS = "FACT";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.ObservacionDocumento"/>
        /// </summary>
        public ObservacionDocumento()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.ObservacionDocumento"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public ObservacionDocumento(DataRow fila)
        {
            this.IdObservacion = Convert.ToInt32(fila[RecursosDominio.ObservacionDocumento_IdObservacion_Entidad]);
            this.Descripcion = fila[RecursosDominio.ObservacionDocumento_Descripcion_Entidad].ToString();
            this.Activo = Convert.ToBoolean(fila[RecursosDominio.ObservacionDocumento_Activo_Entidad]);
            this.TipoProceso = fila[RecursosDominio.ObservacionDocumento_TipoProceso_Entidad].ToString();
            switch (this.TipoProceso)
            {
                case VALORCOTIZACIONES:
                    this.NombreTipoProceso = TEXTOCOTIZACIONES;
                    break;
                case VALORFACTURAS:
                    this.NombreTipoProceso = TEXTOFACTURAS;
                    break;
                default:
                    break;
            }
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece activo.
        /// </summary>
        /// <value>
        /// Tipo Dato Bool.
        /// </value>
        [DataMember]
        public bool Activo { get; set; }

        /// <summary>
        /// Obtiene o establece codigo entidad.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoEntidad { get; set; }

        /// <summary>
        /// Obtiene o establece descripcion.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene o establece id observacion.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdObservacion { get; set; }

        /// <summary>
        /// Obtiene o establece id sede.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdSede { get; set; }

        /// <summary>
        /// Obtiene o establece modulo.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string Modulo { get; set; }

        /// <summary>
        /// Obtiene o establece nombre tipo proceso.
        /// </summary>
        /// <value>
        /// Tipo dato String.
        /// </value>
        [DataMember]
        public string NombreTipoProceso { get; set; }

        /// <summary>
        /// Obtiene o establece tipo proceso.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string TipoProceso { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}