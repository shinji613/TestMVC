using System;
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

                return RedirectToAction("Index");
            } catch (Exception ex) {
                Response.Write("新增員工失敗，失敗原因：" + ex.Message);
            }

            return View();
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

                return RedirectToAction("Index");
            } catch (Exception ex) {
                Response.Write("編輯員工失敗，失敗原因：" + ex.Message);
            }

            return View();
        }

        public ActionResult Delete(int? id) {
            using (NorthwindEntities nw = new NorthwindEntities()) {
                Employee employee = (from emp in nw.Employees where emp.EmployeeID == id select emp).FirstOrDefault();

                return View(employee);
            };
        }

        [HttpPost]
        public ActionResult Delete(Employee postback) {
            try {

                using (NorthwindEntities nw = new NorthwindEntities()) {
                    Employee employee = (from emp in nw.Employees where emp.EmployeeID == postback.EmployeeID select emp).FirstOrDefault();

                    nw.Employees.Remove(employee);
                    nw.SaveChanges();
                }

                return RedirectToAction("Index");
            } catch (Exception ex) {
                Response.Write("刪除員工失敗，失敗原因：" + ex.Message);
            }

            return View();
        }
    }
}