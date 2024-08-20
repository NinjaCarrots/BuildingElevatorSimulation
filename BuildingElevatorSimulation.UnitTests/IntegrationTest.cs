using BuildingElevatorSimulation.Domain.Implementation;
using BuildingElevatorSimulation.Domain.Interfaces;
using BuildingElevatorSimulation.Domain.Models;
using BuildingElevatorSimulation.Infra;

namespace BuildingElevatorSimulation.UnitTests
{
    [TestFixture]
    public class IntegrationTest
    {
        private Building _building;
        private IElevatorService _elevatorService;

        [SetUp]
        public void Setup()
        {
            // Initialize the Building and ElevatorService for testing.
            _building = new Building(numberOfFloors: 10, numberOfElevators: 2, elevatorCapacity: 5);
            _elevatorService = new ElevatorService(_building);
        }

        [Test]
        public async Task ElevatorMovesToCorrectFloorAndLoadsPassengers()
        {
            // Arrange
            int floorToMove = 3;
            int passengersToLoad = 4;

            // Act
            await _elevatorService.CallElevatorAsync(floorToMove, passengersToLoad);
            var elevator = await _elevatorService.GetNearestElevatorAsync(floorToMove);

            // Assert
            Assert.NotNull(elevator);
            Assert.AreEqual(floorToMove, elevator.CurrentFloor);
            Assert.AreEqual(passengersToLoad, elevator.PassengerCount);
            Assert.IsFalse(elevator.IsMoving);
        }

        [Test]
        public async Task CallElevator_ElevatorOverCapacity_DoesNotLoadPassengers()
        {
            // Arrange
            int floorToMove = 3;
            int passengersToLoad = 10; // Exceeding capacity
            var building = new Building(numberOfFloors: 10, numberOfElevators: 1, elevatorCapacity: 5);
            _elevatorService = new ElevatorService(building);

            // Act
            await _elevatorService.CallElevatorAsync(floorToMove, passengersToLoad);
            var elevator = await _elevatorService.GetNearestElevatorAsync(floorToMove);

            // Assert
            Assert.NotNull(elevator);
            Assert.AreEqual(0, elevator.PassengerCount); // Passengers should not be loaded
        }

        [Test]
        public async Task ElevatorMovesToCorrectFloor()
        {
            // Arrange
            int initialFloor = 0;
            int targetFloor = 5;
            var building = new Building(numberOfFloors: 10, numberOfElevators: 1, elevatorCapacity: 5);
            var elevator = building.HighSpeedElevators.First();
            elevator.CurrentFloor = initialFloor;
            _elevatorService = new ElevatorService(building);

            // Act
            await _elevatorService.CallElevatorAsync(targetFloor, 0); // No passengers, just move
            await Task.Delay(1000); 

            // Assert
            Assert.AreEqual(targetFloor, elevator.CurrentFloor);
            Assert.IsFalse(elevator.IsMoving);
        }


        [Test]
        public void SaveAndLoadBuildingState_MultipleElevators_PreservesState()
        {
            // Arrange
            var building = new Building(numberOfFloors: 10, numberOfElevators: 5, elevatorCapacity: 10);
            _elevatorService = new ElevatorService(building);

            // Act
            ElevatorStateStorageFile.SaveBuildingState(building);
            var loadedBuilding = ElevatorStateStorageFile.LoadBuildingState();

            // Assert
            Assert.NotNull(loadedBuilding);
            Assert.AreEqual(building.NumberOfFloors, loadedBuilding.NumberOfFloors);
            Assert.AreEqual(building.HighSpeedElevators.Count + building.FreightElevators.Count,
                            loadedBuilding.HighSpeedElevators.Count + loadedBuilding.FreightElevators.Count);
        }

        [Test]
        public void BuildingInitialization_CorrectElevatorDistribution()
        {
            var building = new Building(numberOfFloors: 10, numberOfElevators: 6, elevatorCapacity: 5);
            Assert.AreEqual(3, building.HighSpeedElevators.Count);
            Assert.AreEqual(3, building.FreightElevators.Count);
        }

        [Test]
        public void ElevatorState_AfterSerializationDeserialization_IsPreserved()
        {
            var building = new Building(10, 3, 5);
            var elevator = building.HighSpeedElevators.First();
            elevator.MoveToFloor(5);
            elevator.LoadPassengers(3);

            ElevatorStateStorageFile.SaveBuildingState(building);
            var loadedBuilding = ElevatorStateStorageFile.LoadBuildingState();

            var loadedElevator = loadedBuilding.HighSpeedElevators.First();
            Assert.AreEqual(5, loadedElevator.CurrentFloor);
            Assert.AreEqual(3, loadedElevator.PassengerCount);
        }

        [Test]
        public void CallElevator_InvalidFloor_ThrowsException()
        {
            int invalidFloor = -1;
            int passengers = 2;
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _elevatorService.CallElevatorAsync(invalidFloor, passengers));
            Assert.AreEqual("Invalid floor number", ex.Message);
        }

        [Test]
        public void BuildingInitialization_ZeroElevators_HandledGracefully()
        {
            var building = new Building(10, 0, 5);
            Assert.AreEqual(1, building.Elevators.Count);  // Ensure at least one elevator is created
        }

        [Test]
        public async Task ElevatorOverCapacity_FailsToLoadPassengers()
        {
            int floor = 3;
            int overCapacityPassengers = 10;
            await _elevatorService.CallElevatorAsync(floor, overCapacityPassengers);
            var elevator = await _elevatorService.GetNearestElevatorAsync(floor);

            Assert.AreEqual(0, elevator.PassengerCount);  // No passengers should be loaded
        }

        [Test]
        public void SaveAndLoadBuildingState_MixedElevators_PreservesState()
        {
            var building = new Building(numberOfFloors: 10, numberOfElevators: 4, elevatorCapacity: 5);
            ElevatorStateStorageFile.SaveBuildingState(building);
            var loadedBuilding = ElevatorStateStorageFile.LoadBuildingState();

            Assert.AreEqual(building.NumberOfFloors, loadedBuilding.NumberOfFloors);
            Assert.AreEqual(building.HighSpeedElevators.Count, loadedBuilding.HighSpeedElevators.Count);
            Assert.AreEqual(building.FreightElevators.Count, loadedBuilding.FreightElevators.Count);
        }
    }
}