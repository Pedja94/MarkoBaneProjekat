using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public class PricePerMiles
    {
        public static int Create(PricePerMileDTO PricePerMileCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                PricePerMile PricePerMile = new PricePerMile()
                {
                    MilesFrom = PricePerMileCreate.MilesFrom,
                    MilesTo = PricePerMileCreate.MilesTo,
                    Cost = PricePerMileCreate.Cost,
                    MovingPricesId = PricePerMileCreate.MovingPricesId
                };

                db.PricePerMiles.InsertOnSubmit(PricePerMile);
                db.SubmitChanges();

                return PricePerMile.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static PricePerMileDTO Read(int PricePerMileId)
        {
            PricePerMileDTO PricePerMileRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from PricePerMile in db.PricePerMiles
                     where PricePerMile.Id == PricePerMileId
                     select PricePerMile).Single();

                PricePerMileRead = new PricePerMileDTO()
                {
                    Cost = query.Cost,
                    Id = query.Id,
                    MilesFrom = query.MilesFrom,
                    MilesTo = query.MilesTo,
                    MovingPricesId = query.MovingPricesId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return PricePerMileRead;
        }

        public static void Update(PricePerMileDTO updatePricePerMile)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from PricePerMile in db.PricePerMiles
                     where PricePerMile.Id == updatePricePerMile.Id
                     select PricePerMile).Single();

                query.MilesFrom = updatePricePerMile.MilesFrom;
                query.MilesTo = updatePricePerMile.MilesTo;
                query.Cost = updatePricePerMile.Cost;
                query.MovingPricesId = updatePricePerMile.MovingPricesId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int PricePerMileId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from PricePerMile in db.PricePerMiles
                     where PricePerMile.Id == PricePerMileId
                     select PricePerMile).Single();

                db.PricePerMiles.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static List<PricePerMileDTO> ReadAll()
        {
            List<PricePerMileDTO> PricePerMiles = new List<PricePerMileDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from PricePerMile in db.PricePerMiles
                     select PricePerMile);

                foreach (PricePerMile PricePerMile in query)
                {
                    PricePerMileDTO PricePerMileRead = new PricePerMileDTO()
                    {
                        Cost = PricePerMile.Cost,
                        MilesFrom = PricePerMile.MilesFrom,
                        MilesTo = PricePerMile.MilesTo,
                        MovingPricesId = PricePerMile.MovingPricesId,
                        Id = PricePerMile.Id
                    };

                    PricePerMiles.Add(PricePerMileRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return PricePerMiles;
        }

        public static List<PricePerMileDTO> ReadAll(int MovingPricesId)
        {
            List<PricePerMileDTO> PricePerMiles = new List<PricePerMileDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from PricePerMile in db.PricePerMiles
                     where PricePerMile.MovingPricesId == MovingPricesId
                     select PricePerMile);

                foreach (PricePerMile PricePerMile in query)
                {
                    PricePerMileDTO PricePerMileRead = new PricePerMileDTO()
                    {
                        Cost = PricePerMile.Cost,
                        MilesFrom = PricePerMile.MilesFrom,
                        MilesTo = PricePerMile.MilesTo,
                        MovingPricesId = PricePerMile.MovingPricesId,
                        Id = PricePerMile.Id
                    };

                    PricePerMiles.Add(PricePerMileRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return PricePerMiles;
        }
    }
}
