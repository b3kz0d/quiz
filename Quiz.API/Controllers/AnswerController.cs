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
    public class AnswerController : ApiController
    {
        private readonly IQuestionAnswerService _QuestionAnswerService;

        public AnswerController(IQuestionAnswerService questionAnswerService)
        {
            _QuestionAnswerService = questionAnswerService;
        }

        [HttpGet]
        [Route("Answers")]
        public HttpResponseMessage Get()
        {
            var models = _QuestionAnswerService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Not found");
        }

        [HttpGet]
        [Route("Answer/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var model = _QuestionAnswerService.GetById(id);
            if (model != null)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Not found");
        }

        [HttpPost]
        [Route("Answer/Create")]
        public HttpResponseMessage Post([FromBody]QuestionAnswerModel model)
        {
            var id = _QuestionAnswerService.Create(model);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }

        [HttpPut]
        [Route("Answer/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]QuestionAnswerModel model)
        {
            if (_QuestionAnswerService.Update(id, model))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [HttpDelete]
        [Route("Answer/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_QuestionAnswerService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, " Not found");
        }
    }
}
