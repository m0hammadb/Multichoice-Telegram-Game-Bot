using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.StringProcessors
{
    public static class StringProcessors
    {
        public static string FindBetween(this String sSource, string S1, string S2, int start = 0)
        {
            string returnValue = "";
            int startIndex = sSource.IndexOf(S1, start);
            int endIndex = -1;
            if (startIndex >= 0)
            {
                endIndex = sSource.IndexOf(S2, startIndex + S1.Length);
                if (endIndex >= 0)
                {
                    returnValue = sSource.Substring(startIndex + S1.Length, endIndex - (startIndex + S1.Length));
                }
            }
            return returnValue;
        }

        
        
        public static string FindEverythingPriorTo(this String sSource, string SearchTerm)
        {
            //salam chetori che khabar
            string tmp = "&!@RANDOM_THING&!@" + sSource;
            return FindBetween(tmp, "&!@RANDOM_THING&!@", SearchTerm);
        }
        public static string FindEverthingFrom(this String sSource, string SearchTerm)
        {
            string tmp = sSource + "&!@RANDOM_THING&!@";
            return tmp.FindBetween(SearchTerm, "&!@RANDOM_THING&!@");
        }
        public static string[] FindBetweenArray(this String sSource, string S1, string S2)
        {
            List<string> myStringList = new List<string>();
            string[] returnValue = null;
            string tmp = sSource;
            string s = tmp.FindBetween(S1, S2);
            while (s != "")
            {
                myStringList.Add(s);
                tmp = tmp.Replace(S1 + s + S2, "");
                s = tmp.FindBetween(S1, S2);
            }
            returnValue = myStringList.ToArray();
            return returnValue;
        }

        public static string FindBetween(this StringBuilder sSource, string S1, string S2, int start = 0)
        {
            return sSource.ToString().FindBetween(S1, S2, start);
        }

        public static string FindEverythingPriorTo(this StringBuilder sSource, string SearchTerm)
        {
            return sSource.ToString().FindEverythingPriorTo(SearchTerm);
        }
        public static string FindEverthingFrom(this StringBuilder sSource, string SearchTerm)
        {
            return sSource.ToString().FindEverthingFrom(SearchTerm);
        }
        public static string[] FindBetweenArray(this StringBuilder sSource, string S1, string S2)
        {
            return sSource.ToString().FindBetweenArray(S1,S2);
        }
    }
}
