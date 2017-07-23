using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class PricePerMileDTO
    {
        public int Id { get; set; }
        public int MilesFrom { get; set; }
        public int MilesTo { get; set; }
        public decimal Cost { get; set; }
        public int? MovingPricesId { get; set; }
    }
}
