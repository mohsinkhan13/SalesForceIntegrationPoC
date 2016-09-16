using IdentityModel.Client;
using SupportCase.UI.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SupportCase.UI.Controllers
{
    public class CaseController : Controller
    {
        // GET: Case
        public async Task<ActionResult> Index()
        {
            var response = await GetTokenAsync();
            //var result = await CallApi(response.AccessToken);

            return Content(response.HttpErrorReason);
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

        private async Task<string> CallApi(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var result = await client.GetStringAsync("https://eu11.salesforce.com/services/data/v37.0/sobjects/case/");

            return result;
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new TokenClient(
                "https://eu11.salesforce.com/services/oauth2/token",
                "3MVG9HxRZv05HarTSJgKRE.cSe2bXxWHSGRRRorSvgPdndC3ij7Rh_ybeToHZ_3IDOnjZeUNLFiM5X4UICdeU",
                "6282425504841271049");

            return await client.RequestResourceOwnerPasswordAsync("mohsink13@gmail.com", "Salesf0rceK08H3JHxrYXz33N9T73cX7ld");
        }
    }

    
}