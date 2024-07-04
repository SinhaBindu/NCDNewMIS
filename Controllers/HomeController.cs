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
        public ActionResult Financial()
        {
            return View();
        }
        public ActionResult GetDataRowList(string SType)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            FilterModel filterModel = new FilterModel();
            filterModel.SType = SType;
            try
            {
                ds = SP_Model.SP_RowDataShow(filterModel);
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
            ViewBag.RoundType = RoundType;
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
                    foreach (var s in splt.ToList())
                    {
                        //foreach (var item in tblmaplist.ToList())
                        //{
                        var scid = Convert.ToInt32((s).ToString());
                        tblmap = new tbl_RegMappping();
                        tblmap = tblmaplist.Any(x => x.SubCenterId_fk == scid)==false?new tbl_RegMappping():null;

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
                            }
                            else if (itemmapid != 0)
                            {
                                //tblmap.SubCenterId_fk = Convert.ToInt32(s);
                                //tblmap.UpdatedBy = User.Identity.Name;
                                //tblmap.UpdatedOn = DateTime.Now;
                            }
                        }
                    }
                    res = db_.SaveChanges();
                    //}
                }
                if (res > 0)
                {
                    var resjsont = Json(new { IsSuccess = true, Message = "SubCenter Mapped Successfully.." }, JsonRequestBehavior.AllowGet);
                    resjsont.MaxJsonLength = int.MaxValue;
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

    }
}