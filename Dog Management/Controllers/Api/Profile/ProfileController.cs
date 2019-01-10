using Dog_Management.Controllers.Abstract;
using Dog_Management.Models.Profile;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Profile;

namespace Dog_Management.Controllers.Api.Profile
{
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseApiController
    {
        protected Models.Profile.ProfileManager ProfileManager
        {
            get
            {
                return Request.GetOwinContext().Get<Models.Profile.ProfileManager>();
            }
        }
        [Route("getDogs")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDogs()
        {
            List<Dog> allDogs = await ProfileManager.GetDogsByUserId(CurrentUser.Id);
            return WrapSuccess(allDogs);
        }
        [AllowAnonymous]
        [Route("getDogById")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDogById(int dogId)
        {
            Dog dog = await ProfileManager.GetDogById(dogId);
            return WrapSuccess(dog);
        }
        [AllowAnonymous]
        [Route("ChangeDogInfo")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangeDogInfo(Dog model)
        {
            await ProfileManager.ChangeDogInfo(model);
            return WrapSuccess();
        }
        [AllowAnonymous]
        [Route("GetDogCatalogs")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDogCatalogs()
        {
            return WrapSuccess(await ProfileManager.GetDogCatalogs());
        }
    }
}