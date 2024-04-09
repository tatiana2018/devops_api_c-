using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Cars
{
    public class Car
    {
        /// <summary>
        /// Model for car entity
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; }

        [BsonElement("plate")]
        [Required(ErrorMessage = "El campo plate es requerido")]
        public string Plate { get; set; }

        [BsonElement("pickItUp")]
        [Required(ErrorMessage = "El campo pickItUp es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'pickItUp' debe ser mayor que 0.")]
        public int PickItUp { get; set; }


        [BsonElement("delivery")]
        [Required(ErrorMessage = "El campo delivery es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'delivery' debe ser mayor que 0.")]
        public int Delivery { get; set; }

        [BsonElement("market")]
        [Required(ErrorMessage = "El campo market es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'market' debe ser mayor que 0.")]
        public int Market { get; set; }


        [BsonElement("model")]
        [Required(ErrorMessage = "El campo model es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo 'model' debe ser mayor que 0.")]
        public int Model { get; set; }
    }
}
