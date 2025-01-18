using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSB_RadioForum.Models
{
    public class UserPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(1024, ErrorMessage = "Content cannot be longer than 1024 characters.")]
        public string Content { get; set; }
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
