using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.CRUD;
using BAL.Util;
using DAL.Models;
using EmpFeedbackSystem.Models;
using Microsoft.AspNetCore.Http;
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
            FeedbackCategoryResp resp = null;
            try
            {
                if (RequestValidator.GetFeedbackCategories(req))
                {
                    resp = new FeedbackCategoryResp()
                    {
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        feedback_categories = FeedbackCRUD.GetAllFeedbackCategories()
                    };
                }
                else
                {
                    resp = new FeedbackCategoryResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new FeedbackCategoryResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }

        [HttpPost(Name = "FeedbackHistory")]
        public IActionResult FeedbackHistory(FeedbackHistoryReq req)
        {
            FeedbackHistoryResp resp = null;
            try
            {
                if (RequestValidator.FeedbackHistory(req))
                {
                    resp = new FeedbackHistoryResp()
                    {
                        IsReplyRequired = FeedbackCRUD.IsReplyRequired(req.feedback_id, req.user_id),
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        IsChatHistoryAccessible=FeedbackCRUD.IsUserAccessibleForFeedbackChat(req.feedback_id, req.user_id),
                        FeedbackDetails = FeedbackCRUD.GetFeedbackDetails(req.feedback_id),
                        FeedbackEscalationHistory = FeedbackCRUD.GetFeedbackEscalationDetails(req.feedback_id)
                    };
                    if (resp.IsChatHistoryAccessible)
                        resp.ReplyList = FeedbackCRUD.ReplyHistory(req.feedback_id);
              
                    resp.IsEscalationAllowed = (DateTime.Now - resp.FeedbackDetails.CreatedOn).Days > 7 ? true : false;
                }
                else
                {
                    resp = new FeedbackHistoryResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new FeedbackHistoryResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
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

                    if (req.feedback_info.Id > 0)
                    {
                        if (req.feedback_info.StatusId == (int)eFeedbackStatus.Closed)
                        {
                            FeedbackCRUD.updateFeedbackStatus(req.feedback_info.Id, (int)eFeedbackStatus.Closed);
                        }
                        else
                        {
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
                    }
                    else
                    {


                        req.feedback_info.LastUpdate = DateTime.Now;
                        req.feedback_info.CreatedOn = DateTime.Now;
                        FeedbackCRUD.AddFeedback(req.feedback_info);
                    }
                    resp = new BaseResponse()
                    {
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                    };
                }
                else
                {
                    resp = new FeedbackHistoryResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new FeedbackHistoryResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }


        [HttpPost(Name = "FeedbackList")]
        public IActionResult FeedbackList(FeedbackListReq req)
        {
            FeedbackListResp resp = null;
            try
            {
                if (RequestValidator.FeedbackList(req))
                {
                    resp = new FeedbackListResp()
                    {
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        FeedbackList = FeedbackCRUD.GetFeedbackList(req.user_id, req.escalated_user_id)
                    };
                }
                else
                {
                    resp = new FeedbackListResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new FeedbackListResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }


        [HttpPost(Name = "FeedbackDetailList")]
        public IActionResult FeedbackDetailList(FeedbackListReq req)
        {
            FeedbackDetailListResp resp = null;
            try
            {
                if (RequestValidator.FeedbackList(req))
                {
                    int maxScale = UserCRUD.GetMaxScale();

                    resp = new FeedbackDetailListResp()
                    {
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        IsEscalationRequired = UserCRUD.GetUserScale(req.user_id) == maxScale ? false : true,
                        FeedbackCreatedByMe = FeedbackCRUD.MyFeedbacks(req.user_id),
                        FeedbackCreatedForMe = FeedbackCRUD.FeedbacksCreatedForMe(req.user_id),
                        FeedbackEscalatedToMe = FeedbackCRUD.FeedbacksEscalatedToMe(req.user_id)
                    };
                }
                else
                {
                    resp = new FeedbackDetailListResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                resp = new FeedbackDetailListResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }



        [HttpPost(Name = "ReplyToFeedback")]
        public IActionResult ReplyToFeedback(ReplyReq req)
        {
            BaseResponse resp = null;
            try
            {
                if (RequestValidator.ReplyToFeedback(req))
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

                        resp = new BaseResponse()
                        {
                            status_code = Ok().StatusCode,
                            status_message = Ok().ToString()
                        };
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
                    resp = new BaseResponse()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }
            }
            catch (Exception es)
            {
                resp = new BaseResponse()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }


    }
}