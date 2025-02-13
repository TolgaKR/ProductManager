using MaterMan.Data;
using MaterMan.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MaterMan.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public ChatController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        // Tüm kullanıcıları listele
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // Belirli bir kullanıcıyla mesajlaşma
        public async Task<IActionResult> ChatWith(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Kullanıcılar arasındaki mesajları getir
            var messages = await _appDbContext.Messages
                .Where(m => (m.SenderId == currentUser.Id && m.ReceiverId == userId) ||
                            (m.SenderId == userId && m.ReceiverId == currentUser.Id))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            ViewBag.ReceiverId = user.Id;
            ViewBag.ReceiverName = user.UserName;

            return View(messages);
        }
    }
}
