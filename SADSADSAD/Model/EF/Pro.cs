namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pro")]
    public partial class Pro
    {
        public int id { get; set; }

        [StringLength(255)]
        public string namepj { get; set; }

        [StringLength(255)]
        public string description { get; set; }
    }
}
