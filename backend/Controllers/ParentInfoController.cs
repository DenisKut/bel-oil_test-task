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
  public class ParentInfoController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<ParentInfo> _parentRepo;
    private readonly ILogger<ParentInfoController> _logger;

    public ParentInfoController(KinderContext kinderContext, IEntityRepo<ParentInfo> parentRepo, ILogger<ParentInfoController> logger)
    {
      _kinderContext = kinderContext;
      _parentRepo = parentRepo;
      _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ParentInfo>>> GetParents()
    {
       _logger.LogInformation("| Log || ParentInfo || GetAll |");
      try
      {
        var items = await _parentRepo.GetAll();
        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParentInfo>> GetSingleParent(long id)
    {
      _logger.LogInformation("| Log || ParentInfo || GetSingle |");
      var item = await _parentRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this item doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ParentInfo>> AddParent(CreateParentsDto parent)
    {
      _logger.LogInformation("| Log || ParentInfo || Add |");
      var parents = new ParentInfo{
        Mother = parent.Mother,
        Father = parent.Father,
        FatherProfession = parent.FatherProfession,
        MotherProfession = parent.MotherProfession,
        FatherAge = parent.FatherAge,
        MotherAge = parent.MotherAge,
        Id = _kinderContext.ParentInfos.Max(x => x.Id) + 1
    };
      var item = await _parentRepo.Add(parents);
      return CreatedAtAction("GetSingleParent", item.Id, item);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ParentInfo>> Update(long id, CreateParentsDto parent)
    {
      _logger.LogInformation("| Log || ParentInfo || Update |");
     var parents = new ParentInfo{
        Mother = parent.Mother,
        Father = parent.Father,
        FatherProfession = parent.FatherProfession,
        MotherProfession = parent.MotherProfession,
        FatherAge = parent.FatherAge,
        MotherAge = parent.MotherAge,
        Id = id
    };
      var item = await _parentRepo.Update(parents);
      if(item == null)
        return BadRequest("Invalid id");

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
      _logger.LogInformation("| Log || ParentInfo || Delete |");
      await _parentRepo.Remove(id);
      return NoContent();
    }

    //У кого больше одного ребёнка в саду
    [HttpGet]
    [Route("GetParentsWithManyChildren")]
    public async Task<ActionResult<List<ParentInfo>>> GetParentsWithManyChildren()
    {
      _logger.LogInformation("| Log || ParentInfo ||  GetParentsWithManyChildren |");
      try
      {
        var items = _kinderContext.ParentInfos.GroupJoin(
          _kinderContext.Children,
          parents => parents.Id,
          child => child.ParentInfoId,
            (parents, children) => new {
              parents.Id,
              parents.Father,
              parents.Mother,
              countOfChilds = children.Count()
            }
        )
        .Where(x => x.countOfChilds > 1)
        .Select(x => x);

        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}