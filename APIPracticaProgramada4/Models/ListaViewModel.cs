using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPracticaProgramada4.Models
{
    public class ListaViewModel
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; }
        public int? ClassificationId { get; set; }

        public ClasificationViewModel Clasification { get; set; }
    }
}