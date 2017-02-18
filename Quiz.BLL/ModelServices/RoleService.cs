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
    public interface IRoleService : IModelService<RoleModel>
    {

    }
    class RoleService:IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private member methods

        private Role ModelToEntity(RoleModel model)
        {
            var entity = new Role()
            {
                Name = model.Name,
            };
            return entity;
        }

        private Role ModelToEntity(Role entity, RoleModel model)
        {
            entity.Name = model.Name;
            return entity;
        }

        private RoleModel EntityToModel(Role entity)
        {
            var model = new RoleModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            return model;
        }

        #endregion

        public int Create(RoleModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<Role>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<Role>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<Role>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;

                    }
                }
            }
            return success;
        }

        public IList<RoleModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Role>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<RoleModel>();
        }

        public RoleModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<Role>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;

            }
            return null;
        }


        public bool Update(int id, RoleModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<Role>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<Role>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return true;
        }

    }
}
