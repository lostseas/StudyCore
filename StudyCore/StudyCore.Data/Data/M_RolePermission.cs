using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data.Data
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class M_RolePermission : BaseModel<Int64>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Int64 RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public Int64 PermissionId { get; set; }
    }
}
