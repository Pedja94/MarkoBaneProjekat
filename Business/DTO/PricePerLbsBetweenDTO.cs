using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class PricePerLbsBetweenDTO
    {
        public int Id { get; set; }
        public int LbsFrom { get; set; }
        public int LbsTo { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int FromTo { get; set; }
    }
}
