using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBot.App_Code.StoredStrings
{
    public class StoredValues
    {
        public static string WelcomeMessage =
            "سلام☺️\r\nبه ربات بتاگیمر خیلی خوش اومدی❤️\r\nامیدوارم از کارکردن با این ربات نهایت لذت و استفاده رو ببری🌹\r\nالان %E تا انرژی داری😍\r\nاگه دوس داری بدونی با انرژیت چه کارا می تونی انجام بدی دکمه \"با انرژی چه کارا می شه کرد\" رو لمس کن";

        public static string NameChangedMessage =
            "باشه پس از این به بعد %N صدات می کنم";

        public static string NameLessThanThree =
            "اسم که از 3 حرف کمتر نمی تونه باشه🤔\r\nلطفا اسم واقعیت رو وارد کن☹";

        

        public static string EnergyInfo = "%N\r\nالان 👑%E👑 تا انرژی داری\r\nاگر دوس داری بدونی با انرژی چیکارا می شه کرد %EH رو لمس کن\r\n هر وقت هم که دوس داشتی بدونی چقدر انرژی داری %ER رو لمس کن".Replace("%EH", StoredCommands.EnergyHelp).Replace("%ER",StoredCommands.RemainingEnergy);

        public static string EnergyRemainingInfo = "الان 👑%E👑 تا انرژی داری";
        public static string MainMenuText = "شما %E تا انرژی دارید\r\nبا انرژی کارهای زیر رو می تونی انجام بدی";
        public static string EnergyHelpText = "%E1 - کانال تلگرامتو توی روبات تبلیغ کن\r\n%E2 - کانال تلگرامتو توی کانال بتاگیمر تبلیغ کن\r\n%E3 - بازی هاتو توی روبات آگهی کن و بفروش\r\n%E4 -  بازی هاتو توی کانال بتاگیمر آگهی کن و بفروش\r\n";
        public static string RobotID = "VCSXBot";
        public static string RoboAdvURL = "http://telegram.me/" + RobotID + "?start=%d";
        public static string StoryFinished = "داستان شما به پایان رسیده است.";
        public static string EnergyLowHelp = "انرژیت تموم شده☹️\r\nولی نگران نباش😍😍😍 \r\nاین عکسی که برات فرستادم رو برای هر کی می شناسی و هر گروهی که توش هستی بفرستی هم می فهمی دوستات می تونن به اینجا که رسیدی برسن یا نه هم انرژی می گیری\r\n\r\nراستی وقتی انرژیت زیاد باشه من بهت هدیه هم می دم که ارزش مادی زیادی هم داره";
        public static string RefLinkText = " چقدر تصمیم هایی که می گیری درسته؟🤔\r\nتو شرایط سخت چقدر می تونی خوب تصمیم بگیری؟😥\r\nدوس داری جواب این سوالا رو با یه بازی باحال بگیری؟😍 \r\n اگر لینک پایین رو لمس کنی هم می تونی بازی کنی و هم می تونی جایزه های نقدی بگیری\r\n👉 https://telegram.me/StoryBot?start=%u 👈";
        public static string InviteToChannelText = "لطفا برای ادامه استفاده از این روبات در کانال بتاگیمر هم عضو شوید☺️\r\n\r\n لینک کانال : https://t.me/joinchat/AAAAAEzyuaBpY6MRNbzcmg \r\n\r\n بعد از اینکه در کانال عضو شدید دکمه زیر رو لمس کنید";
    }

    public class EnergyValues
    {
        public static int MinimumEnergy = 150000;
        public static int ViewEnergyConsumptionRate = 5;
        public static int StoryPathEnergyConsumptionRate = 0;

        public static int UserRefEnergy = 50;

        
    }

    public class GiftValues
    {
        public static int Gift1Energy = 800;
        public static int Gift2Energy = 1500;
        public static int Gift3Energy = 6000;
        public static int Gift4Energy = 12000;

        public static string Gift1Command = "🎁کارت شارژ 5000 تومانی🎁";
        public static string Gift2Command = "🎁🎁کارت شارژ 10000 تومانی🎁🎁";
        public static string Gift3Command = "🔮اکانت بازی Ps4 به ارزش 50000 تومان🔮";
        public static string Gift4Command = "🔮🔮اکانت بازی Ps4 به ارزش 120000 تومان🔮🔮";
    }
    public class StoredCommands
    {
        public  const string NameOkey
            = "/okeye";
        public  const string EnergyHelp
            = "/energyHelp";
        public  const string RemainingEnergy
            = "/energy";
        public const string StartCommand
            = "/start";
        public const string GetAdvURL
            = "/moarefi";
        public const string WhatToDoWithEnergy
            = "با انرژی چیکارا می شه کرد؟🤔";

        public const string GameList
            = "🔮می تونی بازی کنی🔮";
        public const string GiftList
            = "🎁می تونی هدیه بگیری🎁";
        public const string StoryStart
            = "😊شروع کن😊";
        public const string Story1Command
            = "😲بازی سقوط هواپیما😲";
        public const string Story2Command
            = "😈بازی سقوط هواپیما - قسمت دوم😈";
        public const string StoryReplay
            = "😊شروع دوباره😊";
        public const string MainMenu
            = "بازگشت به منو اصلی";
        public const string HowToGetMoreEnergy
            = "چجوری می تونم انرژی بیشتری بگیرم؟";
        public const string IntroduceToFriends
            = "به دوستات منو معرفی کن";
    }

    public class StoredDirectories
    {
        public const string Story1Directory = "Story1";
        public const string Story2Directory = "Story2";
    }
}
