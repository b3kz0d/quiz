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
    public interface IQuizSessionService : IModelService<QuizSessionModel>
    {

    }
    public class QuizSessionService: IQuizSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;
        private readonly QuizOptionService _quizOptionService;

        public QuizSessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new UserService(_unitOfWork);
            _categoryService = new CategoryService(_unitOfWork);
            _quizOptionService = new QuizOptionService(_unitOfWork);
        }

        #region Private member methods

        private QuizSession ModelToEntity(QuizSessionModel model)
        {
            var entity = new QuizSession()
            {
                UserId = model.UserId,
                CategoryId = model.CategoryId,
                QuizOptionId = model.QuizOptionId,
                SessionStart = model.SessionStart,
                LastUpdate = model.LastUpdate,
                Status = model.Status

            };
            return entity;
        }

        private QuizSession ModelToEntity(QuizSession entity, QuizSessionModel model)
        {
            entity.CategoryId = model.CategoryId;
            entity.UserId = model.UserId;
            entity.QuizOptionId = model.QuizOptionId;
            entity.SessionStart = model.SessionStart;
            entity.LastUpdate = model.LastUpdate;
            entity.Status = model.Status;

            return entity;
        }

        private QuizSessionModel EntityToModel(QuizSession entity)
        {
            var model = new QuizSessionModel()
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                UserId = entity.UserId,
                QuizOptionId = entity.QuizOptionId,
                SessionStart = entity.SessionStart,
                LastUpdate = entity.LastUpdate,
                Status = entity.Status,
                Category = _categoryService.GetById(entity.CategoryId),
                User = _userService.GetById(entity.UserId),
                QuizOption = _quizOptionService.GetById(entity.QuizOptionId)

            };
            return model;
        }
        #endregion

        public int Create(QuizSessionModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<QuizSession>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<QuizSession>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<QuizSession>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<QuizSessionModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<QuizSession>().GetAll().ToList();

            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }

            return new List<QuizSessionModel>();
        }

        public QuizSessionModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<QuizSession>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, QuizSessionModel model)
        {
            if (id > 0)
            {

                var entity = _unitOfWork.GetRepository<QuizSession>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<QuizSession>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
