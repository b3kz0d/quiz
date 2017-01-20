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
    public interface IQuizOptionService : IModelService<QuizOptionModel>
    {

    }
    public class QuizOptionService : IQuizOptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private member methods

        private QuizOption ModelToEntity(QuizOptionModel model)
        {
            var entity = new QuizOption()
            {
                Name = model.Name,
                Amount = model.Amount,
                RequiredPercentage = model.RequiredPercentage
            };
            return entity;
        }

        private QuizOption ModelToEntity(QuizOption entity, QuizOptionModel model)
        {
            entity.Name = model.Name;
            entity.Amount = model.Amount;
            entity.RequiredPercentage = model.RequiredPercentage;

            return entity;
        }

        private QuizOptionModel EntityToModel(QuizOption entity)
        {
            var model = new QuizOptionModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Amount = entity.Amount,
                RequiredPercentage = entity.RequiredPercentage
            };
            return model;
        }

        #endregion

        public int Create(QuizOptionModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<QuizOption>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<QuizOption>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<QuizOption>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;

                    }
                }
            }
            return success;
        }

        public IList<QuizOptionModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<QuizOption>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<QuizOptionModel>();
        }

        public QuizOptionModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<QuizOption>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;

            }
            return null;
        }


        public bool Update(int id, QuizOptionModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<QuizOption>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<QuizOption>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return true;
        }

    }
}
