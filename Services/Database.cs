using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB;
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

      var constring = "mongodb://1234:1234@172.17.0.1:27017/test";

      var settings = MongoClientSettings.FromConnectionString(constring);
      // Set the version of the Versioned API on the client.
      settings.ServerApi = new ServerApi(ServerApiVersion.V1);
      var client = new MongoClient(settings);

      Database._database = client.GetDatabase("senpher");

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
