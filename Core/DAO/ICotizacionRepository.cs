using System;
using Core.Models;

namespace Core.DAO
{
    public interface ICotizacionRepository
    {
        Cotizacion GetByCodigo(String codigo);
    }
}