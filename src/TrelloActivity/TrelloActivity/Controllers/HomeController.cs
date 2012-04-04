using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace TrelloActivity.Controllers
{
    public class HomeController : Controller
    {
     

        public HomeController()
        {
        
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}

