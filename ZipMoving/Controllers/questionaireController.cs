using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipMoving.Models;

namespace ZipMoving.Controllers
{
    public class questionaireController : Controller
    {
        // GET: questionaire
        public ActionResult questionaire()
        {
            questionaireModel model = new questionaireModel();
            return View(model);
        }

        public ActionResult SubmitEverything(questionaireModel model)
        {
            if (ModelState.IsValid)
            {
                int id;
                id = model.ToDatabase();
                //if (id != -1)
                    model.ToEmail(id);

                return RedirectToAction("Index", "Index");
            }

            return View("~/Views/questionaire/questionaire.cshtml", model);
        }
    }
}