using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class FilterModel
    {
        [DisplayName("District")]
        public string DistrictId { get; set; }
        [DisplayName("Block")]
        public string BlockId { get; set; }
        [DisplayName("CHC")]
        public string CHCId { get; set; }
        [DisplayName("PHC")]
        public string PHCId { get; set; }
        [DisplayName("Sub-Center")]
        public string SCId { get; set; }
        [DisplayName("Panchayat")]
        public string GPId { get; set; }
        [DisplayName("Village")]
        public string VIId { get; set; }
        [DisplayName("From Date")]
        public string FormDt { get; set; }
        [DisplayName("To Date")]
        public string ToDt { get; set; }
        [DisplayName("Type")]
        public string Type { get; set; }
        [DisplayName("Status")]
        public string IsActive { get; set; }
        [DisplayName("Screening Type")]
        public string SType { get; set; }
        [DisplayName("Round Type")]
        public string RoundType { get; set; }
        public string DistrictBlockType { get; set; }
    }
}