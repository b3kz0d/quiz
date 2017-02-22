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
    public class LevelController : ApiController
    {
        private readonly IQuestionLevelService _QuestionLevelService;

        public LevelController(IQuestionLevelService questionLevelService)
        {
            _QuestionLevelService = questionLevelService;
        }

        [HttpGet]
        [Route("Levels")]
        public HttpResponseMessage Get()
        {
            var models = _QuestionLevelService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
        }

        [HttpGet]
        [Route("Level/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var model = _QuestionLevelService.GetById(id);
            if (model != null)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Not found");
        }
        [AuthorizationRequired]
        [HttpPost]
        [Route("Level/Create")]
        public HttpResponseMessage Post([FromBody]QuestionLevelModel model)
        {
            var id = _QuestionLevelService.Create(model);

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }
        [AuthorizationRequired]
        [HttpPut]
        [Route("Level/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]QuestionLevelModel model)
        {
            if (_QuestionLevelService.Update(id, model))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [AuthorizationRequired]
        [HttpDelete]
        [Route("Level/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_QuestionLevelService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Not found");
        }
    }
}
