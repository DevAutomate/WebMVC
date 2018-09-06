using Newtonsoft.Json;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebOnline.Models;

namespace WebOnline.Controllers
{
    public class SPUserController : Controller
    {

        string Baseurl = "http://localhost:8000";
        public int GetCheck(string username, string password)
        {
            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");//
                response = client.DownloadString("http://localhost:8000/api/SPUser/geta?UserName=" + username + "&Password=" + password + "");
                response = response.TrimStart('\"');
                response = response.TrimEnd('\"');
                response = response.Replace("\\", "");
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //var emdetails = serializer.Deserialize<List<Employee>>(response);
                //EmpInfo = emdetails as List<Employee>;

            }
            return int.Parse(response);
        }

       
        public async Task<ActionResult> GetUser()
        {
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("~/api/SPUser/getb?UserName=NAVY&Password=7449");
                if (response.IsSuccessStatusCode)
                {
                    var emdetails = response.Content.ReadAsStringAsync().Result;
                    emdetails = emdetails.TrimStart('\"');
                    emdetails = emdetails.TrimEnd('\"');
                    emdetails = emdetails.Replace("\\", "");
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(emdetails);


                }
                return View(EmpInfo);
            }
        }
        /*
        public ActionResult Post()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("EmID", "150"),
                new KeyValuePair<string, string>("EmFname", "Test01"),
                new KeyValuePair<string, string>("EmLname", "L02"),
                new KeyValuePair<string, string>("UserName", "ttt"),
                new KeyValuePair<string, string>("Password", "ppp"),
                new KeyValuePair<string, string>("admin", "0"),
                new KeyValuePair<string, string>("addJob", "0"),
                new KeyValuePair<string, string>("EditJob", "0"),
                new KeyValuePair<string, string>("Department", "TE"),
                new KeyValuePair<string, string>("SaleName", "STE"),
                new KeyValuePair<string, string>("CHK", ""),
                new KeyValuePair<string, string>("DepID", "AD"),
                new KeyValuePair<string, string>("stat", "1"),
                new KeyValuePair<string, string>("Email", "test@gmail.com"),
                });
                var response = client.PostAsync("api/SPUser", content).Result;

                //string stringData = JsonConvert.SerializeObject(e);
                //var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                //var response = client.PostAsync("api/SPUser", contentData).Result;
                //ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View();
            }
        }
        */

    }
    }    


