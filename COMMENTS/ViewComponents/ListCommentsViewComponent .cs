using PresentationLayer.ViewModels;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite.Internal;

namespace PresentationLayer.ViewComponents
{
    public class ListCommentsViewComponent : ViewComponent
    {
        IConfiguration _configuration;

        public List<VComment> GetComments(string path)
        {
            var list = new BusinessLayer.Comments(_configuration).GetListByPath(path);
            
            var flat = list.Select(x => new VComment {
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

        public List<VComment> GetTree(List<VComment> comments)
        {
            var tree = comments.Where(x => x.ParentId == null).ToList();      
            
            foreach(var c in tree)
            {
                c.Children = GetChildren(comments, c);
            }
            
            return tree;
        }

        public List<VComment> GetChildren(List<VComment> comments, VComment current)
        {
            var children = comments.Where(x => x.ParentId == current.Id).ToList();
            foreach(var c in children)
            {
                c.Children = GetChildren(comments, c);
            }

            return children;
        }

        public ListCommentsViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string path)
        {
            var data = GetComments(path);
            return await Task.FromResult((IViewComponentResult)View("Default", data));
        }

    }
}
