using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.Domain.Pitches.DataModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.MongoDB.Ideas
{
    public class IdeasRepository : IWriteIdeas, IReadIdeas
    {
        private readonly MongoDBOptions _mongoDBOptions;
        private readonly IMongoDatabase _database;
        private const string IdeaCollectionName = "ideas";
        private const string PicthCollectionName = "pitches";

        public IdeasRepository(IOptionsSnapshot<MongoDBOptions> mongoDBOptionsSnapshot)
        {
            _mongoDBOptions = mongoDBOptionsSnapshot.Value;
            var mongoClient = new MongoClient(_mongoDBOptions.ConnectionString);
            _database = mongoClient.GetDatabase(_mongoDBOptions.DatabaseName);
        }

        public async Task<string> AddIdeaAsync(IdeaInput input, string userId, CancellationToken cancellationToken = default)
        {
            var ideaCollection = _database.GetCollection<Idea>(IdeaCollectionName);
            var id = Guid.NewGuid().ToString();

            await ideaCollection.InsertOneAsync(new Idea(id, input.Name, input.Description, userId), new InsertOneOptions { }, cancellationToken);

            return id;
        }

        public async Task<IdeaResultSet> GetIdeasAsync(GetIdeasRequest request, CancellationToken cancellationToken = default)
        {
            var ideaCollection = _database.GetCollection<Idea>(IdeaCollectionName);
            var filterBuilder = Builders<Idea>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, request.UserId);
            var filter = userFiler;

            if (!string.IsNullOrWhiteSpace(request.PitchId))
            {
                var pitchesCollection = _database.GetCollection<Pitch>(PicthCollectionName);
                var pitch = await pitchesCollection.Find((p) => p.Id == request.PitchId).FirstOrDefaultAsync(cancellationToken);

                if (pitch != null)
                {
                    var ideaFilter = filterBuilder.Eq(c => c.Id, pitch.IdeaId);
                    filter = filterBuilder.And(userFiler, ideaFilter);
                }
            }

            var find = ideaCollection.Find(filter);

            var totalCount = await find.CountDocumentsAsync(cancellationToken);

            var ideas = await find.Skip(request.Skip).Limit(request.Take).ToListAsync(cancellationToken);

            return new IdeaResultSet(ideas, totalCount, request.Skip + ideas.Count < totalCount, request.Skip > 0 && ideas.Count > 0);
        }

        public Task RemoveIdeaAsync(string id, string userId, CancellationToken cancellationToken = default)
        {
            var ideaCollection = _database.GetCollection<Client>(IdeaCollectionName);
            var filterBuilder = Builders<Client>.Filter;
            var userFiler = filterBuilder.Eq((c) => c.UserId, userId);
            var clientFilter = filterBuilder.Eq(c => c.Id, id);
            var filter = filterBuilder.And(userId, clientFilter);

            return ideaCollection.DeleteOneAsync(filter, cancellationToken);
        }
    }
}
