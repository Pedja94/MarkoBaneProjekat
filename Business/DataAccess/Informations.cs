using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Informations
    {
        public static int Create(InformationDTO informationCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Information information = new Information()
                {
                    Address = informationCreate.Address,
                    FullSelfPack = informationCreate.FullSelfPack,
                    TypeOfAccess = informationCreate.TypeOfAccess,
                    TypeOfBuilding = informationCreate.TypeOfBuilding,
                    UOElevatorDesc = informationCreate.UOElevatorDesc,
                    UOElevatorFlag = informationCreate.UOElevatorFlag,
                    UOStairsDesc = informationCreate.UOStairsDesc,
                    UOStairsFlag = informationCreate.UOStairsFlag,
                    ZipCode = informationCreate.ZipCode,
                    SizeOfBuilding = informationCreate.SizeOfBuilding,
                    RegureasCOI = informationCreate.RegureasCOI
                };

                db.Informations.InsertOnSubmit(information);
                db.SubmitChanges();

                return information.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static InformationDTO Read(int informationId)
        {
            InformationDTO informationRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from information in db.Informations
                     where information.Id == informationId
                     select information).Single();

                informationRead = new InformationDTO()
                {
                    Address = query.Address,
                    FullSelfPack = query.FullSelfPack,
                    TypeOfAccess = query.TypeOfAccess,
                    TypeOfBuilding = query.TypeOfBuilding,
                    UOElevatorDesc = query.UOElevatorDesc,
                    UOElevatorFlag = query.UOElevatorFlag,
                    UOStairsDesc = query.UOStairsDesc,
                    UOStairsFlag = query.UOStairsFlag,
                    ZipCode = query.ZipCode,
                    Id = query.Id,
                    SizeOfBuilding = query.SizeOfBuilding,
                    RegureasCOI = query.RegureasCOI
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return informationRead;
        }

        public static void Update(InformationDTO updateInformation)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from information in db.Informations
                     where information.Id == updateInformation.Id
                     select information).Single();

                query.Address = updateInformation.Address;
                query.FullSelfPack = updateInformation.FullSelfPack;
                query.TypeOfAccess = updateInformation.TypeOfAccess;
                query.TypeOfBuilding = updateInformation.TypeOfBuilding;
                query.UOElevatorDesc = updateInformation.UOElevatorDesc;
                query.UOElevatorFlag = updateInformation.UOElevatorFlag;
                query.UOStairsDesc = updateInformation.UOStairsDesc;
                query.UOStairsFlag = updateInformation.UOStairsFlag;
                query.ZipCode = updateInformation.ZipCode;
                query.RegureasCOI = updateInformation.RegureasCOI;
                query.SizeOfBuilding = updateInformation.SizeOfBuilding;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int informationId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from information in db.Informations
                     where information.Id == informationId
                     select information).Single();

                db.Informations.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
