using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.StringProcessors;
using System.Data.SQLite;
using Newtonsoft.Json;
using BetaGamerBot.App_Code;
using BetaGamerBot.App_Code.DataBase;
using BetaGamerBot.App_Code.TelegramOperations;
using BetaGamerBot.App_Code.Types;
namespace BetaGamerBot
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
           // FormHandle = this;
        }


        //public Form1 FormHandle = new Form1();
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
           // Telegram.CreateLink("ehemmmm");
          //  Telegram.GetChatMember(-1001388737581, 64712496);
           // List<string> x = new List<string>();
           // x.Add("gozine1");
           // Telegram.SendMessage(64712496, "test");
            
        }

        private void tmrController_Tick(object sender, EventArgs e)
        {
            tmrController.Stop();
            int cID = DataBase.GetLastCheckID();
            cID++;
            UpdateData uData = Telegram.UpdateTelegram(cID);
            Processor.ProcessUpdateData(uData);
            tmrController.Start();
        }

        
    }
}
