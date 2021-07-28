using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Senpher.Models;
using Senpher.Services;

namespace Senpher.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FileController : ControllerBase
  {
    [HttpGet("{id}")]
    public File Get(int id)
    {
      var db = Database.Get();
      var col = db.GetCollection<File>("file");
      Console.WriteLine(col);

      return new File
      {
        Name = $"grobgroop {id}"
      };
    }
  }
}
