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
    public class ChildController : ControllerBase
    {
      private static KinderContext _kinderContext;
      private static IEntityRepo<Child> _childRepo;
      private readonly ILogger<ChildController> _logger;
      public ChildController(KinderContext kinderContext, IEntityRepo<Child> childRepo, ILogger<ChildController> logger)
      {
        _kinderContext = kinderContext;
        _childRepo = childRepo;
        _logger = logger;
      }

    [HttpGet]
    public async Task<ActionResult<List<Child>>> GetChildren()
    {
       _logger.LogInformation("| Log || Child || GetAll |");
      try
      {
        var items = await _childRepo.GetAll();
        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Child>> GetSingleChild(long id)
    {
      _logger.LogInformation("| Log || Child || GetSingleChild |");
      var item = await _childRepo.GetById(id);
      if (item is null)
        return NotFound("Sorry, but this child doesn't exist");
      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Child>> AddChild(CreateChildDto child)
    {
      _logger.LogInformation("| Log || Child || AddChild |");
      var ch = new Child{
        Name = child.Name,
        Age = child.Age,
        Passport = child.Passport,
        StateOfHealth = child.StateOfHealth,
        GroupId = child.GroupId,
        Weight = child.Weight,
        Growth = child.Growth,
        ParentInfoId = child.ParentInfoId,
        ContractId = child.ContractId,
        Region = child.Region,
        DirtyZone = _kinderContext.Zone.Any(x => x.NameOfDirtyZone == child.Region),
        Id = _kinderContext.Children.Max(x => x.Id) + 1
    };
      var item = await _childRepo.Add(ch);
      return CreatedAtAction("GetSingleChild", item.Id, item);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Child>> Update(long id, CreateChildDto child)
    {
      _logger.LogInformation("| Log || Child || Update |");
      var ch = new Child{
        Name = child.Name,
        Age = child.Age,
        Passport = child.Passport,
        StateOfHealth = child.StateOfHealth,
        GroupId = child.GroupId,
        Weight = child.Weight,
        Growth = child.Growth,
        ParentInfoId = child.ParentInfoId,
        ContractId = child.ContractId,
        Region = child.Region,
        DirtyZone = _kinderContext.Zone.Any(x => x.NameOfDirtyZone == child.Region),
        Id = id
    };
      var item = await _childRepo.Update(ch);
      if(item == null)
        return BadRequest("Invalid id");

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
      _logger.LogInformation("| Log || Child || Delete |");
      await _childRepo.Remove(id);
      return NoContent();
    }

    //получить детей и их родителей в чистой зоне
    [HttpGet]
    [Route("ChildrenWithoutDirtyZone")]
    public async Task<ActionResult<List<Child>>> GetClearZoneChildren()
    {
      _logger.LogInformation("| Log || Child || GetClearZoneChildren |");
      try
      {
        //Чисто дети (интерпретация is not exist)
        // var items = from c in _kinderContext.Children
        //   where !_kinderContext.ParentInfos.Any(p => (p.Id == c.ParentInfoId) && (c.DirtyZone == true))
        //   select c;

      //Вместе с родителями
        var items = _kinderContext.Children.Join(
        _kinderContext.ParentInfos,
        children => children.ParentInfoId,
        parents => parents.Id,
          (child, parent) => new {
            child.Id, child.Name, child.Age, child.DirtyZone, child.Region,
            child.ParentInfoId, parent.Mother, parent.Father
          }
      )
      .Where(x => x.DirtyZone != true)
      .Select(c => c);

        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    //ФИО детей за которых заплатили в пределах даты (ДАТА в виде YYYY-MM-DD)
    [HttpGet]
    [Route("InfoAboutPaysByDates")]
    public async Task<ActionResult<List<Child>>> GetChildrenWithPays(DateTime FirstDate, DateTime LastDate)
    {
      _logger.LogInformation("| Log || Child ||  GetChildrenWithPays |");
      try
      {
        var items = _kinderContext.Children.Join(
        _kinderContext.Payments,
        children => children.Id,
        pays => pays.ChildId,
          (child, pays) => new {
            child.Id, child.Name, child.Age, child.DirtyZone, child.Region,
            child.ParentInfoId, pays.DateOfPayment, pays.TypeOfPayment, pays.PaymentAmount
          }
      )
      .Where(x => x.DateOfPayment >= FirstDate && x.DateOfPayment <= LastDate )
      .Select(c => c);

        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}