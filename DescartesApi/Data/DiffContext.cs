using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DescartesApi.Models;

namespace DescartesApi.Data
{
    public class DiffContext : DbContext
    {
        public DiffContext (DbContextOptions<DiffContext> options)
            : base(options)
        {
        }

        public DbSet<DescartesApi.Models.DiffItem>? DiffItems { get; set; }
    }
}
