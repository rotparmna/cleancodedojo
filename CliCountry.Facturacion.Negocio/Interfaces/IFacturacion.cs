// --------------------------------
// <copyright file="IFacturacion.cs" company="InterGrupo S.A.">
//     COPYRIGHT(C) 2013, Intergrupo S.A
// </copyright>
// <summary>Interfaz IFacturacion.</summary>
// --------------------------------
namespace CliCountry.Facturacion.Negocio.Interfaces
{
    using System.Collections.Generic;
    using CliCountry.Facturacion.Dominio.Entidades;
    using SAHI.Comun.Utilidades;
    using SAHI.Dominio.Entidades;
    using SAHI.Dominio.Entidades.Facturacion;
    using SAHI.Dominio.Entidades.Facturacion.Auditoria;
    using SAHI.Dominio.Entidades.Facturacion.Ventas;
    using SAHI.Dominio.Entidades.Productos;

    /// <summary>
    /// Interfaz IFacturacion.
    /// </summary>
    public interface IFacturacion
    {
        #region Metodos 

        /// <summary>
        ///     Metodo para realizar la actualización dela Atencion.
        /// </summary>
        /// <param name="atencionCliente">The atencion cliente.</param>
        /// <returns>Indica el Resultado de la Actualizacion.</returns>
        /// <remarks>
        ///     Autor: David Mauricio Gutiérrez Ruiz - INTERGRUPO\dgutierrez.
        ///     FechaDeCreacion: 25/06/2013.
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy).
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        ///     Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<bool> ActualizarAtencion(AtencionCliente atencionCliente);

        /// <summary>
        ///     Método para actualizar la vinculación.
        /// </summary>
        /// <param name="vinculacion">The vinculacion.</param>
        /// <returns>
        ///     Registro modificado.
        /// </returns>
        /// <remarks>
        ///     Autor: Iván José Pimienta Serrano - INTERGRUPO\Ipimienta.
        ///     FechaDeCreacion: 22/05/2013.
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy).
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        ///     Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<int> ActualizarVinculacion(Vinculacion vinculacion);

        /// <summary>
        ///     Permite Consultar los planes o contratos en general.
        /// </summary>
        /// <param name="paginacion">The atencion.</param>
        /// <returns>
        ///     Lista de Datos.
        /// </returns>
        /// <remarks>
        ///     Autor: Alex David Mattos rodriguez - INTERGRUPO\amattos.
        ///     FechaDeCreacion: 28/04/2013.
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy).
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        ///     Descripción: Descripción detallada del metodo, procure especificar todo el metodo aqui.
        /// </remarks>
        Resultado<Paginacion<List<ContratoPlan>>> ConsultarContratoPlan(Paginacion<ContratoPlan> paginacion);

        /// <summary>
        ///     Permite Consultar Depositos Asociados a una Atención.
        /// </summary>
        /// <param name="atencion">The atencion.</param>
        /// <returns>
        ///     Lista de depositos.
        /// </returns>
        /// <remarks>
        ///     Autor: Jorge Arturo Cortes - INTERGRUPO\jcortesm.
        ///     FechaDeCreacion: 24/06/2013.
        ///     UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio).
        ///     FechaDeUltimaModificacion: (dd/MM/yyyy).
        ///     EncargadoSoporte: (Nombre del Autor - Usuario del dominio).
        ///     Descripción: Permite Consultar conceptos asociados a una atención.
        /// </remarks>
        Resultado<List<Deposito>> ConsultarDepositos(Atencion atencion);

        #endregion Metodos 
    }
}
