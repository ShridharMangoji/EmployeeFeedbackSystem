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
                        status_code = Ok().StatusCode,
                        status_message = Ok().ToString(),
                        FeedbackDetails = FeedbackCRUD.GetFeedbackDetails(req.feedback_id),
                        FeedbackEscalationHistory = FeedbackCRUD.GetFeedbackEscalationDetails(req.feedback_id)
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

    }
}