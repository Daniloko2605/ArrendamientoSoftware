using ArrendamientoSoftware.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Properties> Properties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
