using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class Response
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public object Result { get; set; }

        public static ObjectResult ObjResult(HttpStatusCode httpCode, bool success, string message, string code, object result)
        {
            Response response = new Response();
            response.IsSuccess = success;
            response.Message = message;
            response.Result = result;
            response.Code = code;

            ObjectResult obj = new ObjectResult(response);
            obj.StatusCode = (int)httpCode;
            return obj;
        }
    }
}
