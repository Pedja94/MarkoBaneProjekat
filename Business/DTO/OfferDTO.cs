using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public bool EstimateFlag { get; set; }
        public Nullable<DateTime> EstimateDate { get; set; }
        public Nullable<decimal> EstimateTime { get; set; }
        public int? AdminId { get; set; }
        public InformationDTO InforamtionFrom { get; set; }
        public InformationDTO InforamtionTo { get; set; }
        public string WhatToMove { get; set; }
        public string WhoIsPresentAtPickup { get; set; }
        public string AdditionStop { get; set; }
        public string AdditionalService { get; set; }
        public string HowDidYouFindUs { get; set; }
        public bool VideoFlag { get; set; }
        public string VideoLink { get; set; }
        public bool InventoryFlag { get; set; }
        public string PickUpDateFlex { get; set; }
        public string WhenToAcceptdelivery { get; set; }
        public string WhenToMove { get; set; }
        public List<int> RoomIds { get; set; }
        public List<int> ItemIds { get; set; }

        public OfferDTO()
        {
            RoomIds = new List<int>();
            ItemIds = new List<int>();
        }
    }
}
