using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MVC_SHOP.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Account { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public int? RoleId { get; set; }

        public bool? IsActive { get; set; }

        public string ImageUrl { get; set; }

        public virtual UserRole UserRoles { get; set; }

        public string Createdby { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Modifiedby { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(255,MinimumLength = 1,ErrorMessage = "Input value less than 255 characters")]
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Creared { get; set; }
        public string ModifieddBy { get; set; }
        public DateTime? Modified { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public System.DateTime? BirthDate { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập thông tin đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ mật khẩu?")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Bạn chưa điền tên tài khoản")]
        [Display(Name = "User name")]

        public string UserName { get; set; }

        [Required(ErrorMessage ="Bạn chưa điền mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Xác nhận mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }

        public string PhoneNo { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập địa chỉ email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}