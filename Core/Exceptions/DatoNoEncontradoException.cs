using System;

namespace Core.Exceptions
{
    public class DatoNoEncontradoException: Exception
    {
        public DatoNoEncontradoException(String mensaje) : base(mensaje)
        {
            
        }
    }
}