using System;
using System.Linq;
using BAL.CRUD;
using BAL.Util;
using DAL.Models;
using EmpFeedbackSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpFeedbackSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {

        [HttpPost(Name = "GetFeedbackCategories")]
        public IActionResult GetFeedbackCategories(BaseRequest req)
        {
            FeedbackCategoryResp resp = new FeedbackCategoryResp();
            try
            {
                if (RequestValidator.GetFeedbackCategories(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {

                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.feedback_categories = FeedbackCRUD.GetAllFeedbackCategories();

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

        [HttpPost(Name = "FeedbackHistory")]
        public IActionResult FeedbackHistory(FeedbackHistoryReq req)
        {
            FeedbackHistoryResp resp = new FeedbackHistoryResp();
            try
            {

                if (RequestValidator.FeedbackHistory(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {

                        resp.IsReplyRequired = FeedbackCRUD.IsReplyRequired(req.feedback_id, req.user_id);
                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.IsChatHistoryAccessible = FeedbackCRUD.IsUserAccessibleForFeedbackChat(req.feedback_id, req.user_id);
                        resp.FeedbackDetails = FeedbackCRUD.GetFeedbackDetails(req.feedback_id);
                        resp.FeedbackEscalationHistory = FeedbackCRUD.GetFeedbackEscalationDetails(req.feedback_id);

                        if (resp.IsChatHistoryAccessible)
                            resp.ReplyList = FeedbackCRUD.ReplyHistory(req.feedback_id);
                        if (resp.FeedbackEscalationHistory.Count > 0)
                        {
                            resp.FeedbackDetails.EscalatedUserName = resp.FeedbackEscalationHistory.OrderByDescending(x => x.LastUpdate).FirstOrDefault().feedback_escalated_username;
                        }
                        resp.IsEscalationAllowed = FeedbackCRUD.IsEscalationAllowed(req.user_id, resp.FeedbackDetails.CreatedFor, resp.FeedbackDetails.CreatedOn);
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


        [HttpPost(Name = "UpdateFeedback")]
        public IActionResult UpdateFeedback(UpdateFeedbackReq req)
        {
            BaseResponse resp = new BaseResponse();
            try
            {
                if (RequestValidator.UpdateFeedback(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        if (req.feedback_info.Id > 0)
                        {
                            if (Constants.ClosedStatus.Contains(req.feedback_info.StatusId))
                            {
                                FeedbackCRUD.updateFeedbackStatus(req.feedback_info.Id, req.feedback_info.StatusId);
                            }
                            else
                            {
                                FeedbackCRUD.updateFeedbackStatus(req.feedback_info.Id, (int)eFeedbackStatus.Escalated);
                                FeedbackEscalationMapping escalation = new FeedbackEscalationMapping()
                                {
                                    EscalatedUserId = req.feedback_info.CreatedFor,
                                    FeedbackId = req.feedback_id,
                                    LastUpdate = DateTime.Now,
                                    Message = req.feedback_info.Message,
                                    Subject = req.feedback_info.Subject
                                };
                                FeedbackCRUD.AddFeedbackEscalation(escalation);
                            }
                            resp.status_code = Ok().StatusCode;
                            resp.status_message = StatusMessage.Success;

                        }
                        else
                        {
                            if (!FeedbackCRUD.AnyOpenFeedback(req.feedback_info.CreatedBy, req.feedback_info.CreatedFor))
                            {
                                req.feedback_info.LastUpdate = DateTime.Now;
                                req.feedback_info.CreatedOn = DateTime.Now;
                                FeedbackCRUD.AddFeedback(req.feedback_info);
                                resp.status_code = Ok().StatusCode;
                                resp.status_message = StatusMessage.Success;
                            }
                            else
                            {
                                resp.status_code = Conflict().StatusCode;
                                resp.status_message = StatusMessage.FeedbackExists;
                            }
                        }
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


        [HttpPost(Name = "FeedbackList")]
        public IActionResult FeedbackList(FeedbackListReq req)
        {
            FeedbackListResp resp = new FeedbackListResp();
            try
            {
                if (RequestValidator.FeedbackList(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {
                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.FeedbackList = FeedbackCRUD.GetFeedbackList(req.user_id, req.escalated_user_id);
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


        [HttpPost(Name = "FeedbackDetailList")]
        public IActionResult FeedbackDetailList(FeedbackListReq req)
        {
            FeedbackDetailListResp resp = new FeedbackDetailListResp();
            try
            {
                if (RequestValidator.FeedbackList(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {

                        int maxScale = UserCRUD.GetMaxScale();


                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                        resp.IsEscalationRequired = UserCRUD.GetUserScale(req.user_id) == maxScale ? false : true;
                        resp.FeedbackCreatedByMe = FeedbackCRUD.MyFeedbacks(req.user_id);
                        resp.FeedbackCreatedForMe = FeedbackCRUD.FeedbacksCreatedForMe(req.user_id);
                        resp.FeedbackEscalatedToMe = FeedbackCRUD.FeedbacksEscalatedToMe(req.user_id);

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



        [HttpPost(Name = "ReplyToFeedback")]
        public IActionResult ReplyToFeedback(ReplyReq req)
        {
            BaseResponse resp = new BaseResponse();
            try
            {
                if (RequestValidator.ReplyToFeedback(req))
                {
                    if (req.user_id == JwtToken.GetUserID(req.token))
                    {

                        if (!FeedbackCRUD.IsALreadyRepliedToFeedback(req.feedback_id, req.user_id))
                        {
                            var dbReq = new FeedbackChats()
                            {
                                FeedbackId = req.feedback_id,
                                Reply = req.reply,
                                LastUpdate = DateTime.Now,
                                ReplyGivenBy = req.user_id
                            };

                            FeedbackCRUD.ReplyToFeedback(dbReq);

                            resp.status_code = Ok().StatusCode;
                            resp.status_message = StatusMessage.Success;

                        }
                        else
                        {
                            resp = new BaseResponse()
                            {
                                status_code = 201,
                                status_message = StatusMessage.RepliedAlready
                            };
                        }
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