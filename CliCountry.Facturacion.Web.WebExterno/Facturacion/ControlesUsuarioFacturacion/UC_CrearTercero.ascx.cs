// --------------------------------
// <copyright file="UC_CrearTercero.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Clases;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.Facturacion.Web.WebExterno.Properties;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Basicas;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_CrearTercero.
    /// </summary>
    public partial class UC_CrearTercero : WebUserControl
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The INFORMACIONTERCERO
        /// </summary>
        private const string INFORMACIONTERCERO = "InformacionTercero";

        /// <summary>
        /// The TERCERO
        /// </summary>
        private const string TERCERO = "Tercero";

        /// <summary>
        /// The TIPOOPERACION
        /// </summary>
        private const string TIPOOPERACION = "TipoOperacion";

        #endregion Constantes 

        #endregion Declaraciones Locales 

        #region Delegados y Eventos 

        #region Delegados 

        /// <summary>
        /// Delegado para el manejo del Evento de Afectación de Un Tercero
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="tipoOperacion">Tipo de Operación ejecutada</param>
        /// <param name="NombreTercero">Nombre del Tercero recién creado o recién modificado</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public delegate void OnAfectacionTercero(object sender, Global.TipoOperacion tipoOperacion, string NombreTercero);

        #endregion Delegados 
        #region Eventos 

        /// <summary>
        /// Evento de Afectación de Un Tercero.
        /// Dispara evento en el control UC_BuscarTercero después de cada inserción/modificación exitosa
        /// </summary>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public event OnAfectacionTercero AfectacionTercero;

        #endregion Eventos 

        #endregion Delegados y Eventos 

        #region Propiedades 

        #region Propiedades Publicas 

        /// <summary>
        /// Obtiene o establece tercero
        /// </summary>
        public Tercero Tercero
        {
            get
            {
                return ViewState[TERCERO] != null ? ViewState[TERCERO] as Tercero : new Tercero();
            }

            set
            {
                ViewState[TERCERO] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece tipo operacion
        /// </summary>
        public Global.TipoOperacion TipoOperacion
        {
            get
            {
                return ViewState[TIPOOPERACION] != null ? (Global.TipoOperacion)ViewState[TIPOOPERACION] : Global.TipoOperacion.CREACION;
            }

            set
            {
                ViewState[TIPOOPERACION] = value;
            }
        }

        #endregion Propiedades Publicas 
        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece informacion tercro
        /// </summary>
        private InformacionBasicaTercero InformacionTercero
        {
            get
            {
                return ViewState[INFORMACIONTERCERO] as InformacionBasicaTercero;
            }

            set
            {
                ViewState[INFORMACIONTERCERO] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo de Cargar Controles.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarControles()
        {
            switch (this.TipoOperacion)
            {
                case Global.TipoOperacion.CREACION:
                    CargarControlesEvento(false);
                    break;

                case Global.TipoOperacion.MODIFICACION:
                    CargarControlesEvento(true);
                    break;
            }
        }

        /// <summary>
        /// Inhabilita los campos al mostrar el detalle del tercero en modo consulta.
        /// </summary>
        /// <param name="activo">If set to <c>true</c> [activo].</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 05/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void DeshabilitarControles(bool activo, Global.TipoOperacion tipoOperacion)
        {
            if (tipoOperacion == Global.TipoOperacion.MODIFICACION)
            {
                lblCampoID.Enabled = activo;
                txtNombre.Enabled = !activo;
                txtNroDocumento.Enabled = !activo;
                DdlTipoPersona.Enabled = !activo;
                DdlTipoDocumento.Enabled = !activo;
                lblCampoDigitoVerificacion.Enabled = activo;
                ImgGuardar.Enabled = !activo;
            }
            else
            {
                lblCampoID.Enabled = activo;
                txtNombre.Enabled = activo;
                txtNroDocumento.Enabled = activo;
                DdlTipoPersona.Enabled = activo;
                DdlTipoDocumento.Enabled = activo;
                lblCampoDigitoVerificacion.Enabled = activo;
                ImgGuardar.Enabled = activo;
            }
        }

        /// <summary>
        /// Metodo de condición para establecer el tipo de documento como Nit.
        /// </summary>
        /// <param name="condicion">if set to <c>true</c> [condicion].</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 05/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void EstablecerTipoDocumentoJuridico(bool condicion)
        {
            if (condicion)
            {
                DdlTipoPersona.SelectedValue = Settings.Default.TipoPersona_Juridica;
                CargarComboTipoDocumento(DdlTipoPersona.SelectedValue);
                DdlTipoDocumento.SelectedValue = Resources.GlobalWeb.General_ValorUno;
                DdlTipoPersona.Enabled = false;
                DdlTipoDocumento.Enabled = false;
                revNroDocumento.Enabled = true;
            }
        }

        /// <summary>
        /// Configura el formulario para realizar la edición de un tercero seleccionado.
        /// </summary>
        /// <param name="terceroEditar">Objeto 'Tercero' a editar.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void HabilitarEdicionTercero(Tercero terceroEditar)
        {
            this.Tercero = terceroEditar;
            LimpiarCampos();
            CargarControles();
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 06/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void LimpiarCampos()
        {
            lblMensaje.Visible = false;
            lblCampoID.Text = "0";
            txtNombre.Text = string.Empty;
            txtNroDocumento.Text = string.Empty;
            txtNombre.ReadOnly = false;
            txtNroDocumento.ReadOnly = false;
            DdlTipoPersona.Enabled = true;
            DdlTipoDocumento.Enabled = true;
            DdlTipoDocumento.DataSource = null;
            DdlTipoDocumento.DataBind();
            lblCampoDigitoVerificacion.Text = string.Empty;
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Muestra los tipos de documento segun tipo de persona.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void DdlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarModal();
            DdlTipoPersona.Enabled = true;
            DdlTipoDocumento.Enabled = true;
            DdlTipoDocumento.DataSource = null;
            DdlTipoDocumento.DataBind();
            CargarComboTipoDocumento(DdlTipoPersona.SelectedValue);
        }

        /// <summary>
        /// Metodo para almacenar el tercero.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            lblMensaje.Visible = false;

            if (Page.IsValid)
            {
                if (this.TipoOperacion == Global.TipoOperacion.CREACION)
                {
                    GuardarTercero();                    
                }
                else if (this.TipoOperacion == Global.TipoOperacion.MODIFICACION)
                {
                    GuardarModificacionTercero();
                }                
            }
        }

        /// <summary>
        /// Evento para regresar a la vista general.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(SAHI.Comun.Utilidades.Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Metodo de renderizacion de la pagina.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 20/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnPreRender(EventArgs e)
        {
            if (InformacionTercero == null)
            {
                CargarCampos();
            }

            base.OnPreRender(e);
            txtNombre.Focus();
        }

        /// <summary>
        /// Metodo de carga de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 05/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Método para Recalcular el Dígito de Verificación cada vez que se cambia el # de Documento para personas Jurídicas.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void TxtNroDocumento_TextChanged(object sender, EventArgs e)
        {
            RecargarModal();

            // Si selecciona Tipo de Documento = NIT
            if (DdlTipoDocumento.SelectedValue == "1") 
            {
                this.lblCampoDigitoVerificacion.Text = SAHI.Comun.Utilidades.DigitoVerificacion.CalcularDigitoVerificacion(txtNroDocumento.Text);
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para realizar la carga de los campos.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarCampos()
        {
            lblMensaje.Visible = false;
            var resultado = WebService.Facturacion.ConsultarInformacionBasicaTercero();

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                InformacionTercero = resultado.Objeto;
                lblCampoRegimen.Text = resultado.Objeto.NombreRegimen;
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Carga los tipos de documento para el tercero.
        /// </summary>
        /// <param name="codigoGrupo">The codigo grupo.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarComboTipoDocumento(string codigoGrupo)
        {
            if (!codigoGrupo.Equals("-1"))
            {
                var listaTiposDocumento = WebService.Integracion.ConsultarTipoDocumento(new TipoDocumento()
                    {
                        IndCliente = null,
                        IndTercero = true,
                        IndHabilitado = true
                    });

                if (listaTiposDocumento.Ejecuto)
                {
                    var resultado = from item in listaTiposDocumento.Objeto
                                    where item.CodigoTipoPersona == codigoGrupo
                                    select item;

                    DdlTipoDocumento.DataSource = resultado;

                    DdlTipoDocumento.DataValueField = "Id";
                    DdlTipoDocumento.DataTextField = "Nombre";
                    DdlTipoDocumento.DataBind();
                    CargaObjetos.AdicionarItemPorDefecto(DdlTipoDocumento, false);

                    if (codigoGrupo == Settings.Default.TipoPersona_Juridica)
                    {
                        revNroDocumento.Enabled = true;
                        DdlTipoDocumento.SelectedValue = Resources.GlobalWeb.General_ValorUno;
                    }
                    else
                    {
                        revNroDocumento.Enabled = false;
                    }
                }
                else
                {
                    MostrarMensaje(listaTiposDocumento.Mensaje, TipoMensaje.Error);
                } 
            }
            else
            {
                DdlTipoDocumento.Items.Clear();
                DdlTipoDocumento.DataSource = null;
                DdlTipoDocumento.DataBind();
                DdlTipoDocumento.Enabled = false;
            }
        }

        /// <summary>
        /// Carga la lista de tipos de persona.
        /// </summary>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 04/07/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarComboTipoPersona()
        {
            var resultado = WebService.Integracion.ConsultarGenListas(new Basica { Id = 20, IndHabilitado = true }).Objeto;

            DdlTipoPersona.DataSource = resultado;
            DdlTipoPersona.DataValueField = "Codigo";
            DdlTipoPersona.DataTextField = "Nombre";
            DdlTipoPersona.DataBind();
            DdlTipoDocumento.DataSource = null;
            DdlTipoDocumento.DataBind();
            DdlTipoDocumento.SelectedIndex = -1;
            CargaObjetos.AdicionarItemPorDefecto(DdlTipoPersona, false);
        }

        /// <summary>
        /// Metodo para cargar controles por evento.
        /// </summary>
        /// <param name="modificacion">if set to <c>true</c> [modificacion].</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 22/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarControlesEvento(bool modificacion)
        {
            CargarComboTipoPersona();
            DeshabilitarControles(true, Global.TipoOperacion.CONSULTA);

            if (modificacion)
            {
                lblCampoID.Text = Tercero.Id.ToString();
                DdlTipoPersona.SelectedValue = Tercero.IdNaturaleza == 3 ? Settings.Default.TipoPersona_Juridica : Settings.Default.TipoPersona_Natural;
                CargarComboTipoDocumento(DdlTipoPersona.SelectedValue);
                DdlTipoDocumento.SelectedValue = Tercero.IdTipoDocumento.ToString();
                txtNombre.Text = Tercero.Nombre;
                txtNroDocumento.Text = Tercero.NumeroDocumento;
                lblCampoDigitoVerificacion.Text = Tercero.DigitoVerificacion.ToString();
                DeshabilitarControles(false, Global.TipoOperacion.MODIFICACION);
            }
        }

        /// <summary>
        /// Método para guardar la modificación de un tercero.
        /// </summary>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 31/07/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarModificacionTercero()
        {
            var calcularDigitoVerificacion = SAHI.Comun.Utilidades.DigitoVerificacion.CalcularDigitoVerificacion(txtNroDocumento.Text);

            Tercero tercero = new Tercero()
            {
                Id = Convert.ToInt32(lblCampoID.Text),
                DigitoVerificacion = Convert.ToInt32(calcularDigitoVerificacion),
                IdNaturaleza = DdlTipoPersona.SelectedValue == Settings.Default.TipoPersona_Juridica ? Convert.ToInt16(3) : Convert.ToInt16(4),
                IdTipoDocumento = Convert.ToByte(DdlTipoDocumento.SelectedValue),
                Nombre = txtNombre.Text,
                NumeroDocumento = txtNroDocumento.Text
            };

            Resultado<int> resultado = WebService.Facturacion.GuardarModificacionTercero(tercero);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format("Se ha modificado el Tercero con Id {0} satisfactoriamente", resultado.Objeto), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;

                LimpiarFormulario();

                // Disparar Evento en contenedor Padre
                if (AfectacionTercero != null)
                {
                    AfectacionTercero(this, this.TipoOperacion, tercero.Nombre);
                }
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Metodo para guardar el tercero.
        /// </summary>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 08/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void GuardarTercero()
        {
            var calcularDigitoVerificacion = SAHI.Comun.Utilidades.DigitoVerificacion.CalcularDigitoVerificacion(txtNroDocumento.Text);

            Tercero tercero = new Tercero()
            {
                CodigoRegimen = InformacionTercero.CodigoRegimen,
                DigitoVerificacion = Convert.ToInt32(calcularDigitoVerificacion),
                IdNaturaleza = DdlTipoPersona.SelectedValue == Settings.Default.TipoPersona_Juridica ? Convert.ToInt16(3) : Convert.ToInt16(4),
                IdTipoDocumento = Convert.ToByte(DdlTipoDocumento.SelectedValue),
                IdTipoTercero = InformacionTercero.IdTipoTercero,
                IndHabilitado = true,
                Nombre = txtNombre.Text,
                NumeroDocumento = txtNroDocumento.Text
            };

            Resultado<int> resultado = WebService.Facturacion.GuardarTercero(tercero);

            if (resultado.Ejecuto && string.IsNullOrEmpty(resultado.Mensaje))
            {
                MostrarMensaje(string.Format(Resources.ControlesUsuario.CrearTercero_MsjCreacion, resultado.Objeto), TipoMensaje.Ok);
                lblMensaje.CssClass = Resources.GlobalWeb.Estilo_MensajeOK;

                LimpiarFormulario();

                // Disparar Evento en contenedor Padre
                if (AfectacionTercero != null)
                {
                    AfectacionTercero(this, this.TipoOperacion, tercero.Nombre);
                }
            }
            else
            {
                MostrarMensaje(resultado.Mensaje, TipoMensaje.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario después de una inserción exitosa de un tercero.
        /// </summary>
        /// <remarks>
        /// Autor: Gemay Leandro Ocampo Pino - INTERGRUPO\gocampo
        /// FechaDeCreacion: 27/06/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void LimpiarFormulario()
        {
            DdlTipoPersona.SelectedIndex = 0;

            DdlTipoDocumento.Items.Clear();
            DdlTipoDocumento.DataSource = null;
            DdlTipoDocumento.DataBind();
            DdlTipoDocumento.Enabled = false;

            txtNombre.Text = String.Empty;
            txtNroDocumento.Text = String.Empty;
            lblCampoDigitoVerificacion.Text = String.Empty;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}