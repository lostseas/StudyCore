using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    public class M_UserPermission : BaseModel<Int64>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Int64 UserId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public Int64 PermissionId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }
    }
}
