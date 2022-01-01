﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Models
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(string id = null) {
            ViewBag.ResultMsg = TempData["ResultMsg"] != null ? TempData["ResultMsg"] : "";

            using (NorthwindEntities nw = new NorthwindEntities()) {
                List<Employee> listEmps = (from emp in nw.Employees select emp).ToList();

                return View(listEmps);
            }           
        }

        public ActionResult New() {
            return View();
        }

        [HttpPost]
        public ActionResult New(Employee postback) {
            try {
                using (NorthwindEntities nw = new NorthwindEntities()) {
                    nw.Employees.Add(postback);
                    nw.SaveChanges();
                }

                TempData["ResultMsg"] = string.Format("新增員工成功");

            } catch (Exception ex) {
                TempData["ResultMsg"] = string.Format("新增員工失敗，失敗原因：{0}", ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modify(int? id) {
            using (NorthwindEntities nw = new NorthwindEntities()) {
                Employee employee = (from emp in nw.Employees where emp.EmployeeID == id select emp).FirstOrDefault();

                return View(employee);
            };
        }

        [HttpPost]
        public ActionResult Modify(Employee postback) {
            try {
                using (NorthwindEntities nw = new NorthwindEntities()) {
                    Employee employee = (from emp in nw.Employees where emp.EmployeeID == postback.EmployeeID select emp).FirstOrDefault();

                    employee.FirstName = postback.FirstName;
                    employee.LastName = postback.LastName;

                    nw.SaveChanges();
                }

                TempData["ResultMsg"] = string.Format("編輯員工【{0}】成功", postback.EmployeeID);
            } catch (Exception ex) {
                TempData["ResultMsg"] = string.Format("編輯員工【{0}】失敗，失敗原因：{1}", postback.EmployeeID, ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id) {
            using (NorthwindEntities nw = new NorthwindEntities()) {
                Employee employee = (from emp in nw.Employees where emp.EmployeeID == id select emp).FirstOrDefault();

                return View(employee);
            };
        }

        [HttpPost]
        public ActionResult Delete(int empID) {
            try {

                using (NorthwindEntities nw = new NorthwindEntities()) {
                    Employee employee = (from emp in nw.Employees where emp.EmployeeID == empID select emp).FirstOrDefault();

                    nw.Employees.Remove(employee);
                    nw.SaveChanges();
                }

                TempData["ResultMsg"] = string.Format("刪除員工【{0}】成功", empID);

            } catch (Exception ex) {
                TempData["ResultMsg"] = string.Format("刪除員工【{0}】失敗，失敗原因：{1}", empID, ex.Message);
            }

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult Delete(Employee postback) {
        //    try {

        //        using (NorthwindEntities nw = new NorthwindEntities()) {
        //            Employee employee = (from emp in nw.Employees where emp.EmployeeID == postback.EmployeeID select emp).FirstOrDefault();

        //            nw.Employees.Remove(employee);
        //            nw.SaveChanges();
        //        }

        //        TempData["ResultMsg"] = string.Format("刪除員工成功");

        //    } catch (Exception ex) {
        //        TempData["ResultMsg"] = string.Format("刪除員工失敗，失敗原因：{0}", ex.Message);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}