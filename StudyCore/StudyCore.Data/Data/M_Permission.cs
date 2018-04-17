using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    public class M_Auth : BaseModel<Int64>
    {
        /// <summary>
        /// 权限名称
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
