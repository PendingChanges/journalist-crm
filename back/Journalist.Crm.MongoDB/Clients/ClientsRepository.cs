using Amazon.Runtime.Internal;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.MongoDB.Clients
{
    public class ClientsRepository : IWriteClients, IReadClients
    {
        private readonly MongoDBOptions _mongoDBOptions;
        private readonly IMongoDatabase _database;
        private const string ClientCollectionName = "clients";
        private const string PicthCollectionName = "pitches";

        public ClientsRepository(IOptionsSnapshot<MongoDBOptions> mongoDBOptionsSnapshot)
        {
            _mongoDBOptions = mongoDBOptionsSnapshot.Value;
            var mongoClient = new MongoClient(_mongoDBOptions.ConnectionString);
            _database = mongoClient.GetDatabase(_mongoDBOptions.DatabaseName);
        }

        public async Task<string> AddClientAsync(ClientInput input, string userId, CancellationToken cancellationToken = default)
        {
            IMongoCollection<Client> clientCollection = GetClientCollection();
            var id = Guid.NewGuid().ToString();

            await clientCollection.InsertOneAsync(new Client(id, input.Name, userId), new InsertOneOptions { }, cancellationToken);

            return id;
        }

        public async Task<ClientResultSet> GetClientsAsync(GetClientsRequest request, CancellationToken cancellationToken = default)
        {
            var clientCollection = GetClientCollection();
            var filterBuilder = Builders<Client>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, request.UserId);
            var filter = userFiler;

            if (!string.IsNullOrWhiteSpace(request.PitchId))
            {
                var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
                var pitch = await pitchesCollection.Find((p) => p.Id == request.PitchId).FirstOrDefaultAsync(cancellationToken);

                if (pitch != null)
                {
                    var clientFilter = filterBuilder.Eq(c => c.Id, pitch.ClientId);
                    filter = filterBuilder.And(userFiler, clientFilter);
                }
            }

            var find = clientCollection.Find(filter);

            var totalCount = await find.CountDocumentsAsync(cancellationToken);

            var clients = await find.Skip(request.Skip).Limit(request.Take).ToListAsync(cancellationToken);

            return new ClientResultSet(clients, totalCount, request.Skip + clients.Count < totalCount, request.Skip > 0 && clients.Count > 0);
        }

        public Task RemoveClientAsync(string id, string userId, CancellationToken cancellationToken = 
default) => GetClientCollection().DeleteOneAsync(BuildOneClientFilter(id, userId), cancellationToken);

        public Task<Client?> GetClientAsync(string clientId, string userId, CancellationToken cancellationToken) 
            => GetClientCollection().Find(BuildOneClientFilter(clientId, userId)).FirstOrDefaultAsync<Client?>(cancellationToken);

        public async Task<IEnumerable<Client>> AutoCompleteClientasync(string text, string userId, CancellationToken cancellationToken)
        {
            var clientCollection = GetClientCollection();
            var filterBuilder = Builders<Client>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, userId);
            var queryExpr = new BsonRegularExpression(new Regex(text, RegexOptions.IgnoreCase));
            var clientNameFilter = filterBuilder.Regex(c => c.Name, queryExpr);
            var filter = filterBuilder.And(userFiler, clientNameFilter);

            return await clientCollection.Find(filter).ToListAsync(cancellationToken);
        }

        private FilterDefinition<Client> BuildOneClientFilter(string clientId, string userId)
        {
            var filterBuilder = Builders<Client>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, userId);
            var clientFilter = filterBuilder.Eq(c => c.Id, clientId);
            return filterBuilder.And(userFiler, clientFilter);
        }

        private IMongoCollection<Client> GetClientCollection() => _database.GetCollection<Client>(ClientCollectionName);
    }
}
