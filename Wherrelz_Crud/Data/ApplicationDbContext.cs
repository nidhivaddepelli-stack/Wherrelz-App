using Microsoft.EntityFrameworkCore;
using Wherrelz_Crud.Models;

namespace Wherrelz_Crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<EntryModel> Entries { get; set; }

        public DbSet<AuditModel> Audits { get; set; }
    }
}
