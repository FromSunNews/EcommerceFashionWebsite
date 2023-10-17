using System.ComponentModel.DataAnnotations;

namespace EcommerceFashionWebsite.Models
{
    public class ReviewModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
