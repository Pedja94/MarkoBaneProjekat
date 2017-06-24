using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
     public class ZipCodeDTO
     {
        public int Id { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
     }
}
