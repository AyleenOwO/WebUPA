using System;
using Core.Controllers;
using Core.Models;
using Xunit;

namespace TestCore.Models
{
    public class UsuarioTest
    {
        [Fact]
        public void CreacionInicializacionTest()
        {
            Persona persona = new Persona()
            {
                Nombre = "Felipe",
                Email = "felipe@gmail.com",
                Materno = "jara",
                Paterno = "varas",
                Rut = "194517319"
            };

            Usuario usuario = new Usuario()
            {
                Persona = persona,
                Password = "123"
            };
            
            
        }
    }
}