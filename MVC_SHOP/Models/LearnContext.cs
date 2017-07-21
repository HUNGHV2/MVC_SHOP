using System.Data.Entity;

namespace MVC_SHOP.Models
{
    public class LearnContext : DbContext
    {
    
        public LearnContext() : base("name=LearnContext")
        {
        }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Register> Registers { get; set; }

        public System.Data.Entity.DbSet<MVC_SHOP.Models.Categories> Categories { get; set; }

        public System.Data.Entity.DbSet<MVC_SHOP.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<MVC_SHOP.Models.FeedBacks> FeedBacks { get; set; }
    }
}
