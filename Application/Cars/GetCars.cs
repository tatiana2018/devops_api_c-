using Application.Contracts;
using Application.Validation;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Cars;
using Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars
{
    public class GetCars
    {


        public class Query : IRequest<IActionResult>
        {
            public LocationFilters filters { get; set; }
        }

        public class Handler : IRequestHandler<Query, IActionResult>
        {
            private readonly IRepositoryCars _repoCar;
            private readonly IGeolocationService _geolocationService;
            private readonly IDefinedService _definedTypeService;


            public Handler(IRepositoryCars iCar, IGeolocationService iGeoService, IDefinedService iDefService)
            {
                _repoCar = iCar;
                _geolocationService = iGeoService;
                _definedTypeService = iDefService;

            }

            /// <summary>
            /// Realiza el llamado a la consulta pasando por requests los datos a utilizar.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns>Vehiculo insertado</returns>
            public async Task<IActionResult> Handle(Query request, CancellationToken cancellationToken)
            {
                Response response = new();

                try
                {
                    //area donde esta ubicado el usuario
                    int locationArea = await _geolocationService.GetZone(request.filters);

                    //definir tipo de carro

                    int carMarket = await _definedTypeService.DefinedCarType(locationArea, request.filters.CollectLocation);

                    List<Car> result = await _repoCar.GetCars(request.filters, carMarket);


                    if (result != null)
                    {
                        response.Message = "Se consulto exitosamente";
                        response.IsSuccess = true;
                        response.Code = "OK";
                        return Response.ObjResult(HttpStatusCode.OK, response.IsSuccess, response.Message, response.Code, result);
                    } else
                    {
                        response.Message = "Se genero un error, intente nuevamente";
                        response.IsSuccess = false;
                        response.Code = "ERROR";
                        return Response.ObjResult(HttpStatusCode.NotFound, response.IsSuccess, response.Message, response.Code, result);
                    }
                } catch(Exception ex)
                {
                    return Response.ObjResult(HttpStatusCode.BadRequest, response.IsSuccess, response.Message, response.Code, ex.Message);
                }
               
            }
        }
    }
}
