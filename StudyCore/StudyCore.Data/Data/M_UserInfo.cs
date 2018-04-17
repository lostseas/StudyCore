using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class M_UserInfo : BaseModel<Int64>
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
    }
}
