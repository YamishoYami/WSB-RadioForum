using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WSB_RadioForum.Models;

namespace WSB_RadioForum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WSB_RadioForum.Models.UserPost> UserPost { get; set; } = default!;
        public DbSet<WSB_RadioForum.Models.Comments> Comments { get; set; } = default!;
    }
}
