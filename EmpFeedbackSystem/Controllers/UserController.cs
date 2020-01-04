using System;
using BAL.CRUD;
using BAL.Util;
using EmpFeedbackSystem.Models;
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
            EscalationUserResp resp = new EscalationUserResp();
            try
            {
                if (RequestValidator.UserEscalationList(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        var user = UserCRUD.GetUser(req.user_id);
                        int? scale = UserCRUD.GetUserRole(req.user_id).Scale;
                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.UserList = UserCRUD.GetEscalationUserList(user.Id, user.CompanyId, scale ?? 0, 0);

                    }
                    else
                    {
                        resp.status_code = Unauthorized().StatusCode;
                        resp.status_message = StatusMessage.UnAuthorised;
                    }
                }
                else
                {
                    resp.status_code = BadRequest().StatusCode;
                    resp.status_message = StatusMessage.BadRequest;
                }

            }
            catch (Exception es)
            {
                resp.status_code = 500;
                resp.status_message = StatusMessage.InternalServerError;
            }

            return Ok(resp);
        }

        [HttpPost(Name = "TeamList")]
        public IActionResult TeamList(BaseRequest req)
        {
            EscalationUserResp resp = new EscalationUserResp();
            try
            {
                if (RequestValidator.TeamList(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        var user = UserCRUD.GetUser(req.user_id);
                        int? scale = UserCRUD.GetUserRole(req.user_id).Scale;

                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.UserList = UserCRUD.GetEscalationUserList(user.Id, user.CompanyId, scale ?? 0, req.feedback_id);

                    }
                    else
                    {
                        resp.status_code = Unauthorized().StatusCode;
                        resp.status_message = StatusMessage.UnAuthorised;
                    }
                }
                else
                {
                    resp.status_code = BadRequest().StatusCode;
                    resp.status_message = StatusMessage.BadRequest;
                }

            }
            catch (Exception es)
            {
                resp.status_code = 500;
                resp.status_message = StatusMessage.InternalServerError;
            }

            return Ok(resp);
        }

        [HttpPost(Name = "EscalatedUserList")]
        public IActionResult EscalatedUserList(BaseRequest req)
        {
            EscalatedUserListResp resp = new EscalatedUserListResp();
            try
            {
                if (RequestValidator.EscalatedUserList(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        var userList = FeedbackCRUD.GetEscalatedUser(req.user_id);
                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.UserList = userList;

                    }
                    else
                    {
                        resp.status_code = Unauthorized().StatusCode;
                        resp.status_message = StatusMessage.UnAuthorised;
                    }
                }
                else
                {
                    resp.status_code = BadRequest().StatusCode;
                    resp.status_message = StatusMessage.BadRequest;
                }

            }
            catch (Exception es)
            {
                resp.status_code = 500;
                resp.status_message = StatusMessage.InternalServerError;
            }

            return Ok(resp);
        }

        [HttpPost(Name = "FeedbackEscalationTeam")]
        public IActionResult FeedbackEscalationTeam(FeedbackEscalationTeamReq req)
        {
            EscalationUserResp resp = new EscalationUserResp();
            try
            {
                if (RequestValidator.FeedbackEscalationTeam(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        var user = UserCRUD.GetUser(req.user_id);
                        int? scale = UserCRUD.GetUserRole(req.user_id).Scale;

                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.UserList = UserCRUD.GetEscalationUserList(user.Id, user.CompanyId, scale ?? 0, 0);

                    }
                    else
                    {
                        resp.status_code = Unauthorized().StatusCode;
                        resp.status_message = StatusMessage.UnAuthorised;
                    }
                }
                else
                {
                    resp.status_code = BadRequest().StatusCode;
                    resp.status_message = StatusMessage.BadRequest;
                }
            }
            catch (Exception es)
            {
                resp.status_code = 500;
                resp.status_message = StatusMessage.InternalServerError;
            }

            return Ok(resp);
        }

    }
}