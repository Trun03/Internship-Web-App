namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Storage")]
    public partial class Storage
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Device_Name { get; set; }

        [StringLength(50)]
        public string Serial_No { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ImportDate { get; set; }
    }
}
