using ElizabethJames_CodeExample.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ElizabethJames_CodeExample.DAL
{
    public class ComputerGamesContext : DbContext
    {

        public ComputerGamesContext() : base("ComputerGamesContext")
        {
        }

        public DbSet<ComputerGame> ComputerGames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}