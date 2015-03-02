// --------------------------------
// <copyright file="UC_DefinirCondiciones.ascx.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion
{
    using System;
    using System.Web.UI;
    using CliCountry.Facturacion.Web.WebExterno.Comun.Paginas;
    using CliCountry.SAHI.Comun.Utilidades;
    using CliCountry.SAHI.Dominio.Entidades;
    using CliCountry.SAHI.Dominio.Entidades.Facturacion;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Facturacion.ControlesUsuarioFacturacion.UC_DefinirCondiciones.
    /// </summary>
    public partial class UC_DefinirCondiciones : WebUserControl
    {
        #region Metodos 

        #region Metodos Publicos 

        /// <summary>
        /// Metodo para realizar la carga de datos iniciales.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 10/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        public void CargarDatosPaginaDefinirCondiciones(TipoFacturacion tipoFacturacion)
        {
            TxtContrato.Text = VinculacionSeleccionada.Contrato.Nombre;
            TxtEntidad.Text = VinculacionSeleccionada.Tercero.Nombre;
            TxtAtencion.Text = VinculacionSeleccionada.IdAtencion.ToString();
            TxtPlan.Text = VinculacionSeleccionada.Plan.Nombre;
            CargarOpcionesPorTipoFacturacion(tipoFacturacion);
        }

        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo para Definir Condiciones de Cubrimiento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgCondicionCubrimiento_Click(object sender, ImageClickEventArgs e)
        {
            ucCondicionesCubrimientos.LimpiarCampos();
            var condicionCubrimiento = new CondicionCubrimiento()
            {
                Cubrimiento = new Cubrimiento()
                {
                    Tercero = new Tercero()
                    {
                        Id = VinculacionSeleccionada.Tercero.Id,
                        Nombre = VinculacionSeleccionada.Tercero.Nombre
                    },

                    Contrato = new Contrato()
                    {
                        Id = VinculacionSeleccionada.Contrato.Id,
                        Nombre = VinculacionSeleccionada.Contrato.Nombre
                    },

                    Plan = new Plan()
                    {
                        Id = VinculacionSeleccionada.Plan.Id,
                        Nombre = VinculacionSeleccionada.Plan.Nombre
                    },

                    IdAtencion = VinculacionSeleccionada.IdAtencion
                },
            };

            ucCondicionesCubrimientos.CargarTiposComponentes();
            ucCondicionesCubrimientos.VisualizarConfiguracion = false;
            ucCondicionesCubrimientos.PrecargueInformacion(condicionCubrimiento);
            mpeCondicionesCubrimientos.Show();
        }

        /// <summary>
        /// LLama a condiciones facturacion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgCondicionFacturacion_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarCondicionesFacturacion.LimpiarCampos();
            ucBuscarCondicionesFacturacion.VisualizarConfiguracion = false;
            ucBuscarCondicionesFacturacion.CargarDatosControles();
            mpeBuscarCondicionesFacturacion.Show();
        }

        /// <summary>
        /// Llama a condiciones tarifas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgCondicionTarifa_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarCondicionesTarifas.LimpiarCampos();
            ucBuscarCondicionesTarifas.CargarDatosControles();
            mpeBuscarCondicionesTarifas.Show();
        }

        /// <summary>
        /// Muestra el control precargado de definición de cubrimientos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex Mattos - INTERGRUPO\amattos
        /// FechaDeCreacion: 12/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgDefinirCubrimiento_Click(object sender, ImageClickEventArgs e)
        {
            ucDefinirCubrimientos.LimpiarCampos();
            var cubrimiento = new Cubrimiento()
            {
                Tercero = new Tercero()
                {
                    Id = VinculacionSeleccionada.Tercero.Id,
                    Nombre = VinculacionSeleccionada.Tercero.Nombre
                },

                Contrato = new Contrato()
                {
                    Id = VinculacionSeleccionada.Contrato.Id,
                    Nombre = VinculacionSeleccionada.Contrato.Nombre
                },

                Plan = new Plan()
                {
                    Id = VinculacionSeleccionada.Plan.Id,
                    Nombre = VinculacionSeleccionada.Plan.Nombre
                },

                IdAtencion = VinculacionSeleccionada.IdAtencion
            };

            ucDefinirCubrimientos.CargarTiposComponentes();
            ucDefinirCubrimientos.PrecargueInformacion(cubrimiento);
            mpeDefinirCubrimientos.Show();
        }

        /// <summary>
        /// Definir Exclusiones.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgDefinirExclusiones_Click(object sender, ImageClickEventArgs e)
        {
            ucDefinirExclusiones.LimpiarCampos();
            ucDefinirExclusiones.CargarTiposComponentes();
            ucDefinirExclusiones.PrecargueInformacion();
            mpeDefinirExclusiones.Show();
        }

        /// <summary>
        /// Click Descuentos.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs" /> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgDescuentos_Click(object sender, ImageClickEventArgs e)
        {
            ucBuscarDescuentos.LimpiarCampos();
            ucBuscarDescuentos.ModoPantalla = Resources.GlobalWeb.Facturacion_Modo_Pantalla;
            ucBuscarDescuentos.CargarDatosControles(new Tercero());
            mpeBuscarDescuentos.Show();
        }

        /// <summary>
        /// Metodo para Recargar el Modal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 21/05/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void ImgRegresar_Click(object sender, ImageClickEventArgs e)
        {
            ResultadoEjecucion(Global.TipoOperacion.SALIR);
        }

        /// <summary>
        /// Metodo para regresar de Descuentos.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected override void OnInit(EventArgs e)
        {
            ucBuscarDescuentos.OperacionEjecutada += BuscarDescuentos_OperacionEjecutada;
            ucBuscarCondicionesTarifas.OperacionEjecutada += BuscarCondicionesTarifas_OperacionEjecutada;
            ucBuscarCondicionesFacturacion.OperacionEjecutada += BuscarCondicionesFacturacion_OperacionEjecutada;
            base.OnInit(e);
        }

        /// <summary>
        /// Cargue de la pagina.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// FechaDeCreacion: 18/04/2013
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
        /// Metodo de condiciones de Facturacion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarCondicionesFacturacion_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
        }

        /// <summary>
        /// Metodo para  Operacion Ejecutada Condiciones de Tarifa.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarCondicionesTarifas_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
        }

        /// <summary>
        /// Metodo para realizar la Busqueda del Descuento.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tipoOperacion">The tipo operacion.</param>
        /// Autor: Alex David Mattos R. - INTERGRUPO\amattos
        /// <remarks>
        /// Autor: (Nombre del Autor y Usuario del dominio)
        /// FechaDeCreacion: (dd/MM/yyyy)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void BuscarDescuentos_OperacionEjecutada(object sender, Global.TipoOperacion tipoOperacion)
        {
            RecargarModal();
        }

        /// <summary>
        /// Carga las opciones por tipo de facturación.
        /// </summary>
        /// <param name="tipoFacturacion">The tipo facturacion.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarOpcionesPorTipoFacturacion(TipoFacturacion tipoFacturacion)
        {
            switch (tipoFacturacion)
            {
                case TipoFacturacion.FacturacionActividad:
                    DeshabilitarOpciones(true);
                    break;

                case TipoFacturacion.FacturacionPaquete:
                    DeshabilitarOpciones(false);
                    break;
            }
        }

        /// <summary>
        /// Deshabilita o Habilita las opciones segun tipo facturación.
        /// </summary>
        /// <param name="resultado">If set to <c>true</c> [resultado].</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 11/06/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void DeshabilitarOpciones(bool resultado)
        {
            LblCondicionFacturacion.Visible = resultado;
            imgCondicionFacturacion.Visible = resultado;
            LblDefinirCubrimiento.Visible = resultado;
            imgDefinirCubrimiento.Visible = resultado;
            LblCondicionCubrimiento.Visible = resultado;
            imgCondicionCubrimiento.Visible = resultado;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}