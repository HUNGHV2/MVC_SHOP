using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MVC_SHOP.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<News> NewsList { get; set; }
    }
    public class News
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string Title { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string CreatedBy { get; set; }
        public DateTime? Creared { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string ModifieddBy { get; set; }
        public DateTime? Modified { get; set; }
        [Column(TypeName = "ntext"),AllowHtml,MaxLength]
        public  string Content { get; set; }
        public  string Icon { get; set; }
        public  string Image { get; set; }
        public virtual Categories Catrgories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }

    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string Title { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string CreatedBy { get; set; }
        public DateTime? Creared { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string ModifieddBy { get; set; }
        public DateTime? Modified { get; set; }
        public virtual ICollection<News> NewsCategories { get; set; }
    }
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string CommentsText { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string CreatedBy { get; set; }
        public DateTime? Creared { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string ModifieddBy { get; set; }
        public DateTime? Modified { get; set; }
        public string Reply { get; set; }
        public virtual News News { get; set; }
    }

    public class FeedBacks
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "ntext"), MaxLength]
        public string Content { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string CreatedBy { get; set; }
        public DateTime? Creared { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Input value less than 255 characters")]
        public string ModifieddBy { get; set; }
        public DateTime? Modified { get; set; }
    }

}