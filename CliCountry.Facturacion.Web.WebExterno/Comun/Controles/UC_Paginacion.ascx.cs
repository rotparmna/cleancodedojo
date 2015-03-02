// --------------------------------
// <copyright file="UC_Paginacion.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun.Controles
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Utilidades;
    using CliCountry.SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.Controles.Paginacion
    /// </summary>
    public partial class UC_Paginacion : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The PAGINADOR
        /// </summary>
        private const string PAGINADOR = "Paginador";

        #endregion Constantes

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado para el evento de Seleccion
        /// </summary>
        /// <param name="e">Parámetro e.</param>
        public delegate void OnPageIndexChanged(EventoControles<Paginador> e);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de on page index_ change
        /// </summary>
        public event OnPageIndexChanged PageIndexChanged;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece data source
        /// </summary>
        public Paginador ObjetoPaginador
        {
            get
            {
                return this.Session[PAGINADOR] as Paginador;
            }

            set
            {
                if (this.Session[PAGINADOR] != value)
                {
                    this.Session[PAGINADOR] = value;
                    this.OnPropertyChanged("ObjetoPaginador");
                }
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Protegidos 

        /// <summary>
        /// ImgPagAnterior Click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Luis Fernando Quintero - Intergrupo 
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: Luis Fernando Quintero - Intergrupo FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        protected void ImgPagAnterior_Click(object sender, ImageClickEventArgs e)
        {
            this.ObjetoPaginador.PaginaActual -= 1;
            this.CambiarIndicePagina();
        }

        /// <summary>
        /// Metodo para controlar el Evento de Pagina Siguiente
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgPagSiguiente_Click(object sender, ImageClickEventArgs e)
        {
            this.ObjetoPaginador.PaginaActual += 1;
            this.CambiarIndicePagina();
        }

        /// <summary>
        /// Metodo para controlar el Evento de Primera Pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgPrimeraPag_Click(object sender, ImageClickEventArgs e)
        {
            this.ObjetoPaginador.PaginaActual = 0;
            this.CambiarIndicePagina();
        }

        /// <summary>
        /// Metodo para controlar el Evento de Ultima Pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs" /> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgUltimaPag_Click(object sender, ImageClickEventArgs e)
        {
            this.ObjetoPaginador.PaginaActual = this.ObjetoPaginador.CantidadPaginas - 1;
            this.CambiarIndicePagina();
        }

        /// <summary>
        /// Metodo de Cambio de pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void LnkCambioPagina_Click(object sender, EventArgs e)
        {
            this.RecargarModal();
            int salida = 0;
            var valor = hfPaginaCambio.Value;
            if (!string.IsNullOrEmpty(valor) && int.TryParse(valor, out salida))
            {
                this.ObjetoPaginador.PaginaActual = salida - 1;
                this.CambiarIndicePagina();
            }
            else
            {
                this.CambiarIndicePagina();
            }
        }

        /// <summary>
        /// Metodo de Carga de la Pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para invocar el evento
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CambiarIndicePagina()
        {
            this.RecargarModal();
            if (this.PageIndexChanged != null)
            {
                string controlPadre = this.Parent.ID;
                this.PageIndexChanged(new EventoControles<Paginador>(this, this.ObjetoPaginador));
            }
        }

        /// <summary>
        /// Metodo para configurar paginas
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ConfigurarBotonesPagina()
        {
            int valorInicial = 0;
            int maximoBotones = this.ObjetoPaginador.MaximoPaginas;

            if (this.ObjetoPaginador.CantidadPaginas > 0)
            {
                this.ConfigurarCampos(true);
                maximoBotones = this.ObjetoPaginador.CantidadPaginas < maximoBotones ? this.ObjetoPaginador.CantidadPaginas : maximoBotones;

                if (this.ObjetoPaginador.PaginaActual + 1 > maximoBotones)
                {
                    valorInicial += (this.ObjetoPaginador.PaginaActual + 1) - maximoBotones;
                    maximoBotones = valorInicial > this.ObjetoPaginador.CantidadPaginas ? this.ObjetoPaginador.CantidadPaginas : maximoBotones + valorInicial;
                }

                this.CrearBotonesPaginacion(valorInicial, maximoBotones);
                lblPaginaActual.InnerText = string.Format(Resources.GlobalWeb.Paginador_ControlPaginas, this.ObjetoPaginador.PaginaActual + 1, this.ObjetoPaginador.CantidadPaginas);
                LblTotalRegistros.InnerText = string.Format(Resources.GlobalWeb.Paginador_TotalRegistros, this.ObjetoPaginador.TotalRegistros);
            }
            else
            {
                this.ConfigurarCampos(false);
            }
        }

        /// <summary>
        /// Metodo para configurar campos
        /// </summary>
        /// <param name="carga">if set to <c>true</c> [carga].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ConfigurarCampos(bool carga)
        {
            if (carga)
            {
                this.controlPaginas.Visible = true;
                ImgPagAnterior.Enabled = !(this.ObjetoPaginador.PaginaActual == 0);
                ImgPrimeraPag.Enabled = !(this.ObjetoPaginador.PaginaActual == 0);
                ImgPagSiguiente.Enabled = !(this.ObjetoPaginador.PaginaActual == this.ObjetoPaginador.CantidadPaginas - 1);
                ImgUltimaPag.Enabled = !(this.ObjetoPaginador.PaginaActual == this.ObjetoPaginador.CantidadPaginas - 1);
            }
            else
            {
                ImgPagAnterior.Enabled = false;
                ImgPrimeraPag.Enabled = false;
                ImgPagSiguiente.Enabled = false;
                ImgUltimaPag.Enabled = false;
                this.controlPaginas.Visible = false;
                lblPaginaActual.InnerText = string.Format(Resources.GlobalWeb.Paginador_ControlPaginas, 0, 0);
                LblTotalRegistros.InnerText = string.Format(Resources.GlobalWeb.Paginador_TotalRegistros, 0);
            }
        }

        /// <summary>
        /// Metodo para controlar la adicion de paginas en el control
        /// </summary>
        /// <param name="valorInicial">The valor inicial.</param>
        /// <param name="maximoBotones">The maximo botones.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 09/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CrearBotonesPaginacion(int valorInicial, int maximoBotones)
        {
            this.controlPaginas.Controls.Clear();

            for (int i = valorInicial; i < maximoBotones; i++)
            {
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.InnerText = (i + 1).ToString();
                anchor.Attributes.Add("onclick", string.Format("CambioPagina_Click('{0}','{1}')", anchor.InnerText, LnkCambioPagina.ClientID));

                if (i == this.ObjetoPaginador.PaginaActual)
                {
                    anchor.Attributes.Add("class", "paginaseleccionada");
                }
                else
                {
                    anchor.Attributes.Add("class", "paginaNormal");
                }

                this.controlPaginas.Controls.Add(anchor);
            }
        }

        /// <summary>
        /// Metodo para validar el cambio de la propiedad
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "ObjetoPaginador")
            {
                this.ConfigurarBotonesPagina();
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}