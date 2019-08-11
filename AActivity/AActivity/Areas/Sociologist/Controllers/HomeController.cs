using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AActivity.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace AActivity.Controllers
{
    [Area("Sociologist")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //string baseUrl = "http://localhost:5000/api/emps";

            //using (HttpClient client = new HttpClient())
            //{
            //    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            //    {
            //        //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
            //        using (HttpContent content = res.Content)
            //        {
            //            var data = await content.ReadAsStringAsync();
            //            if (data != null)
            //            {

                          
            //                return Ok(data);
            //            }
            //            else
            //            {
            //                return BadRequest("not data");
            //            }
            //        }
            //    }

            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
