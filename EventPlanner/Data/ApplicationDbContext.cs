using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;

namespace EventPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<EventItem> EventItems => Set<EventItem>();
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Registration> Registrations => Set<Registration>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Registration>()
                .HasIndex(r => new { r.EventItemId, r.ParticipantId })
                .IsUnique();
        }

        

    }
}
