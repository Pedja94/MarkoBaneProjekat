using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.DTO;

namespace Business.DataAccess
{
    public static class ZipCodeHomeEstimates
    {
        public static int Create(ZipCodeHomeEstimateDTO zipCodeHomeEstimateCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Business.ZipCodeHomeEstimate zipCodeHomeEstimate = new Business.ZipCodeHomeEstimate()
                {
                    Code = zipCodeHomeEstimateCreate.Code,
                    RadiusId = zipCodeHomeEstimateCreate.RadiusId
                };

                db.ZipCodeHomeEstimates.InsertOnSubmit(zipCodeHomeEstimate);
                db.SubmitChanges();

                return zipCodeHomeEstimate.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static ZipCodeHomeEstimateDTO Read(int zipCodeHomeEstimateId)
        {
            ZipCodeHomeEstimateDTO zipCodeHomeEstimateRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcodehomeestimate in db.ZipCodeHomeEstimates
                     where zipcodehomeestimate.Id == zipCodeHomeEstimateId
                     select zipcodehomeestimate).Single();

                zipCodeHomeEstimateRead = new ZipCodeHomeEstimateDTO()
                {
                    Id = zipCodeHomeEstimateId,
                    Code = query.Code,
                    RadiusId = (int)query.RadiusId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return zipCodeHomeEstimateRead;
        }

        public static List<ZipCodeHomeEstimateDTO> ReadAll()
        {
            List<ZipCodeHomeEstimateDTO> zipCodeHomeEstimates = new List<ZipCodeHomeEstimateDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcodehomeestimate in db.ZipCodeHomeEstimates
                     select zipcodehomeestimate);

                foreach (ZipCodeHomeEstimate zipcodehomeestimate in query)
                {
                    ZipCodeHomeEstimateDTO zipCodeHomeEstimateRead = new ZipCodeHomeEstimateDTO()
                    {
                        Id = zipcodehomeestimate.Id,
                        Code = zipcodehomeestimate.Code,
                        RadiusId = (int)zipcodehomeestimate.RadiusId
                    };

                    zipCodeHomeEstimates.Add(zipCodeHomeEstimateRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return zipCodeHomeEstimates;
        }

        public static void Update(ZipCodeHomeEstimateDTO updateZipCodeHomeEstimate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcodehomeestimate in db.ZipCodeHomeEstimates
                     where zipcodehomeestimate.Id == updateZipCodeHomeEstimate.Id
                     select zipcodehomeestimate).Single();

                query.Code = updateZipCodeHomeEstimate.Code;
                query.RadiusId = updateZipCodeHomeEstimate.RadiusId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int zipCodeHomeEstimateId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from zipcodehomeestimate in db.ZipCodeHomeEstimates
                     where zipcodehomeestimate.Id == zipCodeHomeEstimateId
                     select zipcodehomeestimate).Single();

                db.ZipCodeHomeEstimates.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
