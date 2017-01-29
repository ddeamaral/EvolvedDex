using EvolvedDex.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EvolvedDex.Models.Contexts.DataContext
{
    public class PokedexContext : DbContext
    {
        private readonly string connectionString;

        public PokedexContext(DbContextOptions options, IConfigurationRoot builder) : base(options)
        {
            connectionString = builder.GetConnectionString("PokedexDb");
        }
        
        public DbSet<Pokemon> Pokemon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql(connectionString);           
        

    }
}
