using System.ComponentModel.DataAnnotations;

namespace Flamix.Areas.Admin.ViewModels
{
    public class CreateServiceVM
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

    }
}
