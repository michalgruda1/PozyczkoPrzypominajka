using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Models;

namespace PozyczkoPrzypominajkaV2.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}

		public DbSet<Loan> Loans { get; set; } = null!;
		public DbSet<Notification> Notifications { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Loan>().HasKey(l => l.LoanID);
			builder.Entity<Loan>().Property(l => l.Date).IsRequired();
			builder.Entity<Loan>().HasOne(l => l.Giver).WithMany().IsRequired().HasForeignKey("GiverID");
			builder.Entity<Loan>().HasOne(l => l.Receiver).WithMany().IsRequired().HasForeignKey("ReceiverID");
			builder.Entity<Loan>().Property(l => l.Amount).IsRequired().HasColumnType("decimal(18,4)");
			builder.Entity<Loan>().Property(l => l.RepaymentDate).IsRequired();
			builder.Entity<Loan>().Property(l => l.RepaymentAmount).IsRequired().HasColumnType("decimal(18,4)");
			builder.Entity<Loan>().Property(l => l.Status).IsRequired().HasConversion<int>();
			builder.Entity<Loan>().HasMany<Notification>().WithOne(n => n.Loan).HasForeignKey(n => n.LoanID).OnDelete(DeleteBehavior.Cascade).IsRequired();

			builder.Entity<Notification>().Property(n => n.When).IsRequired();
			builder.Entity<Notification>().Property(n => n.Message).IsRequired();
			builder.Entity<Notification>().Property(n => n.Channel).IsRequired().HasConversion<int>();
			builder.Entity<Notification>().HasOne(n => n.Loan).WithMany(l => l.Notifications).HasForeignKey(p => p.LoanID);

			builder.Entity<AppUser>().Property(u => u.Imie).IsRequired();
			builder.Entity<AppUser>().Property(u => u.Nazwisko).IsRequired();

			base.OnModelCreating(builder);

			// Users and roles init is in DBInitialization class

		}
	}
}
