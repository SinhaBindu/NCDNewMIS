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
    
    public partial class tbl_Login
    {
        public int LoginId_pk { get; set; }
        public Nullable<int> RegId_fk { get; set; }
        public Nullable<int> RegMapId_fk { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
