using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CauhinhDAO
    {
        private InternshipDbContext intern;
        public CauhinhDAO()
        {
            intern = new InternshipDbContext();
            intern.Database.CommandTimeout = 180;
        }

        public void CreateCauHinh(Cauhinh cauhinh)
        {
            intern.Cauhinhs.Add(cauhinh);
            intern.SaveChanges();
        }

        public List<Cauhinh> GetCauHinhData()
        {
            return intern.Cauhinhs.ToList();
        }

        public List<Cauhinh> GetCauhinhByDeviceID(int DeviceID)
        {
            return intern.Cauhinhs.Where(x => x.id_Device == DeviceID).ToList();
        }


        public void UpdateCauHinh(Cauhinh cauhinh)
        {
            var existingCauhinh = intern.Cauhinhs.Find(cauhinh.id);
            if (existingCauhinh != null)
            {
                existingCauhinh.Chip = cauhinh.Chip;
                existingCauhinh.RAM = cauhinh.RAM;
                existingCauhinh.HDD = cauhinh.HDD;
                existingCauhinh.SSD = cauhinh.SSD;
                existingCauhinh.Main = cauhinh.Main;
                existingCauhinh.description = cauhinh.description;

                intern.SaveChanges();
            }
        }

        public Cauhinh GetCauHinhByIdDevice(int? idDevice)
        {
            if (!idDevice.HasValue)
                return null;

            // Lấy danh sách Cau Hinh từ database
            var cauhinhList = GetCauHinhData();

            // Tìm Cau Hinh có id_Device khớp với idDevice
            return cauhinhList.FirstOrDefault(c => c.id_Device == idDevice.Value);
        }
        public void DeleteCauHinh(int id)
        {
            var cauhinh = intern.Cauhinhs.Find(id);
            if (cauhinh != null)
            {
                intern.Cauhinhs.Remove(cauhinh);
                intern.SaveChanges();
            }
        }


        public Cauhinh GetConfigurationByDeviceId(int deviceId)
        {
            return intern.Cauhinhs.FirstOrDefault(c => c.id_Device == deviceId);
        }


        public List<Cauhinh> SearchCauHinh(string searchText)
        {
            var cauhinhs = intern.Cauhinhs.ToList();
            var filteredCauhinhs = cauhinhs.Where(x =>
                x.Chip.Contains(searchText) ||
                x.RAM.Contains(searchText) ||
                x.HDD.Contains(searchText) ||
                x.SSD.Contains(searchText) ||
                x.Main.Contains(searchText) ||
                x.description.Contains(searchText)
            ).ToList();

            return filteredCauhinhs;
        }

    }
}
