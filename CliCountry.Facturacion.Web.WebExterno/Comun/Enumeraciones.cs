// --------------------------------
// <copyright file="Enumeraciones.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Comun
{
    /// <summary>
    /// Tipos de mensajes para mostrar en pantalla al usuario
    /// </summary>
    public enum TipoMensaje
    {
        /// <summary>
        /// Mensajes de error
        /// </summary>
        ERROR,

        /// <summary>
        /// Mensajes de aviso
        /// </summary>
        AVISO,

        /// <summary>
        /// Mensajes informativos
        /// </summary>
        INFORMACION
    }

    /// <summary>
    /// Tipo de estado que se aplica a una exclusión.
    /// </summary>
    public enum TipoExclusion
    {
        /// <summary>
        /// Indica si la exclusión es de Contrato.
        /// </summary>
        Manual,

        /// <summary>
        /// Indica si la exclusión es de Manual.
        /// </summary>
        Contrato
    }

    /// <summary>
    /// Tipo de relación que se aplica a un proceso.
    /// </summary>
    public enum TipoRelacion : int
    {
        /// <summary>
        /// The no definido
        /// </summary>
        NoDefinido = 0,

        /// <summary>
        /// The ajuste sobre tarifa
        /// </summary>
        AjusteSobreTarifa = 1,

        /// <summary>
        /// The valor maximo
        /// </summary>
        ValorMaximo = 2,

        /// <summary>
        /// The valor propio
        /// </summary>
        ValorPropio = 3,

        /// <summary>
        /// The valor maximo porcentaje
        /// </summary>
        ValorMaximoPorcentaje = 4,

        /// <summary>
        /// The cantidad
        /// </summary>
        Cantidad = 5,

        /// <summary>
        /// The no cubrimiento
        /// </summary>
        NoCubrimiento = 6
    }

    /// <summary>
    ///     Propiedad enumerado para evaluar el tipo de Reporte a generar.
    /// </summary>
    public enum TipoReporte
    {
        /// <summary>
        ///     Actividades Agrupado.
        /// </summary>
        ActividadesAgrupado = 1,

        /// <summary>
        ///     Actividades Detallado.
        /// </summary>
        ActividadesDetallado = 2,

        /// <summary>
        ///     Paquetes Agrupado.
        /// </summary>
        PaquetesAgrupado = 3,

        /// <summary>
        ///     Paquetes Detallado.
        /// </summary>
        PaquetesDetallado = 4
    }
}