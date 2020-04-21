using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatAppSignalR.Pages
{
    public class LoginModel : PageModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string  Password { get; set; }
        public string  ReturnedUrl { get; set; }
        public void OnGet(string returnUrl = null)
        {
            ReturnedUrl = returnUrl;
        }
        public ActionResult OnPost([FromQuery] string returnurl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userName = Request.Form["userName"];
            string Password = Request.Form["Password"];


            if(Password != "123")
            {
                return Page();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , userName),
                new Claim(ClaimTypes.Role,"Support")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties
            {
                RedirectUri = returnurl ?? Url.Content("/")
            };


            return SignIn(new ClaimsPrincipal(identity), properties, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}