using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using StoryBot;
namespace StoryBot.App_Code.TelegramOperations
{
    public class Telegram
    {
        
       private static string BotToken = "";
        private static string TelegramAddress = "https://api.telegram.org/";
        private static string BotAddress = TelegramAddress + BotToken + "/";

        private static string UpdateURL = BotAddress + "getUpdates";
        private static string SendMessageURL = BotAddress + "sendMessage";
        private static string SendPhotoURL = BotAddress + "sendPhoto";
        private static string GetChatMemberURL = BotAddress + "getChatMember";
        private static int BannerGroupID = -254630165;
        public static long ChannelID = -1001290975648;
        private static string BannerPhotoID = "";
        public static void SendMessage(long chatID, string message,List<string> cmds)
        {
            WebClient wb = new WebClient();
            wb.Encoding = System.Text.Encoding.UTF8;
            string sJson = "";
            if(cmds == null || cmds.Count == 0)
            {
                SendMessageObjectWithoutKeyboard so = new SendMessageObjectWithoutKeyboard();
                so.chat_id = chatID;
                so.text = message;
                sJson = JsonConvert.SerializeObject(so);
            }
            else
            {
                SendMessageObject so = new SendMessageObject();
                if (cmds != null)
                {
                    ReplyKeyboardMarkup rp = new ReplyKeyboardMarkup();
                    rp.keyboard = Telegram.CreateKeyboard(cmds);
                    so.reply_markup = rp;
                }

                so.chat_id = chatID;
                so.text = message;
                sJson = JsonConvert.SerializeObject(so);
            }
            try
            {
                wb.Headers["Content-Type"] = "application/json";
                wb.UploadString(SendMessageURL, sJson);
            }
            catch(Exception ex)
            {

            }
            System.Threading.Thread.Sleep(200);
        }

        public static UpdateData UpdateTelegram(int offset = 0)
        {
            WebClient wb = new WebClient();
            string url = UpdateURL + "?offset=" + offset;
            string src = "";
            UpdateData uData = new UpdateData();
            wb.Encoding = System.Text.Encoding.UTF8;
            try
            {
                 src = wb.DownloadString(url);
                uData = JsonConvert.DeserializeObject<UpdateData>(src);
            }
            catch(Exception ex)
            {
                System.IO.File.AppendAllText("error.txt", ex.Message + "\r\n");
            }
            
            
            return uData;
        }

        public static bool IsMemberOfChannel(long chatID,long memberID)
        {
            bool ret = true;
            GetChatMemberInfoObject gf = new GetChatMemberInfoObject();
            WebClient wb = new WebClient();
            string sJson = "";
            gf.chat_id = chatID;
            gf.user_id = memberID;

            wb.Headers["Content-Type"] = "application/json";
            wb.Encoding = System.Text.Encoding.UTF8;
            sJson = JsonConvert.SerializeObject(gf);
            try
            {
                string s = wb.UploadString(GetChatMemberURL, sJson);
                if (s.Contains("\"status\":\"left\""))
                {
                    ret = false;
                }
            }
            catch(Exception ex)
            {
                ret = true;
            }
            return ret;
        }
        public static void SendPhoto(long chatID, string message,string photoID)
        {
            WebClient wb = new WebClient();
            wb.Encoding = System.Text.Encoding.UTF8;
            string sJson = "";
            SendPhotoObject so = new SendPhotoObject();
            so.chat_id = chatID;
            so.caption = message;
            so.photo = photoID;
            sJson = JsonConvert.SerializeObject(so);
            wb.Headers["Content-Type"] = "application/json";
            try
            {
                string s = wb.UploadString(SendPhotoURL, sJson);
            }
            catch(Exception ex)
            {

            }
        }

        public static void CreateLink(string msg,long chatID)
        {
            SendPhoto(chatID, msg,BannerPhotoID);
        }

        public static KeyboardButton[][] CreateKeyboard(List<string> btnList)
        {
            List<KeyboardButton[]> ret = new List<KeyboardButton[]>();
            foreach(string s in btnList)
            {
                List<KeyboardButton> btn = new List<KeyboardButton>();
                KeyboardButton btnNew = new KeyboardButton();
                btnNew.text = s;
                btn.Add(btnNew);
                ret.Add(btn.ToArray());
            }

            return ret.ToArray();
        }
        
    }
}
