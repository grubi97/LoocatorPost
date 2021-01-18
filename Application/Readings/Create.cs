using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Readings
{
    public class Create
    {
       
        public class Command : IRequest
        {
            public int Id { get; set; }
            public float Temperature { get; set; }
            public float Pressure { get; set; }
            public int SensorId { get; set; }
            public DateTime Date { get; set; }


        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
               

                var reding = new Reading
                {
                    Id = request.Id,
                    Temperature = request.Temperature,
                    Pressure = request.Pressure,
                    SensorId = request.SensorId,
                    Date = DateTime.UtcNow
 


                };

                

                 


                _context.Readings.Add(reding);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;//prazan objekt(vraća 200 ok)

                throw new Exception("Problem saving changes");

            }

             
        }
    }
}