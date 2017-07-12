using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Offers
    {
        public static int Create(OfferDTO offerCreate)
        {
            int id1, id2;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Offer offer = new Offer()
                {
                    AdditionalService = offerCreate.AdditionalService,
                    AdditionStop = offerCreate.AdditionStop,
                    Email = offerCreate.Email,
                    AdminId = null,
                    EndDate = null,
                    EstimateDate = offerCreate.EstimateDate,
                    EstimateFlag = offerCreate.EstimateFlag,
                    EstimateTime = offerCreate.EstimateTime,
                    HowDidYouFindUs = offerCreate.HowDidYouFindUs,
                    InventoryFlag = offerCreate.InventoryFlag,
                    Name = offerCreate.Name,
                    RegureasCOI = offerCreate.RegureasCOI,
                    Serial = offerCreate.Serial,
                    StartDate = System.DateTime.Now,
                    Surname = offerCreate.Surname,
                    Type = offerCreate.Type,
                    VideoFlag = offerCreate.VideoFlag,
                    VideoLink = offerCreate.VideoLink,
                    WhatToMove = offerCreate.WhatToMove, 
                    WhoIsPresentAtPickup = offerCreate.WhoIsPresentAtPickup,
                };

                if (offerCreate.InforamtionFrom != null)
                {
                    id1 = Informations.Create(offerCreate.InforamtionFrom);
                    offer.InformationFromId = id1;
                }

                if (offerCreate.InforamtionTo != null)
                {
                    id2 = Informations.Create(offerCreate.InforamtionTo);
                    offer.InformationToId = id2;
                }

                db.Offers.InsertOnSubmit(offer);
                db.SubmitChanges();

                Random rnd = new Random(offer.Id);

                offer.Serial = offer.Serial + (rnd.Next() % 10000).ToString();

                db.SubmitChanges();

                foreach (int Id in offerCreate.ItemIds)
                {
                    Offers.AddItemToOffer(Id, offer.Id);
                }

                foreach (int Id in offerCreate.RoomIds)
                {
                    Offers.AddRoomToOffer(Id, offer.Id);
                }

                return offer.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static void AddRoomToOffer(int roomId, int offerId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                InventoryRoom conn = new InventoryRoom()
                {
                    OfferId = offerId,
                    RoomId = roomId
                };

                db.InventoryRooms.InsertOnSubmit(conn);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void AddItemToOffer(int itemId, int offerId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                InventoryItem conn = new InventoryItem()
                {
                    OfferId = offerId,
                    ItemId = itemId
                };

                db.InventoryItems.InsertOnSubmit(conn);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Update(OfferDTO updateOffer)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.Id == updateOffer.Id
                     select offer).Single();

                query.AdditionalService = updateOffer.AdditionalService;
                query.AdditionStop = updateOffer.AdditionStop;
                query.Email = updateOffer.Email;
                query.AdminId = updateOffer.AdminId;
                query.EndDate = updateOffer.EndDate;
                query.EstimateDate = updateOffer.EstimateDate;
                query.EstimateFlag = updateOffer.EstimateFlag;
                query.EstimateTime = updateOffer.EstimateTime;
                query.HowDidYouFindUs = updateOffer.HowDidYouFindUs;
                query.InformationFromId = updateOffer.InforamtionFrom.Id;
                query.InformationToId = updateOffer.InforamtionTo.Id;
                query.InventoryFlag = updateOffer.InventoryFlag;
                query.Name = updateOffer.Name;
                query.RegureasCOI = updateOffer.RegureasCOI;
                query.Serial = updateOffer.Serial;
                query.StartDate = updateOffer.StartDate;
                query.Surname = updateOffer.Surname;
                query.Type = updateOffer.Type;
                query.VideoFlag = updateOffer.VideoFlag;
                query.VideoLink = updateOffer.VideoLink;
                query.WhatToMove = updateOffer.WhatToMove;
                query.WhoIsPresentAtPickup = updateOffer.WhoIsPresentAtPickup;
                query.Id = updateOffer.Id;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static OfferDTO Read(int offerId)
        {
            OfferDTO offerRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.Id == offerId
                     select offer).Single();

                offerRead = new OfferDTO()
                {
                    AdditionalService = query.AdditionalService,
                    AdditionStop = query.AdditionStop,
                    AdminId = query.AdminId,
                    Email = query.Email,
                    EndDate = query.EndDate,
                    EstimateDate = query.EstimateDate,
                    WhoIsPresentAtPickup = query.WhoIsPresentAtPickup,
                    WhatToMove = query.WhatToMove,
                    EstimateFlag = query.EstimateFlag,
                    EstimateTime = query.EstimateTime,
                    HowDidYouFindUs = query.HowDidYouFindUs,
                    InventoryFlag = query.InventoryFlag,
                    Name = query.Name,
                    RegureasCOI = query.RegureasCOI,
                    Serial = query.Serial,
                    StartDate = query.StartDate,
                    Surname = query.Surname,
                    Type = query.Type,
                    VideoFlag = query.VideoFlag,
                    VideoLink = query.VideoLink,
                    Id = query.Id
                };

                if (query.InformationFromId != null)
                {
                    offerRead.InforamtionFrom = Informations.Read((int)query.InformationFromId);
                }
                if (query.InformationToId != null)
                {
                    offerRead.InforamtionTo = Informations.Read((int)query.InformationToId);
                }

                if (query.InventoryRooms != null)
                {
                    offerRead.RoomIds = new List<int>();
                    foreach (InventoryRoom ir in query.InventoryRooms)
                    {
                        offerRead.RoomIds.Add((int)ir.RoomId);
                    }
                }
                if (query.InventoryItems != null)
                {
                    offerRead.ItemIds = new List<int>();
                    foreach (InventoryItem ii in query.InventoryItems)
                    {
                        offerRead.RoomIds.Add((int)ii.ItemId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return offerRead;
        }

        public static List<OfferDTO> ReadAll()
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     select offer);

                foreach (Offer offer in query)
                {
                    OfferDTO offerRead = new OfferDTO()
                    {
                        AdditionalService = offer.AdditionalService,
                        AdditionStop = offer.AdditionStop,
                        AdminId = offer.AdminId,
                        Email = offer.Email,
                        EndDate = offer.EndDate,
                        EstimateDate = offer.EstimateDate,
                        WhoIsPresentAtPickup = offer.WhoIsPresentAtPickup,
                        WhatToMove = offer.WhatToMove,
                        EstimateFlag = offer.EstimateFlag,
                        EstimateTime = offer.EstimateTime,
                        HowDidYouFindUs = offer.HowDidYouFindUs,
                        InventoryFlag = offer.InventoryFlag,
                        Name = offer.Name,
                        RegureasCOI = offer.RegureasCOI,
                        Serial = offer.Serial,
                        StartDate = offer.StartDate,
                        Surname = offer.Surname,
                        Type = offer.Type,
                        VideoFlag = offer.VideoFlag,
                        VideoLink = offer.VideoLink,
                        Id = offer.Id
                    };

                    if (offer.InformationFromId != null)
                    {
                        offerRead.InforamtionFrom = Informations.Read((int)offer.InformationFromId);
                    }
                    if (offer.InformationToId != null)
                    {
                        offerRead.InforamtionTo = Informations.Read((int)offer.InformationToId);
                    }

                    if (offer.InventoryRooms != null)
                    {
                        offerRead.RoomIds = new List<int>();
                        foreach (InventoryRoom ir in offer.InventoryRooms)
                        {
                            offerRead.RoomIds.Add((int)ir.RoomId);
                        }
                    }
                    if (offer.InventoryItems != null)
                    {
                        offerRead.ItemIds = new List<int>();
                        foreach (InventoryItem ii in offer.InventoryItems)
                        {
                            offerRead.RoomIds.Add((int)ii.ItemId);
                        }
                    }

                    offers.Add(offerRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return offers;
        }

        public static List<OfferDTO> ReadNotProcesssed()
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.EndDate == null
                     select offer);

                foreach (Offer offer in query)
                {
                    OfferDTO offerRead = new OfferDTO()
                    {
                        AdditionalService = offer.AdditionalService,
                        AdditionStop = offer.AdditionStop,
                        AdminId = offer.AdminId,
                        Email = offer.Email,
                        EndDate = offer.EndDate,
                        EstimateDate = offer.EstimateDate,
                        WhoIsPresentAtPickup = offer.WhoIsPresentAtPickup,
                        WhatToMove = offer.WhatToMove,
                        EstimateFlag = offer.EstimateFlag,
                        EstimateTime = offer.EstimateTime,
                        HowDidYouFindUs = offer.HowDidYouFindUs,
                        InventoryFlag = offer.InventoryFlag,
                        Name = offer.Name,
                        RegureasCOI = offer.RegureasCOI,
                        Serial = offer.Serial,
                        StartDate = offer.StartDate,
                        Surname = offer.Surname,
                        Type = offer.Type,
                        VideoFlag = offer.VideoFlag,
                        VideoLink = offer.VideoLink,
                        Id = offer.Id
                    };

                    if (offer.InformationFromId != null)
                    {
                        offerRead.InforamtionFrom = Informations.Read((int)offer.InformationFromId);
                    }
                    if (offer.InformationToId != null)
                    {
                        offerRead.InforamtionTo = Informations.Read((int)offer.InformationToId);
                    }

                    if (offer.InventoryRooms != null)
                    {
                        offerRead.RoomIds = new List<int>();
                        foreach (InventoryRoom ir in offer.InventoryRooms)
                        {
                            offerRead.RoomIds.Add((int)ir.RoomId);
                        }
                    }
                    if (offer.InventoryItems != null)
                    {
                        offerRead.ItemIds = new List<int>();
                        foreach (InventoryItem ii in offer.InventoryItems)
                        {
                            offerRead.RoomIds.Add((int)ii.ItemId);
                        }
                    }

                    offers.Add(offerRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return offers;
        }

        public static List<OfferDTO> ReadProcesssed()
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.EndDate != null
                     select offer);

                foreach (Offer offer in query)
                {
                    OfferDTO offerRead = new OfferDTO()
                    {
                        AdditionalService = offer.AdditionalService,
                        AdditionStop = offer.AdditionStop,
                        AdminId = offer.AdminId,
                        Email = offer.Email,
                        EndDate = offer.EndDate,
                        EstimateDate = offer.EstimateDate,
                        WhoIsPresentAtPickup = offer.WhoIsPresentAtPickup,
                        WhatToMove = offer.WhatToMove,
                        EstimateFlag = offer.EstimateFlag,
                        EstimateTime = offer.EstimateTime,
                        HowDidYouFindUs = offer.HowDidYouFindUs,
                        InventoryFlag = offer.InventoryFlag,
                        Name = offer.Name,
                        RegureasCOI = offer.RegureasCOI,
                        Serial = offer.Serial,
                        StartDate = offer.StartDate,
                        Surname = offer.Surname,
                        Type = offer.Type,
                        VideoFlag = offer.VideoFlag,
                        VideoLink = offer.VideoLink,
                        Id = offer.Id
                    };

                    if (offer.InformationFromId != null)
                    {
                        offerRead.InforamtionFrom = Informations.Read((int)offer.InformationFromId);
                    }
                    if (offer.InformationToId != null)
                    {
                        offerRead.InforamtionTo = Informations.Read((int)offer.InformationToId);
                    }

                    if (offer.InventoryRooms != null)
                    {
                        offerRead.RoomIds = new List<int>();
                        foreach (InventoryRoom ir in offer.InventoryRooms)
                        {
                            offerRead.RoomIds.Add((int)ir.RoomId);
                        }
                    }
                    if (offer.InventoryItems != null)
                    {
                        offerRead.ItemIds = new List<int>();
                        foreach (InventoryItem ii in offer.InventoryItems)
                        {
                            offerRead.RoomIds.Add((int)ii.ItemId);
                        }
                    }

                    offers.Add(offerRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return offers;
        }

        public static List<OfferDTO> ReadFromAdmin(int adminId)
        {
            List<OfferDTO> offers = new List<OfferDTO>();

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.AdminId == adminId
                     select offer);

                foreach (Offer offer in query)
                {
                    OfferDTO offerRead = new OfferDTO()
                    {
                        AdditionalService = offer.AdditionalService,
                        AdditionStop = offer.AdditionStop,
                        AdminId = offer.AdminId,
                        Email = offer.Email,
                        EndDate = offer.EndDate,
                        EstimateDate = offer.EstimateDate,
                        WhoIsPresentAtPickup = offer.WhoIsPresentAtPickup,
                        WhatToMove = offer.WhatToMove,
                        EstimateFlag = offer.EstimateFlag,
                        EstimateTime = offer.EstimateTime,
                        HowDidYouFindUs = offer.HowDidYouFindUs,
                        InventoryFlag = offer.InventoryFlag,
                        Name = offer.Name,
                        RegureasCOI = offer.RegureasCOI,
                        Serial = offer.Serial,
                        StartDate = offer.StartDate,
                        Surname = offer.Surname,
                        Type = offer.Type,
                        VideoFlag = offer.VideoFlag,
                        VideoLink = offer.VideoLink,
                        Id = offer.Id
                    };

                    if (offer.InformationFromId != null)
                    {
                        offerRead.InforamtionFrom = Informations.Read((int)offer.InformationFromId);
                    }
                    if (offer.InformationToId != null)
                    {
                        offerRead.InforamtionTo = Informations.Read((int)offer.InformationToId);
                    }

                    if (offer.InventoryRooms != null)
                    {
                        offerRead.RoomIds = new List<int>();
                        foreach (InventoryRoom ir in offer.InventoryRooms)
                        {
                            offerRead.RoomIds.Add((int)ir.RoomId);
                        }
                    }
                    if (offer.InventoryItems != null)
                    {
                        offerRead.ItemIds = new List<int>();
                        foreach (InventoryItem ii in offer.InventoryItems)
                        {
                            offerRead.RoomIds.Add((int)ii.ItemId);
                        }
                    }

                    offers.Add(offerRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return offers;
        }

        public static void Delete(int offerId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from offer in db.Offers
                     where offer.Id == offerId
                     select offer).Single();

                db.Offers.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
