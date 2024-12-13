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
        public SP_Model()
        {
            //con = null;
            con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
            else
            {
                con.Close();
            }
        }
        public static DataTable SPLoginCheck(LoginModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_LoginCheck");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_LoginCheck";
                cmd.Parameters.AddWithValue("@MobileNo", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Version", model.Version);
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                //sp.ExecuteDataSet().Tables[0];
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet SPLoginCheck1(LoginModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                // //StoredProcedure sp = new StoredProcedure("SP_LoginCheck1");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_LoginCheck1";
                cmd.CommandTimeout = 9000;
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Version", model.Version);
                da.SelectCommand = cmd;
                DataSet dt = new DataSet();
                da.Fill(dt);//sp.ExecuteDataSet();
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_JsonPostData(PostDataModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_JsonPostData");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_JsonPostData";
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Version", model.Version);
                cmd.Parameters.AddWithValue("@JsonData", model.JsonData);
                cmd.Parameters.AddWithValue("@RowAfected", 0);
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);// sp.ExecuteDataSet().Tables[0];
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable uspRegistration(PostDataModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("uspRegistration");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "uspRegistration";
                cmd.Parameters.AddWithValue("@json", model.JsonData);
                cmd.Parameters.AddWithValue("@RowAfected", 0);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);// sp.ExecuteDataSet().Tables[0];
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet SP_BlockMapSubmission(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_BlockMapSubmission");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_RowDataShow(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_RowDataShow");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RowDataShow";
                cmd.Parameters.AddWithValue("@SType", model.SType);
                cmd.Parameters.AddWithValue("@FD", model.FormDt);
                cmd.Parameters.AddWithValue("@TD", model.ToDt);
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
                DataSet ds = new DataSet();// sp.ExecuteDataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_AllRawDataShow(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_AllRawDataDownload");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_AllRawDataDownload";
                //sp.Command.AddParameter("@SType", model.SType);
                //sp.Command.AddParameter("@FD", model.FormDt);
                //sp.Command.AddParameter("@TD", model.ToDt);
                DataSet ds = new DataSet();// sp.ExecuteDataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_AllRawDataFollowUpDownload(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_AllRawDataFollowUpDownload");
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_AllRawDataFollowUpDownload";
                //sp.Command.AddParameter("@SType", model.SType);
                //sp.Command.AddParameter("@FD", model.FormDt);
                //sp.Command.AddParameter("@TD", model.ToDt);
                DataSet ds = new DataSet();// sp.ExecuteDataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataTable SP_RegLocation(RegLocationModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_RegLocation");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RegLocation";
                cmd.Parameters.AddWithValue("@flg", model.flg);
                cmd.Parameters.AddWithValue("@Isvalue", model.Isvalue);
                cmd.Parameters.AddWithValue("@value", model.value);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet SP_IndicatorData(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_IndicatorData");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_IndicatorData";
                cmd.Parameters.AddWithValue("@flg", model.DistrictBlockType);
                cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
                cmd.Parameters.AddWithValue("@SType", model.SType);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_RegApproved(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                ////StoredProcedure sp = new StoredProcedure("SP_RegApproved");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RegApproved";
                cmd.Parameters.AddWithValue("@DistrictId", 0);
                cmd.Parameters.AddWithValue("@BlockId", Convert.ToInt32(model.BlockId));
                cmd.Parameters.AddWithValue("@CHCId", Convert.ToInt32(model.CHCId));
                cmd.Parameters.AddWithValue("@PHCId", Convert.ToInt32(model.PHCId));
                cmd.Parameters.AddWithValue("@Approved", model.IsActive);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_DistrictBlockData(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                // //StoredProcedure sp = new StoredProcedure("Usp_DistBlockWiseData");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_DistBlockWiseData";
                //sp.Command.CommandTimeout = 0;//300 5mint; 2 9000 mints;
                cmd.Parameters.AddWithValue("@flg", model.DistrictBlockType);
                cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
                cmd.Parameters.AddWithValue("@SType", model.SType);
                cmd.Parameters.AddWithValue("@StartDate", model.FormDt);
                cmd.Parameters.AddWithValue("@EndDate", model.ToDt);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_PendingUserMembersSubmission(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_PendingUserMembersSubmission");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_PendingUserMembersSubmission";
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_ACT2High(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT2High");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT2High";
                cmd.Parameters.AddWithValue("@SType", model.SType);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_ACT1Indicator(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT1Indicator";
                cmd.Parameters.AddWithValue("@PageType", model.PageType);
                cmd.Parameters.AddWithValue("@IndicatorId", model.IndicatorId);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataTable GetGroupACT1Indicator(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("GetGroupACT1Indicator");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "GetGroupACT1Indicator";
                cmd.Parameters.AddWithValue("@PageType", model.PageType);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable GetGroupACT1PageType()
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("GetGroupACT1Page");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "GetGroupACT1Page";
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_GetCHCPHCSubCenter(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_GetCHCPHCSubCenter");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_GetCHCPHCSubCenter";
                cmd.Parameters.AddWithValue("@DistrictId", 1);
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@CHCId", model.CHCId);
                cmd.Parameters.AddWithValue("@PHCId", model.PHCId);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet SP_RegMapped(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_RegMapped");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RegMapped";
                cmd.Parameters.AddWithValue("@DistrictId", 1);
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@CHCId", model.CHCId);
                cmd.Parameters.AddWithValue("@PHCId", model.PHCId);
                cmd.Parameters.AddWithValue("@Approved", model.IsActive);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_GetPHCCHC_SubCenterWise(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_GetPHCCHC_SubCenterWise");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_GetPHCCHC_SubCenterWise";
                cmd.Parameters.AddWithValue("@DistrictId", 1);
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataTable GetHealthEducation(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT1Indicator_Health";
                cmd.Parameters.AddWithValue("@PageType", model.PageType);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_ACT2Indicator_Health(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT2Indicator_Health");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT2Indicator_Health";
                cmd.Parameters.AddWithValue("@PageType", model.PageType);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_SummaryUserSubmission(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_SummaryUserSubmission");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_SummaryUserSubmission";
                cmd.Parameters.AddWithValue("@FD", model.FormDt);
                cmd.Parameters.AddWithValue("@TD", model.ToDt);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_GetImageUploadList(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_GalleryUploadList");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_GalleryUploadList";
                cmd.Parameters.AddWithValue("@DistrictId", model.DistrictId);
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@RoundType", model.RoundType);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable SP_ACT1Block()
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT1Block");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT1Block";
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable GetFinUtilization(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_FinUtilizationQuaterWise");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_FinUtilizationQuaterWise";
                cmd.Parameters.AddWithValue("@PageType", Convert.ToInt32(model.RoundType));
                cmd.Parameters.AddWithValue("@fyear", model.PageType);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet GetHealthScreeing(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health1112");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT1Indicator_Health1112";
                cmd.Parameters.AddWithValue("@PageType", model.PageType);
                DataSet dt = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet GetHealthScreeing2(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_HB");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_ACT1Indicator_HB";
                //cmd.Parameters.AddWithValue("@PageType", model.PageType);
                DataSet dt = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }

        public static DataSet SP_RawDataFollowup(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_RowDataShowFollowUP");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RowDataShowFollowUP";
                cmd.Parameters.AddWithValue("@BlockId", model.BlockId);
                cmd.Parameters.AddWithValue("@SubCenterId", model.SCId);
                cmd.Parameters.AddWithValue("@SType", model.SType);
                cmd.Parameters.AddWithValue("@FD", model.FormDt);
                cmd.Parameters.AddWithValue("@TD", model.ToDt);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet SP_RawDataSummary()
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                // //StoredProcedure sp = new StoredProcedure("SP_RawDataSummary");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_RawDataSummary";
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                // sp.Command.CommandTimeout = 3600;
                //DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet Sp_FollowupSuspectedSummary()
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummary");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_FollowupSuspectedSummary";
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }

        public static DataTable SP_UploadImgApi(string imgid = "", string filepath = "", string type = "", string Version = "")
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_SaveApiImage");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_SaveApiImage";
                cmd.Parameters.AddWithValue("@imgid", imgid);
                cmd.Parameters.AddWithValue("@filepath", filepath);
                cmd.Parameters.AddWithValue("@filetype", type);
                cmd.Parameters.AddWithValue("@Version", Version);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet Sp_FollowupSuspectedSummaryData(string BlockId, string StartDate, string EndDate, string TypeOfPatient, string GenderId, string Ageyrs)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummaryData");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
        public static DataSet Sp_FollowupSuspectedSummaryInDownload(string StartDate, string EndDate)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("SP_Followup_UniqueFacilityVisit");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SP_Followup_UniqueFacilityVisit";
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }

        public static DataTable Usp_FollowupImageDocumentLoad(FilterModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_FollowupImageDocumentLoad");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_FollowupImageDocumentLoad";
                cmd.Parameters.AddWithValue("@DistrictId", Convert.ToInt32(model.DistrictId));
                cmd.Parameters.AddWithValue("@BlockId", Convert.ToInt32(model.BlockId));
                cmd.Parameters.AddWithValue("@SubCenterId", Convert.ToInt32(model.SCId));
                cmd.Parameters.AddWithValue("@PanchayatId", Convert.ToInt32(model.GPId));
                cmd.Parameters.AddWithValue("@VillageId", Convert.ToInt32(model.VIId));
                cmd.Parameters.AddWithValue("@FD", model.FormDt);
                cmd.Parameters.AddWithValue("@TD", model.ToDt);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataTable Usp_ZipFileFollowDownload(string MId = "", string FMId = "")
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("Usp_ZipFileFollowDownload");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "Usp_ZipFileFollowDownload";
                cmd.Parameters.AddWithValue("@MId", MId);
                cmd.Parameters.AddWithValue("@FMId", FMId);
                DataTable dt = new DataTable();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return dt;
            }
        }
        public static DataSet SP_FollowUpDetailsDownload()
        {
            using (var context = new ApplicationDbContext())
            {
                con = (SqlConnection)context.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //StoredProcedure sp = new StoredProcedure("USP_FollowUpDetailsData");
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "USP_FollowUpDetailsData";
                DataSet ds = new DataSet();// sp.ExecuteDataSet().Tables[0];
                da.SelectCommand = cmd;
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ds;
            }
        }
    }
}