// --------------------------------
// <copyright file="ReporteMultiple.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Controles
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using Comun.Extensiones;
    using Microsoft.Reporting.WebForms;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Controles.Reporte
    /// </summary>
    public partial class ReporteMultiple : WebUserControl
    {
        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece los data sets para el llenado del reporte
        /// </summary>
        public Dictionary<string, IEnumerable> DataSetsReporte { get; set; }

        /// <summary>
        /// Obtiene o establece si se puede exportar a Excel
        /// </summary>
        public bool ExportarExcel { get; set; }

        /// <summary>
        /// Obtiene o establece si se puede exportar a PDF
        /// </summary>
        public bool ExportarPdf { get; set; }

        /// <summary>
        /// Obtiene o establece si se puede exportar a Word
        /// </summary>
        public bool ExportarWord { get; set; }

        /// <summary>
        /// Obtiene o establece imprimir
        /// </summary>
        public bool Imprimir { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre reporte en el servidor de reportes
        /// </summary>
        public string NombreReporte { get; set; }

        /// <summary>
        /// Obtiene o establece si es un reporte local, si no indica que es un reporte publicado en el servidor de reportes
        /// </summary>
        public bool ReporteLocal { get; set; }

        /// <summary>
        /// Obtiene o establece reporte modo popup
        /// </summary>
        public bool ReporteModoPopup { get; set; }

        /// <summary>
        /// Obtiene o establece zoom
        /// </summary>
        public bool Zoom { get; set; }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo de realizar la Carga del Reporte
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcin: (Descripcin detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        public void CargarReporte()
        {
            if (this.ReporteLocal)
            {
                this.ConfigurarVisorLocal();
                rpvGenericoMultiple.LocalReport.Refresh();
            }
            else
            {
                this.ConfigurarVisorRemoto();
                rpvGenericoMultiple.LocalReport.Refresh();
            }
        }

        /// <summary>
        /// Metodo de realizar la Carga del Reporte.
        /// </summary>
        /// <param name="tipo">Parámetro tipo.</param>
        /// <param name="tabla">Parámetro tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcin: (Descripcin detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void CargarReporte(int tipo, string tabla, SqlConnection conn)
        {
            if (tipo == 1) 
            {
                // Detallado
                this.ConfigurarVisorRemoto(tabla, conn);
                Microsoft.Reporting.WebForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WebForms.ReportParameter[2];
                reportParameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("TablaDetalle", tabla + "Detalle");
                reportParameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("TablaEncabezado", tabla + "Encabezado");
                rpvGenericoMultiple.ServerReport.SetParameters(reportParameters);
                rpvGenericoMultiple.DataBind();
                rpvGenericoMultiple.ShowParameterPrompts = false;
                rpvGenericoMultiple.ShowPrintButton = true;
                rpvGenericoMultiple.AsyncRendering = false;
                rpvGenericoMultiple.SizeToReportContent = true;
                rpvGenericoMultiple.ServerReport.Refresh();
            }
        }

        /// <summary>
        /// Configura el objeto ReportViewer para visualizar un reporte Local, llenado el o los DataSet del reporte a partir de la propiedad DataSetsReporte
        /// </summary>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        public void ConfigurarVisorLocal()
        {
            LocalReport localReport = rpvGenericoMultiple.LocalReport;
            rpvGenericoMultiple.ProcessingMode = ProcessingMode.Local;
            localReport.DataSources.Clear();
            if (this.DataSetsReporte != null)
            {
                foreach (var dataSet in this.DataSetsReporte)
                {
                    ReportDataSource datasource = new ReportDataSource(dataSet.Key, dataSet.Value);
                    rpvGenericoMultiple.LocalReport.DataSources.Add(datasource);
                }
            }

            if (this.Imprimir)
            {
                rpvGenericoMultiple.ShowPrintButton = true;
            }
            else
            {
                rpvGenericoMultiple.ShowPrintButton = false;
            }

            if (this.Zoom)
            {
                rpvGenericoMultiple.ShowZoomControl = true;
            }
            else
            {
                rpvGenericoMultiple.ShowZoomControl = false;
            }

            this.DeshabilitarExtensiones(localReport);
            localReport.ReportPath = Server.MapPath(this.NombreReporte);
            localReport.Refresh();
        }

        #endregion Metodos Publicos 
        #region Metodos Privados Estaticos 

        /// <summary>
        /// Crea tabla.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="tabla">Parámetro tabla.</param>
        /// <param name="dt">Parámetro dt.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 01/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Crea tabla.
        /// </remarks>
        private static void creaTabla(SqlConnection connection, string tabla, DataTable dt)
        {
            string columns = string.Empty;
            DataTable datatableColumnas = dt.Copy();
            foreach (DataColumn col in datatableColumnas.Columns)
            {
                if (col.DataType.Name.ToUpper() != "TIPOFACTURACION" && col.DataType.Name.ToUpper().IndexOf("LIST") == -1
                    && col.DataType.Name.ToUpper() != "ATENCIONCLIENTE"
                    && col.DataType.Name.ToUpper() != "INFORMACIONFACTURA"
                    && col.DataType.Name.ToUpper() != "RESPONSABLE"
                    && col.DataType.Name.ToUpper() != "TIPOFACTURACION")
                {
                    columns += col.ColumnName + " " + col.DataType.Name.ToUpper() + ",\r\n";
                }
                else 
                {
                    dt.Columns.Remove(col.ColumnName);
                }
            }

            columns = columns.Replace("STRING", "VARCHAR(1000)");
            columns = columns.Replace("INT32", "INT");
            columns = columns.Replace("INT16", "INT");
            columns = columns.Replace("BYTE[]", "VARBINARY(MAX)");
            columns = columns.Replace("DATETIME", "VARCHAR(30)");
            string query = "create table " + tabla + " (" + columns + ")";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180000;
            command.ExecuteNonQuery();
            SqlBulkCopy bcopy = new SqlBulkCopy(connection);
            bcopy.DestinationTableName = tabla;
            bcopy.WriteToServer(dt);
        }

        #endregion Metodos Privados Estaticos 
        #region Metodos Privados 

        /// <summary>
        /// Configura el objeto ReportViewer para visualizar un reporte desde el servidor de reportes, obteniendo el nombre del reporte de la propiedad NombreReporte
        /// </summary>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void ConfigurarVisorRemoto()
        {
            string urlReportes = ConfigurationManager.AppSettings["ReportesServidor"].ToString();
            string urlCarpeta = ConfigurationManager.AppSettings["ReportesCarpeta"].ToString();
            ServerReport serverReport = rpvGenericoMultiple.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            serverReport.ReportPath = urlCarpeta + this.NombreReporte;
            rpvGenericoMultiple.ProcessingMode = ProcessingMode.Remote;
            serverReport.Timeout = 0;
            this.DeshabilitarExtensiones(serverReport);
        }

        /// <summary>
        /// Configura el objeto ReportViewer para visualizar un reporte desde el servidor de reportes, obteniendo el nombre del reporte de la propiedad NombreReporte.
        /// </summary>
        /// <param name="tabla">Parámetro tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void ConfigurarVisorRemoto(string tabla, SqlConnection conn)
        {
            string urlReportes = ConfigurationManager.AppSettings["ReportesServidor"].ToString();
            string urlCarpeta = ConfigurationManager.AppSettings["ReporteDetalladoActividades"].ToString();
            string dominio = ConfigurationManager.AppSettings["ReportesDominioUsuario"].ToString();
            string usuario = ConfigurationManager.AppSettings["ReportesNombreUsuario"].ToString();
            string pass = ConfigurationManager.AppSettings["ReportesClaveUsuario"].ToString();
            ServerReport serverReport = rpvGenericoMultiple.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            serverReport.ReportPath = urlCarpeta;
            rpvGenericoMultiple.ServerReport.ReportServerCredentials = new ReportCredentials(usuario, pass, dominio);
            rpvGenericoMultiple.ProcessingMode = ProcessingMode.Remote;
            string orden = "Encabezado";
            foreach (var dataSet in this.DataSetsReporte)
            {
                DataTable dt = ObtainDataTableFromIEnumerable(dataSet.Value);
                creaTabla(conn, tabla + orden, dt);
                orden = "Detalle";
            }

            this.DeshabilitarExtensiones(serverReport);
        }

        /// <summary>
        /// Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte
        /// </summary>
        /// <param name="serverReport">Objeto Server Report del visualizador del reporte.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void DeshabilitarExtensiones(ServerReport serverReport)
        {
            List<string> extensionesDeshabilitar = this.ObtenerExtensiones();
            serverReport.DisableUnwantedExportFormats(extensionesDeshabilitar.ToArray());
        }

        /// <summary>
        /// Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte
        /// </summary>
        /// <param name="localReport">Objeto Local Report del visualizador del reporte.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void DeshabilitarExtensiones(LocalReport localReport)
        {
            List<string> extensionesDeshabilitar = this.ObtenerExtensiones();
            localReport.DisableUnwantedExportFormats(extensionesDeshabilitar.ToArray());
        }

        /// <summary>
        /// Obtain Data Table From IEnumerable.
        /// </summary>
        /// <param name="ien">Parámetro ien.</param>
        /// <returns>Data Table.</returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 01/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Obtain Data Table From IEnumerable.
        /// </remarks>
        private static DataTable ObtainDataTableFromIEnumerable(IEnumerable ien)
        {
            DataTable dt = new DataTable();
            foreach (object obj in ien)
            {
                Type t = obj.GetType();
                PropertyInfo[] pis = t.GetProperties();
                if (dt.Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in pis)
                    {
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in pis)
                {
                    object value = pi.GetValue(obj, null);
                    dr[pi.Name] = value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Genera un listado con las extensiones a deshabilitar en el visor del reporte
        /// </summary>
        /// <returns>Listado de las extensiones a deshabilitar</returns>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 01/03/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 01/03/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private List<string> ObtenerExtensiones()
        {
            List<string> extensionesDeshabilitar = new List<string>();
            extensionesDeshabilitar.AddRange(new string[] { "XML", "MHTML", "CSV", "IMAGE" });
            if (this.ExportarPdf == false)
            {
                extensionesDeshabilitar.Add("PDF");
            }

            if (this.ExportarWord == false)
            {
                extensionesDeshabilitar.Add("WORDOPENXML");
            }

            if (this.ExportarExcel == false)
            {
                extensionesDeshabilitar.Add("EXCELOPENXML");
            }

            return extensionesDeshabilitar;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }    
}