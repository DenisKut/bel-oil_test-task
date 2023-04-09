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
  public class ContractController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Contract> _entityRepo;
    private readonly ILogger<ContractController> _logger;

    public ContractController(
      KinderContext kinderContext,
      IEntityRepo<Contract> entityRepo,
      ILogger<ContractController> logger
      )
    {
      _kinderContext = kinderContext;
      _entityRepo = entityRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contract>>> GetMany()
    {
       _logger.LogInformation("| Log || Contract || GetAll |");
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
    public async Task<ActionResult<Contract>> GetSingle(long id)
    {
      _logger.LogInformation("| Log || Contract || GetSingle |");
      var item = await _entityRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Contract>> Add(CreateContractDto contract)
    {
      _logger.LogInformation("| Log || Contract || Add |");
      var parents = new Contract{
        HeadId = contract.HeadId,
        Date = contract.Date,
        Description = contract.Description,
        Id = _kinderContext.Contracts.Max(x => x.Id) + 1
    };
      var item = await _entityRepo.Add(parents);
      return CreatedAtAction("GetSingle", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<Contract>> Update(long id, CreateContractDto contract)
    {
      _logger.LogInformation("| Log || Contract || Update |");
     var parents = new Contract{
        HeadId = contract.HeadId,
        Date = contract.Date,
        Description = contract.Description,
        Id = id
    };
      var item = await _entityRepo.Update(parents);
      if(item == null)
        return BadRequest("Invalid id");

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
      _logger.LogInformation("| Log || Contract || Delete |");
      await _entityRepo.Remove(id);
      return NoContent();
    }
  }
}