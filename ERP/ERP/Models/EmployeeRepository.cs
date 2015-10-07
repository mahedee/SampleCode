using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERP.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //ERPContext context = new ERPContext();


        ERPContext context = new ERPContext();


        public EmployeeRepository()
            : this(new ERPContext())
        {

        }

        public EmployeeRepository(ERPContext context)
        {

            this.context = context;
        }


        public IQueryable<Employee> All
        {
            get { return context.Employees; }
        }

        public IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties)
        {
            IQueryable<Employee> query = context.Employees;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Employee Find(string id)
        {
            return context.Employees.Find(id);
        }

        public void InsertOrUpdate(Employee employee)
        {
            //if (employee.Id == default(string))
            //{
            //    // New entity
            //    context.Employees.Add(employee);
            //}
            //else
            //{
            //    // Existing entity
            //    context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            //}

            if (employee.Gid == default(long))
            {
                // New entity
                context.Employees.Add(employee);
            }
            else
            {
                // Existing entity
                context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var employee = context.Employees.Find(id);
            context.Employees.Remove(employee);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IEmployeeRepository : IDisposable
    {
        IQueryable<Employee> All { get; }
        IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties);
        Employee Find(string id);
        void InsertOrUpdate(Employee employee);
        void Delete(string id);
        void Save();
    }
}