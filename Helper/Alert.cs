using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Helper
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
    }

    public static class AlertStyles
    {
        public const string Success = "inline-block rounded-lg font-medium text-[15px] max-xl:text-xs leading-5 py-[0.719rem] max-xl:px-4 px-[1.563rem] max-xl:py-2.5 border border-success bg-success text-white hover:bg-success-hover duration-300 btn sweet-success";
        public const string Information = "inline-block rounded-lg font-medium text-[15px] max-xl:text-xs leading-5 py-[0.719rem] max-xl:px-4 px-[1.563rem] max-xl:py-2.5 border border-primary bg-primary text-white hover:bg-hover-primary hover:border-hover-primary duration-300 btn sweet-html";
        public const string Warning = "inline-block rounded-lg font-medium text-[15px] max-xl:text-xs leading-5 py-[0.719rem] max-xl:px-4 px-[1.563rem] max-xl:py-2.5 border border-warning bg-warning text-white hover:bg-warning-hover duration-300 btn sweet-confirm";
        public const string Danger = "inline-block rounded-lg font-medium text-[15px] max-xl:text-xs leading-5 py-[0.719rem] max-xl:px-4 px-[1.563rem] max-xl:py-2.5 border border-danger bg-danger text-white hover:bg-danger-hover duration-300 btn sweet-auto";
    }
}