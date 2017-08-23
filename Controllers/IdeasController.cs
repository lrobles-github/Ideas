using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Ideas.Models;
using Microsoft.EntityFrameworkCore;

namespace Ideas.Controllers
{
    public class IdeasController : Controller
    {

        private IdeasContext _context;
 
        public IdeasController(IdeasContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ideaboard")]
        public IActionResult IdeaBoard()
        {

            if (HttpContext.Session.GetInt32("LoggedIn") == 1) 
            {
                User UserChecker = _context.Users.SingleOrDefault(x => x.Id == HttpContext.Session.GetInt32("UserId"));
                ViewBag.DisplayName = UserChecker.FirstName;

                ViewBag.AllIdeas = _context.Ideas.Include(u => u.User).Include(l => l.Likes).ToList();

                return View("ideaboard");
            }
            else
            {
                return View("index");
            }

        }


        [HttpPost]
        [Route("post")]
        public IActionResult Post(string idea)
        {

            User UserChecker = _context.Users.SingleOrDefault(x => x.Id == HttpContext.Session.GetInt32("UserId"));

            Idea NewIdea = new Idea { 
                Content = idea,
                User = UserChecker,
                CreatedAt = DateTime.Now

            };

            _context.Ideas.Add(NewIdea);
            _context.SaveChanges();

            return RedirectToAction("ideaboard");

        }


        [HttpGet]
        [Route("/user/{id}")]
        public IActionResult ShowUser(int id)
        {

            if (HttpContext.Session.GetInt32("LoggedIn") == 1) 
            {
                User UserChecker = _context.Users.SingleOrDefault(x => x.Id == id);

                ViewBag.IdeaCount = _context.Ideas.Count(p => p.User == UserChecker);

                ViewBag.LikeCount = _context.Likes.Count(p => p.User == UserChecker);
                            
                return View("user", UserChecker);
            }
            else
            {
                return View("index");
            }
        }


        [HttpGet]
        [Route("/idea/{id}")]
        public IActionResult ShowIdea(int id)
        {

            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                Idea GetIdea = _context.Ideas.
                    Include(u => u.User).
                    Include(l => l.Likes).
                    SingleOrDefault(x => x.Id == id);                    
                
                ViewBag.Likers = _context.Likes.
                    Include(u => u.User).
                    Where(i => i.Idea == GetIdea);

                return View("idea", GetIdea);
            }
            else 
            {
                return View("index");
            }

        }


        [HttpGet]
        [Route("/like/{id}")]
        public IActionResult Like(int id)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {

                User UserChecker = _context.Users.SingleOrDefault(x => x.Id == HttpContext.Session.GetInt32("UserId"));

                List<Like> LikeChecker = _context.Likes.Where(x => x.IdeaId == id).ToList();

                bool flag = false; 

                foreach(var l in LikeChecker)
                {
                    if (l.UserId == UserChecker.Id)
                    {
                        flag = true;
                    }

                }

                if (flag == false)
                {
                    Like NewLike = new Like {
                        IdeaId = id,
                        UserId = (int)UserChecker.Id,
                        CreatedAt = DateTime.Now
                    };
        
                    _context.Add(NewLike);
                    _context.SaveChanges();
                                
                }                
                
                return RedirectToAction("ideaboard");

            }

            else 
            {
                return View("index");
            }
    
        }

    }
}