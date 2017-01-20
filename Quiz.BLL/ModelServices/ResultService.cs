using Quiz.BLL.Models;
using Quiz.DAL;
using Quiz.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Quiz.BLL.ModelServices
{
    public interface IResultService : IModelService<ResultModel>
    {

    }
    public class ResultService: IResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QuizSessionService _quizSessionService;
        private readonly QuestionService _questionService;
        public ResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _quizSessionService = new QuizSessionService(_unitOfWork);
            _questionService = new QuestionService(_unitOfWork);

        }

        #region Private member methods

        private Result ModelToEntity(ResultModel model)
        {
            var entity = new Result()
            {
                QuizSessionId = model.QuizSessionId,
                QuestionId = model.QuestionId,
                CreatedTime = model.CreatedTime,
                QuestionOrder = model.QuestionOrder,
                UpdatedTime = model.UpdatedTime,

            };
            return entity;
        }
        private Result ModelToEntity(Result entity, ResultModel model)
        {
            entity.QuestionId = model.QuestionId;
            entity.QuizSessionId = model.QuizSessionId;
            entity.QuestionOrder = model.QuestionOrder;
            entity.CreatedTime = model.CreatedTime;
            entity.UpdatedTime = model.UpdatedTime;
            return entity;
        }

        private ResultModel EntityToModel(Result entity)
        {
            var model = new ResultModel()
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                QuizSessionId = entity.QuizSessionId,
                CreatedTime = entity.CreatedTime,
                UpdatedTime = entity.UpdatedTime,
                QuestionOrder = entity.QuestionOrder,
                Question = _questionService.GetById(entity.QuestionId),
                QuizSession = _quizSessionService.GetById(entity.QuizSessionId),

            };
            return model;
        }

        #endregion

        public int Create(ResultModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<Result>().Insert(entity);
                _unitOfWork.Save();
                scope.Complete();
                return entity.Id;
            }

        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var entity = _unitOfWork.GetRepository<Result>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<Result>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }

            }
            return success;
        }


        public IList<ResultModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Result>().GetAll().ToList();
            if(entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<ResultModel>();
        }

        public ResultModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<Result>().GetByID(id);
            if(entity!=null)
            {
                var model = EntityToModel(entity);
                return model;
            }

            return null;

        }

        public bool Update(int id,ResultModel model)
        {
            if(id>0)
            {
                var entity = _unitOfWork.GetRepository<Result>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<Result>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;

            }
            return false;
        }

    }
}
