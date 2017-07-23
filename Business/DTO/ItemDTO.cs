using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconLink { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Packing  { get; set; }
        public Nullable<decimal> AdditionalFee { get; set; }
        public System.Nullable<decimal> Dimension { get; set; }
    }
}
