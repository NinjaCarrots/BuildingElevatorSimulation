using BuildingElevatorSimulation.Domain.Interfaces;
using BuildingElevatorSimulation.Domain.Models;

namespace BuildingElevatorSimulation.Domain.Implementation
{
    public class ElevatorService : IElevatorService
    {
        private readonly Building _building;

        public ElevatorService(Building building)
        {
            _building = building;
        }

        public async Task<Elevator> GetNearestElevatorAsync(int floor)
        {
            return await Task.Run(() =>
            {
                var allElevators = _building.HighSpeedElevators.Cast<Elevator>().Concat(_building.FreightElevators);

                return allElevators
                    .Where(e => !e.IsMoving)
                    .OrderBy(e => System.Math.Abs(e.CurrentFloor - floor))
                    .FirstOrDefault();
            });
        }

        public async Task<Results> CallElevatorAsync(int floor, int passengers)
        {
            var elevator = await GetNearestElevatorAsync(floor);
            if (elevator == null)
            {
                return new Results { Success = false, Message = "No available elevator at the moment." };
            }

            if (elevator.CanAcceptPassengers(passengers))
            {
                await Task.Run(() => elevator.MoveToFloor(floor));
                elevator.LoadPassengers(passengers);
                return new Results { Success = true, Message = $"Elevator {elevator.Id} arrived at floor {floor} with {elevator.PassengerCount} passengers." };
            }
            else
            {
                return new Results { Success = false, Message = "No available elevator can accommodate the passengers." };
            }
        }


        public async Task DisplayElevatorStatusAsync()
        {
            if (!_building.Elevators.Any())
            {
                Console.WriteLine("Error: No elevators found in the building.");
                return;
            }

            foreach (var elevator in _building.Elevators)
            {
                Console.WriteLine($"Elevator {elevator.Id} | Floor: {elevator.CurrentFloor} | Direction: {elevator.CurrentDirection} | Passengers: {elevator.PassengerCount}");
                await Task.Delay(100);
            }
        }
    }
}