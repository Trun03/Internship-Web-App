using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProDao
        {
            private InternshipDbContext intern;

            public ProDao()
            {
                intern = new InternshipDbContext();
                intern.Database.CommandTimeout = 180;
            }

            public List<Pro> GetProData()
            {
                return intern.Pros.ToList();
            }

        public void CreatePro(Pro pro)
        {
            intern.Pros.Add(pro);
            intern.SaveChanges();
        }

        public void UpdatePro(Pro pro)
        {
            var existingPro = intern.Pros.Find(pro.id);
            if (existingPro != null)
            {
                existingPro.namepj = pro.namepj;
                existingPro.description = pro.description;
                intern.SaveChanges();
            }
        }

        public bool ProjectContainsSubProjects(int proId)
        {
            return intern.SubProjects.Any(sp => sp.Idprj == proId);
        }


        public void DeletePro(int proId)
        {
            var proToDelete = intern.Pros.Find(proId);
            if (proToDelete != null)
            {
                intern.Pros.Remove(proToDelete);
                intern.SaveChanges();
            }
        }

        public List<Pro> SearchProjects(string searchText)
        {
            return intern.Pros.Where(x => x.namepj.Contains(searchText) || 
                                     x.description.Contains(searchText)).
                                     ToList();
        }


        public bool IsDuplicateProjectName(string projectName)
        {
            var projectNames = intern.Pros.Select(p => p.namepj).ToList();

            return projectNames.Any(name => name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
        }


    }
}


