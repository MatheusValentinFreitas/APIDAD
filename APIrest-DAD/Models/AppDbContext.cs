using Microsoft.EntityFrameworkCore;

namespace APIrest_DAD.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        public DbSet<Notificacao> notificacao { get; set; }

        public DbSet<OauthToken> oauthToken { get; set; }
    }
}
