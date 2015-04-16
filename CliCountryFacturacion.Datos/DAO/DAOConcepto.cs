using CliCountry.Facturacion.Datos.Recursos;
using CliCountry.SAHI.Comun.AccesoDatos;
using CliCountry.SAHI.Dominio.Entidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CliCountry.Facturacion.Datos.DAO
{
    public class DAOConcepto : OperacionesBase
    {
        /// <summary>
        /// Método para consultar conceptos asociados a una atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>Conceptos de la atención.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        /// FechaDeCreacion: 24/06/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public DataTable ConsultarConceptos(Atencion atencion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(this.CrearParametro(Parametros.DetalleFactura_IdAtencion_Param, DbType.Int32, atencion.IdAtencion));
            return this.Consultar(OperacionesBaseDatos.Concepto_ObtenerConcepto, parametros);
        }
    }
}
