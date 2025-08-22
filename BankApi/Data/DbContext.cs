using BankApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<BankCard> BankCards { get; set; } = null!;
        public DbSet<Transaction> transactions { get; set; } = null!;
    }
}
