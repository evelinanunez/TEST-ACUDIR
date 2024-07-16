using Acudir.Test.Apis.Models;

namespace Acudir.Test.Apis.Repositories
{
    public interface IPersonaRepository
    {

        List<Persona> obtenerTodos();
    }

    public class PersonaRepository : IPersonaRepository
    {
        private readonly List<Persona> _Personas;

        public PersonaRepository(List<Persona> personas) { 
            this._Personas = personas;
        }

        public List<Persona> obtenerTodos()
        {
            return _Personas;
        }
    }
}
