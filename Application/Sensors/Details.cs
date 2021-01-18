using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Sensors
{

    public class Details
    {
        public class Query : IRequest<Sensor>
        {

            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Sensor>
        {
            private readonly DataContext _context;


            public Handler(DataContext context)
            {
                _context = context;


            }

            public async Task<Sensor> Handle(Query request, CancellationToken cancellationToken)
            {
                var sensors = await _context.Sensors.Include(r => r.Readings).ToListAsync();

                var sensor = sensors.Find(s => s.Id == request.Id);








                return sensor;
            }


        }
    }
}