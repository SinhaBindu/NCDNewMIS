﻿using Microsoft.AspNetCore.Cors;
using NCDNewMIS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public async Task<string> JsonRegLocationData(int flg, int Isvalue, string value)
        {
            var accessToken = "";// HttpContext.Request.Headers["Authorization"];
            NCD_DBEntities db_ = new NCD_DBEntities();
            //var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            RegLocationModel model = new RegLocationModel();
            model.flg = flg;
            model.Isvalue = Isvalue;
            model.value = value;
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
                DataTable dt = new DataTable();
                dt = SP_Model.SP_RegLocation(model);
                string RetVal = string.Empty;
                string jsval = string.Empty;
                string jsval2 = string.Empty;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    jsval = jsval + dt.Rows[j].ItemArray[0].ToString();
                }
                jsval = jsval.Remove(0, 1).Split(']')[0];
                jsval = jsval + "],";
                jsval2 = jsval2 + jsval;
                jsval = string.Empty;

                jsval = "{" + jsval2.Remove(jsval2.Length - 1, 1) + "}";
                return jsval;

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
        public async Task<string> JsonPostData(string UserName, string Password, string Version, string JsonData)
        {
            string filepath = "/NCDRequestData/";
            var accessToken = "";// HttpContext.Request.Headers["Authorization"];
            NCD_DBEntities db_ = new NCD_DBEntities();
            //var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            PostDataModel model = new PostDataModel();
            model.UserName = UserName;
            model.Password = Password;
            model.Version = Version;
            model.JsonData = JsonData;
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
                    DataTable dt = new DataTable();
                    dt = SP_Model.SP_JsonPostData(model);
                    string RetVal = string.Empty;
                    string jsval = string.Empty;
                    string jsval2 = string.Empty;
                    int i = 0;
                    i = Convert.ToInt32(dt.Rows[0][0]);
                    if (i > 0)
                    {
                        jsval = "{\"Table\":[{\"RetValue\":\"Success\"}]}";
                        filepath = filepath + "/Archive/";
                    }
                    else
                    {
                        if (i == -2)
                        {
                            jsval = "{\"Table\":[{\"RetValue\":\"Invalid Json Data\"}]}";
                        }
                        else if (i == -3)
                        {
                            jsval = "{\"Table\":[{\"RetValue\":\"Invalid User\"}]}";
                        }
                        else
                        {
                            jsval = "{\"Table\":[{\"RetValue\":\"Failed\"}]}";
                        }
                        filepath = filepath + "/Error/";
                    }
                    filepath = filepath + UserName + "^" + Version + "^" + Guid.NewGuid() + "^-" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".txt";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath(filepath), false, System.Text.Encoding.UTF8);
                    sw.WriteLine(JsonData);
                    sw.Close();
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
        public async Task<string> JsonPostRegistration(string JsonData)
        {
            var accessToken = "";// HttpContext.Request.Headers["Authorization"];
            NCD_DBEntities db_ = new NCD_DBEntities();
            //var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            PostDataModel model = new PostDataModel();
            model.JsonData = JsonData;
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
                DataTable dt = new DataTable();
                dt = SP_Model.uspRegistration(model);
                string RetVal = string.Empty;
                string jsval = string.Empty;
                string jsval2 = string.Empty;
                int i = 0;
                i = Convert.ToInt32(dt.Rows[0][0]);
                if (i > 0)
                {
                    jsval = "{\"Table\":[{\"RetValue\":\"Success\"}]}";
                }
                else
                {
                    if (i == -2)
                    {
                        jsval = "{\"Table\":[{\"RetValue\":\"Already Mobile No\"}]}";
                    }
                    else if (i == -1)
                    {
                        jsval = "{\"Table\":[{\"RetValue\":\"Already Mobile No\"}]}";
                    }
                    else if (i == -3)
                    {
                        jsval = "{\"Table\":[{\"RetValue\":\"Invalid User\"}]}";
                    }
                    else
                    {
                        jsval = "{\"Table\":[{\"RetValue\":\"Failed\"}]}";
                    }
                }
                return jsval;// Json(JsonConvert.DeserializeObject(strMsg), JsonRequestBehavior.AllowGet);

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
        public async Task<string> JsonPostImage(string imgid = "", string base64 = "", string type = "", string SurveyType = "", string Version = "")
        {
            string fullOutputPath = string.Empty; bool bsaveimg = false; string filetype = string.Empty; string jsval = string.Empty;
            string filepath = "~/ImageUploads/FollowUp_MainArchive/";
            var resjsondata = "{imgid:" + imgid + " ,base64:" + base64 + ",type:" + type + ",SurveyType:" + SurveyType + ",Version:" + Version + "}";
            filepath = filepath + "^" + Version + "^" + Guid.NewGuid() + "^-" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".txt";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath(filepath), false, System.Text.Encoding.UTF8);
            sw.WriteLine(resjsondata);
            sw.Close();
            if (SurveyType == "F")
                fullOutputPath = "~/ImageUploads/SurveyImages/FollowUp/";
            else
                fullOutputPath = "~/ImageUploads/SurveyImages/FollowUp/";

            byte[] bytes = Convert.FromBase64String(base64);
            if (type == ".pdf" || type == ".PDF" || type == "pdf" || type == "PDF")
            {
                fullOutputPath = fullOutputPath + imgid + ".PDF";
                if (ValidateImageSizeDocoument(bytes, 5)) // 5 MB validation
                {
                    System.IO.File.WriteAllBytes(Server.MapPath(fullOutputPath), bytes);
                    bsaveimg = true;
                    filetype = "PDF";
                    
                }
                else
                {
                    //sval = "{\"Table\":[{\"Name\":\"" + imgid + "\",\"Massage\":\"" + "Document size exceeds 10 MB." + "\",\"path\":\"" + fullOutputPath + "\"}]}";
                    jsval = "{\"Table\":[{\"statuscode\":\"" + "201" + "\",\"msg\":\"" + "Document size exceeds 5 MB." + "\"}]}";
                    bsaveimg = false;
                }
            }
            else
            {
                fullOutputPath = fullOutputPath + imgid + ".png";
                System.Drawing.Image image;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                {
                    using (image = System.Drawing.Image.FromStream(ms))
                    {
                        if (ValidateImageSize(ms))
                        {
                            image.Save(Server.MapPath(fullOutputPath), System.Drawing.Imaging.ImageFormat.Png);
                            bsaveimg = true;
                        }
                        else
                        {
                            //jsval = "{\"Table\":[{\"Name\":\"" + imgid + "\",\"Massage\":\"" + "Image size exceeds 1 MB." + "\",\"path\":\"" + fullOutputPath + "\"}]}";
                            jsval = "{\"Table\":[{\"statuscode\":\"" + "201" + "\",\"msg\":\"" + "Image size exceeds 1 MB." + "\"}]}";
                            bsaveimg = false;
                        }
                    }
                }
            }
            if (bsaveimg)
            {
                DataTable dt = new DataTable();
                dt = SP_Model.SP_UploadImgApi(imgid, fullOutputPath, filetype, Version);
                if (dt.Rows.Count > 0)
                {
                    //jsval = "{\"Table\":[{\"Name\":\'" + imgid + "',\"path\":\'" + fullOutputPath + "'}]}";

                    //jsval = "{\"Table\":[{\"S\":\"" + imgid + "\",\"path\":\"" + fullOutputPath + "\"}]}";
                    jsval = "{\"Table\":[{\"statuscode\":\"" + "200" + "\",\"msg\":\"" + "" + "\"}]}";
                }
                else
                {
                    //jsval = "{\"Table\":[{\"Name\":\"\",\"path\":\"\"}]}";
                    // jsval = "{\"Table\":[{\"statuscode\":\"\",\"path\":\"\"}]}";
                    jsval = "{\"Table\":[{\"statuscode\":\"" + "201" + "\",\"msg\":\"" + "Server Error." + "\"}]}";
                }
            }
            return jsval;
        }
        public static bool ValidateImageSize(MemoryStream ms)
        {
            const int maxSizeInBytes = 1 * 1024 * 1024; // 1 MB in bytes

            // Check if the stream size is less than or equal to 1 MB
            if (ms.Length <= maxSizeInBytes)
            {
                return true; // Valid size
            }

            return false; // Invalid size
        }
        public static bool ValidateImageSizeDocoument(byte[] imageBytes, int maxMB)
        {
            // Convert MB to bytes (1 MB = 1024 * 1024 bytes)
            int maxSizeInBytes = maxMB * 1024 * 1024;

            // Check if the image size is less than or equal to the specified limit
            if (imageBytes.Length <= maxSizeInBytes)
            {
                return true; // Valid size
            }

            return false; // Invalid size
        }
    }
}