using BuildingElevatorSimulation.Domain.Implementation;
using BuildingElevatorSimulation.Domain.Interfaces;
using BuildingElevatorSimulation.Domain.Models;
using BuildingElevatorSimulation.Infra;


int numberOfFloors = 10;
int numberOfElevators = 3;
int elevatorCapacity = 5;

Building building = ElevatorStateStorageFile.LoadBuildingState();

if (building == null)
{
    Console.WriteLine("Failed to load building state. Creating a new building.");
    building = new Building(numberOfFloors, numberOfElevators, elevatorCapacity);
}
else
{
    Console.WriteLine("Loaded existing building state.");
}

IElevatorService elevatorService = new ElevatorService(building);

while (true)
{
    Console.Clear();

    // Display the current status of all elevators
    await elevatorService.DisplayElevatorStatusAsync();

    // Prompt user for input
    Console.WriteLine("\nEnter floor number to call an elevator (0 to exit): ");
    if (!int.TryParse(Console.ReadLine(), out int floor) || floor < 0 || floor > building.NumberOfFloors)
    {
        Console.WriteLine($"Invalid floor number. Please enter a valid number between 0 and {building.NumberOfFloors}.");
        continue;
    }

    // Exit condition
    if (floor == 0)
    {
        break;
    }

    // Prompt for the number of passengers
    Console.WriteLine("Enter the number of passengers waiting: ");
    if (!int.TryParse(Console.ReadLine(), out int passengers) || passengers < 0)
    {
        Console.WriteLine("Invalid number of passengers. Please enter a non-negative number.");
        continue;
    }

    // Call the elevator
    await elevatorService.CallElevatorAsync(floor, passengers);

    // Save the building state after every operation
    ElevatorStateStorageFile.SaveBuildingState(building);

    // Wait for user input before continuing
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}