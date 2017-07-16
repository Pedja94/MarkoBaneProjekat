using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipMoving.Models;

namespace ZipMoving.Controllers
{
    public class bookInHomeEstimateController : Controller
    {
        // GET: bookInHomeEstimate
        public ActionResult bookInHomeEstimate()
        {
            bookInHomeEstimateModel model = new bookInHomeEstimateModel();
            model.LoadDates();
            return View(model);
        }

        public ActionResult SubmitEverything(bookInHomeEstimateModel model)
        {
            if (ModelState.IsValid)
            {
                int id;
                id = model.ToDatabase();
                if (id != -1)
                    model.ToEmail(id);

                return RedirectToAction("Index", "Index");
            }

            model.LoadDates();
            return View("bookInHomeEstimate", model);
        }
    }
}