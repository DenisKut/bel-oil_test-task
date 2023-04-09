using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Contexts;
using Microsoft.EntityFrameworkCore;
using backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace backend.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")] //Обновляем роут по названию контроллера, избавляясь от хардода
  [ApiController]
  public class ZoneController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Zone> _entityRepo;
    private readonly ILogger<ZoneController> _logger;

    public ZoneController(
      KinderContext kinderContext,
      IEntityRepo<Zone> entityRepo,
      ILogger<ZoneController> logger
      )
    {
      _kinderContext = kinderContext;
      _entityRepo = entityRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Zone>>> GetMany()
    {
       _logger.LogInformation("| Log || Zone || GetAll |");
      try
      {
        var items = await _entityRepo.GetAll();
        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Zone>> GetSingle(long id)
    {
      _logger.LogInformation("| Log || Zone || GetSingle |");
      var item = await _entityRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Zone>> Add(Zone zone)
    {
      _logger.LogInformation("| Log || Zone || Add |");
      var x = new Zone{
        NameOfDirtyZone = zone.NameOfDirtyZone,
        Id = _kinderContext.Contracts.Max(x => x.Id) + 1
    };
      var item = await _entityRepo.Add(x);
      return CreatedAtAction("GetSingle", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<Zone>> Update(long id, Zone zone)
    {
      _logger.LogInformation("| Log || Zone || Update |");
      var x = new Zone{
        NameOfDirtyZone = zone.NameOfDirtyZone,
        Id = id
    };
      var item = await _entityRepo.Update(x);
      if(item == null)
        return BadRequest("Invalid id");

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
      _logger.LogInformation("| Log || Zone || Delete |");
      await _entityRepo.Remove(id);
      return NoContent();
    }
  }
}