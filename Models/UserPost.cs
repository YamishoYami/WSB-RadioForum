using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSB_RadioForum.Models
{
    public class UserPost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        [NotMapped]
        public bool DeleteImage { get; set; } = false;
        public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

        public string GetFormattedDate()
        {
            return DateAdded.ToString("dd/MM/yyyy");
        }

    }
}
