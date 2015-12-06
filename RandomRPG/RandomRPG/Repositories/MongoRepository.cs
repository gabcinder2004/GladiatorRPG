using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Units;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace RandomRPG.Repositories
{
    public class MongoRepository : IRepository
    {
        private MongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }

        public MongoRepository()
        {
            Client = new MongoClient("mongodb://gabs:cooper@ds049104.mongolab.com:49104/gladiator?maxPoolSize=5000");
            Database = Client.GetDatabase("gladiator");
        }

        public async void AddGladiator(Gladiator glad)
        {
            //Need to figure out how to save all gladiator fields since it has methods on it, may have to store properties in another object, and instantiate a gladiator not sure
            //May have to refactor gladiator to just have a model/controller pattern
            //var collection = Database.GetCollection<Gladiator>("gladcontent");
            //await collection.InsertOneAsync(glad);
        }

        public async void AddGladiatorToHistory(Gladiator glad)
        {
            var GladiatorDBModel = new GladiatorViewModel(glad);
            var Collec = Database.GetCollection<GladiatorViewModel>("gladiatorhist");

            await Collec.InsertOneAsync(GladiatorDBModel);
            //Console.ReadLine();
        }

        public async Task<GladiatorViewModel> GetGladiatorHistory(string gladName)
        {
            //var Collec = Database.GetCollection<BsonDocument>("gladiatorhist");
            //var filter = Builders<BsonDocument>.Filter.Eq("Name", gladName);
            //var result = Collec.Find(filter).ToListAsync();
            //var temp = result.Result;
            //var myObj = BsonSerializer.Deserialize<GladiatorViewModel>(temp.FirstOrDefault());
            
            //return myObj;
            //Console.ReadLine();
            var Collec = Database.GetCollection<GladiatorViewModel>("gladiatorhist");
            var filter = Builders<GladiatorViewModel>.Filter.Eq("Name", gladName);
            var result = Collec.Find(filter).ToListAsync();
            var temp = result.Result.FirstOrDefault();
            return temp;
        }

        public async void RemoveGladiatorHistoryRecord(string gladName)
        {
            //var Collec = Database.GetCollection<BsonDocument>("gladiatorhist");
            //var filter = Builders<BsonDocument>.Filter.Eq("Name", gladName);
            //var result = Collec.Find(filter).ToListAsync();
            //var temp = result.Result;
            //var myObj = BsonSerializer.Deserialize<GladiatorViewModel>(temp.FirstOrDefault());

            //return myObj;
            //Console.ReadLine();
            var filter = Builders<GladiatorViewModel>.Filter.Eq("Name", gladName);
            await Database.GetCollection<GladiatorViewModel>("gladiatorhist").DeleteOneAsync(filter);
        }
    }
    
    public class GladiatorViewModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string kills { get; set; }
        public string Reputation { get; set; }
        public string GladiatorType { get; set; }
        public GladiatorViewModel(Gladiator glad)
        {
            this.Name = glad.Name;
            this.kills = glad.Kills.ToString();
            this.Reputation = glad.Reputation.ToString();
        }
    }

    public interface IRepository
    {
        void AddGladiator(Gladiator glad);
        void AddGladiatorToHistory(Gladiator glad);
    }
}
