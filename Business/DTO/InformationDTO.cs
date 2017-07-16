using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class InformationDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string TypeOfBuilding { get; set; }
        public bool UOElevatorFlag { get; set; }
        public string UOElevatorDesc { get; set; }
        public bool UOStairsFlag { get; set; }
        public string UOStairsDesc { get; set; }
        public string TypeOfAccess { get; set; }
        public bool FullSelfPack { get; set; }
        public string SizeOfBuilding { get; set; }
        public string RegureasCOI { get; set; }
    }
}
