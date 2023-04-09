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
  public class EducatorController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Educator> _entityRepo;
    private readonly ILogger<EducatorController> _logger;

    public EducatorController(
      KinderContext kinderContext,
      IEntityRepo<Educator> entityRepo,
      ILogger<EducatorController> logger
      )
    {
      _kinderContext = kinderContext;
      _entityRepo = entityRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Educator>>> GetMany()
    {
       _logger.LogInformation("| Log || Educator || GetAll |");
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
    public async Task<ActionResult<Educator>> GetSingle(long id)
    {
      _logger.LogInformation("| Log || Educator || GetSingle |");
      var item = await _entityRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Educator>> Add(CreateEducatorDto educator)
    {
      _logger.LogInformation("| Log || Educator || Add |");
      var x = new Educator{
        Name = educator.Name,
        Age = educator.Age,
        Email = educator.Email,
        Id = _kinderContext.Contracts.Max(x => x.Id) + 1
    };
      var item = await _entityRepo.Add(x);
      return CreatedAtAction("GetSingle", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<Educator>> Update(long id, CreateEducatorDto educator)
    {
      _logger.LogInformation("| Log || Educator || Update |");
     var x = new Educator{
        Name = educator.Name,
        Age = educator.Age,
        Email = educator.Email,
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
      _logger.LogInformation("| Log || Educator || Delete |");
      await _entityRepo.Remove(id);
      return NoContent();
    }
  }
}