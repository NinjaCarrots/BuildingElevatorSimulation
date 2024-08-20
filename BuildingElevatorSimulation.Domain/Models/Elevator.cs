using BuildingElevatorSimulation.Infra.Constants;
using Newtonsoft.Json;

namespace BuildingElevatorSimulation.Domain.Models
{
    public abstract class Elevator
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public DirectionEnum CurrentDirection { get; set; }
        public bool IsMoving { get; set; }
        public int PassengerCount { get; set; }
        public int Capacity { get; set; }

        protected Elevator(int id, int capacity)
        {
            Id = id;
            CurrentFloor = 0;
            CurrentDirection = DirectionEnum.Stationary;
            IsMoving = false;
            PassengerCount = 0;
            Capacity = capacity;
        }

        public abstract void MoveToFloor(int floor);

        public bool CanAcceptPassengers(int count)
        {
            return PassengerCount + count <= Capacity;
        }

        public void LoadPassengers(int count)
        {
            if (CanAcceptPassengers(count))
            {
                PassengerCount += count;
            }
        }

        public void UnloadPassengers(int count)
        {
            PassengerCount -= System.Math.Min(PassengerCount, count);
        }
    }
}