using System;
using Core;
using Core.Models;
using Xunit;

namespace TestCore.Models
{
    /// <summary>
    /// Testing de la clase Persona.
    /// </summary>
    public class PersonaTests
    {
        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            Console.WriteLine("Creating Persona ..");
            Persona persona = new Persona()
            {
                Rut = "13014491-8",
                Nombre = "Diego",
                Paterno = "Urrutia",
                Materno = "Astorga"
            };

            Console.WriteLine(persona);
        }

        [Fact]
        public void TestEqual()
        {
            Persona persona1 = new Persona()
            {
                Rut = "194517319",
                Nombre = "Felipe",
                Paterno = "Varas",
                Materno = "Jara"
            };
            
            Persona persona2 = new Persona()
            {
                Rut = "194517319",
                Nombre = "Felipe",
                Paterno = "Varas",
                Materno = "Jara"
            };
            
            Assert.True(persona1.Equals(persona2));
        }

        [Fact]
        public void TestValidate()
        {
            //Persona correcta
            Persona persona1 = new Persona()
            {
                Rut = "194517319",
                Nombre = "Felipe",
                Paterno = "Varas",
                Materno = "Jara",
                Email = "felipe@gmail.com"
            };
            
            // Rut null
            {
                persona1.Rut = null;
            }
            var exception = Assert.Throws<ModelException>(() => persona1.Validate());

            //Nombre null
            {
                persona1.Rut = "194517319";
                persona1.Nombre = null;
                
                exception = Assert.Throws<ModelException>(() => persona1.Validate());
            }
            
            //Paterno null
            {
                persona1.Nombre = "Felipe";
                persona1.Paterno = null;
                Assert.Throws<ModelException>(() => persona1.Validate());
            }
            
            //Mail null
            {
                persona1.Materno = "Jara";
                persona1.Email = null;
                Assert.Throws<ModelException>(() => persona1.Validate());
            }
        }
    }
}