// --------------------------------
// <copyright file="FachadaFacturacionXML.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Fachada Facturacion XML.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Datos.Fachada
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Xml.Linq;
    using CliCountry.Facturacion.Datos.DAO;
    using CliCountry.Facturacion.Datos.Recursos;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;
    using CliCountry.SAHI.Dominio.Recursos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Datos.Fachada.FachadaFacturacionXML.
    /// </summary>
    public static class FachadaFacturacionXML
    {
        #region Declaraciones Locales

        #region Variables

        /// <summary>
        /// Referencia a la clase que resuelve las necesidades de datos de facturacion.
        /// </summary>
        private static DAOFacturacion daoFacturacion = new DAOFacturacion(OperacionesBaseDatos.ConexionFacturacion);

        #endregion Variables

        #endregion Declaraciones Locales

        #region Metodos

        #region Metodos Publicos Estaticos

        /// <summary>
        /// Obtiene las Validaciones.
        /// </summary>
        /// <param name="nombreElemento">The nombre elemento.</param>
        /// <param name="documentoXML">The documento XML.</param>
        /// <returns>
        /// Lista de Nodos de XML.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 20/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static ObservableCollection<NodoValidacion> CargarArbolValidaciones(string nombreElemento, XDocument documentoXML)
        {
            var nodos = new ObservableCollection<NodoValidacion>();
            EnumerarNodos(documentoXML.Descendants(), 0);
            var elemento = documentoXML.Element(XName.Get(nombreElemento)).Elements();
            LeerXml(nodos, elemento);
            return nodos;
        }

        /// <summary>
        /// COnsulta las condiciones de proceso de los XML.
        /// </summary>
        /// <returns>Lista de Condiciones de Proceso.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 24/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static List<CondicionProceso> ConsultarCondicionesProceso()
        {
            IEnumerable<CondicionProceso> condicionesProceso;

            using (DataTable filas = daoFacturacion.ConsultarCondicionesProceso())
            {
                condicionesProceso = from
                                          fila in filas.Select()
                                     select
                                          new CondicionProceso(fila);
            }

            return condicionesProceso != null ? condicionesProceso.ToList() : null;
        }

        #endregion Metodos Publicos Estaticos

        #region Metodos Privados Estaticos

        /// <summary>
        /// Crea el campo Id para cada uno de los elementos.
        /// </summary>
        /// <param name="elementos">The elementos.</param>
        /// <param name="identificadorNodo">The id nodo.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 22/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static void EnumerarNodos(IEnumerable<XElement> elementos, short identificadorNodo)
        {
            foreach (XElement item in elementos)
            {
                if (item.Attribute(XName.Get(RecursosDominio.NodoValidacion_IdElemento_Entidad)) == null)
                {
                    using (var writer = item.CreateWriter())
                    {
                        writer.WriteAttributeString(RecursosDominio.NodoValidacion_IdElemento_Entidad, identificadorNodo.ToString());
                        writer.Flush();
                    }

                    identificadorNodo += 1;
                }
            }
        }

        /// <summary>
        /// Genera la Estructura del Arbol de Validaciones.
        /// </summary>
        /// <param name="nodosValidacion">The nodos validacion.</param>
        /// <param name="elementos">The elementos.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        /// FechaDeCreacion: 20/02/2013.
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        /// FechaDeUltimaModificacion: (dd/MM/yyyy).
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private static void LeerXml(ObservableCollection<NodoValidacion> nodosValidacion, IEnumerable<XElement> elementos)
        {
            NodoValidacion nodoValidacion = null;

            foreach (var item in elementos)
            {
                nodoValidacion = new NodoValidacion(item);
                nodosValidacion.Add(nodoValidacion);

                if (item.HasElements)
                {
                    LeerXml(nodoValidacion.Elementos, item.Elements());
                }
            }
        }

        #endregion Metodos Privados Estaticos

        #endregion Metodos
    }
}