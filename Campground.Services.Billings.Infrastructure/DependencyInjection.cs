using Campground.Services.Billings.Infrastructure.Data;
using Campground.Services.Billings.Infrastructure.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campground.Services.Billings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection Infrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BillingStoreDatabaseSettings>(
                configuration.GetSection("BillingStoreDatabase"));

            services.AddScoped<BillingsRepository>();

            return services;
        }
    }
}
