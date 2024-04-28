using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class SP_Model
    {
        public static DataTable SPLoginCheck(LoginModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_LoginCheck");
            sp.Command.AddParameter("@MobileNo", model.UserName, DbType.String);
            sp.Command.AddParameter("@Password", model.Password, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}