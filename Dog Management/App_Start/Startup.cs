using System;
using System.Configuration;
using System.Threading.Tasks;
using DarkSide;
using Dog_Management.Models;
using Dog_Management.Models.Profile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Dog_Management.App_Start.Startup))]

namespace Dog_Management.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<Concrete>(CreateConcrete);

            // Сначала настраиваем Identity, т.к. его тип могут понадобиться в типах предметной области
            ConfigureAuth(app);
            DomainConfiguration(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => ApplicationDbContext.Create(ConnectionName));

            app.CreatePerOwinContext<UserManager>(UserManager.Create);

            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);


            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //CookieName = "doggies",
                LoginPath = new PathString("/api/account/login")
            });
        }
        public void DomainConfiguration(IAppBuilder app)
        {
            app.CreatePerOwinContext<ProfileManager>((IdentityFactoryOptions<ProfileManager> options, IOwinContext context) =>
            {
                return new ProfileManager(context.Get<Concrete>());
            });
            //app.CreatePerOwinContext<MyUserManager>((IdentityFactoryOptions<MyUserManager> options, IOwinContext context) =>
            //{
            //    return new MyUserManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<JudgeManager>((IdentityFactoryOptions<JudgeManager> options, IOwinContext context) =>
            //{
            //    return new JudgeManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<OrganizationManager>((IdentityFactoryOptions<OrganizationManager> options, IOwinContext context) =>
            //{
            //    return new OrganizationManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<RequestManager>((IdentityFactoryOptions<RequestManager> options, IOwinContext context) =>
            //{
            //    return new RequestManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<DocumentCreatorManager>((IdentityFactoryOptions<DocumentCreatorManager> options, IOwinContext context) =>
            //{
            //    return new DocumentCreatorManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<RequestApprovalManager>((IdentityFactoryOptions<RequestApprovalManager> options, IOwinContext context) =>
            //{
            //    return new RequestApprovalManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<EventCreatorManager>((IdentityFactoryOptions<EventCreatorManager> options, IOwinContext context) =>
            //{
            //    return new EventCreatorManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<EventsEditorManager>((IdentityFactoryOptions<EventsEditorManager> options, IOwinContext context) =>
            //{
            //    return new EventsEditorManager(context.Get<Concrete>());
            //});
            //app.CreatePerOwinContext<AddDogManager>((IdentityFactoryOptions<AddDogManager> options, IOwinContext context) =>
            //{
            //    return new AddDogManager(context.Get<Concrete>());
            //});
        }


        /// <summary>
        /// Имя строки соединения с БД
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return ConfigurationManager.AppSettings["sys:connectionName"];
            }
        }

        /// <summary>
        /// Метод возвращает строку соединения по умолчанию
        /// </summary>
        /// <returns>Объект строки соединения</returns>
        public ConnectionStringSettings GetConnectionStringSettings()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionName];
        }

        /// <summary>
        /// Создает объект управления подключением к БД
        /// </summary>
        public Concrete CreateConcrete(IdentityFactoryOptions<Concrete> options, IOwinContext context)
        {
            return GetConnectionStringSettings().With(conf => new Concrete(conf));
        }
    }
}
