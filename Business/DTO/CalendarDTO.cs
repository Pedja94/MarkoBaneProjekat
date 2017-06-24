using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class CalendarDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool LateAfternoon { get; set; }
    }
}
