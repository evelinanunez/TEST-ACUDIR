using Acudir.Test.Apis.Models;
using Acudir.Test.Apis.Repositories;

namespace Acudir.Test.Apis.Services
{
    public interface IPersonaService
    {
        List<Persona> obtenerTodosLosRegistros();

    }

    public class PersonaService : IPersonaService
    {

        private readonly IPersonaRepository _PersonaRepository;
        public PersonaService(IPersonaRepository personaRepository) {
            this._PersonaRepository = personaRepository;
        }

        public List<Persona> obtenerTodosLosRegistros()
        {
            return _PersonaRepository.obtenerTodos();
        }
    }
}
