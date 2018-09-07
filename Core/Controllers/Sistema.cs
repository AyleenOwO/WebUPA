using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.DAO;
using Core.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Core.Controllers
{
    /// <summary>
    /// Implementacion de la interface ISistema.
    /// </summary>
    public sealed class Sistema : ISistema
    {
        // Patron Repositorio, generalizado via Generics
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
        private readonly IPersonaRepository _repositoryPersona;
        private readonly ICotizacionRepository _repositoryCotizacion;
        private readonly IRepository<Usuario> _repositoryUsuario;

        /// <summary>
        /// Inicializa los repositorios internos de la clase.
        /// </summary>
        public Sistema(IPersonaRepository repositoryPersona, IRepository<Usuario> repositoryUsuario, ICotizacionRepository repositoryCotizacion)
        {
            // Setter!
            _repositoryPersona = repositoryPersona ??
                                 throw new ArgumentNullException("Se requiere el repositorio de personas");
            _repositoryUsuario = repositoryUsuario ??
                                 throw new ArgumentNullException("Se requiere repositorio de usuarios");
            _repositoryCotizacion = repositoryCotizacion ??
                                    throw new ArgumentNullException("Se requiere repositorio de cotizaciones");

            // Inicializacion del repositorio.
            _repositoryPersona.Initialize();
            _repositoryUsuario.Initialize();
        }

        /// <inheritdoc />
        public void Save(Cotizacion cotizacion)
        {
            if(cotizacion == null)
                throw new ModelException("La cotizacion a guarar no puede ser null.");
            _repositoryCotizacion.Add(cotizacion);
        }

        public void Update(Cotizacion cotizacion)
        {
            if(cotizacion == null)
                throw new ArgumentException("La cotizacion es null.");
            Cotizacion oldCotizacion = _repositoryCotizacion.GetById(cotizacion.Id);
            if(oldCotizacion == null)
                throw new DataException("La cotizacion no existe en la base de datos.");
            oldCotizacion.Update(cotizacion);
        }

        public void Eliminar(Cotizacion cotizacion)
        {
            if (cotizacion == null)
                throw new ArgumentException("La cotizacion a eliminar es null");
            
            _repositoryCotizacion.Remove(cotizacion);
        }

        public List<Cotizacion> FindCotizaciones(string rutEmail)
        {
            Persona persona = Find(rutEmail);
            if(persona == null)
                throw new DataException("No existe persona asociada al rut/mail.");

            List<Cotizacion> cotizaciones = _repositoryCotizacion.GetByRut(persona.Rut);
            if(cotizaciones.Count == 0)
                throw new DataException("No hay cotizaciones asociadas a la persona.");

            return cotizaciones;
        }

        /// <inheritdoc />
        public void Save(Persona persona)
        {
            // Verificacion de nulidad
            if (persona == null)
            {
                throw new ModelException("Persona es null");
            }

            // Saving the Persona en el repositorio.
            // La validacion de los atributos ocurre en el repositorio.
            _repositoryPersona.Add(persona);
        }

        /// <inheritdoc />
        public IList<Persona> GetPersonas()
        {
            return _repositoryPersona.GetAll();
        }

        /// <inheritdoc />
        public void Save(Persona persona, string password)
        {
            // Guardo o actualizo en el backend.
            _repositoryPersona.Add(persona);

            // Busco si el usuario ya existe
            Usuario usuario = _repositoryUsuario.GetAll(u => u.Persona.Equals(persona)).FirstOrDefault();
            
            // Si no existe, lo creo
            if (usuario == null)
            {
                usuario = new Usuario()
                {
                    Persona =  persona
                };
            }
            
            // Hash del password
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(password);
            
            // Almaceno en el backend
            _repositoryUsuario.Add(usuario);
            
        }

        /// <inheritdoc />
        public Usuario Login(string rutEmail, string password)
        {
            Persona persona = _repositoryPersona.GetByRutOrEmail(rutEmail);
            if (persona == null)
            {
                throw new ModelException("Usuario no encontrado");
            }
            
            IList<Usuario> usuarios = _repositoryUsuario.GetAll(u => u.Persona.Equals(persona));
            if (usuarios.Count == 0)
            {
                throw new ModelException("Existe la Persona pero no tiene credenciales de acceso");
            }

            if (usuarios.Count > 1)
            {
                throw new ModelException("Mas de un usuario encontrado");
            }

            Usuario usuario = usuarios.Single();
            if (!BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                throw new ModelException("Password no coincide");
            }

            return usuario;

        }

        /// <inheritdoc />
        public Persona Find(string rutEmail)
        {
            return _repositoryPersona.GetByRutOrEmail(rutEmail);
        }
    }
}