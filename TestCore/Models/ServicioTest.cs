using Core.Models;
using Xunit;
using System;
using Core;

namespace TestCore.Models
{
    public class ServicioTest
    {
        [Fact]
        public void CreacionInicializacionTest()
        {
            Servicio servicio = new Servicio()
            {
                Estado = EstadoServicio.PREPRODUCCION,
                Nombre = "video",
                Precio = 100
            };
            
            //Validacion exitosa
            servicio.Validate();
            
            //precio negativo 
            {
                servicio.Precio = -23;
                Assert.Throws<ModelException>(() => servicio.Validate());
                servicio.Precio = 100;
            }
            
            //nombre vacio
            {
                servicio.Nombre = "";
                Assert.Throws<ModelException>(() => servicio.Validate());
            }
            
            //nombre nulo
            {
                servicio.Nombre = null;
                Assert.Throws<ModelException>(() => servicio.Validate());
            }
            
        }
    }
}