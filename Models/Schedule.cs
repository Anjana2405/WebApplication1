using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Schedule
    {
        public int id { get; set; }
        [ForeignKey("doc")]
        public int docID { get; set; }
        public Doctor doc { get; set; }
        public string schedule { get; set; }
    }
}