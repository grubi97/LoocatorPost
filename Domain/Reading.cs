using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Reading
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public int SensorId { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        public virtual Sensor Sensor { get; set; }
        public DateTime Date { get; set; }



    }
}