using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetaGamerBot.App_Code.DataBase;
using BetaGamerBot.App_Code.TelegramOperations;
using BetaGamerBot.App_Code.StoredStrings;
using BetaGamerBot.App_Code.Types;
using BetaGamerBot.App_Code.Procedures;
using System.IO;
using System.Text.StringProcessors;
using Newtonsoft.Json;
namespace BetaGamerBot.App_Code
{
    class Processor
    {
        public static void ProcessUpdateData(UpdateData uData)
        {
            if (uData.result != null)
            {
                foreach (result currentResult in uData.result)
                {

                    if (currentResult != null && currentResult.message != null && currentResult.message.text != "")
                    {
                        //bool isMember = true;
                        bool isMember = Telegram.IsMemberOfChannel(Telegram.ChannelID, currentResult.message.from.id);
                        DataBase.DataBase.LogMessage(currentResult.message.from.id.ToString(), currentResult.message.text);
                        ChatResponse sMsg = new ChatResponse();
                        if (!DataBase.DataBase.IsUserNew(currentResult.message.from.id.ToString()))
                        {
                            DataBase.DataBase.IncreaseMsgCount(currentResult.message.from.id.ToString());
                            int msgCount = DataBase.DataBase.GetMsgCount(currentResult.message.from.id.ToString());
                            if (msgCount >= 0 && !isMember)
                            {
                                sMsg = InviteToChannel(currentResult);

                            }
                            else
                            {
                                string txtmsg = currentResult.message.text;
                                if (txtmsg == StoredCommands.WhatToDoWithEnergy)
                                {
                                    sMsg = ProcessWhatToDoWithEnergy();
                                }
                                else if (txtmsg == StoredCommands.GameList)
                                {
                                    sMsg = ProcessGameList();
                                }
                                else if (txtmsg == StoredCommands.IntroduceToFriends)
                                {
                                    sMsg = ProcessIntroduce(currentResult);
                                }
                                else if (txtmsg == StoredCommands.GiftList)
                                {
                                    sMsg = ProcessGiftList();
                                }
                                else if (txtmsg == GiftValues.Gift1Command)
                                {
                                    sMsg = ProcessGetGift1(currentResult);
                                }
                                else if (txtmsg == GiftValues.Gift2Command)
                                {
                                    sMsg = ProcessGetGift2(currentResult);
                                }

                                else if (txtmsg == GiftValues.Gift3Command)
                                {
                                    sMsg = ProcessGetGift3(currentResult);
                                }
                                else if (txtmsg == GiftValues.Gift4Command)
                                {
                                    sMsg = ProcessGetGift4(currentResult);
                                }
                                else if (txtmsg == StoredCommands.MainMenu)
                                {
                                    sMsg = ProcessMainMenu(currentResult);
                                }
                                else if (txtmsg == StoredCommands.Story1Command)
                                {
                                    sMsg = ProcessStory1Cmd(currentResult);
                                }
                                else if (txtmsg == StoredCommands.Story2Command)
                                {
                                    sMsg = ProcessStory2Cmd(currentResult);
                                }
                                else if (txtmsg == StoredCommands.HowToGetMoreEnergy)
                                {
                                    sMsg = HowToGetMoreEnergyProcess(currentResult);
                                }
                                else
                                {
                                    Procedure proc = Procedures.Procedures.GetProcedure(currentResult.message.from.id.ToString());
                                    sMsg = ProcessProcedure(currentResult, proc);
                                }
                            }
                        }
                        else
                        {


                            sMsg = ProcessNewUser(currentResult);

                        }

                        if (sMsg.Message != "")
                        {
                            if (sMsg.Commands != null)
                            {
                                sMsg.Commands.Add(StoredCommands.IntroduceToFriends);
                            }
                            Telegram.SendMessage(currentResult.message.chat.id, sMsg.Message, sMsg.Commands);
                          
                        }


                    }
                    DataBase.DataBase.UpdateLastCheckID(currentResult.update_id);
                }
            }
        }

        private static ChatResponse ProcessGiftList()
        {
            ChatResponse ret = new ChatResponse();
            ret.Message = "  شما می تونید با استفاده از انرژی خودتون هدیه های زیر یا معادل نقدیشون رو دریافت کنید";
            ret.Commands = new List<string>();
            ret.Commands.Add(GiftValues.Gift1Command);
            ret.Commands.Add(GiftValues.Gift2Command);
            ret.Commands.Add(GiftValues.Gift3Command);
            ret.Commands.Add(GiftValues.Gift4Command);
            ret.Commands.Add(StoredCommands.MainMenu);

            return ret;
        }

        private static ChatResponse ProcessIntroduce(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            Telegram.CreateLink(StoredValues.RefLinkText.Replace("%u",cResult.message.from.id.ToString()), cResult.message.chat.id);
            ret.Message = "پیام بالا رو برا دوستات فوروارد کن\r\nهم باعث می شی اونا هم بازی لذت ببرن هم انرژی می گیری";
            return ret;
        }
        private static ChatResponse ProcessGetGift1(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            if (energy < GiftValues.Gift1Energy)
            {
                ret.Message = "شما به " + GiftValues.Gift1Energy + " انرژی برای دریافت این هدیه نیاز دارید";
                ret.Commands = new List<string>();
                ret.Commands.Add(StoredCommands.HowToGetMoreEnergy);
                ret.Commands.Add(StoredCommands.MainMenu);
            }
            else
            {
                DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), GiftValues.Gift1Energy);
                ret.Message = "تبریک می گم! لطفا با آیدی روبرو برای دریافت جایزه تماس بگیرید @m0hammad_b";
                File.AppendAllText("gift1.txt", JsonConvert.SerializeObject(cResult) + "\r\n");

            }
            return ret;
        }
        private static ChatResponse ProcessGetGift2(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            if (energy < GiftValues.Gift2Energy)
            {
                ret.Message = "شما به " + GiftValues.Gift2Energy + " انرژی برای دریافت این هدیه نیاز دارید";
                ret.Commands = new List<string>();
                ret.Commands.Add(StoredCommands.HowToGetMoreEnergy);
                ret.Commands.Add(StoredCommands.MainMenu);
            }
            else
            {
                DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), GiftValues.Gift2Energy);
                ret.Message = "تبریک! به زودی با شما تماس گرفته خواهد شد";
                File.AppendAllText("gift2.txt", JsonConvert.SerializeObject(cResult) + "\r\n");
            }
            return ret;
        }

        private static ChatResponse ProcessGetGift3(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            if (energy < GiftValues.Gift3Energy)
            {
                ret.Message = "شما به " + GiftValues.Gift3Energy + " انرژی برای دریافت این هدیه نیاز دارید";
                ret.Commands = new List<string>();
                ret.Commands.Add(StoredCommands.HowToGetMoreEnergy);
                ret.Commands.Add(StoredCommands.MainMenu);
            }
            else
            {
                DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), GiftValues.Gift3Energy);
                ret.Message = "تبریک! به زودی با شما تماس گرفته خواهد شد";
                File.AppendAllText("gift3.txt", JsonConvert.SerializeObject(cResult) + "\r\n");
            }
            return ret;
        }

        private static ChatResponse ProcessGetGift4(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            if (energy < GiftValues.Gift4Energy)
            {
                ret.Message = "شما به " + GiftValues.Gift4Energy + " انرژی برای دریافت این هدیه نیاز دارید";
                ret.Commands = new List<string>();
                ret.Commands.Add(StoredCommands.HowToGetMoreEnergy);
                ret.Commands.Add(StoredCommands.MainMenu);
            }
            else
            {
                DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), GiftValues.Gift4Energy);
                ret.Message = "تبریک! به زودی با شما تماس گرفته خواهد شد";
                File.AppendAllText("gift4.txt", JsonConvert.SerializeObject(cResult) + "\r\n");
            }
            return ret;
        }
        private static ChatResponse InviteToChannel(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            ret.Commands = new List<string>();
            ret.Commands.Add(cResult.message.text);
            ret.Message = StoredValues.InviteToChannelText;
            return ret;
        }
        private static ChatResponse ProcessGameList()
        {
            ChatResponse ret = new ChatResponse();
            ret.Message = "بازی زیر رو می تونی انجام بدی";
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.Story1Command);
            ret.Commands.Add(StoredCommands.Story2Command);
            return ret;
        }

        private static ChatResponse ProcessMainMenu(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            string msg = StoredValues.MainMenuText;
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            DataBase.DataBase.SetProc(cResult.message.from.id.ToString(), ProcedureValues.IdleProcedure);
            msg = msg.Replace("%E", energy.ToString());
            ret.Message = msg;
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.GameList);
            ret.Commands.Add(StoredCommands.GiftList);
            return ret;
        }
        private static ChatResponse ProcessStory1Cmd(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            DataBase.DataBase.SetProc(cResult.message.from.id.ToString(), ProcedureValues.Story1Procedure);
            ret.Message = "برای شروع بازی گزینه زیر رو لمس کن";
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.StoryStart);
            return ret;
        }

        private static ChatResponse ProcessStory2Cmd(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            DataBase.DataBase.SetProc(cResult.message.from.id.ToString(), ProcedureValues.Story2Procedure);
            ret.Message = "برای شروع بازی گزینه زیر رو لمس کن";
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.StoryStart);
            return ret;
        }
        private static ChatResponse ProcessWhatToDoWithEnergy()
        {
            ChatResponse ret = new ChatResponse();
            ret.Message = "با انرژی کارهای زیر رو می تونی انجام بدی";
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.GameList);
            ret.Commands.Add(StoredCommands.GiftList);
            return ret;
        }
        private static ChatResponse ProcessNewUser(result cResult)
        {
            ChatResponse ret = new ChatResponse();
             string sMsg="";
            try
            {
              sMsg  = cResult.message.text;

            }
            catch(Exception ex)
            {
                sMsg = "";
            }
            if(sMsg == null)
            {
                sMsg = "";
            }
            if (sMsg.StartsWith("/start "))
            {
                sMsg = sMsg.Replace("/start ", "");
                bool uExist = DataBase.DataBase.IsUserNew(sMsg);
                if (!uExist)
                {
                    DataBase.DataBase.AddEnergy(sMsg, EnergyValues.UserRefEnergy);
                }
            }
            DataBase.DataBase.CreateNewUserRecord(cResult.message.from.id.ToString(), cResult.message.from.first_name);
            string msg = StoredValues.WelcomeMessage;
            int energy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
            msg = msg.Replace("%E", energy.ToString());
            ret.Message = msg;
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.WhatToDoWithEnergy);
            //DataBase.DataBase.CreateNewUserRecord()
            return ret;
        }
        public static ChatResponse ProcessProcedure(result cResult, Procedure proc)
        {
            ChatResponse ret = new ChatResponse();

            if (proc == Procedure.Story1)
            {
                ret = Story1Process(cResult);
            }
            else if(proc == Procedure.Story2)
            {
                ret = Story2Process(cResult);
            }
            else
            {
                ret = ProcessMainMenu(cResult);
            }
            return ret;
        }

        public static ChatResponse Story1Process(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            try
            {
                
                int uEnergy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
                if (uEnergy >= EnergyValues.StoryPathEnergyConsumptionRate)
                {
                    string tMsg = cResult.message.text;
                    Story st;
                    if (tMsg == StoredCommands.StoryStart || tMsg == StoredCommands.StoryReplay)
                    {
                        st = GetStory(StoredDirectories.Story1Directory, "0");
                        DataBase.DataBase.SetLastStoryFile(cResult.message.from.id.ToString(), "0");

                    }
                    else
                    {
                        st = GetStory(StoredDirectories.Story1Directory, tMsg.FindBetween("(", ")"));

                    }

                    if (st.MainText != null && st.MainText != "" && st.Choices != null)
                    {
                        DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), EnergyValues.StoryPathEnergyConsumptionRate);
                        DataBase.DataBase.SetLastStoryFile(cResult.message.from.id.ToString(), tMsg.FindBetween("(", ")"));
                        List<string> ch = new List<string>();
                        if (st.Choices.Length > st.CFiles.Length)
                        {
                            List<string> tmp = st.CFiles.ToList();
                            for (int i = 0; i < st.Choices.Length - st.CFiles.Length; i++)
                            {
                                tmp.Add(st.CFiles[0]);

                            }
                            st.CFiles = tmp.ToArray();
                        }
                        for (int i = 0; i < st.Choices.Length; i++)
                        {
                            string currentChoice = st.Choices[i];
                            string currentFile = st.CFiles[i];
                            currentFile = currentFile.FindEverythingPriorTo(".");
                            string currentCmd = "🔮(" + currentFile + ") - " + currentChoice + "🔮";
                            ch.Add(currentCmd);
                        }


                        //  ch.Add("شروع دوباره");
                        if (ch.Count == 0)
                        {
                            ch.Add(StoredCommands.StoryReplay);
                            ch.Add(StoredCommands.MainMenu);
                        }
                        ch.Add(StoredCommands.MainMenu);
                        ret.Commands = ch;
                        ret.Message = st.MainText;
                    }
                    else if (st.Choices == null || st.Choices.Length == 0)
                    {
                        ret.Message = "این پیام رو نمی شناسم لطفا از گزینه هایی که برات فرستادم یکی رو انتخاب کن";
                        List<string> ch = new List<string>();
                        string lStory = DataBase.DataBase.GetLastStoryFile(cResult.message.from.id.ToString());
                        if (lStory != "")
                        {
                            st = GetStory(StoredDirectories.Story1Directory, lStory);
                            if (st.Choices.Length > st.CFiles.Length)
                            {
                                List<string> tmp = st.CFiles.ToList();
                                for (int i = 0; i < st.Choices.Length - st.CFiles.Length; i++)
                                {
                                    tmp.Add(st.CFiles[0]);

                                }
                                st.CFiles = tmp.ToArray();
                            }

                            for (int i = 0; i < st.Choices.Length; i++)
                            {
                                string currentChoice = st.Choices[i];
                                string currentFile = st.CFiles[i];
                                currentFile = currentFile.FindEverythingPriorTo(".");
                                string currentCmd = "🔮(" + currentFile + ") - " + currentChoice + "🔮";
                                ch.Add(currentCmd);
                            }
                        }
                        else
                        {

                        }
                        ch.Add(StoredCommands.MainMenu);
                        ret.Commands = ch;
                        // DataBase.DataBase.SetProc(cResult.message.from.id.ToString(), ProcedureValues.IdleProcedure);
                    }
                }
                else
                {
                    ret = LowEnergyHelp(cResult);
                }
            }
            catch(Exception ex)
            {
                ret.Message = "خطایی در پردازش پیام شما به وجود آمده است";
            }
            return ret;
        }

        public static ChatResponse Story2Process(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            try
            {

                int uEnergy = DataBase.DataBase.GetEnergy(cResult.message.from.id.ToString());
                if (uEnergy >= EnergyValues.StoryPathEnergyConsumptionRate)
                {
                    string tMsg = cResult.message.text;
                    Story st;
                    if (tMsg == StoredCommands.StoryStart || tMsg == StoredCommands.StoryReplay)
                    {
                        st = GetStory(StoredDirectories.Story2Directory, "0");
                        DataBase.DataBase.SetLastStoryFile(cResult.message.from.id.ToString(), "0");

                    }
                    else
                    {
                        st = GetStory(StoredDirectories.Story2Directory, tMsg.FindBetween("(", ")"));

                    }

                    if (st.MainText != null && st.MainText != "" && st.Choices != null)
                    {
                        DataBase.DataBase.DecreaseEnergy(cResult.message.from.id.ToString(), EnergyValues.StoryPathEnergyConsumptionRate);
                        DataBase.DataBase.SetLastStoryFile(cResult.message.from.id.ToString(), tMsg.FindBetween("(", ")"));
                        List<string> ch = new List<string>();
                        if (st.Choices.Length > st.CFiles.Length)
                        {
                            List<string> tmp = st.CFiles.ToList();
                            for (int i = 0; i < st.Choices.Length - st.CFiles.Length; i++)
                            {
                                tmp.Add(st.CFiles[0]);

                            }
                            st.CFiles = tmp.ToArray();
                        }
                        for (int i = 0; i < st.Choices.Length; i++)
                        {
                            string currentChoice = st.Choices[i];
                            string currentFile = st.CFiles[i];
                            currentFile = currentFile.FindEverythingPriorTo(".");
                            string currentCmd = "🔮(" + currentFile + ") - " + currentChoice + "🔮";
                            ch.Add(currentCmd);
                        }


                        //  ch.Add("شروع دوباره");
                        if (ch.Count == 0)
                        {
                            ch.Add(StoredCommands.StoryReplay);
                            ch.Add(StoredCommands.MainMenu);
                        }
                        ch.Add(StoredCommands.MainMenu);
                        ret.Commands = ch;
                        ret.Message = st.MainText;
                    }
                    else if (st.Choices == null || st.Choices.Length == 0)
                    {
                        ret.Message = "این پیام رو نمی شناسم لطفا از گزینه هایی که برات فرستادم یکی رو انتخاب کن";
                        List<string> ch = new List<string>();
                        string lStory = DataBase.DataBase.GetLastStoryFile(cResult.message.from.id.ToString());
                        if (lStory != "")
                        {
                            st = GetStory(StoredDirectories.Story1Directory, lStory);
                            if (st.Choices.Length > st.CFiles.Length)
                            {
                                List<string> tmp = st.CFiles.ToList();
                                for (int i = 0; i < st.Choices.Length - st.CFiles.Length; i++)
                                {
                                    tmp.Add(st.CFiles[0]);

                                }
                                st.CFiles = tmp.ToArray();
                            }

                            for (int i = 0; i < st.Choices.Length; i++)
                            {
                                string currentChoice = st.Choices[i];
                                string currentFile = st.CFiles[i];
                                currentFile = currentFile.FindEverythingPriorTo(".");
                                string currentCmd = "🔮(" + currentFile + ") - " + currentChoice + "🔮";
                                ch.Add(currentCmd);
                            }
                        }
                        else
                        {

                        }
                        ch.Add(StoredCommands.MainMenu);
                        ret.Commands = ch;
                        // DataBase.DataBase.SetProc(cResult.message.from.id.ToString(), ProcedureValues.IdleProcedure);
                    }
                }
                else
                {
                    ret = LowEnergyHelp(cResult);
                }
            }
            catch (Exception ex)
            {
                ret.Message = "خطایی در پردازش پیام شما به وجود آمده است";
            }
            return ret;
        }
        public static Story GetStory(string baseDirectory, string fileNumber)
        {
            Story ret = new Story();
            string fName = baseDirectory + "\\" + fileNumber + ".txt";
            if (File.Exists(fName))
            {
                string fContent = File.ReadAllText(fName);
                string mText = fContent.FindBetween("<main>", "</main>");
                string[] choices = fContent.FindBetweenArray("<choice>", "</choice>");
                string[] cFiles = fContent.FindBetweenArray("<cfile>", "</cfile>");
                ret.MainText = mText;
                ret.Choices = choices;
                ret.CFiles = cFiles;
            }
            return ret;
        }

        public static void SendAdvUrl(long chatID, string userID)
        {
            string txtToSend = MakeAdvUrl(userID);
            // Telegram.SendMessage(chatID, txtToSend);
        }

        public static string MakeAdvUrl(string userID)
        {
            return StoredValues.RoboAdvURL.Replace("%d", userID);
        }

        public static ChatResponse LowEnergyHelp(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            string msg = StoredValues.RefLinkText;
            msg = msg.Replace("%u", cResult.message.from.id.ToString());

            Telegram.CreateLink(msg, cResult.message.chat.id);
            ret.Message = StoredValues.EnergyLowHelp;
            ret.Commands = new List<string>();
            ret.Commands.Add(cResult.message.text);
            ret.Commands.Add(StoredCommands.MainMenu);
            return ret;
        }

        public static ChatResponse HowToGetMoreEnergyProcess(result cResult)
        {
            ChatResponse ret = new ChatResponse();
            string msg = StoredValues.RefLinkText;
            msg = msg.Replace("%u", cResult.message.from.id.ToString());
            ret.Commands = new List<string>();
            ret.Commands.Add(StoredCommands.MainMenu);
            Telegram.CreateLink(msg, cResult.message.chat.id);
            ret.Message = "برای دریافت انرژی بیشتر کافیه لینک بالا رو برای تمام دوستات فوروارد کنی\r\nهرکسی روی لینک کلیک کرد به تو انرژی اضافه می شه";
            return ret;
        }
        public static bool ProcessCommand(int messageState, result currentResult)
        {
            string msgText = currentResult.message.text.ToLower();
            string userID = currentResult.message.from.id.ToString();
            bool returnValue = true;
            if (msgText.StartsWith(StoredCommands.StartCommand))
            {
                if (DataBase.DataBase.IsUserNew(userID))
                {
                    msgText = msgText.Replace(StoredCommands.StartCommand + " ", "");
                    msgText = msgText.Trim();
                    if (msgText != "")
                    {
                        string refUserID = msgText;
                        UserData ud = DataBase.DataBase.GetUserDataById(refUserID);

                    }
                    FirstTimeUserProcess(messageState, currentResult);
                }
                returnValue = false;
            }
            else if (msgText.StartsWith(StoredCommands.GetAdvURL))
            {
                SendAdvUrl(currentResult.message.chat.id, userID);
                returnValue = false;
            }

            return returnValue;
        }
        public static void ProcessMessageState(int messageState, result currentResult)
        {
            bool messageStateCheck = true;

            switch (currentResult.message.text)
            {
                case StoredCommands.RemainingEnergy:
                    {
                        SendRemainingEnergy(currentResult.message.from.id.ToString(), currentResult.message.chat.id);
                        messageStateCheck = false;
                    }
                    break;

                case StoredCommands.EnergyHelp:
                    {
                        //Telegram.SendMessage(currentResult.message.chat.id, StoredValues.EnergyHelpText);
                        messageStateCheck = false;
                    }
                    break;

            }

            if (messageStateCheck)
            {
                switch (messageState)
                {
                    case -2:
                        {
                            FirstTimeUserProcess(messageState, currentResult);

                        }
                        break;
                    case 0:
                        {
                            ChangeNameProcess(currentResult);
                        }
                        break;
                    case -1:
                        {

                        }
                        break;
                    default:

                        break;
                }
            }
        }

        public static void FirstTimeUserProcess(int messageState, result currentResult)
        {
            string userID = currentResult.message.from.id.ToString();
            string firstName = currentResult.message.from.first_name;
            long chatID = currentResult.message.chat.id;
            string fText = StoredValues.WelcomeMessage.Replace("%N", firstName);
            DataBase.DataBase.CreateNewUserRecord(userID, currentResult.message.from.first_name);
            //Telegram.SendMessage(chatID, fText);
            DataBase.DataBase.SetMessageState(userID, 0);

        }

        public static void FinishUserState(string userID)
        {
            DataBase.DataBase.SetMessageState(userID, -1);
        }
        public static void ChangeNameProcess(result currentResult)
        {
            string text = currentResult.message.text;
            bool changeNameOk = false;
            string changedName = "";
            if (text.ToLower() == StoredCommands.NameOkey)
            {
                changeNameOk = true;
                string pName = currentResult.message.from.first_name;
                string userID = currentResult.message.from.id.ToString();
                DataBase.DataBase.SetPName(userID, pName);
                string sText = StoredValues.NameChangedMessage.Replace("%N", pName);
                FinishUserState(userID);
                //  Telegram.SendMessage(currentResult.message.chat.id, sText);
                changedName = pName;
            }
            else
            {
                if (text.Length < 3)
                {
                    string sText = StoredValues.NameLessThanThree;
                    //Telegram.SendMessage(currentResult.message.chat.id, sText);
                }
                else
                {
                    changeNameOk = true;
                    string pName = text;
                    string userID = currentResult.message.from.id.ToString();
                    DataBase.DataBase.SetPName(userID, pName);
                    string sText = StoredValues.NameChangedMessage.Replace("%N", pName);
                    DataBase.DataBase.SetMessageState(userID, -1);
                    //  Telegram.SendMessage(currentResult.message.chat.id, sText);
                    changedName = pName;
                }
            }

            if (changeNameOk)
            {
                int currentEnergy = DataBase.DataBase.GetEnergy(currentResult.message.from.id.ToString());
                string sText = StoredValues.EnergyInfo;
                sText = sText.Replace("%N", changedName).Replace("%E", currentEnergy.ToString());
                //   Telegram.SendMessage(currentResult.message.chat.id, sText);
            }
        }

        private static void SendRemainingEnergy(string userID, long chatID)
        {
            int currentEnergy = DataBase.DataBase.GetEnergy(userID);
            string sText = StoredValues.EnergyRemainingInfo.Replace("%E", currentEnergy.ToString());
            // Telegram.SendMessage(chatID, sText);
        }
    }
}
