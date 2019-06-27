using Microsoft.EntityFrameworkCore;

namespace Basty.Models
{
    public class BastyDBContext : DbContext
    {
         public BastyDBContext(DbContextOptions<BastyDBContext> options)
            :base(options)
        {
        }
        
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Team>()
        //         .HasOne<Player>(s => s.Player)
        //         .WithMany(g => g.Team)
        //         .HasForeignKey(s => s.TeamId);
        // }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // { 
        //     optionsBuilder.UseSqlServer(@"Server=.\;Database=BastyDB;Trusted_Connection=True;");
        // }

    }

   

//dotnet ef migrations add 
//dotnet ef database update
}