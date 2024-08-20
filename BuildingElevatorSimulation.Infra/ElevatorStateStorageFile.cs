using Newtonsoft.Json;
using BuildingElevatorSimulation.Domain.Models;

namespace BuildingElevatorSimulation.Infra
{
    public static class ElevatorStateStorageFile
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "elevator_state.json");

        /// <summary>
        /// Saves the state of the building to a JSON file.
        /// </summary>
        /// <param name="building">The building object to save.</param>
        public static void SaveBuildingState(Building building)
        {
            try
            {
                // Serialize the building object to JSON
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented
                };

                var json = JsonConvert.SerializeObject(building, settings);

                // Write the JSON to the file
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving building state: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads the state of the building from a JSON file.
        /// </summary>
        /// <returns>The deserialized Building object, or null if the file doesn't exist or an error occurs.</returns>
        public static Building LoadBuildingState()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
                    };
                    var building = JsonConvert.DeserializeObject<Building>(json, settings);
                    return building;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading building state: {ex.Message}");
                return null;
            }
        }
    }
}