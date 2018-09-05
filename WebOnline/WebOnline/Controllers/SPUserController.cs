using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebOnline.Models;

namespace WebOnline.Controllers
{
    public class SPUserController : Controller
    {
        string Baseurl = "http://localhost:13567/"; 
        public async Task<ActionResult> GetUser()
        {
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/SPUser/getb?UserName=NAVY&Password=7449");
                if (response.IsSuccessStatusCode)
                {
                    var emdetails = response.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(emdetails);
                    

                }
                return View(EmpInfo);
            }
            }
        }
    }
