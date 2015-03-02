// --------------------------------
// <copyright file="CargaObjetos.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Clases
{
    using System;
    using System.Collections.ObjectModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using CliCountry.SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Clases.CargaObjetos
    /// </summary>
    public class CargaObjetos
    {
        #region Metodos 

        #region Metodos Publicos Estaticos 

        /// <summary>
        /// Método para adicionar el item por defecto
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="valorSeleccionado">if set to <c>true</c> [valor seleccionado].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz
        /// FechaDeCreacion: (30/01/2013)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Método para adicionar el item por defecto
        /// </remarks>
        public static void AdicionarItemPorDefecto(ComboBox combo, bool valorSeleccionado)
        {
            combo.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Resources.GlobalWeb.General_ComboItemTexto, Resources.GlobalWeb.General_ComboItemValor));

            if (!valorSeleccionado)
            {
                combo.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Método para adicionar el item por defecto
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="valorSeleccionado">if set to <c>true</c> [valor seleccionado].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz
        /// FechaDeCreacion: (30/01/2013)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Método para adicionar el item por defecto
        /// </remarks>
        public static void AdicionarItemPorDefecto(DropDownList combo, bool valorSeleccionado)
        {
            combo.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Resources.GlobalWeb.General_ComboItemTexto, Resources.GlobalWeb.General_ComboItemValor));

            if (!valorSeleccionado)
            {
                combo.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Metodo para realizar la carga del combo de tipo de producto
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="tipoProductos">The tipo productos.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void CargarComboTipoProducto(DropDownList combo, ObservableCollection<TipoProducto> tipoProductos)
        {
            combo.DataSource = tipoProductos;
            combo.DataValueField = "IdTipoProducto";
            combo.DataTextField = "Nombre";
            combo.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(combo, false);
        }

        /// <summary>
        /// Metodo para realizar la carga del combo de tipo de producto.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="tipoProductos">The tipo productos.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static void CargarComboTipoProducto(ComboBox combo, ObservableCollection<TipoProducto> tipoProductos)
        {
            combo.DataSource = tipoProductos;
            combo.DataValueField = "IdTipoProducto";
            combo.DataTextField = "Nombre";
            combo.DataBind();
            CargaObjetos.AdicionarItemPorDefecto(combo, false);
        }

        /// <summary>
        /// Metodo para Configurar Los botones de la Grilla
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <param name="grilla">The grilla.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 24/11/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void ConfigurarBotonGrilla(Page pagina, byte tipoOperacion, GridView grilla)
        {
            string codigoJs = string.Format("ConfigurarBotonesGrilla('{0}','{1}');", grilla.ID, tipoOperacion);
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), codigoJs, true);
        }

        /// <summary>
        /// Metodo para Configurar Botones de la Grilla
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="grilla">The grilla.</param>
        /// <param name="valor">The valor.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 25/11/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void ConfigurarBotonGrilla(Page pagina, GridView grilla, int valor)
        {
            string codigoJs = string.Format("ConfigurarConsultaVentas('{0}','{1}');", grilla.ID, valor);
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), codigoJs, true);
        }

        /// <summary>
        /// Metodo para realizar consulta de Ordenamiento de Grilla
        /// </summary>
        /// <param name="grilla">The grilla.</param>
        /// <param name="dataSource">The data source.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 11/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void OrdenamientoGrilla(GridView grilla, object dataSource)
        {
            grilla.DataSource = dataSource;
            grilla.DataBind();

            if (grilla.Rows.Count > 0)
            {
                grilla.UseAccessibleHeader = true;
                grilla.HeaderRow.TableSection = TableRowSection.TableHeader;
                grilla.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        /// <summary>
        /// Metodo de Ordenamiento de la Grilla
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="grilla">The grilla.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 25/11/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void OrdenamientoGrilla(Page pagina, GridView grilla)
        {
            if (grilla.Rows.Count > 0)
            {
                grilla.UseAccessibleHeader = true;
                grilla.HeaderRow.TableSection = TableRowSection.TableHeader;
                grilla.FooterRow.TableSection = TableRowSection.TableFooter;
                string codigoJs = string.Format("OrdenarGrilla('{0}');", grilla.ID);
                ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), grilla.ID, codigoJs, true);
            }
        }

        /// <summary>
        /// Metodo para generar el ordenamiento de los Controles de Usuario
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="grilla">The grilla.</param>
        /// <param name="dataSource">The data source.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 19/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static void OrdenamientoGrilla(Page pagina, GridView grilla, object dataSource)
        {
            grilla.DataSource = dataSource;
            grilla.DataBind();

            if (grilla.Rows.Count > 0)
            {
                grilla.UseAccessibleHeader = true;
                grilla.HeaderRow.TableSection = TableRowSection.TableHeader;
                grilla.FooterRow.TableSection = TableRowSection.TableFooter;
                string codigoJs = string.Format("OrdenarGrilla('{0}');", grilla.ID);
                ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), grilla.ID, codigoJs, true);
            }
        }

        #endregion Metodos Publicos Estaticos 
        #region Metodos Publicos 

        /// <summary>
        /// Metodo encargado de aplicar los permisos a los botones de las paginas.
        /// </summary>
        /// <param name="pagina">The pagina.</param>
        /// <param name="permisos">The permisos.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 20/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public static void AplicarPermisos(Page pagina, string permisos)
        {
            string codigoJs = string.Format(Resources.GlobalWeb.General_ConfigurarPermisos, permisos);
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), string.Empty, codigoJs, true);
        }

        #endregion Metodos Publicos 

        #endregion Metodos 
    }
}