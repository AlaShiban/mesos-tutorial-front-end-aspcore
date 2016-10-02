using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.IO;

namespace FrontEndCoreService.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private AppConfig _appConfig;
         

        public HomeController(IHostingEnvironment environment, IOptions<AppConfig> appConfig)
        {
            _environment = environment;
            _appConfig = appConfig.Value;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            
            var client = new System.Net.Http.HttpClient();
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {

                if (file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.Add("X-FileName", file.FileName);
                    fileContent.Headers.Add("X-ContentType", file.ContentType);
                    Console.WriteLine(_appConfig.ImageProcessorUrl);
                    var response = await client.PostAsync(_appConfig.ImageProcessorUrl, fileContent);
                    
                    //var imgData = Newtonsoft.Json.JsonConvert.DeserializeObject();
                    var byteArray = await response.Content.ReadAsByteArrayAsync();  
                    var guid = Guid.NewGuid();          
                    var saveFilePath = _environment.ContentRootPath + "/wwwroot/upimages/" + guid + ".jpg";
                    System.IO.File.WriteAllBytes(saveFilePath, byteArray);
                    ViewData["filename"] = "upimages/" + guid + ".jpg";
                    
                    

                }
            }
            return View("Viewer");
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
