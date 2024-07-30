using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class PhongbanDAO
    {
        private InternshipDbContext db = null;

        public PhongbanDAO()
        {
            db = new InternshipDbContext();
        }

        public List<Phongban> GetPhongbans()
        {
            return db.Phongbans.ToList();
        }


        public void AddPhongban(Phongban phongban)
        {
            db.Phongbans.Add(phongban);
            db.SaveChanges();
        }

        public void UpdatePhongban(Phongban phongban)
        {
            var existingPhongban = db.Phongbans.Find(phongban.id);
            if (existingPhongban != null)
            {
                existingPhongban.id_donvi = phongban.id_donvi;
                existingPhongban.name = phongban.name;
                existingPhongban.description = phongban.description;

                db.SaveChanges();
            }
        }

        public void DeletePhongban(int id)
        {
            var phongban = db.Phongbans.Find(id);
            if (phongban != null)
            {
                db.Phongbans.Remove(phongban);
                db.SaveChanges();
            }
        }

        public bool HasDepartmentsInUnit(int unitId)
        {
            return db.Phongbans.Any(pb => pb.id_donvi == unitId);
        }

        public List<Phongban> GetDepBYUnitID(int unitID)
        {
            return db.Phongbans.Where(x => x.id_donvi == unitID).ToList();
        }

        //Searchfunc
        public List<Phongban> SearchDep(string searchText)
        {
            return db.Phongbans.Where(x => x.name.Contains(searchText) ||
                                           
                                            x.description.Contains(searchText)).ToList();
        }

        public bool HasUsersInDepartment(int departmentId)
        {
            return db.Users.Any(u => u.id_phongban == departmentId);
        }

        //Func không trùng nhau 
        public bool IsDuplicateDepartmentNameInUnit(int? unitId, string departmentName)
        {
            // Nếu unitId là null, không có đơn vị nào tồn tại
            if (!unitId.HasValue)
            {
                return false;
            }

            // Kiểm tra xem có phòng ban trùng tên trong đơn vị không
            return db.Phongbans.Any(pb => pb.id_donvi == unitId && pb.name == departmentName);
        }

        public Phongban GetDepartmentById(int departmentId)
        {
            return db.Phongbans.FirstOrDefault(p => p.id == departmentId);
        }
    }
}
