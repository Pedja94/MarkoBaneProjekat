using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Items
    {
        public static int Create(ItemDTO itemCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Item item = new Item()
                {
                    IconLink = itemCreate.IconLink,
                    Name = itemCreate.Name,
                    Weight = itemCreate.Weight,
                    AdditionalFee = itemCreate.AdditionalFee,
                    Packing = itemCreate.Packing,
                    CuFt = itemCreate.CuFt,
                    RoomId = itemCreate.RoomId
                };

                db.Items.InsertOnSubmit(item);
                db.SubmitChanges();

                return item.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static ItemDTO Read(int ItemId)
        {
            ItemDTO itemRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     where item.Id == ItemId
                     select item).Single();

                itemRead = new ItemDTO()
                {
                    IconLink = query.IconLink,
                    Name = query.Name,
                    Weight = query.Weight,
                    Id = query.Id,
                    AdditionalFee = query.AdditionalFee,
                    Packing = query.Packing,
                    CuFt = query.CuFt,
                    RoomId = query.RoomId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return itemRead;
        }

        public static List<ItemDTO> ReadAll()
        {
            List<ItemDTO> items = new List<ItemDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     select item);

                foreach (Item item in query)
                {
                    ItemDTO itemRead = new ItemDTO()
                    {
                        IconLink = item.IconLink,
                        Name = item.Name,
                        Weight = item.Weight,
                        Id = item.Id,
                        AdditionalFee = item.AdditionalFee,
                        Packing = item.Packing,
                        CuFt = item.CuFt,
                        RoomId = item.RoomId
                    };

                    items.Add(itemRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return items;
        }

        public static List<ItemDTO> ReadAllInRoom(int RoomId)
        {
            List<ItemDTO> items = new List<ItemDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     where item.RoomId == RoomId 
                     select item);

                foreach (Item Item in query)
                {
                    ItemDTO itemRead = new ItemDTO()
                    {
                        AdditionalFee = Item.AdditionalFee,
                        CuFt = Item.CuFt,
                        IconLink = Item.IconLink,
                        Id = Item.Id,
                        Name = Item.Name,
                        Packing = Item.Packing,
                        RoomId = RoomId,
                        Weight = Item.Weight
                    };
                    items.Add(itemRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return items;
        }

        public static List<ItemDTO> ReadAllInOffer(int OfferId)
        {
            List<ItemDTO> items = new List<ItemDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from itemoffer in db.InventoryItems
                     where itemoffer.OfferId == OfferId
                     select itemoffer.ItemId);

                foreach (int Id in query)
                {
                    ItemDTO itemRead = Items.Read(Id);
                    items.Add(itemRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return items;
        }

        public static void Update(ItemDTO updateItem)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     where item.Id == updateItem.Id
                     select item).Single();

                query.IconLink = updateItem.IconLink;
                query.Name = updateItem.Name;
                query.Weight = updateItem.Weight;
                query.Packing = updateItem.Packing;
                query.AdditionalFee = updateItem.AdditionalFee;
                query.CuFt = updateItem.CuFt;
                query.RoomId = updateItem.RoomId;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int itemId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     where item.Id == itemId
                     select item).Single();

                db.Items.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void AddItemToRoom(int itemId, int roomId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from item in db.Items
                     where item.Id == itemId
                     select item).Single();

                query.RoomId = roomId;

                db.Items.InsertOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
