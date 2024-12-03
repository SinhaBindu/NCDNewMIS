using DocumentFormat.OpenXml.EMMA;
using NCDNewMIS.Models;
using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace NCDNewMIS.Models
{
    public class SP_Model
    {
        public static SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        public static SqlCommand cmd;
        public static SqlDataAdapter da;
        public static DataTable SPLoginCheck(LoginModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_LoginCheck");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_LoginCheck";
            cmd.Parameters.AddWithValue("@MobileNo", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Version", model.Version);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            //sp.ExecuteDataSet().Tables[0];
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet SPLoginCheck1(LoginModel model)
        {
            // //StoredProcedure sp = new StoredProcedure("SP_LoginCheck1");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_LoginCheck1";
            cmd.CommandTimeout = 120;
            cmd.Parameters.AddWithValue("@UserName", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Version", model.Version);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet dt = new DataSet();
            da.Fill(dt);//sp.ExecuteDataSet();
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_JsonPostData(PostDataModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_JsonPostData");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_JsonPostData";
            cmd.Parameters.AddWithValue("@UserName", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Version", model.Version);
            cmd.Parameters.AddWithValue("@JsonData", model.JsonData);
            cmd.Parameters.AddWithValue("@RowAfected", 0);
            DataTable dt = new DataTable();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);// sp.ExecuteDataSet().Tables[0];
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable uspRegistration(PostDataModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("uspRegistration");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "uspRegistration";
            cmd.Parameters.AddWithValue("@json", model.JsonData);
            cmd.Parameters.AddWithValue("@RowAfected", 0);
            da = new SqlDataAdapter();
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da.SelectCommand=cmd;
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet SP_BlockMapSubmission(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_BlockMapSubmission");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_BlockMapSubmission";
            cmd.Parameters.AddWithValue("@Type", model.Type);
            //sp.Command.AddParameter("@Block", model.Block);
            cmd.Parameters.AddWithValue("@FromDt", model.FormDt);
            cmd.Parameters.AddWithValue("@ToDt", model.ToDt);
            DataSet ds = new DataSet();// sp.ExecuteDataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_RowDataShow(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_RowDataShow");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RowDataShow";
            cmd.Parameters.AddWithValue("@SType", model.SType);
            cmd.Parameters.AddWithValue("@FD", model.FormDt);
            cmd.Parameters.AddWithValue("@TD", model.ToDt);
            cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
            cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
            DataSet ds = new DataSet();// sp.ExecuteDataSet();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_AllRawDataShow(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_AllRawDataDownload");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_AllRawDataDownload";
            //sp.Command.AddParameter("@SType", model.SType);
            //sp.Command.AddParameter("@FD", model.FormDt);
            //sp.Command.AddParameter("@TD", model.ToDt);
            DataSet ds = new DataSet();// sp.ExecuteDataSet();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_AllRawDataFollowUpDownload(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_AllRawDataFollowUpDownload");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_AllRawDataFollowUpDownload";
            //sp.Command.AddParameter("@SType", model.SType);
            //sp.Command.AddParameter("@FD", model.FormDt);
            //sp.Command.AddParameter("@TD", model.ToDt);
            DataSet ds = new DataSet();// sp.ExecuteDataSet();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataTable SP_RegLocation(RegLocationModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_RegLocation");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RegLocation";
            cmd.Parameters.AddWithValue("@flg", model.flg);
            cmd.Parameters.AddWithValue("@Isvalue", model.Isvalue);
            cmd.Parameters.AddWithValue("@value", model.value);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet SP_IndicatorData(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_IndicatorData");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_IndicatorData";
            cmd.Parameters.AddWithValue("@flg", model.DistrictBlockType);
            cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
            cmd.Parameters.AddWithValue("@SType", model.SType);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_RegApproved(FilterModel model)
        {
            ////StoredProcedure sp = new StoredProcedure("SP_RegApproved");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RegApproved";
            cmd.Parameters.AddWithValue("@DistrictId", 0);
            cmd.Parameters.AddWithValue("@BlockId", Convert.ToInt32(model.BlockId));
            cmd.Parameters.AddWithValue("@CHCId", Convert.ToInt32(model.CHCId));
            cmd.Parameters.AddWithValue("@PHCId", Convert.ToInt32(model.PHCId));
            cmd.Parameters.AddWithValue("@Approved", model.IsActive);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_DistrictBlockData(FilterModel model)
        {
            // //StoredProcedure sp = new StoredProcedure("Usp_DistBlockWiseData");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_DistBlockWiseData";
            //sp.Command.CommandTimeout = 0;//300 5mint; 2 120 mints;
            cmd.Parameters.AddWithValue("@flg", model.DistrictBlockType);
            cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
            cmd.Parameters.AddWithValue("@SType", model.SType);
            cmd.Parameters.AddWithValue("@StartDate", model.FormDt);
            cmd.Parameters.AddWithValue("@EndDate", model.ToDt);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_PendingUserMembersSubmission(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_PendingUserMembersSubmission");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_PendingUserMembersSubmission";
            cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
            cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_ACT2High(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT2High");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT2High";
            cmd.Parameters.AddWithValue("@SType", model.SType);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_ACT1Indicator(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT1Indicator";
            cmd.Parameters.AddWithValue("@PageType", model.PageType);
            cmd.Parameters.AddWithValue("@IndicatorId", model.IndicatorId);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataTable GetGroupACT1Indicator(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("GetGroupACT1Indicator");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "GetGroupACT1Indicator";
            cmd.Parameters.AddWithValue("@PageType", model.PageType);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable GetGroupACT1PageType()
        {
            //StoredProcedure sp = new StoredProcedure("GetGroupACT1Page");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "GetGroupACT1Page";
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_GetCHCPHCSubCenter(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_GetCHCPHCSubCenter");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_GetCHCPHCSubCenter";
            cmd.Parameters.AddWithValue("@DistrictId", 1);
            cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
            cmd.Parameters.AddWithValue("@CHCId", model.CHCId);
            cmd.Parameters.AddWithValue("@PHCId", model.PHCId);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet SP_RegMapped(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_RegMapped");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RegMapped";
            cmd.Parameters.AddWithValue("@DistrictId", 1);
           cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
           cmd.Parameters.AddWithValue("@CHCId", model.CHCId);
           cmd.Parameters.AddWithValue("@PHCId", model.PHCId);
           cmd.Parameters.AddWithValue("@Approved", model.IsActive);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_GetPHCCHC_SubCenterWise(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_GetPHCCHC_SubCenterWise");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_GetPHCCHC_SubCenterWise";
            cmd.Parameters.AddWithValue("@DistrictId", 1);
           cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
           cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataTable GetHealthEducation(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT1Indicator_Health";
            cmd.Parameters.AddWithValue("@PageType", model.PageType);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_ACT2Indicator_Health(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT2Indicator_Health");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT2Indicator_Health";
            cmd.Parameters.AddWithValue("@PageType", model.PageType);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_SummaryUserSubmission(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_SummaryUserSubmission");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_SummaryUserSubmission";
            cmd.Parameters.AddWithValue("@FD", model.FormDt);
           cmd.Parameters.AddWithValue("@TD", model.ToDt);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_GetImageUploadList(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_GalleryUploadList");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_GalleryUploadList";
            cmd.Parameters.AddWithValue("@DistrictId", model.DistrictId);
           cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
           cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable SP_ACT1Block()
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT1Block");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT1Block";
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable GetFinUtilization(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("Usp_FinUtilizationQuaterWise");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_FinUtilizationQuaterWise";
            cmd.Parameters.AddWithValue("@PageType", Convert.ToInt32(model.RoundType));
           cmd.Parameters.AddWithValue("@fyear", model.PageType);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet GetHealthScreeing(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health1112");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT1Indicator_Health1112";
            cmd.Parameters.AddWithValue("@PageType", model.PageType);
            DataSet dt = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet GetHealthScreeing2(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_HB");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_ACT1Indicator_HB";
            //cmd.Parameters.AddWithValue("@PageType", model.PageType);
            DataSet dt = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }

        public static DataSet SP_RawDataFollowup(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("SP_RowDataShowFollowUP");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RowDataShowFollowUP";
            cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
           cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
           cmd.Parameters.AddWithValue("@SType", model.SType);
           cmd.Parameters.AddWithValue("@FD", model.FormDt);
           cmd.Parameters.AddWithValue("@TD", model.ToDt);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet SP_RawDataSummary()
        {
            // //StoredProcedure sp = new StoredProcedure("SP_RawDataSummary");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_RawDataSummary";
            da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            // sp.Command.CommandTimeout = 3600;
            //DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet Sp_FollowupSuspectedSummary()
        {
            //StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummary");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_FollowupSuspectedSummary";
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }

        public static DataTable SP_UploadImgApi(string imgid = "", string filepath = "", string type = "", string Version = "")
        {
            //StoredProcedure sp = new StoredProcedure("Usp_SaveApiImage");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_SaveApiImage";
            cmd.Parameters.AddWithValue("@imgid", imgid);
            cmd.Parameters.AddWithValue("@filepath", filepath);
            cmd.Parameters.AddWithValue("@filetype", type);
            cmd.Parameters.AddWithValue("@Version", Version);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet Sp_FollowupSuspectedSummaryData(string BlockId, string StartDate,string EndDate, string TypeOfPatient, string GenderId,string Ageyrs)
        {
            //StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummaryData");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_FollowupSuspectedSummaryData";
            cmd.Parameters.AddWithValue("@BlockId", BlockId);
           cmd.Parameters.AddWithValue("@StartDate", StartDate);
           cmd.Parameters.AddWithValue("@EndDate", EndDate);
           cmd.Parameters.AddWithValue("@TypeOfPatient", TypeOfPatient);
           cmd.Parameters.AddWithValue("@GenderId", GenderId);
           cmd.Parameters.AddWithValue("@Ageyrs", Ageyrs);
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
        public static DataSet Sp_FollowupSuspectedSummaryInDownload(string StartDate, string EndDate)
        {
            //StoredProcedure sp = new StoredProcedure("SP_Followup_UniqueFacilityVisit");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "SP_Followup_UniqueFacilityVisit";
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }

        public static DataTable Usp_FollowupImageDocumentLoad(FilterModel model)
        {
            //StoredProcedure sp = new StoredProcedure("Usp_FollowupImageDocumentLoad");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_FollowupImageDocumentLoad";
            cmd.Parameters.AddWithValue("@DistrictId",Convert.ToInt32(model.DistrictId));
           cmd.Parameters.AddWithValue("@BlockId", Convert.ToInt32(model.BlockId));
           cmd.Parameters.AddWithValue("@SubCenterId", Convert.ToInt32(model.SCId));
           cmd.Parameters.AddWithValue("@PanchayatId", Convert.ToInt32(model.GPId));
           cmd.Parameters.AddWithValue("@VillageId", Convert.ToInt32(model.VIId));
           cmd.Parameters.AddWithValue("@FD", model.FormDt);
           cmd.Parameters.AddWithValue("@TD", model.ToDt);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataTable Usp_ZipFileFollowDownload(string MId = "",string FMId="")
        {
            //StoredProcedure sp = new StoredProcedure("Usp_ZipFileFollowDownload");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "Usp_ZipFileFollowDownload";
            cmd.Parameters.AddWithValue("@MId", MId);
           cmd.Parameters.AddWithValue("@FMId", FMId);
            DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            return dt;
        }
        public static DataSet SP_FollowUpDetailsDownload()
        {
            //StoredProcedure sp = new StoredProcedure("USP_FollowUpDetailsData");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;
            cmd.CommandText = "USP_FollowUpDetailsData";
            DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
            da.SelectCommand = cmd;
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            return ds;
        }
    }
}