﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PozyczkoPrzypominajka.Models;

namespace PozyczkoPrzypominajkaV2.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}

		public DbSet<Loan> Loans { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Loan>().Property(l => l.Date).IsRequired();
			builder.Entity<Loan>().HasOne(l => l.Giver).WithMany().IsRequired();
			builder.Entity<Loan>().HasOne(l => l.Receiver).WithMany().IsRequired();
			builder.Entity<Loan>().Property(l => l.Amount).IsRequired();
			builder.Entity<Loan>().Property(l => l.RepaymentDate).IsRequired();
			builder.Entity<Loan>().Property(l => l.RepaymentAmount).IsRequired();
			builder.Entity<Loan>().HasMany<Notification>().WithOne(n => n.Loan).HasForeignKey(n => n.LoanID).OnDelete(DeleteBehavior.Cascade).IsRequired();
			builder.Entity<Loan>().HasMany<Payment>().WithOne(p => p.Loan).HasForeignKey(p => p.LoanID).OnDelete(DeleteBehavior.Cascade).IsRequired();

			builder.Entity<Payment>().Property(p => p.PlannedPaymentDate).IsRequired();
			builder.Entity<Payment>().Property(p => p.Amount).IsRequired();
			builder.Entity<Payment>().Property(p => p.IsPaid).IsRequired();

			builder.Entity<Notification>().Property(n => n.When).IsRequired();
			builder.Entity<Notification>().Property(n => n.Text).IsRequired();
			builder.Entity<Notification>().Property(n => n.Channel).IsRequired().HasConversion<int>();

			builder.Entity<User>().Property(u => u.Imie).IsRequired();
			builder.Entity<User>().Property(u => u.Nazwisko).IsRequired();

			base.OnModelCreating(builder);
		}
	}
}
