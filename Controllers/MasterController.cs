using Microsoft.AspNetCore.Cors;
using NCDNewMIS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NCDNewMIS.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [EnableCors("*")]
        public async Task<string> LoginPost(LoginModel model)
        {
            var accessToken = "";// HttpContext.Request.Headers["Authorization"];
            NCD_DBEntities db_ = new NCD_DBEntities();
            //var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            accessToken = "";
            string strtoken = ""; //Bearer ALaPRfBMWBRDgurutJcdc4rRDsXmfK6EsI+hWtYTAUYQ/XPWUVbntbRKF8oJbTnpMg==//"ALaPRfBMWBRDgurutJcdc4rRDsXmfK6EsI+hWtYTAUYQ/XPWUVbntbRKF8oJbTnpMg==";
            string strMsg = "";
            var date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return CommonModel.GetEnumDisplayName(Enums.AlterMsg.Required);// Json(CommonModel.GetEnumDisplayName(Enums.AlterMsg.Required), JsonRequestBehavior.AllowGet);
            }
            try
            {
                //if (!string.IsNullOrWhiteSpace(strtoken))
                //{
                    if ((accessToken).ToLower() == (strtoken).ToLower())
                    {
                        DataSet ds = new DataSet();
                        ds = SP_Model.SPLoginCheck1(model);
                        string RetVal = string.Empty;
                        string jsval = string.Empty;
                        string jsval2 = string.Empty;
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                            {
                                jsval = jsval + ds.Tables[i].Rows[j].ItemArray[0].ToString();
                            }
                            jsval = jsval.Remove(0, 1).Split(']')[0];
                            jsval = jsval + "],";
                            jsval2 = jsval2 + jsval;
                            jsval = string.Empty;
                        }
                        jsval = "{" + jsval2.Remove(jsval2.Length - 1, 1) + "}";

                        //if (dt.Rows.Count > 0)
                        //{
                        //    tbl_Login tbl = new tbl_Login();
                        //    tbl.RegMapId_fk = Convert.ToInt32(dt.Rows[0]["RegMapId_pk"].ToString());
                        //    tbl.RegId_fk =Convert.ToInt32(dt.Rows[0]["RegId_pk"].ToString());
                        //    tbl.UserName = model.UserName;
                        //    tbl.Password = model.Password;
                        //    tbl.Version = model.Version;
                        //    tbl.IsActive = true;
                        //    tbl.CreatedBy = dt.Rows[0]["RegMapId_pk"].ToString();  
                        //    tbl.CreatedOn= DateTime.Now;
                        //    db_.tbl_Login.Add(tbl);
                        //    int res = await db_.SaveChangesAsync();
                        //    if (res > 0)
                        //    {
                        //        strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.Loginsuccess);
                        //        return Json(strMsg, JsonRequestBehavior.AllowGet);
                        //    }
                        //}
                        //else
                        //{
                        //    strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.Loginfailed);
                        //    return Json(strMsg, JsonRequestBehavior.AllowGet);
                        //}
                        // strMsg = jsval;
                        return jsval;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.SecurityToken);
                        return strMsg;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                    }
                //}
                //else
                //{
                //    strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.SecurityTokenNot);
                //    return strMsg;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;// Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [EnableCors("*")]
        public async Task<string> JsonPostData(PostDataModel model)
        {
            var accessToken = "";// HttpContext.Request.Headers["Authorization"];
            NCD_DBEntities db_ = new NCD_DBEntities();
            //var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            accessToken = "";
            string strtoken = ""; //Bearer ALaPRfBMWBRDgurutJcdc4rRDsXmfK6EsI+hWtYTAUYQ/XPWUVbntbRKF8oJbTnpMg==//"ALaPRfBMWBRDgurutJcdc4rRDsXmfK6EsI+hWtYTAUYQ/XPWUVbntbRKF8oJbTnpMg==";
            string strMsg = "";
            var date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return CommonModel.GetEnumDisplayName(Enums.AlterMsg.Required);// Json(CommonModel.GetEnumDisplayName(Enums.AlterMsg.Required), JsonRequestBehavior.AllowGet);
            }
            try
            {
                //if (!string.IsNullOrWhiteSpace(strtoken))
                //{
                if ((accessToken).ToLower() == (strtoken).ToLower())
                {
                    DataSet ds = new DataSet();
                    ds = SP_Model.SP_JsonPostData(model);
                    string RetVal = string.Empty;
                    string jsval = string.Empty;
                    string jsval2 = string.Empty;
                    //for (int i = 0; i < ds.Tables.Count; i++)
                    //{
                    //    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    //    {
                    //        jsval = jsval + ds.Tables[i].Rows[j].ItemArray[0].ToString();
                    //    }
                    //    jsval = jsval.Remove(0, 1).Split(']')[0];
                    //    jsval = jsval + "],";
                    //    jsval2 = jsval2 + jsval;
                    //    jsval = string.Empty;
                    //}
                    //jsval = "{" + jsval2.Remove(jsval2.Length - 1, 1) + "}";
                    jsval = "Success";
                    return jsval;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.SecurityToken);
                    return strMsg;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                }
                //}
                //else
                //{
                //    strMsg = CommonModel.GetEnumDisplayName(Enums.AlterMsg.SecurityTokenNot);
                //    return strMsg;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;// Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


    }
}