using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB;
using MongoDB.Driver;
using Senpher.Models;
using Senpher.Services;

namespace Senpher.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DocumentController : ControllerBase
  {
    [HttpGet("{id}")]
    public Document Get(string id)
    {
      var db = Database.Get();
      var col = db.GetCollection<Document>("files");
      Console.WriteLine(col);

      // return col.Find(_ => true).ToList();

      return (Document)col.Find(x => x.ID == id);

      // return new File
      // {
      //   Name = $"grobgroop {id}"
      // };
    }

    [HttpPost]
    public async Task<string> RecieveFile()
    {
      Console.WriteLine($"ContentType: {Request.ContentType}");
      Console.WriteLine($"ContentLength: {Request.ContentLength}");

      Request.EnableBuffering();

      StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8, true, 4096);
      var content = await sr.ReadToEndAsync();

      StreamWriter sw = new StreamWriter("./uploaded-file", false, Encoding.UTF8, 4096);
      sw.Write(content);
      sw.Close();

      // Reset position in stream
      Request.Body.Position = 0;

      return content;
    }
  }
}
