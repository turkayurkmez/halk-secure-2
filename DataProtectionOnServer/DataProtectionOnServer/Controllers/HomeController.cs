using DataProtectionOnServer.Models;
using DataProtectionOnServer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataProtectionOnServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /*
         * Obfuscator: Bir .NET uygulamasının kod dosyalarını çözülemez hale getiren teknik/uygulama
         * DataProtection: Sunucuda release edilmiş bir uygulamanın ihtiyaç duyduğu verileri koruma sistemi
         */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DataProtector dataProtector = new DataProtector("sifreli.txt");
            var outputLength = dataProtector.EncryptedData("Bu şifre içeren bir cümle");
            string value = dataProtector.DecryptData(outputLength);

            ViewBag.Value = value;
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