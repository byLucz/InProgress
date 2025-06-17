using XKANBAN.Models.User;
using XKANBAN.Models.Project;
using Microsoft.EntityFrameworkCore;
using DIPLOMKANBAN.Models.Template;

namespace XKANBAN.Contxet
{
    public class KanBanContext : DbContext
    {
        public KanBanContext(DbContextOptions<KanBanContext> options) : base(options)
        { }

        public DbSet<User> Users { internal get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Column> CustomColumns { get; set; }
        public DbSet<SAnnouncement> Announcement { get; set; }
        public DbSet<Template> Templates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Project>()
                .HasQueryFilter(p => !p.IsDelete);

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
        }
    }
}