using Microsoft.EntityFrameworkCore;
using DesafioBancoDigital.Domain.Entites;

namespace DesafioBancoDigital.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Conta> Contas { get; set; }
    }
}
