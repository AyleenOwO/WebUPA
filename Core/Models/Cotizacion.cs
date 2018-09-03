using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Cotizacion : BaseEntity
    {
        /// <summary>
        /// Identificador unico.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// fecha en la que se realizo la cotizacion.
        /// </summary>
        public string Fecha { get; set; }

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

            if (String.IsNullOrEmpty(Fecha))
            {
                throw new ModelException("Nombre no puede ser null o de tamanio inferior a 2");
            }

            if (Int16.TryParse())
            {
                throw new ModelException("Apellido Paterno no puede ser null o tamanio inferior a 2");
            }

            ///if ()
           /// {
              ///  throw new ModelException("Email no puede ser null o vacio.");
            ///}
            throw new System.NotImplementedException();
            
        }
    }
}