using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.DAO
{
    public interface ICotizacionRepository: IRepository<Cotizacion>
    {
        /// <summary>
        /// Obtiene las cotizaciones asociadas al rut.
        /// </summary>
        /// <param name="rut">rut de la persona a la se asocian las cotizaciones</param>
        /// <returns></returns>
        List<Cotizacion> GetByRut(String rut);
    }
}