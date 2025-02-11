using MaterMan.Business.Abstract;
using MaterMan.Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace MaterMan.Controllers
{
    public class AdminController : Controller
    {

        private IReceteBaslikService _receteBaslikService;
        private IReceteKalemService _receteKalemService;


        public AdminController(IReceteBaslikService receteBaslikService,IReceteKalemService receteKalemService)
        {
            _receteBaslikService = receteBaslikService;
            _receteKalemService = receteKalemService;
        }


        public async Task<IActionResult> OnayDurumu(int id)
        {
            var result = await _receteKalemService.GetReceteDetayAsync(id);
            return View(result);
        }
        public async Task<IActionResult> AcceptRecete(int id)
        {
            var result=await _receteBaslikService.AcceptReceteBaslikAsync(id);
            
            return RedirectToAction("OnayDurumu", "Admin", new { id = id });

        }



        public IActionResult Index()
        {
            return RedirectToAction("ReceteList", "Recete");
        }
    }
}
