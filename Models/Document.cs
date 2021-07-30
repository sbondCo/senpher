using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Senpher.Models
{
  public class Document
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    public string Name { get; set; }
  }
}
