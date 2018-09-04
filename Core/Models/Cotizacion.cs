using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace Core.Models
{
    
    public class Cotizacion : BaseEntity
    {
        public EstadoCotizacion estado;
        /// <summary>
        /// Identificador unico.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// fecha en la que se realizo la cotizacion.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Precio total de la cotizacion.
        /// </summary>
        public int Precio { get; set; }

        /// <summary>
        /// Servicios que contiene la cotizacoon
        /// </summary>
        public List<Servicio> Servicios = new List<Servicio>();


        /// <inheritdoc cref="BaseEntity.Validate"/> 
        public override void Validate()
        {
            if (String.IsNullOrEmpty(Codigo))
            {
                throw new ModelException("Codigo no puede ser null");
            }
            
            Models.Validate.ValidarFecha(Fecha.Day, Fecha.Month, Fecha.Year);
            
            if (Precio == null)
            {
                throw new ModelException("Precio no puede ser null");
            }
            
            ///Validar servicios??
            /// 
            throw new System.NotImplementedException();
            
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
                throw new ArgumentOutOfRangeException("Indice del servicio fuera de rango.");
            Servicios.RemoveAt(index);
        }
    }
}