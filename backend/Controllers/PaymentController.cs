using System.Linq;
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
  public class PaymentController : ControllerBase
  {
    private static KinderContext _kinderContext;
    private static IEntityRepo<Payment> _paymentRepo;
    public PaymentController(KinderContext kinderContext, IEntityRepo<Payment> paymentRepo)
    {
      _kinderContext = kinderContext;
      _paymentRepo = paymentRepo;

    }

    [HttpGet]
    public async Task<ActionResult<List<Payment>>> GetPays()
    {
      var items = await _paymentRepo.GetAll();
      return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetSinglePayment(long id)
    {
      var pay = await _paymentRepo.GetById(id);
      if (pay is null)
        return NotFound("Sorry, but this pay doesn't exist");
      return Ok(pay);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> AddPay(CreatePaymentDto pay)
    {
      var payment = new Payment{
        DateOfPayment = pay.DateOfPayment,
        TypeOfPayment = pay.TypeOfPayment,
        PaymentAmount = pay.PaymentAmount,
        ChildId = pay.ChildId,
        Id = _kinderContext.Payments.Max(p => p.Id) + 1
    };
      var item = await _paymentRepo.Add(payment);
      return CreatedAtAction("GetSinglePayment", item.Id, item);
    }

    [HttpPatch]
    public async Task<ActionResult<Payment>> Update(long id, CreatePaymentDto pay)
    {
      var payment = new Payment{
        DateOfPayment = pay.DateOfPayment,
        TypeOfPayment = pay.TypeOfPayment,
        PaymentAmount = pay.PaymentAmount,
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
      await _paymentRepo.Remove(id);
      return NoContent();
    }
    //получить Список детей у которых прошла оплата в определённой сумме
    [HttpGet]
    [Route("PaysForAmount")]
    public async Task<ActionResult> GetLambdaPays(double amount)
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
    //получить Список детей у которых прошла оплата за заданную дату
    //Экранирую - не работает
    [HttpGet]
    [Route("DifferentPay")]
    public async Task<ActionResult> GetMonthPays() {
      var pays = _kinderContext.Payments.FromSqlInterpolated($"select \"Children\".\"Id\", \"Children\".\"Name\", \"Payments\".\"DateOfPayment\" from \"Children\" Right join \"Payments\" on \"Payments\".\"ChildId\" = \"Children\".\"Id\" where \"Payments\".\"DateOfPayment\" = '2020-10-13'").ToList();
      return Ok(pays);
    }
  }

}