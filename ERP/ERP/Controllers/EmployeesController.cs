using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Models;

namespace ERP.Controllers
{
    public class EmployeesController : Controller
    {
        //private ERPContext context = new ERPContext();

        //private PMToolContext context = new PMToolContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Projects/

        public ViewResult Index()
        {

            //return View(unitOfWork.EmployeeRepository.AllIncluding(employee => employee.Id).ToList());
            return View(unitOfWork.EmployeeRepository.AllIncluding().ToList());
        }

        ////
        //// GET: /Employees/

        //public ViewResult Index()
        //{
        //    return View(context.Employees.ToList());
        //}

        ////
        //// GET: /Employees/Details/5

        //public ViewResult Details(string id)
        //{
        //    Employee employee = context.Employees.Single(x => x.Id == id);
        //    return View(employee);
        //}

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employees/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Employees.Add(employee);
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(employee);

            employee.Id = "BD123";
            employee.JoiningDt = DateTime.Now;
            


            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.InsertOrUpdate(employee);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        ////
        //// GET: /Employees/Edit/5

        //public ActionResult Edit(string id)
        //{
        //    Employee employee = context.Employees.Single(x => x.Id == id);
        //    return View(employee);
        //}

        ////
        //// POST: /Employees/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}

        ////
        //// GET: /Employees/Delete/5

        //public ActionResult Delete(string id)
        //{
        //    Employee employee = context.Employees.Single(x => x.Id == id);
        //    return View(employee);
        //}

        ////
        //// POST: /Employees/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Employee employee = context.Employees.Single(x => x.Id == id);
        //    context.Employees.Remove(employee);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}