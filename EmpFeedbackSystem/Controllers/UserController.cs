using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.CRUD;
using BAL.Util;
using EmpFeedbackSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpFeedbackSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "UserEscalationList")]
        public IActionResult UserEscalationList(BaseRequest req)
        {
            EscalationUserResp resp = null;
            try
            {
                if (RequestValidator.UserEscalationList(req))
                {
                    var user= UserCRUD.GetUser(req.user_id);
                    int? scale = user.UserRoleMapping.Select(x => x.Role.Scale).FirstOrDefault();
                    resp = new EscalationUserResp()
                    {
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        UserList = UserCRUD.GetEscalationUserList(user.Id,user.CompanyId,scale??0)
                    };
                }
                else
                {
                    resp = new EscalationUserResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new EscalationUserResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }
    }
}