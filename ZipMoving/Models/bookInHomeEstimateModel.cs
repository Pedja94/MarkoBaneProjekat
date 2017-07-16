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
        [Required(ErrorMessage = "You have to pick a date.")]
        public string estimateDate { get; set; }
        public int estimateTime { get; set; } //0 - morning 1 - afternoon 2 - late afternoon
        [Required(ErrorMessage = "Field can't be empty")]
        public string FullName { get; set; }
        [CustomEmailPhoneValidator]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string ZIPCode { get; set; }

        public List<string> takenDates { get; set; }
        public List<string> partialyTakenDates { get; set; }
        public List<int> partialyTakenDatesMorning { get; set; }
        public List<int> partialyTakenDatesAfternoon { get; set; }
        public List<int> partialyTakenDatesLateAfternoon { get; set; }
        public string today { get; set; }


        public bookInHomeEstimateModel()
        {
            takenDates = new List<string>();
            partialyTakenDates = new List<string>();
            partialyTakenDatesMorning = new List<int>();
            partialyTakenDatesAfternoon = new List<int>();
            partialyTakenDatesLateAfternoon = new List<int>();
        }

        //functions
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
            DateTime date = new DateTime(Int32.Parse(estimateDate.Substring(0, 4)),
                Int32.Parse(estimateDate.Substring(5, 2)), Int32.Parse(estimateDate.Substring(8, 2)));

            OfferDTO offer = new OfferDTO()
            {
                Serial = "",
                Name = name[0],
                Surname = name[1],
                Email = Email,
                Type = "",
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
                new System.Net.Mail.MailAddress("zipmovingreceiver@outlook.com"));
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

        public void LoadDates()
        {
            DateTime from, to;
            List<CalendarDTO> dates = new List<CalendarDTO>();

            from = DateTime.Today;
            to = from.AddYears(1);
            dates = Calendars.Read(from, to);

            for (int i = 0; i < dates.Count; i++)
            {
                if (dates[i].Morning && dates[i].Afternoon && dates[i].LateAfternoon)
                {
                    takenDates.Add(CreateString(dates[i].Date));
                }
                else
                {
                    partialyTakenDates.Add(CreateString(dates[i].Date));
                    partialyTakenDatesMorning.Add(Convert.ToInt32(dates[i].Morning));
                    partialyTakenDatesAfternoon.Add(Convert.ToInt32(dates[i].Afternoon));
                    partialyTakenDatesLateAfternoon.Add(Convert.ToInt32(dates[i].LateAfternoon));
                }
            }

            today = CreateString(DateTime.Today);
        }

        private string CreateString(DateTime date)
        {
            string s;

            s = date.Year.ToString() + "-";
            if (date.Month < 10)
            {
                s += "0" + date.Month.ToString() + "-";
            }
            else
            {
                s += date.Month.ToString() + "-";
            }
            if (date.Day < 10)
            {
                s += "0" + date.Day.ToString();
            }
            else
            {
                s += date.Day.ToString();
            }

            return s;
        }
    }
}