﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CliCountry.Facturacion.Negocio.Comun.Recursos {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Constantes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Constantes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CliCountry.Facturacion.Negocio.Comun.Recursos.Constantes", typeof(Constantes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 01/01/1753.
        /// </summary>
        internal static string General_FechaPorDefecto {
            get {
                return ResourceManager.GetString("General_FechaPorDefecto", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 3349.
        /// </summary>
        internal static string IdResponsable_AdminCountry {
            get {
                return ResourceManager.GetString("IdResponsable_AdminCountry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BIL.
        /// </summary>
        internal static string MovimientoContabilidad_BIL {
            get {
                return ResourceManager.GetString("MovimientoContabilidad_BIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRAB.
        /// </summary>
        internal static string MovimientoContabilidad_CRAB {
            get {
                return ResourceManager.GetString("MovimientoContabilidad_CRAB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AC.
        /// </summary>
        internal static string TipoFacturacion_Actividades {
            get {
                return ResourceManager.GetString("TipoFacturacion_Actividades", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PR.
        /// </summary>
        internal static string TipoFacturacion_NoClinicas {
            get {
                return ResourceManager.GetString("TipoFacturacion_NoClinicas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PA.
        /// </summary>
        internal static string TipoFacturacion_Paquetes {
            get {
                return ResourceManager.GetString("TipoFacturacion_Paquetes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RA.
        /// </summary>
        internal static string TipoFacturacion_Relacion {
            get {
                return ResourceManager.GetString("TipoFacturacion_Relacion", resourceCulture);
            }
        }
    }
}
