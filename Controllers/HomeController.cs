using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NCDNewMIS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCDNewMIS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult GetDataRowList()
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                ds = SP_Model.SP_RowDataShow();
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = ds.Tables[0];
                    IsCheck = true;
                    html = ConvertViewToString("_RowData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult Blockmap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Blockmap(string Type, string Fromdt, string Todt)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.Type = Type; filterModel.FormDt = Fromdt; filterModel.ToDt = Todt;
            ds = SP_Model.SP_BlockMapSubmission(filterModel);
            //try
            //{
            //    if (ds.Tables.Count > 0)
            //    {
            //        dt = ds.Tables[0];
            //        return PartialView("_DashboardmapBlock", dt);
            //    }
            //    else
            //    {
            //        return PartialView("_DashboardmapBlock", dt);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string er = ex.Message;
            //    //Danger("Something went wrong! Please try again...", true);
            //    return View("Error");
            //}
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_DashboardmapBlock", dt);
                    var Dthtml = ConvertViewToString("_BlockData", dt);
                    //var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Datahtml = html, DataT = Dthtml }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        public ActionResult Districtmap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Districtmap(string DistrictBlockType, string RoundType, string SType)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = DistrictBlockType; filterModel.RoundType = RoundType; filterModel.SType = SType;
            ds = SP_Model.SP_IndicatorData(filterModel);
            ViewBag.DistrictBlockType = DistrictBlockType;
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_DashboardmapDistrict", dt);
                    var Dthtml = ConvertViewToString("_DistrictBlockIndicatorData", dt);
                    //var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Datahtml = html, DataT = Dthtml }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        public ActionResult RegApprove()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegApprove(FilterModel m)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = SP_Model.SP_RegApproved(m);
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_RegApproveData", dt);
                    //var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Datahtml = html }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        [HttpPost]
        public ActionResult PostAppoved(int RegId, int RegMapId, string IsApproved)
        {
            NCD_DBEntities db_ = new NCD_DBEntities();
            var tbl = db_.tbl_Register.Find(RegId);
            tbl.IsApproved = Convert.ToBoolean(IsApproved) == true ? false : true;
            var strApproved = tbl.IsApproved == true ? "Active" : "IsActive";
            db_.SaveChanges();
            var res = Json(new { IsSuccess = true, Message = strApproved + " Successfully.." }, JsonRequestBehavior.AllowGet);
            res.MaxJsonLength = int.MaxValue;
            return res;
        }

        #region Master Bind
        public ActionResult GetBlockList(int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetBlock(SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCHCList(int BlockId, int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetCHC(BlockId, SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPHCList(int BlockId, int CHCId, int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetPHC(BlockId, CHCId, SelectAll);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        public ActionResult SubmissionData()
        {
            return View();
        }
        public ActionResult GetSubmissionData(string DistrictBlockType, string RoundType, string SType)
        {
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = DistrictBlockType; filterModel.RoundType = RoundType; filterModel.SType = SType;
            try
            {
                ds = SP_Model.SP_DistrictBlockData(filterModel);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ViewBag.ST = !string.IsNullOrWhiteSpace(filterModel.SType) ?
                        CommonModel.GetScreeningType()?
                        .FirstOrDefault(x => x.Value == filterModel.SType && !string.IsNullOrWhiteSpace(filterModel.SType)).Text : null;
                    var resdt = JsonConvert.SerializeObject(dt);
                    var Dthtml = ConvertViewToString("_DistBlockSubData", dt);
                    if (dt.Rows.Count > 0)
                    {
                        var res = Json(new
                        {
                            IsSuccess = true,
                            resData = resdt,
                            reshtml = Dthtml
                        }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;

                    }
                    else
                    {
                        var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;
                    }
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult HighData()
        {
            return View();
        }
        public ActionResult GetHighData(string SType)
        {
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.SType = SType;
            try
            {
                ds = SP_Model.SP_ACT2High(filterModel);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];
                    var resdtHY = JsonConvert.SerializeObject(dt);
                    var resdtBS = JsonConvert.SerializeObject(dt1);
                    var HYThtml = ConvertViewToString("_HYTData", dt);
                    var RBShtml = ConvertViewToString("_RBSData", dt1);
                    if (dt.Rows.Count > 0)
                    {
                        var res = Json(new
                        {
                            IsSuccess = true,
                            HYData = resdtHY,
                            BSData = resdtBS,
                            HYreshtml = HYThtml,
                            BSreshtml = RBShtml
                        }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;

                    }
                    else
                    {
                        var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;
                    }
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

    }
}