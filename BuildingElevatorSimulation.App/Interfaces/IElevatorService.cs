using BuildingElevatorSimulation.Domain.Models;

namespace BuildingElevatorSimulation.Domain.Interfaces
{
    public interface IElevatorService
    {
        Task<Elevator> GetNearestElevatorAsync(int floor);
        Task<Results> CallElevatorAsync(int floor, int passengers);
        Task DisplayElevatorStatusAsync();
    }
}