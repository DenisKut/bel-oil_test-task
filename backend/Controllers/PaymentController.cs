using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Contexts;
using Microsoft.EntityFrameworkCore;
using backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

namespace backend.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")] //Обновляем роут по названию контроллера, избавляясь от хардода
  [ApiController]
  public class PaymentController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Payment> _paymentRepo;
    private readonly ILogger<PaymentController> _logger;
    public PaymentController(KinderContext kinderContext, IEntityRepo<Payment> paymentRepo, ILogger<PaymentController> logger)
    {
      _kinderContext = kinderContext;
      _paymentRepo = paymentRepo;
      _logger = logger;

    }

    [HttpGet]
    public async Task<ActionResult<List<Payment>>> GetPays()
    {
       _logger.LogInformation("| Log || Payment || GetAll |");
      try
      {
        var items = await _paymentRepo.GetAll();
        return Ok(items);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetSinglePayment(long id)
    {
      _logger.LogInformation("| Log || Payment || GetSinglePay |");
      var pay = await _paymentRepo.GetById(id);
      if (pay is null)
        return NotFound("Sorry, but this pay doesn't exist");
      return Ok(pay);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> AddPay(CreatePaymentDto pay)
    {
      _logger.LogInformation("| Log || Payment || AddPay |");

      //Скидка тем, кто живёт в зоне Чернобыльского загрязнения
      double discount = 0;
      var diryZoneLiving = _kinderContext.Children.Include(x => x.Id == pay.ChildId)
        .Where(x => x.DirtyZone == true);
      if(diryZoneLiving != null)
        discount = 0.3;

      var payment = new Payment{
        DateOfPayment = pay.DateOfPayment,
        TypeOfPayment = pay.TypeOfPayment,
        PaymentAmount = 250 * (1 - discount),
        ChildId = pay.ChildId,
        Id = _kinderContext.Payments.Max(p => p.Id) + 1
    };
      var item = await _paymentRepo.Add(payment);
      return CreatedAtAction("GetSinglePayment", item.Id, item);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Payment>> Update(long id, CreatePaymentDto pay)
    {
      _logger.LogInformation("| Log || Payment || Update |");
      var payment = new Payment{
        DateOfPayment = pay.DateOfPayment,
        TypeOfPayment = pay.TypeOfPayment,
        ChildId = pay.ChildId,
        Id = id
    };
      var item = await _paymentRepo.Update(payment);
      if(item == null)
        return BadRequest("Invalid id");

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
      _logger.LogInformation("| Log || Payment || Delete |");
      await _paymentRepo.Remove(id);
      return NoContent();
    }


    //получить Список детей у которых прошла оплата в определённой сумме
    [HttpGet]
    [Route("PaysForAmount")]
    public async Task<ActionResult<List<Payment>>> GetLambdaPays(double amount)
    {
      var pays = _kinderContext.Payments.Join(
        _kinderContext.Children,
        paymentsChID => paymentsChID.ChildId,
        childID => childID.Id,
          (payments, child) => new {
            payments.DateOfPayment, child.Name, payments.PaymentAmount
          }
      )
      .Where(payments => payments.PaymentAmount >= amount)
      .Select(s=>s);
      return Ok(pays);
    }
  }
}