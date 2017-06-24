using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Calendars
    {
        public static bool Create(DateTime from, DateTime to)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                for (DateTime date = from; date <= to; date.AddDays(1))
                {
                    Calendar calendar = new Calendar()
                    {
                        Date = date,
                        Morning = false,
                        Afternoon = false,
                        LateAfternoon = false,
                    };

                    db.Calendars.InsertOnSubmit(calendar);
                    db.SubmitChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public static List<CalendarDTO> Read(DateTime from, DateTime to)
        {
            List<CalendarDTO> calendar = new List<CalendarDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from c in db.Calendars
                     where c.Date >= @from && c.Date <= to
                     select c);

                foreach (Calendar date in query)
                {
                    CalendarDTO retVal = new CalendarDTO()
                    {
                        Date = date.Date,
                        Morning = date.Morning,
                        Afternoon = date.Afternoon,
                        LateAfternoon = date.LateAfternoon
                    };

                    calendar.Add(retVal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return calendar;
        }

        public static List<CalendarDTO> ReadAll()
        {
            List<CalendarDTO> calendar = new List<CalendarDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from c in db.Calendars
                     select c);

                foreach (Calendar date in query)
                {
                    CalendarDTO retVal = new CalendarDTO()
                    {
                        Date = date.Date,
                        Morning = date.Morning,
                        Afternoon = date.Afternoon,
                        LateAfternoon = date.LateAfternoon
                    };

                    calendar.Add(retVal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return calendar;
        }

        public static void Update(CalendarDTO updateCalendar)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from c in db.Calendars
                     where c.Id == updateCalendar.Id
                     select c).Single();

                query.Date = updateCalendar.Date;
                query.Morning = updateCalendar.Morning;
                query.Afternoon = updateCalendar.Afternoon;
                query.LateAfternoon = updateCalendar.LateAfternoon;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static bool Delete(DateTime from, DateTime to)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from c in db.Calendars
                     where c.Date >= @from && c.Date <= to
                     select c);

                foreach (Calendar calendar in query)
                {
                    db.Calendars.DeleteOnSubmit(calendar);
                    db.SubmitChanges();
                }

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
