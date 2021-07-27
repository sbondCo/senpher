using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Senpher.Models;

namespace Senpher.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FileController : ControllerBase
  {
    public FileController()
    {
    }

    [HttpGet("{id}")]
    public File Get(int id)
    {
      return new File
      {
        Name = $"hello {id}"
      };
    }
  }
}
