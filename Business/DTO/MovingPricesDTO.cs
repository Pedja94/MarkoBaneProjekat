using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class MovingPricesDTO
    {
        public int Id { get; set; }
        public string StateNums { get; set; }
        public int? FromToId { get; set; }
        public List<PricePerMileDTO> listOfPrices { get; set; }

        public MovingPricesDTO()
        {
            listOfPrices = new List<PricePerMileDTO>();
        }
    }
}
