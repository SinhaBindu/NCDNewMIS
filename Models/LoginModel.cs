using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class LoginModel
    {
       public LoginModel()
        {
            LoginId_pk = 0;
        }
        public int LoginId_pk { get; set; }
        public int RegId_fk { get; set; }
        public int RegMapId_fk { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
    }
}