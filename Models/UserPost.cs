namespace WSB_RadioForum.Models
{
    public class UserPost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

        public string GetFormattedDate()
        {
            return DateAdded.ToString("dd/MM/yyyy");
        }

    }
}
