using Campground.Services.Emails.Models;
using Campground.Services.Emails.Services;
using Campground.Shared.Communication.AzureServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Emails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(EmailService emailService) : ControllerBase
    {
        private readonly EmailService _emailService = emailService;

        // Crea metodo para enviar correo
        [HttpPost]
        public async Task<IActionResult> Send(Email email)
        {
            await _emailService.SendMessageAsync(email);
            return Ok();
        }


    }
}
