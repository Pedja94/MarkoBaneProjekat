using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.DataAccess;
using Business.DTO;

namespace ZipMoving.Models
{
    public class questionaireModel
    {
        public class AdditionalService
        {
            public bool isChecked { get; set; }
            public string text { get; set; }
        }

        [Required(ErrorMessage = "Field can't be empty")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string TypeOfMove { get; set; } //1 - In State, 2 - Out Of the State

        //Pickup Information
        [Required(ErrorMessage = "Select one of the types")]
        public string ToMove { get; set; } //1 - Everything, 2 - Just A Few Things

        [Required(ErrorMessage = "Select one of the types")]
        public string PresentAtPickup { get; set; } //1 - My self, 2 - Someone Else, 3 - I'm Not Sure

        [Required(ErrorMessage = "Field can't be empty")]
        public string ZIPPickup { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string AdditionalStopOffAtPickup { get; set; } //1 - Yes, 2 - No, 3 - Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string MovingFrom { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Storage

        [Required(ErrorMessage = "Select one of the types")]
        public string SizeOfOfficePickup { get; set; } //1 - 10x5, 2 - 10x10, 3 - 10x15, 4 - 10x20, 5 - 10x25, 6 - 10x30, 7 - >30

        [Required(ErrorMessage = "Select one of the types")]
        public string COIPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ElevatorPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string StairsPickup { get; set; } //1 - No, 2 - More than 10, 3 - More than 20, 4 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ParkingPickup { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away


        //Delivery Information
        [Required(ErrorMessage = "Field can't be empty")]
        public string ZIPDelivery { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string MovingTo { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Public Storage

        [Required(ErrorMessage = "Select one of the types")]
        public string COIDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ElevatorDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string StairsDelivery { get; set; } //1 - more than 10 steps, 2 - more than 20 steps, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ParkingDelivery { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away, 4 - I'm Not Sure

        //[Required(ErrorMessage = "Please tell us when do you want to move or select that you aren't sure")]
        public string WhenToMove { get; set; } //1 - Date, 2 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string PickupDateFlexibile { get; set; } //1 - No, 2 - up to 1-2 days, 3 - up to 2-4 days

        [Required(ErrorMessage = "Select one of the types")]
        public string AcceptDelivery { get; set; } //1 - Next day, 2 - two days, 3 - something else

        public List<AdditionalService> Additional { get; set; }

        //[Required(ErrorMessage = "Write something or select that you have nothing to specify")]
        public string SomethingElse { get; set; } //1 - text, 2 - nothing


        //functions
        public List<SelectListItem> GetAllSizes()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            string[] pom = { "10x5", "10x10", "10x15", "10x20", "10x25", "10x30", "> 30" };

            for (int i = 0; i < 7; i++)
            {
                listItems.Add(new SelectListItem
                {
                    Text = pom[i],
                    Value = pom[i]
                });
            }

            return listItems;
        }

        public questionaireModel()
        {
            Additional = new List<AdditionalService>();

            Additional.Add(new AdditionalService { isChecked = false, text = "Full Parking Service" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Storage Services (First Month Free)" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Piano Upright" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Baby Grand Piano" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Motorcycle" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Kayak / Canoe" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Pool Table" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Jacuzzi" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Safe (More Than 300lbs)" });
            Additional.Add(new AdditionalService { isChecked = false, text = "Aquarium" });
        }

        public bool ToEmail(int id)
        {
            OfferDTO offer = Offers.Read(id);

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
               new System.Net.Mail.MailAddress("zipmovingsender@outlook.com", "Questionaire with video Serial:" + offer.Serial),
               new System.Net.Mail.MailAddress("zipmovingreceiver@outlook.com"));
            m.Subject = "Questionaire with video Serial:" + offer.Serial;

            if (WhenToMove == null || WhenToMove == "")
                WhenToMove = "I'm Not Sure";

            if (SomethingElse == null || SomethingElse == "")
                SomethingElse = "Thank you, nothing";

            string AdditionalService = "";
            for (int i = 0; i < 10; i++)
            {
                if (Additional[i].isChecked == true)
                    AdditionalService += Additional[i].text + ", ";
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

            m.Body = string.Format(format, FullName, TypeOfMove, ToMove, PresentAtPickup,
                                   ZIPPickup, AdditionalStopOffAtPickup, MovingFrom, SizeOfOfficePickup,
                                   COIPickup, ElevatorPickup, StairsPickup, ParkingPickup,
                                   ZIPDelivery, MovingTo, COIDelivery, ElevatorDelivery, StairsDelivery,
                                   ParkingDelivery, WhenToMove, PickupDateFlexibile, AcceptDelivery,
                                   AdditionalService, SomethingElse);

            m.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
            smtp.Credentials = new System.Net.NetworkCredential("zipmovingsender@outlook.com", "sifra123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(m);

            return true;
        }

        public int ToDatabase()
        {
            InformationDTO infFrom = new InformationDTO()
            {
                ZipCode = ZIPPickup,
                TypeOfBuilding = MovingFrom,
                SizeOfBuilding = SizeOfOfficePickup,
                RegureasCOI = COIPickup,
                UOElevatorFlag = true,
                UOElevatorDesc = ElevatorPickup,
                UOStairsFlag = true,
                UOStairsDesc = StairsPickup,
                TypeOfAccess = ParkingPickup,
                Address = "",
                FullSelfPack = true,
            };

            InformationDTO infTo = new InformationDTO()
            {
                ZipCode = ZIPDelivery,
                TypeOfBuilding = MovingTo,
                RegureasCOI = COIDelivery,
                UOElevatorFlag = true,
                UOElevatorDesc = ElevatorDelivery,
                UOStairsFlag = true, 
                UOStairsDesc = StairsDelivery,
                TypeOfAccess = ParkingDelivery,
                Address = "",
                FullSelfPack = true,
            };

            string[] name = FullName.Split(' ');
            string AdditionalService = "";
            for (int i = 0; i < 10; i++)
            {
                if (Additional[i].isChecked == true)
                    AdditionalService += Additional[i].text + ", ";
            }
            AdditionalService += SomethingElse;

            OfferDTO offer = new OfferDTO()
            {
                Name = name[0],
                Surname = name[1],
                Type = TypeOfMove,
                WhatToMove = ToMove,
                WhoIsPresentAtPickup = PresentAtPickup,
                AdditionStop = AdditionalStopOffAtPickup,
                WhenToMove = WhenToMove,
                PickUpDateFlex = PickupDateFlexibile,
                WhenToAcceptdelivery = AcceptDelivery,
                AdditionalService = AdditionalService,
                Email = "",
                EstimateFlag = false,
                InforamtionFrom = infFrom,
                InforamtionTo = infTo,
                InventoryFlag = false,
                Serial = "",
                EndDate = null,
                StartDate = DateTime.Now,
                VideoFlag = true,
                VideoLink = "", //treba da se ubaci
            };

            int val = Offers.Create(offer);

            return val;
        }
    }
}