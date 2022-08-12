using Microsoft.EntityFrameworkCore;

namespace KonsultProfil.Api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<ConsultProfile> ConsultProfiles { get; set; } = null!;
        public DbSet<Assignment> Assignments { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
    }
}