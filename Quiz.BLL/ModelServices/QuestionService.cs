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
    public interface IQuestionService : IModelService<QuestionModel>
    {

    }

    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryService _categoryService;
        private readonly QuestionLevelService _questionLevelService;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryService = new CategoryService(_unitOfWork);
            _questionLevelService = new QuestionLevelService(_unitOfWork);

        }

        #region Private member methods

        private Question ModelToEntity(QuestionModel model)
        {
            var entity = new Question()
            {
                CategoryId = model.CategoryId,
                QuestionLevelId=model.QuestionLevelId,
                QuestionContent=model.QuestionContent,
            };
            return entity;
        }

        private Question ModelToEntity(Question entity, QuestionModel model)
        {
            entity.CategoryId = model.CategoryId;
            entity.QuestionLevelId = model.QuestionLevelId;
            entity.QuestionContent = model.QuestionContent;
            return entity;
        }

        private QuestionModel EntityToModel(Question entity)
        {
            var model = new QuestionModel()
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                QuestionLevelId = entity.QuestionLevelId,
                QuestionContent = entity.QuestionContent,
                Category = _categoryService.GetById(entity.CategoryId),
                QuestionLevel = _questionLevelService.GetById(entity.QuestionLevelId)
            };
            return model;
        }

        #endregion

        public int Create(QuestionModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<Question>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<Question>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<Question>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<QuestionModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Question>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<QuestionModel>();
        }

        public QuestionModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<Question>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, QuestionModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<Question>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<Question>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
