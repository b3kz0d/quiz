using System;
using System.Configuration;
using System.Linq;
using Quiz.BLL.Models;
using Quiz.DAL;
using Quiz.DAL.EntityModels;


namespace Quiz.BLL.ModelServices
{
    public class TokenService: ITokenService
    {
        #region Private member variables.
        private readonly UnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        #endregion

        #region Public constructor.
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new UserService(unitOfWork);
            //_contentPermissionService = new ContentPermissionService(unitOfWork);
        }
        #endregion


        #region Public member methods.

        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokenModel GenerateToken(int userId)
        {
            var expire = ConfigurationManager.AppSettings["AuthTokenExpiry"];
            var userUsedTokens = _unitOfWork.GetRepository<Token>().GetMany(x => x.UserId == userId);
            var lastLogin = DateTime.Now;
            if (userUsedTokens.Any())
                lastLogin = userUsedTokens.Max(x => x.IssuedOn); 

            _unitOfWork.GetRepository<Token>().Delete(x => x.UserId == userId);

            string token = Guid.NewGuid().ToString();

            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddMinutes(
            Convert.ToDouble(expire));
            var tokenEntity = new Token
            {
                AuthToken = token,
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                LastLogin=lastLogin,
            };

            _unitOfWork.GetRepository<Token>().Insert(tokenEntity);
            _unitOfWork.Save();

            var tokenModel = new TokenModel()
            {
                AuthToken = token,
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                LastLogin = lastLogin,
            };
            
            return tokenModel;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenId)
        {
            var tokenEntity = _unitOfWork.GetRepository<Token>().Get(t => t.AuthToken == tokenId);
            if (tokenEntity != null && (DateTime.Now < tokenEntity.ExpiresOn))
            {
                tokenEntity.ExpiresOn = DateTime.Now.AddMinutes(
                Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                _unitOfWork.GetRepository<Token>().Update(tokenEntity);
                _unitOfWork.Save();
                return true;
            }
            //else if(tokenEntity != null&& (DateTime.Now >= tokenEntity.ExpiresOn))
            //{
            //    Kill(tokenEntity.AuthToken);
            //}
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public bool Kill(string tokenId)
        {
            var tokenEntity = _unitOfWork.GetRepository<Token>().Get(t => t.AuthToken == tokenId);
            if (tokenEntity != null)
            {
                tokenEntity.ExpiresOn = DateTime.Now.AddSeconds(
                -Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                _unitOfWork.GetRepository<Token>().Update(tokenEntity);
                _unitOfWork.Save();
                return true;
            }
            else return false;
        }

        private bool Delete(string tokenId)
        {
            _unitOfWork.GetRepository<Token>().Delete(x => x.AuthToken == tokenId);
            _unitOfWork.Save();
            var isNotDeleted = _unitOfWork.GetRepository<Token>().GetMany(x => x.AuthToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.GetRepository<Token>().Delete(x => x.UserId == userId);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.GetRepository<Token>().GetMany(x => x.UserId == userId).Any();
            return !isNotDeleted;
        }

        //public UserProfileModel GetUserProfile(int userId)
        //{
        //    var model=_userService.GetById(userId);
        //    if (model != null)
        //    {
        //        //var contentPermissions = _contentPermissionService.GetAll().Where(x => x.RoleId == model.RoleId).ToList();
        //        var userProfileModel = new UserProfileModel
        //        {
        //            FullName = model.FullName,
        //            RoleType = (RoleType)model.Role.RoleId,
        //            LastLogin = DateTime.Now,
        //            //ContentPermissions = contentPermissions,
        //        };
        //        return userProfileModel;
        //    }
        //    else
        //    {
        //        return new UserProfileModel();
        //    }
        //}

        #endregion
    }
}
