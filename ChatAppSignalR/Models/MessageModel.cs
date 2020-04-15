using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppSignalR.Models
{
    public class MessageModel
    {
        public string Name { get; set; }
        public string text { get; set; }
        public DateTimeOffset sendAt { get; set; }
    }
}
