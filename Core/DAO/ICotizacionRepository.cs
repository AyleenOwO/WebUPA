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

        /// <summary>
        /// Elimina la cotizacion de la base de datos.
        /// </summary>
        /// <param name="cotizacion"></param>
        void Remove(Cotizacion cotizacion);

        Cotizacion GetById(int id);
    }
}