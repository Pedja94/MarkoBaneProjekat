using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public class AccessoriesTables
    {
        public static List<AccessoriesTableDTO> ReadAll()
        {
            List<AccessoriesTableDTO> Accessories = new List<AccessoriesTableDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from Accessorie in db.AccessoriesTables
                     select Accessorie);

                foreach (AccessoriesTable Accessorie in query)
                {
                    AccessoriesTableDTO AccessoriesRead = new AccessoriesTableDTO()
                    {
                        Id = Accessorie.Id,
                        Name = Accessorie.Name,
                        Description = Accessorie.Description,
                        Cost = Accessorie.Cost
                    };

                    Accessories.Add(AccessoriesRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Accessories;
        }
    }
}
