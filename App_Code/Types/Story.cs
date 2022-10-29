using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaGamerBot.App_Code.Types
{
    public class Story
    {
        public string MainText { get; set; }

        public string[] Choices { get; set; }

        public string[] CFiles { get; set; }
    }
}
