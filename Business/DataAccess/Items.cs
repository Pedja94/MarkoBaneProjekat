﻿using System;
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
                    Cost = itemCreate.Cost,
                    Description = itemCreate.Description,
                    IconLink = itemCreate.IconLink,
                    Name = itemCreate.Name,
                    Weight = itemCreate.Weight
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
                    Cost = query.Cost,
                    Description = query.Description,
                    IconLink = query.IconLink,
                    Name = query.Name,
                    Weight = query.Weight,
                    Id = query.Id
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
                        Cost = item.Cost,
                        Description = item.Description,
                        IconLink = item.IconLink,
                        Name = item.Name,
                        Weight = item.Weight,
                        Id = item.Id
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
                    (from itemroom in db.RoomItems
                     where itemroom.RoomId == RoomId 
                     select itemroom.ItemId);

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

                query.Cost = updateItem.Cost;
                query.Description = updateItem.Description;
                query.IconLink = updateItem.IconLink;
                query.Name = updateItem.Name;
                query.Weight = updateItem.Weight;

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

                RoomItem conn = new RoomItem()
                {
                    ItemId = itemId,
                    RoomId = roomId
                };

                db.RoomItems.InsertOnSubmit(conn);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
