
using System.ComponentModel.DataAnnotations;

namespace Flamix.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
