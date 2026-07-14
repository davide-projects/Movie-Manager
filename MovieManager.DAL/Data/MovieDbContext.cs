using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Entities;

namespace MovieManager.DAL.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            
            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            
            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            
            modelBuilder.Entity<Movie>()
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Movie>()
                .Property(m => m.Budget)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movie>()
                .Property(m => m.Revenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Director>()
                .Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Director>()
                .Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Actor>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Actor>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewerName)
                .IsRequired()
                .HasMaxLength(100);

           
            modelBuilder.Entity<Review>()
                .Property(r => r.Score)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("CK_Review_Score", "Score >= 1 AND Score <= 10"));
        }
    }
}
