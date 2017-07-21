using System.ComponentModel.DataAnnotations;

namespace MVC_SHOP.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}