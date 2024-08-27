using asterian_server.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class AppDBContext : DbContext
    {

        private readonly IConfiguration _configuration;
        public AppDBContext(IConfigurationRoot configuration) 
        {
            _configuration = configuration;
        }

        public DbSet<Machine> Machine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    
    }
}
