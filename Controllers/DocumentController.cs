using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
    public async Task<FileResult> Get(string id)
    {
      var db = Database.Get();
      var col = db.GetCollection<Document>("files");

      Document doc = (Document)await (await col.FindAsync(x => x.ID == id, new FindOptions<Document> { Limit = 1 }))
        .FirstOrDefaultAsync();

      var path = Path.Join("./uploaded-files", doc.ID, doc.Name);
      new FileExtensionContentTypeProvider().TryGetContentType(path, out var contentType);
      contentType ??= "application/unknown";

      return new FileContentResult(System.IO.File.ReadAllBytes(path), contentType)
      {
        FileDownloadName = doc.Name
      };
    }

    [HttpPost("{name}")]
    public async Task<Document> RecieveFile(string name)
    {
      foreach (var h in Request.Headers)
      {
        Console.WriteLine(h);
      }

      var doc = new Document
      {
        Name = name
      };

      // Add to DB
      var db = Database.Get();
      var col = db.GetCollection<Document>("files");
      await col.InsertOneAsync(doc);

      // Make sure request can be read multiple times
      Request.EnableBuffering();

      // Read stream
      StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8, true, 4096);
      var content = await sr.ReadToEndAsync();

      // Write stream to file
      var outDir = $"./uploaded-files/{doc.ID}";
      Directory.CreateDirectory(outDir);
      StreamWriter sw = new StreamWriter(Path.Join(outDir, doc.Name), false, Encoding.UTF8, 4096);
      sw.Write(content);
      sw.Close();

      // Reset position in stream
      Request.Body.Position = 0;

      return doc;
    }
  }
}
