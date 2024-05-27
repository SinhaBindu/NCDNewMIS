using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class PostDataModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public string JsonData { get; set; }
    }
}