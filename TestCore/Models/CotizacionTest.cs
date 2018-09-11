using System;
using Core.Models;
using Xunit;

namespace TestCore.Models
{
    public class CotizacionTest
    {
        [Fact]
        public void CreacionInicializacionTest()
        {
            Cotizacion cotizacion = new Cotizacion()
            {
                estado = EstadoCotizacion.BORRADOR,
                Fecha = DateTime.Now
            };
            
            Persona persona = new Persona()
            {
                Nombre = "Felipe",
                Email = "felipe@gmail.com",
                Materno = "jara",
                Paterno = "varas",
                Rut = "194517319"
            };
            
            
            
            Servicio servicio = new Servicio()
            {
                Estado = EstadoServicio.PREPRODUCCION,
                Nombre = "video",
                Precio = 100
            };
            
            //Insercion persona
            {
                cotizacion.Persona = persona;
                Assert.NotNull(cotizacion.Persona);
            }
            
            //Insercion Servicio
            {
                cotizacion.Add(servicio);
                Assert.NotEmpty(cotizacion.Servicios);
            }
            
            //comparacion de persona
            {
                Assert.True(cotizacion.rutEquals(persona.Rut));
            }
        }
    }
}