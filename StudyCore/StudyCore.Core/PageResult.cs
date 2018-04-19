using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Core
{
    /// <summary>
    /// 分页返回数据实体
    /// </summary>
    public partial class PageResult<T>
    {
        /// <summary>
        /// 返回的分页数据
        /// </summary>
        public IList<T> ResultList { get; set; }
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 返回页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (TotalCount % PageSize > 0)
                {
                    return TotalCount / PageSize + 1;
                }
                else
                {
                    return TotalCount / PageSize;
                };
            }
        }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        public PageResult(IList<T> resultList, int pageIndex, int pageSize, int totalCount)
        {
            this.ResultList = resultList;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
        }
    }
}
