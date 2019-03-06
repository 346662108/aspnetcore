﻿using Mozlite.Extensions.Security;
using MozliteDemo.Extensions.Security;

namespace MozliteDemo.Extensions
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public abstract class ControllerBase : Mozlite.Mvc.ControllerBase
    {
        private User _user;
        /// <summary>
        /// 当前用户。
        /// </summary>
        protected new User User => _user ?? (_user = HttpContext.GetUser<User>());

        private Role _role;
        /// <summary>
        /// 当前用户角色。
        /// </summary>
        protected Role Role => _role ?? (_role = GetRequiredService<IRoleManager>().FindById(User.RoleId));
    }
}