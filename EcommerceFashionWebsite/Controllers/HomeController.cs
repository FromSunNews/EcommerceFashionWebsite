using EcommerceFashionWebsite.Models;
using EcommerceFashionWebsite.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceFashionWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LanguageService _lang;

        public HomeController(ILogger<HomeController> logger, LanguageService lang)
        {
            _lang = lang;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.WelcomeMessage = _lang.Getkey("str_welcome_message");
            //get culture information
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            return View();
        }

        #region Localization

  
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion

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