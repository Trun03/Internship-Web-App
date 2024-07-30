namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        public int? id_phongban { get; set; }

        public int? id_thietbi { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        public int? id_donvi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartBorrowingDate { get; set; }
    }
}
