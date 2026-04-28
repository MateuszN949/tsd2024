using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcYummy.Models;

namespace MvcYummy.Data
{
    public class MvcYummyContext : DbContext
    {
        public MvcYummyContext (DbContextOptions<MvcYummyContext> options)
            : base(options)
        {
        }

        public DbSet<MvcYummy.Models.Recipe> Recipe { get; set; } = default!;
    }
}
