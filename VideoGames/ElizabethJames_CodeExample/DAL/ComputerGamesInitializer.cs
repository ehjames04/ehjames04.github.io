using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ElizabethJames_CodeExample.Models;

namespace ElizabethJames_CodeExample.DAL
{
    public class ComputerGamesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ComputerGamesContext>
    {
        protected override void Seed(ComputerGamesContext context)
        {
            var computerGames = new List<ComputerGame>
            {
                new ComputerGame{ Name="Minecraft",Description="Minecraft is all about building. You're dropped in a randomly generated world and can make tools and buildings out of raw materials.",ReleaseDate=DateTime.Parse("2011-11-18"),Rating=10}
            };

            computerGames.ForEach(cg => context.ComputerGames.Add(cg));
            context.SaveChanges();
        }
    }
}