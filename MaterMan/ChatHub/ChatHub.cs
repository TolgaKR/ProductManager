using MaterMan.Data;
using MaterMan.Entity;
using MaterMan.Entity.Concrete;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace MaterMan.ChatHub
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _appDbContext;

        public ChatHub(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task SenderMessage(string receiverId, string messageContent)
        {
            // Kullanıcının kimliğini doğrulamak
            var senderId = Context.UserIdentifier;  // SignalR kullanıcısı için kimlik
            if (string.IsNullOrEmpty(senderId)) return;  // Eğer geçerli bir kullanıcı yoksa, işlem yapılmaz.

            // Alıcıya mesaj gönder
            await Clients.User(receiverId).SendAsync("ReceiveMessage", Context.User.Identity.Name, messageContent);

            // Veritabanına mesajı kaydet
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = messageContent,
                Timestamp = DateTime.UtcNow,
            };

            // Mesajı veritabanına ekle
            _appDbContext.Messages.Add(message);
            await _appDbContext.SaveChangesAsync();

            // Gönderen ve alıcıya mesajın kaydını gönder
            await Clients.User(receiverId).SendAsync("ReceiveMessage", Context.User.Identity.Name, messageContent);
        }
    }
}
