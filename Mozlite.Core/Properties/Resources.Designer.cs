﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mozlite.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mozlite.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
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
        ///   查找类似 属性表达式&apos;{0}&apos;不正确， 表达式必须提供一种属性访问，如： &apos;t =&gt; t.MyProperty&apos;；如果式多个属性，需要如下代码表示：&apos;t =&gt; new {{ t.MyProperty1, t.MyProperty2 }}&apos;。 的本地化字符串。
        /// </summary>
        internal static string InvalidPropertiesExpression {
            get {
                return ResourceManager.GetString("InvalidPropertiesExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 属性表达式&apos;{0}&apos;不正确， 表达式必须提供一种属性访问，如： &apos;t =&gt; t.MyProperty&apos;。 的本地化字符串。
        /// </summary>
        internal static string InvalidPropertyExpression {
            get {
                return ResourceManager.GetString("InvalidPropertyExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 新增了 的本地化字符串。
        /// </summary>
        internal static string LogAction_Add {
            get {
                return ResourceManager.GetString("LogAction_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ”{0}“(”{1}“) 的本地化字符串。
        /// </summary>
        internal static string LogAction_AddFormat {
            get {
                return ResourceManager.GetString("LogAction_AddFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 修改了 的本地化字符串。
        /// </summary>
        internal static string LogAction_Modify {
            get {
                return ResourceManager.GetString("LogAction_Modify", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 “{0}”由”{1}“修改为“{2}” 的本地化字符串。
        /// </summary>
        internal static string LogAction_ModifyFormat {
            get {
                return ResourceManager.GetString("LogAction_ModifyFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 移除了 的本地化字符串。
        /// </summary>
        internal static string LogAction_Remove {
            get {
                return ResourceManager.GetString("LogAction_Remove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ”{0}“(”{1}“) 的本地化字符串。
        /// </summary>
        internal static string LogAction_RemoveFormat {
            get {
                return ResourceManager.GetString("LogAction_RemoveFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{1}”的属性“{0}”必须包含get访问器。 的本地化字符串。
        /// </summary>
        internal static string NoGetter {
            get {
                return ResourceManager.GetString("NoGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{1}”的属性“{0}”必须包含set访问器。 的本地化字符串。
        /// </summary>
        internal static string NoSetter {
            get {
                return ResourceManager.GetString("NoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 字节大小必须能被 8 整除。 的本地化字符串。
        /// </summary>
        internal static string RandomNumberGenerator_SizeInvalid {
            get {
                return ResourceManager.GetString("RandomNumberGenerator_SizeInvalid", resourceCulture);
            }
        }
    }
}
