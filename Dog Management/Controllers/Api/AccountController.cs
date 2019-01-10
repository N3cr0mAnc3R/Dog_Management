using Dog_Management.Controllers.Abstract;
using Dog_Management.Models;
using Dog_Management.Models.Api;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Dog_Management.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return WrapError("Логин / пароль не введены.");
                //return BadRequest(ModelState);
            }

            // если логин содержит собаку, то проверяем, как email
            if (model.Login.Contains("@"))
            {
                User emailUser = await UserManager.FindByEmailAsync(model.Login);

                if (emailUser != null)
                {
                    model.Login = emailUser.UserName;
                }
            }

            User user = await UserManager.FindAsync(model.Login, model.Password);

            if (user == null)
            {
                return WrapError("Логин / пароль введены не верно.");
            }

            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, ident);

            return WrapSuccess(await CreateUserProfile(user));
        }
        private async Task<UserProfile> CreateUserProfile(User user)
        {
            if (user != null)
            {
                UserProfile profile = new UserProfile(user)
                {
                    Roles = (await UserManager.GetRolesAsync(user.Id)).Select(a => a.ToLower())
                };
                return profile;
            }

            return null;
        }

        public async Task SignInAsync(User user, bool isPersistent)
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(identity);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetCurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            if (AuthManager.User.Identity.IsAuthenticated)
            {
                var t = await CreateUserProfile(await UserManager.FindByNameAsync(AuthManager.User.Identity.Name));
                SetResponseMessage(ApiResponseWrap.MessageType.success, "Нормально");
                return WrapSuccess(t);
            }
            else return WrapSuccess(null);
        }
        [HttpPost]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut();
            return WrapSuccess(null);
        }
    }
}