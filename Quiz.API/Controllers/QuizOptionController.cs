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
    public class QuizOptionController : ApiController
    {
        private readonly IQuizOptionService _QuizOptionService;

        public QuizOptionController(IQuizOptionService quizOptionService)
        {
            _QuizOptionService = quizOptionService;
        }

        [HttpGet]
        [Route("QuizOptions")]
        public HttpResponseMessage Get()
        {
            var models = _QuizOptionService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
        }

        [HttpGet]
        [Route("QuizOption/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var model = _QuizOptionService.GetById(id);
            if (model != null)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Not found");
        }
        [AuthorizationRequired]
        [HttpPost]
        [Route("QuizOption/Create")]
        public HttpResponseMessage Post([FromBody]QuizOptionModel model)
        {
            var id = _QuizOptionService.Create(model);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }
        [AuthorizationRequired]
        [HttpPut]
        [Route("QuizOption/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]QuizOptionModel model)
        {
            if (_QuizOptionService.Update(id, model))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [AuthorizationRequired]
        [HttpDelete]
        [Route("QuizOption/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_QuizOptionService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Not found");
        }
    }
}
