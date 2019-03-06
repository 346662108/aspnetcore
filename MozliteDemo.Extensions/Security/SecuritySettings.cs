﻿using System.ComponentModel;

namespace MozliteDemo.Extensions.Security
{
    /// <summary>
    /// 安全配置。
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// 扩展名称（区域名称）。
        /// </summary>
        public const string ExtensionName = "security";
        
        /// <summary>
        /// 是否需要确认电子邮件。
        /// </summary>
        [DisplayName("确认电子邮件")]
        public bool RequiredEmailConfirmed { get; set; }

        /// <summary>
        /// 是否需要确认电话号码。
        /// </summary>
        [DisplayName("确认电话号码")]
        public bool RequiredPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 是否需要二次验证。
        /// </summary>
        [DisplayName("二次登陆验证")]
        public bool RequiredTwoFactorEnabled { get; set; }

        /// <summary>
        /// 开放注册。
        /// </summary>
        [DisplayName("开放注册")]
        public bool Registrable { get; set; }
    }
}