﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Animat.Studio.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Animat.Studio.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap chibi_e {
            get {
                object obj = ResourceManager.GetObject("chibi_e", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap logo_banner {
            get {
                object obj = ResourceManager.GetObject("logo_banner", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /* Colors and such
        /// * -------------------------------------------------------*/
        ///
        ////* Backgrounds */
        ///.background-highlight {background: #A0A0A0; }
        ///.background-ultralight { background: #384153; }
        ///.background-light { background: #42454B; }
        ///.background-dark { background-color: #252B36; }
        ///
        ////* Link &amp; Text Colors */
        ///a { color: #86c3ff; text-decoration: none; }
        ///a:hover { color: #cfe7ff; }
        ///a.absent { color: #86c3ff; }
        ///.text { color: white; }
        ///
        ////* Fonts */
        ///.global-font { font: 13px/1.6 &apos;Microsoft YaHei&apos; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StartPageDark {
            get {
                return ResourceManager.GetString("StartPageDark", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ////* Visual Styles 
        /// * -------------------------------------------------------*/
        ///body, html {
        ///    height: 100%;    
        ///    padding: 0px;
        ///    margin: 0px;
        ///}
        ///
        ///body { min-width: 1000px;
        ///    margin-top: -20;
        ///}
        /// 
        ///h1, h2, h3 {
        ///    margin: 20px 0 0px;
        ///    position: relative;
        ///}
        ///
        ///h1 { font-size: 28px; }
        ///h2 { font-size: 20px; }
        ///h3 { font-size: 16px; }
        ///
        ////* Layout Wrappers
        /// * ----------------------------------------------------------*/
        ///.wrapper, .middle, .container {
        ///    width: 100%;
        ///    /*height: [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StartPageLayout {
            get {
                return ResourceManager.GetString("StartPageLayout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /* Colors and such
        /// * -------------------------------------------------------*/
        ///
        ////* Backgrounds */
        ///.background-highlight {background: #A0A0A0; }
        ///.background-ultralight { background: #d5ecfc; }
        ///.background-light { background: #ECF0F1; }
        ///.background-dark { background-color: #b7e2ff; }
        ///
        ////* Link &amp; Text Colors */
        ///a { color: #076715; text-decoration: none; }
        ///a:hover { color: #B23B2E; }
        ///a.absent { color: #66221A; }
        ///.text { color: #000000; }
        ///
        ////* Fonts */
        ///.global-font { font: 13px/1.6 &apos;Microsoft YaHe [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StartPageLight {
            get {
                return ResourceManager.GetString("StartPageLight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///    &lt;head&gt;
        ///        &lt;meta charset=&quot;utf-8&quot; /&gt;
        ///        &lt;!--[if lt IE 9]&gt;&lt;script src=&quot;http://html5shiv.googlecode.com/svn/trunk/html5.js&quot;&gt;&lt;/script&gt;&lt;![endif]--&gt;
        ///        &lt;title&gt;Animat Studio&lt;/title&gt;
        ///        &lt;base href=&quot;{{BaseUri}}&quot; /&gt;
        ///        
        ///        &lt;style&gt;
        ///          {{LayoutCSS}}
        ///          {{ColorThemeCSS}}
        ///        &lt;/style&gt;
        ///        
        ///        &lt;!--&lt;link rel=&quot;stylesheet&quot; type=&quot;text/css&quot; href=&quot;theme-dark.css&quot; /&gt;
        ///        &lt;link rel=&quot;stylesheet&quot; type=&quot;text/css&quot; href=&quot;index.css&quot; /&gt;- [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StartPageTemplate {
            get {
                return ResourceManager.GetString("StartPageTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap watermark {
            get {
                object obj = ResourceManager.GetObject("watermark", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap window_explorer {
            get {
                object obj = ResourceManager.GetObject("window_explorer", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap window_preview {
            get {
                object obj = ResourceManager.GetObject("window_preview", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap window_start {
            get {
                object obj = ResourceManager.GetObject("window_start", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
