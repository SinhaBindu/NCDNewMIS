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
    
    public partial class tbl_RegMappping
    {
        public int RegMapId_pk { get; set; }
        public Nullable<System.DateTime> MapDate { get; set; }
        public Nullable<int> RegId_fk { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> CHCId_fk { get; set; }
        public Nullable<int> PHCId_fk { get; set; }
        public Nullable<int> SubCenterId_fk { get; set; }
        public Nullable<int> GPId_fk { get; set; }
        public Nullable<int> VillageId_fk { get; set; }
        public string Version { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}