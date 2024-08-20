namespace BuildingElevatorSimulation.Domain.Models
{
    public class Building
    {
        public int NumberOfFloors { get; set; }
        public List<HighSpeedElevator> HighSpeedElevators { get; set; }
        public List<FreightElevator> FreightElevators { get; set; }

        public Building(int numberOfFloors, int numberOfElevators = 1, int elevatorCapacity = 5)
        {
            NumberOfFloors = numberOfFloors;

            numberOfElevators = numberOfElevators > 0 ? numberOfElevators : 1;
            elevatorCapacity = elevatorCapacity > 0 ? elevatorCapacity : 5;

            HighSpeedElevators = new List<HighSpeedElevator>();
            FreightElevators = new List<FreightElevator>();

            for (int i = 0; i < numberOfElevators; i++)
            {
                if (i % 2 == 0)
                {
                    HighSpeedElevators.Add(new HighSpeedElevator(i + 1, elevatorCapacity));
                }
                else
                {
                    FreightElevators.Add(new FreightElevator(i + 1, elevatorCapacity));
                }
            }
        }

        public List<Elevator> Elevators
        {
            get
            {
                var allElevators = new List<Elevator>();
                allElevators.AddRange(HighSpeedElevators);
                allElevators.AddRange(FreightElevators);
                return allElevators;
            }
        }
    }
}