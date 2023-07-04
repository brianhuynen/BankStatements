using BankStatements.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Models
{
    public class BankStatementContext : DbContext
    {
        public DbSet<BankStatement> BankStatements { get; set; }
        public DbSet<Iban> Ibans { get; set; }

        public BankStatementContext(DbContextOptions<BankStatementContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Preseeds the DB with an Iban and BankStatement entry
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Iban>().HasData(new Iban
            {
                Id = 1,
                IbanNo = "NL12BANK3456789100"
            });

            modelBuilder.Entity<BankStatement>().HasData(new BankStatement
            { 
                Id = 1,
                ReferenceId = 1,
                IbanNo = "NL12BANK3456789100",
                BalanceStart = 0,
                Mutation = 1000,
                BalanceEnd = 1000
            });
        }
    }
}
