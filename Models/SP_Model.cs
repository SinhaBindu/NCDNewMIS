using NCDNewMIS.Models;
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
            sp.Command.AddParameter("@Version", model.Version, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SPLoginCheck1(LoginModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_LoginCheck1");
            sp.Command.AddParameter("@MobileNo", model.UserName, DbType.String);
            sp.Command.AddParameter("@Password", model.Password, DbType.String);
            sp.Command.AddParameter("@Version", model.Version, DbType.String);
            DataSet dt = sp.ExecuteDataSet();
            return dt;
        }
        public static DataTable SP_JsonPostData(PostDataModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_JsonPostData");
            sp.Command.AddParameter("@UserName", model.UserName, DbType.String);
            sp.Command.AddParameter("@Password", model.Password, DbType.String);
            sp.Command.AddParameter("@Version", model.Version, DbType.String);
            sp.Command.AddParameter("@JsonData", model.JsonData, DbType.String);
            sp.Command.AddParameter("@RowAfected", 0, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_RowDataShow()
        {
            StoredProcedure sp = new StoredProcedure("SP_RowDataShow");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
    }
}