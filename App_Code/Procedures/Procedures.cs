using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBot.App_Code.Procedures
{
    public enum Procedure
    {
        Idle,ChangeName,Story1,UnKnown,Story2
    }
    public class ProcedureValues
    {
        public const string IdleProcedure = "Idle";
        public const string ChangeNameProcedure = "CName";
        public const string Story1Procedure = "Story1";
        public const string Story2Procedure = "Story2";
    }

    public class Procedures
    {
       public static Procedure GetProcedure(string userID)
        {
            Procedure ret = Procedure.UnKnown;
            string proc = DataBase.DataBase.GetProc(userID);
           switch(proc)
           {
               case ProcedureValues.IdleProcedure:
                   ret = Procedure.Idle;
                   break;
               case ProcedureValues.ChangeNameProcedure:
                   ret = Procedure.ChangeName;
                   break;
               case ProcedureValues.Story1Procedure:
                   ret = Procedure.Story1;
                   break;
               case ProcedureValues.Story2Procedure:
                   ret = Procedure.Story2;
                   break;
           }
            return ret;
        }
    }
}
