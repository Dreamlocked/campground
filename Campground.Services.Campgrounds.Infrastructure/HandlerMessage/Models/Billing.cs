using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Campgrounds.Infrastructure.HandlerMessage.Models
{
    public class Billing
    {
        public string? Id { get; set; }

        public string? TenantId { get; set; }

        public string? HostId { get; set; }

        public string? BookingId { get; set; }

        public decimal? Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
