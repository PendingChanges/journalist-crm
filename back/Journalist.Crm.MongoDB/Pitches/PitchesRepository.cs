using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.MongoDB.Pitches
{
    internal class PitchesRepository : IReadPitches, IWritePitches
    {
        private readonly MongoDBOptions _mongoDBOptions;
        private readonly IMongoDatabase _database;
        private const string PicthCollectionName = "pitches";

        public PitchesRepository(IOptionsSnapshot<MongoDBOptions> mongoDBOptionsSnapshot)
        {
            _mongoDBOptions = mongoDBOptionsSnapshot.Value;
            var mongoClient = new MongoClient(_mongoDBOptions.ConnectionString);
            _database = mongoClient.GetDatabase(_mongoDBOptions.DatabaseName);
        }

        public async Task<PitchResultSet> GetPitchesAsync(GetPitchesRequest request, CancellationToken cancellationToken = default)
        {
            var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
            var filterBuilder = Builders<Pitch>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, request.UserId);
            var filter = userFiler;

            if (!string.IsNullOrWhiteSpace(request.ClientId))
            {
                var clientFilter = filterBuilder.Eq(c => c.ClientId, request.ClientId);
                filter = filterBuilder.And(filter, clientFilter);
            }

            if (!string.IsNullOrWhiteSpace(request.IdeaId))
            {
                var ideaFilter = filterBuilder.Eq(c => c.IdeaId, request.IdeaId);
                filter = filterBuilder.And(filter, ideaFilter);
            }

            var find = pitchesCollection.Find(filter);

            var totalCount = await find.CountDocumentsAsync(cancellationToken);

            var pitches = await find.Skip(request.Skip).Limit(request.Take).ToListAsync(cancellationToken);

            return new PitchResultSet(pitches, totalCount, request.Skip + pitches.Count < totalCount, request.Skip > 0 && pitches.Count > 0);
        }

        public async Task<long> GetPitchesNbByClientIdAsync(string clientId, string userId, CancellationToken cancellationToken = default)
        {
            var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
            var filterBuilder = Builders<Pitch>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, userId);
            var clientFilter = filterBuilder.Eq(c => c.ClientId, clientId);
            var filter = filterBuilder.And(userFiler, clientFilter);

            var totalCount = await pitchesCollection.Find(filter).CountDocumentsAsync(cancellationToken);

            return totalCount;
        }

        public async Task<string> AddPitchAsync(PitchInput pitchInput, string userId, CancellationToken cancellationToken)
        {
            var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
            var id = Guid.NewGuid().ToString();

            await pitchesCollection.InsertOneAsync(new Pitch(id, pitchInput.Title, pitchInput.Content, pitchInput.DeadLineDate, pitchInput.IssueDate, "DRAFT", pitchInput.ClientId, pitchInput.IdeaId, userId), new InsertOneOptions { }, cancellationToken);

            return id;
        }

        public async Task<long> GetPitchesNbByIdeaIdAsync(string ideaId, string userId, CancellationToken cancellationToken)
        {
            var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
            var filterBuilder = Builders<Pitch>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, userId);
            var ideaFilter = filterBuilder.Eq(c => c.IdeaId, ideaId);
            var filter = filterBuilder.And(userFiler, ideaFilter);

            var totalCount = await pitchesCollection.Find(filter).CountDocumentsAsync(cancellationToken);

            return totalCount;
        }
    }
}
