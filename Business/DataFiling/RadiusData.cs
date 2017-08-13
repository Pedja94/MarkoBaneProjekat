using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.DTO;
using Business.DataAccess;

namespace Business.DataFiling
{
    public static class RadiusData
    {
        public static void RadiusFiling()
        {
            string[] region = new string [3] { "GA", "IL", "MD" };

            for (int i = 0; i < 3; i++)
                for (int j = 1; j <=4; j++)
                {
                    RadiusDTO radius = new RadiusDTO()
                    {
                        Id = 1,
                        RadiusNumber = j,
                        Region = region[i]
                    };

                    Radiuses.Create(radius);
                }
        }
    }
}
