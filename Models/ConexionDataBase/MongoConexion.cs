using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ConexionDataBase
{
    public class MongoConexion
    {

         public string ConexionChain { get; set; } = null!;

         public string DataBase { get; set; } = null!;

         public string ColectionName { get; set; } = null!;
    }
}
