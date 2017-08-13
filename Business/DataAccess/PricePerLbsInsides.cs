using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class PricePerLbsInsides
    {
        public static int Create(PricePerLbsInsideDTO pricePerLbsInsideCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.PricePerLbsInside pricePerLbsInside = new Business.PricePerLbsInside()
                {
                    LbsFrom = pricePerLbsInsideCreate.LbsFrom,
                    LbsTo = pricePerLbsInsideCreate.LbsTo,
                    Cost = pricePerLbsInsideCreate.Cost,
                    FromTo = pricePerLbsInsideCreate.FromTo
                };

                db.PricePerLbsInsides.InsertOnSubmit(pricePerLbsInside);
                db.SubmitChanges();

                return pricePerLbsInside.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static PricePerLbsInsideDTO Read(int pricePerLbsInsideId)
        {
            PricePerLbsInsideDTO pricePerLbsInsideRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsinside in db.PricePerLbsInsides
                     where priceperlbsinside.Id == pricePerLbsInsideId
                     select priceperlbsinside).Single();

                pricePerLbsInsideRead = new PricePerLbsInsideDTO()
                {
                    Id = query.Id,
                    LbsFrom = (int)query.LbsFrom,
                    LbsTo = (int)query.LbsTo,
                    Cost = query.Cost,
                    FromTo = (int)query.FromTo
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return pricePerLbsInsideRead;
        }

        public static List<PricePerLbsInsideDTO> ReadAll()
        {
            List<PricePerLbsInsideDTO> pricePerLbsInsides = new List<PricePerLbsInsideDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsinside in db.PricePerLbsInsides
                     select priceperlbsinside);

                foreach (PricePerLbsInside pricePerLbsInside in query)
                {
                    PricePerLbsInsideDTO pricePerLbsInsideRead = new PricePerLbsInsideDTO()
                    {
                        LbsFrom = (int)pricePerLbsInside.LbsFrom,
                        LbsTo = (int)pricePerLbsInside.LbsTo,
                        Cost = pricePerLbsInside.Cost,
                        FromTo = (int)pricePerLbsInside.FromTo
                    };

                    pricePerLbsInsides.Add(pricePerLbsInsideRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return pricePerLbsInsides;
        }

        public static void Update(PricePerLbsInsideDTO updatePricePerLbsInside)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsinside in db.PricePerLbsInsides
                     where priceperlbsinside.Id == updatePricePerLbsInside.Id
                     select priceperlbsinside).Single();

                query.LbsFrom = (int)updatePricePerLbsInside.LbsFrom;
                query.LbsTo = (int)updatePricePerLbsInside.LbsTo;
                query.Cost = updatePricePerLbsInside.Cost;
                query.FromTo = (int)updatePricePerLbsInside.FromTo;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int pricePerLbsInsideId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsinside in db.PricePerLbsInsides
                     where priceperlbsinside.Id == pricePerLbsInsideId
                     select priceperlbsinside).Single();

                db.PricePerLbsInsides.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
