using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class M_User : BaseModel<Int64>
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

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LastLoginTime { get; set; }
    }
}
