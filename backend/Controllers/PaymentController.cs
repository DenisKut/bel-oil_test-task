using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [Route("api/[controller]")] //Обновляем роут по названию контроллера, избавляясь от хардода
  [ApiController]
  public class PaymentController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Hello from my controller");
    }
  }
}