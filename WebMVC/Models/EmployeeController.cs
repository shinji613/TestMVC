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
        public ActionResult New(Employee employee) {
            try {
                using (NorthwindEntities nw = new NorthwindEntities()) {
                    nw.Employees.Add(employee);
                    nw.SaveChanges();
                }
            } catch (Exception ex) {
                Response.Write("新增員工失敗，失敗原因：" + ex.Message);
            }

            return View();
        }

        public ActionResult Modify(int id) {
            using (NorthwindEntities nw = new NorthwindEntities()) {
                Employee employee = (from emp in nw.Employees where emp.EmployeeID == id select emp).FirstOrDefault();

                return View(employee);
            };
        }

        [HttpPost]
        public ActionResult Modify(Employee employee) {
            try {
                using (NorthwindEntities nw = new NorthwindEntities()) {
                    nw.SaveChanges();
                }
            } catch (Exception ex) {
                Response.Write("編輯員工失敗，失敗原因：" + ex.Message);
            }

            return View();
        }
    }
}