using System;
using System.Collections.Generic;

namespace Core.Models
{
    enum EstadoC { BORRADOR, ENVIADO, ACEPTADO, RECHAZADO, CANCELADO, COBRADO};
    
    public class Cotizacion : BaseEntity
    {
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
    }
}