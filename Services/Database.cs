using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Senpher.Services
{
  public static class Database
  {
    private static IMongoDatabase _database;

    public static void Connect()
    {
      if (Database._database != null)
      {
        Console.WriteLine("Already connected to DB.");
        return;
      }

      var settings = new MongoClientSettings
      {
        Credential = MongoCredential.CreateCredential("senpher", "senpher", "senpherpwd"),
        Server = new MongoServerAddress("172.17.0.1", 27017)
      };

      // Set the version of the Versioned API on the client.
      settings.ServerApi = new ServerApi(ServerApiVersion.V1);

      var client = new MongoClient(settings);
      Database._database = client.GetDatabase("senpher");

      // Database._database.RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }").Wait();

      Console.WriteLine("Connected to DB");
    }

    public static IMongoDatabase Get()
    {
      if (Database._database != null)
      {
        return Database._database;
      }

      throw new Exception("Not connected to a DB");
    }
  }
}
