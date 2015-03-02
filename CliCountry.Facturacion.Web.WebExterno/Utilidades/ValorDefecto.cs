// --------------------------------
// <copyright file="ValorDefecto.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Utilidades
{
    using System;

    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.PropiedadGeneral
    /// </summary>
    public static class ValorDefecto
    {
        #region Declaraciones Locales

        #region Variables

        /// <summary>
        /// The estado activo
        /// </summary>
        private static string estadoActivo;

        /// <summary>
        /// The ind habilidado
        /// </summary>
        private static short indHabilidado;

        /// <summary>
        /// The indice defecto combo
        /// </summary>
        private static int indiceDefectoCombo = -1;

        /// <summary>
        /// The valor cero
        /// </summary>
        private static byte? valorCero;

        /// <summary>
        /// The valor defecto combo
        /// </summary>
        private static string valorDefectoCombo;

        /// <summary>
        /// The valor negativo
        /// </summary>
        private static short valorNegativo;

        #endregion Variables

        #endregion Declaraciones Locales

        #region Propiedades

        #region Propiedades Publicas

        /// <summary>
        /// Obtiene o establece valor cero
        /// </summary>
        public static byte Cero
        {
            get
            {
                if (!valorCero.HasValue)
                {
                    valorCero = Convert.ToByte(Resources.GlobalWeb.General_ValorCero);
                }

                return valorCero.Value;
            }
        }

        /// <summary>
        /// Obtiene o establece estado activo
        /// </summary>
        public static string EstadoActivo
        {
            get
            {
                if (string.IsNullOrEmpty(estadoActivo))
                {
                    estadoActivo = Convert.ToString(Resources.GlobalWeb.General_EstadoActivo);
                }

                return estadoActivo.ToString();
            }
        }

        /// <summary>
        /// Obtiene o establece ind habilidado
        /// </summary>
        public static short IndHabilidado
        {
            get
            {
                if (indHabilidado == 0)
                {
                    indHabilidado = 0;
                }

                return 1;
            }

            private set
            {
                ValorDefecto.indHabilidado = value;
            }
        }

        /// <summary>
        /// Obtiene o establece indice defecto combo
        /// </summary>
        public static int IndiceDefectoCombo
        {
            get
            {
                return indiceDefectoCombo;
            }
        }

        /// <summary>
        /// Obtiene o establece valor negativo
        /// </summary>
        public static short Negativo
        {
            get
            {
                valorNegativo = Convert.ToByte(Resources.GlobalWeb.General_ValorNegativo);

                return valorNegativo;
            }
        }

        /// <summary>
        /// Obtiene o establece valor defecto combo
        /// </summary>
        public static string ValorDefectoCombo
        {
            get
            {
                if (string.IsNullOrEmpty(valorDefectoCombo))
                {
                    valorDefectoCombo = Resources.GlobalWeb.General_ComboItemValor;
                }

                return valorDefectoCombo;
            }

            private set
            {
                ValorDefecto.valorDefectoCombo = value;
            }
        }

        #endregion Propiedades Publicas

        #endregion Propiedades
    }
}