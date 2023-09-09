using System;
using Microsoft.EntityFrameworkCore;
using backend.Core.Entities;
namespace backend.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>()
                .HasOne(job => job.Company)
                .WithMany(company => company.Jobs)
                .HasForeignKey(job => job.CompanyId);
            modelBuilder.Entity<Candidate>()
                .HasOne(candidate => candidate.Job)
                .WithMany(job => job.Candidates)
                .HasForeignKey(candi => candi.JobId);
            modelBuilder.Entity<Company>()
                .Property(company => company.Size)
                .HasConversion<string>();
            modelBuilder.Entity<Job>()
          .Property(job => job.Level)
          .HasConversion<string>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ResumeManagementDB;Encrypt=False;User ID=SA;Password=yourStrong(!)Password");
        }
    }
}

