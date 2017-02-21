using Quiz.API.ActionFilters;
using Quiz.BLL.Models;
using Quiz.BLL.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Quiz.API.Controllers
{
   /// [AuthorizationRequired]
    public class RoleController : ApiController
    {
        private readonly IRoleService _RoleService;

        public RoleController(IRoleService RoleService)
        {
            _RoleService = RoleService;
        }

        [HttpGet]
        [Route("Roles")]
        public HttpResponseMessage Get()
        {
            var models = _RoleService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Role not found");
        }

        [HttpGet]
        [Route("Role/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var model = _RoleService.GetById(id);
            if (model != null)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Role not found");
        }
        [AuthorizationRequired]
        [HttpPost]
        [Route("Role/Create")]
        public HttpResponseMessage Post([FromBody]RoleModel model)
        {
            var id = _RoleService.Create(model);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }
        [AuthorizationRequired]
        [HttpPut]
        [Route("Role/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]RoleModel model)
        {
            if (_RoleService.Update(id, model))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [AuthorizationRequired]
        [HttpDelete]
        [Route("Role/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_RoleService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Role not found");
        }
    }
}
