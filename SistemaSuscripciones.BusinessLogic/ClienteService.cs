using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repositorio;

        public ClienteService(IClienteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarCliente(Cliente cliente)
        {
            // Validaciones de negocio antes de registrar
            if (string.IsNullOrWhiteSpace(cliente.Nombres))
                throw new Exception("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellidos))
                throw new Exception("Los apellidos del cliente son obligatorios.");

            if (string.IsNullOrWhiteSpace(cliente.DocumentoIdentidad))
                throw new Exception("El documento de identidad es obligatorio.");

            _repositorio.Registrar(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return _repositorio.ListarTodo();
        }

        public Cliente ObtenerCliente(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public void EditarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombres))
                throw new Exception("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellidos))
                throw new Exception("Los apellidos del cliente son obligatorios.");

            _repositorio.Editar(cliente);
        }

        public void EliminarCliente(int id)
        {
            _repositorio.Eliminar(id);
        }
    }
}
