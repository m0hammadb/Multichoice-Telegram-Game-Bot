using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoryBot.App_Code.Types;
namespace StoryBot.App_Code.DataBase
{
    class DataBase
    {
        private static string DataBaseName = "StoryBot.sqlite";
        private static string DataBaseConnectionString = "Data Source=" + DataBaseName + ";Version=3;";
        public static void CreateDataBase()
        {
            if (!File.Exists(DataBaseName))
            {
                SQLiteConnection.CreateFile(DataBaseName);
            }
        }

        private static void ExecuteVoid(string sql)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(DataBaseConnectionString);
            m_dbConnection.Open();
            SQLiteCommand sqlCmd = new SQLiteCommand(sql, m_dbConnection);
            sqlCmd.ExecuteNonQuery();
        }

        private static SQLiteDataReader ExecuteReader(string sql)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(DataBaseConnectionString);
            m_dbConnection.Open();
            SQLiteCommand sqlCmd = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader ret = sqlCmd.ExecuteReader();
            return ret;
        }
        public static void UpdateLastCheckID(int checkID)
        {
            string sql = "UPDATE `BotOP` SET `LastUpdateID`=" + checkID;
            ExecuteVoid(sql);
        }

        public static int GetLastCheckID()
        {
            string sql = "SELECT * FROM `BotOP`";
            SQLiteDataReader rd = ExecuteReader(sql);
            int ret = 0;

            while(rd.Read())
            {
                ret = Convert.ToInt32(rd["LastUpdateID"]);
            }
            return ret;
        }

        public static int GetMessageState(string userID)
        {
            string sql = "SELECT MState FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            int ret = -2;

            while (rd.Read())
            {
                ret = Convert.ToInt32(rd["MState"]);
            }
            return ret;
        }

        public static void LogMessage(string userID,string msg)
        {
            try
            {
                string sql = "INSERT INTO `MessagesLog`(UserID,Message) VALUES('%u','%m');".Replace("%u", userID).Replace("%m", msg);
                ExecuteVoid(sql);
            }
            catch (Exception ex)
            {

            }
        }
        public static void SetMessageState(string userID,int mState)
        {
            string sql = "UPDATE `Users` SET `MState`=" + mState + " WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }

        public static void SetLastStoryFile(string userID,string storyFile)
        {
            string sql = "UPDATE `Users` SET `LastStoryFile`='" + storyFile + "' WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }

        public static string GetLastStoryFile(string userID)
        {
            string sql = "SELECT LastStoryFile FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            string ret = "";

            while (rd.Read())
            {
                ret = rd["LastStoryFile"].ToString();
            }
            return ret;
        }
        public static void CreateNewUserRecord(string userID,string Name)
        {
            try
            {
                string sql = "INSERT INTO `Users`(UserID,Name) VALUES('%u','%n');";
                sql = sql.Replace("%u", userID).Replace("%n", Name);
                ExecuteVoid(sql);
            }
            catch(Exception ex)
            {
                CreateNewUserRecord(userID, "ErrorUser");
            }
        }

        public static int GetFComplete(string userID)
        {
            string sql = "SELECT `FComplete` FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            int ret = -1;

            while (rd.Read())
            {
                ret = Convert.ToInt32(rd["FComplete"]);
            }
            return ret;
        }

        public static void SetFComplete(string userID, int mState)
        {
            string sql = "UPDATE `Users` SET `FComplete`=" + mState + " WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }

        public static int GetMsgCount(string userID)
        {
            string sql = "SELECT `MsgCount` FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            int ret = -1;

            while (rd.Read())
            {
                ret = Convert.ToInt32(rd["MsgCount"]);
            }
            return ret;
        }

        public static void IncreaseMsgCount(string userID)
        {
            string sql = "UPDATE `Users` SET `MsgCount` = `MsgCount` + 1 WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }
        public static string GetPName(string userID)
        {
            string sql = "SELECT `PName` FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            string ret="";

            while (rd.Read())
            {
                ret = rd["PName"].ToString();
            }
            return ret;
        }
        public static void SetPName(string userID, string Value)
        {
            string sql = "UPDATE `Users` SET `PName`='" + Value + "' WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }

        public static string GetProc(string userID)
        {
            string sql = "SELECT `Proc` FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            string ret = "";

            while (rd.Read())
            {
                ret = rd["Proc"].ToString();
            }
            return ret;
        }

        public static void SetProc(string userID, string Value)
        {
            string sql = "UPDATE `Users` SET `Proc`='" + Value + "' WHERE `UserID` = '" + userID + "';";
            ExecuteVoid(sql);
        }

        public static UserData GetUserDataById(string userID)
        {
            string sql = "SELECT * FROM `Users` WHERE `UserID`='" + userID + "';";
            UserData ret = new UserData();
            ret.user_id = "";
            SQLiteDataReader rd = ExecuteReader(sql);

            while(rd.Read())
            {
                ret.user_id = userID;
                ret.first_name = rd["Name"].ToString();
                ret.last_name = rd["LName"].ToString();
                ret.PName = rd["PName"].ToString();
                ret.Energy = Convert.ToInt32(rd["Energy"]);
            }

            return ret;
        }
        public static bool IsUserNew(string userID)
        {
            string sql = "SELECT * FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            bool ret = true;

            while (rd.Read())
            {
                ret = false;
            }
            return ret;
        }
        


        public static int GetEnergy(string userID)
        {
            string sql = "SELECT `Energy` FROM `Users` WHERE `UserID`='" + userID + "';";
            SQLiteDataReader rd = ExecuteReader(sql);
            int ret = -1;

            while (rd.Read())
            {
                ret = Convert.ToInt32(rd["Energy"]);
            }
            return ret;
        }

        public static void AddEnergy(string userId,int EnergyValue)
        {
            string sql = "UPDATE `Users` SET `Energy`=`Energy`+" + EnergyValue + " WHERE `UserID` = '" + userId + "';";
            ExecuteVoid(sql);
        }
        public static void DecreaseEnergy(string userId, int EnergyValue)
        {
            string sql = "UPDATE `Users` SET `Energy`=`Energy`-" + EnergyValue + " WHERE `UserID` = '" + userId + "';";
            ExecuteVoid(sql);
        }
    }
}
