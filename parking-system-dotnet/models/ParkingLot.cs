using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parking_system_dotnet.models
{
    public class ParkingLot
    {
        public int TotalLots { get; private set; }
        public Dictionary<int, Vehicle> OccupiedLots { get; private set; } = new Dictionary<int, Vehicle>();

        public ParkingLot(int totalLots)
        {
            TotalLots = totalLots;
        }

        public bool CheckInVehicle(Vehicle vehicle)
        {
            if (OccupiedLots.Count >= TotalLots)
                return false;

            int availableLot = Enumerable.Range(1, TotalLots).Except(OccupiedLots.Keys).FirstOrDefault();
            OccupiedLots[availableLot] = vehicle;
            vehicle.CheckInTime = DateTime.Now;
            return true;
        }

        public bool CheckOutVehicle(string licensePlate, out double cost)
        {
            var lot = OccupiedLots.FirstOrDefault(l => l.Value.LicensePlate == licensePlate);

            if (lot.Key == 0)
            {
                cost = 0;
                return false;
            }

            var checkInTime = lot.Value.CheckInTime;
            cost = Math.Ceiling((DateTime.Now - checkInTime).TotalHours) * 5; // Example hourly rate is 5
            OccupiedLots.Remove(lot.Key);
            return true;
        }

        public int GetAvailableLotsCount() => TotalLots - OccupiedLots.Count;

        public Dictionary<string, int> GetVehicleCountByType() =>
            OccupiedLots.Values.GroupBy(v => v.Type.ToString())
                              .ToDictionary(g => g.Key, g => g.Count());

        public Dictionary<string, int> GetVehicleCountByColor() =>
            OccupiedLots.Values.GroupBy(v => v.Color)
                              .ToDictionary(g => g.Key, g => g.Count());

        public Dictionary<string, int> GetVehicleCountByPlateParity()
        {
            return new Dictionary<string, int>
            {
                { "Odd", OccupiedLots.Values.Count(v => int.Parse(v.LicensePlate[^1].ToString()) % 2 != 0) },
                { "Even", OccupiedLots.Values.Count(v => int.Parse(v.LicensePlate[^1].ToString()) % 2 == 0) }
            };
        }

        public bool IsLastCharacterDigit(string licensePlate)
        {
            return !string.IsNullOrEmpty(licensePlate) && char.IsDigit(licensePlate[^1]);
        }

        public int GetOccupiedLotsCount() => OccupiedLots.Count;
    }
}