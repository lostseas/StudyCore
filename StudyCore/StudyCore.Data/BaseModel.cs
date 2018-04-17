using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StudyCore.Data
{
    public class BaseModel<T>
    {
        /// <summary>
        /// ID
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public int UpdateUserId { get; set; }

        /// <summary>
        /// 数据有效性 0 数据无效 1 数据即将删除（回收站数据） 2 数据 有效
        /// </summary>
        public int DataFlag { get; set; }
    }
}
