using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class M_Role : BaseModel<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplyName { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }
    }
}
