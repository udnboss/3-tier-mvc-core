using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers
{
    public class HomeController : BaseController
    {
        BusinessLayer.Comments business;
        public HomeController(IConfiguration _configuration) : base(_configuration)
        {
            business = new BusinessLayer.Comments(_configuration);
        }

        public List<VComment> GetComments(string path)
        {
            var list = business.GetListByPath(path);

            var flat = list.Select(x => new VComment
            {
                Comment = x.Comment,
                DatePosted = x.DatePosted,
                DomainId = x.DomainId,
                Id = x.Id,
                Ip = x.Ip,
                Name = x.Name,
                ParentId = x.ParentId,
                Path = x.Path,
                QueryString = x.QueryString,
                Children = new List<VComment>()
            }).ToList();

            var tree = GetTree(flat);

            return tree;
        }

        private List<VComment> GetTree(List<VComment> comments)
        {
            var tree = comments.Where(x => x.ParentId == null).ToList();

            foreach (var c in tree)
            {
                c.Children = GetChildren(comments, c);
            }

            return tree;
        }

        private List<VComment> GetChildren(List<VComment> comments, VComment current)
        {
            var children = comments.Where(x => x.ParentId == current.Id).ToList();
            foreach (var c in children)
            {
                c.Children = GetChildren(comments, c);
            }

            return children;
        }

        public IActionResult Index()
        {
            var data = GetComments("/");
            ViewBag.Comment = Newtonsoft.Json.JsonConvert.SerializeObject(new VComment() { Path = "/" });
            return View(data);
        }

        [HttpPost]
        public IActionResult PostComment(VComment c)
        {
            var comment = new DataLayer.Models.TComment
            {
                Id = Guid.NewGuid(),
                Comment = c.Comment,
                DatePosted = DateTime.Now,
                DomainId = "gpucheck",
                Ip = "11111111",
                Path = "/",
                Name = c.Name
            };

            var posted = business.PostComment(comment);
            return Json(posted);
        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}
