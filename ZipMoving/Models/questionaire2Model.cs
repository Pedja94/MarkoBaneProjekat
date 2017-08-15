using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.DataAccess;
using Business.DTO;
using System.Collections;

namespace ZipMoving.Models
{
    public class questionaire2Model
    {
        public class AdditionalService
        {
            public bool isChecked { get; set; }
            public string text { get; set; }
        }

        [Required(ErrorMessage = "Field can't be empty")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Address { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string TypeOfMove { get; set; } //1 - In State, 2 - Out Of the State

        //Pickup Information
        [Required(ErrorMessage = "Select one of the types")]
        public string ToMove { get; set; } //1 - Everything, 2 - Just A Few Things

        [Required(ErrorMessage = "Select one of the types")]
        public string PresentAtPickup { get; set; } //1 - My self, 2 - Someone Else, 3 - I'm Not Sure

        [CustomZipCodePickupValidator]
        public string ZIPPickup { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string AdditionalStopOffAtPickup { get; set; } //1 - Yes, 2 - No, 3 - Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string MovingFrom { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Storage

        //[Required(ErrorMessage = "Select one of the types")]
        public string SizeOfOfficePickup { get; set; } //1 - 10x5, 2 - 10x10, 3 - 10x15, 4 - 10x20, 5 - 10x25, 6 - 10x30, 7 - >30

        public string HowFullIsStoragePickup { get; set; }

        //[Required(ErrorMessage = "Select one of the types")]
        public string COIPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        //[Required(ErrorMessage = "Select one of the types")]
        public string ElevatorPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string StairsPickup { get; set; } //1 - No, 2 - More than 10, 3 - More than 20, 4 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ParkingPickup { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away

        //Delivery Information
        [CustomZipCodeDeliveryValidator]
        public string ZIPDelivery { get; set; }

        [Required(ErrorMessage = "Select one of the types")]
        public string AdditionalStopOffAtDelivery { get; set; } //1 - Yes, 2 - No, 3 - Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string MovingTo { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Public Storage

        //[Required(ErrorMessage = "Select one of the types")]
        public string COIDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        //[Required(ErrorMessage = "Select one of the types")]
        public string ElevatorDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string StairsDelivery { get; set; } //1 - more than 10 steps, 2 - more than 20 steps, 3 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string ParkingDelivery { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away, 4 - I'm Not Sure

        [Required(ErrorMessage = "Please tell us when do you want to move or select that you aren't sure")]
        public string WhenToMove { get; set; } //1 - Date, 2 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string PickupDateFlexibile { get; set; } //1 - No, 2 - up to 1-2 days, 3 - up to 2-4 days

        [Required(ErrorMessage = "Select one of the types")]
        public string AcceptDelivery { get; set; } //1 - Next day, 2 - two days, 3 - something else

        public List<AdditionalService> Additional { get; set; }

        [Required(ErrorMessage = "Write something or select that you have nothing to specify")]
        public string SomethingElse { get; set; } //1 - text, 2 - nothing

        //Inventory
        public Hashtable InventoryHashTable { get; set; }

        public List<SelectListItem> AllRoomsNames()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<RoomDTO> rooms = Rooms.ReadAll();

            for (int i = 0; i < rooms.Count; i++)
            {
                listItems.Add(new SelectListItem
                {
                    Text = rooms[i].Name,
                    Value = rooms[i].Id.ToString()
                });
            }

            return listItems;
        }

        public List<ItemDTO> ItemsInRoom(string RoomId)
        {
            return Items.ReadAllInRoom(Int32.Parse(RoomId));
        }

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

        public questionaire2Model()
        {
            InventoryHashTable = new Hashtable();
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

        public Hashtable CreateInventoryOffer(int soba, int[] niz)
        {
            //ako je vec napravljena hash tabela, vratiti je iz sesije
            if (HttpContext.Current.Session["InventoryOffer"] != null)
                InventoryHashTable = (Hashtable)HttpContext.Current.Session["InventoryOffer"];

            if (!InventoryHashTable.ContainsKey(soba))
            {
                InventoryHashTable.Add(soba, niz);
            }
            else
            {
                InventoryHashTable.Remove(soba);
                InventoryHashTable.Add(soba, niz);
            }

            return InventoryHashTable;
        }

        public Hashtable RemoveRoomFromHash(int soba)
        {
            InventoryHashTable = (Hashtable)HttpContext.Current.Session["InventoryOffer"];
            InventoryHashTable.Remove(soba);

            return InventoryHashTable;
        }

        public string InventoryOfferString()
        {
            Hashtable tabela = (Hashtable)HttpContext.Current.Session["InventoryOffer"];
            string InventoryOffer = "";

            foreach (DictionaryEntry pair in tabela)
            {
                InventoryOffer += "<BR>";
                RoomDTO soba = Rooms.Read((int)pair.Key);
                InventoryOffer += "<B>" + soba.Name + ": </B>";

                List<ItemDTO> items = Items.ReadAllInRoom((int)pair.Key);
                string[] ItemsNames = new string[items.Count];
                int j = 0;
                foreach (ItemDTO item in items)
                {
                    ItemsNames[j++] = item.Name;
                }

                for (j = 0; j < ItemsNames.Length; j++)
                {
                    if (((int[])pair.Value)[j] != 0)
                        InventoryOffer += ItemsNames[j] + " <B>x" + ((int[])pair.Value)[j] + "</B>; ";
                }
                InventoryOffer += "</BR>";
            }

            return InventoryOffer;

        }


        public string TotalCostString()
        {
            Hashtable tabela = (Hashtable)HttpContext.Current.Session["InventoryOffer"];

            string totalCostString = "";

            List<ItemDTO> itemsWithPackingFee = new List<ItemDTO>();
            List<ItemDTO> itemsWithAdditionalFee = new List<ItemDTO>();
            int totalWeight = 0;

            int totalCost = 0;

            /*totalCostString += "Packing fee for " + item.Name + " - " + item.Packing + "$";*/

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            foreach (DictionaryEntry pair in tabela)
            {
                RoomDTO soba = Rooms.Read((int)pair.Key);

                List<ItemDTO> items = Items.ReadAllInRoom((int)pair.Key);

                int j = 0;

                foreach (ItemDTO item in items)
                {
                    totalWeight += (int)item.Weight * ((int[])pair.Value)[j];
                    if (item.Packing > 0)
                        itemsWithPackingFee.Add(item);
                    if (item.AdditionalFee > 0)
                        itemsWithAdditionalFee.Add(item);
                    j++;
                }
            }

            ZipCodeDTO zipCodeFrom = ZipCodes.ReadFromZipCodeString(ZIPPickup);
            ZipCodeDTO zipCodeTo = ZipCodes.ReadFromZipCodeString(ZIPDelivery);

            totalCostString += "TOTAL COST\n\n";

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";

            int pricePerLbs = ZipCodes.ReturnPriceFromZipCodesAndLbs(zipCodeFrom, zipCodeTo, totalWeight);
            totalCost += pricePerLbs;

            totalCostString += "Total weight: " + totalWeight.ToString() + " - " + pricePerLbs + "\n";

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";

            foreach (ItemDTO item in itemsWithPackingFee)
            {
                totalCostString += "Packing fee for " + item.Name + " - " + item.Packing.ToString() + "$\n";
                totalCost += (int)item.Packing;
            }

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";

            foreach (ItemDTO item in itemsWithAdditionalFee)
            {
                totalCostString += "Aditional fee for " + item.Name + " - " + item.AdditionalFee.ToString() + "$\n";
                totalCost += (int)item.AdditionalFee;
            }

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";

            if (AdditionalStopOffAtPickup.Equals("Yes"))
            {
                totalCostString += "Extra pickup location - 80$\n";
                totalCost += 80;
            }

            if (ElevatorPickup.Equals("Yes"))
            {
                int elevatorCost = totalWeight / 100;
                elevatorCost = elevatorCost * 2;
                totalCost += elevatorCost;
                totalCostString += "Elevator use at pickup - " + elevatorCost.ToString() + "$\n";
            }

            if (StairsPickup.Contains("10"))
            {
                int stairsCost = totalWeight / 100;
                stairsCost = (int)(stairsCost * 1.5);
                totalCost += stairsCost;
                totalCostString += "Stairs besides inside of facility at pickup- " + stairsCost.ToString() + "$\n";
            }
            else if (StairsPickup.Contains("20"))
            {
                int stairsCost = totalWeight / 100;
                stairsCost = stairsCost * 3;
                totalCost += stairsCost;
                totalCostString += "Stairs besides inside of facility at pickup - " + stairsCost.ToString() + "$\n";
            }
            else if (StairsPickup.Contains("Sure"))
            {
                totalCostString += "Stairs besides inside of facility at pickup - Every 10 steps 1.5$ per 100lbs\n";
            }

            if (ParkingPickup.Contains("Away"))
            {
                int longCarryCost = totalWeight / 100;
                longCarryCost = (int)(longCarryCost * 1.5);
                totalCost += longCarryCost;
                totalCostString += "Long carry (over 75ft) at pickup - " + longCarryCost.ToString() + "$\n";
            } 
            else if (ParkingPickup.Contains("Sure"))
            {
                totalCostString += "Long carry (over 75ft) at pickup - Every 75ft 1.5$ per 100lbs\n";
            }

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";
            
            if (AdditionalStopOffAtDelivery.Equals("Yes"))
            {
                totalCostString += "Extra delivery location - 80$\n";
                totalCost += 80;
            }
            

            if (ElevatorDelivery.Equals("Yes"))
            {
                int elevatorCost = totalWeight / 100;
                elevatorCost = elevatorCost * 2;
                totalCost += elevatorCost;
                totalCostString += "Elevator use at delivery - " + elevatorCost.ToString() + "$\n";
            }

            if (StairsDelivery.Contains("10"))
            {
                int stairsCost = totalWeight / 100;
                stairsCost = (int)(stairsCost * 1.5);
                totalCost += stairsCost;
                totalCostString += "Stairs besides inside of facility at delivery - " + stairsCost.ToString() + "$\n";
            }
            else if (StairsDelivery.Contains("20"))
            {
                int stairsCost = totalWeight / 100;
                stairsCost = stairsCost * 3;
                totalCost += stairsCost;
                totalCostString += "Stairs besides inside of facility at delivery - " + stairsCost.ToString() + "$\n";
            }
            else if (StairsDelivery.Contains("Sure"))
            {
                totalCostString += "Stairs besides inside of facility at delivery - Every 10 steps 1.5$ per 100lbs\n";
            }

            if (ParkingDelivery.Contains("Away"))
            {
                int longCarryCost = totalWeight / 100;
                longCarryCost = (int)(longCarryCost * 1.5);
                totalCost += longCarryCost;
                totalCostString += "Long carry (over 75ft) at delivery - " + longCarryCost.ToString() + "$\n";
            }
            else if (ParkingDelivery.Contains("Sure"))
            {
                totalCostString += "Long carry(over 75ft) at delivery - Every 75ft 1.5$ per 100lbs\n";
            }

            for (int i = 0; i < 80; i++)
                totalCostString += "_";

            totalCostString += "\n";

            if (Additional[0].isChecked == true)
            {
                int fullPackingServiceCost = totalWeight / 100;
                fullPackingServiceCost = fullPackingServiceCost * 23;
                totalCost += fullPackingServiceCost;
                totalCostString += "Full packing service - " + fullPackingServiceCost.ToString() + "$\n";
            }

            if (Additional[1].isChecked == true)
            {
                totalCostString += "Storage Services (First Month Free) - 56$ per 700Lbs\n";
            }

            for (int i = 0; i < 80; i++)
                totalCostString += "_";
            
            totalCostString += "\nTotal estimate price: " + totalCost.ToString() + "\n";


            return totalCostString;

        }

        public bool ToEmail(int id)
        {
            //OfferDTO offer = Offers.Read(id);

            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
               new System.Net.Mail.MailAddress("zipmovingsender@outlook.com", "Questionaire with Inventory"),
               new System.Net.Mail.MailAddress("zipmovingreceiver@outlook.com"));
            m.Subject = "Questionaire with Inventory";

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
                            "<B>Something else:</B> {22}</BR>" +
                            "<B>--------------------INVENTORY--------------------</B></BR>" +
                            "{23}</BR>";

            m.Body = string.Format(format, FullName, TypeOfMove, ToMove, PresentAtPickup,
                                   ZIPPickup, AdditionalStopOffAtPickup, MovingFrom, SizeOfOfficePickup,
                                   COIPickup, ElevatorPickup, StairsPickup, ParkingPickup,
                                   ZIPDelivery, MovingTo, COIDelivery, ElevatorDelivery, StairsDelivery,
                                   ParkingDelivery, WhenToMove, PickupDateFlexibile, AcceptDelivery,
                                   AdditionalService, SomethingElse, InventoryOfferString());

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