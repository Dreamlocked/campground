using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Campground.Services.Billings.Infrastructure.Data.Repository;
namespace Campground.Services.Billings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController(BillingsRepository billingsRespository) : ControllerBase
    {

        private readonly BillingsRepository _billingsRepository = billingsRespository;

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
        public async Task<IActionResult> Post(Domain.Entities.Billing newBilling)
        {
            await _billingsRepository.CreateAsync(newBilling);

            return CreatedAtAction(nameof(Get), new { id = newBilling.Id }, newBilling);
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
