using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZipMoving.Models
{
    public class TotalCostData
    {
        public string AdditionalStopOffAtPickup { get; set; }
        public string ElevatorPickup { get; set; }
        public string StairsPickup { get; set; }
        public string ParkingPickup { get; set; }
        public string HowFullIsStoragePickup { get; set; }
        public string AdditionalStopOffAtDelivery { get; set; }
        public string ElevatorDelivery { get; set; }
        public string StairsDelivery { get; set; }
        public string ParkingDelivery { get; set; }
        public bool FullPackingService { get; set; }
        public bool StorageService { get; set; }
        public string StoragePickupDimension { get; set; }
    }
}