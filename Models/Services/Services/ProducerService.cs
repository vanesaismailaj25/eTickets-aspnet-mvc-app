using eTickets.Context;
using eTickets.Models.BaseRepository;
using eTickets.Models.Services.IServices;

namespace eTickets.Models.Services.Services
{
    public class ProducerService : BaseEntityRepository<Producer>, IProducerService
    {
        public ProducerService(AppDbContext context) : base(context) { }

    }
}
