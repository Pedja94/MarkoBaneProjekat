using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Business.DataAccess;
using Business.DTO;

namespace Business.DataFiling
{
    public static class PricePerLbsBetweenData
    {
        public static void PricePerLbsInsideFiling()
        {
            string[] region = new string[] { "GA", "IL", "MD" };
            int[] priceLevels = new int[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000, 14000, 16000, 18000, 20000, 22000 };

            for (int i = 0; i < 3; i++)
                for (int j = 1; j <= 4; j++)
                {
                    DirectoryInfo d = new DirectoryInfo("D:\\Moving Prices\\Between\\" + region[i] + "\\" + j.ToString());
                    FileInfo[] Files = d.GetFiles("*.txt");

                    foreach (FileInfo file in Files)
                    {
                        string name = file.Name;
                        string regionTo = name.Substring(0, 2).ToUpper();
                        int radiusNumberTo = name[2] - 48;

                        string path = "D:\\Moving Prices\\Between\\" + region[i] + "\\" + j.ToString() + "\\" + file.Name;
                        string readZipCodes = File.ReadAllText(path);
                        string[] lines = readZipCodes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                        int radiusFrom = Radiuses.ReadIdFromRegionAndNumber(region[i], j);
                        int radiusTo = Radiuses.ReadIdFromRegionAndNumber(regionTo, radiusNumberTo);

                        int fromTo = Radiuses.CreateFromToRadius(radiusFrom, radiusTo);
                        
                        for (int k = 0; k < lines.Length; k++)
                        {

                            PricePerLbsBetweenDTO pricePerLbsBetween = new PricePerLbsBetweenDTO()
                            {
                                Id = 1,
                                LbsFrom = (k > 0) ? priceLevels[k - 1] + 1 : 0,
                                LbsTo = priceLevels[k],
                                Cost = Decimal.Parse(lines[k]),
                                FromTo = fromTo
                            };

                            PricePerLbsBetweens.Create(pricePerLbsBetween);

                        }
                    }
                }
        }
    }
}
