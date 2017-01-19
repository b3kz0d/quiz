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
    public interface IQuestionAnswerService : IModelService<QuestionAnswerModel>
    {

    }
    class QuestionAnswerService: IQuestionAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QuestionService _questionService;

        public QuestionAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _questionService = new QuestionService(_unitOfWork);
        }

        #region Private member methods

        private QuestionAnswer ModelToEntity(QuestionAnswerModel model)
        {
            var entity = new QuestionAnswer()
            {
                QuestionId = model.QuestionId,
                AnswerContent = model.AnswerContent,
                IsCorrect = model.IsCorrect,
            };
            return entity;

        }
        private QuestionAnswer ModelToEntity(QuestionAnswer entity, QuestionAnswerModel model)
        {
            entity.QuestionId = model.QuestionId;
            entity.AnswerContent = model.AnswerContent;

            entity.IsCorrect = model.IsCorrect;
            return entity;
        }

        private QuestionAnswerModel EntityToModel(QuestionAnswer entity)
        {
            var model = new QuestionAnswerModel()
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                IsCorrect = entity.IsCorrect,
                AnswerContent = entity.AnswerContent,
                Question = _questionService.GetById(entity.QuestionId)

            };
            return model;

        }

        #endregion 
        public int Create(QuestionAnswerModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<QuestionAnswer>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<QuestionAnswer>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<QuestionAnswer>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<QuestionAnswerModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<QuestionAnswer>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<QuestionAnswerModel>();
        }


        public QuestionAnswerModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<QuestionAnswer>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }


        public bool Update(int id, QuestionAnswerModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<QuestionAnswer>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<QuestionAnswer>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

    }
}
