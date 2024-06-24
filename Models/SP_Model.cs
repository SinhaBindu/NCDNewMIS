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
            sp.Command.AddParameter("@UserName", model.UserName, DbType.String);
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
        public static DataTable uspRegistration(PostDataModel model)
        {
            StoredProcedure sp = new StoredProcedure("uspRegistration");
            sp.Command.AddParameter("@json", model.JsonData, DbType.String);
            sp.Command.AddParameter("@RowAfected", 0, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_BlockMapSubmission(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BlockMapSubmission");
            sp.Command.AddParameter("@Type", model.Type, DbType.String);
            //sp.Command.AddParameter("@Block", model.Block, DbType.String);
            sp.Command.AddParameter("@FromDt", model.FormDt, DbType.String);
            sp.Command.AddParameter("@ToDt", model.ToDt, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_RowDataShow(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RowDataShow");
            sp.Command.AddParameter("@SType", model.SType, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable SP_RegLocation(RegLocationModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RegLocation");
            sp.Command.AddParameter("@flg", model.flg, DbType.Int32);
            sp.Command.AddParameter("@Isvalue", model.Isvalue, DbType.Int32);
            sp.Command.AddParameter("@value", model.value, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_IndicatorData(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_IndicatorData");
            sp.Command.AddParameter("@flg", model.DistrictBlockType, DbType.Int16);
            sp.Command.AddParameter("@RoundType", model.RoundType, DbType.String);
            sp.Command.AddParameter("@SType", model.SType, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_RegApproved(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RegApproved");
            sp.Command.AddParameter("@DistrictId", 0, DbType.Int16);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int16);
            sp.Command.AddParameter("@CHCId", model.CHCId, DbType.Int16);
            sp.Command.AddParameter("@PHCId", model.PHCId, DbType.Int16);
            sp.Command.AddParameter("@Approved", model.IsActive, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_DistrictBlockData(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Usp_DistBlockWiseData");
            sp.Command.AddParameter("@flg", model.DistrictBlockType, DbType.Int16);
            sp.Command.AddParameter("@RoundType", model.RoundType, DbType.String);
            sp.Command.AddParameter("@SType", model.SType, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_ACT2High(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT2High");
            sp.Command.AddParameter("@SType", model.SType, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_ACT1Indicator(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator");
            sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            sp.Command.AddParameter("@IndicatorId", model.IndicatorId, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable GetGroupACT1Indicator(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("GetGroupACT1Indicator");
            sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetGroupACT1PageType()
        {
            StoredProcedure sp = new StoredProcedure("GetGroupACT1Page");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}