Building Elevator Simulation

Overview
This project simulates the operation of elevators within a building. 
The simulation manages the state of multiple elevators, allowing users to call elevators to specific floors and simulate the loading of passengers.
The simulation handles different types of elevators (e.g., high-speed and freight elevators) and preserves the state across sessions using a file-based storage system.

Features
Multiple Elevator Types: The simulation includes both high-speed and freight elevators, each with distinct properties and behaviors.
State Persistence: The state of the building, including elevator positions and passenger counts, is saved to a file and loaded upon restarting the simulation.
Dynamic Passenger Management: Users can specify the number of passengers waiting at a floor, and the simulation will determine the best elevator to handle the request.
Elevator Movement Simulation: Elevators move to the requested floor, load passengers (if capacity allows), and update their state accordingly.

Getting Started

Prerequisites
.NET 8
Newtonsoft.Json library for JSON serialization

Installation
Clone the Repository:
git clone https://github.com/Sinethemba-Mndela/BuildingElevatorSimulation.git
cd BuildingElevatorSimulation

Restore Dependencies:

dotnet restore
Build the Project:
dotnet build

Run the Simulation:
dotnet run --project BuildingElevatorSimulation

Usage

Running the Simulation
When you run the simulation, it will either load a saved building state or initialize a new building with a specified number of floors, elevators, and elevator capacity.

The current state of all elevators is displayed, showing their current floor, direction, and the number of passengers.

You can call an elevator by entering the desired floor number and the number of passengers waiting.

The elevator will move to the requested floor, load the passengers (if it has capacity), and update its state.

The state of the building and elevators is saved automatically after each elevator call.

Exiting the Simulation
Enter 0 when prompted to exit the simulation. The state of the building will be saved automatically.

Project Structure

BuildingElevatorSimulation/
├── BuildingElevatorSimulation.Domain/
│   ├── Implementation/
│   ├── Interfaces/
├── BuildingElevatorSimulation.Domain/
│   └── Models/
├── BuildingElevatorSimulation.Infra/
│   ├── Constants/
│   ├── ElevatorStateStorageFile.cs
├── BuildingElevatorSimulation.UnitTests/
│   └── IntegrationTest.cs
├── BuildingElevatorSimulation.Common/
│   └── DirectionEnum.cs
├── BuildingElevatorSimulation.Presentation/
│   └── Program.cs
└── README.md

Key Classes
Building: Represents the building containing the elevators, including the list of floors and elevators.
Elevator: Abstract base class representing a generic elevator. Specific types like HighSpeedElevator and FreightElevator inherit from this class.
ElevatorService: Manages the interaction between the building and elevators, handling elevator requests and movements.
ElevatorStateStorageFile: Handles saving and loading the state of the building and elevators to and from a JSON file.

Testing
Unit tests are included in the BuildingElevatorSimulation.UnitTests project. These tests cover key scenarios, including:

Moving elevators to specific floors
Loading and unloading passengers
Persisting and restoring elevator state
Handling invalid inputs and edge cases

To run the tests, use the following command:
dotnet test

Contributing
Contributions are welcome! Please fork the repository and create a pull request with your changes. 
Ensure that your code follows the existing coding style and includes appropriate tests.

License
This project is licensed under the MIT License. See the LICENSE file for details.

Contact
For questions or suggestions, feel free to contact the project maintainers at mndelasinethemba@gmail.com.
