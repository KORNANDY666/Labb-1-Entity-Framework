using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sistatestetlab1.Models;

namespace Sistatestetlab1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Sistatestetlab1.Models.Employee> Employee { get; set; } = default!;
        public DbSet<Sistatestetlab1.Models.Vacation> Vacation { get; set; } = default!;
    }
}