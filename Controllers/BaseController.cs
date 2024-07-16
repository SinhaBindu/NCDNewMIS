using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//using NCDNewMIS.Models;
using NCDNewMIS.Helper;
using NCDNewMIS.Models;
//using AlertStyles = NCDNewMIS.Helper.AlertStyles;

namespace NCDNewMIS.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        // GET: Base
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(NCDNewMIS.Models.AlertStyles.Success, message, dismissable);
        }

        //public void Information(string message, bool dismissable = false)
        //{
        //    AddAlert(NCDNewMIS.Models.AlertStyles.Information, message, dismissable);
        //}

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(NCDNewMIS.Models.AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(NCDNewMIS.Models.AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Helper.Alert.TempDataKey)
                ? (List<Helper.Alert>)TempData[Helper.Alert.TempDataKey]
                : new List<Helper.Alert>();

            alerts.Add(new Helper.Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Helper.Alert.TempDataKey] = alerts;
        }

        //[HttpGet]
        //public JsonResult GetDepartmentbyhub(int id)
        //{
        //    dbEntities db = new dbEntities();

        //    var list = db.m_Department.Where(x=>x.HubId_Fk==id).OrderBy(o => o.Department).Select(b => new SelectListItem { Value = b.DepartId_pk.ToString(), Text = b.Department.ToString() });
        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}
        //=======================================================================

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public bool RegisterUser(string UserId,string Email, string Password, string Role)
        {
            var user = new ApplicationUser { UserName = UserId, Email = Email };
            var result = UserManager.Create(user, Password);
            if (result.Succeeded)
            {
                var u = UserManager.FindByEmail(Email);
                if (u != null)
                {
                    UserManager.AddToRole(u.Id, Role);
                   // SendMailToApplicant(user.Email, Password);
                }
                return true;
            }
            return false;
        }
        public bool UserRemoveFromRole(string userId, string roleName)
        {
            bool status = false;
            var userInDb = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
            {
                return status;
            }

            var roleInDb = UserManager.GetRoles(userId).SingleOrDefault();

            if (roleInDb == null)
            { return status; }
            var result = UserManager.RemoveFromRole(userInDb.Id, roleInDb);
            if (result.Succeeded)
            {
                return status = true;
            }
            return status;
        }
        public List<string>GetUserRoles(string userId )
        {
            List<string> roles = UserManager.GetRoles(userId).ToList();
            return roles;
        }
        public bool UserAddInRole(string userId, string roleName)
        {
            bool status = false;
            var userInDb = UserManager.Users.SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
            {
                return status;
            }

            var result = UserManager.AddToRole(userId, roleName);
            if (result.Succeeded)
            {
                return status = true;
            }
            return status;
        }
    }
}