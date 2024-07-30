namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cauhinh")]
    public partial class Cauhinh
    {
        public int id { get; set; }

        public int? id_Device { get; set; }

        [StringLength(250)]
        public string Chip { get; set; }

        [StringLength(250)]
        public string RAM { get; set; }

        [StringLength(250)]
        public string HDD { get; set; }

        [StringLength(250)]
        public string SSD { get; set; }

        [StringLength(250)]
        public string Main { get; set; }

        public string description { get; set; }
    }
}
