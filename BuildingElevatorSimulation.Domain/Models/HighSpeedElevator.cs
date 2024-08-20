using BuildingElevatorSimulation.Infra.Constants;

namespace BuildingElevatorSimulation.Domain.Models
{
    public class HighSpeedElevator : Elevator
    {
        public HighSpeedElevator(int id, int capacity) : base(id, capacity)
        {
            Type = "HighSpeed"; // Set type for discriminating
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
            Thread.Sleep(500 * System.Math.Abs(floor - CurrentFloor) / 2);
            CurrentFloor = floor;
            IsMoving = false;
            CurrentDirection = DirectionEnum.Stationary;
        }
    }
}