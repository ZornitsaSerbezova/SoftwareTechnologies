﻿using Solutions.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solutions.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Post/List
        public ActionResult List()
        {
            using (var database = new ApplicationDbContext())
            {
                var posts = database.Posts
                    .Include(a => a.Author)
                    .ToList();

                return View(posts);
            }
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                using (var database = new ApplicationDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;

                    post.AuthorId = authorId;

                    database.Posts.Add(post);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }
    }
}