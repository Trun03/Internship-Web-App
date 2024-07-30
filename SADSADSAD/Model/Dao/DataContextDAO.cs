using System.Linq;
using Model.EF;

namespace Model.Dao
{
    public class DataContextDAO
    {
        private InternshipDbContext db;

        public DataContextDAO()
        {
            db = new InternshipDbContext(); // Khởi tạo đối tượng db
        }

        //public Account GetAccount(string username, string password)
        //{
        //    return db.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        //}
    }
}
