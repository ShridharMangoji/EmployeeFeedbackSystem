using BAL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.CRUD
{
    public static class UserCRUD
    {
        public static bool IsValidUser(long userID)
        {
            using (var db = new Entities())
            {
                var result = db.Users.Any(x => x.Id == userID);
                return result;
            }
        }

        public static Users GetUser(long userID)
        {
            using (var db = new Entities())
            {
                var result = db.Users.Where(x => x.Id == userID).FirstOrDefault();
                return result;
            }
        }

        public static List<UserModel> GetEscalationUserList(long userID, long companyID, int scale)
        {
            using (var db = new Entities())
            {
                var result = db.Users.Where(x => x.CompanyId == companyID && x.UserRoleMapping.Any(y => y.Role.Scale > scale))
                   .Select(x => new
                    UserModel()
                   {
                       ID = x.Id,
                       Name = x.Name
                   }).ToList();

                return result;
            }
        }
    }
}
