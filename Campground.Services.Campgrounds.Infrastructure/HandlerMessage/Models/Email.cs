using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Campgrounds.Infrastructure.Queue.Models
{
    public class Email
    {
        public required string To { get; init; }
        public required string Subject { get; init; }
        public required string Body { get; init; }
    }
}
