
using Acudir.Test.Apis.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Acudir.Test.Apis.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string NombreCompleto {  get; set; }

        [Range(0, 120)]
        public int Edad {  get; set; }

        [MaxLength(200)]
        public string Domicilio { get; set; }

        [Phone]
        public string Telefono     { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Profesion Profesion {get; set; }
    }
}
