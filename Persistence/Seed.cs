using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {

            if (!context.Sensors.Any())
            {

                var sens = new List<Sensor>{

                 new Sensor{
                     Id=1,
                     Latitude=3.3,
                     Longitude=1.1,
                     Location="Osijek",
                     Name="Test",
                     Password="123",
                     


                 },
                 new Sensor{
                     Id=2,
                     Latitude=3.5,
                     Longitude=2.1,
                     Location="Djakovo",
                     Name="Test2",
                     Password="1234",
                    

                 }
             };






         context.Sensors.AddRangeAsync(sens);
         context.SaveChanges();


            }

        }
    }
}