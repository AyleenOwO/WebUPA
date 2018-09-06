using System;
using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DAO
{
    public class CotizacionRepository: ModelRepository<Cotizacion>, ICotizacionRepository
    {
        public CotizacionRepository(DbContext dbContext): base(dbContext)
        {
            
        }

        /// <inheritdoc />
        public List<Cotizacion> GetByRut(String rut)
        {
            Validate.ValidarRut(rut);
            List<Cotizacion> cotizaciones = _dbContext.Set<Cotizacion>().Where(t => t.rutEquals(rut)).ToList();
            if(cotizaciones.Count == 0)
                throw new DatoNoEncontradoException("Cotizacion no encontrada");
            return cotizaciones;
        }
    }
}