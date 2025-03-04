using ABCBank.Models;
using ABCBank.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExchangeRateService _exchangeRateService;
        public HomeController(ILogger<HomeController> logger, ExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        public IActionResult Index()
        {
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

        [HttpGet]
        public async Task<IActionResult> ExchangeRate()
        {
            string fromDate = "2024-06-12";
            string toDate = "2024-06-12";

            var exchangeRateResponse = await _exchangeRateService.GetExchangeRateAsync(fromDate, toDate);

            return View(exchangeRateResponse);
        }
    }
}
