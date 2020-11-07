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

namespace PresentationLayer.ViewComponents
{
    public class CreateCommentViewComponent : ViewComponent
    {

        public CreateCommentViewComponent(IConfiguration _configuration)
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {            
            return await Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
