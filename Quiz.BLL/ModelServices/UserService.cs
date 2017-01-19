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
    public interface IUserService : IModelService<UserModel>
    {

    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private member methods

        private User ModelToEntity(UserModel model)
        {
            var entity = new User()
            {
                UserName = model.UserName,
                Password=model.Password,
            };
            return entity;
        }

        private User ModelToEntity(User entity, UserModel model)
        {
            entity.UserName = model.UserName;
            entity.Password = model.Password;
            return entity;
        }

        private UserModel EntityToModel(User entity)
        {
            var model = new UserModel()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Password = entity.Password,
            };
            return model;
        }

        #endregion

        public int Create(UserModel model)
        {
            using (var scope = new TransactionScope())
            {
                var entity = ModelToEntity(model);
                _unitOfWork.GetRepository<User>().Insert(entity);
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
                    var entity = _unitOfWork.GetRepository<User>().GetByID(id);
                    if (entity != null)
                    {
                        _unitOfWork.GetRepository<User>().Delete(entity);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IList<UserModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<User>().GetAll().ToList();
            if (entities.Any())
            {
                var models = Helper.Map(entities, EntityToModel);
                return models;
            }
            return new List<UserModel>();
        }

        public UserModel GetById(int id)
        {
            var entity = _unitOfWork.GetRepository<User>().GetByID(id);
            if (entity != null)
            {
                var model = EntityToModel(entity);
                return model;
            }
            return null;
        }

        public bool Update(int id, UserModel model)
        {
            if (id > 0)
            {
                var entity = _unitOfWork.GetRepository<User>().GetByID(id);
                var updatedEntity = ModelToEntity(entity, model);
                _unitOfWork.GetRepository<User>().Update(updatedEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
