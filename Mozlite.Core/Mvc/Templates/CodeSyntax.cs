﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mozlite.Mvc.Templates
{
    /// <summary>
    /// 代码语法。
    /// </summary>
    public class FunctionCodeSyntax : Syntax
    {
        /// <summary>
        /// 参数。
        /// </summary>
        public IEnumerable<string> Parameters { get; set; }

        /// <summary>
        /// 当前语法的呈现字符串。
        /// </summary>
        /// <returns>返回当前语法的呈现字符串。</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Declarings.Count > 0)
                builder.AppendLine(Declarings.Join("\r\n"));
            builder.AppendFormat("{0}(", Name);
            if (Parameters?.Any() == true)
            {
                builder.Append(Parameters.Select(x=>$"\"{x}\"").Join(", ")).Append(" ");
            }
            if (IsBlock)
            {
                builder.AppendLine("){");
                builder.AppendLine(this.Join("\r\n"));
                builder.AppendLine("}");
            }
            else
            {
                builder.Append(");");
            }
            return builder.ToString();
        }
    }

    /// <summary>
    /// 代码语法。
    /// </summary>
    public class PropertyCodeSyntax : Syntax
    {
        /// <summary>
        /// 当前语法的呈现字符串。
        /// </summary>
        /// <returns>返回当前语法的呈现字符串。</returns>
        public override string ToString()
        {
            return "@" + Name;
        }
    }
}