using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using StudyCore.BLL.Dto;
using StudyCore.Core.Context;
using StudyCore.Data;
using StudyCore.Data.Data;

namespace StudyCore.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private IRepository<M_User, long> userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userRepository"></param>
        public UserBLL(IRepository<M_User, Int64> _userRepository)
        {
            userRepository = _userRepository;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool CreateUser(UserDto userDto)
        {
            var user = Mapper.Map<M_User>(userDto);
            user.LastLoginTime = string.Empty;
            return userRepository.AddAsync(user).Result.Id > 0;
        }

        public bool UpdatePassword(long Id, string password)
        {

            var user = userRepository.GetById(Id);
            if (user != null)
            {
                user.Password = password;
                userRepository.UpdateAsync(user);
            }

            return userRepository.AddAsync(user).Result.Id > 0;
        }

        public bool Delete(long Id)
        {
            return userRepository.Delete(Id) > 0;
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        public EUserLoginResult CheckLoginUser(UserLoginDto userLoginDto)
        {
            var userList = userRepository.GetQueryable(t => t.EmailAddress == userLoginDto.EmailAddress || t.LoginName == userLoginDto.LoginName || t.UserName == userLoginDto.UserName).Where(t => t.DataFlag == (int)BaseModelDataFlagType.数据有效).ToList();
            if (userList.Any())
            {
                if (userList.Any(t => t.Password == userLoginDto.Password))
                {
                    return EUserLoginResult.Successful;
                }
                else
                {
                    return EUserLoginResult.WrongPassword;
                }
            }
            else
            {
                return EUserLoginResult.UserNotExist;
            }
        }


        public EUserLoginResult Login(UserLoginDto userLoginDto)
        {
            if (!CheckUserLoginDto(userLoginDto))
            {
                return EUserLoginResult.UserNotExist;
            }
            var userLoginResult = CheckLoginUser(userLoginDto);  
            if (userLoginResult.Equals(EUserLoginResult.Successful))
            {

            }
            return 0;

        }

        #region MyRegion

        /// <summary>
        /// 验证输入参数
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        protected bool CheckUserLoginDto(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
                return false;

            if (string.IsNullOrWhiteSpace(userLoginDto.EmailAddress) && string.IsNullOrWhiteSpace(userLoginDto.LoginName) && string.IsNullOrWhiteSpace(userLoginDto.UserName))
                return false;

            if (string.IsNullOrWhiteSpace(userLoginDto.Password))
                return false;

            return true;
        }

        #endregion
    }
}
