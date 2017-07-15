using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipMoving.Models;

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
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                new System.Net.Mail.MailAddress("zipmovingsender@outlook.com", "New message!!!"),
                new System.Net.Mail.MailAddress("zipmovingreceiver@outlook.com"));
                m.Subject = "New message";

                string AdditionalService = "";
                for (int i = 0; i < 10; i++)
                {
                    if (model.Additional[i].isChecked == true)
                        AdditionalService += model.Additional[i].text + ", ";
                }

                if (AdditionalService != "")
                    AdditionalService = AdditionalService.Remove(AdditionalService.Length - 2, 2);

                string format = "<B>Full name:</B> {0}</BR>" +
                                "<B>Type of move:</B> {1}</BR></BR>" +
                                "<B>--------------------PICKUP INFORMATION--------------------</B></BR>" +
                                "<B>To move:</B> {2}</BR>" +
                                "<B>Present on the pickup location:</B> {3}</BR>" +
                                "<B>ZIP of the pickup location:</B> {4}</BR>" +
                                "<B>Additional stop off:</B> {5}</BR>" +
                                "<B>Moving from:</B> {6}</BR>" +
                                "<B>Size of office garage:</B> {7}</BR>" +
                                "<B>COI regulation:</B> {8}</BR>" +
                                "<B>Use of the elevator:</B> {9}</BR>" +
                                "<B>Use of stairs:</B> {10}</BR>" +
                                "<B>Parking on the pickup:</B> {11}</BR></BR>" +
                                "<B>--------------------DELIVERY INFORMATION--------------------</B></BR>" +
                                "<B>ZIP of the delivery location:</B> {12}</BR>" +
                                "<B>Moving to:</B> {13}</BR>" +
                                "<B>COI regulation:</B> {14}</BR>" +
                                "<B>Use of the elevator:</B> {15}</BR>" +
                                "<B>Use of stairs:</B> {16}</BR>" +
                                "<B>Parking on the delivery:</B> {17}</BR>" +
                                "<B>When to move:</B> {18}</BR>" +
                                "<B>Pickup date flexible:</B> {19}</BR>" +
                                "<B>When client can accept the delivery:</B> {20}</BR>" +
                                "<B>Additional service:</B> {21}</BR>" +
                                "<B>Something else:</B> {22}</BR>";

                m.Body = string.Format(format, model.FullName, model.TypeOfMove, model.ToMove, model.PresentAtPickup,
                                       model.ZIPPickup, model.AdditionalStopOffAtPickup, model.MovingFrom, model.SizeOfOfficePickup,
                                       model.COIPickup, model.ElevatorPickup, model.StairsPickup, model.ParkingPickup,
                                       model.ZIPDelivery, model.MovingTo, model.COIDelivery, model.ElevatorDelivery, model.StairsDelivery,
                                       model.ParkingDelivery, model.WhenToMove, model.PickupDateFlexibile, model.AcceptDelivery,
                                       AdditionalService, model.SomethingElse);

                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
                smtp.Credentials = new System.Net.NetworkCredential("zipmovingsender@outlook.com", "sifra123");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(m);
            }

            return View("~/Views/questionaire2/questionaire2.cshtml", model);
        }
    }
}