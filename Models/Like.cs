using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Belt_Exam.Models;
using Microsoft.AspNetCore.Http;


namespace Belt_Exam.Models
{

    public class Like
    {   
        [Key]
        public int likeid { get; set; }

        public int ideaid {get; set;}
        public Idea Idea { get; set; }

        public int userid { get; set; }
        public User User { get; set; }
        
    }
}