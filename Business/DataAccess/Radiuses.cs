using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Radiuses
    {
        public static int Create(RadiusDTO radiusCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.Radius radius = new Business.Radius()
                {
                    RadiusNumber = radiusCreate.RadiusNumber,
                    Region = radiusCreate.Region
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

                    RadiusNumber = query.RadiusNumber,
                    Region = query.Region,
                };           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return radiusRead;
        }

        public static int ReadIdFromRegionAndNumber(string radiusRegion, decimal radiusNumber)
        {
            RadiusDTO radiusRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     where radius.RadiusNumber == radiusNumber
                     && radius.Region == radiusRegion
                     select radius).Single();

              
                radiusRead = new RadiusDTO()
                {
                    RadiusNumber = query.RadiusNumber,
                    Region = query.Region,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return radiusRead.Id;
        }

        public static List<RadiusDTO> ReadAll()
        {
            List<RadiusDTO> radiuses = new List<RadiusDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     select radius);

                foreach (Radius radius in query)
                {
                    RadiusDTO radiusRead = new RadiusDTO()
                    {
                        RadiusNumber = radius.RadiusNumber,
                        Region = radius.Region
                    };

                    radiuses.Add(radiusRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return radiuses;
        }

        public static void Update(RadiusDTO updateRadius)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     where radius.Id == updateRadius.Id
                     select radius).Single();

                query.RadiusNumber = updateRadius.RadiusNumber;
                query.Region = updateRadius.Region;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int radiusId)
        {
           try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     where radius.Id == radiusId
                     select radius).Single();

                db.Radius.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static int CreateFromToRadius(int idFrom, int idTo)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.FromToRadius fromToRadius = new Business.FromToRadius()
                {
                    RadiusFromId = idFrom,
                    RadiusToId = idTo
                };

                db.FromToRadius.InsertOnSubmit(fromToRadius);
                db.SubmitChanges();

                return fromToRadius.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static int FindFromToBetweenId(string regionFrom, int radiusNumberFrom, string regionTo, int radiusNumberTo)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                int idFrom = ReadIdFromRegionAndNumber(regionFrom, radiusNumberFrom);
                int idTo = ReadIdFromRegionAndNumber(regionTo, radiusNumberTo);

                var query =
                    (from fromtoradius in db.FromToRadius
                     where fromtoradius.RadiusFromId == idFrom
                     && fromtoradius.RadiusToId == idTo
                     select fromtoradius).Single();

                return (int)query.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

    }
}
