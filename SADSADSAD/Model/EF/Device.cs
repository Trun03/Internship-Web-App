namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Device
    {
        public int DeviceID { get; set; }

        public int SubProjectID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Serial { get; set; }

        [StringLength(250)]
        public string Model { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
