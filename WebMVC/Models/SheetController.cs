using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Models
{
    public class SheetController : Controller
    {
        // GET: Sheet
        public ActionResult Index(string id = null)
        {
            using (NorthwindEntities nw = new NorthwindEntities()) {
                List<SheetSet> listSheet = (from sheet in nw.SheetSets select sheet).ToList();

                return View(listSheet);
            }
        }

        public ActionResult New() {
            return View();
        }

        [HttpPost]
        public ActionResult New(SheetSet sheet) {
            try {
                using (NorthwindEntities nw = new NorthwindEntities()) {
                    nw.SheetSets.Add(sheet);
                    nw.SaveChanges();
                }
            } catch (Exception) {
                Response.Write("新增表單失敗");
            }

            return View();
        }
    }
}