using EasyCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// AccountController handles user registration, login, email verification, and JWT token generation
// It allows users to register, log in, log out, and verify their email for account activation.


namespace EasyCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager; //UserManager is used to handle operations related to user accounts
        private readonly SignInManager<Customer> _signInManager; //SignInManager is responsible for handling the authentication of users
        private readonly EmailService _emailService; // Service for sending emails (e.g., for email verification)
        private readonly IConfiguration _configuration;  // Access configuration settings, like JWT key

        // Constructor to inject dependencies
        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, EmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            var user = new Customer { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            // if (result.Succeeded)
            // {
            //     Generate an email verification token
            //     var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //     // Create the verification link
            //     var verificationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

            //     // Send the verification email 
            //     var emailSubject = "Email Verification";
            //     var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
            //     _emailService.SendEmail(user.Email, emailSubject, emailBody);
                
               
            //     return Ok("User registered successfully. An email verification link has been sent.");
            // }

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);   // Get the roles of the user (used for role-based authorisation)
                var token = GenerateJwtToken(user,roles);  // Generate a JWT (JSON Web Token) for the registered user
                return Ok(new { Token = token });
            }


            return BadRequest(result.Errors);
        }


        
        // [HttpGet("verify-email")]
        // public async Task<IActionResult> VerifyEmail(string userId, string token)
        // {
        //     // Attempt to find the user by their ID using UserManager

        //     var user = await _userManager.FindByIdAsync(userId);

        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }
        //    // Attempt to confirm the user's email using the provided token
        //     var result = await _userManager.ConfirmEmailAsync(user, token);

        //     if (result.Succeeded)
        //     {
        //         return Ok("Email verification successful.");
        //     }

        //     return BadRequest("Email verification failed.");
        // }



        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            //Attempt to sign in the user using the provided email and password
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // If successful, find the user by email using UserManager
                var roles = await _userManager.GetRolesAsync(user);   // Get the roles of the user (used for role-based authorisation)
                var token = GenerateJwtToken(user,roles);  // Generate a JWT (JSON Web Token) for the logged-in user
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid login attempt.");
        }

        //Logs out the currently authenticated user
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
        
        // Generates a JWT token for the user, including their roles
        private string GenerateJwtToken(Customer user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            // add user id as a claim
            claims.Add(new Claim("userId", user.Id.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
