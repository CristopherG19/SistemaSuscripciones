using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IClienteService
    {
        void RegistrarCliente(Cliente cliente);
        List<Cliente> ObtenerClientes();
        Cliente ObtenerCliente(int id);
        void EditarCliente(Cliente cliente);
        void EliminarCliente(int id);
    }
}
