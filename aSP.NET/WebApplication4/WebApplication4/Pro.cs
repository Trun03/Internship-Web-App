using System.ComponentModel.DataAnnotations;

namespace WebApplication4
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Pro
    {
        public Pro()
        {
            this.Goithaus = new HashSet<Goithau>();
        }
        public SelectList dropdown { get; set; }
        public List<Pro> lstpr { get; set; }
        public int id { get; set; }
        public string namepj { get; set; }

        [MaxLength(5000)]
        public string description { get; set; }

        public virtual ICollection<Goithau> Goithaus { get; set; }
    }
}