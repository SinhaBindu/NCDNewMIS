﻿using NCDNewMIS.Models;
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
            sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_AllRawDataShow(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AllRawDataDownload");
            //sp.Command.AddParameter("@SType", model.SType, DbType.String);
            //sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            //sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_AllRawDataFollowUpDownload(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AllRawDataFollowUpDownload");
            //sp.Command.AddParameter("@SType", model.SType, DbType.String);
            //sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            //sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
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
            sp.Command.AddParameter("@DistrictId", 0, DbType.Int32);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int32);
            sp.Command.AddParameter("@CHCId", model.CHCId, DbType.Int32);
            sp.Command.AddParameter("@PHCId", model.PHCId, DbType.Int32);
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
        public static DataSet SP_PendingUserMembersSubmission(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PendingUserMembersSubmission");
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
        public static DataTable SP_GetCHCPHCSubCenter(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetCHCPHCSubCenter");
            sp.Command.AddParameter("@DistrictId", 1, DbType.Int32);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int32);
            sp.Command.AddParameter("@CHCId", model.CHCId, DbType.Int32);
            sp.Command.AddParameter("@PHCId", model.PHCId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_RegMapped(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RegMapped");
            sp.Command.AddParameter("@DistrictId", 1, DbType.Int32);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int32);
            sp.Command.AddParameter("@CHCId", model.CHCId, DbType.Int32);
            sp.Command.AddParameter("@PHCId", model.PHCId, DbType.Int32);
            sp.Command.AddParameter("@Approved", model.IsActive, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_GetPHCCHC_SubCenterWise(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetPHCCHC_SubCenterWise");
            sp.Command.AddParameter("@DistrictId", 1, DbType.Int32);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int32);
            sp.Command.AddParameter("@SubCenterId", model.SCId, DbType.Int32);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable GetHealthEducation(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health");
            sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ACT2Indicator_Health(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT2Indicator_Health");
            sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_SummaryUserSubmission(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_SummaryUserSubmission");
            sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetImageUploadList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GalleryUploadList");
            sp.Command.AddParameter("@DistrictId", model.DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.Int32);
            sp.Command.AddParameter("@RoundType", model.RoundType, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ACT1Block()
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT1Block");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable GetFinUtilization(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Usp_FinUtilizationQuaterWise");
            sp.Command.AddParameter("@PageType", Convert.ToInt32(model.RoundType), DbType.Int32);
            sp.Command.AddParameter("@fyear", model.PageType, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetHealthScreeing(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_Health1112");
            sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            DataSet dt = sp.ExecuteDataSet();
            return dt;
        }
        public static DataSet GetHealthScreeing2(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_ACT1Indicator_HB");
            // sp.Command.AddParameter("@PageType", model.PageType, DbType.String);
            DataSet dt = sp.ExecuteDataSet();
            return dt;
        }

        public static DataSet SP_RawDataFollowup(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RowDataShowFollowUP");
            sp.Command.AddParameter("@SType", model.SType, DbType.String);
            sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_RawDataSummary()
        {
            StoredProcedure sp = new StoredProcedure("SP_RawDataSummary");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet Sp_FollowupSuspectedSummary()
        {
            StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummary");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }

        public static DataTable SP_UploadImgApi(string imgid = "", string filepath = "", string type = "", string Version = "")
        {
            StoredProcedure sp = new StoredProcedure("Usp_SaveApiImage");
            sp.Command.AddParameter("@imgid", imgid, DbType.String);
            sp.Command.AddParameter("@filepath", filepath, DbType.String);
            sp.Command.AddParameter("@filetype", type, DbType.String);
            sp.Command.AddParameter("@Version", Version, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet Sp_FollowupSuspectedSummaryData()
        {
            StoredProcedure sp = new StoredProcedure("Usp_FollowupSuspectedSummaryData");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }

        public static DataTable Usp_FollowupImageDocumentLoad(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Usp_FollowupImageDocumentLoad");
            sp.Command.AddParameter("@DistrictId",Convert.ToInt32(model.DistrictId), DbType.Int32);
            sp.Command.AddParameter("@BlockId", Convert.ToInt32(model.BlockId), DbType.Int32);
            sp.Command.AddParameter("@SubCenterId", Convert.ToInt32(model.SCId), DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", Convert.ToInt32(model.GPId), DbType.Int32);
            sp.Command.AddParameter("@VillageId", Convert.ToInt32(model.VIId), DbType.Int32);
            sp.Command.AddParameter("@FD", model.FormDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable Usp_ZipFileFollowDownload(string MId = "",string FMId="")
        {
            StoredProcedure sp = new StoredProcedure("Usp_ZipFileFollowDownload");
            sp.Command.AddParameter("@MId", MId, DbType.String);
            sp.Command.AddParameter("@FMId", FMId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}