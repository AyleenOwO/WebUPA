using System;
using System.Collections.Generic;
using System.Linq;
using Core.DAO;
using Core.Exceptions;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.DAO
{
    public class CotizacionRepositoryTest
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public CotizacionRepositoryTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException("Se requiere la consola");
        }

        [Fact]
        public void InsercionBusquedaCotizacionTest()
        {
            DbContext dbContext = BuildTestModelContext();
            CotizacionRepository repository = new CotizacionRepository(dbContext);
            
            Persona persona = new Persona()
            {
                Nombre = "Felipe",
                Email = "felipevarasjara@gmail.com",
                Materno = "Varas",
                Paterno = "jara",
                Rut = "194517319"
            };
            
            Servicio servicio = new Servicio()
            {
                Estado = EstadoServicio.PREPRODUCCION,
                Nombre = "video",
                Precio = 230000
            };
            Cotizacion cotizacion = new Cotizacion()
            {
                estado = EstadoCotizacion.BORRADOR,
                Fecha = DateTime.Now,
                Persona = persona,
            };
            
            //Agregar servicio
            {
                cotizacion.Add(servicio);
                Assert.NotEmpty(cotizacion.Servicios);
            }
            
            //Agregar cotizacion
            {
                repository.Add(cotizacion);
                Assert.NotEmpty(repository.GetAll());
            }
            
            //Busqueda por rut  (exitosa)
            {
                List<Cotizacion> busqueda = repository.GetByRut("194517319");
                Assert.NotEmpty(busqueda);
            }
            
            //Busqueda por rut (no exitosa)
            {
                List<Cotizacion> cotizaciones = repository.GetByRut("194441568");
                Assert.Empty(cotizaciones);
            }
            
            //Busqueda por rut (no exitosa - rut nulo)
            {
                List<Cotizacion> busqueda = repository.GetByRut(null);
                Assert.Empty(busqueda);
            }
            
        }
        
        /// <summary>
        /// Construccion del DbContext de prueba
        /// </summary>
        /// <returns></returns>
        private static DbContext BuildTestModelContext()
        {
            DbContextOptions<ModelDbContext> options = new DbContextOptionsBuilder<ModelDbContext>()
                // .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseSqlite(@"Data Source=cotizaciones.db") // SQLite
                .EnableSensitiveDataLogging()
                .Options;
            
            return new ModelDbContext(options);
        }
    }
}