using System.ComponentModel.DataAnnotations;

namespace Wherrelz_Crud.ViewModels
{
    public class LoginVM
    {
       
        public int Id { get; set; }
        
        public string LoginId { get; set; }
       
        public string Password { get; set; }
    }
}
