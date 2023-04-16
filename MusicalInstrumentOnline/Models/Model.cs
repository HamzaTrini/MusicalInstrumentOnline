using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicalInstrumentOnline.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Contact
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string? name { get; set; }
        [EmailAddress(ErrorMessage = "The Email is required")]
        public string? email { get; set; }
        [Required(ErrorMessage = "The SpicalNote is required")]
        public string? spicalNote { get; set; }
    }
    public class Category
    {
        public int categoryId { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string? name { get; set; }
        public string? emagePath { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "The Image File is required")]
        public IFormFile? emageFile { get; set; }
    }

    public class TeamMember
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string? name { get; set; }
        public string? emagePath { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "The Image File is required")]
        public IFormFile? emageFile { get; set; }
        [Required(ErrorMessage = "The Description is required")]
        public string? description { get; set; }    
    }

    public class Tastimonal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Description is required")]

        public string? Description { get; set; }
        public string? imagepath { get;  set; }
        [Required(ErrorMessage = "The Name is required")]
        public string? name { get;  set; }
        [NotMapped]
        [Required(ErrorMessage = "The Image File is required")]
        public IFormFile? imageFile { get;  set; }    

    }

    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Description is required")]

        public string? Description1 { get; set; }
        [Required(ErrorMessage = "The Description is required")]

        public string? Description2 { get; set; }
        [Required(ErrorMessage = "The Description is required")]

        public string? Description3 { get; set; }
        public string? imagepath { get; set; }
        
        [NotMapped]
        [Required(ErrorMessage = "The Image File is required")]
        public IFormFile? imageFile { get; set; }

    }
    public class Product
    {
        public int productId { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string? name { get; set; }
        public string? imagePath { get; set; }
        [Required(ErrorMessage = "The Price is required")]
        public string? price { get; set; }
        [Required(ErrorMessage = "The CategoryId is required")]
        public int categoryId { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "The Image File is required")]
        public IFormFile? emageFile { get; set; }
    }

    public class RegisterClass
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The Fullname is required")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage = "The User Name is required")]
        public string? userName { get; set; }
        [Required(ErrorMessage = "The Password is required")]
        [PasswordPropertyText]
        public string? Password { get; set; }
        [Required(ErrorMessage = "The Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        
    }

    public class User_
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The User Name is required")]
        public string? userName { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        [PasswordPropertyText]
        public string? Password { get; set; }
        public string? Rollid { get; set; }

    }
}