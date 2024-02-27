using eTickets.Context;
using eTickets.Data.BaseRepository;
using eTickets.Data.Services.IServices;
using eTickets.Models;

namespace eTickets.Data.Services.Services
{
    public class ProducerService : BaseEntityRepository<Producer>, IProducerService
    {
        public ProducerService(AppDbContext context) : base(context) { }

    }
}
