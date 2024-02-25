using DAL.Entity;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class MyDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> User { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=RIHAB\SQLEXPRESS;Initial Catalog=BloggingTech;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        }

        
    }
}
