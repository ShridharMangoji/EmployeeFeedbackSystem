using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.CRUD;
using BAL.Util;
using DAL.Models;
using EmpFeedbackSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            BaseResponse resp = null;
            try
            {
                if (RequestValidator.GenerateOTP(req))
                {
                    if (UserCRUD.IsValidUser(req.user_id))
                    {
                        RegisteredDevice device = new RegisteredDevice()
                        {
                            DeviceId = req.device_id,
                            LastUpdate = DateTime.Now,
                            OsType = req.os_type,
                            Otp = "1111",
                            RegisteredOn = DateTime.Now,
                            UserId = req.user_id
                        };
                        DeviceCRUD.AddDeviceIfNotExist(device);
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
                            status_code = Unauthorized().StatusCode,
                            status_message = Unauthorized().ToString()
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

        [HttpPost(Name = "VerifyOTP")]
        [AllowAnonymous]
        public IActionResult VerifyOTP(VerifyOTPReq req)
        {
            VerifyOTPResp resp = null;
            try
            {
                if (RequestValidator.VerifyOTP(req))
                {
                    if (UserCRUD.IsValidUser(req.user_id))
                    {
                        if (DeviceCRUD.VerifyOTP(req.device_id, req.user_id, Convert.ToString(req.otp)))
                        {
                            var user = UserCRUD.GetUser(req.user_id);
                            DeviceCRUD.NulifyOTP(req.device_id, req.user_id, Convert.ToString(req.otp));
                            RegisteredDevice device = DeviceCRUD.GetDevice(req.device_id);
                            resp = new VerifyOTPResp()
                            { name=user.Name,
                                status_code = Ok().StatusCode,
                                status_message = Ok().ToString(),
                                token = JwtToken.GenerateJwtToken(device)
                            };
                        }
                        else
                        {
                            resp = new VerifyOTPResp()
                            {
                                status_code = Unauthorized().StatusCode,
                                status_message = Unauthorized().ToString()
                            };
                        }
                    }
                    else
                    {
                        resp = new VerifyOTPResp()
                        {
                            status_code = BadRequest().StatusCode,
                            status_message = BadRequest().ToString()
                        };
                    }
                }
                else
                {
                    resp = new VerifyOTPResp()
                    {
                        status_code = BadRequest().StatusCode,
                        status_message = BadRequest().ToString()
                    };
                }

            }
            catch (Exception es)
            {
                System.IO.File.AppendAllText(@"E:\IIS\EMP\WriteText.txt", es.Message);
                resp = new VerifyOTPResp()
                {
                    status_code = 500,
                    status_message = StatusMessage.InternalServerError
                };
            }

            return Ok(resp);
        }

    }



}