using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDAO
    {
        private InternshipDbContext db = null;

        public UserDAO()
        {
            db = new InternshipDbContext();
        }


        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }


        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = db.Users.Find(user.id);
            if (existingUser != null)
            {
                // Update user properties
                existingUser.id_donvi = user.id_donvi;
                existingUser.id_phongban = user.id_phongban;
                existingUser.id_thietbi = user.id_thietbi;
                existingUser.name = user.name;



                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }


        public List<User> GetUsersByUnit(int unitID)
        {
            return db.Users.Where(u => u.id_donvi == unitID).ToList();
        }


        public List<User> GetUserbyDepartID(int DepartID)
        {
            return db.Users.Where(x => x.id_phongban == DepartID).ToList();
        }


        public List<User> SearchUser(string searchText)
        {
            return db.Users.Where(u => u.name.Contains(searchText)).ToList();
        }



        //DETAIL
        public User GetUserById(int id)
        {
            return db.Users.Find(id);
        }

    }
}
