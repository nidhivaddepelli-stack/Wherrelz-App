using System.ComponentModel.DataAnnotations;

namespace Wherrelz_Crud.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string LoginId { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; } 

        [EmailAddress] 
        [MaxLength(256)] 
        public string Email { get; set; }
    }
}
