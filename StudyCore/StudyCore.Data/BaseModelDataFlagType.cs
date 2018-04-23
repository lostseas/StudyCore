using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data
{
    public enum BaseModelDataFlagType
    {
        无效 = 0,
        /// <summary>
        /// 放入回收站 可恢复
        /// </summary>
        数据删除 = 1,
        数据有效 = 2
    }
}
