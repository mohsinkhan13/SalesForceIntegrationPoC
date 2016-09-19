using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using SupportCase.UI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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
            var response = await GetTAsync();// GetTokenAsync();
            var oauthToken = (string)response["access_token"];
            var serviceUrl = (string)response["instance_url"];

            var result = await CallApi(oauthToken, serviceUrl);

            return Content(result);
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

        private async Task<string> CallApi(string token, string serviceUrl)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var result = await client.GetStringAsync(serviceUrl + "/services/data/v37.0/sobjects/case/");

            return result;
        }

        private async Task<JObject> GetTAsync()
        {
            HttpClient authClient = new HttpClient();

            //set OAuth key and secret variables
            string sfdcConsumerKey = "3MVG9HxRZv05HarTSJgKRE.cSe3KbPrAjSP7LJjklbr9XlU7M3aYghoaoI9njk3IVHzqf8gYxwy7_y63AlK8I";
            string sfdcConsumerSecret = "8402646785698026683";

            //set to Force.com user account that has API access enabled
            string sfdcUserName = "mohsink13@gmail.com";
            string sfdcPassword = "Salesf0rce";
            string sfdcToken = "K08H3JHxrYXz33N9T73cX7ld";

            //create login password value
            string loginPassword = sfdcPassword + sfdcToken;

            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
              {
                 {"grant_type","password"},
                 {"client_id",sfdcConsumerKey},
                 {"client_secret",sfdcConsumerSecret},
                 {"username",sfdcUserName},
                 {"password",loginPassword}
               }
                        );

            HttpResponseMessage message = await authClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);

            string responseString = await message.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(responseString);
            return obj;
            //var oauthToken = (string)obj["access_token"];
            //var serviceUrl = (string)obj["instance_url"];

            //return $"AUTH_TOKEN={oauthToken} AND SERVICE URL = {serviceUrl}";

        }
    }

    
}