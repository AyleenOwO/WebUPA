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

        [Fact]
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
                //Agregar persona
                {
                    sistema.Save(persona);
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
                    Assert.Throws<System.Data.DataException>(() => sistema.FindCotizaciones("194441568"));

                }
                _output.WriteLine("Done..");
                _output.WriteLine("Probando busqueda por email...");
                //Busqueda por email  de cliente (exitosa)
                {
                    List<Cotizacion> busqueda = sistema.FindCotizaciones("felipevarasjara@gmail.com");
                    Assert.NotEmpty(busqueda);
                    Assert.NotNull(busqueda);
                }
                //Busqueda por email de cliente (no exitosa)
                {
                    Assert.Throws<System.Data.DataException>(() => sistema.FindCotizaciones("felipe_varas@gmail.com"));
                }

                _output.WriteLine("Done");
            }

        }
        
        [Fact]
        public void AllSavesTest()
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
                    estado = EstadoCotizacion.BORRADOR,
                    Fecha = DateTime.Now,
                    Persona = persona,
                };
                Persona personaNull = new Persona()
                {
                    Nombre = "",
                    Email = " ",
                    Materno = " ",
                    Paterno = "",
                    Rut = ""
                };

                Cotizacion cotizacionNull = new Cotizacion()
                {
                    estado = EstadoCotizacion.RECHAZADO,
                    Fecha = DateTime.Now,
                    Persona = null,
                };

                //Agregar servicio
                {
                    cotizacion.Add(servicio);
                    Assert.NotEmpty(cotizacion.Servicios);
                }
                _output.WriteLine("Done");
                _output.WriteLine("Probando guardar persona");
                //Guardar persona (exitosa)
                {
                   sistema.Save(persona);
                }
                //Guardar persona (no exitosa)
                {
                    Assert.Throws<Core.ModelException>(() => sistema.Save(personaNull));
                }
                _output.WriteLine("Done..");
                _output.WriteLine("Probando guardar cotizacion");
                //Guardar cotizacion (exitosa)
                {
                    sistema.Save(cotizacion);
                }
                //Guardar cotizacion (no exitosa)
                {
                    Assert.Throws<Core.ModelException>(() => sistema.Save(cotizacionNull));
                }
                _output.WriteLine("Done..");
                _output.WriteLine("Probando guardar Usuario");
                //Guardar Usuario (exitosa)
                {
                    sistema.Save(persona, "felipev123");
                }
                //Guardar Usuario (no exitosa)
                {
                    Assert.Throws<Core.ModelException>(() => sistema.Save(personaNull,null));
                    Assert.Throws<ArgumentNullException>(() => sistema.Save(persona,null));
                    Assert.Throws<Core.ModelException>(() => sistema.Save(personaNull,"felipev123"));
                }
                _output.WriteLine("Done..");

            }
        }

        [Fact]
        public void EliminarTest()
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
                    Id = 1,
                    estado = EstadoCotizacion.CANCELADO,
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
                _output.WriteLine("Testing find Cotizacion ..");
                // getCotizaciones
                {
                    Cotizacion cotizacionEliminada = sistema.Find(cotizacion.Id);
                    Assert.NotNull(cotizacionEliminada);
                    
                    _output.WriteLine("Testing Eliminar Cotizacion ..");
                    // Eliminar cotizacion (exitosa)
                    {
                        sistema.Eliminar(cotizacionEliminada);
                    }
                    // Eliminar cotizacion (no exitosa)
                    {
                        Assert.Throws<ArgumentException>(() => sistema.Eliminar(null));
                    }
                    _output.WriteLine("Done");

                }

            }
         
        }
        
        [Fact]
        public void FindPersonaCotizacionTest()
        {
            _output.WriteLine("Starting Sistema test ...");
            ISistema sistema = Startup.BuildSistema();

            //Crear persona
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
                Cotizacion cotizacion = new Cotizacion()
                {
                    Id = 1,
                    estado = EstadoCotizacion.ENVIADO,
                    Fecha = DateTime.Now,
                    Persona = persona,
                };
                //Agregar persona
                {
                    sistema.Save(persona);
                } 
                //Agregar cotizacion
                {
                    sistema.Save(cotizacion);
                }   
            }
            //Busqueda en blanco o null
            {
                Assert.Throws<System.Data.DataException>(() => sistema.Find(null));
                Assert.Throws<System.Data.DataException>(() => sistema.Find(""));
                Assert.Throws<System.Data.DataException>(() => sistema.Find(" "));
            }
            
            _output.WriteLine("Done..");
            _output.WriteLine("Probando busqueda por rut...");
            //Busqueda por rut  de cliente (exitosa)
            {
                Persona busqueda = sistema.Find("194517319");
                Assert.NotNull(busqueda);
            }
            //Busqueda por rut de cliente (no exitosa)
            {
                Assert.Throws<System.Data.DataException>(() => sistema.Find("194441568"));
            }
            
            _output.WriteLine("Done");
            _output.WriteLine("Probando busqueda por email...");
            //Busqueda por rut  de cliente (exitosa)
            {
                Persona busqueda = sistema.Find("felipevarasjara@gmail.com");
                Assert.NotNull(busqueda);
            }
            //Busqueda por rut de cliente (no exitosa)
            {
                Assert.Throws<System.Data.DataException>(() => sistema.Find("felipe_varas@gmail.com"));  
            }
            _output.WriteLine("Done..");
            _output.WriteLine("Probando busqueda por id...");
            //Busqueda por id de cotizacion (exitosa)
            {
                Cotizacion busqueda = sistema.Find(1);
                Assert.NotNull(busqueda);
            }
            //Busqueda por id de cotizacion (no exitosa)
            {
                Assert.Throws<System.Data.DataException>(() => sistema.Find(-1));  
            }
            _output.WriteLine("Done..");
            
            
        }
    }
}