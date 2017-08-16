using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Rooms
    {
        public static int Create(RoomDTO roomCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Room room = new Room()
                {
                    Name = roomCreate.Name,
                };

                db.Rooms.InsertOnSubmit(room);
                db.SubmitChanges();

                return room.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static RoomDTO Read(int RoomId)
        {
            RoomDTO roomRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from room in db.Rooms
                     where room.Id == RoomId
                     select room).Single();

                roomRead = new RoomDTO()
                {
                    Name = query.Name,
                    Id = query.Id
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return roomRead;
        }

        public static List<RoomDTO> ReadAll()
        {
            List<RoomDTO> rooms = new List<RoomDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from room in db.Rooms
                     select room);

                foreach (Room room in query)
                {
                    RoomDTO roomRead = new RoomDTO()
                    {
                        Name = room.Name,
                        Id = room.Id
                    };

                    rooms.Add(roomRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return rooms;
        }

        public static List<RoomDTO> ReadAllInOffer(int OfferId)
        {
            List<RoomDTO> items = new List<RoomDTO>(); ;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from roomoffer in db.InventoryRooms
                     where roomoffer.OfferId == OfferId
                     select roomoffer.RoomId);

                foreach (int Id in query)
                {
                    RoomDTO roomRead = Rooms.Read(Id);
                    items.Add(roomRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return items;
        }

        public static void Update(RoomDTO updateRoom)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from room in db.Rooms
                     where room.Id == updateRoom.Id
                     select room).Single();

                query.Name = updateRoom.Name;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int roomId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from room in db.Rooms
                     where room.Id == roomId
                     select room).Single();

                db.Rooms.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}