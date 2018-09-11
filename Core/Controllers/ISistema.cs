using System.Collections.Generic;
using Core.Models;

namespace Core.Controllers
{
    /// <summary>
    /// Operaciones del sistema.
    /// </summary>
    public interface ISistema
    {
        /// <summary>
        /// Operacion de sistema: Almacena una persona en el sistema.
        /// </summary>
        /// <param name="persona">Persona a guardar en el sistema.</param>
        void Save(Persona persona);

        /// <summary>
        /// Obtiene todas las personas del sistema.
        /// </summary>
        /// <returns>The IList of Persona</returns>
        IList<Persona> GetPersonas();

        /// <summary>
        /// Guarda a un usuario en el sistema
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="password"></param>
        void Save(Persona persona, string password);

        /// <summary>
        /// Guarda a una cotizacion en el sistema
        /// </summary>
        /// <param name="cotizacion"></param>
        void Save(Cotizacion cotizacion);

        /// <summary>
        /// Elimina a una cotizacion del sistema
        /// </summary>
        /// <param name="cotizacion"></param>
        void Eliminar(Cotizacion cotizacion);

        /// <summary>
        /// Entrega las cotizaciones
        /// </summary>
        /// <param name="rutMailCodigo"></param>
        /// <returns></returns>
        List<Cotizacion> FindCotizaciones(string rutMailCodigo);

        /// <summary>
        /// Obtiene el usuario desde la base de datos, verificando su login y password.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <param name="password">Contrasenia de acceso al sistema</param>
        /// <returns></returns>
        Usuario Login(string rutEmail, string password);

        /// <summary>
        /// Busqueda de una persona por rut o correo electronico.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <returns>La persona si existe</returns>
        Persona Find(string rutEmail);

        /// <summary>
        /// Busqueda de una cotizacion por id.
        /// </summary>
        /// <param name="id">id de cotizacion</param>
        /// <returns>La persona si existe</returns>
        Cotizacion Find(int id);
    }
}