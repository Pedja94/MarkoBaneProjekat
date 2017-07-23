using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public class MovingPriceses
    {
        public static MovingPricesDTO Read(int MovingPricesId)
        {
            MovingPricesDTO MovingPricesRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.Id == MovingPricesId
                     select MovingPrices).Single();

                MovingPricesRead = new MovingPricesDTO()
                {
                    FromToId = query.FromToId,
                    Id = query.Id,
                    StateNums = query.StateNums,
                };

                MovingPricesRead.listOfPrices = PricePerMiles.ReadAll(MovingPricesRead.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return MovingPricesRead;
        }

        public static void Update(MovingPricesDTO updateMovingPrices)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.Id == updateMovingPrices.Id
                     select MovingPrices).Single();

                query.StateNums = updateMovingPrices.StateNums;
                query.FromToId = updateMovingPrices.FromToId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int MovingPricesId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.Id == MovingPricesId
                     select MovingPrices).Single();

                db.MovingPrices.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static List<MovingPricesDTO> ReadAll()
        {
            List<MovingPricesDTO> MovingPricess = new List<MovingPricesDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from MovingPrices in db.MovingPrices
                     select MovingPrices);

                foreach (MovingPrice MovingPrices in query)
                {
                    MovingPricesDTO MovingPricesRead = new MovingPricesDTO()
                    {
                        Id = MovingPrices.Id,
                        StateNums = MovingPrices.StateNums,
                        FromToId = MovingPrices.FromToId
                    };

                    MovingPricess.Add(MovingPricesRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return MovingPricess;
        }

        public static List<MovingPricesDTO> ReadAll(int fromRadiusId, int toRadiusId)
        {
            List<MovingPricesDTO> MovingPricess = new List<MovingPricesDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query2 =
                    (from FromTo in db.FromTos
                     where (FromTo.RadiusFromId == fromRadiusId
                        && FromTo.RadiusFromId == toRadiusId)
                     select FromTo).Single();

                var query =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.FromToId == query2.Id
                     select MovingPrices);

                foreach (MovingPrice MovingPrices in query)
                {
                    MovingPricesDTO MovingPricesRead = new MovingPricesDTO()
                    {
                        Id = MovingPrices.Id,
                        StateNums = MovingPrices.StateNums,
                        FromToId = MovingPrices.FromToId
                    };

                    MovingPricesRead.listOfPrices = PricePerMiles.ReadAll(MovingPricesRead.Id);

                    MovingPricess.Add(MovingPricesRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return MovingPricess;
        }

        public static int CreateRadiusToRadius(int fromRadiusId, int toRadiusId, string stateNums)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                FromTo fromTo = new FromTo()
                {
                    RadiusFromId = fromRadiusId,
                    RadiusToId = toRadiusId
                };

                db.FromTos.InsertOnSubmit(fromTo);
                db.SubmitChanges();

                MovingPrice MovingPrices = new MovingPrice()
                {
                    StateNums = stateNums,
                    FromToId = fromTo.Id
                };

                db.MovingPrices.InsertOnSubmit(MovingPrices);
                db.SubmitChanges();

                return MovingPrices.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static int CreateRadiusToRadius(int fromRadiusId, int toRadiusId, List<MovingPricesDTO> list)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                FromTo fromTo = new FromTo()
                {
                    RadiusFromId = fromRadiusId,
                    RadiusToId = toRadiusId
                };

                db.FromTos.InsertOnSubmit(fromTo);
                db.SubmitChanges();

                int i;
                for (i = 0; i < list.Count; i++)
                {
                    MovingPrice MovingPrices = new MovingPrice()
                    {
                        StateNums = list[i].StateNums,
                        FromToId = fromTo.Id
                    };

                    db.MovingPrices.InsertOnSubmit(MovingPrices);
                    db.SubmitChanges();

                    if (list[i].listOfPrices.Count > 0)
                    {
                        for (int j = 0; j < list[i].listOfPrices.Count; j++)
                        {
                            PricePerMile PricePerMile = new PricePerMile()
                            {
                                MilesFrom = list[i].listOfPrices[j].MilesFrom,
                                MilesTo = list[i].listOfPrices[j].MilesTo,
                                Cost = list[i].listOfPrices[j].Cost,
                                MovingPricesId = MovingPrices.Id
                            };

                            db.PricePerMiles.InsertOnSubmit(PricePerMile);
                            db.SubmitChanges();
                        }
                    }
                }

                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static decimal FindCost(int fromRadiusId, int toRadiusId, int stateNumTo, int miles)
        {
            decimal cost = -1;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from FromTos in db.FromTos
                     where (FromTos.RadiusFromId == fromRadiusId &&
                        FromTos.RadiusToId == toRadiusId)
                     select FromTos).Single();

                var query1 =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.FromToId == query.Id
                     select MovingPrices);

                foreach (MovingPrice mp in query1)
                {
                    if (InStateNums(stateNumTo, mp.StateNums))
                    {
                        var query2 =
                            (from PricePerMiles in db.PricePerMiles
                             where PricePerMiles.MovingPricesId == mp.Id
                             select PricePerMiles);

                        foreach (PricePerMile ppm in query2)
                        {
                            if (ppm.MilesFrom <= miles && ppm.MilesTo >= miles)
                            {
                                return ppm.Cost;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return cost;
        }

        public static bool CanMove(int fromRadiusId, int toRadiusId, int stateNumTo)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from FromTos in db.FromTos
                     where (FromTos.RadiusFromId == fromRadiusId &&
                        FromTos.RadiusToId == toRadiusId)
                     select FromTos).Single();

                var query1 =
                    (from MovingPrices in db.MovingPrices
                     where MovingPrices.FromToId == query.Id
                     select MovingPrices);

                foreach (MovingPrice mp in query1)
                {
                    if (InStateNums(stateNumTo, mp.StateNums))
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        private static bool InStateNums(int stateNum, string stateNums)
        {
            string[] list = stateNums.Split(' ');
            int num;

            for (int i = 0; i < list.Length; i++)
            {
                if (Int32.TryParse(list[i], out num))
                {
                    if (num == stateNum)
                        return true;
                }
            }

            return false;
        }
    }
}
