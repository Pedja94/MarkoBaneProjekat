using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Business.DTO;
using Business.DataAccess;


namespace Business.DataFiling
{
    public static class ItemData
    {
        public static void ItemFiling()
        {
            string[] roomNames = new string[] { "Appliances", "Bedroom", "Dining Room", "Garage", "Kitchen", "Living Room", "Nursery", "Office", "Porch Outdoor"};
            string[] itemAttributes = new string[] { "AdditionalFee.txt", "CuFt.txt", "itemNames.txt", "Lbs.txt", "PackingFee.txt"};

            for (int i = 0; i < roomNames.Length; i++)
            {
                RoomDTO room = new RoomDTO()
                {
                    Id = 1,
                    Name = roomNames[i]
                };

                int roomId = Rooms.Create(room);

                string path = "D:\\Inventory\\" + roomNames[i] + "\\";
                
                string[] additionalFee = File.ReadAllText(path +itemAttributes[0]).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                string[] cuFt = File.ReadAllText(path + itemAttributes[1]).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                string[] itemNames = File.ReadAllText(path + itemAttributes[2]).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                string[] lbs = File.ReadAllText(path + itemAttributes[3]).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                string[] packingFee = File.ReadAllText(path + itemAttributes[4]).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int j = 0; j < itemNames.Length; j++)
                {
                    //~\\Content\\icons\\png\\Bedroom\\Bed Bunk1.png
                    ItemDTO item = new ItemDTO()
                    {
                        Id = 1,
                        Name = itemNames[j],
                        IconLink = "~\\Content\\icons\\png\\" + roomNames[i] + "\\" + itemNames[j] + ".png",
                        Weight = Decimal.Parse(lbs[j]),
                        Packing = Decimal.Parse(packingFee[j]),
                        AdditionalFee = Decimal.Parse(additionalFee[j]),
                        CuFt = Decimal.Parse(cuFt[j]),
                        RoomId = roomId
                    };

                    Items.Create(item);
                }


            }

        }
    }
}
