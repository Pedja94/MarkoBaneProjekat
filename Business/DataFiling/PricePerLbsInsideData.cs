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
            int[][] matrixOfAreas = new int[12][];

            //GA
            matrixOfAreas[0] = new int[] { 1, 2, 9, 10 };
            matrixOfAreas[1] = new int[] { 3, 4, 5, 6, 7, 8, 11, 12 };
            matrixOfAreas[2] = new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            matrixOfAreas[3] = new int[] { 22, 23, 24 };
            //IL
            matrixOfAreas[4] = new int[] { 1, 4, 5 };
            matrixOfAreas[5] = new int[] { 2, 3, 6, 7, 8 };
            matrixOfAreas[6] = new int[] { 9, 10, 13, 14, 15, 16 };
            matrixOfAreas[7] = new int[] { 11, 12, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            //MD
            matrixOfAreas[8] = new int[] { 1, 2, 3, 4, 5, 6};
            matrixOfAreas[9] = new int[] { 7, 8, 9, 10, 11 };
            matrixOfAreas[10] = new int[] { 12, 13, 14, 15, 20 };
            matrixOfAreas[11] = new int[] { 16, 17, 18, 19 };


            int[] priceLevels = new int[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000, 14000, 16000, 18000, 20000, 22000 };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                   
                    int matrixOfAreasI = 4 * i + j - 1;
                    int radiusIdFrom = Radiuses.ReadIdFromRegionAndNumber(region[i], j);
                    
                    for (int l = 0; l < matrixOfAreas[matrixOfAreasI].Length; l++)
                    {
                        if (!((i == 1) && (matrixOfAreas[matrixOfAreasI][l] == 18)))
                        {
                            int areaFrom = Areas.ReadIdFromNumberAndRadiusId(matrixOfAreas[matrixOfAreasI][l], radiusIdFrom);
                            DirectoryInfo d = new DirectoryInfo("D:\\Moving Prices\\Inside\\" + region[i] + "\\" + j.ToString() + "\\" + matrixOfAreas[matrixOfAreasI][l].ToString());
                            FileInfo[] Files = d.GetFiles("*.txt");

                            foreach (FileInfo file in Files)
                            {
                                string path = "D:\\Moving Prices\\Inside\\" + region[i] + "\\" + j.ToString() + "\\" + matrixOfAreas[matrixOfAreasI][l].ToString() + "\\" + file.Name;
                                string readZipCodes = File.ReadAllText(path);
                                string[] lines = readZipCodes.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                                string[] niz = file.Name.Split('.');
                                int areaNumber = Int32.Parse(niz[0]);
                                int radiusNumber = 0;
                                for (int x = 0; x < 4; x++)
                                {
                                    int tempX = 4 * i + x;
                                    for (int y = 0; y < matrixOfAreas[tempX].Length; y++)
                                    {
                                        if (matrixOfAreas[tempX][y] == areaNumber)
                                        {
                                            radiusNumber = x + 1;
                                        }
                                    }
                                }
                                int radiusIdTo = Radiuses.ReadIdFromRegionAndNumber(region[i], radiusNumber);
                                int areaTo = Areas.ReadIdFromNumberAndRadiusId(areaNumber, radiusIdTo);

                                int fromTo = Areas.CreateFromToArea(areaFrom, areaTo);

                                for (int k = 0; k < lines.Length; k++)
                                {
                                    PricePerLbsInsideDTO pricePerLbsInside = new PricePerLbsInsideDTO()
                                    {
                                        Id = 1,
                                        LbsFrom = (k > 0) ? priceLevels[k - 1] + 1 : 0,
                                        LbsTo = priceLevels[k],
                                        Cost = Decimal.Parse(lines[k]),
                                        FromTo = fromTo
                                    };

                                    PricePerLbsInsides.Create(pricePerLbsInside);
                                }
                            }
                            
                        }
                        
                    }

                }
            }
                
        }
    }
}
