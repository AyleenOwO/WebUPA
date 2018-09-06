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
        public List<Cotizacion> GetByRut(string rut)
        {
            return _dbContext.Set<Cotizacion>().Where(t => t.rutEquals(rut)).ToList();
        }
    }
}