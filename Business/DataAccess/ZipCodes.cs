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
        public static int Create(ZipCodeDTO codeCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                ZipCode code = new ZipCode()
                {
                    State = codeCreate.State,
                    Town = codeCreate.Town,
                    ZipCode1 = codeCreate.ZipCode
                };

                db.ZipCodes.InsertOnSubmit(code);
                db.SubmitChanges();

                return code.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static void Update(ZipCodeDTO updateCode)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from code in db.ZipCodes
                     where code.Id == updateCode.Id
                     select code).Single();

                query.State = updateCode.State;
                query.Town = updateCode.Town;
                query.ZipCode1 = updateCode.ZipCode;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static List<ZipCodeDTO> ReadAll()
        {
            List<ZipCodeDTO> codes = new List<ZipCodeDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from code in db.ZipCodes
                     select code);

                foreach (ZipCode code in query)
                {
                    ZipCodeDTO codeRead = new ZipCodeDTO()
                    {
                        State = code.State,
                        Town = code.Town,
                        ZipCode = code.ZipCode1
                    };

                    codes.Add(codeRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return codes;
        }

        public static void Delete(int zipCodeId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from code in db.ZipCodes
                     where code.Id == zipCodeId
                     select code).Single();

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
