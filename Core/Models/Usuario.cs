using System;

namespace Core.Models
{
    /// <summary>
    /// Usuario del sistema
    /// </summary>
    public class Usuario : BaseEntity
    {
        /// <summary>
        /// Persona que representa a este usuario
        /// </summary>
        public Persona Persona { get; set; }
        
        /// <summary>
        /// Contrasenia de acceso de la Persona
        /// </summary>
        public string Password { get; set; }
        
        /// <inheritdoc cref="BaseEntity.Validate"/>
        public override void Validate()
        {
            if (Persona == null)
            {
                throw new ModelException("La persona no puede ser nula.");
            }

            if (Password == null || Password.Equals("") || Password.Equals(" "))
            {
                throw new ModelException("El usuario debe tener contraseña");
            }
        }

        public override bool Equals(object obj)
        {
            Usuario other = (Usuario) obj ?? throw new ArgumentException();
            return other.Persona.Equals(this.Persona);
        }
    }
}