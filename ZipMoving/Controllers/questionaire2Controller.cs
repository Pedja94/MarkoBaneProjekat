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
                int id;
                id = model.ToDatabase();
                if (id != -1)
                    model.ToEmail(id);

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

        //javascript matrica se prosledjuje kao niz stringova
        public ActionResult CollectInventory(string[] niz, questionaire2Model model)
        {
            Session["InventoryOffer"] = model.CreateInventoryOffer(niz);
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}