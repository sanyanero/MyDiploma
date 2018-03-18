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
            optionsBuilder.UseSqlServer(_options?.Value?.DefaultConnectionSqlServer ??
                "Server = САНЯ-ПК; Database = MyDb; Trusted_Connection = True; MultipleActiveResultSets = true");
        }
        #endregion

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
