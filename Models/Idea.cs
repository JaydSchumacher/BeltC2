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

    public class Idea
    {
        public int ideaid { get; set; }
        public string description { get; set; }
        
        public int userid { get; set; }
        public User User { get; set; } 

        public List<Like> Likes { get; set; }
        
        public Idea ()
        {
            Likes = new List<Like>();
        }
    }
}