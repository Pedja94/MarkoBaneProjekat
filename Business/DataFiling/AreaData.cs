using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.DTO;
using Business.DataAccess;

namespace Business.DataFiling
{
    public static class AreaData
    {
        public static void AreaFiling()
        {
            string[] region = new string[3] { "GA", "IL", "MD" };
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j <=4; j++)
                { 
                    DirectoryInfo d = new DirectoryInfo("D:\\Zip Codes\\General\\"+region[i] + "\\" + region[i] + " " + j.ToString());
                    FileInfo[] Files = d.GetFiles("*.txt");

                    foreach(FileInfo file in Files)
                    {
                        int areaNumber = Int32.Parse(file.Name);
                        AreaDTO area = new AreaDTO()
                        {
                            Id = 1,
                            Number = areaNumber,
                            RadiusId = Radiuses.ReadIdFromRegionAndNumber(region[i], j)
                        };

                        Areas.Create(area);

                    }

                }

        }
    }
}
