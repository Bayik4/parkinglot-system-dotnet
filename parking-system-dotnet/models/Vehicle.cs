using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using parking_system_dotnet.constant;

namespace parking_system_dotnet.models
{
    public class Vehicle
    {
        public string LicensePlate { get; set; } = string.Empty;
        public VehicleType Type { get; set; }
        public string Color { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
    }
}