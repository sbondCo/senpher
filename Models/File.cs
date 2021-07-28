using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Senpher.Models
{
  public class File
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string Name { get; set; }
  }
}
