using System.Security.Claims;
using Cars.Api.Models.Cars;
using Cars.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/cars")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCars([FromQuery] GetCarsFilter filter)
    {
        var cars = await _carService.GetCars(filter);
        return Ok(cars);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var car = await _carService.GetById(id);
        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarRequest model, [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
        
        var carId = await _carService.Create(model, userId);
        return CreatedAtAction(nameof(GetCarById), new { id = carId }, carId);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarRequest model)
    {
        await _carService.Update(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        await _carService.Delete(id);
        return NoContent();
    }
}