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
    public static class PricePerLbsInsideData
    {
        public static void PricePerLbsInsideFiling()
        {
            string[] region = new string[] { "GA", "IL", "MD" };
            int[] numberOfAreas = new int[] { 24, 25, 20 };
            int[] priceLevels = new int[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000, 14000, 16000, 18000, 20000, 22000 };

            for (int i = 0; i < 3; i++)
                for (int j = 1; j <= numberOfAreas[i]; j++)
                {
                    DirectoryInfo d = new DirectoryInfo("D:\\Moving Prices\\Inside\\" + region[i] + "\\" + j.ToString());
                    FileInfo[] Files = d.GetFiles("*.txt");

                    foreach (FileInfo file in Files)
                    {
                        string path = "D:\\Moving Prices\\Inside\\" + region[i] + "\\" + j.ToString() + "\\" + file.Name;
                        string readZipCodes = File.ReadAllText(path);
                        string[] lines = readZipCodes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                        for (int k = 0; k < lines.Length; k++)
                        {
                            PricePerLbsInsideDTO pricePerLbsInside = new PricePerLbsInsideDTO()
                            {
                                Id = 1,
                                LbsFrom = (k > 0) ? priceLevels[k - 1] + 1 : 0,
                                LbsTo = priceLevels[k],
                                Cost = Decimal.Parse(lines[k]),
                                FromTo = 1 // FromTo = FindFromToInsideId(j, Int32.Parse(file.Name));
                            };

                            PricePerLbsInsides.Create(pricePerLbsInside);
                        }
                    }
                }
        }
    }
}
