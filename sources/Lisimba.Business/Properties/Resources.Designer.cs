﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DustInTheWind.Lisimba.Business.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DustInTheWind.Lisimba.Business.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Cannot close current address book. It has unsaved modifications..
        /// </summary>
        internal static string CloseAddressBooks_NotSavedError {
            get {
                return ResourceManager.GetString("CloseAddressBooks_NotSavedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to x is not a Contact object..
        /// </summary>
        internal static string ContactComparer_XIsNotContact {
            get {
                return ResourceManager.GetString("ContactComparer_XIsNotContact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to y is not a Contact object..
        /// </summary>
        internal static string ContactComparer_YIsNotContact {
            get {
                return ResourceManager.GetString("ContactComparer_YIsNotContact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New Address Book.
        /// </summary>
        internal static string DefaultAddressBookName {
            get {
                return ResourceManager.GetString("DefaultAddressBookName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Gate that knows nothing, does nothing. It just exists..
        /// </summary>
        internal static string EmptyGate_Description {
            get {
                return ResourceManager.GetString("EmptyGate_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EmptyGate cannot open anything..
        /// </summary>
        internal static string EmptyGate_LoadError {
            get {
                return ResourceManager.GetString("EmptyGate_LoadError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EmptyGate cannot save anything..
        /// </summary>
        internal static string EmptyGate_SaveError {
            get {
                return ResourceManager.GetString("EmptyGate_SaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no gate with id = {0}.
        /// </summary>
        internal static string GateNotFoundError {
            get {
                return ResourceManager.GetString("GateNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No address book is opened..
        /// </summary>
        internal static string NoAddessBookOpenedError {
            get {
                return ResourceManager.GetString("NoAddessBookOpenedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt; Unnamed &gt;.
        /// </summary>
        internal static string NoAddressBookName {
            get {
                return ResourceManager.GetString("NoAddressBookName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No gate is associated with the address book..
        /// </summary>
        internal static string NoGateWasSpecifiedError {
            get {
                return ResourceManager.GetString("NoGateWasSpecifiedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A location has to be specified..
        /// </summary>
        internal static string NoLocationWasSpecifiedError {
            get {
                return ResourceManager.GetString("NoLocationWasSpecifiedError", resourceCulture);
            }
        }
    }
}
