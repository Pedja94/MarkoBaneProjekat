using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Business.DTO;
using Business.DataAccess;

namespace ZipMoving.Models
{
    public class bookInHomeEstimateModel
    {
        public string estimateDate { get; set; }
        public int estimateTime { get; set; } //0 - morning 1 - afternoon 2 - late afternoon
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZIPCode { get; set; }

        public int ToDatabase()
        {
            InformationDTO infFrom = new InformationDTO()
            {
                Address = Address,
                ZipCode = ZIPCode,
                UOElevatorFlag = false,
                UOStairsFlag = false,
                FullSelfPack = false
            };

            string[] name = FullName.Split(' ');
            Random rnd = new Random(DateTime.Now.Millisecond);
            string srNum = "ZM" + (rnd.Next() % 10000).ToString();
            DateTime date = new DateTime(Int32.Parse(estimateDate.Substring(0, 4)),
                Int32.Parse(estimateDate.Substring(5, 2)), Int32.Parse(estimateDate.Substring(8, 2)));

            OfferDTO offer = new OfferDTO()
            {
                Serial = srNum,
                Name = name[0],
                Surname = name[1],
                Email = Email,
                Type = 0,
                StartDate = DateTime.Now,
                EstimateFlag = true,
                EstimateDate = date,
                EstimateTime = estimateTime,
                InforamtionFrom = infFrom,
                VideoFlag = false, 
                InventoryFlag = false
            };

            int val = Offers.Create(offer);

            return val;
        }

        public bool ToEmail(int id)
        {
            OfferDTO offer = Offers.Read(id);

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                new System.Net.Mail.MailAddress("zipmovingsender@outlook.com", "Book in home estimate Serial:" + offer.Serial),
                new System.Net.Mail.MailAddress("djape94@gmail.com"));
            m.Subject = "Book in home estimate Serial:" + offer.Serial;

            string format = "<B>Full name:</B> {0}</BR>" +
                            "<B>Address:</B> {1}</BR>" +
                            "<B>Zip Code:</B> {2}</BR>" +
                            "<B>E-mail/Phone number:</B> {3}</BR>" +
                            "<B>Estimate date:</B> {4}</BR>" +
                            "<B>Estimate time:</B> ";

            if (estimateTime == 0)
                format += "Morning</BR>";
            else if (estimateTime == 1)
                format += "Afternoon</BR>";
            else if (estimateTime == 2)
                format += "Late afternoon</BR>";

            m.Body = string.Format(format, FullName, Address, ZIPCode, Email, estimateDate.ToString());

            m.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
            smtp.Credentials = new System.Net.NetworkCredential("zipmovingsender@outlook.com", "sifra123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(m);

            return true;
        }
    }
}