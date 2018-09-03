using System;

namespace Core.Models
{
    
    enum EstadoS { PREPRODUCCION, GRABADO, MONTAJE, POSTPRODUCCION};
    
    public class Servicio : BaseEntity
    {
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
            
            throw new System.NotImplementedException();
        }
    }
}