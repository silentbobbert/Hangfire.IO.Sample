using Hangfire.IO.Sample.BusinessLogic;
using System;
using System.Web.Mvc;

namespace Hangfire.IO.Sample.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Queue()
        {
            BackgroundJob.Enqueue<Worker>(w => w.DoWork(DateTime.Now));

            return RedirectToAction("Index");
        }
    }
}