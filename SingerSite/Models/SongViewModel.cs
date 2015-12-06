using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace SingerSite.Models
{
    public class SongViewModel : IUpdateModel 
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        //[RegularExpression(@"http(s)?://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)([\w\-]+)(&(amp;)?[\w\?=]*)?")]
        [StringLength(100)]
        public string YouTube { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        [StringLength(100)]
        public String Link { get; set; }

        //public String DummyField { get; set; }

        public int? FilePathId { get; set; }
        public FilePath FilePath { get; set; }

        public String PostType
        {
            get
            {
                string strReturn = "YouTube Video";

                if (FilePath != null)
                {
                    switch (FilePath.FileType)
                    {
                        case FileType.mp3:
                            {
                                strReturn = "MP3";
                            }
                            break;
                        case FileType.mp4:
                            {
                                strReturn = "MP4";
                            }
                            break;
                        default:
                            {
                            }
                            break;
                    }
                }

                return strReturn;
            }
        }
    }

    public class SongDBContext : DbContext, IUpdateContext
    {
        public SongDBContext() : base("DefaultConnection") { }

        public DbSet<SongViewModel> Songs { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<FilePath>()
        //        .HasRequired(lu => lu.SongViewModel)
        //        .WithOptional(pi => pi.FilePath);
        //}

        public List<IUpdateModel> TableEntries
        {
            get
            {
                List<IUpdateModel> entries = null;
                if (Songs != null)
                {
                    entries = new List<IUpdateModel>();
                    foreach (SongViewModel model in Songs)
                    {
                        if (model is IUpdateModel)
                        {
                            entries.Add(model as IUpdateModel);
                        }
                    }
                }
                return entries;
            }
        }
    }
}