using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Data
{
    public class StudyCoreDbContext : DbContext
    {
        public StudyCoreDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
