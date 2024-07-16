namespace Acudir.Test.Apis.Controllers
{
    using Acudir.Test.Apis.DTO;
    using Acudir.Test.Apis.Enums;
    using Acudir.Test.Apis.Models;
    using Acudir.Test.Apis.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPersonaService _PersonaService;
        public TestController(IPersonaService personaService) { 
            _PersonaService = personaService;
        }
        //Devolver una lista que retorne Personas
        [HttpGet("GetAll")]

        public ActionResult<List<Persona>>? GetAll(string? NombreCompleto, int? Edad, string? Domicilio, string? Telefono, Profesion? Profesion)
        {
            
            List<Persona> lista = _PersonaService.obtenerTodosLosRegistros();

            if (!string.IsNullOrEmpty(NombreCompleto))
            {
                lista = lista.Where(p => p.NombreCompleto.Contains(NombreCompleto)).ToList();
            }
            if (Edad != null)
            {
                lista = lista.Where(p => p.Edad == Edad).ToList();
            }

            if (!string.IsNullOrEmpty(Domicilio))
            {
                lista = lista.Where(p => p.Domicilio.Contains(Domicilio)).ToList();
            }

            if (!string.IsNullOrEmpty(Telefono))
            {
                lista = lista.Where(p => p.Telefono.Contains(Telefono)).ToList();
            }

            if (Profesion.HasValue)
            {
                lista = lista.Where(p => p.Profesion == Profesion.Value).ToList();
            }
            return lista;
        }


        //Post Guardar una Persona o mas
        [HttpPost("agregar")]
        public IActionResult agregarPersona(PersonaDTO persona) {


            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<Persona> lista = _PersonaService.obtenerTodosLosRegistros();
                int nuevaID = lista.Count > 0 ? lista.Max(persona => persona.Id) + 1 : 1;

                var nuevaPersona = new Persona
                {
                    Id = nuevaID,
                    NombreCompleto = persona.NombreCompleto,
                    Edad = persona.Edad,
                    Telefono = persona.Telefono,
                    Profesion = persona.Profesion,
                    Domicilio = persona.Domicilio
                };

                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                };


                lista.Add(nuevaPersona);
                var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(@"Data/Test.json", json);
                return Ok(nuevaPersona);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }


        /*
        Hice la edición completando todo el modelo personaDTO, porque le puse validaciones con DataAnnotations y con el protocolo http PUT
        al usar la verificación si el modelo era valido o no, toma todos como requeridos.

        Con el protocolo http Patch podría solamente generar un modelo parcial para la edición. Pero mantuve el protocolo http PUT. 
         */
        //Put Modificarlas
        [HttpPut("modificar/{id}")]
        public IActionResult modificar(int id, PersonaDTO personaDTO)
        {


            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<Persona> lista = _PersonaService.obtenerTodosLosRegistros();

                Persona personaAModificar = lista.Where(persona => persona.Id == id)
                                                 .First();
                if (personaAModificar == null)
                {
                    return NotFound($"La persona con ID = {id} no fue encontrada");
                }



                personaAModificar.NombreCompleto = personaDTO.NombreCompleto;
                personaAModificar.Edad = personaDTO.Edad;
                personaAModificar.Telefono = personaDTO.Telefono;
                personaAModificar.Profesion = personaDTO.Profesion;
                personaAModificar.Domicilio = personaDTO.Domicilio;


                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                };


                var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(@"Data/Test.json", json);
                return Ok(personaAModificar);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }

}
