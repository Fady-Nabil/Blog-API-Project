using System;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
