using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using StudyCore.BLL.Dto;
using StudyCore.Core.Context;
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



    }
}
