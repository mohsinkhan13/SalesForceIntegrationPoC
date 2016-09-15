using SupportCase.UI.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SupportCase.UI.Controllers
{
    public class CaseController : Controller
    {
        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void CreateCase(Case model)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory,
                   Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);
                
            }

            Response.RedirectToRoute(new { controller = "case", action = "index" });
        }
    }

    
}