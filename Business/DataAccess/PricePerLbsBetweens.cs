using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class PricePerLbsBetweens
    {
        public static int Create(PricePerLbsBetweenDTO pricePerLbsBetweenCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.PricePerLbsBetween pricePerLbsBetween = new Business.PricePerLbsBetween()
                {
                    LbsFrom = pricePerLbsBetweenCreate.LbsFrom,
                    LbsTo = pricePerLbsBetweenCreate.LbsTo,
                    Cost = (decimal)pricePerLbsBetweenCreate.Cost,
                    FromTo = pricePerLbsBetweenCreate.FromTo
                };

                db.PricePerLbsBetweens.InsertOnSubmit(pricePerLbsBetween);
                db.SubmitChanges();

                return pricePerLbsBetween.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static PricePerLbsBetweenDTO Read(int pricePerLbsBetweenId)
        {
            PricePerLbsBetweenDTO pricePerLbsBetweenRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsbetween in db.PricePerLbsBetweens
                     where priceperlbsbetween.Id == pricePerLbsBetweenId
                     select priceperlbsbetween).Single();

                pricePerLbsBetweenRead = new PricePerLbsBetweenDTO()
                {
                    Id = query.Id,
                    LbsFrom = query.LbsFrom,
                    LbsTo = query.LbsTo,
                    Cost = query.Cost,
                    FromTo = (int)query.FromTo
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return pricePerLbsBetweenRead;
        }

        public static List<PricePerLbsBetweenDTO> ReadAll()
        {
            List<PricePerLbsBetweenDTO> pricePerLbsBetweens = new List<PricePerLbsBetweenDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsbetween in db.PricePerLbsBetweens
                     select priceperlbsbetween);

                foreach (PricePerLbsBetween pricePerLbsBetween in query)
                {
                    PricePerLbsBetweenDTO pricePerLbsBetweenRead = new PricePerLbsBetweenDTO()
                    {
                        Id = pricePerLbsBetween.Id,
                        LbsFrom = pricePerLbsBetween.LbsFrom,
                        LbsTo = pricePerLbsBetween.LbsTo,
                        Cost = pricePerLbsBetween.Cost,
                        FromTo = (int)pricePerLbsBetween.FromTo
                    };

                    pricePerLbsBetweens.Add(pricePerLbsBetweenRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return pricePerLbsBetweens;
        }

        public static void Update(PricePerLbsBetweenDTO updatePricePerLbsBetween)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsbetween in db.PricePerLbsBetweens
                     where priceperlbsbetween.Id == updatePricePerLbsBetween.Id
                     select priceperlbsbetween).Single();

                query.LbsFrom = updatePricePerLbsBetween.LbsFrom;
                query.LbsTo = updatePricePerLbsBetween.LbsTo;
                query.Cost = (decimal)updatePricePerLbsBetween.Cost;
                query.FromTo = updatePricePerLbsBetween.FromTo;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int pricePerLbsBetweenId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from priceperlbsbetween in db.PricePerLbsBetweens
                     where priceperlbsbetween.Id == pricePerLbsBetweenId
                     select priceperlbsbetween).Single();

                db.PricePerLbsBetweens.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
