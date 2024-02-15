using Campground.Services.Billings.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Billings.Infrastructure.Data.Repository
{
    public class BillingsRepository
    {
        private readonly IMongoCollection<Billing> _billingsCollection;

        public BillingsRepository(
            IOptions<BillingStoreDatabaseSettings> billingStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                billingStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                billingStoreDatabaseSettings.Value.DatabaseName);

            _billingsCollection = mongoDatabase.GetCollection<Domain.Entities.Billing>(
                billingStoreDatabaseSettings.Value.BillingsCollectionName);
        }

        public async Task<List<Domain.Entities.Billing>> GetAsync() =>
            await _billingsCollection.Find(_ => true).ToListAsync();

        public async Task<Domain.Entities.Billing?> GetAsync(string id) =>
            await _billingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Domain.Entities.Billing?> GetAsyncByTenant(string id) =>
            await _billingsCollection.Find(x => x.TenantId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Domain.Entities.Billing newBilling) =>
            await _billingsCollection.InsertOneAsync(newBilling);

        public async Task UpdateAsync(string id, Domain.Entities.Billing updatedBilling) =>
            await _billingsCollection.ReplaceOneAsync(x => x.Id == id, updatedBilling);

        public async Task RemoveAsync(string id) =>
            await _billingsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
