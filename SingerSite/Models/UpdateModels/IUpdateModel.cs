using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SingerSite.Models
{
    public interface IUpdateModel
    {
        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        DateTime PostDate { get; set; }

        String PostType { get; }

        String Title { get; set; }

        String Link { get; set; }

        //String Image { get; }
    }

    public interface IUpdateContext
    {
        List<IUpdateModel> TableEntries { get; }
    }
}