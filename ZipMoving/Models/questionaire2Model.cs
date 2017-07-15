using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Required(ErrorMessage = "Please tell us when do you want to move or select that you aren't sure")]
        public string WhenToMove { get; set; } //1 - Date, 2 - I'm Not Sure

        [Required(ErrorMessage = "Select one of the types")]
        public string PickupDateFlexibile { get; set; } //1 - No, 2 - up to 1-2 days, 3 - up to 2-4 days

        [Required(ErrorMessage = "Select one of the types")]
        public string AcceptDelivery { get; set; } //1 - Next day, 2 - two days, 3 - something else

        public List<AdditionalService> Additional { get; set; }

        [Required(ErrorMessage = "Write something or select that you have nothing to specify")]
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

        public questionaire2Model()
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
    }
}