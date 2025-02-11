using Microsoft.AspNetCore.Mvc;

namespace MaterMan.Controllers
{
    public class CurrencyController : Controller
    {

        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService= currencyService;
        }


        public async Task<IActionResult> Index()
        {
            var currencies = await _currencyService.GetCurrenciesAsync();
            return View(currencies);
        }
    }
}
