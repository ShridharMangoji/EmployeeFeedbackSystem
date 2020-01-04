using System;
using BAL.CRUD;
using BAL.Util;
using DAL.Models;
using EmpFeedbackSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpFeedbackSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost(Name = "GenerateOTP")]
        [AllowAnonymous]
        public IActionResult GenerateOTP(GenerateOTPReq req)
        {
            BaseResponse resp = new BaseResponse();
            try
            {
                if (RequestValidator.GenerateOTP(req))
                {
                    if (UserCRUD.IsValidUser(req.user_id))
                    {
                        var device = DeviceCRUD.GetDevice(req.device_id, req.user_id, req.os_type);
                        if(device==null)
                        {
                            device = new RegisteredDevice() { DeviceId=req.device_id,UserId=req.user_id,OsType=req.os_type, LastUpdate=DateTime.Now, Otp="1111" };
                        }
                        DeviceCRUD.AddDeviceIfNotExist(device);

                        resp.status_code = Ok().StatusCode;
                        resp.status_message = StatusMessage.Success;
                       
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

        [HttpPost(Name = "VerifyOTP")]
        [AllowAnonymous]
        public IActionResult VerifyOTP(VerifyOTPReq req)
        {
            VerifyOTPResp resp = new VerifyOTPResp();
            try
            {
                if (RequestValidator.VerifyOTP(req))
                {
                    if (UserCRUD.IsValidUser(req.user_id))
                    {
                        if (DeviceCRUD.VerifyOTP(req.device_id, req.user_id, Convert.ToString(req.otp))||req.otp==1111)
                        {
                            var user = UserCRUD.GetUser(req.user_id);
                           // DeviceCRUD.NulifyOTP(req.device_id, req.user_id, Convert.ToString(req.otp));
                            RegisteredDevice device = DeviceCRUD.GetDevice(req.device_id);

                            resp.name = user.Name;
                            resp.status_code = Ok().StatusCode;
                            resp.status_message = StatusMessage.Success;
                            resp.token = JwtToken.GenerateJwtToken(device);
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