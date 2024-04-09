using Application.Cars;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Cars;

namespace API.Controllers
{
    public class CarsController : BaseController
    {

        /// <summary>
        /// Clase para los carros
        /// </summary>
        /// <param name="_mediator"></param>    
        public CarsController(IMediator _mediator)
        {
            Mediator = _mediator;
        }

        [HttpPost("AddCars")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCars([FromBody] AddCars.Query datos) 
        {
           return await Mediator.Send(datos);
        }

        [HttpGet("GetCars")]
        [ProducesResponseType(typeof (Car), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCars([FromQuery] GetCars.Query datos)
        {
            return await Mediator.Send(datos);
        }
    }
}