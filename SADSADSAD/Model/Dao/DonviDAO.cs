using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class DonviDAO
    {
        private InternshipDbContext db = null;

        public DonviDAO()
        {
            db = new InternshipDbContext();
        }

        public List<Donvi> GetUnits()
        {
            return db.Donvis.ToList();
        }

        public bool CanDeleteUnit(int unitId)
        {
            return !db.Phongbans.Any(p => p.id_donvi == unitId);
        }

        public void DeleteUnit(int id)
        {
            var unit = db.Donvis.Find(id);
            if (unit != null && CanDeleteUnit(id))
            {
                db.Donvis.Remove(unit);
                db.SaveChanges();
            }
        }

        public bool IsDuplicateUnitName(string unitName)
        {
            return db.Donvis.Any(u => u.name.Equals(unitName, StringComparison.OrdinalIgnoreCase));
        }

        public void AddUnit(Donvi unit)
        {
            db.Donvis.Add(unit);
            db.SaveChanges();
        }

        public void UpdateUnit(Donvi unit)
        {
            var existingUnit = db.Donvis.Find(unit.id);
            if (existingUnit != null)
            {
                existingUnit.name = unit.name;
                existingUnit.description = unit.description;
                db.SaveChanges();
            }
        }

        public Donvi GetUnitByName(string unitName)
        {
            return db.Donvis.FirstOrDefault(d => d.name.Equals(unitName, StringComparison.OrdinalIgnoreCase));
        }

        // Searchbar
        public List<Donvi> SearchUnit(string searchText)
        {
            return db.Donvis.Where(x => x.name.Contains(searchText) || x.description.Contains(searchText)).ToList();
        }

        public Donvi GetUnitById(int UnitId)
        {
            return db.Donvis.FirstOrDefault(d => d.id == UnitId);
        }
    }
}
