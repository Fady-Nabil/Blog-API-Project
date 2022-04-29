

using System;

namespace BlogAPI.Models.Dto
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
