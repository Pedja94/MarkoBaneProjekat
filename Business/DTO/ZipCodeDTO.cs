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
        public string Code { get; set; }
        public int AreaId { get; set; }
    }
}
