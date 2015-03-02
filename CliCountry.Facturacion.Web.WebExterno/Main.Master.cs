// --------------------------------
// <copyright file="Main.Master.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using CliCountry.Facturacion.Negocio.Controlador;
using CliCountry.Facturacion.Web.WebExterno.Comun.Controles;
using CliCountry.Facturacion.Web.WebExterno.Properties;
using CliCountry.SAHI.Dominio.Entidades;
using CliCountry.SAHI.Dominio.Entidades.Menu;
using CliCountry.SAHI.Dominio.Entidades.Seguridad;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Main
    /// </summary>
    public partial class Main : System.Web.UI.MasterPage
    {
        #region Declaraciones Locales 

        #region Constantes 

        /// <summary>
        /// The ROLUSUARIOACTUAL
        /// </summary>
        private const string PERMISOPAGINA = "PERMISOPAGINA";

        /// <summary>
        /// The ROLUSUARIOACTUAL
        /// </summary>
        private const string ROLUSUARIOACTUAL = "ROLUSUARIOACTUAL";

        /// <summary>
        /// The USUARIO
        /// </summary>
        private const string USUARIO = "Usuario";

        #endregion Constantes 
        #region Variables 

   

        #endregion Variables 

        #endregion Declaraciones Locales 

        #region Propiedades 

        #region Propiedades Privadas 

        /// <summary>
        /// Obtiene o establece modulos
        /// </summary>
        private List<PermisoRol> ListaPermisosRol
        {
            get
            {
                var roles = Session["PERMISOROLESUSUARIO"] as List<PermisoRol>;
                return roles != null ? roles : new List<PermisoRol>();
            }

            set
            {
                Session["PERMISOROLESUSUARIO"] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece modulos
        /// </summary>
        private List<Rol> ListaRolesUsuario
        {
            get
            {
                var roles = Session["ROLESUSUARIO"] as List<Rol>;
                return roles != null ? roles : new List<Rol>();
            }

            set
            {
                Session["ROLESUSUARIO"] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece menu cargado.
        /// </summary>
        /// <value>
        /// Valor bool.
        /// </value>
        private bool MenuCargado
        {
            get
            {
                if (ViewState["ModulosCargados"] == null)
                {
                    return false;
                }

                bool modCargados;
                if (!bool.TryParse(ViewState["ModulosCargados"].ToString(), out modCargados))
                {
                    modCargados = false;
                }

                return modCargados;
            }

            set
            {
                ViewState["ModulosCargados"] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece permiso rol usuario
        /// </summary>
        private List<PermisoRol> PermisoRolUsuario
        {
            get
            {
                return Session[ROLUSUARIOACTUAL] == null ? new List<PermisoRol>() : Session[ROLUSUARIOACTUAL] as List<PermisoRol>;
            }

            set
            {
                Session[ROLUSUARIOACTUAL] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece permiso pagina
        /// </summary>
        private List<PermisosPaginaUsuario> PermisosPaginaUsuarioAux
        {
            get
            {
                return Session[PERMISOPAGINA] == null ? new List<PermisosPaginaUsuario>() : Session[PERMISOPAGINA] as List<PermisosPaginaUsuario>;
            }

            set
            {
                Session[PERMISOPAGINA] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece permisos usuario agrupado.
        /// </summary>
        /// <value>
        /// Tipo Dato Resultado.
        /// </value>
        private Resultado<List<PermisosPaginaUsuario>> PermisosUsuarioAgrupado
        {
            get
            {
                if (Session["PERMISOSUSUARIOAGRUPADO"] == null)
                {
                    Session["PERMISOSUSUARIOAGRUPADO"] = new Resultado<List<PermisosPaginaUsuario>>();
                }

                return Session["PERMISOSUSUARIOAGRUPADO"] as Resultado<List<PermisosPaginaUsuario>>;
            }

            set
            {
                Session["PERMISOSUSUARIOAGRUPADO"] = value;
            }
        }

        /// <summary>
        /// Obtiene o establece roles por usuario.
        /// </summary>
        /// <value>
        /// Tipo dato Resultado.
        /// </value>
        private Resultado<List<Rol>> RolesPorUsuario
        {
            get
            {
                if (Session["ROLESPORUSUARIO"] == null)
                {
                    Session["ROLESPORUSUARIO"] = new Resultado<List<Rol>>();
                }

                return Session["ROLESPORUSUARIO"] as Resultado<List<Rol>>;
            }

            set
            {
                Session["ROLESPORUSUARIO"] = value;
            }
        }

        #endregion Propiedades Privadas 

        #endregion Propiedades 

        #region Metodos 

        #region Metodos Publicos 
        
        #endregion Metodos Publicos 
        #region Metodos Protegidos 

        /// <summary>
        /// Metodo encargado de finalizar la sesion en la aplicacion
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MenuEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 20/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void MenuFacturacion_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "Salir")
            {
                if (Session["idAtencion"] != null)
                {
                    Negocio.Controlador.ControlFacturacion neg = new Negocio.Controlador.ControlFacturacion();
                    var bloqueo = neg.ActualizarBloquearAtencion(Convert.ToInt32(Session["idAtencion"]), Context.User.Identity.Name);
                }

                Logout();
            }
        }

        /// <summary>
        /// Metodo que se ejecuta al momento de cargar la pagina
        /// </summary>
        /// <param name="sender">Objeto sender</param>
        /// <param name="e">Eventos del cargue</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 30/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            //    {
            //        lblInfoUsuario.InnerText = HttpContext.Current.User.Identity.Name;

            //        Usuario usuario = new Usuario() { CodigoUsuario = HttpContext.Current.User.Identity.Name };

            //        if (!this.MenuCargado)
            //        {
            //            IEnumerable<Modulo> modulos = CargarMenu(usuario);
            //            RolesPorUsuario = segClient.ConsultarRolesPorUsuario(usuario.CodigoUsuario);
            //            CargarRolesUsuario(usuario);
            //            PermisosUsuarioAgrupado = segClient.ConsultarPermisosUsuarioAgrupado(usuario.CodigoUsuario, true, Convert.ToInt32(Settings.Default.ModuloWeb_IdModulo));
            //            CargarMenu(modulos, usuario);
            //        }

            //        if (!ValidarPermisoIngresoURL(usuario, HttpContext.Current.Request.Url.AbsolutePath))
            //        {
            //            if (!ValidarPermisosEspeciales())
            //            {
            //                if (this.Page.Request.UrlReferrer == null || this.Page.Request.UrlReferrer.AbsolutePath != "/NoAutorizado.aspx")
            //                {
            //                    this.Page.Response.Redirect("~/NoAutorizado.aspx");
            //                    Session.Remove(Resources.GlobalWeb.General_Session_PERMISOPAGINAUSUARIO);
            //                }
            //            }
            //        }

            //        if (this.Page.Request.UrlReferrer == null || this.Page.Request.UrlReferrer.AbsolutePath != "/NoAutorizado.aspx" || this.Page.Request.UrlReferrer.AbsolutePath == "/Configuracion/AdministrarTiposMovimientos.aspx")
            //        {
            //            if (HttpContext.Current.Request.Url.AbsolutePath == "/Configuracion/AdministrarTiposMovimientos.aspx")
            //            {
            //                if (!this.ValidaRoles())
            //                {
            //                    this.Page.Response.Redirect("~/NoAutorizado.aspx");
            //                    Session.Remove(Resources.GlobalWeb.General_Session_PERMISOPAGINAUSUARIO);
            //                }
            //            }
            //        }

            //        PermisosPaginaUsuario(usuario);
            //        ValidarMenuPrincipal();
            //    }
            //    else if (string.IsNullOrEmpty(lblInfoUsuario.InnerText) && HttpContext.Current.User.Identity.IsAuthenticated)
            //    {
            //        lblInfoUsuario.InnerText = HttpContext.Current.User.Identity.Name;
            //    }
            //}

            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    menuUsuario.Items.Clear();
            //}
        }

        /// <summary>
        /// Metodo encargado de realizar la aplicacion y consulta de permisos en la pagina.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 20/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        protected void PermisosPaginaUsuario(Usuario usuario)
        {
            var request = HttpContext.Current.Request.Url.AbsolutePath.Substring(1, HttpContext.Current.Request.Url.AbsolutePath.Length - 1);
            PermisosPaginaUsuario permisosUsuario = null;

            if (PermisosPaginaUsuarioAux.Count > 0)
            {
                if (request != "Bienvenida.aspx")
                {
                    permisosUsuario = FiltrarPagina(PermisosPaginaUsuarioAux);

                    if (permisosUsuario != null)
                    {
                        Session[Resources.GlobalWeb.General_Session_PERMISOPAGINAUSUARIO] = permisosUsuario.IndNuevo.ToString() + "," + permisosUsuario.IndModificar.ToString() + "," + permisosUsuario.IndConsultar.ToString() + "," + permisosUsuario.IndEliminar.ToString();
                    }
                }
            }
            else
            {
                if (request != "Bienvenida.aspx")
                {
                    if (PermisosUsuarioAgrupado.Ejecuto && PermisosUsuarioAgrupado.Objeto != null)
                    {
                        if (PermisosUsuarioAgrupado.Objeto.Count > 0)
                        {
                            PermisosPaginaUsuarioAux = PermisosUsuarioAgrupado.Objeto.ToList();
                            permisosUsuario = FiltrarPagina(PermisosPaginaUsuarioAux);

                            if (permisosUsuario != null)
                            {
                                Session[Resources.GlobalWeb.General_Session_PERMISOPAGINAUSUARIO] = permisosUsuario.IndNuevo.ToString() + "," + permisosUsuario.IndModificar.ToString() + "," + permisosUsuario.IndConsultar.ToString() + "," + permisosUsuario.IndEliminar.ToString() + "," + permisosUsuario.IndImprimir.ToString();
                            }
                        }
                        else
                        {
                            MostrarMensaje(Resources.GlobalWeb.General_NoExistenPermisos, Comun.Paginas.WebPage.TipoMensaje.Error);
                            return;
                        }
                    }
                    else
                    {
                        MostrarMensaje(Resources.GlobalWeb.General_ObjetoNulo, Comun.Paginas.WebPage.TipoMensaje.Error);
                        return;
                    }
                }
            }
        }

        #endregion Metodos Protegidos 
        #region Metodos Privados 

        /// <summary>
        /// Metodo para cargar menu de cartera.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns>Lista de módulos.</returns>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 20/03/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private IEnumerable<Modulo> CargarMenu(Usuario usuario)
        {
            IEnumerable<Modulo> ret = new List<Modulo>();
            //segClient = new ControlSeguridad();
            //var resultado = segClient.ConsultarMenuUsuario(new ConsultaMenu()
            //{
            //    Usuario = usuario.CodigoUsuario,
            //    IndHabilitado = true,
            //    Modulo = ConsultaMenu.ModuloWeb.Facturacion
            //});

            //if (resultado.Ejecuto)
            //{
            //    this.MenuCargado = resultado.Objeto.Count > 0;
            //    ret = resultado.Objeto.ToList();
            //}
            //else
            //{
            //    MostrarMensaje(resultado.Mensaje, Comun.Paginas.WebPage.TipoMensaje.Error);
            //}

            return ret;
        }

        /// <summary>
        /// Metodo para realizar la Carga del Menu.
        /// </summary>
        /// <param name="modulos">The modulos.</param>
        /// <param name="usuario">The usuario.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 01/10/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private void CargarMenu(IEnumerable<Modulo> modulos, Usuario usuario)
        {
            var menuBase = CargarMenuPrincipal(modulos);

            MenuItem menuInicio = new MenuItem("Inicio", string.Empty, string.Empty, "~/Bienvenida.aspx");
            menuUsuario.Items.Add(menuInicio);

            // var permisos = from
            //                   item in PermisosUsuario.Objeto
            //               where
            //                   item.IdModuloWeb == Convert.ToInt32(Settings.Default.ModuloWeb_IdModulo)
            //               select
            //                   item;
            var permisos = this.PermisosUsuarioAgrupado.Objeto;

            foreach (var item in menuBase)
            {
                MenuItem menuItem = new MenuItem()
                {
                    Text = item.NombreModulo,
                    ToolTip = item.NombreModulo
                };

                item.SubItems = CargarSubMenu(modulos, item.IdNodo);

                foreach (var subItem in item.SubItems)
                {
                    MenuItem subMenuItem = new MenuItem()
                    {
                        Text = subItem.NombreModulo,
                        NavigateUrl = subItem.Url
                    };

                    var existe = from
                                     itemP in permisos
                                 where
                                     itemP.DescripcionRol == subItem.NombreModulo
                                 select
                                     itemP;

                    if (existe.Count() > 0)
                    {
                        menuItem.ChildItems.Add(subMenuItem);
                    }
                }

                menuUsuario.Items.Add(menuItem);
            }

            menuInicio = new MenuItem("Salir", "Salir");
            menuUsuario.Items.Add(menuInicio);
        }

        /// <summary>
        /// Metodo para Obtener el Primer Nivel del menu
        /// </summary>
        /// <param name="modulos">The modulos.</param>
        /// <returns>Lista de Modulos Principales</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private List<NodoMenu> CargarMenuPrincipal(IEnumerable<Modulo> modulos)
        {
            var resultado = from
                                item in modulos
                            group item by new
                            {
                                Id = item.IdModuloPadre,
                                NombreModulo = item.NombreModulo
                            }

                                into mods
                                select new NodoMenu()
                                {
                                    IdNodo = mods.Key.Id,
                                    NombreModulo = mods.Key.NombreModulo,
                                    NodoPrincipal = true
                                };

            return resultado == null || resultado.Count() == 0 ? new List<NodoMenu>() : resultado.ToList();
        }

        /// <summary>
        /// Cargar Roles Usuario
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <remarks>
        /// Autor: Aura Victoria Forero Varela - INTERGRUPO\aforero 
        /// FechaDeCreacion: 13/02/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui)
        /// </remarks>
        private void CargarRolesUsuario(Usuario usuario)
        {
            try
            {
                if (RolesPorUsuario.Ejecuto)
                {
                    if (RolesPorUsuario.Objeto != null && RolesPorUsuario.Objeto.Count > 0)
                    {
                        Session[Resources.GlobalWeb.Session_RolesUsuario] = RolesPorUsuario.Objeto.ToList();
                        ListaRolesUsuario = RolesPorUsuario.Objeto.ToList();
                    }
                }
                else
                {
                    throw new Exception(RolesPorUsuario.Mensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para obtener Lista de Modulos dependientes
        /// </summary>
        /// <param name="modulos">The modulos.</param>
        /// <param name="identificadorModuloPadre">The id modulo raiz.</param>
        /// <returns>
        /// Lista de modulos dependientes
        /// </returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private List<NodoMenu> CargarSubMenu(IEnumerable<Modulo> modulos, byte identificadorModuloPadre)
        {
            var resultado = from
                                item in modulos
                            where
                                item.IdModuloPadre == identificadorModuloPadre
                            group item by new
                            {
                                Id = item.IdModuloPadre,
                                NombreOpcion = item.DescripcionRol,
                                Url = item.Url
                            }

                                into opcion
                                select new NodoMenu()
                                {
                                    IdNodo = opcion.Key.Id,
                                    NombreModulo = opcion.Key.NombreOpcion,
                                    Url = opcion.Key.Url
                                };

            return resultado == null || resultado.Count() == 0 ? new List<NodoMenu>() : resultado.ToList();
        }

        /// <summary>
        /// Metodo para filtrar permisos del usuario
        /// </summary>
        /// <param name="permisos">The permisos.</param>
        /// <returns>Retorna Permisos.</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        private PermisosPaginaUsuario FiltrarPagina(IEnumerable<PermisosPaginaUsuario> permisos)
        {
            var resultado = from
                                item in PermisosPaginaUsuarioAux
                            where
                                item.Url == HttpContext.Current.Request.Url.AbsolutePath.Substring(1, HttpContext.Current.Request.Url.AbsolutePath.Length - 1)
                            select new PermisosPaginaUsuario
                            {
                                IndConsultar = item.IndConsultar,
                                IndEliminar = item.IndEliminar,
                                IndImprimir = item.IndImprimir,
                                IndModificar = item.IndModificar,
                                IndNuevo = item.IndNuevo,
                                Url = item.Url
                            };

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Metodo encargado de limpiar la sesion
        /// </summary>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 20/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void Logout()
        {
            ////TODO: desbloquear
            Session.Clear();
            menuUsuario.Items.Clear();
            FormsAuthentication.SignOut();
            this.Page.Response.Redirect(FormsAuthentication.LoginUrl);
        }

        /// <summary>
        /// Metodo para visulzair el mensaje
        /// </summary>
        /// <param name="mensaje">The mensaje.</param>
        /// <param name="clase">The clase.</param>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 30/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void MostrarMensaje(string mensaje, Comun.Paginas.WebPage.TipoMensaje clase)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                mensaje = mensaje.Replace("'", string.Empty);

                short identificadorTipo = 0;
                string estilo = string.Empty;

                switch (clase)
                {
                    case Comun.Paginas.WebPage.TipoMensaje.Ok:
                        estilo = Resources.GlobalWeb.Estilo_MensajeOK;
                        break;

                    case Comun.Paginas.WebPage.TipoMensaje.Error:
                        estilo = Resources.GlobalWeb.Estilo_MensajeERROR;
                        identificadorTipo = 1;
                        break;
                }

                var dialogo = form1.FindControl("Dialogo") as Dialogo;

                if (dialogo != null)
                {
                    dialogo.MostrarMensaje(this.Page, mensaje, estilo, identificadorTipo);
                }
            }
        }

        /// <summary>
        /// Metodo encargado de quitar los menus que no tengan opciones para el usuario
        /// </summary>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 23/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private void ValidarMenuPrincipal()
        {
            var menu = menuUsuario;
            int cantidadItems = menu.Items.Count - 2;

            for (int i = cantidadItems; i > 1; i--)
            {
                if (menu.Items[i].ChildItems.Count == 0)
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Valida rol para ingreso.
        /// </summary>
        /// <returns>True si valida roles.</returns>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto 
        /// FechaDeCreacion: 01/05/2014
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: (Descripción detallada del metodo, procure especificar todo el metodo aqui).
        /// </remarks>
        private bool ValidaRoles()
        {
            bool valida = false;

            var permisoRol = this.ListaRolesUsuario.FindAll(c => c.CodigoAlfanumerico == "BILFA0" || c.CodigoAlfanumerico == "SISADM").ToList();

            if (permisoRol.Count > 0)
            {
                valida = true;
            }

            return valida;
        }

        /// <summary>
        /// Valida el ingreso de la url desde el explorador
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <param name="url">The URL.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 13/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private bool ValidarPermisoIngresoURL(Usuario usuario, string url)
        {
            IEnumerable<PermisosPaginaUsuario> permisos = this.PermisosUsuarioAgrupado.Objeto;
            bool valida = false;
            string urlIngresada = url.Substring(1, url.Length - 1);

            if (PermisosUsuarioAgrupado.Ejecuto)
            {
                if (permisos != null && permisos.Count() > 0)
                {
                    if (permisos.Count() > 0)
                    {
                        Session["ValidaMenu"] = true;

                        if (urlIngresada.ToLower() != "bienvenida.aspx" && urlIngresada.ToLower() != "default.aspx")
                        {
                            var buscarUrl = from
                                                item in permisos
                                            where
                                                item.Url == urlIngresada
                                            select
                                                item;

                            if (buscarUrl.Count() > 0)
                            {
                                valida = true;
                            }
                        }
                        else
                        {
                            valida = true;
                        }
                    }
                }
            }
            else
            {
                MostrarMensaje("no lo muestra", Comun.Paginas.WebPage.TipoMensaje.Error);
            }

            return valida;
        }

        /// <summary>
        /// Metodo para validar pagina de acceso
        /// </summary>
        /// <returns>Indica si la pagina es valida</returns>
        /// <remarks>
        /// Autor: David Mauricio Gutierrez Ruiz - INTERGRUPO\dgutierrez
        /// FechaDeCreacion: 26/12/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        private bool ValidarPermisosEspeciales()
        {
            var resultado = from
                                item in Properties.Settings.Default.PermisosEspeciales.Split(',')
                            where
                                item.ToUpper() == this.Page.Request.Url.AbsolutePath.ToUpper()
                            select
                                item;

            return resultado.Count() > 0 ? true : false;
        }

        #endregion Metodos Privados 

        #endregion Metodos 
    }
}