using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace NCDNewMIS.Models
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public static SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        public override void OnException(ExceptionContext filterContext)
        {
            // Log the error to the database
            LogError(filterContext.Exception, filterContext);

            // Set the exception as handled
            filterContext.ExceptionHandled = true;

            // Redirect to a custom error page
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }

        private void LogError(Exception exception, ExceptionContext context)
        {
            using (var contextdb = new ApplicationDbContext())
            {
                con = (SqlConnection)contextdb.Database.Connection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = @"
                INSERT INTO ErrorLog (ErrorMessage, StackTrace, Controller, Action, Timestamp)
                VALUES (@ErrorMessage, @StackTrace, @Controller, @Action, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ErrorMessage", exception.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", exception.StackTrace ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Controller", context.RouteData.Values["controller"]?.ToString() ?? "Unknown");
                    cmd.Parameters.AddWithValue("@Action", context.RouteData.Values["action"]?.ToString() ?? "Unknown");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}