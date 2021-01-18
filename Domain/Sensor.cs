using System;
using System.Collections.Generic;

namespace Domain
{
    public class Sensor
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }

        virtual public List<Reading> Readings { get; set; }


    }
}