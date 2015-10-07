using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class UnitOfWork : IDisposable
    {
        private ERPContext context;
        public UnitOfWork()
        {
            context = new ERPContext();
        }

        public UnitOfWork(ERPContext _context)
        {
            this.context = _context;
        }



        private EmployeeRepository _employeeRepository;

        public EmployeeRepository EmployeeRepository
        {
            get
            {

                if (this._employeeRepository == null)
                {
                    this._employeeRepository = new EmployeeRepository(context);
                }
                return _employeeRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}