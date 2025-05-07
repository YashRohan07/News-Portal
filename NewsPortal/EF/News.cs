using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortal.EF
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; } // Example: "Business", "Sports"
        public DateTime Date { get; set; }
    }
}