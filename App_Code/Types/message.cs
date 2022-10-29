using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaGamerBot
{
    public class message
    {
        public int message_id { get; set; }
        public from from { get; set; }
        public chat chat { get; set; }
        public string text { get; set; }
    }
}
