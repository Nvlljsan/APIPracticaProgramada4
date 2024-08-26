using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIPracticaProgramada4.Models
{
    public class ClasificationViewModel
    {
        public int ClassificationId { get; set; }
        public string PriorityLevel { get; set; }
    }
}