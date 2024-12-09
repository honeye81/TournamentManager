using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;


namespace Tournament.Data.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tournaments.Core.Entities.Tournament> Tournaments { get; set; }
        public DbSet<Tournaments.Core.Entities.Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournaments.Core.Entities.Tournament>()
                .HasMany(t => t.Games)
                .WithOne(g => g.Tournament)
                .HasForeignKey(g => g.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
