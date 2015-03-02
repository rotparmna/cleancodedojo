// --------------------------------
// <copyright file="Reporte.ascx.cs" company="InterGrupo S.A.">
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
    public partial class Reporte : WebUserControl
    {
        #region Propiedades

        #region PropiedadesPublicas

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

        #endregion PropiedadesPublicas
        #endregion Propiedades

        #region Metodos

        #region MetodosPublicos

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
            if (ReporteLocal)
            {
                ConfigurarVisorLocal();
                rpvGenerico.LocalReport.Refresh();
            }
            else
            {
                ConfigurarVisorRemoto();
                rpvGenerico.LocalReport.Refresh();
            }
        }

        /// <summary>
        /// Metodo de realizar la Carga del Reporte.
        /// </summary>
        /// <param name="tipoReporte">The tipo reporte.</param>
        /// <param name="tabla">The tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcin: (Descripcin detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void CargarReporte(TipoReporte tipoReporte, string tabla, SqlConnection conn)
        {
            ConfigurarVisorRemoto(tabla, conn, tipoReporte);

            // Detallado Actividades
            if (tipoReporte == TipoReporte.ActividadesDetallado || tipoReporte == TipoReporte.PaquetesDetallado
                || tipoReporte == TipoReporte.ActividadesAgrupado || tipoReporte == TipoReporte.PaquetesAgrupado) 
            {                
                Microsoft.Reporting.WebForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WebForms.ReportParameter[2];
                reportParameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("TablaDetalle", tabla + "Detalle");
                reportParameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("TablaEncabezado", tabla + "Encabezado");
                rpvGenerico.ServerReport.SetParameters(reportParameters);
            }
            
            rpvGenerico.DataBind();
            rpvGenerico.ShowParameterPrompts = false;
            rpvGenerico.ShowPrintButton = true;
            rpvGenerico.AsyncRendering = false;
            rpvGenerico.SizeToReportContent = true;
            rpvGenerico.ServerReport.Refresh();
        }

        /// <summary>
        /// Cargar Reporte Remoto Parametros.
        /// </summary>
        /// <param name="parametros">The parametros.</param>
        /// <param name="nombreReporte">The nombre reporte.</param>
        /// <remarks>
        /// Autor: Sin Información.
        /// FechaDeCreacion: 09/02/2015
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del método, procure especificar todo el método aqui).
        /// </remarks>
        public void CargarReporteRemotoParametros(List<Microsoft.Reporting.WebForms.ReportParameter> parametros, string nombreReporte)
        {
            string urlReportes = ConfigurationManager.AppSettings["ReportesServidor"].ToString();
            string urlCarpeta = ConfigurationManager.AppSettings["ReportesCarpeta"].ToString();
            ////Usuario
            string dominio = ConfigurationManager.AppSettings["ReportesDominioUsuario"].ToString();
            string usuario = ConfigurationManager.AppSettings["ReportesNombreUsuario"].ToString();
            string pass = ConfigurationManager.AppSettings["ReportesClaveUsuario"].ToString();
            ServerReport serverReport = rpvGenerico.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            urlCarpeta = string.Format("{0}/{1}", urlCarpeta, nombreReporte);
            serverReport.ReportPath = urlCarpeta;
            rpvGenerico.ServerReport.ReportServerCredentials = new ReportCredentials(usuario, pass, dominio);
            rpvGenerico.ProcessingMode = ProcessingMode.Remote;
            DeshabilitarExtensiones(serverReport);
            rpvGenerico.ShowParameterPrompts = false;
            ////Adiciona los parametros
            rpvGenerico.ServerReport.SetParameters(parametros);
            rpvGenerico.ServerReport.Refresh();
        }

        /// <summary>
        /// Metodo de realizar la Carga del Reporte.
        /// </summary>
        /// <param name="tipoReporte">The tipo reporte.</param>
        /// <param name="tabla">The tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <param name="reimpresion">if set to <c>true</c> [reimpresion].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificacion - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripcin: (Descripcin detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        public void CargarReporte(TipoReporte tipoReporte, string tabla, SqlConnection conn, bool reimpresion)
        {
            ConfigurarVisorRemoto(tabla, conn, tipoReporte, reimpresion);

            // Detallado Actividades
            if (tipoReporte == TipoReporte.ActividadesDetallado || tipoReporte == TipoReporte.PaquetesDetallado
                || tipoReporte == TipoReporte.ActividadesAgrupado || tipoReporte == TipoReporte.PaquetesAgrupado)
            {
                Microsoft.Reporting.WebForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WebForms.ReportParameter[2];
                reportParameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("TablaDetalle", tabla + "Detalle");
                reportParameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("TablaEncabezado", tabla + "Encabezado");
                rpvGenerico.ServerReport.SetParameters(reportParameters);
            }

            rpvGenerico.DataBind();
            rpvGenerico.ShowParameterPrompts = false;
            rpvGenerico.ShowPrintButton = true;
            rpvGenerico.AsyncRendering = false;
            rpvGenerico.SizeToReportContent = true;
            rpvGenerico.ServerReport.Refresh();
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
            LocalReport localReport = rpvGenerico.LocalReport;
            rpvGenerico.ProcessingMode = ProcessingMode.Local;
            localReport.DataSources.Clear();
            if (DataSetsReporte != null)
            {
                foreach (var dataSet in DataSetsReporte)
                {
                    ReportDataSource datasource = new ReportDataSource(dataSet.Key, dataSet.Value);
                    rpvGenerico.LocalReport.DataSources.Add(datasource);
                }
            }

            if (Imprimir)
            {
                rpvGenerico.ShowPrintButton = true;
            }
            else
            {
                rpvGenerico.ShowPrintButton = false;
            }

            if (Zoom)
            {
                rpvGenerico.ShowZoomControl = true;
            }
            else
            {
                rpvGenerico.ShowZoomControl = false;
            }

            DeshabilitarExtensiones(localReport);
            localReport.ReportPath = Server.MapPath(NombreReporte);
            localReport.Refresh();
        }

        #endregion MetodosPublicos
        #region MetodosProtegidos
        
        #endregion MetodosProtegidos
        #region MetodosPrivados

        /// <summary>
        /// Crea Tabla.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="tabla">The tabla.</param>
        /// <param name="dt">Parametro dt.</param>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias 
        /// FechaDeCreacion: 01/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Crea Tabla.
        /// </remarks>
        private static void CreaTabla(SqlConnection connection, string tabla, DataTable dt)
        {
            string columns = string.Empty;
            DataTable datatableColumnas = dt.Copy();
            foreach (DataColumn col in datatableColumnas.Columns)
            {
                if (col.DataType.Name.ToUpper() != "TIPOFACTURACION" && col.DataType.Name.ToUpper().IndexOf("LIST") == -1
                    && col.DataType.Name.ToUpper() != "ATENCIONCLIENTE"
                    && col.DataType.Name.ToUpper() != "INFORMACIONFACTURA"
                    && col.DataType.Name.ToUpper() != "RESPONSABLE"
                    && col.DataType.Name.ToUpper() != "TIPOFACTURACION"
                    && col.DataType.Name.ToUpper() != "DETALLETARIFA"
                    && col.DataType.Name.ToUpper() != "VENTADETALLE"
                    && !col.DataType.Name.ToUpper().Contains("DICTIONARY"))
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
            columns = columns.Replace("BOOLEAN", "BIT");
            columns = columns.Replace("NOFACTURABLE", "VARCHAR(MAX)");
            columns = columns.Replace("BYTE", "INT");
            columns = columns.Replace("DECIMAL", "DECIMAL(18,5)");

            string query = string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')) DROP TABLE {0} CREATE TABLE {0} ( {1} ) ", tabla, columns);

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180000;
            command.ExecuteNonQuery();
            SqlBulkCopy bcopy = new SqlBulkCopy(connection);
            bcopy.DestinationTableName = tabla;
            bcopy.WriteToServer(dt);
        }

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
            ServerReport serverReport = rpvGenerico.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            serverReport.ReportPath = urlCarpeta + NombreReporte;
            rpvGenerico.ProcessingMode = ProcessingMode.Remote;
            serverReport.Timeout = 0;
            DeshabilitarExtensiones(serverReport);
        }

        /// <summary>
        /// Configura el objeto ReportViewer para visualizar un reporte desde el servidor de reportes, obteniendo el nombre del reporte de la propiedad NombreReporte.
        /// </summary>
        /// <param name="tabla">The tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <param name="tipoReporte">The tipo reporte.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void ConfigurarVisorRemoto(string tabla, SqlConnection conn, TipoReporte tipoReporte)
        {
            string urlReportes = ConfigurationManager.AppSettings["ReportesServidor"].ToString();
            string urlCarpeta = string.Empty;
            if (tipoReporte == TipoReporte.ActividadesDetallado)
            {
                urlCarpeta = ConfigurationManager.AppSettings["ReporteDetalladoActividades"].ToString();

                string orden = "Encabezado";
                foreach (var dataSet in DataSetsReporte)
                {
                    DataTable dt = ObtainDataTableFromIEnumerable(dataSet.Value);
                    CreaTabla(conn, tabla + orden, dt);
                    orden = "Detalle";
                }
            }
            else if (tipoReporte == TipoReporte.PaquetesDetallado)
            {
                urlCarpeta = ConfigurationManager.AppSettings["ReporteDetalladoPaquetes"].ToString();

                string orden = "Encabezado";
                foreach (var dataSet in DataSetsReporte)
                {
                    DataTable dt = ObtainDataTableFromIEnumerable(dataSet.Value);
                    CreaTabla(conn, tabla + orden, dt);
                    orden = "Detalle";
                }
            }

            string dominio = ConfigurationManager.AppSettings["ReportesDominioUsuario"].ToString();
            string usuario = ConfigurationManager.AppSettings["ReportesNombreUsuario"].ToString();
            string pass = ConfigurationManager.AppSettings["ReportesClaveUsuario"].ToString();
            ServerReport serverReport = rpvGenerico.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            serverReport.ReportPath = urlCarpeta;
            rpvGenerico.ServerReport.ReportServerCredentials = new ReportCredentials(usuario, pass, dominio);
            rpvGenerico.ProcessingMode = ProcessingMode.Remote;
            
            DeshabilitarExtensiones(serverReport);
        }

        /// <summary>
        /// Configura el objeto ReportViewer para visualizar un reporte desde el servidor de reportes, obteniendo el nombre del reporte de la propiedad NombreReporte.
        /// </summary>
        /// <param name="tabla">The tabla.</param>
        /// <param name="conn">Parámetro conn.</param>
        /// <param name="tipoReporte">The tipo reporte.</param>
        /// <param name="boolEsReimpresion">if set to <c>true</c> [es reimpresion].</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin:
        /// </remarks>
        private void ConfigurarVisorRemoto(string tabla, SqlConnection conn, TipoReporte tipoReporte, bool boolEsReimpresion)
        {
            string urlReportes = ConfigurationManager.AppSettings["ReportesServidor"].ToString();
            string urlCarpeta = string.Empty;
            if (tipoReporte == TipoReporte.ActividadesDetallado)
            {
                if (boolEsReimpresion)
                {
                    urlCarpeta = ConfigurationManager.AppSettings["ReimpresionDetalladoActividades"].ToString();
                }
                else
                {
                    urlCarpeta = ConfigurationManager.AppSettings["ReporteDetalladoActividades"].ToString();
                }

                string orden = "Encabezado";
                foreach (var dataSet in DataSetsReporte)
                {
                    DataTable dt = ObtainDataTableFromIEnumerable(dataSet.Value);
                    CreaTabla(conn, tabla + orden, dt);
                    orden = "Detalle";
                }
            }
            else if (tipoReporte == TipoReporte.PaquetesDetallado)
            {
                urlCarpeta = ConfigurationManager.AppSettings["ReporteDetalladoPaquetes"].ToString();

                string orden = "Encabezado";
                foreach (var dataSet in DataSetsReporte)
                {
                    DataTable dt = ObtainDataTableFromIEnumerable(dataSet.Value);
                    CreaTabla(conn, tabla + orden, dt);
                    orden = "Detalle";
                }
            }

            string dominio = ConfigurationManager.AppSettings["ReportesDominioUsuario"].ToString();
            string usuario = ConfigurationManager.AppSettings["ReportesNombreUsuario"].ToString();
            string pass = ConfigurationManager.AppSettings["ReportesClaveUsuario"].ToString();
            ServerReport serverReport = rpvGenerico.ServerReport;
            serverReport.ReportServerUrl = new Uri(urlReportes);
            serverReport.ReportPath = urlCarpeta;
            rpvGenerico.ServerReport.ReportServerCredentials = new ReportCredentials(usuario, pass, dominio);
            rpvGenerico.ProcessingMode = ProcessingMode.Remote;

            DeshabilitarExtensiones(serverReport);
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
        /// Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte.
        /// </summary>
        /// <param name="serverReport">Objeto Server Report del visualizador del reporte.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin: Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte.
        /// </remarks>
        private void DeshabilitarExtensiones(ServerReport serverReport)
        {
            List<string> extensionesDeshabilitar = ObtenerExtensiones();
            serverReport.DisableUnwantedExportFormats(extensionesDeshabilitar.ToArray());
        }

        /// <summary>
        /// Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte.
        /// </summary>
        /// <param name="localReport">Objeto Local Report del visualizador del reporte.</param>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 28/02/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 28/02/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin: Metodo auxiliar que deshabilita las posibles opciones de exportacin del reporte.
        /// </remarks>
        private void DeshabilitarExtensiones(LocalReport localReport)
        {
            List<string> extensionesDeshabilitar = ObtenerExtensiones();
            localReport.DisableUnwantedExportFormats(extensionesDeshabilitar.ToArray());
        }

        /// <summary>
        /// Genera un listado con las extensiones a deshabilitar en el visor del reporte.
        /// </summary>
        /// <returns>Listado de las extensiones a deshabilitar.</returns>
        /// <remarks>
        /// Autor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeCreacion: 01/03/2013
        /// UltimaModificacionPor: William Vsquez R. - INTRGRUPO\wvasquez
        /// FechaDeUltimaModificacion: 01/03/2013
        /// EncargadoSoporte: William Vsquez R. - INTRGRUPO\wvasquez
        /// Descripcin: Genera un listado con las extensiones a deshabilitar en el visor del reporte.
        /// </remarks>
        private List<string> ObtenerExtensiones()
        {
            List<string> extensionesDeshabilitar = new List<string>();
            extensionesDeshabilitar.AddRange(new string[] { "XML", "MHTML", "CSV", "IMAGE" });
            if (ExportarPdf == false)
            {
                extensionesDeshabilitar.Add("PDF");
            }

            if (ExportarWord == false)
            {
                extensionesDeshabilitar.Add("WORDOPENXML");
            }

            if (ExportarExcel == false)
            {
                extensionesDeshabilitar.Add("EXCELOPENXML");
            }

            return extensionesDeshabilitar;
        }

        #endregion MetodosPrivados
        #endregion Metodos
    }

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Controles.ReportCredentials.
    /// </summary>
    public class ReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        #region Declaraciones Locales 

        #region Variables 

        /// <summary>
        /// The _user name.
        /// </summary>
        private string userNameAux, passwordAux, domainAux;

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Constructores 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CliCountry.Facturacion.Web.WebExterno.Comun.Controles.ReportCredentials"/>
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain.</param>
        public ReportCredentials(string userName, string password, string domain)
        {
            userNameAux = userName;
            passwordAux = password;
            domainAux = domain;
        }

        #endregion Constructores 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> of the user to impersonate when the <see cref="T:Microsoft.Reporting.WebForms.ReportViewer" /> control connects to a report server.
        /// </summary>
        /// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the user to impersonate.</returns>
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the network credentials that are used for authentication with the report server.
        /// </summary>
        /// <returns>An implementation of <see cref="T:System.Net.ICredentials" /> that contains the credential information for connecting to a report server.</returns>
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new System.Net.NetworkCredential(userNameAux, passwordAux, domainAux);
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Get Forms Credentials.
        /// </summary>
        /// <param name="authCoki">The auth coki.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="authority">The authority.</param>
        /// <returns>
        /// Retorna true - false.
        /// </returns>
        /// <remarks>
        /// Autor: José Alexander Murcia Salamanca - INTERGRUPO\jmurcias
        /// FechaDeCreacion: 01/09/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Get Forms Credentials.
        /// </remarks>
        public bool GetFormsCredentials(out System.Net.Cookie authCoki, out string userName, out string password, out string authority)
        {
            userName = userNameAux;
            password = passwordAux;
            authority = domainAux;
            authCoki = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "hhi");
            return true;
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}