using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Appointment
    {
        public int id { get; set; }
        [ForeignKey("doctor")]
        public int docID { get; set; }
        public Doctor doctor { get; set; }
        [ForeignKey("patient")]
        public int ptID { get; set; }
        public Patient patient { get; set; }
        public DateTime date { get; set; }
    }
}