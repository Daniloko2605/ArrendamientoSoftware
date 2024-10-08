using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data.Entities;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;
using ArrendamientoSoftware.Web.Data.Entities.PrivateBlog.Web.Data.Entities;

namespace ArrendamientoSoftware.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Propiedad> Properties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
