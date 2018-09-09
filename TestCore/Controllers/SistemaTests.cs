using System;
using System.Collections.Generic;
using Core;
using Core.Controllers;
using Core.DAO;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Controllers
{
    /// <summary>
    /// Test del sistema
    /// </summary>
    public class SistemaTests
    {

        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public SistemaTests(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void LoginTest()
        {
            _output.WriteLine("Starting Sistema test ...");
            ISistema sistema = Startup.BuildSistema();

            // Insert persona
            {
                _output.WriteLine("Testing insert ..");
                Persona persona = new Persona()
                {
                    Rut = "130144918",
                    Nombre = "Diego",
                    Paterno = "Urrutia",
                    Materno = "Astorga",
                    Email = "durrutia@ucn.cl"
                };

                sistema.Save(persona);
            }

            // GetPersonas
            {
                _output.WriteLine("Testing getPersonas ..");
                Assert.NotEmpty(sistema.GetPersonas());
            }

            // Buscar persona
            {
                _output.WriteLine("Testing Find ..");
                Assert.NotNull(sistema.Find("durrutia@ucn.cl"));
                Assert.NotNull(sistema.Find("130144918"));
            }

            // Busqueda de usuario
            {
                Exception usuarioNoExiste =
                    Assert.Throws<ModelException>(() => sistema.Login("notfound@ucn.cl", "durrutia123"));
                Assert.Equal("Usuario no encontrado", usuarioNoExiste.Message);

                Exception usuarioNoExistePersonaSi =
                    Assert.Throws<ModelException>(() => sistema.Login("durrutia@ucn.cl", "durrutia123"));
                Assert.Equal("Existe la Persona pero no tiene credenciales de acceso",
                    usuarioNoExistePersonaSi.Message);
            }

            // Insertar usuario
            {
                Persona persona = sistema.Find("durrutia@ucn.cl");
                Assert.NotNull(persona);
                _output.WriteLine("Persona: {0}", Utils.ToJson(persona));

                sistema.Save(persona, "durrutia123");
            }

            // Busqueda de usuario
            {
                Exception usuarioExisteWrongPassword =
                    Assert.Throws<ModelException>(() => sistema.Login("durrutia@ucn.cl", "este no es mi password"));
                Assert.Equal("Password no coincide", usuarioExisteWrongPassword.Message);

                Usuario usuario = sistema.Login("durrutia@ucn.cl", "durrutia123");
                Assert.NotNull(usuario);
                _output.WriteLine("Usuario: {0}", Utils.ToJson(usuario));

            }

        }

        [Theory]
        public void FindCotizacionesTest()
        {
            _output.WriteLine("Starting Sistema test ...");
            ISistema sistema = Startup.BuildSistema();


            //Insert Cotizacion
            {
                _output.WriteLine("Testing insert ..");
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
                    estado = EstadoCotizacion.ACEPTADO,
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
                    sistema.Save(cotizacion);
                }
                _output.WriteLine("Done");
                _output.WriteLine(" busqueda null o vacio");

                //Busqueda en blanco o null
                {
                    Assert.Throws<ArgumentException>(() => sistema.FindCotizaciones(null));
                    Assert.Throws<ArgumentException>(() => sistema.FindCotizaciones(""));
                    Assert.Throws<ArgumentException>(() => sistema.FindCotizaciones(" "));
                }
                _output.WriteLine("Done..");
                _output.WriteLine("Probando busqueda por rut...");
                //Busqueda por rut  de cliente (exitosa)
                {
                    List<Cotizacion> busqueda = sistema.FindCotizaciones("194517319");
                    Assert.NotEmpty(busqueda);
                    Assert.NotNull(busqueda);
                }
                //Busqueda por rut de cliente (no exitosa)
                {
                    List<Cotizacion> busqueda = sistema.FindCotizaciones("194441568");
                    Assert.Empty(busqueda);
                    Assert.Null(busqueda);
                }
                _output.WriteLine("Done");
                _output.WriteLine("Probando busqueda por fecha...");
                {

                }
                _output.WriteLine("Done");
                _output.WriteLine("Probando busqueda por email...");
                {
                    
                }
                _output.WriteLine("Done");
                _output.WriteLine("Probando busqueda por texto...");
                {
                    
                }
            }

        }
        
    }
}