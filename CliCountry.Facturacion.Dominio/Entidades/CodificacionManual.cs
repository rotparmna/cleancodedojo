// --------------------------------
// <copyright file="CodificacionManual.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Entidad Codificacion Manual.</summary>
// ---------------------------------

namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Dominio.Entidades.CodificacionManual.
    /// </summary>
    [DataContract]
    [Serializable]
    public class CodificacionManual
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CodificacionManual"/>
        /// </summary>
        public CodificacionManual()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.CodificacionManual"/>
        /// </summary>
        /// <param name="fila">DataRow fila.</param>
        public CodificacionManual(DataRow fila)
        {
            this.IdManual = Convert.ToInt32(fila[RecursosDominio.CodificacionManual_IdManual_Entidad]);
            this.VigenciaManual = Convert.ToDateTime(fila[RecursosDominio.CodificacionManual_VigenciaManual_Entidad]);
            this.IdProducto = Convert.ToInt32(fila[RecursosDominio.CodificacionManual_IdProducto_Entidad]);
            this.CodigoManual = fila[RecursosDominio.CodificacionManual_CodigoManual_Entidad].ToString();
            this.NombreProcedimiento = fila[RecursosDominio.CodificacionManual_NombreProcedimiento_Entidad].ToString();
            this.VigenciaManualProducto = Convert.ToDateTime(fila[RecursosDominio.CodificacionManual_VigenciaManualProducto_Entidad]);
            this.IndHabilitado = Convert.ToInt16(fila[RecursosDominio.CodificacionManual_IndHabilitado_Entidad]);
            if (!(fila[RecursosDominio.CodificacionManual_IdTipoProducto_Entidad] is DBNull))
            {
                this.IdTipoProducto = Convert.ToInt32(fila[RecursosDominio.CodificacionManual_IdTipoProducto_Entidad]);
            }

            if (!(fila[RecursosDominio.CodificacionManual_IdGrupoProducto_Entidad] is DBNull))
            {
                this.IdGrupoProducto = Convert.ToInt32(fila[RecursosDominio.CodificacionManual_IdGrupoProducto_Entidad]);
            }

            if (!(fila[RecursosDominio.CodificacionManual_NombreProducto_Entidad] is DBNull))
            {
                this.NombreProducto = fila[RecursosDominio.CodificacionManual_NombreProducto_Entidad].ToString();
            }
            else
            {
                this.NombreProducto = string.Empty;
            }

            if (!(fila[RecursosDominio.CodificacionManual_NombreGrupoProducto_Entidad] is DBNull))
            {
                this.NombreGrupoProducto = fila[RecursosDominio.CodificacionManual_NombreGrupoProducto_Entidad].ToString();
            }
            else
            {
                this.NombreGrupoProducto = string.Empty;
            }

            if (!(fila[RecursosDominio.CodificacionManual_NombreTipoProducto_Entidad] is DBNull))
            {
                this.NombreTipoProducto = fila[RecursosDominio.CodificacionManual_NombreTipoProducto_Entidad].ToString();
            }
            else
            {
                this.NombreTipoProducto = string.Empty;
            }

            if (!(fila[RecursosDominio.CodificacionManual_CodigoProducto_Entidad] is DBNull))
            {
                this.CodigoProducto = fila[RecursosDominio.CodificacionManual_CodigoProducto_Entidad].ToString();
            }
            else
            {
                this.CodigoProducto = string.Empty;
            }

            this.NombreManual = fila[RecursosDominio.CodificacionManual_NombreManual_Entidad].ToString();
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece codigo manual.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoManual { get; set; }

        /// <summary>
        /// Obtiene o establece codigo producto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string CodigoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece id grupo producto.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdGrupoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece id manual.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdManual { get; set; }

        /// <summary>
        /// Obtiene o establece id producto.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdProducto { get; set; }

        /// <summary>
        /// Obtiene o establece id tipo producto.
        /// </summary>
        /// <value>
        /// Tipo Dato Int.
        /// </value>
        [DataMember]
        public int IdTipoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece ind habilitado.
        /// </summary>
        /// <value>
        /// Tipo Dato Short.
        /// </value>
        [DataMember]
        public short IndHabilitado { get; set; }

        /// <summary>
        /// Obtiene o establece nombre grupo producto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreGrupoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece nombre manual.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreManual { get; set; }

        /// <summary>
        /// Obtiene o establece nombre procedimiento.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreProcedimiento { get; set; }

        /// <summary>
        /// Obtiene o establece nombre producto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreProducto { get; set; }

        /// <summary>
        /// Obtiene o establece nombre tipo producto.
        /// </summary>
        /// <value>
        /// Tipo Dato String.
        /// </value>
        [DataMember]
        public string NombreTipoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece vigencia manual.
        /// </summary>
        /// <value>
        /// Tipo Dato DateTime.
        /// </value>
        [DataMember]
        public DateTime VigenciaManual { get; set; }

        /// <summary>
        /// Obtiene o establece vigencia manual producto.
        /// </summary>
        /// <value>
        /// Tipo Dato DateTime.
        /// </value>
        [DataMember]
        public DateTime VigenciaManualProducto { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}