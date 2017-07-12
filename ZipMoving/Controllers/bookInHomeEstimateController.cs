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
        public ActionResult bookInHomeEstimate(bookInHomeEstimateModel model)
        {
            return View(model);
        }

        public ActionResult SubmitEverything(bookInHomeEstimateModel model)
        {
            if (ModelState.IsValid)
            {
              //  int id;
                //id = model.ToDatabase();
                //model.ToEmail(id);

                //return RedirectToAction("Index", "Index");
            }

            return View("bookInHomeEstimate", model);
        }
    }
}