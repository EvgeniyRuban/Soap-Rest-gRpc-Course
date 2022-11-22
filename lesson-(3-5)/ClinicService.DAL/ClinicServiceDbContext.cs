using ClinicService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicService.DAL;

public class ClinicServiceDbContext : DbContext
{
	public ClinicServiceDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Client> Clients { get; set; } = null!;
	public DbSet<Pet> Pets { get; set; } = null!;
    public DbSet<Consultation> Consultations { get; set; } = null!;
	public DbSet<Account> Accounts { get; set; } = null!;
	public DbSet<AccountSession> AccountSessions { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Consultation>().HasOne(p => p.Pet).WithMany(b => b.Consultations).HasForeignKey(p => p.PetId)
				.OnDelete(DeleteBehavior.NoAction);
	}
}