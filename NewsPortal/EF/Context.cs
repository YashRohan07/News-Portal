using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsPortal.EF
{
    public class Context : DbContext
    {
        public DbSet<News> News { get; set; }
    }
}