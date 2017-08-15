using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZipMoving.Models;
using Business.DataAccess;
using Business.DTO;

namespace ZipMoving.Controllers
{
    public class questionaire2Controller : Controller
    {
        // GET: questionaire2
        public ActionResult questionaire2()
        {
            questionaire2Model model = new questionaire2Model();
            return View(model);
        }

        public ActionResult SubmitEverything(questionaire2Model model)
        {
            if (ModelState.IsValid)
            {
                //int id;
                //id = model.ToDatabase();
                //if (id != -1)
                model.ToEmail(0);

                return RedirectToAction("Index", "Index");
            }

            return View("~/Views/questionaire2/questionaire2.cshtml", model);
            
        }
        
        public ActionResult AddEditRoomJson(string room)
        {
            RoomDTO SelectedRoom = Rooms.Read(Int32.Parse(room));
            List<ItemDTO> items = Business.DataAccess.Items.ReadAllInRoom(Int32.Parse(room));
            return Json(new { items = items, SelectedRoom = SelectedRoom }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CollectInventory(int soba, int[] niz, questionaire2Model model)
        {
            Session["InventoryOffer"] = model.CreateInventoryOffer(soba, niz);
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveRoomJson(int soba, questionaire2Model model)
        {
            RoomDTO SelectedRoom = Rooms.Read(soba);
            Session["InventoryOffer"] = model.RemoveRoomFromHash(soba);
            //List<ItemDTO> items = Business.DataAccess.Items.ReadAllInRoom(Int32.Parse(room));
            return Json(new { SelectedRoom = SelectedRoom }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckPickupZipCode(string code)
        {
            ZipCodeDTO zipCode = ZipCodes.ReadFromZipCodeString(code);
            bool res = false;
            if (zipCode != null)
                res = true;

            return Json(res , JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public JsonResult ReturnTotalCost(TotalCostData data)
        {
            questionaire2Model model = new questionaire2Model();
            string retVal = model.TotalCostString();

            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateEmail(string email)
        {
            CustomEmailPhoneValidator validator = new CustomEmailPhoneValidator();
            
            return Json(validator.IsValid(email), JsonRequestBehavior.AllowGet);
        }
    }
}