using Cars.Authentication;
using Cars.Models;
using Cars.Models.VeiwModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cars.Controllers
{
    public class AccountController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager= roleManager;


        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser(UserRegesiterVeiwModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email, // Directly using properties from ApplicationUser
                PhoneNumber = model.Phone
         
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync("UserRole");

                if (!roleExists)
                {
                    // If the role doesn't exist, create it
                    var role = new IdentityRole("UserRole");
                    await _roleManager.CreateAsync(role);
                }

                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, "UserRole");
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }


        [HttpPost("register/Company")]
       public async Task<IActionResult> RegisterCompany(CompanyRegesterVeiwModel model, IFormFile file, IFormFile Logo)
        {

            byte[] companyImage = null;
            byte[] companyLogo = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (file.Length > 0&& Logo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    companyImage = memoryStream.ToArray();


                }
                using (var memoryStream = new MemoryStream())
                {
                    await Logo.CopyToAsync(memoryStream);

                     companyLogo = memoryStream.ToArray();

                }

            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Logo = companyLogo,
                image = companyImage
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)

            {
                var roleExists = await _roleManager.RoleExistsAsync("CompanyRole");

                if (!roleExists)
                {
                    // If the role doesn't exist, create it
                    var role = new IdentityRole("CompanyRole");
                    await _roleManager.CreateAsync(role);
                }

                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, "CompanyRole");
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    Boolean found = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                    if (found)
                    {
                        var cliams = new List<Claim>();
                        cliams.Add(new Claim(ClaimTypes.Name, user.UserName));
                        cliams.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        cliams.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            cliams.Add(new Claim(ClaimTypes.Role, role));


                        }
                        SecurityKey key = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                        SigningCredentials signingCredentials = new SigningCredentials(
                            key, SecurityAlgorithms.HmacSha256
                            );

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _configuration["JWT:Issuer"],

                            audience: _configuration["JWT:Audience"],
                            claims: cliams,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials


                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            exiration = token.ValidTo

                        });



                    }
                }


            }
            return Unauthorized();
        }
    }

  

}
