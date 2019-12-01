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

        public static int GetMaxScale()
        {
            using (var db = new Entities())
            {
                var result = db.UserRole.Max(x => x.Scale);
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
        public static int GetUserScale(long userID)
        {
            using (var db = new Entities())
            {
                var result = db.Users.Where(x => x.Id == userID).Select(x=>x.UserRoleMapping.Select(y=>y.Role.Scale).FirstOrDefault()).FirstOrDefault();
                return result;
            }
        }
        public static UserRole GetUserRole(long userID)
        {
            using (var db = new Entities())
            {
                var result = db.UserRoleMapping.Where(x => x.UserId == userID).Select(x=>x.Role).FirstOrDefault();
                return result;
            }
        }

        public static List<UserModel> GetEscalationUserList(long userID, long companyID, int scale,long feedbackid)
        {
            using (var db = new Entities())
            {
                if (feedbackid > 0)
                {
                    var feedback = db.Feedback.Where(x => x.Id == feedbackid).FirstOrDefault();
                    var result = db.Users.Where(x => x.CompanyId == companyID && Convert.ToInt64(x.Id) != userID) // && x.UserRoleMapping.Any(y => y.Role.Scale > scale)
                                           .Select(x => new
                                            UserModel()
                                           {
                                               ID = x.Id,
                                               Name = x.Name
                                           }).ToList();
                    if (feedback != null)
                        result = result.Where(x => x.ID != feedback.CreatedFor).ToList();
                    return result;
                }
                else
                {
                    var result = db.Users.Where(x => x.CompanyId == companyID && Convert.ToInt64(x.Id) != userID) // && x.UserRoleMapping.Any(y => y.Role.Scale > scale)
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

        public static List<UserModel> GetTeamList(long userID, long companyID, int scale)
        {
            using (var db = new Entities())
            {
                var result = db.Users.Where(x => x.CompanyId == companyID && x.Id!=userID)
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
