using System;
using Core.Models;

namespace Core.DAO
{
    public interface ICotizacionRepository: IRepository<Cotizacion>
    {
        Cotizacion GetByCodigo(String codigo);
    }
}