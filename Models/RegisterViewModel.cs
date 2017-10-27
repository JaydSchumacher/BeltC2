using System.ComponentModel.DataAnnotations;


namespace Belt_Exam.Models
{

    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string name { get; set; }

        [Required]
        [MinLength(2)]
        public string alias { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "Password confirmation must match Password")]
        public string cpassword { get; set; }

        
    }
}