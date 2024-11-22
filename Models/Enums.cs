using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace NCDNewMIS.Models
{
    public class Enums
    {
        public enum AlterMsg
        {
            [Display(Name = "All filed Required")]
            Required = 1,
            [Display(Name = "Security Token key not match.")]
            SecurityToken = 2,
            [Display(Name = "Security Token key not null.")]
            SecurityTokenNot = 22,
            [Display(Name = "Record Submitted!!")]
            Success = 3,
            [Display(Name = "This record is Already Exists !!")]
            Already = 4,
            [Display(Name = "There was a communication error.")]
            Error = 5,
            [Display(Name = "success")]
            Loginsuccess = 6,
            [Display(Name = "failed")]
            Loginfailed = 7
        }

        public enum Default1stValue
        {
            BlockId=1
        }

        }
}