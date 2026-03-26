using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Services;

namespace OrderSystem.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, decimal price, int stock)
    {
        var id = await _service.CreateAsync(name, price, stock);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }
}