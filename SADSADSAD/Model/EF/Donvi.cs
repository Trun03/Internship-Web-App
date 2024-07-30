namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donvi")]
    public partial class Donvi
    {
        public int id { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(250)]
        public string description { get; set; }
    }
}
