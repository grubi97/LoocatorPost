using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;


namespace Application.Sensors
{
    public class Create
    {
        public class Command : IRequest
        {

            public int Id { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public String Password { get; set; }
            public String Name { get; set; }
            public String Location { get; set; }


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
                var key = "b14ca5898a4e4133bbce2ea2315a1916";
                var encryptedString = Security.EncryptString(key, request.Password);
                var sensor = new Sensor
                {
                    Id = request.Id,
                    Latitude = request.Latitude,
                    Location = request.Location,
                    Longitude = request.Longitude,
                    Name = request.Name,
                    Password = encryptedString,


                };

                _context.Sensors.Add(sensor);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;//prazan objekt(vraća 200 ok)

                throw new Exception("Problem saving changes");

            }
        }
    }
}