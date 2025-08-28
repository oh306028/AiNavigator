using AiNavigator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiNavigator.Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<RequestHistory> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RequestHistory>(entity =>
            {
                entity.OwnsOne(r => r.Details, details =>
                {
                    details.Property(d => d.Name).HasColumnName("Name");
                    details.Property(d => d.ShortDescription).HasColumnName("ShortDescription");
                    details.Property(d => d.Link).HasColumnName("Link");
                    details.Property(d => d.Pros).HasColumnName("Pros");
                    details.Property(d => d.Cons).HasColumnName("Cons");
                    details.Property(d => d.Rank).HasColumnName("Rank");
                    details.Property(d => d.GeneralSummary).HasColumnName("GeneralSummary");
                    details.Property(d => d.Category).HasColumnName("Category");
                    details.Property(d => d.QueryDate).HasColumnName("QueryDate");
                });
            });
        }

    }

}
