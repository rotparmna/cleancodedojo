// --------------------------------
// <copyright file="UC_BuscarClienteTercero.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Dominio.Entidades;
    using SAHI.Comun.Utilidades;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_BuscarClienteTercero
    /// </summary>
    public partial class UC_BuscarClienteTercero : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The JURIDICA
        /// </summary>
        private const string JURIDICA = "Condicion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece condicion juridica
        /// </summary>
        public bool Condicion
        {
            get
            {
                return (bool)ViewState[JURIDICA];
            }

            set
            {
                ViewState[JURIDICA] = value;
            }
        }

        #endregion Propiedades Publicas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para Limpiar Campos
        /// </summary>
        /// <param name="cliente">if set to <c>true</c> [cliente].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 23/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public void LimpiarCampos(bool cliente)
        {
            if (cliente)
            {
                TxtTercero.Text = string.Empty;
                LblNombreTercero.Text = string.Empty;
                LblNroDocumentoTercero.Text = string.Empty;
            }
            else
            {
                TxtCliente.Text = string.Empty;
                LblNombres.Text = string.Empty;
                LblApellidos.Text = string.Empty;
                LblNroDocumentoCliente.Text = string.Empty;
            }
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo del Evento CLick de Cargar Cliente
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BtnCargarCliente_Click(object sender, EventArgs e)
        {
            LblMensaje.Visible = false;

            if (!string.IsNullOrEmpty(TxtCliente.Text) && TxtCliente.Text != "0")
            {
                var resultado = WebService.Integracion.ConsultarClientes(new Paginacion<Cliente>()
                {
                    Item = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(TxtCliente.Text),
                        IndHabilitado = true
                    }
                });

                if (resultado.Ejecuto == true && resultado.Objeto.Item.Count == 1)
                {
                    CargarDatosCliente(resultado.Objeto.Item.FirstOrDefault());
                }
                else
                {
                    MostrarMensaje(string.Format(Resources.ControlesUsuario.TerceroCliente_NoExiste, TxtCliente.Text), TipoMensaje.Error);
                    LimpiarControles(false);
                }
            }
        }

        /// <summary>
        /// Metodo del Evento CLick de CargarTercero
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BtnCargarTercero_Click(object sender, EventArgs e)
        {
            LblMensaje.Visible = false;

            if (!string.IsNullOrEmpty(TxtTercero.Text) && TxtTercero.Text != "0")
            {
                var resultado = WebService.Integracion.ConsultarTercero(new Paginacion<Tercero>()
                {
                    Item = new Tercero()
                    {
                        Id = Convert.ToInt32(TxtTercero.Text),
                        IndHabilitado = true
                    }
                });

                if (resultado.Ejecuto == true && resultado.Objeto.Item.Count == 1)
                {
                    CargarDatosTercero(resultado.Objeto.Item.FirstOrDefault());
                }
                else
                {
                    MostrarMensaje(string.Format(Resources.ControlesUsuario.TerceroCliente_NoExiste, TxtTercero.Text), TipoMensaje.Error);
                    LimpiarControles(true);
                }
            }
        }

        /// <summary>
        /// Metodo para controlar la operacion ejecutada
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarCliente_OperacionEjecutada(object sender, SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case SAHI.Comun.Utilidades.Global.TipoOperacion.CONSULTA:
                    RecargarModal();
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccionar Cliente
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarCliente_SeleccionarItemGrid(SAHI.Comun.Utilidades.EventoControles<SAHI.Dominio.Entidades.Cliente> e)
        {
            if (e.Resultado != null)
            {
                CargarDatosCliente(e.Resultado);
            }
            else
            {
                LimpiarControles(false);
            }
        }

        /// <summary>
        /// Metodo para controlar la operacion ejecutada
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarTercero_OperacionEjecutada(object sender, SAHI.Comun.Utilidades.Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case SAHI.Comun.Utilidades.Global.TipoOperacion.CONSULTA:
                    RecargarModal();
                    break;
            }
        }

        /// <summary>
        /// Metodo de Seleccionar Tercero
        /// </summary>
        /// <param name="e">The e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void BuscarTercero_SeleccionarItemGrid(SAHI.Comun.Utilidades.EventoControles<SAHI.Dominio.Entidades.Tercero> e)
        {
            if (e.Resultado != null)
            {
                CargarDatosTercero(e.Resultado);
            }
            else
            {
                LimpiarControles(true);
            }
        }

        /// <summary>
        /// Metodo para cargar el popup de Cliente
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarCliente.LimpiarCampos();
            LblMensaje.Visible = false;
            mpeCliente.Show();
        }

        /// <summary>
        /// Metodo para cargar el tercero
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void ImgBuscarTercero_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarTercero.LimpiarCampos();
            ucBuscarTercero.CondicionJuridica = Condicion;
            LblMensaje.Visible = false;
            mpeTercero.Show();
        }

        /// <summary>
        /// Metodo de inicializacion de la pagina
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarTercero.OperacionEjecutada += BuscarTercero_OperacionEjecutada;
            ucBuscarTercero.SeleccionarItemGrid += BuscarTercero_SeleccionarItemGrid;
            ucBuscarCliente.OperacionEjecutada += BuscarCliente_OperacionEjecutada;
            ucBuscarCliente.SeleccionarItemGrid += BuscarCliente_SeleccionarItemGrid;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo de carga de la pagina
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
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
        /// Metodo para cargar datos del Cliente
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarDatosCliente(Cliente cliente)
        {
            TxtCliente.Text = cliente.IdCliente.ToString();
            LblNombres.Text = cliente.Nombre;
            LblApellidos.Text = cliente.Apellido;
            LblNroDocumentoCliente.Text = cliente.NumeroDocumento;
            LimpiarCampos(true);
        }

        /// <summary>
        /// Metodo para cargar datos del tercero
        /// </summary>
        /// <param name="tercero">The tercero.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void CargarDatosTercero(Tercero tercero)
        {
            TxtTercero.Text = tercero.Id.ToString();
            LblNombreTercero.Text = tercero.Nombre;
            LblNroDocumentoTercero.Text = tercero.NumeroDocumento;
            LimpiarCampos(false);
        }

        /// <summary>
        /// Metodo para limpiar los controles
        /// </summary>
        /// <param name="tercero">if set to <c>true</c> [tercero].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void LimpiarControles(bool tercero)
        {
            if (tercero)
            {
                TxtTercero.Text = string.Empty;
                LblNombreTercero.Text = string.Empty;
                LblNroDocumentoTercero.Text = string.Empty;
            }
            else
            {
                TxtCliente.Text = string.Empty;
                LblNombres.Text = string.Empty;
                LblApellidos.Text = string.Empty;
                LblNroDocumentoCliente.Text = string.Empty;
            }
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}