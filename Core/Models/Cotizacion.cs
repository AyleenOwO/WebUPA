using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace Core.Models
{
    
    public class Cotizacion : BaseEntity
    {
        public EstadoCotizacion estado { get; set; }
        
        /// <summary>
        /// id asociada a la cotizacion asignada por la base de datos.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Persona asociada a la cotizacion.
        /// </summary>
        public Persona Persona { get; set; }
        
        /// <summary>
        /// fecha en la que se realizo la cotizacion.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Precio total de la cotizacion.
        /// </summary>
        public int Precio {
            get
            {
                int total = 0;
                foreach (var servicio in Servicios)
                {
                    total += servicio.Precio;
                }

                return total;
            }
        }

        /// <summary>
        /// Servicios que contiene la cotizacoon.
        /// </summary>
        public readonly List<Servicio> Servicios = new List<Servicio>();


        /// <inheritdoc cref="BaseEntity.Validate"/> 
        public override void Validate()
        {
            if (Persona == null)
            {
                throw new ModelException("Persona no puede ser null");
            }
            Persona.Validate();
            
            if (Precio == null)
            {
                throw new ModelException("Precio no puede ser null");
            }
            
            if(Servicios == null)
                throw new ModelException("La persona no puede ser null.");
            foreach (var servicio in Servicios)
            {
                servicio.Validate();
            }
            
        }

        /// <summary>
        /// Agrega un servicio a la cotizacion.
        /// </summary>
        /// <param name="servicio"></param>
        /// <exception cref="ModelException">Servicio que se agregara.</exception>
        public void Add(Servicio servicio)
        {
            if (servicio == null)
                throw new ModelException("El servicio no puede ser null");
            Servicios.Add(servicio);
        }
        
        
        /// <summary>
        /// Elimina un servicio de la cotizacion.
        /// </summary>
        /// <param name="index">Indice del servicio.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RemoveServicio(int index)
        {
            if(index < 0 || index >= Servicios.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            Servicios.RemoveAt(index);
        }

        /// <summary>
        /// Verifica igualdad de rut.
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public bool rutEquals(string rut)
        {
            return Persona.Rut.Equals(rut);
        }

        /// <summary>
        /// Actualiza la cotizacion.
        /// </summary>
        /// <param name="other">Cotizacion que contiene los nuevos datos</param>
        /// <exception cref="ArgumentException"></exception>
        public void Update(Cotizacion other)
        {
            if(other == null)
                throw new ArgumentException("La cotizacion de origen de los datos es nula.");
            Servicios.Clear();
            Servicios.AddRange(other.Servicios);

            estado = other.estado;
            
        }
    }
}