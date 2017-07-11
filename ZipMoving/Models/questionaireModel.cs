using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZipMoving.Models
{
    public class questionaireModel
    {
        public string FullName { get; set; }
        public string TypeOfMove { get; set; } //1 - In State, 2 - Out Of the State

        //Pickup Information
        public string ToMove { get; set; } //1 - Everything, 2 - Just A Few Things
        public string PresentAtPickup { get; set; } //1 - My self, 2 - Someone Else, 3 - I'm Not Sure
        public string ZIPPickup { get; set; }
        public string AdditionalStopOffAtPickup { get; set; } //1 - Yes, 2 - No, 3 - Not Sure
        public string MovingFrom { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Storage
        public string SizeOfOfficePickup { get; set; } //1 - 10x5, 2 - 10x10, 3 - 10x15, 4 - 10x20, 5 - 10x25, 6 - 10x30, 7 - >30
        public string COIPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure
        public string ElevatorPickup { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure
        public string StairsPickup { get; set; } //1 - No, 2 - More than 10, 3 - More than 20, 4 - I'm Not Sure
        public string ParkingPickup { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away

        //Delivery Information
        public string ZIPDelivery { get; set; }
        public string MovingTo { get; set; } //1 - House, 2 - Townhouse, 3 - Apartment, 4 - Office, 5 - Public Storage
        public string COIDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure
        public string ElevatorDelivery { get; set; } //1 - Yes, 2 - No, 3 - I'm Not Sure
        public string StairsDelivery { get; set; } //1 - more than 10 steps, 2 - more than 20 steps, 3 - I'm Not Sure
        public string ParkingDelivery { get; set; } //1 - Private Driveway, 2 - Street Parking in Front, 3 - Street Parking Further Away, 4 - I'm Not Sure
        public string WhenToMove { get; set; } //1 - Date, 2 - I'm Not Sure
        public string PickupDateFlexibile { get; set; } //1 - No, 2 - up to 1-2 days, 3 - up to 2-4 days
        public string AcceptDelivery { get; set; } //1 - Next day, 2 - two days, 3 - something else
        public string AdditionalService { get; set; }
        public string SomethingElse { get; set; } //1 - text, 2 - nothing

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



    }
}