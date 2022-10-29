using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaGamerBot
{
    public class chat
    {
        public long id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }

        public string type { get; set; }
    }

    public class SendMessageObject
    {
        public long chat_id { get; set; }
        public string text { get; set; }
        public bool disable_web_page_preview { get; set; }

        public SendMessageObject()
        {
            this.disable_web_page_preview = true;
        }
       public ReplyKeyboardMarkup reply_markup { get; set; }

    }

    public class SendMessageObjectWithoutKeyboard
    {
        public long chat_id { get; set; }
        public string text { get; set; }
        public bool disable_web_page_preview { get; set; }

        public SendMessageObjectWithoutKeyboard()
        {
            this.disable_web_page_preview = true;
        }

    }
    public class KeyboardButton
    {
        public string text { get; set; }
    }

    public class ReplyKeyboardMarkup
    {
        public KeyboardButton[][] keyboard { get; set; }
        public bool one_time_keyboard { get; set; }

        public ReplyKeyboardMarkup()
        {
            one_time_keyboard = false;
        }
    }
    public class GetChatMemberInfoObject
    {
        public long chat_id { get; set; }
        public long user_id { get; set; }
    }
    public class SendPhotoObject
    {
        public long chat_id { get; set; }
        public string caption { get; set; }

        public string photo { get; set; }
    }

    public class ChatResponse
    {
        public string Message { get; set; }
        public List<string> Commands { get; set; }
    }
}
