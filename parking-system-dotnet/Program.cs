using parking_system_dotnet.constant;
using parking_system_dotnet.models;

Console.WriteLine("Enter total number of lots:");
int totalLots = int.Parse(Console.ReadLine());

var parkingLot = new ParkingLot(totalLots);
string command;

do
{
    Console.WriteLine("\nCommands: checkin, checkout, report, exit");
    command = Console.ReadLine()?.ToLower();

    switch (command)
    {
        case "checkin":
            string licensePlate;
            while(true) {
                Console.WriteLine("Enter License Plate:");
                licensePlate = Console.ReadLine();

                if(parkingLot.IsLastCharacterDigit(licensePlate)) {
                    break;
                } else {
                    Console.WriteLine("End of License Plate must be a number");
                }
            }


            Console.WriteLine("Enter Vehicle Type (SmallCar/Motorbike):");
            VehicleType type = Enum.Parse<VehicleType>(Console.ReadLine(), true);

            Console.WriteLine("Enter Vehicle Color:");
            string color = Console.ReadLine();

            var vehicle = new Vehicle { LicensePlate = licensePlate, Type = type, Color = color };

            if (parkingLot.CheckInVehicle(vehicle))
                Console.WriteLine("Vehicle checked in successfully.");
            else
                Console.WriteLine("Parking full! Unable to check in.");
            break;

        case "checkout":
            Console.WriteLine("Enter License Plate:");
            string plateToCheckOut = Console.ReadLine();

            if (parkingLot.CheckOutVehicle(plateToCheckOut, out double cost))
                Console.WriteLine($"Vehicle checked out. Parking cost: {cost:C}");
            else
                Console.WriteLine("Vehicle not found in the parking lot.");
            break;

        case "report":
            Console.WriteLine($"Occupied Lots: {parkingLot.GetOccupiedLotsCount()}");
            Console.WriteLine($"Available Lots: {parkingLot.GetAvailableLotsCount()}");

            Console.WriteLine("Vehicles by Plate Parity:");
            foreach (var kvp in parkingLot.GetVehicleCountByPlateParity())
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");

            Console.WriteLine("Vehicles by Type:");
            foreach (var kvp in parkingLot.GetVehicleCountByType())
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");

            Console.WriteLine("Vehicles by Color:");
            foreach (var kvp in parkingLot.GetVehicleCountByColor())
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            break;

        case "exit":
            Console.WriteLine("Exiting system.");
            break;

        default:
            Console.WriteLine("Invalid command. Please try again.");
            break;
    }
} while (command != "exit");