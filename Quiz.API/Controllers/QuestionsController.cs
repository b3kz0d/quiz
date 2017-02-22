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
    public class QuestionsController : ApiController
    {
        private readonly IQuestionService _QuestionService;
        private readonly IQuestionAnswerService _QuestionAnswerService;
        public QuestionsController(IQuestionService questionService, IQuestionAnswerService questionAnswerService)
        {
            _QuestionService = questionService;
            _QuestionAnswerService = questionAnswerService;
        }

        [HttpGet]
        [Route("Questions")]
        public HttpResponseMessage Get()
        {
            var models = _QuestionService.GetAll();
            if (models.Any())
                return Request.CreateResponse(HttpStatusCode.OK, models);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Role not found");
        }

        [HttpGet]
        [Route("Question/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            var questionModel = _QuestionService.GetById(id);
            var answerModel = _QuestionAnswerService.GetAll();
            var AnsModel = answerModel.Where(a => a.QuestionId == questionModel.Id).ToList();

            var quesAnsModel = new QuestionServiceModel()
            {
                Id = questionModel.Id,
                QuestionContent = questionModel.QuestionContent,
                QuestionLevelId = questionModel.QuestionLevelId,
                CategoryId = questionModel.CategoryId,
                Answers = AnsModel,
            };

            if (quesAnsModel != null)
                return Request.CreateResponse(HttpStatusCode.OK, quesAnsModel);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, " Not found");
        }

        [HttpPost]
        [Route("Question/Create")]
        public HttpResponseMessage Post([FromBody]QuestionServiceModel model)
        {
            var question = new QuestionModel()
            {
                QuestionContent = model.QuestionContent,
                QuestionLevelId = model.QuestionLevelId,
                CategoryId = model.CategoryId,

            };
            var id = _QuestionService.Create(question);

            var answers = model.Answers;

            for (int i = 0; i < answers.Count; i++)
            {
                var ansmodel = new QuestionAnswerModel()
                {
                    AnswerContent = answers[i].AnswerContent,
                    QuestionId = id,
                    IsCorrect = answers[i].IsCorrect,
                };

                _QuestionAnswerService.Create(ansmodel);
            };

            if (id > 0)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Not Acceptable");
        }

        [HttpPut]
        [Route("Question/Update/{id?}")]
        public HttpResponseMessage Put(int id, [FromBody]QuestionServiceModel model)
        {
            bool check1 = false, check2 = false;
            var questionModel = new QuestionModel()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                QuestionContent = model.QuestionContent,
                QuestionLevelId = model.QuestionLevelId,

            };
            check1 = _QuestionService.Update(id, questionModel);
            var quesAnswerIdList = _QuestionAnswerService.GetAll().Where(q => q.QuestionId == id).Select(a => a.Id).ToList();
            for (int i = 0; i < model.Answers.Count; i++)
            {
                var answerModel = new QuestionAnswerModel()
                {
                    AnswerContent = model.Answers[i].AnswerContent,
                    QuestionId = model.Answers[i].QuestionId,
                    IsCorrect = model.Answers[i].IsCorrect
                };
                check2 = _QuestionAnswerService.Update(quesAnswerIdList[i], answerModel);
            }
            if (check1 && check2)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
        }

        [HttpDelete]
        [Route("Question/Delete/{id?}")]
        public HttpResponseMessage Delete(int id)
        {
            _QuestionService.Delete(id);
            var quesAnswerIdList = _QuestionAnswerService.GetAll().Where(q => q.QuestionId == id).Select(a => a.Id).ToList();

            for (int i = 0; i < quesAnswerIdList.Count; i++)
            {
                _QuestionAnswerService.Delete(quesAnswerIdList[i]);
            };

            if (_QuestionService.Delete(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, " Not found");
        }
    }
}
