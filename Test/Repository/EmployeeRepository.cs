using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Repository
{
    public class EmployeeRepository<T> : IEmployeeRepository<T> where T : class
    {
        public readonly EmployeeContext context = null;
        private DbSet<T> employee = null;
        public EmployeeRepository()
        {
            this.context = new EmployeeContext();
            employee = context.Set<T>();
        }
        public EmployeeRepository(EmployeeContext context)
        {
            this.context = context;
            employee = context.Set<T>();
        }


        public void Add(T Employee)
        {
            employee.Add(Employee);
        }

        public void Delete(object EmployeeID)
        {
            T emp = employee.Find(EmployeeID);
            employee.Remove(emp);
        }

        public IEnumerable<T> GetAll()
        {
            return employee.ToList();
        }

        public T GetById(object EmployeeID)
        {
            return employee.Find(EmployeeID);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T Employee)
        {
            employee.Attach(Employee);
            context.Entry(Employee).State= EntityState.Modified;
        }
    }
}