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
  public class GroupController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Group> _entityRepo;
    private readonly ILogger<GroupController> _logger;

    public GroupController(
      KinderContext kinderContext,
      IEntityRepo<Group> entityRepo,
      ILogger<GroupController> logger
      )
    {
      _kinderContext = kinderContext;
      _entityRepo = entityRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Group>>> GetMany()
    {
       _logger.LogInformation("| Log || Group || GetAll |");
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
    public async Task<ActionResult<Group>> GetSingle(long id)
    {
      _logger.LogInformation("| Log || Group || GetSingle |");
      var item = await _entityRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Add(CreateGroupDto group)
    {
      _logger.LogInformation("| Log || Group || Add |");
      var x = new Group{
        Name = group.Name,
        EducatorId = group.EducatorId,
        Id = _kinderContext.Contracts.Max(x => x.Id) + 1
    };
      var item = await _entityRepo.Add(x);
      return CreatedAtAction("GetSingle", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<Group>> Update(long id, CreateGroupDto group)
    {
      _logger.LogInformation("| Log || Group || Update |");
      var x = new Group{
        Name = group.Name,
        EducatorId = group.EducatorId,
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
      _logger.LogInformation("| Log || Group || Delete |");
      await _entityRepo.Remove(id);
      return NoContent();
    }
  }
}