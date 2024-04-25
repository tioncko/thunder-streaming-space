using Microsoft.EntityFrameworkCore;
using thunder_streaming_space.Properties;
using thunder_streaming_space.Settings;

namespace thunder_streaming_space.Database
{
    internal class EntitySQLConn : DbContext
    {
        public virtual DbSet<ReturnParam>? APIParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
             optionsBuilder.UseNpgsql(Parameters.Build().GetSection("ConnectionStrings").GetChildren().First().Value!);

        //OnModelCreating is used to create the table and columns for the database if they do not exist through the use of the modelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReturnParam>(item => {
                item.ToTable("api_parameters");
                item.Property(p => p.Id).HasColumnName("id").HasDefaultValueSql("nextval('account.item_id_seq'::regclass)");
                item.Property(p => p.Token).HasColumnName("access_token");
                item.Property(p => p.TokenType).HasColumnName("token_type");
            });
        }

        //public async Task<List<APIParam>> GetAPI() {
        public static List<ReturnParam> GetAPI() {
            using (var db = new EntitySQLConn()) {
                return db.APIParameters!.ToListAsync().GetAwaiter().GetResult();
            }
        }
    }
}
