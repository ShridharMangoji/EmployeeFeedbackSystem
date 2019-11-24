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
    }
}