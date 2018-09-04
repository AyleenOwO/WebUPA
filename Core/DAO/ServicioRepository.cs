using System.Linq;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DAO
{
    public class ServicioRepository: ModelRepository<Servicio>, IServicioRepository
    {
        public ServicioRepository(DbContext dbContext) : base(dbContext){}
    }
}