using System.ComponentModel.DataAnnotations;

namespace GrandeTravel.ViewModels
{
    public class ContactViewModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Email Address")]
        public string FromAddress { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
