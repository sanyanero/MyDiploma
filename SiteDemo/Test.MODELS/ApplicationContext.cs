using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Test.MODELS.Entities;
using Test.MODELS.Options;

namespace Test.MODELS
{
    public class ApplicationContext : DbContext
    {
        #region Config
        private readonly IOptions<AppConnectionStrings> _options;

        public ApplicationContext()
        {

        }

        public ApplicationContext(IOptions<AppConnectionStrings> options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_options?.Value?.DefaultConnectionSqlServer ?? "TestDataBase");
        }
        #endregion

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
