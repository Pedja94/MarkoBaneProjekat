using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.DTO;
using Business.DataAccess;

using System.IO;


namespace Business.DataFiling
{
    public static class ZipCodeData
    {
        public static void ZipCodeFiling()
        {
            string[] region = new string[3] { "GA", "IL", "MD" };

            for (int i = 0; i < 3; i++)
                for (int j = 1; j <= 4; j++)
                {
                    DirectoryInfo d = new DirectoryInfo("D:\\Zip Codes\\General\\" + region[i] + "\\" + region[i] + " " + j.ToString());
                    FileInfo[] Files = d.GetFiles("*.txt");

                    foreach (FileInfo file in Files)
                    {
                        string[] niz = file.Name.Split('.');
                        int areaNumber = Int32.Parse(niz[0]);

                        string path = "D:\\Zip Codes\\General\\" + region[i] + "\\" + region[i] + " " + j.ToString() + "\\" + file.Name;
                        string readZipCodes = File.ReadAllText(path);
                        string[] lines = readZipCodes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                        int radiusId = Radiuses.ReadIdFromRegionAndNumber(region[i], j);
                        int areaId = Areas.ReadIdFromNumberAndRadiusId(areaNumber, radiusId);

                        for (int k = 0; k < lines.Length; k++)
                        {
                            ZipCodeDTO zipCode = new ZipCodeDTO()
                            {
                                Id = 1,
                                Code = lines[k],
                                AreaId = areaId
                            };

                            ZipCodes.Create(zipCode);
                        }
                    }
                }
        }
     }
    
}
