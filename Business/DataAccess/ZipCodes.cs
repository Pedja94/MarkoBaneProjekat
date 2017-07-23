using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public class ZipCodes
    {
        public static int Create(ZipCodeDTO ZipCodeCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                ZipCode ZipCode = new ZipCode()
                {
                    Code = ZipCodeCreate.Code,
                    StateNum = ZipCodeCreate.StateNum,
                    RadiusId = ZipCodeCreate.Radius.Id
                };

                db.ZipCodes.InsertOnSubmit(ZipCode);
                db.SubmitChanges();

                return ZipCode.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static ZipCodeDTO Read(int ZipCodeId)
        {
            ZipCodeDTO ZipCodeRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where ZipCode.Id == ZipCodeId
                     select ZipCode).Single();

                ZipCodeRead = new ZipCodeDTO()
                {
                    Code = query.Code,
                    StateNum = query.StateNum,
                    Id = query.Id
                };

                ZipCodeRead.Radius = Radiuses.Read((int)query.RadiusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ZipCodeRead;
        }

        public static ZipCodeDTO Find(string Code)
        {
            ZipCodeDTO ZipCodeRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where ZipCode.Code == Code
                     select ZipCode).Single();

                ZipCodeRead = new ZipCodeDTO()
                {
                    Code = query.Code,
                    StateNum = query.StateNum,
                    Id = query.Id
                };

                ZipCodeRead.Radius = Radiuses.Read((int)query.RadiusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ZipCodeRead;
        }

        public static List<ZipCodeDTO> ReadAll()
        {
            List<ZipCodeDTO> ZipCodes = new List<ZipCodeDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     select ZipCode);

                foreach (ZipCode ZipCode in query)
                {
                    ZipCodeDTO ZipCodeRead = new ZipCodeDTO()
                    {
                        Code = ZipCode.Code,
                        StateNum = ZipCode.StateNum,
                        Id = ZipCode.Id
                    };

                    ZipCodes.Add(ZipCodeRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ZipCodes;
        }

        public static List<ZipCodeDTO> ReadAll(int RadiusId)
        {
            List<ZipCodeDTO> ZipCodes = new List<ZipCodeDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where ZipCode.RadiusId == RadiusId
                     select ZipCode);

                foreach (ZipCode ZipCode in query)
                {
                    ZipCodeDTO ZipCodeRead = new ZipCodeDTO()
                    {
                        Code = ZipCode.Code,
                        StateNum = ZipCode.StateNum,
                        Id = ZipCode.Id
                    };

                    ZipCodes.Add(ZipCodeRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ZipCodes;
        }

        public static List<ZipCodeDTO> ReadAll(int RadiusId, int stateNum)
        {
            List<ZipCodeDTO> ZipCodes = new List<ZipCodeDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where (ZipCode.RadiusId == RadiusId 
                        && ZipCode.StateNum == stateNum) 
                     select ZipCode);

                foreach (ZipCode ZipCode in query)
                {
                    ZipCodeDTO ZipCodeRead = new ZipCodeDTO()
                    {
                        Code = ZipCode.Code,
                        StateNum = ZipCode.StateNum,
                        Id = ZipCode.Id
                    };

                    ZipCodes.Add(ZipCodeRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ZipCodes;
        }

        public static void Update(ZipCodeDTO updateZipCode)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where ZipCode.Id == updateZipCode.Id
                     select ZipCode).Single();

                query.Code = updateZipCode.Code;
                query.StateNum = updateZipCode.StateNum;
                query.RadiusId = updateZipCode.Radius.Id;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int ZipCodeId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from ZipCode in db.ZipCodes
                     where ZipCode.Id == ZipCodeId
                     select ZipCode).Single();

                db.ZipCodes.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
