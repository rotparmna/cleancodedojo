// --------------------------------
// <copyright file="ExclusionFacturaActividades.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2015, Intergrupo S.A
// </copyright>
// <summary>Entidad Recargo Tarifa.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Dominio.Entidades
{
    using System;
    using System.Data;
    using CliCountry.Facturacion.Dominio.Recursos;

    /// <summary>
    /// Entidad que se utiliza para representar las Exclusiones de las facturas por actividades.
    /// </summary>
    public class ExclusionFacturaActividades
    {
        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.ExclusionFacturaActividades"/>
        /// </summary>
        public ExclusionFacturaActividades() 
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Dominio.Entidades.ExclusionFacturaActividades"/>
        /// </summary>
        /// <param name="fila">Parámetro fila.</param>
        public ExclusionFacturaActividades(DataRow fila)
        {
            this.Id = int.Parse(fila[RecursosDominio.ExclusionFacturaActividades_Id_Entidad].ToString());
            this.NombreServicio = fila[RecursosDominio.ExclusionFacturaActividades_NombreServicio_Entidad].ToString();
            this.NombreTipoAtencion = fila[RecursosDominio.ExclusionFacturaActividades_NombreTipoAtencion_Entidad].ToString();
            this.NombreTipoProducto = fila[RecursosDominio.ExclusionFacturaActividades_NombreTipoProducto_Entidad].ToString();
            this.NombreGrupo = fila[RecursosDominio.ExclusionFacturaActividades_NombreGrupo_Entidad].ToString();
            this.NombreProducto = fila[RecursosDominio.ExclusionFacturaActividades_NombreProducto_Entidad].ToString();
            this.NombreProductoAlterno = fila[RecursosDominio.ExclusionFacturaActividades_NombreProductoAlterno_Entidad].ToString();
            this.TipoContrato = int.Parse(fila[RecursosDominio.ExclusionFacturaActividades_TipoContrato_Entidad].ToString());
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece codigo entidad.
        /// </summary>
        /// <value>
        /// Valor string.
        /// </value>
        public string CodigoEntidad { get; set; }

        /// <summary>
        /// Obtiene o establece id.
        /// </summary>
        /// <value>
        /// Valor int.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece id contrato.
        /// </summary>
        /// <value>
        /// Valor int.
        /// </value>
        public int IdContrato { get; set; }

        /// <summary>
        /// Obtiene o establece id plan.
        /// </summary>
        /// <value>
        /// Valor int.
        /// </value>
        public int IdPlan { get; set; }

        /// <summary>
        /// Obtiene o establece id tercero.
        /// </summary>
        /// <value>
        /// Valor int.
        /// </value>
        public int IdTercero { get; set; }

        /// <summary>
        /// Obtiene o establece nombre grupo.
        /// </summary>
        /// <value>
        /// Valor string
        /// </value>
        public string NombreGrupo { get; set; }

        /// <summary>
        /// Obtiene o establece nombre producto.
        /// </summary>
        /// <value>
        /// Valor string
        /// </value>
        public string NombreProducto { get; set; }

        /// <summary>
        /// Obtiene o establece nombre producto alterno.
        /// </summary>
        /// <value>
        /// Valor string.
        /// </value>
        public string NombreProductoAlterno { get; set; }

        /// <summary>
        /// Obtiene o establece nombre servicio.
        /// </summary>
        /// <value>
        /// Valor string
        /// </value>
        public string NombreServicio { get; set; }

        /// <summary>
        /// Obtiene o establece nombre tipo atencion.
        /// </summary>
        /// <value>
        /// Valor string
        /// </value>
        public string NombreTipoAtencion { get; set; }

        /// <summary>
        /// Obtiene o establece nombre tipo producto.
        /// </summary>
        /// <value>
        /// Valor string.
        /// </value>
        public string NombreTipoProducto { get; set; }

        /// <summary>
        /// Obtiene o establece tipo contrato.
        /// </summary>
        /// <value>
        /// Valor int.
        /// </value>
        public int TipoContrato { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 
    }
}