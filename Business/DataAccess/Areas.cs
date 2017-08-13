using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Areas
    {
        public static int Create(AreaDTO areaCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.Area area = new Business.Area()
                {
                    Number = (decimal)areaCreate.Number,
                    RadiusId = areaCreate.RadiusId
                };

                db.Areas.InsertOnSubmit(area);
                db.SubmitChanges();

                return area.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static int CreateFromToArea(int idFrom, int idTo)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.FromToArea fromToArea = new Business.FromToArea()
                {
                    Id = 1,
                    AreaFromId = idFrom,
                    AreaToId = idTo
                };

                db.FromToAreas.InsertOnSubmit(fromToArea);
                db.SubmitChanges();

                return fromToArea.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static int FindFromToInsideId(int areaFromId, int areaToId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from fromtoarea in db.FromToAreas
                     where fromtoarea.AreaFromId == areaFromId
                     && fromtoarea.AreaToId == areaToId
                     select fromtoarea).Single();

                return (int)query.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static AreaDTO Read(int areaId)
        {
            AreaDTO areaRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     where area.Id == areaId
                     select area).Single();

                areaRead = new AreaDTO()
                {
                    Id = query.Id,
                    Number = query.Number,
                    RadiusId = (int)query.RadiusId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return areaRead;
        }

        public static int ReadIdFromNumberAndRadiusId(int areaNumber, int radiusId)
        {

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     where area.Number == areaNumber 
                     && area.RadiusId == radiusId 
                     select area).Single();

                return query.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static List<AreaDTO> ReadAll()
        {
            List<AreaDTO> areas = new List<AreaDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     select area);

                foreach (Area area in query)
                {
                    AreaDTO areaRead = new AreaDTO()
                    {
                        Number = area.Number,
                        RadiusId = (int)area.RadiusId
                    };

                    areas.Add(areaRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return areas;
        }

        public static void Update(AreaDTO updateArea)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     where area.Id == updateArea.Id
                     select area).Single();

                query.Number = (decimal)updateArea.Number;
                query.RadiusId = updateArea.RadiusId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int areaId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     where area.Id == areaId
                     select area).Single();

                db.Areas.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static RadiusDTO ReadRadiusFromArea(AreaDTO area)
        {
            RadiusDTO radiusRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from radius in db.Radius
                     where radius.Id == area.RadiusId
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

    }
}
