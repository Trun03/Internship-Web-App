namespace Model.Dao
{
    using Model.EF;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DevicesDAO
    {
        private InternshipDbContext intern;

        public DevicesDAO()
        {
            intern = new InternshipDbContext();
            intern.Database.CommandTimeout = 180;
        }

        public List<Device> GetAllDevices()
        {
            return intern.Devices.OrderBy(x => x.DeviceID).ToList();
        }

        public List<Device> GetDevicebySubProjectsID(int subProjectID)
        {
            return intern.Devices.Where(x => x.SubProjectID == subProjectID).ToList();
        }

        public void CreateDevice(Device device)
        {
            intern.Devices.Add(device);
            intern.SaveChanges();
        }

        public void UpdateDevice(Device device)
        {
            var existingDevice = intern.Devices.Find(device.DeviceID);
            if (existingDevice != null)
            {
                existingDevice.SubProjectID = device.SubProjectID;
                existingDevice.Name = device.Name;
                existingDevice.Serial = device.Serial;
                existingDevice.Description = device.Description;
                existingDevice.Model = device.Model;
                existingDevice.Status = device.Status; // Cập nhật trường Status
                intern.SaveChanges();
            }
        }

        public void DeleteDevice(int deviceID)
        {
            var device = intern.Devices.Find(deviceID);
            if (device != null)
            {
                var associatedCauhinh = intern.Cauhinhs.FirstOrDefault(c => c.id_Device == deviceID);
                if (associatedCauhinh != null)
                {
                    throw new Exception("Cannot delete this device because it has associated configurations.");
                }
                intern.Devices.Remove(device);
                intern.SaveChanges();
            }
        }

        // Searchbar
        public List<Device> SearchDevices(string searchText)
        {
            return intern.Devices.Where(d => d.Name.Contains(searchText) ||
                                              d.Description.Contains(searchText) ||
                                              d.Serial.Contains(searchText) ||
                                              d.Model.Contains(searchText) ).
                                  ToList();
        }

        public Device GetDeviceById(int deviceId)
        {
            return intern.Devices.FirstOrDefault(d => d.DeviceID == deviceId);
        }
    }
}
