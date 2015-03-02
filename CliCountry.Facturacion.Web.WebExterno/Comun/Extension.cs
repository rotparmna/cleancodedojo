// --------------------------------
// <copyright file="Extension.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun
{
    using System.Net;
    using System.ServiceModel;
    using CliCountry.SAHI.Comun.AuditoriaBase;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Comun.ObtenerUsuarioWCF
    /// </summary>
    public static class Extension
    {
        #region Metodos

        #region Metodos Publicos Estaticos

        /// <summary>
        /// Adicionar Encabezado Soap.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="nombreUsuario">The nombre usuario.</param>
        /// <remarks>
        /// Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta
        /// FechaDeCreacion: 26/08/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Adicionar Encabezado Soap.
        /// </remarks>
        public static void AdicionarEncabezadoSoap(this OperationContextScope scope, string nombreUsuario)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                nombreUsuario = null;
            }

            IPHostEntry equipo;
            string direccionIPLocal = string.Empty;
            equipo = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in equipo.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    direccionIPLocal = ip.ToString();
                }
            }

            scope.AdicionarDatosUsuarioEncabezadoWcf(nombreUsuario, direccionIPLocal);
        }

        /// <summary>
        /// Obtiene la direccion IP del equipo local.
        /// </summary>
        /// <returns>Direccion IP</returns>
        /// <remarks>
        /// Autor: Edson Joel Nieto Ardila - INTERGRUPO\enieto
        /// FechaDeCreacion: 29/08/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static string ObtenerDireccionIP()
        {
            IPHostEntry equipo;
            string direccionIPLocal = string.Empty;
            equipo = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in equipo.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    direccionIPLocal = ip.ToString();
                }
            }

            return direccionIPLocal;
        }

        #endregion Metodos Publicos Estaticos

        #endregion Metodos
    }
}