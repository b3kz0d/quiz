using Quiz.API.ActionFilters;
using Quiz.BLL.Models;
using Quiz.BLL.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Quiz.API.Controllers
{
    [AuthorizationRequired]
    public class UserController : ApiController
    {

        #region Private variable.

        private readonly IUserService _userService;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register(UserModel userModel)
        {
            var id = _userService.Create(userModel);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK,"Logout");
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");

        }

        [HttpGet]
        [Route("Details/{id?}")]
        public HttpResponseMessage Details(int id)
        {
            var model = _userService.GetById(id);
            if (model != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, "User not found!");
        }

        [HttpGet]
        [Route("Users")]
        public HttpResponseMessage GetAll()
        {
            var models = _userService.GetAll();
            if (models != null && models.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, models);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, "User not found!");
        }


    }
}
