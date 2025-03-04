using ABCBank.Areas.Identity.Data;
using ABCBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABCBank.Areas.Identity.Data;

public class ABCBankContext : IdentityDbContext<ABCBankUser>
{
    public ABCBankContext(DbContextOptions<ABCBankContext> options)
        : base(options)
    {
    }

    public DbSet<TransactionModal> Transactions { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
