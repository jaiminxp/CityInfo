using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CityController : ControllerBase
{
    private readonly CitiesDataStore _citiesDataStore;

    public CityController(CitiesDataStore citiesDataStore)
    {
        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    }

    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(_citiesDataStore.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

        return cityToReturn == null ? NotFound() : Ok(cityToReturn);
    }
}
