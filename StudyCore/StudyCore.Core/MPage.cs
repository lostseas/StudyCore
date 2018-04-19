using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Core
{
    public class MPage<TEntity>
    {
        public IEnumerable<TEntity> RowEntities { get; set; }

        public int TotalCount { get; set; }
    }
}
