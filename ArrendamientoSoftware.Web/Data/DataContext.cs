using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data.Entities;
using System.Reflection.Emit;

namespace ArrendamientoSoftware.Web.Data
{
    public class DataContext : IdentityDbContext<Usuarios>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ArrendamientoSoftwareRole> ArrendamientoSoftwareRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Propiedades> Propiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureKeys(builder);
            ConfigureIndexes(builder);

            base.OnModelCreating(builder);
        }

        private void ConfigureKeys(ModelBuilder builder)
        {
            // Role Permissions
            builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>().HasOne(rp => rp.Role)
                                            .WithMany(r => r.RolePermissions)
                                            .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>().HasOne(rp => rp.Permission)
                                            .WithMany(p => p.RolePermissions)
                                            .HasForeignKey(rp => rp.PermissionId);
        }

        private void ConfigureIndexes(ModelBuilder builder)
        {
            // Roles =
            builder.Entity<ArrendamientoSoftwareRole>().HasIndex(r => r.Name)
                                             .IsUnique();
            // Sections = propiedades
            builder.Entity<Propiedades>().HasIndex(s => s.Tipo)
                                             .IsUnique();
            // Users
            builder.Entity<Usuarios>().HasIndex(u => u.Nombre)
                                             .IsUnique();
        }
    }
}
