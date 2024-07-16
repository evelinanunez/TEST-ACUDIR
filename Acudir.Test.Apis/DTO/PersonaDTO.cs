using Acudir.Test.Apis.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Acudir.Test.Apis.DTO
{
    public class PersonaDTO
    {


        [MaxLength(200)]
        public string NombreCompleto { get; set; }

      
        [Range(0, 120)]
        public int Edad { get; set; }

  
        [MaxLength(200)]
        public string Domicilio { get; set; }

      
        [Phone]
        public string Telefono { get; set; }

        //public string Profesion {  get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Profesion Profesion { get; set; }
    }
}
