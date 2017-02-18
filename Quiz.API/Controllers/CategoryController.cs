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
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _CategoryService;

        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet]
        [Route("Categories")]
        public HttpResponseMessage Get()
        {
            var models = _CategoryService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Role not found");
        }

        [HttpGet]
        [Route("Category/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var model = _CategoryService.GetById(id);
            if (model != null)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Role not found");
        }

        [HttpPost]
        [Route("Category/Create")]
        public HttpResponseMessage Post([FromBody]CategoryModel model)
        {
            var id = _CategoryService.Create(model);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }

        [HttpPut]
        [Route("Category/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]CategoryModel model)
        {
            if (_CategoryService.Update(id, model))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [HttpDelete]
        [Route("Category/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_CategoryService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Role not found");
        }
    }
}
