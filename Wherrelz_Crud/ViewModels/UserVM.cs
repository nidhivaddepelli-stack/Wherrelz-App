using System.ComponentModel.DataAnnotations;

namespace Wherrelz_Crud.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public int HiddenId { get; set; }

        public string LoginId { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; }
       
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
