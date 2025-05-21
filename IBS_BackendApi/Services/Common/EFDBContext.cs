using IBS_BackendApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IBS_BackendApi.Services.Common
{
    public class EFDBContext : DbContext
    {
        public EFDBContext(DbContextOptions options) : base(options) { }

        public DbSet<AdminEntities> Admin { get; set; }
        public DbSet<CustomerEntities> Customer { get; set; }
        public DbSet<AccountEntities> Account { get; set; }
        public DbSet<TransactionHistoryEntities> TransactionHistory { get; set; }
        public DbSet<DepositeEntities> Desposite { get; set; }
    }
}
