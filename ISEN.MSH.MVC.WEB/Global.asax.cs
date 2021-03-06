﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Spring.Context;
using Spring.Context.Support;
using ISEN.MSH.APP.Service.Base.User.Service;
using Spring.Web.Mvc;
using ISEN.MSH.Nhibernate.Model.Users;
using ISEN.MSH.APP.Service.Mail.Service;

namespace ISEN.MSH.MVC.WEB
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}/{action}/{id}", // 带有参数的 URL
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            //);
            routes.MapRoute("NoActionASPX", "{controller}.aspx", new { controller = "home", action = "index", id = "" });//无Action的匹配
            routes.MapRoute("NoIDASPX", "{controller}/{action}.aspx", new { controller = "home", action = "index", id = "" });//无ID的匹配
            routes.MapRoute("DefaultASPX", "{controller}/{action}/{id}.aspx", new { controller = "home", action = "index", id = "" });//默认匹配
            routes.MapRoute("NoAction", "{controller}", new { controller = "home", action = "index", id = "" });//无Action的匹配
            routes.MapRoute("NoID", "{controller}/{action}", new { controller = "home", action = "index", id = "" });//无ID的匹配
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "home", action = "index", id = "" });//默认匹配
            routes.MapRoute("Root", "", new { controller = "home", action = "index", id = "" });//根目录匹配


        }

        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
            this.SetInitAccount();
        }

        /// <summary>
        /// 设置初始账号
        /// </summary>
        private void SetInitAccount()
        {
            IApplicationContext cxt = ContextRegistry.GetContext();
            IUserManager manger = (IUserManager)cxt.GetObject("Manager.User");
            IMailManager mangerFolder = (IMailManager)cxt.GetObject("Manager.Mail");
            const string account = "admin";
            var user = manger.Get(account);
            if (user == null)
            {
                user = new UserModel
                {
                    Account = account,
                    Name = "管理员",
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    IsEnabled = true
                };
                manger.Save(user);
            }
        }
    }
}