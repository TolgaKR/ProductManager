using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Entity.Concrete
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual AppUser Receiver { get; set; }
    }
}
