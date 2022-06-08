using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DescartesApi.Models
{
    public class DiffContext : DbContext
    {
        public DiffContext(DbContextOptions<DiffContext> options)
            : base(options)
        {
        }

        public DbSet<DiffItem> DiffItems { get; set; } = null!;
    }
}
