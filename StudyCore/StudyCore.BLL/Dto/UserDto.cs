using System;
using System.Collections.Generic;
using System.Text;
using StudyCore.Data.Data;

namespace StudyCore.BLL.Dto
{
    public class UserDto
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

    }
}
