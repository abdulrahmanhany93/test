using CleanProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; init; }
    public DbSet<AccountTransaction> AccountTransactions { get; init; }
    public DbSet<Atm> Atms { get; init; }
    public DbSet<AtmTransaction> AtmTransactions { get; init; }
    public DbSet<CreditCard> CreditCards { get; init; }
    public DbSet<Account> Accounts { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}