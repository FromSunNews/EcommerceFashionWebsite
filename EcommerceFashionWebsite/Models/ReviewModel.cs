﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class ReviewModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
