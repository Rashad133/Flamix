using Microsoft.Build.Framework;

namespace Flamix.Areas.Admin.ViewModels
{
    public class UpdateSettingVM
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
