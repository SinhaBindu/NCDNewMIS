﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NCDNewMIS.Models
{
    public static class CommonModel
    {
        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }
        #region BaseUrl
        public static string GetBaseUrl()
        {
            var str = HttpContext.Current.Request.Url.Host;
            //return str;
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            string host = HttpContext.Current.Request.Url.Host;
            if (host == "localhost")
            {
                host = HttpContext.Current.Request.Url.Authority;
                return HttpContext.Current.Request.Url.Scheme + "://" + host;
            }
            //return urlHelper.Content("~/");
            return HttpContext.Current.Request.Url.Scheme + "://" + str;
        }
        public static string GetWebUrl()
        {
            return ConfigurationManager.AppSettings["WebUrl"];
        }

        public static bool IsEmailConfiguredToLive()
        {
            var isLive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsEmailSetLive"].ToString());
            return isLive;
        }
        public static string GetLocalEmailAddress()
        {
            var emailAddr = ConfigurationManager.AppSettings["LocalEmailAddress"].ToString();
            return emailAddr;
        }

        public static string GetFileUrl(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return CommonModel.GetBaseUrl() + filePath.ToString().Replace("~", "");
            else
                return string.Empty;
        }

        public static string GetMultipleFileUrlFromComma(string filePaths)
        {
            //string filePath = string.Empty;
            //var filePathArray = filePaths.Split(',');
            List<string> filePathList = new List<string>();
            foreach (var path in filePaths.Split(','))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    //return CommonModel.GetBaseUrl() + path.ToString().Replace("~", "");
                    filePathList.Add(CommonModel.GetBaseUrl() + path.Trim().ToString().Replace("~", ""));
                }
                //else
                //    return string.Empty;

            }
            //filePathList=.Join(',');
            return string.Join(",", filePathList);
        }

        public static string GetHeaderUSLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/USAID_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/USAID_Template.png\"");
            else
                return string.Empty;
        }
        public static string GetHeaderCareLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/Care_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/Care_Template.png\"");
            else
                return string.Empty;
        }

        public static List<SelectListItem> GetSP_GetSubCenterCenterLists(int IsAll = 2, int BlockId = 0, int CHCId = 0, int PHCId = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                if (BlockId > 0)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.User.IsInRole(RoleNameCont.Admin))
                        {
                            FilterModel model = new FilterModel();
                            model.BlockId = Convert.ToString(BlockId);
                            model.CHCId = Convert.ToString(CHCId);
                            model.PHCId = Convert.ToString(PHCId);
                            DataTable dt = SP_Model.SP_GetCHCPHCSubCenter(model);
                            var itemid = Convert.ToInt32(BlockId);//.Where(x => x.Field<int>("TrainerId") == itemid)
                            //list = new SelectList(dt.AsEnumerable(), "SubCenterId", "CHCPHCSubCenter").OrderBy(x => x.Text).ToList();
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    list.Add(new SelectListItem { Value = dr["SubCenterId"].ToString(), Text = dr["CHCPHCSubCenter"].ToString(), Selected = true });
                                }
                                //return list.OrderBy(x => x.Text).ToList();
                            }
                            if (IsAll == 0)
                            {
                                list.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                            }
                            else if (IsAll == 1)
                            {
                                list.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                            }
                            return list;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        #endregion
        #region Get User Role 
        public static string GetUserRole()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole(RoleNameCont.Admin))
                {
                    return "all";
                }
                if (HttpContext.Current.User.IsInRole(RoleNameCont.State))
                {
                    return "all";
                }
                else if (HttpContext.Current.User.IsInRole(RoleNameCont.Viewer))
                {
                    return "all";
                }
                else if (HttpContext.Current.User.IsInRole(RoleNameCont.District))
                {
                    return RoleNameCont.District;
                }
                else if (HttpContext.Current.User.IsInRole(RoleNameCont.User))
                {
                    return RoleNameCont.User;
                }
            }
            return "all";
        }

        //    public static UserViewModel User
        //    {
        //         return "";

        //    //get
        //    //{
        //    //    if (HttpContext.Current.User.Identity.IsAuthenticated)
        //    //    {
        //    //        if (HttpContext.Current.Session["User"] != null)
        //    //        {
        //    //            return (UserViewModel)HttpContext.Current.Session["User"];
        //    //        }
        //    //        //    else
        //    //        //    //{
        //    //        //    //    var u = dbe.AspNetUsers.Single(x => x.UserName == HttpContext.Current.User.Identity.Name);
        //    //        //    //    var locations = (from l in dbe.MST_Location
        //    //        //    //                     join ul in dbe.AspNetUsers_Location on l.ID equals ul.LocationId into loctionGroup
        //    //        //    //                     from uLoc in loctionGroup.DefaultIfEmpty()
        //    //        //    //                     where ((u.LocationID != 0 && uLoc.UserId.ToString() == u.Id) || u.LocationID == 0)
        //    //        //    //                     && l.IsActive.Value
        //    //        //    //                     select l).ToList();
        //    //        //    //    var Organizations = (from o in dbe.MST_Organization
        //    //        //    //                         where ((u.IDOrganization != 0 && o.ID == u.IDOrganization) || u.IDOrganization == 0)
        //    //        //    //                         && o.IsActive
        //    //        //    //                         select o).ToList();

        //    //        //        var role = GetUserRole();
        //    //        //        var forAll = new List<string>() { "All", "CPMU" };

        //    //        //        var user = new UserViewModel
        //    //        //        {
        //    //        //            Id = u.Id,
        //    //        //            Name = u.Name,
        //    //        //            Email = u.Email,
        //    //        //            IDOrganization = u.IDOrganization.Value,
        //    //        //            LocationID = u.LocationID.Value,
        //    //        //            PhoneNumber = u.PhoneNumber,
        //    //        //            Role = u.AspNetRoles.First()?.Name,
        //    //        //            OrganizationName = string.Join(", ", Organizations.Select(x => x.Name)),
        //    //        //            LocationName = string.Join(", ", locations.Select(x => x.Name)),
        //    //        //            Locations = locations
        //    //        //        };
        //    //        //        HttpContext.Current.Session["User"] = user;
        //    //        //        return user;
        //    //        //    }
        //    //    }
        //    //    else
        //    //    {
        //    //        HttpContext.Current.Response.Redirect("~/Account/Login", false);
        //    //        return null;
        //    //    }
        //    //}
        //}

        public class User_Model
        {
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public string DistrictName { get; set; }
            public string MobileNo { get; set; }
            public string EmailId { get; set; }
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsStatus { get; set; }
        }

        #endregion

        #region Master 
        public static List<SelectListItem> GetActive()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "-1", Text = "All" });
            list.Add(new SelectListItem { Value = "1", Text = "Active" });
            list.Add(new SelectListItem { Value = "0", Text = "InActive" });
            return list.OrderBy(x => Convert.ToInt16(x.Value)).ToList();
        }
        public static List<SelectListItem> GetACT1IndicatorSome(int IsSelectAll = 0, FilterModel model = null)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = SP_Model.GetGroupACT1Indicator(model);
                var lists = new List<SelectListItem>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lists.Add(new SelectListItem { Value = dr["indicatorId"].ToString(), Text = dr["indicatorText"].ToString(), Selected = true });
                    }
                }
                if (IsSelectAll == 0)
                {
                    lists.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    lists.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return lists.OrderBy(x => x.Text).ToList(); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetACT1PageTypeSome(int IsSelectAll = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = SP_Model.GetGroupACT1PageType();
                var lists = new List<SelectListItem>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lists.Add(new SelectListItem { Value = dr["Value"].ToString(), Text = dr["Text"].ToString(), Selected = true });
                    }
                }
                if (IsSelectAll == 0)
                {
                    lists.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    lists.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return lists.OrderBy(x => x.Text).ToList(); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetDistrict(int IsSelectAll = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = new DataTable();
                var listitem = new SelectList(_db.District_Master.Where(x => x.IsActive == true), "DistrictId", "District").OrderBy(x => x.Text).ToList();
                listitem = new SelectList(listitem, "Value", "Text").OrderBy(x => x.Text).ToList();
                if (IsSelectAll == 0)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return listitem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetBlock(int IsSelectAll = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = new DataTable();
                var listitem = new SelectList(_db.Block_Master.Where(x => x.IsActive == true && x.DistrictId == 1), "BlockId", "Block").OrderBy(x => x.Text).ToList();
                listitem = new SelectList(listitem, "Value", "Text").OrderBy(x => x.Text).ToList();
                if (IsSelectAll == 0)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return listitem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetCHC(int BlockId, int IsSelectAll = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = new DataTable();
                var listitem = new SelectList(_db.CHC_Master.Where(x => x.IsActive == true && x.DistrictId == 1 && x.BlockId == BlockId), "CHCId", "CHCN").OrderBy(x => x.Text).ToList();
                listitem = new SelectList(listitem, "Value", "Text").OrderBy(x => x.Text).ToList();
                if (IsSelectAll == 0)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return listitem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<SelectListItem> GetPHC(int BlockId, int CHCId, int IsSelectAll = 0)
        {
            NCD_DBEntities _db = new NCD_DBEntities();
            try
            {
                DataTable dt = new DataTable();
                var listitem = new SelectList(_db.PHC_Master.Where(x => x.IsActive == true && x.DistrictId == 1 && x.BlockId == BlockId && x.CHCId == CHCId), "PHCId", "PHC").OrderBy(x => x.Text).ToList();
                listitem = new SelectList(listitem, "Value", "Text").OrderBy(x => x.Text).ToList();
                if (IsSelectAll == 0)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "All" });
                }
                if (IsSelectAll == 1)
                {
                    listitem.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                }
                return listitem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public static List<SelectListItem> GetBoolYesNo()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "1", Text = "Yes" });
        //    list.Add(new SelectListItem { Value = "0", Text = "No" });
        //    return list.OrderByDescending(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetCalling()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "Yes", Text = "Yes" });
        //    list.Add(new SelectListItem { Value = "No", Text = "No" });
        //    //list.Add(new SelectListItem { Value = "Other", Text = "Other" });
        //    return list.OrderByDescending(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetGender()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    list.Add(new SelectListItem { Value = "Male", Text = "Male" });
        //    list.Add(new SelectListItem { Value = "Female", Text = "Female" });
        //    return list.OrderByDescending(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetRoleList(bool IsAll = false)
        //{
        //    Hunar_DBEntities db_ = new Hunar_DBEntities();
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    try
        //    {
        //        //if (HttpContext.Current.User.IsInRole(RoleNameCont.State) || HttpContext.Current.User.IsInRole(RoleNameCont.Admin))
        //        //{
        //        items = new SelectList(db_.AspNetRoles.ToList(), "ID", "Name").OrderBy(x => x.Text).ToList();
        //        //}
        //        //else
        //        //{
        //        //    items = new SelectList(db_.AspNetRoles, "ID", "Name").OrderBy(x => x.Text).ToList();
        //        //}
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "", Text = "Select", Selected = true });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetState(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.State_Master, "ID", "StateName").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        //DO To
        //        throw;
        //    }
        //}

        //public static List<SelectListItem> GetCourses(bool IsAll = false, int DistrictId = 0)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.Courses_Master.Where(x => x.IsActive == true), "ID", "CourseName").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetBatch(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    try
        //    {
        //        DataTable dt = SPManager.SP_Batch();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            items.Add(new SelectListItem { Value = dr["ID"].ToString(), Text = dr["BatchName"].ToString() });
        //        }
        //        //new SelectList(_db.Batch_Master.Where(x => x.IsActive == true), "Id", "BatchName").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetEducational(bool IsAll = false, int DistrictId = 0, int BlockId = 0)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.Educational_Master.Where(x => x.IsActive == true), "ID", "QualificationName").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetTrainingAgency(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.TrainingAgency_Master.Where(x => x.IsActive == true), "ID", "TrainingAgencyName").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetTrainingCenter(bool IsAll = false, int DistrictId = 0, int TrainingAgencyId = 0)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        //&& x.TrainingAgencyID_fk == TrainingAgencyId
        //        var items = new SelectList(_db.TrainingCenter_Master.Where(x => x.IsActive == true && x.DistrictID_fk == DistrictId && x.TrainingAgencyID_fk == TrainingAgencyId), "ID", "TrainingCenter").OrderBy(x => x.Text).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<SelectListItem> GetLocatedKM(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.LocatedKM_Master, "ID", "KM").OrderBy(x => Convert.ToInt32(x.Value)).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    //List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new SelectListItem { Value = "1", Text = "Less than 10Km" });
        //    //list.Add(new SelectListItem { Value = "2", Text = "10 Km - 25 Km" });
        //    //list.Add(new SelectListItem { Value = "3", Text = "25 Km - 50 Km" });
        //    //list.Add(new SelectListItem { Value = "4", Text = "More than 50 Km" });
        //    //return list.OrderByDescending(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetBenefit(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.Benefit_Master, "ID", "Benefit").OrderBy(x => Convert.ToInt32(x.Value)).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    //List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new SelectListItem { Value = "1", Text = "Food" });
        //    //list.Add(new SelectListItem { Value = "2", Text = "Accommodation" });
        //    //list.Add(new SelectListItem { Value = "3", Text = "Travel allowance" });
        //    //list.Add(new SelectListItem { Value = "4", Text = "Medical Insurance" });
        //    //list.Add(new SelectListItem { Value = "5", Text = "Other" });
        //    //return list.OrderByDescending(x => x.Text).ToList();
        //}
        //public static List<SelectListItem> GetEmployed(bool IsAll = false)
        //{
        //    Hunar_DBEntities _db = new Hunar_DBEntities();
        //    try
        //    {
        //        var items = new SelectList(_db.JobEmployed_Master, "ID", "JobInterest").OrderBy(x => Convert.ToInt32(x.Value)).ToList();
        //        if (IsAll)
        //        {
        //            items.Insert(0, new SelectListItem { Value = "0", Text = "All" });
        //        }
        //        return items;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    //List<SelectListItem> list = new List<SelectListItem>();
        //    //list.Add(new SelectListItem { Value = "", Text = "Select" });
        //    //list.Add(new SelectListItem { Value = "1", Text = "Didn't get selected in interviews" });
        //    //list.Add(new SelectListItem { Value = "2", Text = "Not interested to work" });
        //    //list.Add(new SelectListItem { Value = "3", Text = "Other" });
        //    //return list.OrderByDescending(x => x.Text).ToList();
        //}

        //private static List<T> ConvertDataTable<T>(DataTable dt)
        //{
        //    List<T> data = new List<T>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        T item = GetItem<T>(row);
        //        data.Add(item);
        //    }
        //    return data;
        //}
        //private static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}

        ////public static List<SelectListItem> GetState()
        ////{
        ////    try
        ////    {
        ////        var dis = new SelectList(dbe.MST_State, "StateId", "StateName").ToList();
        ////        return dis.OrderBy(x => x.Text).ToList();
        ////    }
        ////    catch (Exception)
        ////    {
        ////        //DO To
        ////        throw;
        ////    }
        ////}
        ////public static List<SelectListItem> GetDistrict(int StateId)
        ////{
        ////    try
        ////    {
        ////        if (StateId != 0)
        ////        {
        ////            var dis = new SelectList(dbe.MST_District.Where(x => (x.IDState == StateId)), "DistrictId", "DistrictName").ToList();
        ////            return dis.OrderBy(x => x.Text).ToList();
        ////        }
        ////        else
        ////        {
        ////            var dis = new SelectList(dbe.MST_District, "DistrictId", "DistrictName").ToList();
        ////            return dis.OrderBy(x => x.Text).ToList();
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        //DO To
        ////        throw;
        ////    }
        ////}
        ////public static List<SelectListItem> GetBlock(int? DistrictId)
        ////{
        ////    try
        ////    {
        ////        if (DistrictId != 0)
        ////        {
        ////            var dis = new SelectList(dbe.MST_Block.Where(x => (x.IDDistrict == DistrictId)), "BlockId", "BlockName").ToList();
        ////            return dis.OrderBy(x => x.Text).ToList();
        ////        }
        ////        else
        ////        {
        ////            var dis = new SelectList(dbe.MST_Block, "BlockId", "BlockName").ToList();
        ////            return dis.OrderBy(x => x.Text).ToList();
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        //DO To
        ////        throw;
        ////    }
        ////}

        ////public static List<LeadModal> GetLeadList(int locationid, string Rolen)
        ////{
        ////    LeadModal LDetail;
        ////    List<LeadModal> list = new List<LeadModal>();
        ////    if (!string.IsNullOrEmpty(Rolen))
        ////    {
        ////        StoredProcedure sp = new StoredProcedure("SP_GetLeadDetails");
        ////        sp.Command.AddParameter("@LocationId", locationid, DbType.Int16);
        ////        sp.Command.AddParameter("@RoleName", Rolen, DbType.String);
        ////        DataTable dt = sp.ExecuteDataSet().Tables[0];
        ////        foreach (DataRow dr in dt.Rows)
        ////        {
        ////            LDetail = new LeadModal()
        ////            {
        ////                LeadID = dr["UserId"].ToString(),
        ////                LeadName = dr["Name"].ToString(),
        ////                LeadEmailID = dr["Email"].ToString(),
        ////                LeadLocationID = dr["LocationID"].ToString(),
        ////                LeadLocation = dr["Locaton"].ToString(),
        ////                LeadRoleID = dr["RoleId"].ToString(),
        ////                LeadRoleName = dr["RoleName"].ToString()
        ////            };
        ////            list.Add(LDetail);
        ////        }
        ////        return list.ToList();
        ////    }
        ////    return list;

        ////}

        #endregion

        #region Document Upload
        public static string GetFilePath(HttpPostedFileBase file, string Module, string RegNo, string Ques_fk, string Folder)
        {
            var url = string.Empty;
            try
            {
                string file_extension = Path.GetExtension(file.FileName).ToLower();
                Stream strmStream = file.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    url = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                    string extension = Path.GetExtension(file.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(url)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(url));
                    }
                    int index = 1;
                    string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                    string fname = filenamewithoutext + extension;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(url, fname))))
                    {
                        index++;
                        fname = file.FileName + "(" + index.ToString() + ")" + extension;
                    }
                    //url = HttpContext.Current.Server.MapPath(url + Ques_fk + "_" + fname);
                    url = url + Ques_fk + "_" + fname;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return url;
        }
        public static string SaveFileDynamically(HttpPostedFileBase[] files, string Module, string RegNo, string Ques_fk, string Folder)
        {
            string URL = "";
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string file_extension = Path.GetExtension(file.FileName).ToLower();
                        Stream strmStream = file.InputStream;
                        if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                        {
                            URL = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                            string extension = Path.GetExtension(file.FileName);

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                            }
                            int index = 1;
                            string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                            string fname = filenamewithoutext + extension;
                            while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                            {
                                index++;
                                fname = file.FileName + "(" + index.ToString() + ")" + extension;
                            }
                            file.SaveAs(HttpContext.Current.Server.MapPath(URL + Ques_fk + "_" + fname));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return URL;
        }
        public static string SaveFile(HttpPostedFileBase[] files, string Module, string RegNo, string Folder)
        {
            string URL = "";
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string file_extension = Path.GetExtension(file.FileName).ToLower();
                        Stream strmStream = file.InputStream;
                        if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                        {
                            //URL = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";
                            URL = "~/Uploads/" + RegNo + " / ";
                            string extension = Path.GetExtension(file.FileName);

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                            }
                            int index = 1;
                            string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                            string fname = filenamewithoutext + extension;
                            while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                            {
                                index++;
                                fname = file.FileName + "(" + index.ToString() + ")" + extension;
                            }
                            file.SaveAs(HttpContext.Current.Server.MapPath(URL + fname));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return URL;
        }
        public static string SaveSingleFile(HttpPostedFileBase files, string Module, string RegNo)
        {
            string URL = "";
            string filepath = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".doc" || file_extension == ".dotx" || file_extension == ".dot" || file_extension == ".pptx" || file_extension == ".ppt" || file_extension == ".rar" || file_extension == ".zip" || file_extension == ".xls" || file_extension == ".xlsx")
                {
                    //URL = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + Module + "/" + RegNo + "/"));
                    URL = "~/Uploads/" + RegNo + "/";
                    string extension = Path.GetExtension(files.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                    }
                    int index = 1;
                    string fname = files.FileName;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                    {
                        index++;
                        fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    }
                    files.SaveAs(HttpContext.Current.Server.MapPath(URL + (Module + "-" + fname)));
                    filepath = URL + (Module + "-" + fname);
                }
            }

            return filepath;
        }
        public static string SaveSingleExcelFile(HttpPostedFileBase files, string Module, string RegNo)
        {
            string URL = "";
            string filepath = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".xls" || file_extension == ".xlsx")
                {
                    //URL = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + Module + "/" + RegNo + "/"));
                    URL = "~/Uploads/" + RegNo + "/";
                    string extension = Path.GetExtension(files.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                    }
                    int index = 1;
                    string fname = files.FileName;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                    {
                        index++;
                        fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    }
                    files.SaveAs(HttpContext.Current.Server.MapPath(URL + (Module + "-" + fname)));
                    filepath = URL + (Module + "-" + fname);
                }
            }

            return filepath;
        }
        public static string SaveFileImage(HttpPostedFileBase[] files, string ID, string Folder)
        {
            string URL = "";
            string st = "";
            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string file_extension = Path.GetExtension(file.FileName).ToLower();
                        Stream strmStream = file.InputStream;
                        if (IsValidImage(strmStream) == true || file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                        {
                            //URL = "~/Uploads/" + Module + "/" + RegNo + "/" + Folder + "/";

                            URL = "~/ImageUploads/" + Folder+"_" + ID + "/";
                            string extension = Path.GetExtension(file.FileName);

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                            }
                            int index = 1;
                            string filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                            string fname = filenamewithoutext + ID + extension;
                            while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                            {
                                index++;
                                fname = filenamewithoutext + ID + "(" + index.ToString() + ")" + extension;
                            }
                            file.SaveAs(HttpContext.Current.Server.MapPath(URL + fname));
                            st +=!string.IsNullOrWhiteSpace(st)? "," + URL + fname: URL + fname;
                        }
                        URL = st;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return URL;
        }
        public static string DeleteSingleFile(HttpPostedFileBase files, string Module, string RegNo)
        {
            //ToDo: Add code to delete single file from directory
            string URL = "";
            string filepath = string.Empty;



            return filepath;
        }

        public static string SaveSingleFile(HttpPostedFileBase files, string Module, string RegNo, string CustomFileName)
        {
            string URL = "";
            string filepath = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    //URL = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + Module + "/" + RegNo + "/"));
                    URL = "~/Uploads/" + RegNo + "/";
                    string extension = Path.GetExtension(files.FileName);

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(URL)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(URL));
                    }
                    int index = 1;
                    string fname = CustomFileName + extension; // files.FileName;
                    while (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(URL, fname))))
                    {
                        index++;
                        fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    }
                    files.SaveAs(HttpContext.Current.Server.MapPath(URL + (Module + "-" + fname)));
                    filepath = URL + (Module + "-" + fname);
                }
            }

            return filepath;
        }
        public static string GetFileName(HttpPostedFileBase files)
        {
            string filename = string.Empty;

            if (files != null && files.ContentLength > 0)
            {
                string file_extension = Path.GetExtension(files.FileName).ToLower();
                string filenamewithoutext = Path.GetFileNameWithoutExtension(files.FileName);
                Stream strmStream = files.InputStream;
                if (IsValidImage(strmStream) == true || file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".pptx")
                {
                    string extension = Path.GetExtension(files.FileName);
                    int index = 1;
                    string fname = files.FileName;
                    fname = filenamewithoutext + "(" + index.ToString() + ")" + extension;
                    filename = fname;
                }
            }
            return filename;
        }
        public static bool IsValidImage(Stream stream)
        {
            try
            {
                //Read an image from the stream...
                var i = Image.FromStream(stream);
                //Move the pointer back to the beginning of the stream
                stream.Seek(0, SeekOrigin.Begin);

                if (ImageFormat.Jpeg.Equals(i.RawFormat))
                    return true;
                return ImageFormat.Png.Equals(i.RawFormat) || ImageFormat.Gif.Equals(i.RawFormat);
            }
            catch
            {
                return false;
            }
        }
        //public static Binary BinarySaveSingleFile(HttpPostedFileBase files)
        //{
        //    byte[] Databytes;
        //    //try
        //    //{
        //    string empFilename = Path.GetFileName(files.FileName);
        //    string FilecontentType = files.ContentType;
        //    using (var reader = new BinaryReader(files.InputStream))
        //    {
        //        Databytes = reader.ReadBytes(files.ContentLength);
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    string error = ex.Message;
        //    //}
        //    return Databytes;
        //}
        public static string GetFileExt(string filename)
        {
            string myFilePath = filename;
            string ext = Path.GetExtension(myFilePath);
            return ext;
        }
        #endregion

        #region Monthly,Year
        public static List<SelectListItem> GetFunMonthList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i <= 11; i++)
            {
                list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return list.ToList();
        }
        public static List<SelectListItem> GetFunYearList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DateTime dt = DateTime.Now;
            for (int i = 0; i <= 19; i++) //ToDo: Sunil - Change to 17 years after all backlog entry
            {
                // dt = dt.AddYears(-1);
                var year = i; //dt.Year;
                list.Add(new SelectListItem { Value = year.ToString(), Text = year.ToString() });
            }
            return list.ToList();
        }

        public static List<SelectListItem> GetScreeningType(bool IsAll = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "Camp", Text = "Camp" });
            list.Add(new SelectListItem { Value = "Home", Text = "Home" });
            if (IsAll)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }
            else if (!IsAll)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            return list.ToList();
        }
        public static List<SelectListItem> GetDistrictBlockType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "District" });
            list.Add(new SelectListItem { Value = "2", Text = "Block" });
            return list.ToList();
        }
        public static List<SelectListItem> GetRoundType(bool IsAll = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "Act-I" });
            list.Add(new SelectListItem { Value = "2", Text = "Act-II" });
            //if (IsAll)
            //{
            //    list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            //}
            //else if (!IsAll)
            //{
            //    list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            //}
            return list.ToList();
        }
        #endregion

        #region Date Formats Functions
        public static DateTime FormateDt(DateTime date)
        {
            return date.Date;
        }
        public static string FormateDtMDY(string date)
        {
            string dt = "";
            if (!string.IsNullOrEmpty(date))
            {
                dt = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
            }
            return dt;
        }
        public static string FormateDtDMY(string date)
        {
            string dt = "";
            if (!string.IsNullOrEmpty(date))
            {
                dt = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            }
            return dt;
        }
        #endregion
        public static decimal ToDecimal(string str)
        {
            return Decimal.TryParse(str, out decimal res) ? res : 0M;
        }
        public class RoleNameCont
        {
            public const string Admin = "Admin";
            public const string Viewer = "Viewer";
            public const string User = "User";
            public const string District = "District";
            public const string State = "State";
        }

    }
}