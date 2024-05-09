using Cars.Authentication;
using Cars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cars.NewFolder
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<User> SystemUser { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Images> Images { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var confing = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var ConnectionString = confing.GetSection("constr").Value;
            optionsBuilder.UseSqlServer(ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }


}
