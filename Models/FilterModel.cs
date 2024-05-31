using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class FilterModel
    {
        public string Block { get; set; }
        public string District { get; set; }
        public string FormDt { get; set; }
        public string ToDt { get; set; }
        public string Type { get; set; }
    }
}