using System;

namespace Core.Models
{
    public class Servicio : BaseEntity
    {
        public EstadoServicio Estado { get; set; }
        /// <summary>
        /// Nombre del servicio
        /// </summary>
        public String Nombre { get; set; }
        
        /// <summary>
        /// Precio del servicio
        /// </summary>
        public int Precio { get; set; }
        
        public override void Validate()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new ModelException("Nombre no puede ser null");
            }
            
            if (Precio == null)
            {
                throw new ModelException("Precio no puede ser null");
            }
            
        }
    }
}