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
    public interface IAnswerResultService : IModelService<AnswerResultModel>
    {

    }
    public class AnswerResultService: IAnswerResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QuestionAnswerService _questionAnswerService;
        private readonly ResultService _resultService;

        public AnswerResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _questionAnswerService = new QuestionAnswerService(_unitOfWork);
            _resultService = new ResultService(_unitOfWork);
        }

        #region Private member methods

        private AnswerResult ModelToEntity(AnswerResultModel model)
        {
            var entity = new AnswerResult()
            {
                QuestionAnswerId = model.QuestionAnswerId,
                ResultId = model.ResultId
            };
            return entity;

        }

        private AnswerResult ModelToEntity(AnswerResult entity, AnswerResultModel model)
        {
            entity.QuestionAnswerId = model.QuestionAnswerId;
            entity.ResultId = model.ResultId;
            return entity;
        }

        private AnswerResultModel EntityToModel(AnswerResult entity)
        {
            var model = new AnswerResultModel()
            {
                Id = entity.Id,
                QuestionAnswerId = entity.QuestionAnswerId,
                ResultId = entity.ResultId,
                QuestionAnswer = _questionAnswerService.GetById(entity.QuestionAnswerId),
                Result = _resultService.GetById(entity.ResultId)

            };
            return model;
        }

        #endregion

        public int Create(AnswerResultModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<AnswerResult>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<AnswerResult>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<AnswerResult>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<AnswerResultModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<AnswerResult>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<AnswerResultModel>();
        }

        public AnswerResultModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<AnswerResult>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, AnswerResultModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<AnswerResult>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<AnswerResult>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

    }
}
