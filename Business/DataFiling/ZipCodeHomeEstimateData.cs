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
    public static class ZipCodeHomeEstiateData
    {
        public static void ZipCodeHomeEstimateFiling()
        {

            DirectoryInfo d = new DirectoryInfo("D:\\Zip Codes\\In Home Estimate");
            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                string[] niz = file.Name.Split('.');
                string region = niz[0];

                string path = "D:\\Zip Codes\\In Home Estimate" + "\\" + file.Name;
                string readZipCodes = File.ReadAllText(path);
                string[] lines = readZipCodes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                int radiusId = Radiuses.ReadIdFromRegionAndNumber(region, 1);

                for (int k = 0; k < lines.Length; k++)
                {
                    ZipCodeHomeEstimateDTO zipCode = new ZipCodeHomeEstimateDTO()
                    {
                        Id = 1,
                        Code = lines[k],
                        RadiusId = radiusId
                    };

                    ZipCodeHomeEstimates.Create(zipCode);
                }
            }

        }
    }

}
