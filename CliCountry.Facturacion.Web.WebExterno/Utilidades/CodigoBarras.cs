// --------------------------------
// <copyright file="CodigoBarras.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Clase Codigo Barras.</summary>
// ---------------------------------
namespace CliCountry.Facturacion.Web.WebExterno.Utilidades
{
    /// <summary>
    /// Clase CliCountry.Facturacion.Web.WebExterno.Utilidades.CodigoBarras
    /// </summary>
    public static class CodigoBarras
    {
        /// <summary>
        /// Convertir texto a código de barras 128B
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <param name="largo">The largo.</param>
        /// <param name="ancho">The ancho.</param>
        /// <returns>Resultado operacion.</returns>
        /// <remarks>
        /// Autor: Jorge Arturo Cortes Murcia - INTERGRUPO\Jcortesm
        /// FechaDeCreacion: 19/09/2013
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: (Nombre del Autor - Usuario del dominio)
        /// Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui
        /// </remarks>
        public static byte[] GenerarCodigoBarrasFacturacion(string texto, int largo, int ancho)
        {
            BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
            type = BarcodeLib.TYPE.CODE128B;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode(texto, type);
            b.IncludeLabel = false;
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            b.Width = largo;
            b.Height = ancho;
            b.Encode(type, texto);
            byte[] byteImage = b.Encoded_Image_Bytes;

            return byteImage;
        }
    }
}