using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSharing.Web.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }

        [Required]
        public String Title { get; set; }

        [DisplayName("Picture")]
        public byte[] PhotoFile { get; set; }

        public String ImageMimeType { get; set; }

        [DataType(DataType.MultilineText)]

        [MaxLength(250)]
        public String Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime CreatedDate { get; set; }

        public String UserName { get; set; }
    
        public virtual List<Comment> Comments { get; set; }
}
}