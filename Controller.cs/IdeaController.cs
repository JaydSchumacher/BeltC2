using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Belt_Exam.Models;
using Microsoft.AspNetCore.Http;

namespace Belt_Exam.Controllers
{
    public class IdeaController : Controller
    {

        private Belt_ExamContext _context;
 
        public IdeaController(Belt_ExamContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
            ViewBag.curUserName = HttpContext.Session.GetString("UserName");
            ViewBag.allideas = _context.ideas.Include(u => u.User).ToList();
            List<Like> mylikes = new List<Like>();
            mylikes = _context.likes.Include(u => u.User).ToList();
            ViewBag.mylikes = mylikes;
            ViewBag.likes = _context.likes.ToList();
            return View("Dashboard");
        }

        [HttpGet]
        [Route("likestatus/{ideaid}")]
        public IActionResult UserProfile(int ideaid)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            List<Like> alllikes = new List<Like>();
            alllikes = _context.likes.Include(u => u.User).ToList();
            List<Like> wholiked = new List<Like>();
            foreach(var like in alllikes)
            {
                if(like.ideaid == ideaid)
                {
                    wholiked.Add(like);
                }
            }
            ViewBag.curidea = _context.ideas.Where(i => i.ideaid == ideaid).Include(u => u.User).ToList();
            ViewBag.wholiked = wholiked;
            return View("LikeStatus", "Idea");
        }

        [HttpGet]
        [Route("like/{ideaid}")]
        public IActionResult AddLike(int ideaid)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Like NewLike = new Like{
                        ideaid = ideaid,
                        userid = Convert.ToInt32(HttpContext.Session.GetInt32("Userid"))          
                    };
                    _context.Add(NewLike);
                    _context.SaveChanges();
                return RedirectToAction("Dashboard", "Idea");
            }
        }

        [HttpGet]
        [Route("userprofile/{userid}")]
        public IActionResult CurrentBelt(int userid)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }
            List<User> curProfile = new List<User>();
            curProfile = _context.users.Where(i => i.userid == userid).ToList();
            ViewBag.profileuser = curProfile;
            List<Idea> ideaNum = new List<Idea>();
            ideaNum = _context.ideas.Where(i => i.userid == userid).ToList();
            ViewBag.ideaNum = ideaNum.Count;
            List<Like> likeNum = new List<Like>();
            likeNum = _context.likes.Where(i => i.userid == userid).ToList();
            ViewBag.likeNum = likeNum.Count;
            return View("UserProfile");
        }

        [HttpPost]
        [Route("newidea")]
        public IActionResult AddIdea(IdeaViewModel model)
        {
            if(HttpContext.Session.GetInt32("Userid") == null){
                return RedirectToAction("Index", "Login");
            }            
            Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
            if(ModelState.IsValid == false)
            {
                List<string> errorList = new List<string>();
                foreach (var error in ModelState.Values)
                {
                    foreach(var moreerrors in error.Errors){
                        errorList.Add(moreerrors.ErrorMessage);
                    }
                    
                }
                ViewBag.errors = errorList;
                return View("Dashboard", "Idea");
            }
            else
            {
                Idea NewIdea = new Idea{
                    description = model.description,
                    userid = Convert.ToInt32(HttpContext.Session.GetInt32("Userid"))          
                
                };
                _context.Add(NewIdea);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Idea");
                
            }
        }

        // [HttpGet]
        // [Route("currentbelt/{beltid}")]
        // public IActionResult CurrentBelt(int beltid)
        // {
        //     HttpContext.Session.SetInt32("curBeltId", beltid);
        //     if(HttpContext.Session.GetInt32("Userid") == null){
        //         return RedirectToAction("Index", "Login");
        //     }
        //     List<Belt> curBelt = new List<Belt>();
        //     curBelt = _context.belts.Where(i => i.beltid == beltid).ToList();
        //     ViewBag.curBelt = curBelt;
        //     return View("CurrentBelt");
        // }

        // [HttpPost]
        // [Route("completebelt")]
        // public IActionResult CompleteBelt(IdeaViewModel model)
        // {
        //     if(HttpContext.Session.GetInt32("Userid") == null){
        //         return RedirectToAction("Index", "Login");
        //     }

        //     Console.WriteLine(HttpContext.Session.GetInt32("Userid"));
        //     if(ModelState.IsValid == false)
        //     {
        //         List<string> errorList = new List<string>();
        //         foreach (var error in ModelState.Values)
        //         {
        //             foreach(var moreerrors in error.Errors){
        //                 errorList.Add(moreerrors.ErrorMessage);
        //             }
                    
        //         }
        //         ViewBag.errors = errorList;
        //         return View("CurrentBelt");
        //     }
        //     else
        //     {
                          
        //         BeltsAchieved NewBeltAchieved = new BeltsAchieved{
        //             userid = Convert.ToInt32(HttpContext.Session.GetInt32("Userid")),
        //             beltid = beltid,
        //             achieved_at = model.achieved_at      
                
        //         };
        //         _context.Add(NewBeltAchieved);
        //         _context.SaveChanges();
        //         ViewBag.curBeltAch = NewBeltAchieved;
        //         Console.WriteLine("Created Successfully");
        //         return RedirectToAction("Dashboard");
                
                
        //     }
        // }  

        
    }
}
