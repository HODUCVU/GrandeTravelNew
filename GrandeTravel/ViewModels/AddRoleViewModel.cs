using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class AddRoleViewModel
    {
        [Display(Name = "New Role"), MaxLength(15)]
        public string? NewRole { get; set; }
    }
}
