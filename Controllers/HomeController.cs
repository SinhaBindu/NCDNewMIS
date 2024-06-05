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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}