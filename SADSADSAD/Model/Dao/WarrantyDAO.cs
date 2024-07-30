namespace Model.Dao
{
    using Model.EF;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WarrantyDAO
    {
        private InternshipDbContext intern;

        public WarrantyDAO()
        {
            intern = new InternshipDbContext();
            intern.Database.CommandTimeout = 180;
        }

        public List<Device> GetAllDevices()
        {
            return intern.Devices.OrderBy(x => x.DeviceID).ToList();
        }

    }
}