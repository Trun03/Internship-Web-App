using System.Collections.Generic;
using System.Linq;
using Model.EF;

namespace Model.DAO
{
    public class CustomerDAO
    {
        private InternshipDbContext dbContext;

        public CustomerDAO()
        {
            dbContext = new InternshipDbContext();
        }

        public List<Customer> GetCustomers()
        {
            return dbContext.Customers.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = dbContext.Customers.Find(customer.ID);
            if (existingCustomer != null)
            {
                existingCustomer.Department = customer.Department;
                existingCustomer.Name = customer.Name; 
                existingCustomer.Unit = customer.Unit; 
                existingCustomer.Description = customer.Description;
                existingCustomer.ID = customer.ID;
                dbContext.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = dbContext.Customers.Find(id);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
            }
        }
    }
}
