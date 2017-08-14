using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class ZipCodes
    {
        public static int Create(ZipCodeDTO zipCodeCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.ZipCode zipCode = new Business.ZipCode()
                {
                    Code = zipCodeCreate.Code,
                    AreaId = zipCodeCreate.AreaId
                };

                db.ZipCodes.InsertOnSubmit(zipCode);
                db.SubmitChanges();

                return zipCode.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static ZipCodeDTO Read(int zipCodeId)
        {
            ZipCodeDTO zipCodeRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcode in db.ZipCodes
                     where zipcode.Id == zipCodeId
                     select zipcode).Single();

                zipCodeRead = new ZipCodeDTO()
                {
                    Id = query.Id,
                    Code = query.Code,
                    AreaId = (int)query.AreaId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return zipCodeRead;
        }

        public static ZipCodeDTO ReadFromZipCodeString(string zipCodeString)
        {
            ZipCodeDTO zipCodeRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcode in db.ZipCodes
                     where zipcode.Code == zipCodeString
                     select zipcode).Single();

                zipCodeRead = new ZipCodeDTO()
                {
                    Id = query.Id,
                    Code = query.Code,
                    AreaId = (int)query.AreaId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return zipCodeRead;
        }

        public static List<ZipCodeDTO> ReadAll()
        {
            List<ZipCodeDTO> zipCodes = new List<ZipCodeDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcode in db.ZipCodes
                     select zipcode);

                foreach (ZipCode zipcode in query)
                {
                    ZipCodeDTO zipCodeRead = new ZipCodeDTO()
                    {
                        Code = zipcode.Code,
                        AreaId = (int)zipcode.AreaId
                    };

                    zipCodes.Add(zipCodeRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return zipCodes;
        }

        public static void Update(ZipCodeDTO updateZipCode)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcode in db.ZipCodes
                     where zipcode.Id == updateZipCode.Id
                     select zipcode).Single();

                query.Code = updateZipCode.Code;
                query.AreaId = updateZipCode.AreaId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int zipCodeId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcode in db.ZipCodes
                     where zipcode.Id == zipCodeId
                     select zipcode).Single();

                db.ZipCodes.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static AreaDTO ReadAreaFromZipCode(ZipCodeDTO zipCode)
        {
            AreaDTO areaRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from area in db.Areas
                     where area.Id == zipCode.AreaId
                     select area).Single();

                areaRead = new AreaDTO()
                {
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

        public static bool SameRegion(ZipCodeDTO zipCodeFrom, ZipCodeDTO zipCodeTo)
        {
            AreaDTO areaFrom = new AreaDTO();
            AreaDTO areaTo = new AreaDTO();

            areaFrom = ReadAreaFromZipCode(zipCodeFrom);
            areaTo = ReadAreaFromZipCode(zipCodeTo);

            RadiusDTO radiusFrom = new RadiusDTO();
            RadiusDTO radiusTo = new RadiusDTO();

            radiusFrom = Areas.ReadRadiusFromArea(areaFrom);
            radiusTo = Areas.ReadRadiusFromArea(areaTo);

            return (radiusFrom.Region == radiusTo.Region) ? true : false; 
        }

        public static bool PossibleMoving(ZipCodeDTO zipCodeFrom, ZipCodeDTO zipCodeTo)
        {
            AreaDTO areaFrom = new AreaDTO();
            AreaDTO areaTo = new AreaDTO();

            areaFrom = ReadAreaFromZipCode(zipCodeFrom);
            areaTo = ReadAreaFromZipCode(zipCodeTo);

            RadiusDTO radiusFrom = new RadiusDTO();
            RadiusDTO radiusTo = new RadiusDTO();

            radiusFrom = Areas.ReadRadiusFromArea(areaFrom);
            radiusTo = Areas.ReadRadiusFromArea(areaTo);

            if ((!(radiusFrom.Region == radiusTo.Region)) || (Areas.FindFromToInsideId(areaFrom.Id, areaTo.Id) != -1))
                return true;
            else 
                return false;
        }

        public static bool PossibleFromToAreaMoving(ZipCodeDTO zipCodeFrom, ZipCodeDTO zipCodeTo)
        {
            AreaDTO areaFrom = new AreaDTO();
            AreaDTO areaTo = new AreaDTO();

            areaFrom = ReadAreaFromZipCode(zipCodeFrom);
            areaTo = ReadAreaFromZipCode(zipCodeTo);

            if (Areas.FindFromToInsideId(areaFrom.Id, areaTo.Id) != -1)
                return true;
            else
                return false;
        }

        public static bool PossibleFromToRadiusMoving(ZipCodeDTO zipCodeFrom, ZipCodeDTO zipCodeTo)
        {
            return true;
        }

        public static int ReturnPriceFromZipCodesAndLbs(ZipCodeDTO zipCodeFrom, ZipCodeDTO zipCodeTo, int lbs)
        {
            AreaDTO areaFrom = new AreaDTO();
            AreaDTO areaTo = new AreaDTO();

            areaFrom = ReadAreaFromZipCode(zipCodeFrom);
            areaTo = ReadAreaFromZipCode(zipCodeTo);

            RadiusDTO radiusFrom = new RadiusDTO();
            RadiusDTO radiusTo = new RadiusDTO();

            radiusFrom = Areas.ReadRadiusFromArea(areaFrom);
            radiusTo = Areas.ReadRadiusFromArea(areaTo);

            if (radiusFrom.Region != radiusTo.Region)
            {
                int fromToBetweenId = Radiuses.FindFromToBetweenId(radiusFrom.Region, radiusFrom.RadiusNumber, radiusTo.Region, radiusTo.RadiusNumber);
                if (fromToBetweenId != -1)
                {
                    PricePerLbsBetweenDTO pricePerLbsBetween =  PricePerLbsBetweens.ReadFromLbsAndFromToRadius(lbs, fromToBetweenId);
                    return (int)pricePerLbsBetween.Cost;
                } 
                else
                {
                    return -1;
                }
            }
            else 
            {
                int fromToInsideId = Areas.FindFromToInsideId(areaFrom.Id, areaTo.Id); 
                if (fromToInsideId != -1)
                {
                    PricePerLbsInsideDTO pricePerLbsInside = PricePerLbsInsides.ReadFromLbsAndFromToArea(lbs, fromToInsideId);
                    return (int)pricePerLbsInside.Cost;
                }
                else
                {
                    return -1;
                }
            }
        }

    }
}
