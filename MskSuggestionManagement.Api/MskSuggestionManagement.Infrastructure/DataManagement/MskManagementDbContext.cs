using Microsoft.EntityFrameworkCore;
using MskSuggestionManagement.Domain.Entities;

namespace MskSuggestionManagement.Infrastructure.DataManagement
{
    public class MskManagementDbContext : DbContext
    {
        public MskManagementDbContext(DbContextOptions<MskManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MskRecommendation> MskRecommendations { get; set; }
        public DbSet<EmployeeMskRecommendation> EmployeeMskRecommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<MskRecommendation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<EmployeeMskRecommendation>(entity =>
            {
                entity.HasIndex(e => new { e.EmployeeId, e.MskRecommendationId }).IsUnique();
            });

            modelBuilder.Entity<EmployeeMskRecommendation>(entity =>
            {
                entity.HasKey(x => new { x.EmployeeId, x.MskRecommendationId });
                entity.HasOne(x => x.Employee)
                      .WithMany(e => e.EmployeeMskRecommendations)
                      .HasForeignKey(x => x.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.MskRecommendation)
                      .WithMany(r => r.EmployeeMskRecommendations)
                      .HasForeignKey(x => x.MskRecommendationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
