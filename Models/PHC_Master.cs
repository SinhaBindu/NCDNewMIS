//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NCDNewMIS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHC_Master
    {
        public int PHCId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> CHCId { get; set; }
        public string PHC { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
    }
}
