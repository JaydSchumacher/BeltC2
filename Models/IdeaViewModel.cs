using System.ComponentModel.DataAnnotations;


namespace Belt_Exam.Models
{

    public class IdeaViewModel
    {
        [Required]
        [MinLength(10)]
        public string description { get; set; }  

        
    }
}