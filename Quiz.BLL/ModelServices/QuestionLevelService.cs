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
    public interface IQuestionLevelService : IModelService<QuestionLevelModel>
    {

    }

    public class QuestionLevelService : IQuestionLevelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionLevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private member methods

        private QuestionLevel ModelToEntity(QuestionLevelModel model)
        {
            var entity = new QuestionLevel()
            {
                Name = model.Name,
                Score=model.Score,
                Description=model.Description,
            };
            return entity;
        }

        private QuestionLevel ModelToEntity(QuestionLevel entity, QuestionLevelModel model)
        {
            entity.Name = model.Name;
            entity.Score = model.Score;
            entity.Description = model.Description;
            return entity;
        }

        private QuestionLevelModel EntityToModel(QuestionLevel entity)
        {
            var model = new QuestionLevelModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Score = entity.Score,
                Description = entity.Description,
            };
            return model;
        }

        #endregion

        public int Create(QuestionLevelModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<QuestionLevel>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<QuestionLevel>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<QuestionLevel>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<QuestionLevelModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<QuestionLevel>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<QuestionLevelModel>();
        }

        public QuestionLevelModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<QuestionLevel>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, QuestionLevelModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<QuestionLevel>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<QuestionLevel>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
