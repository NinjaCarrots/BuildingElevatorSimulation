using BuildingElevatorSimulation.Infra.Constants;

namespace BuildingElevatorSimulation.Domain.Models
{
    public class FreightElevator : Elevator
    {
        public FreightElevator(int id, int capacity) : base(id, capacity)
        {
            Type = "Freight"; // Set type for discriminating
        }

        public override void MoveToFloor(int floor)
        {
            if (floor > CurrentFloor)
            {
                CurrentDirection = DirectionEnum.Up;
            }
            else if (floor < CurrentFloor)
            {
                CurrentDirection = DirectionEnum.Down;
            }
            else
            {
                CurrentDirection = DirectionEnum.Stationary;
            }

            IsMoving = true;
            Thread.Sleep(2000 * System.Math.Abs(floor - CurrentFloor)); // Slower for heavy loads
            CurrentFloor = floor;
            IsMoving = false;
            CurrentDirection = DirectionEnum.Stationary;
        }
    }
}