using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace SingerSite.Models
{
    public enum FileType : int
    {
        Unknown = 0,
        jpg = 1,
        jpeg = jpg,
        png = jpg,
        mp3 = 2,
        mp4 = 3,
    }

    public class FilePath
    {
        public int FilePathId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }

        //public int SongViewModelID { get; set; }
        //public SongViewModel SongViewModel { get; set; }
    }
}