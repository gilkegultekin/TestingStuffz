using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WebSocketTestServer.Models;

namespace WebSocketTestServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        [HttpGet("chunked")]
        public async Task ChunkedTest()
        {
            var response = HttpContext.Response;
            response.StatusCode = 200;
            response.Headers[HeaderNames.TransferEncoding] = "chunked";

            var listOfStrings = new List<string> { "Wiki", "pedia", " in", " chunks." };
            foreach (var str in listOfStrings)
            {
                await response.WriteAsync($"{str.Length}\r\n");
                await response.WriteAsync($"{str}\r\n");
                await response.Body.FlushAsync();
                await Task.Delay(15000);
            }

            await response.WriteAsync("0\r\n");
            await response.WriteAsync("\r\n");
            await response.Body.FlushAsync();
        }
    }
}
