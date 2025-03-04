using ABCBank.Areas.Identity.Data;
using ABCBank.Models;
using ABCBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCBank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ABCBankContext _context;
        private readonly ExchangeRateService _exchangeRateService;

        public TransactionController(ABCBankContext context, ExchangeRateService exchangeRateService)
        {
            _context = context;
            _exchangeRateService = exchangeRateService;
        }
        //Main View
        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        //Transfer 
        [HttpPost]
        public async Task<IActionResult> Transfer(TransactionModal model)
        {
            if (ModelState.IsValid)
            {
                string fromDate = DateTime.Now.ToString("yyyy-MM-dd");
                string toDate = fromDate;

                var exchangeRateResponse = await _exchangeRateService.GetExchangeRateAsync(fromDate, toDate);

                var myrToNprRate = exchangeRateResponse.Data.Payload[0].Rates.Find(r => r.Currency.Iso3 == "MYR");

                if (myrToNprRate != null)
                {
                    model.ExchangeRate = myrToNprRate.Sell ?? 0m;  
                    model.PayoutAmountNPR = model.TransferAmountMYR * model.ExchangeRate;
                    model.TransferDate = DateTime.Now;

                    _context.Transactions.Add(model);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("TransferConfirmation");
                }

                ModelState.AddModelError(string.Empty, "Unable to retrieve exchange rate for MYR.");
            }

            return View(model); 
        }

        //Report
        [HttpGet]
        public IActionResult Report()
        {
            var model = new TransactionReportViewModel
            {
                FromDate = DateTime.Now.AddMonths(-1), 
                ToDate = DateTime.Now
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Report(TransactionReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var query = _context.Transactions.AsQueryable();
                if (model.FromDate.HasValue)
                {
                    query = query.Where(t => t.TransferDate >= model.FromDate.Value);
                }
                if (model.ToDate.HasValue)
                {
                    query = query.Where(t => t.TransferDate <= model.ToDate.Value);
                }
                model.Transactions = query.ToList();
            }

            return View(model);
        }

        //Confirmation
        [HttpGet]
        public IActionResult TransferConfirmation()
        {
            return View();
        }
    }
}
