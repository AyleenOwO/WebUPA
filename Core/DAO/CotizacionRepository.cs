using System;
using System.Linq;
using Core.Exceptions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DAO
{
    public class CotizacionRepository: ModelRepository<Cotizacion>, ICotizacionRepository
    {
        CotizacionRepository(DbContext dbContext): base(dbContext)
        {
            
        }

        public Cotizacion GetByCodigo(String codigo)
        {
            Cotizacion cotizacion = _dbContext.Set<Cotizacion>().FirstOrDefault(t => t.Codigo.Equals(codigo))
                ?? throw new DatoNoEncontradoException("Cotizacion no encontrada");
            return cotizacion;
        }
    }
}