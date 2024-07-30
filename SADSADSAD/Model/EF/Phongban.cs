namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phongban")]
    public partial class Phongban
    {
        public int id { get; set; }

        public int? id_donvi { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(250)]
        public string description { get; set; }
    }
}
