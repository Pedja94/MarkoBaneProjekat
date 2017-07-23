using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public class Radiuses
    {
        public static int Create(RadiusDTO radiusCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Radius radius = new Radius()
                {
                    Name = radiusCreate.Name,
                    StateNums = radiusCreate.StateNums,
                };

                db.Radius.InsertOnSubmit(radius);
                db.SubmitChanges();

                return radius.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static RadiusDTO Read(int RadiusId)
        {
            RadiusDTO radiusRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     where radius.Id == RadiusId
                     select radius).Single();

                radiusRead = new RadiusDTO()
                {
                    Id = query.Id,
                    Name = query.Name,
                    StateNums = query.StateNums
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return radiusRead;
        }

        public static List<RadiusDTO> ReadAll()
        {
            List<RadiusDTO> Radiuses = new List<RadiusDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from Radius in db.Radius
                     select Radius);

                foreach (Radius Radius in query)
                {
                    RadiusDTO RadiusRead = new RadiusDTO()
                    {
                        Id = Radius.Id,
                        StateNums = Radius.StateNums,
                        Name = Radius.Name
                    };

                    Radiuses.Add(RadiusRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Radiuses;
        }

        public static void Update(RadiusDTO updateRadius)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from Radius in db.Radius
                     where Radius.Id == updateRadius.Id
                     select Radius).Single();

                query.Name = updateRadius.Name;
                query.StateNums = updateRadius.StateNums;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int RadiusId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from Radius in db.Radius
                     where Radius.Id == RadiusId
                     select Radius).Single();

                db.Radius.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
