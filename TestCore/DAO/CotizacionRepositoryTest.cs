using System;
using System.Collections.Generic;
using System.Linq;
using Core.DAO;
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

            //Crear
            {
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
                cotizacion.Add(servicio);
                
                repository.Add(cotizacion);

                //Busqueda por rut  (exitosa)
                {
                    List<Cotizacion> busqueda = repository.GetByRut("194517319");
                    Assert.NotEmpty(busqueda);
                }
                
                //Busqueda por rut (no exitosa)
                {
                    List<Cotizacion> busqueda = repository.GetByRut("193992773");
                    Assert.Empty(busqueda);
                }
                
                //Busqueda por rut (no exitosa - rut nulo)
                {
                    List<Cotizacion> busqueda = repository.GetByRut(null);
                    Assert.Empty(busqueda);
                }
                
                //Todas las cotizaciones
                {
                    List<Cotizacion> busqueda = repository.GetAll().ToList();
                    Assert.NotEmpty(busqueda);
                }
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