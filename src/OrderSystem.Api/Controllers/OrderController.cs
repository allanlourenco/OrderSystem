using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Services;

namespace OrderSystem.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid productId, int quantity)
    {
        try
        {
            var orderId = await _service.CreateOrderAsync(productId, quantity);
            return Ok(orderId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}