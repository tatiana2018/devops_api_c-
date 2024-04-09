using Application.Validation;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Cars;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;

namespace Application.Cars
{
    public class AddCars
    {
        public class Query : IRequest<IActionResult>
        {
            public string Plate { get; set; }

            public int PickItUp { get; set; }

            public int Delivery { get; set; }

            public int Market { get; set; }

            public int Model { get; set; }

        }

        public class Handler : IRequestHandler<Query, IActionResult>
        {
            private readonly IRepositoryCars _repoCar;

            public Handler(IRepositoryCars iCar)
            {
                _repoCar = iCar;
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

                Car newCar = new();
                newCar.Plate = request.Plate;
                newCar.PickItUp = request.PickItUp;
                newCar.Delivery = request.Delivery;
                newCar.Market = request.Market;
                newCar.Model = request.Model;

                Car result = await _repoCar.AddCar(newCar);

                if (result != null)
                {
                    response.Message = "Se inserto exitosamente";
                    response.IsSuccess = true;
                    response.Code = "OK";
                    return Response.ObjResult(HttpStatusCode.OK, response.IsSuccess, response.Message, response.Code, result);
                } else
                {
                    response.Message = "Se genero un error, intente nuevamente";
                    response.IsSuccess = false;
                    response.Code = "ERROR";
                    return Response.ObjResult(HttpStatusCode.BadRequest, response.IsSuccess, response.Message, response.Code, result);
                }
            }
        }

    }
}