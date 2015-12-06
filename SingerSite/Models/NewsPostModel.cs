using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace SingerSite.Models
{
    public class NewsPostModel
    {
        public int ID { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        public string Content { get; set; }
    }

    public class NewsPostDBContext : DbContext
    {
        public NewsPostDBContext() : base("DefaultConnection")
        {
        }
        public DbSet<NewsPostModel> NewsPosts { get; set; }
    }

    public class NewsIndexViewModel
    {
        public List<IUpdateModel> UpdateModels { get; set; }
        public List<NewsPostModel> NewsPostModels { get; set; }
    }
}