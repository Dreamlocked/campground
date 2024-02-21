﻿using Campground.Shared.Authentication;
using Campground.Shared.Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Authentications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(JwtTokenHandler jwtTokenHandler) : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

        [HttpPost("/api/login")]
        public async Task<ActionResult<AuthenticationResponse?>> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = await _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if(authenticationResponse == null) return Unauthorized();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,
                SameSite = SameSiteMode.None, 
                Expires = DateTime.UtcNow.AddDays(1)
            };
            Response.Cookies.Append("accessToken", authenticationResponse.JwtToken, cookieOptions);
            return Ok(authenticationResponse);
        }

    }
}
