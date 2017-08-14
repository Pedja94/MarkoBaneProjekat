using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipMoving.Models;
using Business.DataAccess;
using Business.DTO;

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
                //int id;
                //id = model.ToDatabase();
               // if (id != -1)
                model.ToEmail(0);

                return RedirectToAction("Index", "Index");
            }

            return View("~/Views/questionaire/questionaire.cshtml", model);
        }

        [HttpGet]
        public JsonResult CheckPickupZipCode(string code)
        {
            ZipCodeDTO zipCode = ZipCodes.ReadFromZipCodeString(code);
            bool res = false;
            if (zipCode != null)
                res = true;

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckDeliveryZipCode(string codeFrom, string codeTo)
        {
            ZipCodeDTO zipCodeFrom = ZipCodes.ReadFromZipCodeString(codeFrom);
            ZipCodeDTO zipCodeTo = ZipCodes.ReadFromZipCodeString(codeTo);

            if (zipCodeFrom != null && zipCodeTo != null)
            {
                if (ZipCodes.PossibleMoving(zipCodeFrom, zipCodeTo))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}