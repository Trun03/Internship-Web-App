using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class StorageDAO
    {
        private InternshipDbContext intern;

        public StorageDAO()
        {
            intern = new InternshipDbContext();
            intern.Database.CommandTimeout = 180;
        }

        public List<Storage> GetDevices()
        {
            return intern.Storages.OrderBy(x => x.Id).ToList();
        }

        public void CreateDevice(Storage device)
        {
            intern.Storages.Add(device);
            intern.SaveChanges();
        }

        public void UpdateDevice(Storage device)
        {
            var entity = intern.Storages.Find(device.Id);
            if (entity != null)
            {
                entity.Device_Name = device.Device_Name;
                entity.Serial_No = device.Serial_No;
                entity.Status = device.Status;
                entity.ImportDate = device.ImportDate; 
                intern.SaveChanges();
            }
        }

        public void DeleteDevice(int id)
        {
            var entity = intern.Storages.Find(id);
            if (entity != null)
            {
                intern.Storages.Remove(entity);
                intern.SaveChanges();
            }
        }
    }
}
