using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Campground.Services.Billings.Infrastructure.Data.Repository;
using Campground.Services.Billings.Infrastructure.HandlerMessage;
using Microsoft.AspNetCore.Authorization;
using Campground.Services.Billings.Models;
namespace Campground.Services.Billings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController(BillingsRepository billingsRespository,MessageSender messageSender) : ControllerBase
    {

        private readonly BillingsRepository _billingsRepository = billingsRespository;
        private readonly MessageSender _messageSender = messageSender;

        [HttpGet]
        public async Task<List<Domain.Entities.Billing>> Get() => await _billingsRepository.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Domain.Entities.Billing>> Get(string id)
        {
            var billing = await _billingsRepository.GetAsync(id);

            if(billing is null) return NotFound();

            return billing;
        }

        [HttpPost]
        public async Task<IActionResult> Post(BillingDto newBilling)
        {
            var billing = new Domain.Entities.Billing
            {
                TenantId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value,
                BookingId = newBilling.BookingId,
                Amount = newBilling.Amount,
                CreatedAt = DateTime.Now
            };
            await _billingsRepository.CreateAsync(billing);
            await _messageSender.SendBillingMessage(billing);

            return CreatedAtAction(nameof(Get), new { id = billing.Id }, billing);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Domain.Entities.Billing updateBilling)
        {
            var billing = await _billingsRepository.GetAsync(id);

            if(billing is null) return NotFound();

            updateBilling.Id = billing.Id;

            await _billingsRepository.UpdateAsync(id, updateBilling);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var billing = await _billingsRepository.GetAsync(id);

            if(billing is null) return NotFound();

            await _billingsRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}
