// --------------------------------
// <copyright file="UC_CrearCliente.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Localizacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearCliente.
    /// </summary>
    public partial class UC_CrearCliente : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The CARGAINICIAL.
        /// </summary>
        private const string CARGAINICIAL = "CargaInicial";

        /// <summary>
        /// The TEXTO.
        /// </summary>
        private const string TEXTO = "Nombre";

        /// <summary>
        /// The TIPOBUSQUEDA.
        /// </summary>
        private const string TIPOBUSQUEDA = "TipoBusqueda";

        /// <summary>
        /// The ID.
        /// </summary>
        private const string VALOR = "Id";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Enumeraciones 

        /// <summary>
        /// Enumeracion de Tipo de Busqueda.
        /// </summary>
        private enum TipoBusqueda
        {
            /// <summary>
            /// The expedicion.
            /// </summary>
            Expedicion,

            /// <summary>
            /// The ciudad nacimiento.
            /// </summary>
            CiudadNacimiento,

            /// <summary>
            /// The ciudad residencia.
            /// </summary>
            CiudadResidencia
        }

        #endregion Enumeraciones 

        #region Propiedades 

        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece carga inicial.
        /// </summary>
        private bool CargaInicial
        {
            get
            {
                return ViewState[CARGAINICIAL] == null ? false : Convert.ToBoolean(ViewState[CARGAINICIAL]);
            }

            set
            {
                ViewState[CARGAINICIAL] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece tipo busqueda ejecutada.
        /// </summary>
        private TipoBusqueda TipoBusquedaEjecutada
        {
            get
            {
                return ViewState[TIPOBUSQUEDA] == null ? TipoBusqueda.Expedicion : (TipoBusqueda)Enum.Parse(typeof(TipoBusqueda), ViewState[TIPOBUSQUEDA].ToString());
            }

            set
            {
                ViewState[TIPOBUSQUEDA] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

/// <summary>
        /// Metodo para cargar campos.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarCampos()
        {
            RanFechaNacimiento.MaximumValue = DateTime.Now.ToShortDateString();
            RanFechaNacimiento.MinimumValue = new DateTime(1900, 1, 1).ToShortDateString();
            CargarTipoDocumento();
            CargarGenero();
            CargarOcupacion();
            CargarEstadoCivil();
            CargarReligion();
            CargarNivel();
            CargarTipoAfilicion();
            CargarSede();
            CargarZona();
            PreseleccionarNivel();
        }

        /// <summary>
        /// Metodo para limpiar Campos.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            lblMensaje.Visible = false;
            TxtAnios.Text = string.Empty;
            TxtApellidos.Text = string.Empty;
            TxtDirOficina.Text = string.Empty;
            TxtDirResidencia.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtFechaNacimiento.Text = string.Empty;
            TxtNombres.Text = string.Empty;
            TxtNroDocumento.Text = string.Empty;
            TxtTelOficina.Text = string.Empty;
            TxtTelResidencia.Text = string.Empty;
            TxtExpedicion.Text = string.Empty;
            TxtCiudadNacimiento.Text = string.Empty;
            TxtBarrio.Text = string.Empty;
            DdlEstadoCivil.SelectedIndex = 0;
            DdlGenero.SelectedIndex = 0;
            DdlNivel.SelectedIndex = 0;
            DdlOcupacion.SelectedIndex = 0;
            DdlReligion.SelectedIndex = 0;
            DdlSede.SelectedIndex = 0;
            DdlTipoAfiliacion.SelectedIndex = 0;
            DdlTipoDocumento.SelectedIndex = 0;
            DdlZona.SelectedIndex = 0;
            PreseleccionarNivel();
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para Controlar Operacion Ejecutada.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BuscarPaisDptoCiudad_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            switch (tipoOperacion)
            {
                case Global.TipoOperacion.SALIR:
                    RecargarModal();
                    multi.ActiveViewIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Metodo para Seleccionar Item del Grid.
        /// </summary>
        /// <param name="e">Parametro e.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void BuscarPaisDptoCiudad_SeleccionarItemGrid(EventoControles<Pais> e)
        {
            RecargarModal();
            multi.ActiveViewIndex = 0;
            if (e.Resultado != null)
            {
                CargarCiudadSeleccionada(e.Resultado);
            }
        }

        /// <summary>
        /// Metodo para Buscar Ciudad Nacimiento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarCiudadNacimiento_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarPaisDptoCiudad.LimpiarCampos();
            RecargarModal();
            TipoBusquedaEjecutada = TipoBusqueda.CiudadNacimiento;
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Metodo para Buscar Ciudad de Residencia.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarCiudadResidencia_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarPaisDptoCiudad.LimpiarCampos();
            RecargarModal();
            TipoBusquedaEjecutada = TipoBusqueda.CiudadResidencia;
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Metodo para Buscar Ciudad Expedicion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgBuscarExpedicion_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarPaisDptoCiudad.LimpiarCampos();
            RecargarModal();
            TipoBusquedaEjecutada = TipoBusqueda.Expedicion;
            multi.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Metodo para guardar el Cliente.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgGuardar_Click(object sender, ImageClickEventArgs e)
        {
            lblMensaje.Visible = false;
            RecargarModal();
            Page.Validate(RfvTipoDocumento.ValidationGroup);

            if (Page.IsValid)
            {
                var cliente = ClienteCreado();
                var resultado = WebService.Facturacion.GuardarCliente(cliente);

                if (resultado.Ejecuto && resultado.Objeto > 0)
                {
                    LblId.Text = resultado.Objeto.ToString();
                    MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearCliente_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
                }
                else
                {
                    MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
                }
            }
        }

        /// <summary>
        /// Metodo de Regresar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(CliCountry.SAHI.Comun.Utilidades.Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Metodo de Inicializacion de la Pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarPaisDptoCiudad.SeleccionarItemGrid += BuscarPaisDptoCiudad_SeleccionarItemGrid;
            ucBuscarPaisDptoCiudad.OperacionEjecutada += BuscarPaisDptoCiudad_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Metodo de Carga de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 27/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para cargar la Ciudad Seleccionada.
        /// </summary>
        /// <param name="pais">The pais.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarCiudadSeleccionada(Pais pais)
        {
            switch (TipoBusquedaEjecutada)
            {
                case TipoBusqueda.Expedicion:
                    HfIdExpedicion.Value = pais.Departamento.Ciudad.IdLugar.ToString();
                    TxtExpedicion.Text = pais.Departamento.Ciudad.Nombre;
                    break;

                case TipoBusqueda.CiudadNacimiento:
                    HfIdCiudadNacimiento.Value = pais.Departamento.Ciudad.IdLugar.ToString();
                    TxtCiudadNacimiento.Text = pais.Departamento.Ciudad.Nombre;
                    break;

                case TipoBusqueda.CiudadResidencia:
                    HfIdBarrio.Value = pais.Departamento.Ciudad.IdLugar.ToString();
                    TxtBarrio.Text = pais.Departamento.Ciudad.Nombre;
                    break;
            }
        }

        /// <summary>
        /// Metodo para Cargar Estado Civil.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarEstadoCivil()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "Civi" });

            if (respuesta.Ejecuto)
            {
                DdlEstadoCivil.DataTextField = TEXTO;
                DdlEstadoCivil.DataValueField = VALOR;
                DdlEstadoCivil.DataSource = respuesta.Objeto;
                DdlEstadoCivil.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlEstadoCivil, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para Cargar genero.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarGenero()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "sexo" });

            if (respuesta.Ejecuto)
            {
                DdlGenero.DataTextField = TEXTO;
                DdlGenero.DataValueField = VALOR;
                DdlGenero.DataSource = respuesta.Objeto;
                DdlGenero.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlGenero, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Nivel Categoria.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarNivel()
        {
            var respuesta = WebService.Integracion.ConsultarNiveles(new Nivel() { IndHabilitado = true });

            if (respuesta.Ejecuto)
            {
                DdlNivel.DataTextField = TEXTO;
                DdlNivel.DataValueField = VALOR;
                DdlNivel.DataSource = respuesta.Objeto;
                DdlNivel.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlNivel, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar ocupaciones.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarOcupacion()
        {
            var respuesta = WebService.Integracion.ConsultarOcupaciones(new Ocupacion() { IndHabilitado = true });

            if (respuesta.Ejecuto)
            {
                DdlOcupacion.DataTextField = TEXTO;
                DdlOcupacion.DataValueField = VALOR;
                DdlOcupacion.DataSource = respuesta.Objeto;
                DdlOcupacion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlOcupacion, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Religion.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarReligion()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "religion" });

            if (respuesta.Ejecuto)
            {
                DdlReligion.DataTextField = TEXTO;
                DdlReligion.DataValueField = VALOR;
                DdlReligion.DataSource = respuesta.Objeto;
                DdlReligion.DataBind();
                DdlReligion.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para Cargar Sede.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarSede()
        {
            var respuesta = WebService.Integracion.ConsultarSede(new Sede() { IndHabilitado = true });

            if (respuesta.Ejecuto)
            {
                DdlSede.DataTextField = TEXTO;
                DdlSede.DataValueField = VALOR;
                DdlSede.DataSource = respuesta.Objeto;
                DdlSede.DataBind();
                DdlSede.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar Tipo de Afiliacion.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarTipoAfilicion()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "afil" });

            if (respuesta.Ejecuto)
            {
                DdlTipoAfiliacion.DataTextField = TEXTO;
                DdlTipoAfiliacion.DataValueField = VALOR;
                DdlTipoAfiliacion.DataSource = respuesta.Objeto;
                DdlTipoAfiliacion.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlTipoAfiliacion, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para Cargar Tipo de Documento.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarTipoDocumento()
        {
            var respuesta = WebService.Integracion.ConsultarTipoDocumento(new TipoDocumento() { IndHabilitado = true, IndCliente = true });

            if (respuesta.Ejecuto)
            {
                DdlTipoDocumento.DataTextField = TEXTO;
                DdlTipoDocumento.DataValueField = VALOR;
                DdlTipoDocumento.DataSource = respuesta.Objeto;
                DdlTipoDocumento.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlTipoDocumento, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar zonas.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarZona()
        {
            var respuesta = WebService.Integracion.ConsultarGenListas(new Basica() { IndHabilitado = true, CodigoGrupo = "zona" });

            if (respuesta.Ejecuto)
            {
                DdlZona.DataTextField = TEXTO;
                DdlZona.DataValueField = VALOR;
                DdlZona.DataSource = respuesta.Objeto;
                DdlZona.DataBind();
                CargaObjetos.AdicionarItemPorDefecto(DdlZona, false);
            }
            else
            {
                MostrarMensaje(respuesta.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para crear cliente.
        /// </summary>
        /// <returns>
        /// Retorna Cliente.
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 28/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private Cliente ClienteCreado()
        {
            var estadoCivil = DdlEstadoCivil.SelectedIndex != -1 && DdlEstadoCivil.SelectedValue != "-1" ? Convert.ToInt16(DdlEstadoCivil.SelectedValue) : 0;
            var genero = DdlGenero.SelectedIndex != -1 && DdlGenero.SelectedValue != "-1" ? Convert.ToInt16(DdlGenero.SelectedValue) : 0;
            var nivelCategoria = DdlNivel.SelectedIndex != -1 && DdlNivel.SelectedValue != "-1" ? Convert.ToByte(DdlNivel.SelectedValue) : 0;
            var ocupacion = DdlOcupacion.SelectedIndex != -1 && DdlOcupacion.SelectedValue != "-1" ? Convert.ToInt16(DdlOcupacion.SelectedValue) : 0;
            var religion = DdlReligion.SelectedIndex != -1 && DdlReligion.SelectedValue != "-1" ? Convert.ToInt16(DdlReligion.SelectedValue) : 0;
            var sede = DdlSede.SelectedIndex != -1 && DdlSede.SelectedValue != "-1" ? Convert.ToInt16(DdlSede.SelectedValue) : 0;
            var tipoAfiliacion = DdlTipoAfiliacion.SelectedIndex != -1 && DdlTipoAfiliacion.SelectedValue != "-1" ? Convert.ToInt16(DdlTipoAfiliacion.SelectedValue) : 0;
            var tipoDocumento = DdlTipoDocumento.SelectedIndex != -1 && DdlTipoDocumento.SelectedValue != "-1" ? Convert.ToInt16(DdlTipoDocumento.SelectedValue) : 0;
            var zona = DdlZona.SelectedIndex != -1 && DdlZona.SelectedValue != "-1" ? Convert.ToInt16(DdlZona.SelectedValue) : 0;

            var cliente = new Cliente()
            {
                Apellido = TxtApellidos.Text,
                CorreoElectronico = TxtEmail.Text,
                DireccionCasa = TxtDirResidencia.Text,
                DireccionOficina = TxtDirOficina.Text,
                FechaNacimiento = Convert.ToDateTime(TxtFechaNacimiento.Text),
                IdAfiliaciontipo = Convert.ToInt16(tipoAfiliacion),
                IdEstadoCivil = Convert.ToInt16(estadoCivil),
                IdLugarDocumento = Convert.ToInt32(HfIdExpedicion.Value),
                IdLugarNacimiento = Convert.ToInt32(HfIdCiudadNacimiento.Value),
                IdLugarCliente = Convert.ToInt32(HfIdBarrio.Value),
                IdNivel = Convert.ToByte(nivelCategoria),
                IdOcupacion = Convert.ToInt16(ocupacion),
                IdRegimenAfiliacion = 0,
                IdReligion = religion,
                IdSede = sede,
                IdSexo = Convert.ToInt16(genero),
                IdTipoCliente = 0,
                IdTipoDocumento = Convert.ToByte(tipoDocumento),
                IdZona = Convert.ToInt16(zona),
                IndHabilitado = true,
                Nombre = TxtNombres.Text,
                NumeroDocumento = TxtNroDocumento.Text,
                TelefonoCasa = TxtTelResidencia.Text,
                TelefonoOficina = TxtTelOficina.Text,
                Usuario = Context.User.Identity.Name
            };

            return cliente;
        }

        /// <summary>
        /// Metodo para preseleccionar Nivel.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 29/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void PreseleccionarNivel()
        {
            var nivel = DdlNivel.Items.FindByText(Properties.Settings.Default.Nivel_Cliente);
            DdlNivel.SelectedValue = nivel.Value;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}