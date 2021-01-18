using System.Collections.Generic;
using System.Threading;
using MediatR;
using Domain;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Sensors
{
    public class List
    {

        public class Query : IRequest<List<Sensor>> { }

        public class Handler : IRequestHandler<Query, List<Sensor>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<List<Sensor>> Handle(Query request, CancellationToken cancellationToken)
            {

                var sensors = await _context.Sensors.Include(r=>r.Readings).ToListAsync();



                return sensors;
            }
        }



    }
}