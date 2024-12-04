using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NCDNewMIS.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using ClosedXML;
using ClosedXML.Excel;
using static NCDNewMIS.Controllers.ResourceController;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO.Compression;
using Ionic.Zip;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;

namespace NCDNewMIS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
        //[AllowAnonymous]
        public ActionResult Home2()
        {
            return View();
        }
        public ActionResult Districtmap(string D, string R)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = D; filterModel.RoundType = R;
            return View(filterModel);
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
            ViewBag.RoundType = RoundType;
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_DashboardmapDistrict", dt);
                    var Dthtml = ConvertViewToString("_DistrictIndicatorData", dt);
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

        public ActionResult Blockmap(string B, string R)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = B; filterModel.RoundType = R;
            return View(filterModel);
        }
        [HttpPost]
        public ActionResult Blockmap(string DistrictBlockType, string RoundType, string SType)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = DistrictBlockType; filterModel.RoundType = RoundType; filterModel.SType = SType;
            ds = SP_Model.SP_IndicatorData(filterModel);
            ViewBag.DistrictBlockType = DistrictBlockType;
            ViewBag.RoundType = RoundType;
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_DashboardmapDistrict", dt);
                    var Dthtml = ConvertViewToString("_BlockIndicatorData", dt);
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
        public ActionResult GalleryOne()
        {
            FilterModel model = new FilterModel();
            model.RoundType = "1";
            DataTable dt = new DataTable();
            dt = SP_Model.SP_GetImageUploadList(model);
            return View(dt);
        }
        public ActionResult GalleryTwo()
        {
            FilterModel model = new FilterModel();
            model.RoundType = "2";
            DataTable dt = new DataTable();
            dt = SP_Model.SP_GetImageUploadList(model);
            return View(dt);
        }
        public ActionResult Financial()
        {
            return View();
        }
        public ActionResult Financial2()
        {
            return View();
        }
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SP_Model.SP_RawDataSummary();
                if (ds.Tables.Count > 0)
                {
                    return View(ds);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View();
        }
        public ActionResult GetDataRowList(string BlockId, string SType, string FD, string TD)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            FilterModel filterModel = new FilterModel();
            filterModel.SType = SType;
            filterModel.FormDt = FD;
            filterModel.ToDt = TD;
            filterModel.BlockId = BlockId;
            try
            {
                ds = SP_Model.SP_RowDataShow(filterModel);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = ds.Tables[0];
                    if (tbllist.Rows.Count > 0)
                    {
                        IsCheck = true;
                        html = ConvertViewToString("_RowData", tbllist);
                        var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                        res1.MaxJsonLength = int.MaxValue;
                        return res1;
                    }
                    IsCheck = false;
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { IsSuccess = false, Data = "Record Issues." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        //Main RawDataDownload
        public ActionResult ReportRawDataExcel()
        {
            DataSet ds = new DataSet();
            DataTable dt = new System.Data.DataTable();
            FilterModel filterModel = new FilterModel();
            //fill datatable by some data i just use empty databale
            System.Text.StringBuilder htmlstr = new System.Text.StringBuilder();
            ds = SP_Model.SP_AllRawDataShow(filterModel);
            dt = ds.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                var DTDAY = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=RAWSUBMISSIONDATA" + DTDAY + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    // memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return new EmptyResult();

        }
        //FollowUp Download
        public ActionResult ReportFollowUPRawDataExcel()
        {
            DataSet ds = new DataSet();
            DataTable dt = new System.Data.DataTable();
            FilterModel filterModel = new FilterModel();
            //fill datatable by some data i just use empty databale
            System.Text.StringBuilder htmlstr = new System.Text.StringBuilder();
            ds = SP_Model.SP_AllRawDataFollowUpDownload(filterModel);
            dt = ds.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                var DTDAY = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=RAWFOLLOWUPSUBMISSIONDATA" + DTDAY + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    // memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return new EmptyResult();

        }

        //public ActionResult Blockmap()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Blockmap(string Type, string Fromdt, string Todt)
        //{
        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    FilterModel filterModel = new FilterModel();
        //    filterModel.Type = Type; filterModel.FormDt = Fromdt; filterModel.ToDt = Todt;
        //    ds = SP_Model.SP_BlockMapSubmission(filterModel);
        //    //try
        //    //{
        //    //    if (ds.Tables.Count > 0)
        //    //    {
        //    //        dt = ds.Tables[0];
        //    //        return PartialView("_DashboardmapBlock", dt);
        //    //    }
        //    //    else
        //    //    {
        //    //        return PartialView("_DashboardmapBlock", dt);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    string er = ex.Message;
        //    //    //Danger("Something went wrong! Please try again...", true);
        //    //    return View("Error");
        //    //}
        //    try
        //    {
        //        if (ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            var html = ConvertViewToString("_DashboardmapBlock", dt);
        //            var Dthtml = ConvertViewToString("_BlockData", dt);
        //            //var dtjson = JsonConvert.SerializeObject(dt);
        //            var res = Json(new { IsSuccess = true, Datahtml = html, DataT = Dthtml }, JsonRequestBehavior.AllowGet);
        //            res.MaxJsonLength = int.MaxValue;
        //            return res;
        //        }
        //        else
        //        {
        //            var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
        //            res.MaxJsonLength = int.MaxValue;
        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string er = ex.Message;
        //        return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
        //    }
        //}

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
            tbl.UpdatedBy = User.Identity.Name;
            tbl.UpdatedOn = DateTime.Now;
            db_.SaveChanges();
            var res = Json(new { IsSuccess = true, Message = strApproved + " Successfully.." }, JsonRequestBehavior.AllowGet);
            res.MaxJsonLength = int.MaxValue;
            return res;
        }
        public ActionResult RegMapped()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegMapped(FilterModel m)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = SP_Model.SP_RegMapped(m);
            try
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var html = ConvertViewToString("_RegMappedData", dt);
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
        public ActionResult PostRegMapped(int RegId, string RegMapIds, string MobileNo, int DistrictId, int BlockId, string SubCenterIds)
        {
            try
            {
                NCD_DBEntities db_ = new NCD_DBEntities();

                int res = 0;
                //tbl.IsApproved = Convert.ToBoolean(IsApproved) == true ? false : true;
                //var strApproved = tbl.IsApproved == true ? "Active" : "IsActive";
                var tblmap = new tbl_RegMappping();
                if (!string.IsNullOrWhiteSpace(SubCenterIds))
                {
                    var splt = SubCenterIds.Split(',');
                    var spltmapp = RegMapIds.Split(',');
                    IEnumerable<int> spltsubcenterids = splt.Select(int.Parse);
                    IEnumerable<int> spltmappids = spltmapp.Select(int.Parse);
                    //List<int> listOfIds = new List<int>(Convert.ToInt32(spltmappids));
                    //List<int> listOfsubpkIds = new List<int>(spltsubcenterids);
                    var tblmaplist = db_.tbl_RegMappping.Where(r => spltmappids.Contains(r.RegMapId_pk) && spltsubcenterids.Contains(r.SubCenterId_fk.Value)).ToList();
                    var spltmappsubids = db_.tbl_RegMappping.Where(x => x.RegId_fk == RegId).ToList();
                    var tblmapUPStatus = spltmappsubids.ToList();
                    tblmapUPStatus.ForEach(a => a.IsActive = 0);
                    db_.SaveChanges();
                    foreach (var s in splt.ToList())
                    {
                        //foreach (var item in tblmaplist.ToList())
                        //{
                        var scid = Convert.ToInt32((s).ToString());
                        tblmap = new tbl_RegMappping();
                        tblmap = tblmaplist.Any(x => x.SubCenterId_fk == scid) == false ? new tbl_RegMappping() : tblmaplist.FirstOrDefault(x => x.SubCenterId_fk == scid);

                        FilterModel model = new FilterModel();
                        model.DistrictId = Convert.ToString(DistrictId);
                        model.BlockId = Convert.ToString(BlockId);
                        model.SCId = s;
                        DataSet ds = SP_Model.SP_GetPHCCHC_SubCenterWise(model);
                        DataTable dtchcphc = ds.Tables[0];

                        var itemmapid = 0;//item.RegMapId_pk!string.IsNullOrWhiteSpace(item.RegMapId_pk) ? Convert.ToInt32(item) : 0;

                        //tblmap = itemmapid != 0 && spltmapp != null ? db_.tbl_RegMappping
                        //    .Where(x => x.RegId_fk == RegId && x.RegMapId_pk == itemmapid && x.SubCenterId_fk == scid)?.FirstOrDefault()
                        //    : new tbl_RegMappping();
                        //tblmap = tblmap == null ? new tbl_RegMappping() : tblmap;
                        if (tblmap != null)
                        {
                            if (tblmap.RegMapId_pk == 0 && !db_.tbl_RegMappping
                            .Any(x => x.RegId_fk == RegId && x.SubCenterId_fk == scid))
                            {
                                tblmap.RegId_fk = RegId;
                                tblmap.MapDate = DateTime.Now.Date;
                                tblmap.DistrictId_fk = DistrictId;
                                tblmap.BlockId_fk = BlockId;
                                tblmap.CHCId_fk = Convert.ToInt32(dtchcphc.Rows[0]["CHCId"]);
                                tblmap.PHCId_fk = Convert.ToInt32(dtchcphc.Rows[0]["PHCId"]);
                                tblmap.SubCenterId_fk = Convert.ToInt32(s);
                                tblmap.Version = "Portal Mapped";
                                tblmap.IsActive = 1;
                                tblmap.CreatedBy = User.Identity.Name;
                                tblmap.CreatedOn = DateTime.Now;
                                db_.tbl_RegMappping.Add(tblmap);
                                res += db_.SaveChanges();
                            }
                            else if (tblmap.RegMapId_pk != 0)
                            {
                                //tblmap.SubCenterId_fk = Convert.ToInt32(s);
                                tblmap.IsActive = 1;
                                tblmap.UpdatedBy = User.Identity.Name;
                                tblmap.UpdatedOn = DateTime.Now;
                                res += db_.SaveChanges();
                            }
                        }
                    }
                    //res = db_.SaveChanges();
                    //}
                }
                if (res > 0)
                {
                    var resjsont1 = Json(new { IsSuccess = true, Message = "SubCenter Mapped Successfully.." }, JsonRequestBehavior.AllowGet);
                    resjsont1.MaxJsonLength = int.MaxValue;
                    return resjsont1;
                }
                var resjsonf = Json(new { IsSuccess = false, Message = "SubCenter Not Mapped" }, JsonRequestBehavior.AllowGet);
                return resjsonf;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                var resjson = Json(new { IsSuccess = false, Message = "No Mapped SubCenter.." }, JsonRequestBehavior.AllowGet);
                resjson.MaxJsonLength = int.MaxValue;
                return resjson;
            }
        }

        #region Master Bind
        public ActionResult GetPageTypeList(int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetACT1PageTypeSome(SelectAll);
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
        public ActionResult GetIndicatorTypeList(int SelectAll = 1, string PageType = "")
        {
            try
            {
                FilterModel filterModel = new FilterModel();
                filterModel.PageType = PageType;
                var items = CommonModel.GetACT1IndicatorSome(SelectAll, filterModel);
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
        public ActionResult GetDistrictList(int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetDistrict(SelectAll);
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
        public ActionResult GetBlockList(int SelectAll = 1, int RoundType = 2)
        {
            try
            {
                var items = CommonModel.GetBlock(SelectAll, RoundType);
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
        public ActionResult GetSubCenterList(int DistrictId = 0, int BlockId = 0, int CHCId = 0, int PHCId = 0, int SelectAll = 1)
        {
            try
            {
                var items = CommonModel.GetSP_GetSubCenterCenterLists(SelectAll, BlockId, CHCId, PHCId);
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
        public ActionResult GetSubmissionData(string DistrictBlockType, string RoundType, string SType, string StartDate, string EndDate)
        {
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.DistrictBlockType = DistrictBlockType; filterModel.RoundType = RoundType; filterModel.SType = SType;
            filterModel.FormDt = StartDate; filterModel.ToDt = EndDate;
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult PendingMembers(int BId = 0)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.BlockId = BId.ToString();
            return View(filterModel);
        }
        public ActionResult GetPendingMembersData(FilterModel filterModel)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SP_Model.SP_PendingUserMembersSubmission(filterModel);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    var resdt = JsonConvert.SerializeObject(dt);
                    var Dthtml = ConvertViewToString("_PendingUserMembersData", dt);
                    if (dt.Rows.Count > 0)
                    {
                        var res = Json(new
                        {
                            IsSuccess = true,
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
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
                    DataTable dt2 = ds.Tables[2];
                    var resdtHY = JsonConvert.SerializeObject(dt);
                    var resdtBS = JsonConvert.SerializeObject(dt1);
                    var resdtHYBS = JsonConvert.SerializeObject(dt2);
                    var HYThtml = ConvertViewToString("_HYTData", dt);
                    var RBShtml = ConvertViewToString("_RBSData", dt1);
                    var HYTToRBShtml = ConvertViewToString("_HYTToRBSData", dt2);
                    if (dt.Rows.Count > 0)
                    {
                        var res = Json(new
                        {
                            IsSuccess = true,
                            HYData = resdtHY,
                            BSData = resdtBS,
                            HYToBSData = resdtHYBS,
                            HYreshtml = HYThtml,
                            BSreshtml = RBShtml,
                            HYToBSreshtml = HYTToRBShtml
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

        public ActionResult ACTOneIndicator()
        {
            return View();
        }
        public ActionResult GetSomeACT1IndicatorData(string PageType, string IndicatorId)
        {
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = PageType; filterModel.IndicatorId = IndicatorId;
            try
            {
                ds = SP_Model.SP_ACT1Indicator(filterModel);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    var resdt = JsonConvert.SerializeObject(dt);
                    var Dthtml = ConvertViewToString("_ACTOneIndicationData", dt);
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
        public ActionResult ImageUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImageUpload(ImageUploadModel model)
        {
            NCD_DBEntities db_ = new NCD_DBEntities();
            int result = 0;
            try
            {
                if (model != null && Request.Files.Count > 0 && model.FileUpload.Count > 0)
                {
                    if (ModelState.IsValid)
                    {
                        //if (db_.tbl_GalleryUpload.Any(x => x.Date == model.Date && x.BlockId == model.BlockId))
                        //{
                        //    var res1 = Json(new { IsSuccess = false, Message = "Date and Block Already Exists !!", JsonRequestBehavior.AllowGet });
                        //    res1.MaxJsonLength = int.MaxValue;
                        //    return res1;
                        //}
                        var maxid = db_.tbl_GalleryUpload.Count() != 0 ? db_.tbl_GalleryUpload?.Max(x => x.Id) : 0;
                        var lastaddmaxid = maxid == 0 ? 1 : maxid + 1;
                        var getblock = db_.Block_Master.Find(model.BlockId);
                        var blockname = "";
                        if (getblock == null)
                        {
                            var dadt = SP_Model.SP_ACT1Block();
                            var results = dadt.Rows.Cast<DataRow>()
                               .FirstOrDefault(x => x.Field<int>("BlockId") == model.BlockId);
                            DataRow getact1 = SP_Model.SP_ACT1Block().AsEnumerable().Where(x => x.Field<int>("BlockId") == model.BlockId)?.FirstOrDefault();
                            var d = getact1["Block"].ToString();
                            blockname = d;
                        }
                        else
                        {
                            blockname = getblock.Block;
                        }
                        var filePath = CommonModel.SaveFileImage(model.FileUpload.ToArray(), lastaddmaxid.ToString(), blockname + model.BlockId.ToString());
                        tbl_GalleryUpload tbl = new tbl_GalleryUpload();
                        tbl.Date = model.Date;
                        tbl.DistrictId = 1;
                        tbl.RoundType = model.RoundType;
                        tbl.BlockId = model.BlockId;
                        tbl.Title = model.Title;
                        tbl.FilePath = filePath;
                        tbl.Description = model.Description;
                        tbl.CreatedBy = User.Identity.Name;
                        tbl.CreatedOn = DateTime.Now;
                        tbl.IsActive = true;
                        db_.tbl_GalleryUpload.Add(tbl);
                        result = db_.SaveChanges();
                    }
                }
                var res = result > 0 ? Json(new { IsSuccess = true, Message = "Record Submitted Successfully.." }, JsonRequestBehavior.AllowGet)
                       : Json(new { IsSuccess = false, Message = "Record Not Submitted.." }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                var res = Json(new { IsSuccess = false, Message = "Record Not Submitted.." }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
        }
        public ActionResult ImageUploadList()
        {
            FilterModel m = new FilterModel();
            return View(m);
        }
        [HttpGet]
        public ActionResult GetImageUpload(string BlockId = "0", string RoundType = "0")
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            FilterModel m = new FilterModel();
            m.BlockId = BlockId == "" ? "0" : BlockId;
            m.RoundType = RoundType == "" ? "0" : RoundType;
            dt = SP_Model.SP_GetImageUploadList(m);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    var html = ConvertViewToString("_ImageData", dt);
                    var res = Json(new { IsSuccess = true, Data = html }, JsonRequestBehavior.AllowGet);
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

        [AllowAnonymous]
        public ActionResult DemoIndia()
        {
            return View();
        }

        public ActionResult SubmissionSummary()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmissionSummary(string FD, string TD)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            FilterModel filterModel = new FilterModel();
            filterModel.FormDt = FD; filterModel.ToDt = FD;
            dt = SP_Model.SP_SummaryUserSubmission(filterModel);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    var html = ConvertViewToString("_SubmissionSummaryData", dt);
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

        #region Act-I Indicator Method
        public ActionResult Training()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Training(string ptype)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = ptype;
            DataTable dt = new DataTable();
            try
            {
                dt = SP_Model.GetHealthEducation(filterModel);
                if (dt.Rows.Count > 0)
                {
                    var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dtjson }, JsonRequestBehavior.AllowGet);
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
        public ActionResult HealthEducation()
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = "health_education";
            DataTable dt = new DataTable();
            try
            {
                dt = SP_Model.GetHealthEducation(filterModel);
            }
            catch (Exception ex)
            {

            }
            return View(dt);
        }
        public ActionResult Screening()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Screening(string ptype)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = ptype;
            DataSet dt = new DataSet();
            try
            {
                dt = SP_Model.GetHealthScreeing(filterModel);
                if (dt.Tables.Count > 0)
                {
                    var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dtjson }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Screening2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Screening2(string ptype)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = ptype;
            DataSet dt = new DataSet();
            try
            {
                dt = SP_Model.GetHealthScreeing2(filterModel);
                if (dt.Tables.Count > 0)
                {
                    var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dtjson }, JsonRequestBehavior.AllowGet);
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
        #endregion
        public ActionResult FinUtilization()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FinUtilization(string ptype, string fyear)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.RoundType = ptype;
            filterModel.PageType = fyear;
            DataTable dt = new DataTable();
            try
            {
                dt = SP_Model.GetFinUtilization(filterModel);
                if (dt.Rows.Count > 0)
                {
                    var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dtjson }, JsonRequestBehavior.AllowGet);
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

        #region Act-II Indicator Method
        public ActionResult Training2()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Training2(string ptype)
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = ptype;
            DataTable dt = new DataTable();
            try
            {
                dt = SP_Model.SP_ACT2Indicator_Health(filterModel);
                if (dt.Rows.Count > 0)
                {
                    var dtjson = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dtjson }, JsonRequestBehavior.AllowGet);
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
        public ActionResult HealthEducation2()
        {
            FilterModel filterModel = new FilterModel();
            filterModel.PageType = "health_education";
            DataTable dt = new DataTable();
            try
            {
                dt = SP_Model.SP_ACT2Indicator_Health(filterModel);
            }
            catch (Exception ex)
            {

            }
            return View(dt);
        }
        public ActionResult RawDataFollowup()
        {
            FilterModel model = new FilterModel();
            model.BlockId = Convert.ToInt16(Enums.Default1stValue.BlockId).ToString();
            return View(model);
        }
        public ActionResult GetDataRawFollowupList(string BlockId, string SType, string FD, string TD)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            FilterModel filterModel = new FilterModel();
            filterModel.SType = SType;
            filterModel.FormDt = FD;
            filterModel.ToDt = TD;
            filterModel.BlockId = BlockId;
            try
            {
                ds = SP_Model.SP_RawDataFollowup(filterModel);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = ds.Tables[0];
                    if (tbllist.Rows.Count > 0)
                    {
                        IsCheck = true;
                        html = ConvertViewToString("_RowDataFollowup", tbllist);
                        var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                        res1.MaxJsonLength = int.MaxValue;
                        return res1;
                    }
                    IsCheck = false;
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult FollowSuspSummary()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SP_Model.Sp_FollowupSuspectedSummary();
            }
            catch (Exception ex)
            {

            }
            return View(ds);
        }
        public ActionResult FollowSuspSummaryData()
        {
            return View();
        }
        public ActionResult GetFollowupDataSummmary(string BlockId, string StartDate, string EndDate, string TypeOfPatient, string GenderId, string Ageyrs)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            try
            {
                ds = SP_Model.Sp_FollowupSuspectedSummaryData(BlockId, StartDate, EndDate, TypeOfPatient, GenderId, Ageyrs);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    var resData = JsonConvert.SerializeObject(ds);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = resData }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult FollowDocumentDownload()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetFollowupImageDoc(FilterModel filtermodel)
        {
            DataTable dt = new DataTable();
            var html = "";
            try
            {
                dt = SP_Model.Usp_FollowupImageDocumentLoad(filtermodel);
                bool IsCheck = false;
                if (dt.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_RowDocumentFollowup", dt);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                IsCheck = false;
                var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult GetDownFollowImgDocZip(string MId = "", string FMId = "")
        {
            DataTable dt = new DataTable();
            dt = SP_Model.Usp_ZipFileFollowDownload(MId, FMId);
            // Define the folder path where images and PDFs are stored
            string folderPath = Server.MapPath("~/ImageUploads/SurveyImages/FollowUp"); // Example path, change as needed
            var random = new Random();
            int month = random.Next(1, 1200);
            // Define the path for the temporary zip file
            string zipPath = Server.MapPath("~/ImageUploads/SurveyImages/CombinedFilesFollowupZip" + month + ".zip");

            // Make sure the TempZips directory exists, if not, create it
            if (!Directory.Exists(Server.MapPath("~/ImageUploads/SurveyImages/TempZips")))
            {
                Directory.CreateDirectory(Server.MapPath("~/ImageUploads/SurveyImages/TempZips"));
            }
            byte[] fileBytes;
            // Create the zip file
            using (ZipArchive zip = System.IO.Compression.ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                // Get all image and PDF files from the folder
                //var files = Directory.GetFiles(folderPath, "*.*");
                // .Where(f => f.EndsWith(".jpg") || f.EndsWith(".jpeg") || f.EndsWith(".png") || f.EndsWith(".pdf") || f.EndsWith(".pdf"));

                foreach (DataRow dr in dt.Rows)
                {
                    string filePath = dr["FilePathFull"].ToString();
                    string fileName = Path.GetFileName(filePath);
                    // Add the file to the zip
                    //var file = Directory.GetFiles(filePath, "*.*");
                    string filePathc = Path.Combine(folderPath, fileName);

                    zip.CreateEntryFromFile(filePathc, fileName);
                }
                // Loop through each file and add it to the zip
                //foreach (var file in files)
                //{
                //    // Get the file name from the full path
                //    string fileName = Path.GetFileName(file);
                //    // Add the file to the zip
                //    zip.CreateEntryFromFile(file, fileName);
                //}
            }

            // Return the zip file as a download
            fileBytes = System.IO.File.ReadAllBytes(zipPath);
            if (System.IO.File.Exists(zipPath))
                System.IO.File.Delete(zipPath);

            return File(fileBytes, "application/zip", "CombinedFilesFollowupZip.zip");
        }
        public ActionResult FollowUpSuspectedSummaryRawDataDownload(string StartDate = "", string EndDate = "")
        {
            DataSet ds = new DataSet();
            DataTable dt = new System.Data.DataTable();
            FilterModel filterModel = new FilterModel();
            //fill datatable by some data i just use empty databale
            System.Text.StringBuilder htmlstr = new System.Text.StringBuilder();
            if (TempData["FUSData"] != null)
            {
                dt = (DataTable)TempData["FUSData"];
            }
            else
            {
                ds = SP_Model.Sp_FollowupSuspectedSummaryInDownload(StartDate, EndDate);
                dt = ds.Tables[0];
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                var DTDAY = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=RawFollowUpVisitedSuspected" + DTDAY + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    // memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return new EmptyResult();

        }
        public ActionResult RawFollowupVisitData()
        {
            return View();
        }
        public ActionResult GetRawFollowupVisitDataList(string StartDate = "", string EndDate = "")
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            TempData["FUSData"] = null;
            var html = "";
            try
            {
                ds = SP_Model.Sp_FollowupSuspectedSummaryInDownload(StartDate, EndDate);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = ds.Tables[0];
                    if (tbllist.Rows.Count > 0)
                    {
                        TempData["FUSData"] = tbllist;
                        IsCheck = true;
                        html = ConvertViewToString("_RawFollowupVisitSuspData", tbllist);
                        var res1 = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                        res1.MaxJsonLength = int.MaxValue;
                        return res1;
                    }
                    IsCheck = false;
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        #endregion
        public ActionResult ReportFollowUPDetailsDataExcel()
        {
            DataSet ds = new DataSet();
            DataTable dt = new System.Data.DataTable();
            System.Text.StringBuilder htmlstr = new System.Text.StringBuilder();
            ds = SP_Model.SP_FollowUpDetailsDownload();
            dt = ds.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                var DTDAY = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FOLLOWUPDETAILSDATA" + DTDAY + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    // memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return new EmptyResult();
        }
    }
}