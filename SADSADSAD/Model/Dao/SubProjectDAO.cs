using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class SubProjectDAO
    {
        private InternshipDbContext intern = null;
        public SubProjectDAO()
        {
            intern = new InternshipDbContext();
            intern.Database.CommandTimeout = 180;
        }

        public List<SubProject> GetAllSubProject()
        {
            try
            {
                return intern.SubProjects.ToList();
            }
            catch (Exception ex)
            {
       
                Console.WriteLine($"An error occurred while retrieving subprojects: {ex.Message}");
                return new List<SubProject>(); 
            }
        }


        public List<SubProject> GetSubProjectsBYProID(int proID)
        {
            return intern.SubProjects.Where(x => x.Idprj == proID).ToList();
        }




        public void CreateSubProject(SubProject subpro)
        {
            if (!IsDuplicateSubProjectName(subpro.Idprj, subpro.Name))
            {
                intern.SubProjects.Add(subpro);
                intern.SaveChanges();
            }
            else
            {
                throw new Exception("Tên gói thầu đã tồn tại trong dự án.");
            }
        }

        public void UpdateSubProject(SubProject subpro)
        {
            var existingSubProject = intern.SubProjects.Find(subpro.SubProjectID);
            if (existingSubProject != null)
            {
                if (!IsDuplicateSubProjectName(subpro.Idprj, subpro.Name, subpro.SubProjectID))
                {
                    existingSubProject.Name = subpro.Name;
                    existingSubProject.Description = subpro.Description;
                    existingSubProject.Idprj = subpro.Idprj;
                    intern.SaveChanges();
                }
                else
                {
                    throw new Exception("Tên gói thầu đã tồn tại trong dự án.");
                }
            }
        }

        public bool IsDuplicateSubProjectName(int projectId, string subProjectName, int? subProjectId = null)
        {
            try
            {
                if (subProjectId.HasValue)
                {
                   
                    return intern.SubProjects.Any(sp => sp.Idprj == projectId && sp.Name == subProjectName && sp.SubProjectID != subProjectId.Value);
                }
                else
                {
           
                    return intern.SubProjects.Any(sp => sp.Idprj == projectId && sp.Name == subProjectName);
                }
            }
            catch (Exception ex)
            {
       
                Console.WriteLine($"An error occurred while checking for duplicate SubProject name: {ex.Message}");
                return false;
            }
        }


        public void DeleteSubProject(int SubProjectID)
        {
            var subpro = intern.SubProjects.Find(SubProjectID);
            if (subpro != null)
            {
                intern.SubProjects.Remove(subpro);
                intern.SaveChanges();
            }
        }

        public bool SubProjectContainsDevices(int subProjectID)
        {
            return intern.Devices.Any(d => d.SubProjectID == subProjectID);
        }
   

        public List<SubProject> SearchSubProject(string searchText)
        {
            return intern.SubProjects.Where(x => x.Name.Contains(searchText) || 
                                            x.Description.Contains(searchText))
                                            .ToList();
        }


       



    }
}