﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CDNApplication.Resources.Validation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UploadDocumentErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UploadDocumentErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CDNApplication.Resources.Validation.UploadDocumentErrorMessages", typeof(UploadDocumentErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to CDN must contain only alphanumeric characters.
        /// </summary>
        internal static string CdnFormatText {
            get {
                return ResourceManager.GetString("CdnFormatText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CDN must be 6-7 characters in length.
        /// </summary>
        internal static string CdnLengthText {
            get {
                return ResourceManager.GetString("CdnLengthText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CDN is a required field.
        /// </summary>
        internal static string CdnNumberNotEmptyText {
            get {
                return ResourceManager.GetString("CdnNumberNotEmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certificate Type is a required field.
        /// </summary>
        internal static string CertificateTypeNotEmptyText {
            get {
                return ResourceManager.GetString("CertificateTypeNotEmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address must be a valid format.
        /// </summary>
        internal static string EmailAddressFormatText {
            get {
                return ResourceManager.GetString("EmailAddressFormatText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address is a required field.
        /// </summary>
        internal static string EmailAddressNotEmptyText {
            get {
                return ResourceManager.GetString("EmailAddressNotEmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Phone number is a required field.
        /// </summary>
        internal static string PhoneNumberNotEmptyText {
            get {
                return ResourceManager.GetString("PhoneNumberNotEmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please identify the type of certificate renewal you want.
        /// </summary>
        internal static string TypeOfCertificateNotEmptyText {
            get {
                return ResourceManager.GetString("TypeOfCertificateNotEmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must upload at least 1 file.
        /// </summary>
        internal static string UploadedFilesNotEmptyText {
            get {
                return ResourceManager.GetString("UploadedFilesNotEmptyText", resourceCulture);
            }
        }
    }
}
