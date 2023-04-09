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
  public class HeadController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<HeadOfKindergarten> _entityRepo;
    private readonly ILogger<HeadController> _logger;

    public HeadController(
      KinderContext kinderContext,
      IEntityRepo<HeadOfKindergarten> entityRepo,
      ILogger<HeadController> logger
      )
    {
      _kinderContext = kinderContext;
      _entityRepo = entityRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<HeadOfKindergarten>>> GetMany()
    {
       _logger.LogInformation("| Log || HeadOfKindergarten || GetAll |");
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
    public async Task<ActionResult<HeadOfKindergarten>> GetSingle(long id)
    {
      _logger.LogInformation("| Log || HeadOfKindergarten || GetSingle |");
      var item = await _entityRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<HeadOfKindergarten>> Add(CreateHeadDto head)
    {
      _logger.LogInformation("| Log || HeadOfKindergarten || Add |");
      var x = new HeadOfKindergarten{
        Name = head.Name,
        Age = head.Age,
        Email = head.Email,
        Id = _kinderContext.Contracts.Max(x => x.Id) + 1
    };
      var item = await _entityRepo.Add(x);
      return CreatedAtAction("GetSingle", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<HeadOfKindergarten>> Update(long id, CreateHeadDto head)
    {
      _logger.LogInformation("| Log || HeadOfKindergarten || Update |");
      var x = new HeadOfKindergarten{
        Name = head.Name,
        Age = head.Age,
        Email = head.Email,
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
      _logger.LogInformation("| Log || HeadOfKindergarten || Delete |");
      await _entityRepo.Remove(id);
      return NoContent();
    }
  }
}