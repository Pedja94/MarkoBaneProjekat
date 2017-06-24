using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Admins
    {
        public static int Create(AdminDTO adminCreate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                Admin admin = new Admin()
                {
                    Name = adminCreate.Name,
                    Password = adminCreate.Password,
                    Surname = adminCreate.Surname,
                    Username = adminCreate.Username
                };

                db.Admins.InsertOnSubmit(admin);
                db.SubmitChanges();

                return admin.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        public static AdminDTO Read(int adminId)
        {
            AdminDTO adminRead = null;

            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from admin in db.Admins
                     where admin.Id == adminId
                     select admin).Single();

                adminRead = new AdminDTO()
                {
                    Name = query.Name,
                    Password = query.Password,
                    Surname = query.Surname,
                    Username = query.Username,
                    Id = query.Id
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return adminRead;
        }

        public static void Update(AdminDTO updateadmin)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from admin in db.Admins
                     where admin.Id == updateadmin.Id
                     select admin).Single();

                query.Name = updateadmin.Name;
                query.Password = updateadmin.Password;
                query.Surname = updateadmin.Surname;
                query.Username = updateadmin.Username;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Delete(int adminId)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var query =
                    (from admin in db.Admins
                     where admin.Id == adminId
                     select admin).Single();

                db.Admins.DeleteOnSubmit(query);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static bool ChangePassword(int adminID, string NewPassword)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var User =
                    (from admin in db.Admins
                     where admin.Id == adminID
                     select admin).Single();

                User.Password = NewPassword;
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public static bool ChangeUsername(int adminID, string NewUsername)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();

                var User =
                    (from user in db.Admins
                     where user.Id == adminID
                     select user).Single();

                User.Username = NewUsername;
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public static AdminDTO Read(string username, string password)
        {
            AdminDTO adminRead = null;

            try
            {
                var db = new DatabaseDataContext();

                var query =
                    (from user in db.Admins
                     where user.Username == username && user.Password == password
                     select user).Single();

                adminRead = new AdminDTO()
                {
                    Name = query.Name,
                    Password = query.Password,
                    Surname = query.Surname,
                    Username = query.Username,
                    Id = query.Id
                };

                return adminRead;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
