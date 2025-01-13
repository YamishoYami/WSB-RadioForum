using WSB_RadioForum.Data;

namespace WSB_RadioForum.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public int UserPostId { get; set; }
        public virtual UserPost? UserPost { get; set; }
    }
}
