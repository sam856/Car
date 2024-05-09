using Cars.Authentication;
using Cars.Helper;
using Cars.Interfaces;
using Cars.Models;
using Cars.Models.VeiwModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cars.Controllers
{
    //[Authorize(Roles = "UserRole")]

    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailController(IEmailService emailService , UserManager<ApplicationUser> _userManager)
        {
            this.emailService = emailService;
            this._userManager = _userManager;
        }   
        [HttpPost("Email")]
        public async Task <IActionResult>SendEmail([FromForm] Mailrequest mailRequest)
        {
            Random random = new Random();
            int[] numbers = new int[6];
            for (int i = 0; i < 6; i++)
            {
                numbers[i] = random.Next(0, 9); // You can adjust the range as needed
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true, 
                Secure = true   
            };
            string numbersString = string.Join("", numbers);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string emailBody = $"Here are your Pass number  {numbersString}\n\n" +
                               $" Availiable until : {timestamp}";

            await emailService.SendEmailAsync(mailRequest, emailBody);
            Response.Cookies.Append("UserCode", numbersString, cookieOptions);
            Response.Cookies.Append("UserEmail", mailRequest.Email, cookieOptions);
            return Ok();
        }



        [HttpPost("Email/CheckCode")]
        public async Task<IActionResult> CheckCode([FromForm]  string code)
        {
            var Code = Request.Cookies["UserCode"];
            if (code == Code)
            {
                return Ok();
            }
            return BadRequest();

        }
        [HttpPost("Email/newpassword")]
        public async Task<IActionResult> NewPassword([FromForm] Passwords passwords)
        {
            var email = Request.Cookies["UserEmail"];
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, passwords.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }





    }
}
