using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upstack.VerificationApp.API.Model;
using Upstack.VerificationApp.API.Contracts;
using Upstack.VerificationApp.API.Services;
using AutoMapper;
using Upstack.VerificationApp.API.Entities;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Upstack.VerificationApp.API.Controllers
{
    [Route("api/{v:apiVersion}/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private SendGridModel _sendgrid; 

        public UserController(IUserRepository userRepository, IOptions<SendGridModel> sendgrid)
        {
            _userRepository = userRepository;
            _sendgrid = sendgrid.Value;
        }

    
        // POST api/1.0/user
        /// <summary>
        /// Receive user details pass to the body of the endpoint
        /// </summary>
        /// <returns> status 200 for success and 500 for badrequest</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserBindingModel _user)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserModel>(_user);
                // check if user  email exist
                var checkEmail = _userRepository.QueryWithOptions(a => a.Email == user.Email).SingleOrDefault();
                if(checkEmail != null)
                {
                    return StatusCode(409, "User with the email exist");
                }               
                user.DateRegistered = DateTime.Now;               
                await _userRepository.InsertAsync(user);
                await _userRepository.Commit();
                return Ok(user);
            }
            else
            {
                var errors = new List<ApiError>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new ApiError {
                            Detail = error.ErrorMessage
                        });
                    }
                }
                return BadRequest(errors);
            }
        }
        // PUT api/1.0/user/?email=
        /// <summary>
        /// Update user user verification with email pass as query to the endpoint
        /// </summary>
        /// <returns> status 200 for success and 500 for badrequest</returns>
        [HttpPut]
        public async Task<IActionResult> Put(string email)
        {
            if (ModelState.IsValid)
            {
               
                // check if user  email exist
                var user = _userRepository.QueryWithOptions(a => a.Email == email).SingleOrDefault();
                if (user != null)
                {
                    user.DateConfirmed = DateTime.Now;
                    user.IsRegistered = true;
                     _userRepository.Update(user);
                    await _userRepository.Commit();
                    return Ok();
                }
                else
                {
                    return StatusCode(404, "User not found");
                }
                
            }
            else
            {
                var errors = new List<ApiError>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new ApiError
                        {
                            Detail = error.ErrorMessage
                        });
                    }
                }
                return BadRequest(errors);
            }
        }
        // POST api/1.0/user/Send-Email?email=email&baseUrl=
        /// <summary>
        /// Send Verification email with email and baseURl passed as query
        /// </summary>
        /// <returns> status 200 for success and 500 for badrequest</returns>
        [HttpPost]
        [Route("Send-Email")]
        public async Task<IActionResult> SendEmail([FromQuery] string baseUrl,[FromQuery] string email)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(email);
                mail.From = new MailAddress("famofash@gmail.com");
                mail.Subject = "Upstack Registration Verification";
                mail.IsBodyHtml = true;
                mail.Body = "<div> You above link to confirm your email <br/> " +
                            "<a href="+baseUrl+"/email="+ email + "> Click here to cofirm your account</a> </div>";

                SmtpClient SmtpServer = new SmtpClient(_sendgrid.SMTPHost);
                Object state = mail;

                SmtpServer.Port = Convert.ToInt16(_sendgrid.SMTPPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(_sendgrid.SMTPUser, _sendgrid.SMTPPassword);
                SmtpServer.EnableSsl = false;

                
                SmtpServer.Send(mail);
                return Ok();

            }
            else
            {
                var errors = new List<ApiError>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new ApiError
                        {
                            Detail = error.ErrorMessage
                        });
                    }
                }
                return BadRequest(errors);
            }
        }

    }
}
