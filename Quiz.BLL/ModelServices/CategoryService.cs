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
    public interface ICategoryService : IModelService<CategoryModel>
    {

    }

    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private member methods

        private Category ModelToEntity(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Description=model.Description,
            };
            return entity;
        }

        private Category ModelToEntity(Category entity, CategoryModel model)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            return entity;
        }

        private CategoryModel EntityToModel(Category entity)
        {
            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
            return model;
        }

        #endregion

        public int Create(CategoryModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<Category>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<Category>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<Category>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<CategoryModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Category>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<CategoryModel>();
        }

        public CategoryModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<Category>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, CategoryModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<Category>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<Category>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
