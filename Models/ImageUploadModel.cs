using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCDNewMIS.Models
{
    public class ImageUploadModel
    {
        public ImageUploadModel() { Id = 0; }   
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> RoundType { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public List<HttpPostedFileBase> FileUpload { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}