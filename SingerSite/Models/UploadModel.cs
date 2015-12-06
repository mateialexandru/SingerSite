using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace SingerSite.Models
{
    public class UploadModel : IUpdateModel
    {
        public int ID { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        public String PostType { get; set; }

        public String Title { get; set; }

        public String Link { get; set; }

        public virtual FilePath FilePath { get; set; }
    }

    public class UploadDBContext : DbContext
    {
        public UploadDBContext() : base("DefaultConnection")
        {
        }

        public DbSet<UploadModel> Uploads { get; set; }

        public DbSet<FilePath> FilePaths { get; set; }
    }
}